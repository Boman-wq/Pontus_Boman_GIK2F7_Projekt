using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Task4.Models
{
    /// <summary>
    /// All the Api routes
    /// </summary>
    public static class ApiRoutes
    {
        private const string ApiBase = "http://localhost:5000";
        /// <summary>
        /// Contains Get, Post, GetById, Put, Delete, Search
        /// </summary>
        public static class Library
        {
            public static readonly string Get = ApiBase + "/form";
            public static readonly string Post = ApiBase + "/form";
            public static readonly string GetById = ApiBase + "/form/";
            public static readonly string Put = ApiBase + "/form/";
            public static readonly string Delete = ApiBase + "/form/";
            public static readonly string Search = ApiBase + "/form/search?l%C3%A4rare=";

        }
    }
}
