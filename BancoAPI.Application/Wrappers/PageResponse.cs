namespace BancoAPI.Application.Wrappers
{
    public class PageResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public PageResponse(T Data, int pageNumber, int pageSize)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = Data;
            this.Message = string.Empty;
            this.Errors = new List<string>();
            this.Succeeded = true;
        }
    }
}
