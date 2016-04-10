// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

namespace ElasticSearchExample.Elastic.Client.Settings
{
    public class ConnectionString
    {
        public readonly string Scheme;

        public readonly string Host;

        public readonly int Port;

        public ConnectionString(string scheme, string host, int port)
        {
            Scheme = scheme;
            Host = host;
            Port = port;
        }

        public override string ToString()
        {
            return string.Format("ConnectionSettings(Host = {0}, Port = {1})", Host, Port);
        }
    }
}
