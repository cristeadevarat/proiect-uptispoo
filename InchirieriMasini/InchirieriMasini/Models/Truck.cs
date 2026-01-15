namespace InchirieriMasini.Models;

/// <summary>
/// Truck class inheriting from Vehicle (demonstrates inheritance hierarchy)
/// </summary>
public class Truck : Vehicle
{
    public decimal CargoCapacity { get; set; } // in tons
    public int NumberOfAxles { get; set; }

    public override decimal CalculateRentalCost(int days)
    {
        // Truck rental includes cargo capacity surcharge
        decimal baseCost = PricePerDay * days;
        decimal capacitySurcharge = CargoCapacity * 5 * days;
        
        return baseCost + capacitySurcharge;
    }

    public override string GetVehicleInfo()
    {
        return $"{base.GetVehicleInfo()} - Cargo: {CargoCapacity}t, {NumberOfAxles} axles";
    }
}
