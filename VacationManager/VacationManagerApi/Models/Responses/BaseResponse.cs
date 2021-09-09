namespace VacationManagerApi.Models.Responses
{
    public abstract class BaseResponse
    {
        public bool Succeeded() => this is Success;

        public bool HasValidations() => this is Validation;

        public bool Failed() => this is Failure;
    }
}