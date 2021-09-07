using System.Collections.Generic;

namespace VacationManagerApi.Models.Responses
{
    public class Success : BaseResponse
    {
        public Success(object response)
        {
            Response = response;
        }

        public object Response { get; }
    }

    public class Validation : BaseResponse
    {
        public Validation(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }

    public class Failure : BaseResponse
    {
        public Failure(IEnumerable<string> errors)
        {
            Errors = errors;
        }

        public IEnumerable<string> Errors { get; }
    }

    public abstract class BaseResponse
    {
        public bool Succeeded() => this is Success;

        public bool HasValidations() => this is Validation;

        public bool Failed() => this is Failure;
    }
}