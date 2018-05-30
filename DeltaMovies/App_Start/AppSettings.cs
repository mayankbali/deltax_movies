using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DeltaMovies.Framework.Extension;

namespace DeltaMovies
{
    public static class AppSettings
    {
        public static string PosterFilePath
        {
            get
            {
                return "PosterUploadPath".getConfigValue();
            }
        }

        public static string NoImageFilePath
        {
            get
            {
                return "NoImageFilePath".getConfigValue();
            }
        }
    }
}