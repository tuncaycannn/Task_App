﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UIPresantation.ApiHelper
{
    public interface IRestClientHelper
    {
        Task<string> GetAsync(string requestUri, Dictionary<string, string> additionalHeaders = null);
        Task<string> PostAsync<T>(string requestUri, T request, Dictionary<string, string> additionalHeaders = null) where T : class;
        Task<string> DeleteAsync(string requestUri, string jwt, Dictionary<string, string> additionalHeaders = null);
        Task<string> PutAsync<T>(string requestUri, T request, Dictionary<string, string> additionalHeaders = null) where T : class;
        Task<string> PatchAsync<T>(string requestUri, T request, Dictionary<string, string> additionalHeaders = null) where T : class;
    }

}
