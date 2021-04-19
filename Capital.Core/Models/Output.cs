namespace Capital.Core.Models
{
    public class Output<T>
    {
        public Output()
        {
        }

        public Output(bool success)
        {
            IsSuccess = success;
        }

        public Output(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public Output(string errorMessage, string errorCode)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public Output(bool status, string errorMessage, string errorCode)
        {
            IsSuccess = status;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }

        public Output(bool success, string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsSuccess = success;
        }

        public Output(bool success, T result)
        {
            Result = result;
            IsSuccess = success;
        }

        public Output(bool success, string errorMessage, T result)
        {
            Result = result;
            ErrorMessage = errorMessage;
            IsSuccess = success;
        }

        public Output(bool success, string errorMessage, string errorCode, T result)
        {
            Result = result;
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            IsSuccess = success;
        }

        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public T Result { get; set; }
        public bool IsSuccess { get; set; }
        public bool IsNotSuccess
        {
            get
            {
                return !IsSuccess;
            }
        }
    }
}
