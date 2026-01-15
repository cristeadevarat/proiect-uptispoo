using System.Text.Json;

namespace InchirieriMasini.Persistence;

public class JsonStorage
{
    private readonly string _filePath;
    private readonly JsonSerializerOptions _opts = new()
    {
        WriteIndented = true
    };

    public JsonStorage(string filePath)
    {
        _filePath = filePath;
    }

    public void Save(AppState state)
    {
        var dir = Path.GetDirectoryName(_filePath);
        if (!string.IsNullOrWhiteSpace(dir))
            Directory.CreateDirectory(dir);

        var json = JsonSerializer.Serialize(state, _opts);
        File.WriteAllText(_filePath, json);
    }

    public AppState Load()
    {
        if (!File.Exists(_filePath))
            return new AppState();

        var json = File.ReadAllText(_filePath);
        var state = JsonSerializer.Deserialize<AppState>(json, _opts);
        return state ?? new AppState();
    }
}