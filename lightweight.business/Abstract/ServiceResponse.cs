using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace lightweight.business.Abstract
{
    [Serializable]
    public class ServiceResponse<T>
    {
        public bool HasExceptionError { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string ExceptionMessage { get; set; }

        public IList<T> List { get; set; }

        [JsonProperty]
        public T Entity { get; set; }

        public int Count { get; set; }

        public bool IsValid => !HasExceptionError  && string.IsNullOrEmpty(ExceptionMessage);

        public bool IsSuccessful { get; set; }

        public ServiceResponse(HttpContext context)
        {
            List = new List<T>();
        }
    }
}