namespace OnlineCoursesPlatform.Application.Common.Exceptions
{
    public class UnauthenticatedException : Exception
    {
        public UnauthenticatedException(string message) : base(message){ }
    }
}
