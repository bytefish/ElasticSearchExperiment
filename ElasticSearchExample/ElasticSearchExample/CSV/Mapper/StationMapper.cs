// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using ElasticSearchExample.CSV.Model;
using TinyCsvParser.Mapping;

namespace ElasticSearchExample.CSV.Mapper
{
    public class StationMapper : CsvMapping<Station>
    {
        public StationMapper()
        {
            MapProperty(0, x => x.WBAN);
            MapProperty(1, x => x.WMO);
            MapProperty(2, x => x.CallSign);
            MapProperty(3, x => x.ClimateDivisionCode);
            MapProperty(4, x => x.ClimateDivisionStateCode);
            MapProperty(5, x => x.ClimateDivisionStationCode);
            MapProperty(6, x => x.Name);
            MapProperty(7, x => x.State);
            MapProperty(8, x => x.Location);
            MapProperty(9, x => x.Latitude);
            MapProperty(10, x => x.Longitude);
            MapProperty(11, x => x.GroundHeight);
            MapProperty(12, x => x.StationHeight);
            MapProperty(13, x => x.Barometer);
            MapProperty(14, x => x.TimeZone);
        }
    }
}
