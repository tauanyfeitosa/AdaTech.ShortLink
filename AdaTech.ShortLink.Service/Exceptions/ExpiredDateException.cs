
namespace AdaTech.ShortLink.Service.Exceptions
{
    public class ExpiredDateException: Exception
    {
        public ExpiredDateException() : base() { }

        public ExpiredDateException(string message) : base(message) { }

        public ExpiredDateException(string message, Exception inner) : base(message, inner) { }
    }
}
