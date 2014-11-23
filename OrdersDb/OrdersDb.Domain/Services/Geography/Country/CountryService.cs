using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using OrdersDb.Domain.Services.SystemServices;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Utils;
using OrdersDb.Domain.Wrappers;

// ReSharper disable PossibleInvalidOperationException

namespace OrdersDb.Domain.Services.Geography.Country
{
    public class CountryService : NamedServiceBase<Country, CountrySearchParameters, CountryDto>, ICountryService
    {
        private readonly Size CountryImageFlagSize = new Size(100, 100);
        private readonly IFileService _fileService;
        private HttpContext HttpContext
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public CountryService(IAppDbContext db, IObjectContext context, IFileService fileService)
            : base(db, context)
        {
            _fileService = fileService;
        }

        public override List<CountryDto> Search(CountrySearchParameters @params)
        {
            var query = Db.Set<Country>()
                .AsQueryable();

            query = SearchByIds(query, @params);
            query = SearchByName(query, @params);

            if (!string.IsNullOrEmpty(@params.Code))
                query = query.Where(x => x.Code.ToLower().Contains(@params.Code.ToLower()));

            if (!string.IsNullOrEmpty(@params.RussianName))
                query = query.Where(x => x.RussianName.ToLower().Contains(@params.RussianName.ToLower()));

            return query.OrderByTakeSkip(@params).Select(x => new CountryDto
                                                              {
                                                                  Id = x.Id,
                                                                  Name = x.Name,
                                                                  Code = x.Code,
                                                                  RussianName = x.RussianName,
                                                                  Flag = x.Flag
                                                              }).ToList();
        }

        public override CountryDto GetById(int id)
        {
            var query = Db.Countries.
                AsQueryable();

            var countryDto = query.Where(x => x.Id == id)
                .Select(x => new CountryDto { Id = x.Id, Name = x.Name, Code = x.Code, Flag = x.Flag, RussianName = x.Name })
                .Single();

            return countryDto;
        }

        public override void Add(Country entity)
        {
            Validate(entity);

            var fileName = GetFileName();
            if (!string.IsNullOrEmpty(fileName))
                entity.Flag = _fileService.ReadAllBytes(fileName);

            Db.AttachIfDetached(entity);
            Db.Entry(entity).State = EntityState.Added;
            Db.SaveChanges();
        }

        public byte[] GetFlag(int? countryId)
        {
            return countryId.IsNullOrZero()
                ? GetFlagTemporary()
                : GetFlagFromDb(countryId.Value);
        }

        public void UploadFlag(byte[] imageData, int? countryId = null)
        {
            if (countryId.IsNullOrZero())
                UploadFlagTemporary(imageData);
            else
                UploadFlagToDb(imageData, countryId.Value);
        }

        private void UploadFlagToDb(byte[] imageData, int id)
        {
            var country = new Country() { Id = id, Flag = imageData };
            Db.AttachIfDetached(country);
            Db.Entry(country).Property(x => x.Flag).IsModified = true;
            CountryFlagPath = string.Empty;
            Db.SaveChanges();
        }

        private byte[] GetFlagFromDb(int id)
        {
            return Db.Countries.Where(x => x.Id == id).Select(x => x.Flag).Single();
        }

        private byte[] GetFlagTemporary()
        {
            return !string.IsNullOrEmpty(CountryFlagPath) ? _fileService.ReadAllBytes(CountryFlagPath) : null;
        }

        private void UploadFlagTemporary(byte[] imageData)
        {
            _fileService.WriteAllBytes(GetFileName(), ProcessImage(imageData));
        }

        private byte[] ProcessImage(byte[] imageData)
        {
            var fullImage = ImageUtils.ConvertToJpg(imageData);
            var bitmap = fullImage.ToBitmap();
            var resized = ImageUtils.ResizeImage(bitmap, CountryImageFlagSize);
            return resized.ToByteArray(ImageFormat.Jpeg);
        }

        private string GetFileName()
        {
            var fileName = string.IsNullOrEmpty(CountryFlagPath)
                ? System.IO.Path.Combine(_fileService.GetTemporaryFolder(),
                    string.Format("{0}{1}.jpg", DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss_"), System.IO.Path.GetRandomFileName()))
                : CountryFlagPath;
            CountryFlagPath = fileName;
            return fileName;
        }

        private string CountryFlagPath
        {
            get
            {
                return HttpContext.Session["CountryFlagPath"] as string;
            }
            set
            {
                HttpContext.Session["CountryFlagPath"] = value;
            }
        }
    }
}
