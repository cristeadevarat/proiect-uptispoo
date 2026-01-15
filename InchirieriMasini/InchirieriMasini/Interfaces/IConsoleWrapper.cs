namespace InchirieriMasini.Interfaces;

/// <summary>
/// Interface for console operations (wrapper for console I/O)
/// Isolates the application from direct console dependencies
/// </summary>
public interface IConsoleWrapper
{
    /// <summary>
    /// Writes a line to the console
    /// </summary>
    void WriteLine(string message);

    /// <summary>
    /// Writes to the console without a new line
    /// </summary>
    void Write(string message);

    /// <summary>
    /// Reads a line from the console
    /// </summary>
    string? ReadLine();

    /// <summary>
    /// Clears the console
    /// </summary>
    void Clear();
}
