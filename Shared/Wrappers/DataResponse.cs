namespace Shared.Wrappers
{
    public class DataResponse<T> : BaseResponse where T : class
    {
        public T Data { get; set; }
    }
}
