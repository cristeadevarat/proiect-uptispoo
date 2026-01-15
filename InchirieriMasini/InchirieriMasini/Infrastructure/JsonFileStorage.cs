using System.Text.Json;
using InchirieriMasini.Interfaces;
using Microsoft.Extensions.Logging;

namespace InchirieriMasini.Infrastructure;

/// <summary>
/// JSON file storage implementation with error handling
/// </summary>
public class JsonFileStorage : IFileStorage
{
    private readonly ILogger<JsonFileStorage> _logger;

    public JsonFileStorage(ILogger<JsonFileStorage> logger)
    {
        _logger = logger;
    }

    public void Save<T>(string filePath, T data)
    {
        try
        {
            _logger.LogInformation("Saving data to file: {FilePath}", filePath);
            
            // Ensure directory exists
            var directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                _logger.LogInformation("Created directory: {Directory}", directory);
            }

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNameCaseInsensitive = true
            };

            var json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json);
            
            _logger.LogInformation("Successfully saved data to {FilePath}", filePath);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "Access denied when saving to {FilePath}", filePath);
            throw new InvalidOperationException($"Access denied to file: {filePath}", ex);
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, "I/O error when saving to {FilePath}", filePath);
            throw new InvalidOperationException($"Failed to save file: {filePath}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error when saving to {FilePath}", filePath);
            throw;
        }
    }

    public T? Load<T>(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                _logger.LogWarning("File not found: {FilePath}", filePath);
                return default;
            }

            _logger.LogInformation("Loading data from file: {FilePath}", filePath);
            
            var json = File.ReadAllText(filePath);
            
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var data = JsonSerializer.Deserialize<T>(json, options);
            _logger.LogInformation("Successfully loaded data from {FilePath}", filePath);
            
            return data;
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogError(ex, "Access denied when loading from {FilePath}", filePath);
            throw new InvalidOperationException($"Access denied to file: {filePath}", ex);
        }
        catch (IOException ex)
        {
            _logger.LogError(ex, "I/O error when loading from {FilePath}", filePath);
            throw new InvalidOperationException($"Failed to load file: {filePath}", ex);
        }
        catch (JsonException ex)
        {
            _logger.LogError(ex, "JSON parsing error when loading from {FilePath}", filePath);
            throw new InvalidOperationException($"Invalid JSON in file: {filePath}", ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error when loading from {FilePath}", filePath);
            throw;
        }
    }

    public bool FileExists(string filePath)
    {
        return File.Exists(filePath);
    }

    public void DeleteFile(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                _logger.LogInformation("Deleted file: {FilePath}", filePath);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting file {FilePath}", filePath);
            throw;
        }
    }
}
