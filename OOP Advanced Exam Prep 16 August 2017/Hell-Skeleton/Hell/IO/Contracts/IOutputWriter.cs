public interface IOutputWriter
{/// <summary>
 /// Method for printing content!
 /// </summary>
 /// <param name="text">parameter that will be printed</param>
    void WriteLine(string text);
    void WriteLine(string format, params string[] args);
}
