using InchirieriMasini.Models;
namespace InchirieriMasini.Services;

public class CarService: ICarService
{
    private readonly List<Car> cars = new();
    private int nextIdCar = 10001; //id.urile = coduri care se genereaza automat

    public IEnumerable<Car> GetAllCars() => cars;

    public IEnumerable<Car> GetAvailableCars()
    {
        List<Car> availableCars = new();
        foreach (var car in cars)
        {
            if (car.GetIsAvailable()) //daca masina e disponibila 
                availableCars.Add(car); // o adauga la lista
        }
        return availableCars; //returneaza lista cu masini disponibile
    }

    public Car? GetById(int id) // poate fi null: masina nu exista 
    {
        foreach (var car in cars)
        {
            if (car.GetId()==id)
                return car;
        }
        return null; //daca nu gaseste masina returneaza null
    }

    public Car AddCar(string brand, string model, int year, double price) //returneaza car (daca nu e nevoie de obiect pentru altceva, schimb in string )
    {
        var car = new Car(nextIdCar, brand, model, year, price);
        nextIdCar++; //nou id pentru urmatoarea masina
        cars.Add(car);
        return car;
    }
    
    public string RemoveCar(int id)
    {
        var car = GetById(id);
        if (car is null) return "Masina pe care doriti sa o stergeti nu exista.";
        cars.Remove(car);
        return "Masina a fost eliminata cu succes.";
    }

    public string MarkRented(int id)
    {
        var car = GetById(id);
        if (car is null) return "Masina pe care doriti sa o inchiriati nu exista."; //daca masina nu exista => mesaj de eroare
        try // incearca sa marcheze masina ca inchiriata
        {
            car.MarkRented();
            return $"Masina cu id-ul {id} a fost inchiriata cu succes."; 
        }
        catch(Exception ex)
        {
            return ex.Message; //daca era deja inchiriata => mesajul de eroare din metoda MarkRented din Car
        }
    }

    public string MarkReturned(int id)
    {
        var car = GetById(id);
        if (car is null) return "Masina pe care doriti sa o returnati nu exista.";
        car.MarkReturned();
        return $"Masina cu id-ul {id} a fost returnata cu succes.";
    }
}