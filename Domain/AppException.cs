using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class AppException
    {
        private string? stackTrace;
        private string v;

        public AppException(int statusCode, string v)
        {
            StatusCode = statusCode;
            this.v = v;
        }

        public AppException(int statusCode, string message, string? stackTrace)
        {
            StatusCode = statusCode;
            Message = message;
            this.stackTrace = stackTrace;
        }

        public int StatusCode { get; set; } 
        public string Message { get; set; } 
        public string? Details { get; set; } 
    }
}
