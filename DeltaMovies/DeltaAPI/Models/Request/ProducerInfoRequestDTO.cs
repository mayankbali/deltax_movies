﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DeltaMovies.DeltaAPI.Models
{
    public class ProducerInfoRequestDTO
    {
        public int ProducerId { get; set; }

        [Required(ErrorMessage = "Producer Name is required!")]
        [StringLength(150, ErrorMessage = "Producer Name Must be between {2} and {1} characters long.", MinimumLength = 0)]
        public string ProducerName { get; set; }


        [Required(ErrorMessage = "Sex is required!")]
        public string Sex { get; set; }

        [Required(ErrorMessage = "Date Of Birth is required!")]
        [DataType(DataType.DateTime)]
        public DateTime DOB { get; set; }

        [StringLength(500, ErrorMessage = "Bio Must be between {2} and {1} characters long.", MinimumLength = 0)]
        public string Bio { get; set; }

        public bool NameConfirm { get; set; }
    }
}