using InchirieriMasini.Services;

namespace InchirieriMasini.Persistence;

public class AppController
{
    private readonly JsonStorage _storage;
    private readonly CarService _carService;
    private readonly ClientService _clientService;
    private readonly RentalService _rentalService;

    public AppController(JsonStorage storage, CarService carService, ClientService clientService, RentalService rentalService)
    {
        _storage = storage;
        _carService = carService;
        _clientService = clientService;
        _rentalService = rentalService;
    }

    public void Load()
    {
        var state = _storage.Load();

        _carService.Import(state.Cars, state.NextCarId);
        _clientService.Import(state.Clients, state.NextClientId);
        _rentalService.Import(state.Rentals, state.NextRentalId);

        // IMPORTANT: după load, sincronizează disponibilitatea mașinilor
        // dacă există rental activ pe car => mașina trebuie indisponibilă
        var activeCarIds = _rentalService.GetActiveRentals().Select(r => r.GetCarId()).ToHashSet();
        foreach (var car in _carService.GetAllCars())
            car.SetAvailability(!activeCarIds.Contains(car.GetId()));
    }

    public void Save()
    {
        var state = new AppState
        {
            Cars = _carService.Export(),
            Clients = _clientService.Export(),
            Rentals = _rentalService.Export(),

            NextCarId = _carService.GetNextId(),
            NextClientId = _clientService.GetNextId(),
            NextRentalId = _rentalService.GetNextId()
        };

        _storage.Save(state);
    }
}