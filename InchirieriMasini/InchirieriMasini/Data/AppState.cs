using System.Text.Json.Serialization;

namespace InchirieriMasini.Persistence;

public class AppState
{
    public List<CarDto> Cars { get; set; } = new();
    public List<ClientDto> Clients { get; set; } = new();
    public List<RentalDto> Rentals { get; set; } = new();

    public int NextCarId { get; set; } = 10001;
    public int NextClientId { get; set; } = 2000;
    public int NextRentalId { get; set; } = 3001;
}

public class CarDto
{
    public int Id { get; set; }
    public string Brand { get; set; } = "";
    public string Model { get; set; } = "";
    public int Year { get; set; }
    public double PricePerDay { get; set; }
    public bool IsAvailable { get; set; }
}

public class ClientDto
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string Phone { get; set; } = "";
    public string Email { get; set; } = "";
}

public class RentalDto
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int ClientId { get; set; }
    public DateTime StartDate { get; set; }
    public int Days { get; set; }
    public bool IsActive { get; set; }
    public double? TotalPrice { get; set; }
}