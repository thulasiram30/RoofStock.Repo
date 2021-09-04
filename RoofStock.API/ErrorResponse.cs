using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoofStock.API
{
    public class ErrorResponse
    {
        public ErrorResponse(int StatusCode, string message)
        {
            this.StatusCode = StatusCode;
            Message = message;
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
    }
}
