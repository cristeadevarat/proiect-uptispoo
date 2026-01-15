namespace InchirieriMasini.Interfaces;

/// <summary>
/// Interface for file storage operations (wrapper for file I/O)
/// Isolates the application from direct file system dependencies
/// </summary>
public interface IFileStorage
{
    /// <summary>
    /// Saves data to a file
    /// </summary>
    /// <typeparam name="T">Type of data to save</typeparam>
    /// <param name="filePath">Path to the file</param>
    /// <param name="data">Data to save</param>
    void Save<T>(string filePath, T data);

    /// <summary>
    /// Loads data from a file
    /// </summary>
    /// <typeparam name="T">Type of data to load</typeparam>
    /// <param name="filePath">Path to the file</param>
    /// <returns>Loaded data or default value if file doesn't exist</returns>
    T? Load<T>(string filePath);

    /// <summary>
    /// Checks if a file exists
    /// </summary>
    /// <param name="filePath">Path to check</param>
    /// <returns>True if file exists</returns>
    bool FileExists(string filePath);

    /// <summary>
    /// Deletes a file if it exists
    /// </summary>
    /// <param name="filePath">Path to the file</param>
    void DeleteFile(string filePath);
}
