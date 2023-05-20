namespace TaskVacancy.Exceptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException(string exceptionText) : base(exceptionText)
        {

        }
    }
}
