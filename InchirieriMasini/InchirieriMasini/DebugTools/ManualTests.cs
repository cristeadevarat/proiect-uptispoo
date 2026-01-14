using System;
using InchirieriMasini.Services;

namespace InchirieriMasini.DebugTools;

public static class ManualTests
{
    public static void Run()
    {
        var carService = new CarService();
        var clientService = new ClientService();
        var rentalService = new RentalService(carService, clientService);

        var c1 = carService.AddCar("Dacia", "Logan", 2018, 120);
        var c2 = carService.AddCar("VW", "Golf", 2020, 200);

        var cl1 = clientService.AddClient("Ana Pop", "0712345678", "ana@email.com");

        var r1 = rentalService.CreateRental(c1.GetId(), cl1.GetId(), DateTime.Today, 3);

        Console.WriteLine("=== RENTAL CREAT ===");
        Console.WriteLine(r1);

        Console.WriteLine("=== MASINI DISPONIBILE ===");
        foreach (var car in carService.GetAvailableCars())
            Console.WriteLine(car);

        Console.WriteLine("=== ZILE RAMASE ===");
        Console.WriteLine(rentalService.GetDaysRemaining(r1.GetId(), DateTime.Today));

        Console.WriteLine("=== INCHID RENTAL ===");
        Console.WriteLine(rentalService.CloseRental(r1.GetId()));

        Console.WriteLine("=== MASINI DISPONIBILE DUPA RETURNARE ===");
        foreach (var car in carService.GetAvailableCars())
            Console.WriteLine(car);
    }
}