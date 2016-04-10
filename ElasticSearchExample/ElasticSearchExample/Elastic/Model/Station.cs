// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Nest;
using System;

namespace ElasticSearchExample.Elastic.Model
{
    public class Station
    {
        [String]
        public string WBAN { get; set; }

        [String]
        public string Name { get; set; }

        [String]
        public string State { get; set; }

        [String]
        public string Location { get; set; }

        [GeoPoint]
        public GeoLocation GeoLocation { get; set; }
    }
}
