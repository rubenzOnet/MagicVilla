using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Magic_Villa_Core.Response
{
    public class Response : BaseResponse, IResponseObject
    {
        public object Data { get; set; }

        public Response() { }

        public Response(object Data, bool IsValid, string Message, HttpStatusCode Type)
        {
            this.Data = Data;
            this.IsValid = IsValid;
            this.Message = Message;
            this.Type = Type;
        }
    }
}
