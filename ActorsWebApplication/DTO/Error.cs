﻿using System.Xml.Serialization;

namespace ActorsWebApplication.DTO
{
    public class Error
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string AdditionalInfo { get; set; }
    }
}
