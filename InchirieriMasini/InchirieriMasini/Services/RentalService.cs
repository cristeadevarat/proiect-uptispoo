using InchirieriMasini.Infrastructure;
using InchirieriMasini.Interfaces;
using InchirieriMasini.Models;
using Microsoft.Extensions.Logging;

namespace InchirieriMasini.Services;

/// <summary>
/// Service for managing rentals with business logic
/// </summary>
public class RentalService
{
    private readonly IFileStorage _fileStorage;
    private readonly ILogger<RentalService> _logger;
    private readonly string _dataFilePath;
    private ApplicationState _state;

    public RentalService(IFileStorage fileStorage, ILogger<RentalService> logger)
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

    public Rental CreateRental(int customerId, int vehicleId, int days)
    {
        _logger.LogInformation("Creating rental for customer {CustomerId} and vehicle {VehicleId}", 
            customerId, vehicleId);

        var customer = _state.Customers.FirstOrDefault(c => c.Id == customerId);
        if (customer == null)
        {
            throw new InvalidOperationException($"Customer with ID {customerId} not found");
        }

        var vehicle = _state.Vehicles.FirstOrDefault(v => v.Id == vehicleId);
        if (vehicle == null)
        {
            throw new InvalidOperationException($"Vehicle with ID {vehicleId} not found");
        }

        if (!vehicle.IsAvailable)
        {
            throw new InvalidOperationException("Vehicle is not available for rental");
        }

        // Calculate cost using polymorphism
        var totalCost = vehicle.CalculateRentalCost(days);

        var rental = new Rental
        {
            Id = _state.NextRentalId++,
            CustomerId = customerId,
            Customer = customer,
            VehicleId = vehicleId,
            Vehicle = vehicle,
            StartDate = DateTime.Now,
            PlannedDays = days,
            TotalCost = totalCost,
            Status = RentalStatus.Active
        };

        vehicle.IsAvailable = false;
        _state.Rentals.Add(rental);
        SaveState();

        _logger.LogInformation("Rental created with ID: {RentalId}", rental.Id);
        return rental;
    }

    public void CompleteRental(int rentalId)
    {
        _logger.LogInformation("Completing rental with ID: {RentalId}", rentalId);

        var rental = _state.Rentals.FirstOrDefault(r => r.Id == rentalId);
        if (rental == null)
        {
            throw new InvalidOperationException($"Rental with ID {rentalId} not found");
        }

        rental.EndDate = DateTime.Now;
        rental.Status = RentalStatus.Completed;

        var vehicle = _state.Vehicles.FirstOrDefault(v => v.Id == rental.VehicleId);
        if (vehicle != null)
        {
            vehicle.IsAvailable = true;
        }

        SaveState();
    }

    public void CancelRental(int rentalId)
    {
        _logger.LogInformation("Cancelling rental with ID: {RentalId}", rentalId);

        var rental = _state.Rentals.FirstOrDefault(r => r.Id == rentalId);
        if (rental == null)
        {
            throw new InvalidOperationException($"Rental with ID {rentalId} not found");
        }

        rental.Status = RentalStatus.Cancelled;

        var vehicle = _state.Vehicles.FirstOrDefault(v => v.Id == rental.VehicleId);
        if (vehicle != null)
        {
            vehicle.IsAvailable = true;
        }

        SaveState();
    }

    public IEnumerable<Rental> GetAllRentals()
    {
        // LINQ operation with ordering
        return _state.Rentals.OrderByDescending(r => r.StartDate);
    }

    public IEnumerable<Rental> GetActiveRentals()
    {
        // LINQ operation with filtering
        return _state.Rentals
            .Where(r => r.Status == RentalStatus.Active)
            .OrderBy(r => r.StartDate);
    }

    public IEnumerable<Rental> GetRentalsByCustomer(int customerId)
    {
        // LINQ operation
        return _state.Rentals
            .Where(r => r.CustomerId == customerId)
            .OrderByDescending(r => r.StartDate);
    }

    public IEnumerable<Rental> GetRentalsByVehicle(int vehicleId)
    {
        // LINQ operation
        return _state.Rentals
            .Where(r => r.VehicleId == vehicleId)
            .OrderByDescending(r => r.StartDate);
    }

    public decimal GetTotalRevenue()
    {
        // LINQ aggregation
        return _state.Rentals
            .Where(r => r.Status == RentalStatus.Completed)
            .Sum(r => r.TotalCost);
    }

    public ApplicationState GetState() => _state;
}
