namespace NoteApi.Models.HttpException
{
    public class UnauthorizedException : System.Exception
    {
        public UnauthorizedException(string message) : base(message) { }
    }
}
