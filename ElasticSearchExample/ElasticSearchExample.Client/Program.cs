// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ElasticSearchExample.Converter;
using ElasticSearchExample.CSV.Parser;
using ElasticSearchExample.Elastic.Client;
using ElasticSearchExample.Elastic.Client.Settings;
using ElasticSearchExample.Utils;
using TinyCsvParser;
using CsvStationType = ElasticSearchExample.CSV.Model.Station;
using CsvLocalWeatherDataType = ElasticSearchExample.CSV.Model.LocalWeatherData;

using ElasticLocalWeatherDataType = ElasticSearchExample.Elastic.Model.LocalWeatherData;


namespace ElasticSearchExample.ConsoleApp
{
    class Program
    {
        public static void Main(string[] args)
        {
            var uri = new Uri("http://localhost:9200");

            // Create a new Client, that writes the Weater Data and creates the Index weather_data:
            var client = new ElasticSearchClient<ElasticLocalWeatherDataType>(uri, "weather_data");

            // Creates the Index, if neccessary:
            client.CreateIndex();

            // Bulk Insert Data:
            foreach(var batch in GetData().Batch(100)) 
            {
                var response = client.BulkInsert(batch);

                if (response.Errors)
                {
                    response.TryGetServerErrorReason(out string reason);

                    Console.Error.WriteLine($"Bulk Write failed. Reason: {reason}");
                }
            }
        }

        private static IEnumerable<ElasticLocalWeatherDataType> GetData()
        {
            // Create Lookup Dictionary to map stations from:
            IDictionary<string, CsvStationType> stations =
                GetStations(@"D:\datasets\201503station.txt")
                .GroupBy(x => x.WBAN, x => x)
                .Select(x => x.First())
                .ToDictionary(station => station.WBAN, station => station);

            // Create the flattened Elasticsearch entry:
            return GetLocalWeatherData(@"D:\datasets\201503hourly.txt")
                .Where(x => stations.ContainsKey(x.WBAN))
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