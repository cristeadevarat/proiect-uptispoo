namespace InchirieriMasini.Models;

/// <summary>
/// Base abstract class for vehicles (demonstrates inheritance and polymorphism)
/// </summary>
public abstract class Vehicle
{
    public int Id { get; set; }
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public string LicensePlate { get; set; } = string.Empty;
    public decimal PricePerDay { get; set; }
    public bool IsAvailable { get; set; } = true;

    /// <summary>
    /// Abstract method to calculate rental cost (polymorphism)
    /// </summary>
    public abstract decimal CalculateRentalCost(int days);

    /// <summary>
    /// Virtual method that can be overridden
    /// </summary>
    public virtual string GetVehicleInfo()
    {
        return $"{Brand} {Model} ({Year}) - {LicensePlate}";
    }
}
