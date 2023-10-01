using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Villa_Core.Response
{
    public static class ResponseExtension
    {
        private static readonly ILogger<Response> _logger = NullLogger<Response>.Instance;
        public static string Serializer(this object source)
        {
            if (source == null)
                source = new object();
            return JsonConvert.SerializeObject(source);
        }

        public static Response ToResponse(this object Source, bool IsValid, HttpStatusCode Type, string Messages = "")
        {
            Response Response = new()
            {
                IsValid = IsValid,
                Type = Type,
                Data = Source,
                Message = Messages
            };
            return Response;
        }
        public static Response ToResponse(this object Source, string Messages = "")
        {
            return Source.ToResponse(true, HttpStatusCode.OK, Messages);
        }

        public static Response ToResponseExeption(this Exception E, bool IsValid, HttpStatusCode Type, string Messages = "There was an error with function")
        {
            Response Response = new()
            {
                IsValid = IsValid,
                Type = Type,
                Data = E.Message,
                Message = Messages
            };
            return Response;
        }
        public static Response ToResponseException(this Exception E, string Messages = "There was an error with function")
        {
            return E.ToResponseExeption(false, HttpStatusCode.InternalServerError, Messages);
        }

        public static Response ToResponseException(this Exception ex)
        {
            if (ex is CustomValidationException)
            {
                return ex.ToResponse(ex.Message);
            }
            else
            {
                _logger.LogError($"An unexpected error has occurred Ex: {ex}");
                throw ex;
            }
        }
    }
}
