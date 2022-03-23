using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace web_proj.Services.Response
{
    public class BaseResponse<T>
    {
        public string Message { get; private set;}
        public bool Success {get; private set;}

        public T Resource {get; private set;}

        public BaseResponse(bool success, string message,T Resource){
            Success = success;
            Message = string.Empty;
            this.Resource = Resource;
        }

        public BaseResponse(bool success, string Message){
            Success = success;
            this.Message = Message;
            this.Resource = default;
        }
    }
}