namespace TaskVacancy.Services.Implementations
{
    public class LogService : ILogService
    {
        public void Crash(string text)
        {
            Writelog(text, "crash");
        }

        public void Log(string text)
        {
            Writelog(text);
        }

        public void Writelog(string text, string category = "log")
        {
            try
            {
                text = $"{DateTime.Now:dd.MM.yyyy HH:mm:ss} {text}";
                File.AppendAllText($"logs/{category}{DateTime.Now:ddMMyyyy}.txt", text);
            }
            catch (Exception)
            {

            }
        }
    }
}
