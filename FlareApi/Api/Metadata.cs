using System;

namespace FlareApi.Api
{
    public class Metadata
    {
        public int Total { get; }
        public int Pages { get; }
        public int? To { get; }
        public int? From { get; }
        public int Page { get; }
        public int Size { get; }

        public Metadata(PaginationRequest pagination, int total)
        {
            var (size, page) = pagination;
            Total = total;
            Size = size;
            if (size == 0)
            {
                Pages = 1;
                From = null;
                To = null;
                Page = 1;
            }
            else
            {
                From = page - 1 <= 0 ? null : (page - 1);
                Pages = (int) Math.Ceiling(total / (float) size);
                To = page + 1 > Pages ? null : (page + 1);
                Page = page;
            }
        }
    }
}