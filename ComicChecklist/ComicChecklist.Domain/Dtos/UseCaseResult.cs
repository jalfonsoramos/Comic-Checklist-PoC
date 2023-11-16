namespace ComicChecklist.Domain.Dtos
{
    public class UseCaseResult<T>
    {
        private UseCaseResult(T value)
        {
            Value = value;
            IsSuccess = true;
            Errors = null;
        }

        private UseCaseResult(List<string> errors)
        {
            Value = default;
            IsSuccess = false;
            Errors = errors;
        }

        public bool IsSuccess { get; }

        public T Value { get; }

        public List<string> Errors { get; }

        public static UseCaseResult<T> CreateSuccessResult(T value)
        {
            return new UseCaseResult<T>(value);
        }

        public static UseCaseResult<T> CreateFailResult(params string[] errors)
        {
            return new UseCaseResult<T>(new List<string>(errors));
        }
    }

    public class UseCaseResult
    {
        private UseCaseResult()
        {
            IsSuccess = true;
            Errors = null;
        }

        private UseCaseResult(List<string> errors)
        {
            IsSuccess = false;
            Errors = errors;
        }

        public bool IsSuccess { get; }
        public List<string> Errors { get; }

        public static UseCaseResult CreateSuccessResult()
        {
            return new UseCaseResult();
        }

        public static UseCaseResult CreateFailResult(params string[] errors)
        {
            return new UseCaseResult(new List<string>(errors));
        }
    }
}
