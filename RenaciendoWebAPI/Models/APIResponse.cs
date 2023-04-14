﻿using System.Net;

namespace RenaciendoWebAPI.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public List<string>? ErrorsMessages { get; set; }
        public object? Result { get; set; }
    }
}