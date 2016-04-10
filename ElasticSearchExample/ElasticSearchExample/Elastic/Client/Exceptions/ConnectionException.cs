// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using System.Runtime.Serialization;

namespace ElasticSearchExample.Elastic.Client.Exceptions
{
    [Serializable]
    public class ConnectionException : Exception
    {
        public ConnectionException()
            : base()
        {
        }

        public ConnectionException(string message)
            : base(message)
        {
        }

        public ConnectionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected ConnectionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
