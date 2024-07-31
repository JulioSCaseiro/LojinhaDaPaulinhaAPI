namespace LojinhaDaPaulinhaAPI.Helpers
{
    public class ApiResponse
    {
        public bool IsError { get; set; }

        public string Message { get; set; }

        public object Result { get; set; }


        public static ApiResponse CustomError(string message) => new ApiResponse
        {
            IsError = true,
            Message = message
        };

        public static ApiResponse DataReturnedNull => new ApiResponse
        {
            IsError = false,
            Message = "Error: data returned null."
        };

        public static ApiResponse GenericError => new ApiResponse
        {
            IsError = true,
            Message = "Error: something went wrong."
        };

        public static ApiResponse IdIsNotValid(int id) => new ApiResponse
        {
            IsError = true,
            Message = $"Error: the ID given is not valid (ID {id})."
        };
    }
}
