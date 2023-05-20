namespace TaskVacancy.Services
{
    public interface ILogService
    {
        void Log(string text);
        void Crash(string text);
        void Writelog(string text, string category = "log");
    }
}
