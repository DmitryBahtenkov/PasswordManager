namespace PasswordManager.Internal.Contract.ViewModels
{
    public record ResultModel<T>
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public T Content { get; set; }

        public ResultModel(T content)
        {
            IsSuccess = true;
            Content = content;
        }

        public ResultModel(){}

        public static ResultModel<T> WithError(string error) => new() { Error = error };
    }

    public record ResultModel
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }

        public ResultModel()
        {
            IsSuccess = true;
        }

        public static ResultModel WithError(string error) => new() {Error = error};
    }
}
