using Microsoft.AspNetCore.Mvc;

namespace LojinhaDaPaulinhaAPI.Helpers
{
    public class ControllerHelper
    {
        private readonly DataService _dataService;

        public ControllerHelper(DataService dataService)
        {
            _dataService = dataService;
        }

        public async Task<IActionResult> TryGet(string dataOperation, object value)
        {
            try
            {
                var data = await _dataService.GetDataAsync(dataOperation, value);
                if (data == null) return DataIsNull();

                // Success.
                return Success(data);
            }
            catch
            {
                // Generic error.
                return Error();
            }
        }

        public async Task<IActionResult> TryPost(string dataOperation, object value)
        {
            try
            {
                var model = await _dataService.PostDataAsync(dataOperation, value);
                if (model == null) return DataIsNull();

                // Success.
                return Success(model);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        public async Task<IActionResult> TryPut(string dataOperation, object value)
        {
            try
            {
                var model = await _dataService.PutDataAsync(dataOperation, value);
                if (model == null) return DataIsNull();

                // Success.
                return Success(model);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        public async Task<IActionResult> TryDelete(string dataOperation, object value)
        {
            try
            {
                var model = await _dataService.DeleteDataAsync(dataOperation, value);
                if (model == null) return DataIsNull();

                // Success.
                return Success(model);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        private static IActionResult HandleException(Exception ex)
        {
            // Get error message if exception is one expected.
            var message = ExceptionHandling.GetCustomErrorMessage(ex);

            // If there is message, return it.
            if (message != null) return Error409Conflict(message);

            // Generic error.
            return Error();
        }


        // <public> controller response for direct controller use.
        public static IActionResult IdIsNotValid(int id)
        {
            return new JsonResult(ApiResponse.IdIsNotValid(id)) { StatusCode = StatusCodes.Status400BadRequest };
        }


        #region Prepared <private> controller responses

        private static IActionResult DataIsNull()
        {
            return new JsonResult(ApiResponse.DataReturnedNull) { StatusCode = StatusCodes.Status404NotFound };
        }

        private static IActionResult Error()
        {
            return new JsonResult(ApiResponse.GenericError) { StatusCode = StatusCodes.Status500InternalServerError };
        }

        private static IActionResult Error409Conflict(string message)
        {
            return new JsonResult(ApiResponse.CustomError(message)) { StatusCode = StatusCodes.Status409Conflict };
        }

        private static IActionResult Success(object data)
        {
            return new JsonResult(data) { StatusCode = StatusCodes.Status200OK };
        }

        #endregion Prepared controller responses
    }
}
