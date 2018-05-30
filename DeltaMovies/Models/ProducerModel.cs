using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeltaMovies.Models
{
    public class ProducerModel
    {
        public int ProducerId { get; set; }
         
        [Display(Name = "Name :")]
        public string ProducerName { get; set; }

        [Display(Name = "Sex :")]
        public string Sex { get; set; }

        [Display(Name = "Date Of Birth :")]
        public DateTime ProducerDOB { get; set; }
         
        [Display(Name = "Bio :")]
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }
         
        public DateTime Modified { get; set; }
        public string LastModifiedOn { get; set; }
    }
}