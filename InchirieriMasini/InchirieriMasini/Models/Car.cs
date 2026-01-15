namespace InchirieriMasini.Models;

/// <summary>
/// Car class inheriting from Vehicle
/// </summary>
public class Car : Vehicle
{
    public int NumberOfDoors { get; set; }
    public string FuelType { get; set; } = string.Empty;
    public string Transmission { get; set; } = string.Empty;

    public override decimal CalculateRentalCost(int days)
    {
        // Standard car rental calculation
        decimal baseCost = PricePerDay * days;
        
        // Add surcharge for automatic transmission
        if (Transmission?.Equals("automatic", StringComparison.OrdinalIgnoreCase) == true)
        {
            baseCost += 10 * days;
        }

        return baseCost;
    }

    public override string GetVehicleInfo()
    {
        return $"{base.GetVehicleInfo()} - {NumberOfDoors} doors, {FuelType}, {Transmission}";
    }
}
