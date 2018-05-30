using DeltaMovies.DeltaAPI.Models;
using DeltaMovies.DeltaAPI.Services;
using DeltaMovies.DeltaAPI.Services.Business;
using DeltaMovies.Framework.Extension;
using DeltaMovies.Framework.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace DeltaMovies.DeltaAPI
{
    [RoutePrefix("api/Movies")]
    public class MovieApiController : ApiController
    {
        [Route("All")]
        public HttpResponseMessage GetAllMovies()
        {
            var result = new ResponseContext<MovieResponseDTO>()
            {
                Status = ResponseStatus.Warning,
                Items = new List<MovieResponseDTO>() { },
                Message = "No Movie(s) found!" 
            };
            DeltaMovieBL bussiness = new DeltaMovieBL();
            try
            {
                var response = bussiness.GetMoviesBy(0);
                if (response.IsNotNullOrEmpty())
                {
                    result.Items = response;
                    result.Status = ResponseStatus.Success;
                    result.Message = string.Format("Total {0} Movie(s) found to display.", response.Count).ToSuccessMessage();
                }
            }
            catch (Exception Error)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseContext<MovieResponseDTO>()
                {
                    Status = ResponseStatus.Error,
                    Message = Error.Message.ToErrorMessage(),
                    Items = new List<MovieResponseDTO>()
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("GetMovieBy/{movieId:long}")]
        public HttpResponseMessage GetAllMovies(long movieId)
        {
            var result = new ResponseContext<MovieResponseDTO>()
            {
                Status = ResponseStatus.Warning
            };
            DeltaMovieBL bussiness = new DeltaMovieBL();
            try
            {
                var response = bussiness.GetMoviesBy(movieId);
                if (response.IsNotNullOrEmpty())
                {
                    result.Item = response.FirstOrDefault();
                    if ((result.Item.Poster ?? "").Length > 0)
                    {
                        var fileName = Path.GetFileName(result.Item.Poster);
                        var physicalPath = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath(AppSettings.PosterFilePath), fileName);
                        if (System.IO.File.Exists(physicalPath))
                        {
                            var fileInfoBy = new FileInfo(physicalPath);
                            result.Item.PosterImageInfo = new PosterImageInfo()
                            {
                                FileExtension = fileInfoBy.Extension,
                                FileName = fileInfoBy.Name,
                                FileSize = fileInfoBy.Length
                            };
                        }
                    }
                    result.Status = ResponseStatus.Success;
                    result.Message = string.Format("Movie info for {0}.", result.Item.Name);
                }
                else {
                    result.Status = ResponseStatus.Warning;
                    result.Message = "No such movie found by your search to update the details.".ToErrorMessage();
                }
            }
            catch (Exception Error)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseContext<MovieResponseDTO>()
                {
                    Status = ResponseStatus.Error,
                    Message = Error.Message.ToErrorMessage(),
                    Item = new MovieResponseDTO()
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }


        [HttpPost]
        [Route("CreateEdit")]
        [System.Web.Mvc.ValidateInput(false)]
        public HttpResponseMessage CreateEditMovie(MovieRequestDTO request)
        {
            if (ModelState.IsValid)
            {
                var result = new ResponseContext<MovieResponseDTO>()
                {
                    Status = ResponseStatus.Warning
                };
                DeltaMovieBL bussiness = new DeltaMovieBL();
                try
                {
                    var response = bussiness.SaveEditMovieInfo(request);
                    if (response != null)
                    {
                        result.Item = new MovieResponseDTO()
                        {
                            MovieId = response.MovieId,
                            Name = response.Name
                        };
                        result.Status = ResponseStatus.Success;
                        result.Message = string.Format("Movie <b>{0}</b> is successfully save/updated.", response.Name).ToSuccessMessage();
                    }
                }
                catch (Exception Error)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseContext<MovieResponseDTO>()
                    {
                        Status = ResponseStatus.Error,
                        Message = Error.Message.ToErrorMessage(),
                        Item = new MovieResponseDTO() { }
                    });
                }
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            else
            {
                string messages = string.Join(string.Empty, ModelState.Values
                                       .SelectMany(x => x.Errors)
                                       .Select(x => string.Format("<p>{0}</p>", x.ErrorMessage)));
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseContext<MovieResponseDTO>()
                {
                    Status = ResponseStatus.Error,
                    Message = messages.ToErrorMessage(),
                    Item = new MovieResponseDTO() { }
                });

            }
        }
    }
}