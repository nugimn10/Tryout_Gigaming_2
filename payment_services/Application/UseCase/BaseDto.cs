namespace payment_services.Application.UseCase
{
    public abstract class BaseDto
    {
        public bool Success { set; get; }
        public string Message { set; get; }
        
    }
    public class RequestData<T>
    {
        public Data<T> Data { get; set; }
    }
    public class Data<T>
    {
        public T Attributes { get; set; }
    }
}