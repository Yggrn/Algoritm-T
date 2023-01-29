using AlgoritmWeb.DBlayer.Enum;

namespace AlgoritmWeb.DBlayer.Response
{
    public class BaseResponse<TClass> : IBaseResponse<TClass>
    {
        public string? Description { get; set; }
        public StatusCode StatusCode { get; set; }
        public TClass Data { get; set; }
    }

    public interface IBaseResponse<TClass>
    {
        TClass Data { get; set; }
    }
}
