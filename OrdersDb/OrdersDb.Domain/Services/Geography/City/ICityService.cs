using System.Collections.Generic;
using OrdersDb.Domain.Services._Common;
using OrdersDb.Domain.Services._Common.Entities;

namespace OrdersDb.Domain.Services.Geography.City
{
    public interface ICityService : INamedServiceBase<City, CitySearchParameters, CityDto>
    {
        /// <summary>
        /// �������� ��� ������ � �������
        /// </summary>
        List<City> GetCitiesInRegion(string regionName);

        /// <summary>
        /// �������� ��������� �������� �������
        /// </summary>
        List<City> GetCities(int take, int skip);

        List<NameValue> GetCitiesInRegion(int regionId);
    }
}