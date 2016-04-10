// Copyright (c) Philipp Wagner. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System.Collections.Generic;
using System.Linq;
using TinyCsvParser.Tokenizer;

namespace ElasticSearchExample.CSV.Tokenizer
{
    public class FixedLengthTokenizer : ITokenizer
    {
        public class ColumnDefinition
        {
            public readonly int Start;

            public readonly int End;

            public ColumnDefinition(int start, int end)
            {
                Start = start;
                End = end;
            }
        }

        public readonly ColumnDefinition[] Columns;


        public FixedLengthTokenizer(ColumnDefinition[] columns)
        {
            Columns = columns;
        }

        public FixedLengthTokenizer(IList<ColumnDefinition> columns)
            : this(columns.ToArray())
        {
        }

        public string[] Tokenize(string input)
        {
            string[] tokenizedLine = new string[Columns.Length];
            
            for (int columnIndex = 0; columnIndex < Columns.Length; columnIndex++)
            {
                var columnDefinition = Columns[columnIndex];
                var columnData = input.Substring(columnDefinition.Start, columnDefinition.End - columnDefinition.Start);

                tokenizedLine[columnIndex] = columnData;
            }

            return tokenizedLine;            
        }
    }
}
