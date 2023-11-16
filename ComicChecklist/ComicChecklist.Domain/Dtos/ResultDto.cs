namespace ComicChecklist.Domain.Dtos
{
    public class ResultDto<T>
    {
        internal ResultDto(T value)
        {
            Value = value;
            IsSuccess = true;
            Errors = null;
        }

        internal ResultDto(List<string> errors)
        {
            Value = default;
            IsSuccess = false;
            Errors = errors;
        }

        public bool IsSuccess { get; }

        public T Value { get; }

        public List<string> Errors { get; }

        public static ResultDto<T> CreateSuccessResult(T value)
        {
            return new ResultDto<T>(value);
        }

        public static ResultDto<T> CreateFailResult(params string[] errors)
        {
            return new ResultDto<T>(new List<string>(errors));
        }
    }
}
