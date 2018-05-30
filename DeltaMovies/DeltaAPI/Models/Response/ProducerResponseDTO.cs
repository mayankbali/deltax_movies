using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaMovies.DeltaAPI
{
    public class ProducerResponseDTO
    {
        public ProducerResponseDTO()
        {
            ProducedMovies = new List<MovieResponseDTO>();
        }

        public int ProducerId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateTime DOB { get; set; }
        public string Bio { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public List<MovieResponseDTO> ProducedMovies { get; set; }
    }
}