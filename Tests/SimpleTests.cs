using InchirieriMasini.Services;
using InchirieriMasini.Persistence;

namespace InchirieriMasini.Tests;

/// <summary>
/// Teste simple pentru verificarea conectării backend-ului
/// </summary>
public static class SimpleTests
{
    public static void RunAll()
    {
        Console.WriteLine("=== ÎNCEPERE TESTE CONECTARE UI-BACKEND ===\n");
        
        TestCarService();
        TestClientService();
        TestRentalService();
        TestPersistence();
        
        Console.WriteLine("\n=== TOATE TESTELE AU TRECUT CU SUCCES! ===");
    }
    
    private static void TestCarService()
    {
        Console.WriteLine("--- Test CarService ---");
        var service = new CarService();
        
        // Test adăugare
        var result = service.TryAddCar("BMW", "X5", 2022, 150.0);
        Assert(result.Success, "Adăugare mașină eșuată");
        Assert(result.Data != null, "Mașină null după adăugare");
        Assert(result.Data!.GetBrand() == "BMW", "Brand incorect");
        
        // Test listare
        var allCars = service.GetAllCars().ToList();
        Assert(allCars.Count == 1, "Număr mașini incorect");
        
        // Test disponibilitate
        var available = service.GetAvailableCars().ToList();
        Assert(available.Count == 1, "Mașină nu e disponibilă");
        
        // Test căutare după ID
        var car = service.GetById(result.Data.GetId());
        Assert(car != null, "Căutare după ID eșuată");
        
        Console.WriteLine("✓ CarService: OK\n");
    }
    
    private static void TestClientService()
    {
        Console.WriteLine("--- Test ClientService ---");
        var service = new ClientService();
        
        // Test adăugare
        var result = service.TryAddClient("Ion Popescu", "0723456789", "ion@test.com");
        Assert(result.Success, "Adăugare client eșuată");
        Assert(result.Data != null, "Client null după adăugare");
        Assert(result.Data!.GetName() == "Ion Popescu", "Nume incorect");
        
        // Test email duplicat
        var duplicate = service.TryAddClient("Alt Nume", "0723456788", "ion@test.com");
        Assert(!duplicate.Success, "Email duplicat nu a fost detectat");
        
        // Test listare
        var allClients = service.GetAllClients().ToList();
        Assert(allClients.Count == 1, "Număr clienți incorect");
        
        // Test căutare după email
        var client = service.GetByEmail("ion@test.com");
        Assert(client != null, "Căutare după email eșuată");
        
        Console.WriteLine("✓ ClientService: OK\n");
    }
    
    private static void TestRentalService()
    {
        Console.WriteLine("--- Test RentalService ---");
        var carService = new CarService();
        var clientService = new ClientService();
        var rentalService = new RentalService(carService, clientService);
        
        // Setup: adaugă mașină și client
        var carResult = carService.TryAddCar("Audi", "A4", 2021, 100.0);
        var clientResult = clientService.TryAddClient("Maria Ionescu", "0734567890", "maria@test.com");
        
        // Test creare închiriere
        var rentalResult = rentalService.TryCreateRental(
            carResult.Data!.GetId(),
            clientResult.Data!.GetId(),
            DateTime.Now,
            7
        );
        Assert(rentalResult.Success, "Creare închiriere eșuată");
        Assert(rentalResult.Data != null, "Închiriere null");
        Assert(rentalResult.Data!.GetDays() == 7, "Durată incorectă");
        Assert(rentalResult.Data.GetTotalPrice() == 700.0, "Preț total incorect");
        
        // Verifică că mașina nu mai e disponibilă
        var car = carService.GetById(carResult.Data.GetId());
        Assert(!car!.GetIsAvailable(), "Mașina ar trebui să fie indisponibilă");
        
        // Test listare închirieri active
        var activeRentals = rentalService.GetActiveRentals().ToList();
        Assert(activeRentals.Count == 1, "Număr închirieri active incorect");
        
        // Test închidere închiriere
        var closeResult = rentalService.TryCloseRental(rentalResult.Data.GetId());
        Assert(closeResult.Success, "Închidere închiriere eșuată");
        
        // Verifică că mașina e din nou disponibilă
        car = carService.GetById(carResult.Data.GetId());
        Assert(car!.GetIsAvailable(), "Mașina ar trebui să fie disponibilă");
        
        Console.WriteLine("✓ RentalService: OK\n");
    }
    
    private static void TestPersistence()
    {
        Console.WriteLine("--- Test Persistence ---");
        var carService = new CarService();
        var clientService = new ClientService();
        var rentalService = new RentalService(carService, clientService);
        var storage = new JsonStorage("test_data.json");
        var controller = new AppController(storage, carService, clientService, rentalService);
        
        // Adaugă date
        carService.TryAddCar("Mercedes", "C-Class", 2023, 180.0);
        clientService.TryAddClient("Alex Dumitru", "0745678901", "alex@test.com");
        
        // Salvare
        controller.Save();
        Assert(File.Exists("test_data.json"), "Fișier JSON nu a fost creat");
        
        // Creare servicii noi și încărcare
        var carService2 = new CarService();
        var clientService2 = new ClientService();
        var rentalService2 = new RentalService(carService2, clientService2);
        var controller2 = new AppController(storage, carService2, clientService2, rentalService2);
        
        controller2.Load();
        
        // Verificare date încărcate
        Assert(carService2.GetAllCars().Count() == 1, "Mașini nu au fost încărcate");
        Assert(clientService2.GetAllClients().Count() == 1, "Clienți nu au fost încărcați");
        
        var loadedCar = carService2.GetAllCars().First();
        Assert(loadedCar.GetBrand() == "Mercedes", "Date mașină incorecte după încărcare");
        
        // Cleanup
        File.Delete("test_data.json");
        
        Console.WriteLine("✓ Persistence: OK\n");
    }
    
    private static void Assert(bool condition, string message)
    {
        if (!condition)
        {
            throw new Exception($"TEST EȘUAT: {message}");
        }
    }
}
