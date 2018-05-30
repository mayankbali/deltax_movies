using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaMovies.DeltaAPI
{
    public class MovieResponseDTO
    {
        public MovieResponseDTO()
        {
            ProducerInfo = new ProducerResponseDTO();
            ActorsInCast = new List<ActorResponseDTO>();
            PosterImageInfo = new PosterImageInfo();
        }
        public long MovieId { get; set; }
        public string Name { get; set; }
        public int YearOfRelease { get; set; }
        public string Plot { get; set; }

        public string Poster { get; set; }
        public PosterImageInfo PosterImageInfo { get; set; }

        public int ProducedBy { get; set; }
        public bool Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public List<ActorResponseDTO> ActorsInCast { get; set; }
        public ProducerResponseDTO ProducerInfo { get; set; }
    }

    public class PosterImageInfo
    {
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public long   FileSize { get; set; }
    }
}