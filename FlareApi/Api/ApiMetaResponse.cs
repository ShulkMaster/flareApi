using AutoWrapper.Wrappers;
using System.Collections.Generic;

namespace FlareApi.Api
{
    public class ApiMetaResponse<T> : ApiResponse
    {
        public Metadata Meta { get; set; }
        public object? Filter { get; set; }

        public ApiMetaResponse(
            IEnumerable<T> value,
            Metadata meta,
            object? filter = null,
            int code = 200
        ) : base(value, code)
        {
            Meta = meta;
            Filter = filter;
        }

        public ApiMetaResponse(int code, object error, object? filter = null) : base(code, error)
        {
            Meta = new Metadata(new PaginationRequest
            {
                Page = 0,
                Size = 0,
            }, 0);
            Filter = filter;
        }
    }
}