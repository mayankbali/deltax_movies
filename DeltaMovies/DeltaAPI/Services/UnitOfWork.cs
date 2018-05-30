using DeltaMovies.DeltaAPI.Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeltaMovies.DeltaAPI.Services
{
    public sealed class UnitOfWork : IDisposable
    {
        private IActorRepository actorRepo;
        private IMovieRepository movieRepo;
        private IProducerRepository producerRepo;

        public UnitOfWork()
        {

        }

        public IActorRepository actorRepository
        {
            get
            {
                if (this.actorRepo == null)
                {
                    this.actorRepo = new ActorRepository();
                }
                return actorRepo;
            }
        }

        public IMovieRepository movieRepository
        {
            get
            {
                if (this.movieRepo == null)
                {
                    this.movieRepo = new MovieRepository();
                }
                return movieRepo;
            }
        }

        public IProducerRepository producerRepository
        {
            get
            {
                if (this.producerRepo == null)
                {
                    this.producerRepo = new ProducerRepository();
                }
                return producerRepo;
            }
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}