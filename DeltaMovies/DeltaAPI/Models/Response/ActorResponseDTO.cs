using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaMovies.DeltaAPI
{
    public class ActorResponseDTO
    {
        public int ActorId { get; set; }
        public string Name { get; set; }
        public string Sex { get; set; }
        public DateTime DOB { get; set; }
        public string Bio { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}