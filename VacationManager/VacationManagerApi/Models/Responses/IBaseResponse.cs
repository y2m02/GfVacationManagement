namespace VacationManagerApi.Models.Responses
{
    public interface IBaseResponse
    {
        sealed bool Succeeded() => this is Success;
        sealed bool Failed() => this is Failure;
    }
}
