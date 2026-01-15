using InchirieriMasini.Models;
using InchirieriMasini.Common;
using InchirieriMasini.Persistence;
namespace InchirieriMasini.Services;

public class RentalService: IRentalService
{
    private readonly List<Rental> rentals = new();
    private int nextIdRental = 3001;

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
    //CreeateRental pentru logica de baza. !!!! poate arunca exceptii. pentru UI folositi TryCreateRental
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

    
    //CloseRental pentru logica de baza. !!!! poate arunca exceptii. pentru UI folositi TryCloseRental
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

    
    //GetDaysRemaining pentru logica de baza. !!!! poate arunca exceptii. pentru UI folositi TryGetDaysRemaining
    public int GetDaysRemaining(int rentalId)
    {
        var rental = GetById(rentalId);
        if(rental == null)
            throw new ArgumentException("Inchirierea nu exista.");

        if(!rental.GetIsActive())
            return 0;
        int remaining = (rental.GetEndDate().Date - DateTime.Today).Days;
        if (remaining < 0) return 0;
        return remaining;
    }

    
    //GetClosingDate pentru logica de baza. !!!! poate arunca exceptii. pentru UI folositi TryGetClosingDate
    public DateTime GetClosingDate(int rentalId)
    {
        var rental = GetById(rentalId);
        if (rental == null)
            throw new ArgumentException("Inchirierea nu exista.");
        return rental.GetEndDate();
    }

    
    //GetTotalPrice pentru logica de baza. !!!! poate arunca exceptii. pentru UI folositi TryGetTotalPrice 
    public double? GetTotalPrice(int rentalId)
    {
        var rental = GetById(rentalId);
        return rental.GetTotalPrice();
    }

    
    //TryCreaterental nu arunca exceptii, returneaza Result (uita.te in fisier Common) - pentru UI si legare butoane
    public Result<Rental> TryCreateRental(int carId, int clientId, DateTime startDate, int days)
    {
        try
        {
            var rental = CreateRental(carId, clientId, startDate, days);
            return new Result<Rental>(true, "Inchiriere creeata cu succes.", rental);
        }
        catch (Exception e)
        {
            return new Result<Rental>(false, e.Message, null);
        }
    }
    
    
    //TryCloseRental nu arunca exceptii, returneaza Result (uita.te in fisier Common) - pentru UI si legare butoane
    public Result TryCloseRental(int rentalId)
    {
        try
        {
            var rental = GetById(rentalId);
            if (rental == null) return new Result(false, "inchirierea nu exista");

            var r = CloseRental(rentalId);
            return new Result(true, r);

        }
        catch (Exception e)
        {
            return new Result(false, e.Message);
        }
    }
    
    //TryGetDaysRemaining nu arunca exceptii, returneaza Result (uita.te in fisier Common) - pentru UI si legare butoane 
    public Result<int> TryGetDaysRemaining(int  rentalId)
    {
        try
        {
            int days = GetDaysRemaining(rentalId);
            return new Result<int>(true,"ok" ,days);
        }
        catch (Exception e)
        {
            return new Result<int>(false, e.Message, default);
        }
    }
   
    
    //TryGetClosingDate nu arunca exceptii, returneaza Result (uita.te in fisier Common) - pentru UI si legare butoane 
    public Result<DateTime> TryGetClosingDate(int rentalId)
    {
        try
        {
            var dt = GetClosingDate(rentalId);
            return new Result<DateTime>(true, "ok", dt);
        }
        catch (Exception e)
        {
            return new Result<DateTime>(false,e.Message, default);
        }
    }
    
    //TryTotalPrice nu arunca exceptii, returneaza Result (uita.te in fisier Common) - pentru UI si legare butoane 
    public Result<double?> TryGetRental(int rentalId)
    {
        try
        {
            double? total = GetTotalPrice(rentalId);
            return new Result<double?>(true, "ok", total);
        }
        catch (Exception e)
        {
            return new Result<double?>(false,  e.Message, default);
        }
    }
    
    
    

    public List<RentalDto> Export()
    {
        return rentals.Select(r => new RentalDto
        {
            Id = r.GetId(),
            CarId = r.GetCarId(),
            ClientId = r.GetClientId(),
            StartDate = r.GetStartDate(),
            Days = r.GetDays(),
            IsActive = r.GetIsActive(),
            TotalPrice = r.GetTotalPrice()
        }).ToList();
    }

    public void Import(List<RentalDto> data, int nextId)
    {
        rentals.Clear();
        foreach (var d in data)
        {
            var rental = new Rental(d.Id, d.CarId, d.ClientId, d.StartDate, d.Days);

            // total price:
            if (d.TotalPrice.HasValue)
                rental.SetTotalPrice(d.TotalPrice.Value);

            // active:
            rental.SetActive(d.IsActive);

            rentals.Add(rental);
        }
        nextIdRental = nextId;
    }

    public int GetNextId() => nextIdRental;
}