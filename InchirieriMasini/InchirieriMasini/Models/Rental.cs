namespace InchirieriMasini.Models;

public class Rental
{
    private int Id;
    private int CarId;
    private int ClientId;
    private DateTime StartDate;
    private DateTime EndDate;
    private int Days;
    private bool IsActive;
    private double? TotalPrice; //???????????????????????????

    public Rental(int id, int carId, int clientId, DateTime startDate, int days)
    {
        if (days <= 0) throw new ArgumentException("Numarul de zile trebuie sa fie > 0");

        Id = id;
        CarId = carId;
        ClientId = clientId;
        StartDate = startDate;
        Days = days;
        IsActive = true;
        EndDate = startDate.AddDays(days);
        TotalPrice = null;
    }

    public void SetTotalPrice(double totalPrice)
    {
        if(totalPrice < 0) throw new ArgumentException("Pretul trebuie sa fie > 0");
        TotalPrice = totalPrice;
    }

    public int GetId() => Id;
    public int GetCarId() => CarId;
    public int GetClientId() => ClientId;
    public DateTime GetStartDate() => StartDate;
    public DateTime GetEndDate() => EndDate;
    public int GetDays() => Days;
    public bool GetIsActive() => IsActive;
    public double? GetTotalPrice() => TotalPrice;

    public void Close() // inchide o inchiriere 
    {
        IsActive = false;
    }

    public override string ToString()
    {
        if(IsActive) return $"Inchiriere activa {Id}: Masina {CarId}, Client {ClientId}, Start {StartDate:d}, End {EndDate:d}";
        else return $"Inchiriere expirata {Id}: Masina {CarId}, Client {ClientId}, Start {StartDate:d}, End {EndDate:d}";
 
    }
}