// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using Elasticsearch.Net;
using ElasticSearchExample.Elastic.Client.Settings;
using Nest;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace ElasticSearchExample.Elastic.Client
{
    public class ElasticSearchClient<TEntity>
        where TEntity : class
    {
        public readonly string IndexName;

        protected readonly IElasticClient Client;

        public ElasticSearchClient(IElasticClient client, string indexName)
        {
            IndexName = indexName;
            Client = client;
        }

        public ElasticSearchClient(Uri uri, string indexName)
            : this(CreateClient(uri), indexName)
        {
        }

        public CreateIndexResponse CreateIndex()
        {
            var response = Client.IndexExists(IndexName);
            if (response.Exists)
            {
                return null;
            }
            return Client.CreateIndex(IndexName, index => index.Map<TEntity>(ms => ms.AutoMap()));
        }

        public BulkResponse BulkInsert(IEnumerable<TEntity> entities)
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

        private static IElasticClient CreateClient(Uri uri)
        {
            var connectionPool = new SingleNodeConnectionPool(uri);
            var connectionSettings = new ConnectionSettings(connectionPool);

            return new ElasticClient(connectionSettings);
        }
    }
}