namespace Utilities
{
    public interface ILogger
    {
        void Init();
        void LogEvent(string msg);
        void LogError(string msg);
        void LogException(string msg, Exception exce);
        void LogCheckHouseKeeping();
    }
  
}