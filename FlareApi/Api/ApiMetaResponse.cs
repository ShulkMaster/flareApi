using AutoWrapper.Wrappers;
using System.Collections.Generic;

namespace FlareApi.Api
{
    public class ApiMetaResponse<T> : ApiResponse
    {
        public Metadata Meta { get; }
        public object? Filter { get; }

        public ApiMetaResponse(
            IEnumerable<T> value,
            Metadata meta,
            int code = 200,
            object? filter = null
        ) : base(value, code)
        {
            Meta = meta;
            Filter = filter;
        }
    }
}