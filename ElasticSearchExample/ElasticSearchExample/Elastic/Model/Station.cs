// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Nest;
using System;

namespace ElasticSearchExample.Elastic.Model
{
    public class Station
    {
        [Text]
        public string WBAN { get; set; }

        [Text]
        public string Name { get; set; }

        [Text]
        public string State { get; set; }

        [Text]
        public string Location { get; set; }

        [GeoPoint]
        public GeoLocation GeoLocation { get; set; }
    }
}
