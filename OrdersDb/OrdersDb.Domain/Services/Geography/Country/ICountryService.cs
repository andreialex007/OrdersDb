using System;
using System.Linq.Expressions;
using OrdersDb.Domain.Services._Common;

namespace OrdersDb.Domain.Services.Geography.Country
{
    public interface ICountryService : INamedServiceBase<Country, CountrySearchParameters, CountryDto>
    {
        void UploadImage(Expression<Func<Country, byte[]>> propertyLambda, byte[] imageData, int? countryId);
    }
}