using InchirieriMasini.Models;
namespace InchirieriMasini.Services;

public class RentalService: IRentalService
{
    private readonly List<Rental> rentals = new();
    private int nextIdRental = 3000;

    private readonly ICarService carService;
    private readonly IClientService clientService;

    public RentalService(ICarService carServ, IClientService clientServ)
    {
        carService = carServ;
        clientService = clientServ;
    }

    public IEnumerable<Rental> GetAllRentals()
    {
        return rentals;
    }

    public IEnumerable<Rental> GetActiveRentals()
    {
        List<Rental> active = new();
        foreach (var r in rentals)
        {
            if (r.GetIsActive())
                active.Add(r);
        }
        return active;
    }

    public Rental? GetById(int id)
    {
        foreach (var r in rentals)
        {
            if (r.GetId() == id)
                return r;
        }
        return null;
    }

    // returneaza inchirierea activa pentru masina daca exista
    public Rental? GetByCarId(int carId)
    {
        foreach (var r in rentals)
        {
            if (r.GetCarId() == carId && r.GetIsActive())
                return r;
        }
        return null;
    }

    //lista doar cu inchirieri active pentru client 
    public IEnumerable<Rental> GetByClientId(int clientId)
    {
        List<Rental> clientRentals = new();
        foreach (var r in rentals)
        {
            if (r.GetClientId() == clientId && r.GetIsActive())
                clientRentals.Add(r);
        }
        return clientRentals;
    }

    public Rental CreateRental(int carId, int clientId, DateTime startDate, int days)
    {
        var car = carService.GetById(carId);
        if (car == null)
            throw new ArgumentException("Masina nu exista.");

        var client = clientService.GetById(clientId);
        if (client == null)
            throw new ArgumentException("Clientul nu exista.");

        if (!car.GetIsAvailable())
            throw new InvalidOperationException("Masina nu este disponibila.");

        //nu permitem doua inchirieri active pe aceeasi masina
        var existing = GetByCarId(carId); //metoda returneaza null daca masina nu e inchiriata, deci e ok
        if (existing != null)
            throw new InvalidOperationException("Masina are deja o inchiriere activa.");

        //marcheaza masina inchiriata (arunca exeptie daca e deja - siguranta suplimentara)
        car.MarkRented();

        var rental = new Rental(nextIdRental, carId, clientId, startDate, days);
        nextIdRental++;
        double total = car.GetPricePerDay() * days;
        rental.SetTotalPrice(total);
        rentals.Add(rental);
        return rental;
    }

    public string CloseRental(int rentalId)
    {
        var rental = GetById(rentalId);
        if (rental == null)
            return "Inchirierea nu exista.";
        rental.Close();
        
        var car = carService.GetById(rental.GetCarId());
        if (car!=null)
        {
            car.MarkReturned(); // cand stergem inchirierea, marcam masina ca returnata
        }
        return $"Inchirierea cu id-ul {rentalId} a fost inchisa cu succes.";
    }

    public int GetDaysRemaining(int rentalId, DateTime today)
    {
        var rental = GetById(rentalId);
        if(rental == null)
            throw new ArgumentException("Inchirierea nu exista.");

        if(!rental.GetIsActive())
            return 0;
        int remaining = (rental.GetEndDate().Date - today.Date).Days;
        if (remaining < 0) return 0;
        return remaining;
    }

    public DateTime GetClosingDate(int rentalId)
    {
        var rental = GetById(rentalId);
        if (rental == null)
            throw new ArgumentException("Inchirierea nu exista.");
        return rental.GetEndDate();
    }

    public double? GetTotalPrice(int rentalId)
    {
        var rental = GetById(rentalId);
        return rental.GetTotalPrice();
    }
}