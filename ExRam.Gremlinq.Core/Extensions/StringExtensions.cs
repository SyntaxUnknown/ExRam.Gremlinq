﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExRam.Gremlinq.Core.Extensions
{
    internal static class StringExtensions
    {
        public static string ToCamelCase(this string source)
        {
            if (source == null || source.Length < 2)
                return source;

           return source.Substring(0, 1).ToLower() + source.Substring(1);
        }
    }
}
