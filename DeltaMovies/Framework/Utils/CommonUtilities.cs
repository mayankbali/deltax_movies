using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaMovies.Framework.Utils
{
    public enum ResponseStatus
    {
        Error = 0,
        Warning = 1,
        Success = 2,
    }

    public class ResponseContext<T>
    {
        public ResponseContext()
        {

        }

        public T Item { get; set; }
        public IEnumerable<T> Items { get; set; }
        public string Message { get; set; }
        public ResponseStatus Status { get; set; }
    }
}