// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Elasticsearch.Net;
using ElasticSearchExample.Elastic.Client.Settings;
using log4net;
using Nest;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ElasticSearchExample.Elastic.Client
{
    public class ElasticSearchClient<TEntity>
        where TEntity : class
    {
        private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        public readonly string IndexName;

        protected readonly IElasticClient Client;

        public ElasticSearchClient(IElasticClient client, string indexName)
        {
            IndexName = indexName;
            Client = client;
        }

        public ElasticSearchClient(ConnectionString connectionString, string indexName)
            : this(CreateClient(connectionString), indexName)
        {
        }

        public ICreateIndexResponse CreateIndex()
        {
            var response = Client.IndexExists(IndexName);
            if (response.Exists)
            {
                return null;
            }
            return Client.CreateIndex(IndexName, index =>
                index.Mappings(ms =>
                    ms.Map<TEntity>(x => x.AutoMap())));
        }

        public IBulkResponse BulkInsert(IEnumerable<TEntity> entities)
        {
            var request = new BulkDescriptor();
            
            foreach (var entity in entities)
            {
                request
                    .Index<TEntity>(op => op
                        .Id(Guid.NewGuid().ToString())
                        .Index(IndexName)
                        .Document(entity));
            }

            return Client.Bulk(request);
        }

        private static IElasticClient CreateClient(ConnectionString connectionString)
        {
            var node = new UriBuilder(connectionString.Scheme, connectionString.Host, connectionString.Port);
            var connectionPool = new SingleNodeConnectionPool(node.Uri);
            var connectionSettings = new ConnectionSettings(connectionPool);

            return new ElasticClient(connectionSettings);
        }
    }
}
