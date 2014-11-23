using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Geography.Country
{
    public interface ICountryService : INamedServiceBase<Country, CountrySearchParameters, CountryDto>
    {
        void UploadFlag(byte[] imageData, int? countryId);
        byte[] GetFlag(int? countryId);
    }
}