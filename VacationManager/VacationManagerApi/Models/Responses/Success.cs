namespace VacationManagerApi.Models.Responses
{
    public class Success : IBaseResponse
    {
        public Success() { }

        public Success(object response)
        {
            Response = response;
        }

        public object Response { get; }
    }
}
