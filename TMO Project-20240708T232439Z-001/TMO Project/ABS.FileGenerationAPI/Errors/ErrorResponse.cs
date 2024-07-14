﻿namespace ABS.FileGenerationAPI.Exceptions
{
    public class ErrorResponse(int statusCode, string message, string? details)
    {
        public int StatusCode { get; set; } = statusCode;
        public string Message { get; set; } = message;
        public string? Details { get; set; } = details;
    }
}
