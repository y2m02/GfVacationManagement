namespace VacationManagerApi.Models.Responses
{
    public class Failure : IBaseResponse
    {
        public Failure(string error)
        {
            Error = error;
        }

        public string Error { get; }
    }
}
