using System;
using System.Collections.Generic;

namespace FlareApi.Api
{
    public class ApiResponse<T>
    {
        public T? Data { get; }
        public IEnumerable<ApiError> Errors { get; } = Array.Empty<ApiError>();

        public ApiResponse(T value)
        {
            Data = value;
        }
        
        public ApiResponse(ApiError error)
        {
            Errors = new[] { error };
        }

        public ApiResponse(string name, string message, ApiError? cause = null)
        {
            Errors = new[] { new ApiError(name, message, cause) };
        }

        public ApiResponse(IEnumerable<ApiError> errors)
        {
            Errors = errors;
        }
    }

    public class ApiMetaResponse<T> : ApiResponse<IEnumerable<T>> where T : class
    {
        public Metadata Meta { get; }
        public object? Filter { get; }

        public ApiMetaResponse(IEnumerable<T> value, Metadata meta, object? filter) : base(value)
        {
            Meta = meta;
            Filter = filter;
        }
    }
}