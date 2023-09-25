namespace Common.Models
{
    public class InternalResult<T>
    {
        private readonly HashSet<string> errors = new HashSet<string>();
        private readonly Dictionary<string, object> parameters = new Dictionary<string, object>();

        public InternalResult(T data, int code = 200)
        {
            Data = data;
            IsSuccess = true;
            Code = code;
        }

        public InternalResult(string message, int code, string type, string error, string url = null)
            : this(message, code, type, url)
        {
            if (string.IsNullOrWhiteSpace(error))
            {
                throw new ArgumentNullException($"{nameof(InternalResult<T>)}.{nameof(Errors)}");
            }

            errors.Add(error);
        }

        private InternalResult(string message, int code, string type, string url)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException($"{nameof(InternalResult<T>)}.{nameof(Message)}");
            }

            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException($"{nameof(InternalResult<T>)}.{nameof(Type)}");
            }

            Message = message;
            Code = code;
            Type = type;
            Url = url;
        }

        public T Data { get; }

        public bool IsSuccess { get; }

        public int Code { get; }

        public string Type { get; }

        public string Message { get; }

        public string Url { get; }

        public IEnumerable<string> Errors
        { get { return new HashSet<string>(errors); } /*private set { errors = value; }*/ }
    }
}