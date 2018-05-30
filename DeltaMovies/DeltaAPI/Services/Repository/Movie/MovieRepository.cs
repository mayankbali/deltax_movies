using DeltaMovies.DeltaAPI.DbEntities;
using DeltaMovies.DeltaAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System;
using DeltaMovies.Framework.Extension;

namespace DeltaMovies.DeltaAPI.Services.Repository
{
    public class MovieRepository : IMovieRepository
    {
        private DeltaMoviesEntities dbContext;
        public MovieRepository()
        {
            dbContext = new DeltaMoviesEntities();
        }

        public List<MovieInfo> MoviesBy(long movieId)
        {
            return dbContext.MovieInfoes.Where(m => m.MovieId == (movieId == 0 ? m.MovieId : movieId)).OrderByDescending(o => (o.ModifiedDate ?? o.CreatedDate)).ToList();
        }

        private MovieInfo UpdateMovieInfo(MovieRequestDTO movie)
        {
            var movieInfo = dbContext.MovieInfoes.Where(m => m.MovieId == movie.MovieId).FirstOrDefault();
            if (movieInfo != null)
            {
                using (var transaction = dbContext.Database.BeginTransaction())
                {
                    //update movie details.
                    try
                    {
                        movieInfo.Name = movie.MovieName;
                        movieInfo.Plot = movie.Plot;
                        movieInfo.Poster = movie.PosterImage;
                        movieInfo.ProducedBy = movie.ProducedBy;
                        movieInfo.Status = true;
                        movieInfo.YearOfRelease = movie.YearOfRelease;
                        movieInfo.ModifiedDate = DateTime.Now;
                        dbContext.SaveChanges();

                        var mappedActors = dbContext.ActorInMovies.Where(m => m.MovieId == movie.MovieId).ToList();
                        if (mappedActors.IsNotNullOrEmpty())
                        {
                            dbContext.ActorInMovies.RemoveRange(mappedActors);
                            dbContext.SaveChanges();
                        }

                        if (movie.Actors.Length > 0)
                        {
                            var actorinMovie = movie.Actors.Distinct().Select(a => new ActorInMovie()
                            {
                                ActorId = a,
                                MovieId = movieInfo.MovieId
                            }).ToList();
                            dbContext.ActorInMovies.AddRange(actorinMovie);
                            dbContext.SaveChanges();
                        }

                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }

                return movieInfo;
            }
            throw new Exception("No such movie found by your search to update the details.");
        }

        private MovieInfo SaveMovieInfo(MovieRequestDTO movie)
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    var movieInfo = new MovieInfo()
                    {
                        Name = movie.MovieName,
                        Plot = movie.Plot,
                        Poster = movie.PosterImage,
                        ProducedBy = movie.ProducedBy,
                        Status = true,
                        YearOfRelease = movie.YearOfRelease,
                        CreatedDate = DateTime.Now
                    };
                    dbContext.MovieInfoes.Add(movieInfo);
                    dbContext.SaveChanges();
                    if (movieInfo.MovieId > 0)
                    {
                        if (movie.Actors.Length > 0)
                        {
                            var actorinMovie = movie.Actors.Distinct().Select(a => new ActorInMovie()
                            {
                                ActorId = a,
                                MovieId = movieInfo.MovieId
                            }).ToList();
                            dbContext.ActorInMovies.AddRange(actorinMovie);
                            dbContext.SaveChanges();
                        }
                    }
                    transaction.Commit();
                    return movieInfo;
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public MovieInfo SaveEditMovieInfo(MovieRequestDTO movie)
        {
            if (movie.MovieId > 0)
            {
                return UpdateMovieInfo(movie);
            }
            return SaveMovieInfo(movie);
        }
    }
}