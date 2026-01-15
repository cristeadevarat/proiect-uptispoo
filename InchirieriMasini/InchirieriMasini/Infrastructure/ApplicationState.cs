using InchirieriMasini.Models;

namespace InchirieriMasini.Infrastructure;

/// <summary>
/// Data transfer object for application state persistence
/// </summary>
public class ApplicationState
{
    public List<Vehicle> Vehicles { get; set; } = new();
    public List<Customer> Customers { get; set; } = new();
    public List<Rental> Rentals { get; set; } = new();
    public int NextVehicleId { get; set; } = 1;
    public int NextCustomerId { get; set; } = 1;
    public int NextRentalId { get; set; } = 1;
}
