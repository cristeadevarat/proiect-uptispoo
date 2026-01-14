namespace InchirieriMasini.Models;

public class Car
{
    private int Id;
    private string Brand;
    private string Model;
    private int Year;
    private double PricePerDay;
    private bool IsAvailable;

    public Car(int id, string brand, string model, int year, double price)
    {
        Id = id;
        
        if (string.IsNullOrWhiteSpace(brand)) throw new ArgumentException("Brand invalid");
        if (string.IsNullOrWhiteSpace(model)) throw new ArgumentException("Model invalid");
        if (year < 1950 || year > DateTime.Now.Year) throw new ArgumentException("Year invalid");
        if (price <= 0) throw new ArgumentException("PricePerDay invalid");
        
        Brand = brand;
        Model = model;
        Year = year;
        PricePerDay = price;
        IsAvailable = true;
    }

    public int GetId() => Id;
    public string GetBrand() => Brand;
    public string GetModel() => Model;
    public int GetYear() => Year;
    public double GetPricePerDay() => PricePerDay;
    public bool GetIsAvailable() => IsAvailable;

    public void MarkRented()
    {
        if (!IsAvailable) throw new InvalidOperationException("Masina este deja inchiriata");
        IsAvailable = false;
    }

    public void MarkReturned()
    {
        IsAvailable = true;
    }

    public override string ToString()
    {
        if(IsAvailable) return $"{Id} - {Brand} {Model} ({Year}) - {PricePerDay} lei/zi - Disponibila";
        else  return $"{Id} - {Brand} {Model} ({Year}) - {PricePerDay} lei/zi - Indisponibila";
    }
        
}