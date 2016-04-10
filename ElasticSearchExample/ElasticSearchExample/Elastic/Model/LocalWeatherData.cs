// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Nest;
using System;

namespace ElasticSearchExample.Elastic.Model
{
    public class LocalWeatherData
    {
        [Nested(IncludeInParent=true)]
        public Station Station { get; set; }

        [Date]
        public DateTime DateTime { get; set; }

        [Number]
        public float Temperature { get; set; }

        [Number]
        public float WindSpeed { get; set; }

        [Number]
        public float StationPressure { get; set; }

        [String]
        public string SkyCondition { get; set; }
    }
}