using InchirieriMasini.Interfaces;

namespace InchirieriMasini.Infrastructure;

/// <summary>
/// Console wrapper implementation
/// </summary>
public class ConsoleWrapper : IConsoleWrapper
{
    public void WriteLine(string message)
    {
        Console.WriteLine(message);
    }

    public void Write(string message)
    {
        Console.Write(message);
    }

    public string? ReadLine()
    {
        return Console.ReadLine();
    }

    public void Clear()
    {
        Console.Clear();
    }
}
