namespace CourseManagerInterface.Presentation.Requests
{
    public class RequestsService
    {
        private Func<Type, Request> _requestFactory;

        public RequestsService(Func<Type, Request> requestFactory)
        {
            _requestFactory = requestFactory;
        }

        public void MakeRequest<TRequest>(object? args)
        {
            var targetRequest = _requestFactory.Invoke(typeof(TRequest));
            try
            {
                targetRequest.Execute(args);
            }
            catch
            {

            }
        }

        public void MakeRequest<TRequest>(object? args, Action onException)
        {
            var targetRequest = _requestFactory.Invoke(typeof(TRequest));
            targetRequest.Execute(args);
        }
    }
}
