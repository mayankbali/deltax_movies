using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeltaMovies.Models
{
    public class ActorModel
    {
        public int ActorId { get; set; }
         
        [Display(Name = "Name :")]
        public string ActorName { get; set; }

        [Display(Name = "Sex :")]
        public string Sex { get; set; }

        [Display(Name = "Date Of Birth :")]
        public DateTime ActorDOB { get; set; }
         
        [Display(Name = "Bio :")]
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }
         
        public DateTime Modified { get; set; }
        public string LastModifiedOn { get; set; }
    }
}