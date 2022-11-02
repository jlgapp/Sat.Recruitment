using System;

namespace Sat.Recruitment.Application.Errors
{
    public class CodeErrorResponse
    {
        public bool IsSuccess { get; set; }
        public int statusCode { get; set; }
        public string? Errors { get; set; }
        public CodeErrorResponse(int statusCode, string? message = null)
        {
            this.statusCode = statusCode;
            this.IsSuccess = false;
            this.Errors = message ?? GetDefaultMessageStatusCode(statusCode);
        }

        private string GetDefaultMessageStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "El request has errores",
                401 => "Unahthorized",
                404 => "Not Found",
                500 => "Server has errores",
                _ => String.Empty
            };
        }
    }

}
