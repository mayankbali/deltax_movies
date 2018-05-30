using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DeltaMovies.DeltaAPI.Models
{
    public class MovieRequestDTO
    {
        public long MovieId { get; set; }

        [Required(ErrorMessage = "Movie Name is required!")]
        [StringLength(150, ErrorMessage = "Movie Name Must be between {2} and {1} characters long.", MinimumLength = 0)]
        public string MovieName { get; set; }

        [Required(ErrorMessage = "Year Of Release is required!")]
        public int YearOfRelease { get; set; }

        [StringLength(500, ErrorMessage = "Plot Must be between {2} and {1} characters long.", MinimumLength = 0)]
        public string Plot { get; set; }

        public string PosterImage { get; set; }

        [Required(ErrorMessage = "Producer Name is required!")]
        public int ProducedBy { get; set; }

        [Required(ErrorMessage = "Actor is required!")]
        public int[] Actors { get; set; }

        public bool NameConfirm { get; set; }
    }
}