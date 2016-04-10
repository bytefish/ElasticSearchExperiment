// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ElasticSearchExample.CSV.Parser;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TinyCsvParser;

using CsvStationType = ElasticSearchExample.CSV.Model.Station;
using CsvLocalWeatherDataType = ElasticSearchExample.CSV.Model.LocalWeatherData;

using ElasticLocalWeatherDataType = ElasticSearchExample.Elastic.Model.LocalWeatherData;

using ElasticSearchExample.Converter;
using ElasticSearchExample.Utils;
using ElasticSearchExample.Elastic.Client;
using ElasticSearchExample.Elastic.Client.Settings;


namespace ElasticSearchExample.Client
{
    class Program
    {
        public static void Main(string[] args)
        {
            var connectionString = new ConnectionString("http", "localhost", 9200);

            // Create a new Client, that writes the Weater Data and creates the Index weather_data:
            var client = new ElasticSearchClient<ElasticLocalWeatherDataType>(connectionString, "weather_data");

            // Creates the Index, if neccessary:
            client.CreateIndex();

            // Bulk Insert Data:
            foreach(var batch in GetData().Batch(100)) 
            {
                var response = client.BulkInsert(batch);
            }
        }

        private static IEnumerable<ElasticLocalWeatherDataType> GetData()
        {
            IDictionary<string, CsvStationType> stations =
                GetStations("C:\\Users\\philipp\\Downloads\\csv\\201503station.txt")
                .ToDictionary(station => station.WBAN, station => station);

            return GetLocalWeatherData("C:\\Users\\philipp\\Downloads\\csv\\201503hourly.txt")
                .Select(x =>
                {
                    var station = stations[x.WBAN];

                    return LocalWeatherDataConverter.Convert(station, x);
                });
        }

        private static IEnumerable<CsvStationType> GetStations(string fileName)
        {
            return Parsers.StationParser
                .ReadFromFile(fileName, Encoding.ASCII)
                .Where(x => x.IsValid)
                .Select(x => x.Result)
                .AsEnumerable();                
        }

        private static IEnumerable<CsvLocalWeatherDataType> GetLocalWeatherData(string fileName)
        {
            return Parsers.LocalWeatherDataParser
                .ReadFromFile(fileName, Encoding.ASCII)
                .Where(x => x.IsValid)
                .Select(x => x.Result)
                .AsEnumerable();
        }
    }
}