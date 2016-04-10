// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ElasticSearchExample.CSV.Model
{
    public class Station
    {
        public string WBAN { get; set; }

        public string WMO { get; set; }

        public string CallSign { get; set; }

        public string ClimateDivisionCode { get; set; }

        public string ClimateDivisionStateCode { get; set; }

        public string ClimateDivisionStationCode { get; set; }

        public string Name { get; set; }

        public string State { get; set; }

        public string Location { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public string GroundHeight { get; set; }         

        public string StationHeight { get; set; }  

        public string Barometer { get; set; }  

        public string TimeZone { get; set; }            
    }
}
