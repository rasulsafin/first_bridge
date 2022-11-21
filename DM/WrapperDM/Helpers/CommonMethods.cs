using System;
using System.Collections.Generic;

namespace WrapperDM.Helpers;

public static class CommonMethods
{
    public static List<TSource> ToList<TSource>(IEnumerable<TSource> source)
    {
        if (source == null)
        {
            Console.WriteLine("Convert IEnumerable<TSource> to List went wrong");
            return null;
        }
        return new List<TSource>(source);
    }
}