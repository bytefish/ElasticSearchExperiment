// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Nest;

using CsvStationType = ElasticSearchExample.CSV.Model.Station;
using CsvLocalWeatherDataType = ElasticSearchExample.CSV.Model.LocalWeatherData;

using ElasticStationType = ElasticSearchExample.Elastic.Model.Station;
using ElasticLocalWeatherDataType = ElasticSearchExample.Elastic.Model.LocalWeatherData;


namespace ElasticSearchExample.Converter
{
    public static class LocalWeatherDataConverter
    {
        public static ElasticLocalWeatherDataType Convert(CsvStationType station, CsvLocalWeatherDataType localWeatherData)
        {
            return new ElasticLocalWeatherDataType
            {
                Station = new ElasticStationType 
                {
                    WBAN = station.WBAN,
                    Name = station.Name,
                    Location = station.Location,
                    State = station.State,
                    GeoLocation = new GeoLocation(station.Latitude, station.Longitude)
                },
                DateTime = localWeatherData.Date.Add(localWeatherData.Time),
                SkyCondition = localWeatherData.SkyCondition,
                StationPressure = localWeatherData.StationPressure,
                Temperature = localWeatherData.DryBulbCelsius,
                WindSpeed = localWeatherData.WindSpeed
            };
        }
    }
}
