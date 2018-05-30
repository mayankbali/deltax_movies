using DeltaMovies.DeltaAPI.Services.Business;
using DeltaMovies.Framework.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DeltaMovies.Framework.Extension;
using DeltaMovies.DeltaAPI.Models;

namespace DeltaMovies.DeltaAPI
{
    [RoutePrefix("api/Actor")]
    public class ActorApiController : ApiController
    {
        [Route("All")]
        [HttpGet, HttpPost]
        public HttpResponseMessage GetAllActors()
        {
            var result = new ResponseContext<ActorResponseDTO>()
            {
                Status = ResponseStatus.Warning,
                Items = new List<ActorResponseDTO>() { },
                Message = "No Actor(s) found!"
            };
            DeltaMovieBL bussiness = new DeltaMovieBL();
            try
            {
                var response = bussiness.GetActorsBy(0);
                if (response.IsNotNullOrEmpty())
                {
                    result.Status = ResponseStatus.Success;
                    result.Items = response;
                }
            }
            catch (Exception Error)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseContext<ActorResponseDTO>()
                {
                    Status = ResponseStatus.Error,
                    Message = Error.Message,
                    Items = new List<ActorResponseDTO>() { }
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("GetActorBy/{actorId:int}")]
        [HttpGet, HttpPost]
        public HttpResponseMessage GetAllActors(int actorId)
        {
            var result = new ResponseContext<ActorResponseDTO>()
            {
                Status = ResponseStatus.Warning
            };
            DeltaMovieBL bussiness = new DeltaMovieBL();
            try
            {
                var response = bussiness.GetActorsBy(actorId);
                if (response.IsNotNullOrEmpty())
                {
                    result.Item = response.FirstOrDefault();
                }
            }
            catch (Exception Error)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseContext<MovieResponseDTO>()
                {
                    Status = ResponseStatus.Error,
                    Message = Error.Message
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("BindActors")]
        [HttpGet, HttpPost]
        public HttpResponseMessage ActorsDropdown()
        {
            DeltaMovieBL bussiness = new DeltaMovieBL();
            try
            {
                var response = bussiness.GetActorsBy(0);
                if (response.IsNotNullOrEmpty())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, response.OrderBy(o => o.Name).Select(r => new
                    {
                        Text = r.Name,
                        Value = r.ActorId,
                        Sex = r.Sex == "M" ? "Male" : "Female",
                        DOB = r.DOB.ToString("dd MMMM yyyy")
                    }));
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new
                    {

                    });
                }
            }
            catch (Exception Error)
            {
                string errorMessage = Error.Message;
                return Request.CreateResponse(HttpStatusCode.OK, new
                {

                });
            }
        }




        [HttpPost]
        [Route("Create")]
        [System.Web.Mvc.ValidateInput(false)]
        public HttpResponseMessage SaveActorInfo(ActorInfoRequestDTO request)
        {
            var propertyToValidate = ModelState["request.DOB"];
            if (propertyToValidate != null && propertyToValidate.Errors.Any())
            {
                ModelState.AddModelError("DOB", "Date Of Birth is required!");
            }
            if (ModelState.IsValid)
            {
                var result = new ResponseContext<ActorResponseDTO>()
                {
                    Status = ResponseStatus.Warning
                };
                DeltaMovieBL bussiness = new DeltaMovieBL();
                try
                {
                    var response = bussiness.SaveActorInfo(request);
                    if (response != null)
                    {
                        result.Item = new ActorResponseDTO()
                        {
                            ActorId = response.ActorId,
                            Name = response.Name
                        };
                        result.Status = ResponseStatus.Success;
                        result.Message = string.Format("{1} <b>{0}</b> is successfully saved.", response.Name, (request.Sex == "M" ? "Actor" : "Actress")).ToSuccessMessage();
                    }
                    else
                    {
                        result.Status = ResponseStatus.Warning;
                        result.Message = string.Format("{2} {0}({1}) is already exists. Are you sure you want to save?.", request.ActorName, request.DOB.ToString("dd/MM/yyyy"), (request.Sex == "M" ? "Actor" : "Actress"));
                    }
                }
                catch (Exception Error)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, new ResponseContext<MovieResponseDTO>()
                    {
                        Status = ResponseStatus.Error,
                        Message = Error.Message.ToErrorMessage()
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
