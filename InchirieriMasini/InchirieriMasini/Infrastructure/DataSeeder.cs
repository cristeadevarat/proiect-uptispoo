using InchirieriMasini.Models;
using InchirieriMasini.Services;
using Microsoft.Extensions.Logging;

namespace InchirieriMasini.Infrastructure;

/// <summary>
/// Seeds the application with sample data for testing purposes
/// </summary>
public class DataSeeder
{
    private readonly VehicleService _vehicleService;
    private readonly CustomerService _customerService;
    private readonly ILogger<DataSeeder> _logger;

    public DataSeeder(
        VehicleService vehicleService,
        CustomerService customerService,
        ILogger<DataSeeder> logger)
    {
        _vehicleService = vehicleService;
        _customerService = customerService;
        _logger = logger;
    }

    public void SeedData()
    {
        try
        {
            _logger.LogInformation("Checking if sample data needs to be seeded");

            // Only seed if there's no data
            if (_vehicleService.GetAllVehicles().Any() || _customerService.GetAllCustomers().Any())
            {
                _logger.LogInformation("Data already exists, skipping seed");
                return;
            }

            _logger.LogInformation("Seeding sample data");

            // Add sample cars
            var cars = new List<Car>
            {
                new Car
                {
                    Brand = "Toyota",
                    Model = "Corolla",
                    Year = 2022,
                    LicensePlate = "B-01-ABC",
                    PricePerDay = 50,
                    NumberOfDoors = 4,
                    FuelType = "Hybrid",
                    Transmission = "Automatic",
                    IsAvailable = true
                },
                new Car
                {
                    Brand = "Honda",
                    Model = "Civic",
                    Year = 2021,
                    LicensePlate = "B-02-DEF",
                    PricePerDay = 45,
                    NumberOfDoors = 4,
                    FuelType = "Petrol",
                    Transmission = "Manual",
                    IsAvailable = true
                },
                new Car
                {
                    Brand = "BMW",
                    Model = "320i",
                    Year = 2023,
                    LicensePlate = "B-03-GHI",
                    PricePerDay = 80,
                    NumberOfDoors = 4,
                    FuelType = "Diesel",
                    Transmission = "Automatic",
                    IsAvailable = true
                },
                new Car
                {
                    Brand = "Volkswagen",
                    Model = "Golf",
                    Year = 2020,
                    LicensePlate = "B-04-JKL",
                    PricePerDay = 40,
                    NumberOfDoors = 5,
                    FuelType = "Petrol",
                    Transmission = "Manual",
                    IsAvailable = true
                }
            };

            foreach (var car in cars)
            {
                _vehicleService.AddVehicle(car);
            }

            // Add sample trucks
            var trucks = new List<Truck>
            {
                new Truck
                {
                    Brand = "Mercedes",
                    Model = "Sprinter",
                    Year = 2022,
                    LicensePlate = "B-10-MNO",
                    PricePerDay = 100,
                    CargoCapacity = 3.5m,
                    NumberOfAxles = 2,
                    IsAvailable = true
                },
                new Truck
                {
                    Brand = "Ford",
                    Model = "Transit",
                    Year = 2021,
                    LicensePlate = "B-11-PQR",
                    PricePerDay = 90,
                    CargoCapacity = 3.0m,
                    NumberOfAxles = 2,
                    IsAvailable = true
                }
            };

            foreach (var truck in trucks)
            {
                _vehicleService.AddVehicle(truck);
            }

            // Add sample customers
            var customers = new List<Customer>
            {
                new Customer
                {
                    FirstName = "Ion",
                    LastName = "Popescu",
                    Email = "ion.popescu@email.com",
                    PhoneNumber = "+40 722 123 456",
                    DriverLicenseNumber = "POP123456"
                },
                new Customer
                {
                    FirstName = "Maria",
                    LastName = "Ionescu",
                    Email = "maria.ionescu@email.com",
                    PhoneNumber = "+40 733 234 567",
                    DriverLicenseNumber = "ION234567"
                },
                new Customer
                {
                    FirstName = "Andrei",
                    LastName = "Dumitrescu",
                    Email = "andrei.dum@email.com",
                    PhoneNumber = "+40 744 345 678",
                    DriverLicenseNumber = "DUM345678"
                },
                new Customer
                {
                    FirstName = "Elena",
                    LastName = "Georgescu",
                    Email = "elena.geo@email.com",
                    PhoneNumber = "+40 755 456 789",
                    DriverLicenseNumber = "GEO456789"
                }
            };

            foreach (var customer in customers)
            {
                _customerService.AddCustomer(customer);
            }

            _logger.LogInformation("Sample data seeded successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error seeding sample data");
        }
    }
}
