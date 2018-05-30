using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeltaMovies.Models
{
    public class MovieModel
    {
        public long MovieId { get; set; }

       
        [Display(Name = "Movie Name :")]
        public string MovieName { get; set; }
         
        [Display(Name = "Year Of Release :")]
        public int YearOfRelease { get; set; }

        
        [Display(Name = "Plot :")]
        [DataType(DataType.MultilineText)]
        public string Plot { get; set; }

        [Display(Name = "Poster Image :")]
        public string PosterImage { get; set; } 

        
        [Display(Name = "Producer Name :")]
        public int ProducedBy { get; set; }

        
        [Display(Name = "Actor(s) :")]
        public int[] Actors { get; set; }

        public DateTime Modified { get; set; }

        public string LastModifiedOn { get; set; }
    }
}