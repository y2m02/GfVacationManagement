namespace VacationManagerApi.Models.Responses
{
    public interface IBaseResponse
    {
        sealed bool Succeeded() => this is Success;
        sealed bool HasValidations() => this is Validation;
        sealed bool Unauthorized() => this is Unauthorized;
        sealed bool Failed() => this is Failure;
    }
}
