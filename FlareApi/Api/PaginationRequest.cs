namespace FlareApi.Api
{
    public class PaginationRequest
    {
        public int Size { get; set; } = 20;
        public int Page { get; set; } = 1;

        public void Deconstruct(out int size, out int page)
        {
            size = Size;
            page = Page;
        }
    }
    
    public class ZeroPageRequest: PaginationRequest { }
}