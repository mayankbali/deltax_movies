using DeltaMovies.DeltaAPI.DbEntities;
using DeltaMovies.DeltaAPI.Models;
using DeltaMovies.Framework.Extension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaMovies.DeltaAPI.Services.Business
{
    public sealed class DeltaMovieBL
    {
        public DeltaMovieBL()
        {

        }

        private List<MovieResponseDTO> MoviesByProducer(List<MovieInfo> movieInfoes)
        {
            var response = new List<MovieResponseDTO>();

            if (movieInfoes.IsNotNullOrEmpty())
            {
                response.AddRange(movieInfoes.Select(m => new MovieResponseDTO()
                {
                    MovieId = m.MovieId,
                    Name = m.Name,
                    YearOfRelease = m.YearOfRelease
                }));
            }

            return response;
        }

        private ProducerResponseDTO ProducerInfo(ProducerInfo producer)
        {
            if (producer != null)
            {
                return new ProducerResponseDTO()
                {
                    ProducerId = producer.ProducerId,
                    Bio = producer.Bio,
                    CreatedDate = producer.CreatedDate,
                    DOB = producer.DOB,
                    ModifiedDate = producer.ModifiedDate,
                    Name = producer.Name,
                    ProducedMovies = this.MoviesByProducer(producer.MovieInfoes.ToList()),
                    Sex = producer.Sex,
                    Status = producer.Status
                };
            }
            return new ProducerResponseDTO();
        }

        public List<MovieResponseDTO> GetMoviesBy(long movieId)
        {
            var result = new List<MovieResponseDTO>();
            using (var unitOfWork = new UnitOfWork())
            {
                var response = unitOfWork.movieRepository.MoviesBy(movieId);
                if (response.IsNotNullOrEmpty())
                {
                    result.AddRange(response.Select(r => new MovieResponseDTO()
                    {
                        ActorsInCast = r.ActorInMovies.Select(a => new ActorResponseDTO()
                        {
                            ActorId = a.ActorId,
                            Bio = a.ActorInfo.Bio,
                            ModifiedDate = a.ActorInfo.ModifiedDate,
                            CreatedDate = a.ActorInfo.CreatedDate,
                            DOB = a.ActorInfo.DOB,
                            Name = a.ActorInfo.Name,
                            Sex = a.ActorInfo.Sex,
                            Status = a.ActorInfo.Status
                        }).ToList(),
                        CreatedDate = r.CreatedDate,
                        ModifiedDate = r.ModifiedDate,
                        MovieId = r.MovieId,
                        Name = r.Name,
                        Plot = r.Plot,
                        Poster = r.Poster,
                        ProducedBy = r.ProducedBy,
                        ProducerInfo = this.ProducerInfo(r.ProducerInfo),
                        Status = r.Status,
                        YearOfRelease = r.YearOfRelease
                    }));
                }
            }

            return result;
        }

        public List<ProducerResponseDTO> ProducerDetailsBy(int producerId)
        {
            var result = new List<ProducerResponseDTO>();
            using (var unitOfWork = new UnitOfWork())
            {
                var response = unitOfWork.producerRepository.ProducerBy(producerId);
                if (response.IsNotNullOrEmpty())
                {
                    foreach (var producer in response)
                    {
                        result.Add(this.ProducerInfo(producer));
                    }
                }
            }

            return result;
        }

        public List<ActorResponseDTO> GetActorsBy(int actorId)
        {
            var result = new List<ActorResponseDTO>();
            using (var unitOfWork = new UnitOfWork())
            {
                var response = unitOfWork.actorRepository.ActorsBy(actorId);
                if (response.IsNotNullOrEmpty())
                {
                    result.AddRange(response.Select(r => new ActorResponseDTO()
                    {
                        ActorId = r.ActorId,
                        Bio = r.Bio,
                        CreatedDate = r.CreatedDate,
                        DOB = r.DOB,
                        ModifiedDate = r.ModifiedDate,
                        Name = r.Name,
                        Sex = r.Sex,
                        Status = r.Status
                    }));
                }
            }
            return result;
        }

        public MovieInfo SaveEditMovieInfo(MovieRequestDTO movie)
        {
            var result = new MovieInfo();
            using (var unitOfWork = new UnitOfWork())
            {
                if (movie.PosterImage.IsNullOrEmpty())
                    movie.PosterImage = AppSettings.NoImageFilePath;
                else
                    movie.PosterImage = string.Format("{0}/{1}", AppSettings.PosterFilePath.Replace("~", ""), movie.PosterImage.Replace(AppSettings.PosterFilePath.Replace("~", ""), ""));

                var response = unitOfWork.movieRepository.SaveEditMovieInfo(movie);
                if (response != null)
                {
                    result = response;
                }
            }
            return result;
        }

        public ActorInfo SaveActorInfo(ActorInfoRequestDTO actorInfo)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.actorRepository.SaveActorInfo(actorInfo);
            }
        }

        public ProducerInfo SaveProducerInfo(ProducerInfoRequestDTO producerInfo)
        {
            using (var unitOfWork = new UnitOfWork())
            {
                return unitOfWork.producerRepository.SaveProducerInfo(producerInfo);
            }
        }
    }
}