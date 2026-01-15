using InchirieriMasini.Infrastructure;
using InchirieriMasini.Interfaces;
using InchirieriMasini.Models;
using Microsoft.Extensions.Logging;

namespace InchirieriMasini.Services;

/// <summary>
/// Service for managing vehicles with LINQ operations
/// </summary>
public class VehicleService
{
    private readonly IFileStorage _fileStorage;
    private readonly ILogger<VehicleService> _logger;
    private readonly string _dataFilePath;
    private ApplicationState _state;

    public VehicleService(IFileStorage fileStorage, ILogger<VehicleService> logger)
    {
        _fileStorage = fileStorage;
        _logger = logger;
        _dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "app_state.json");
        _state = LoadState();
    }

    private ApplicationState LoadState()
    {
        try
        {
            var state = _fileStorage.Load<ApplicationState>(_dataFilePath);
            return state ?? new ApplicationState();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading application state");
            return new ApplicationState();
        }
    }

    private void SaveState()
    {
        try
        {
            _fileStorage.Save(_dataFilePath, _state);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving application state");
            throw;
        }
    }

    public void AddVehicle(Vehicle vehicle)
    {
        _logger.LogInformation("Adding vehicle: {Brand} {Model}", vehicle.Brand, vehicle.Model);
        vehicle.Id = _state.NextVehicleId++;
        _state.Vehicles.Add(vehicle);
        SaveState();
    }

    public void UpdateVehicle(Vehicle vehicle)
    {
        _logger.LogInformation("Updating vehicle with ID: {Id}", vehicle.Id);
        var index = _state.Vehicles.FindIndex(v => v.Id == vehicle.Id);
        if (index >= 0)
        {
            _state.Vehicles[index] = vehicle;
            SaveState();
        }
        else
        {
            throw new InvalidOperationException($"Vehicle with ID {vehicle.Id} not found");
        }
    }

    public void DeleteVehicle(int id)
    {
        _logger.LogInformation("Deleting vehicle with ID: {Id}", id);
        var vehicle = _state.Vehicles.FirstOrDefault(v => v.Id == id);
        if (vehicle != null)
        {
            _state.Vehicles.Remove(vehicle);
            SaveState();
        }
    }

    public Vehicle? GetVehicleById(int id)
    {
        // LINQ operation
        return _state.Vehicles.FirstOrDefault(v => v.Id == id);
    }

    public IEnumerable<Vehicle> GetAllVehicles()
    {
        // LINQ operation
        return _state.Vehicles.OrderBy(v => v.Brand).ThenBy(v => v.Model);
    }

    public IEnumerable<Vehicle> GetAvailableVehicles()
    {
        // LINQ operation with filtering
        return _state.Vehicles.Where(v => v.IsAvailable).OrderBy(v => v.PricePerDay);
    }

    public IEnumerable<Car> GetAllCars()
    {
        // LINQ operation with type filtering (demonstrates polymorphism)
        return _state.Vehicles.OfType<Car>().OrderBy(c => c.Brand);
    }

    public IEnumerable<Truck> GetAllTrucks()
    {
        // LINQ operation with type filtering
        return _state.Vehicles.OfType<Truck>().OrderBy(t => t.Brand);
    }

    public IEnumerable<Vehicle> SearchVehicles(string searchTerm)
    {
        // LINQ operation with complex filtering
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Enumerable.Empty<Vehicle>();
        }

        return _state.Vehicles
            .Where(v => v.Brand.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                       v.Model.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                       v.LicensePlate.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .OrderBy(v => v.Brand);
    }

    public ApplicationState GetState() => _state;
}
