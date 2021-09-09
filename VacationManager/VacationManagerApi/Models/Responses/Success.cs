namespace VacationManagerApi.Models.Responses
{
    public class Success : BaseResponse
    {
        public Success() { }

        public Success(object response)
        {
            Response = response;
        }

        public object Response { get; }
    }
}
