namespace InchirieriMasini.Models;

/// <summary>
/// Rental entity demonstrating composition
/// </summary>
public class Rental
{
    public int Id { get; set; }
    
    // Composition: Rental HAS-A Customer and HAS-A Vehicle
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public int VehicleId { get; set; }
    public Vehicle? Vehicle { get; set; }
    
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PlannedDays { get; set; }
    public decimal TotalCost { get; set; }
    public RentalStatus Status { get; set; }
    public string? Notes { get; set; }

    // Computed property
    public int ActualDays => EndDate.HasValue 
        ? (EndDate.Value - StartDate).Days 
        : (DateTime.Now - StartDate).Days;

    public bool IsActive => Status == RentalStatus.Active;
}

/// <summary>
/// Enum for rental status
/// </summary>
public enum RentalStatus
{
    Active,
    Completed,
    Cancelled
}
