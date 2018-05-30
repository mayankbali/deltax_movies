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
    [RoutePrefix("api/Producer")]
    public class ProducerApiController : ApiController
    {
        [HttpGet, HttpPost]
        [Route("All")]
        public HttpResponseMessage GetAllProducers()
        {
            var result = new ResponseContext<ProducerResponseDTO>()
            {
                Status = ResponseStatus.Warning,
                Items = new List<ProducerResponseDTO>() { },
                Message = "No Producer(s) found!"
            };
            DeltaMovieBL bussiness = new DeltaMovieBL();
            try
            {
                var response = bussiness.ProducerDetailsBy(0);
                if (response.IsNotNullOrEmpty())
                {
                    result.Items = response;
                }
            }
            catch (Exception Error)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseContext<ProducerResponseDTO>()
                {
                    Status = ResponseStatus.Error,
                    Message = Error.Message,
                    Items = new List<ProducerResponseDTO>() { }
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [HttpGet, HttpPost]
        [Route("GetProducerBy/{producerId:int}")]
        public HttpResponseMessage GetAllProducers(int producerId)
        {
            var result = new ResponseContext<ProducerResponseDTO>()
            {
                Status = ResponseStatus.Warning,
                Item = new ProducerResponseDTO() { }
            };
            DeltaMovieBL bussiness = new DeltaMovieBL();
            try
            {
                var response = bussiness.ProducerDetailsBy(producerId);
                if (response.IsNotNullOrEmpty())
                {
                    result.Item = response.FirstOrDefault();
                }
            }
            catch (Exception Error)
            {
                return Request.CreateResponse(HttpStatusCode.OK, new ResponseContext<ProducerResponseDTO>()
                {
                    Status = ResponseStatus.Error,
                    Message = Error.Message
                });
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        [Route("BindProducers")]
        [HttpGet, HttpPost]
        public HttpResponseMessage ProducersDropdown()
        {
            DeltaMovieBL bussiness = new DeltaMovieBL();
            try
            {
                var response = bussiness.ProducerDetailsBy(0);
                if (response.IsNotNullOrEmpty())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, response.OrderBy(o => o.Name).Select(r => new
                    {
                        Text = r.Name,
                        Value = r.ProducerId,
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
        public HttpResponseMessage SaveProducerInfo(ProducerInfoRequestDTO request)
        {
            var propertyToValidate = ModelState["request.DOB"];
            if (propertyToValidate != null && propertyToValidate.Errors.Any())
            {
                ModelState.AddModelError("DOB", "Date Of Birth is required!");
            }

            if (ModelState.IsValid)
            {
                var result = new ResponseContext<ProducerResponseDTO>()
                {
                    Status = ResponseStatus.Warning
                };
                DeltaMovieBL bussiness = new DeltaMovieBL();
                try
                {
                    var response = bussiness.SaveProducerInfo(request);
                    if (response != null)
                    {
                        result.Item = new ProducerResponseDTO()
                        {
                            ProducerId = response.ProducerId,
                            Name = response.Name
                        };
                        result.Status = ResponseStatus.Success;
                        result.Message = string.Format("Producer <b>{0}</b> is successfully saved.", response.Name).ToSuccessMessage();
                    }
                    else
                    {
                        result.Status = ResponseStatus.Warning;
                        result.Message = string.Format("Warning! Producer {0}([{2}]-{1}) is already exists. Are you sure you want to save?.", request.ProducerName, request.DOB.ToString("dd/MM/yyyy"), (request.Sex == "M" ? "Male" : "Female"));
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
