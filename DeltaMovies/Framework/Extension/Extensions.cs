using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace DeltaMovies.Framework.Extension
{
    public static class Extensions
    { 
        public static bool IsNotNullOrEmpty<TSource>(this IEnumerable<TSource> source) where TSource : class
        {
            return (source == null ? false : source.Count<TSource>() > 0);
        }

        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }
        public static string getConfigValue(this string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }

        public static int ToInt(this string current)
        {
            int result;
            int.TryParse(current, out result);
            return result;
        }

        public static string ToSuccessMessage(this string current)
        {
            return string.Format(@"<div class='alert alert-success'><strong>Success Message!</strong> {0} </div>", current);
        }

        public static string ToErrorMessage(this string current)
        {
            return string.Format(@"<div class='alert alert-danger'><strong>Error Message!</strong> {0} </div>", current);
        } 
    }
}