// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ElasticSearchExample.CSV.Mapper;
using ElasticSearchExample.CSV.Model;
using TinyCsvParser;

namespace ElasticSearchExample.CSV.Parser
{
    public static class Parsers
    {
        public static CsvParser<Station> StationParser
        {
            get
            {
                CsvParserOptions csvParserOptions = new CsvParserOptions(true, '|');

                return new CsvParser<Station>(csvParserOptions, new StationMapper());
            }
        }

        public static CsvParser<LocalWeatherData> LocalWeatherDataParser
        {
            get
            {
                CsvParserOptions csvParserOptions = new CsvParserOptions(true, ',');

                return new CsvParser<LocalWeatherData>(csvParserOptions, new LocalWeatherDataMapper());
            }
        }
    }
}
