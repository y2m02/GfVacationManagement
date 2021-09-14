using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationManagerApi.Models.Responses;

namespace VacationManagerApi.Helpers
{
    public static class ResponseHandler
    {
        public static Success Success() => new();

        public static Success Success<T>(T response) => new(response);

        public static Validation Validations(IEnumerable<string> errors) => new(errors);

        public static Unauthorized Unauthorized(string error) => new(new[] { error });

        public static Unauthorized Unauthorized(IEnumerable<string> errors) => new(errors);

        public static Failure Failure(IEnumerable<string> errors) => new(errors.First());

        public static async Task<IBaseResponse> HandleErrors<TReturn>(Func<Task<TReturn>> executor)
        {
            try
            {
                return Success(await executor().ConfigureAwait(false));
            }
            catch (Exception ex)
            {
                return Failure(new[] { ex.Message });
            }
        }

        public static async Task<IBaseResponse> HandleErrors(Func<Task<IBaseResponse>> executor)
        {
            try
            {
                return await executor().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return Failure(new[] { ex.Message });
            }
        }

        public static async Task<IBaseResponse> HandleErrors(Func<Task> executor)
        {
            try
            {
                await executor().ConfigureAwait(false);

                return Success();
            }
            catch (Exception ex)
            {
                return Failure(new[] { ex.Message });
            }
        }
    }
}
