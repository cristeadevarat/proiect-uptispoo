namespace InchirieriMasini.Persistence;

public static class AppStateSeed
{
    public static AppState Create()
    {
        var cars = new List<CarDto>
        {
            new() { Id = 10001, Brand="Dacia",       Model="Logan",        Year=2019, PricePerDay=120, IsAvailable=true },
            new() { Id = 10002, Brand="Dacia",       Model="Duster",       Year=2020, PricePerDay=180, IsAvailable=true },
            new() { Id = 10003, Brand="Renault",     Model="Clio",         Year=2018, PricePerDay=140, IsAvailable=true },
            new() { Id = 10004, Brand="Volkswagen",  Model="Golf",         Year=2019, PricePerDay=190, IsAvailable=true },
            new() { Id = 10005, Brand="Skoda",       Model="Octavia",      Year=2021, PricePerDay=210, IsAvailable=true },
            new() { Id = 10006, Brand="Toyota",      Model="Corolla",      Year=2022, PricePerDay=230, IsAvailable=true },
            new() { Id = 10007, Brand="Hyundai",     Model="i30",          Year=2020, PricePerDay=185, IsAvailable=true },
            new() { Id = 10008, Brand="BMW",         Model="320d",         Year=2020, PricePerDay=380, IsAvailable=true },
            new() { Id = 10009, Brand="Mercedes",    Model="C200",         Year=2021, PricePerDay=420, IsAvailable=true },
            new() { Id = 10010, Brand="Ford",        Model="Transit",      Year=2019, PricePerDay=260, IsAvailable=true }
        };

        var clients = new List<ClientDto>
        {
            new() { Id = 2000, Name="Andrei Pop",          Phone="0722000001", Email="andrei.pop@test.com" },
            new() { Id = 2001, Name="Ioana Ionescu",       Phone="0722000002", Email="ioana.ionescu@test.com" },
            new() { Id = 2002, Name="Mihai Dumitru",       Phone="0722000003", Email="mihai.dumitru@test.com" },
            new() { Id = 2003, Name="Maria Stan",          Phone="0722000004", Email="maria.stan@test.com" },
            new() { Id = 2004, Name="Radu Marinescu",      Phone="0722000005", Email="radu.marinescu@test.com" },
            new() { Id = 2005, Name="Elena Pavel",         Phone="0722000006", Email="elena.pavel@test.com" },
            new() { Id = 2006, Name="Cristina Enache",     Phone="0722000007", Email="cristina.enache@test.com" },
            new() { Id = 2007, Name="Vlad Georgescu",      Phone="0722000008", Email="vlad.georgescu@test.com" }
        };

        // Notă: TotalPrice este opțional (double?). Îl punem calculat ca să fie realist.
        // StartDate + Days (nu ai EndDate în DTO).
        var rentals = new List<RentalDto>
        {
            // ACTIVE (3)
            new() { Id = 3001, CarId=10006, ClientId=2000, StartDate=new DateTime(2026, 1, 18), Days=6,  IsActive=true,  TotalPrice = 6  * 230 },
            new() { Id = 3002, CarId=10002, ClientId=2003, StartDate=new DateTime(2026, 1, 20), Days=3,  IsActive=true,  TotalPrice = 3  * 180 },
            new() { Id = 3003, CarId=10010, ClientId=2005, StartDate=new DateTime(2026, 1, 15), Days=10, IsActive=true,  TotalPrice = 10 * 260 },

            // INACTIVE (3)
            new() { Id = 3004, CarId=10001, ClientId=2001, StartDate=new DateTime(2025, 12, 10), Days=4, IsActive=false, TotalPrice = 4  * 120 },
            new() { Id = 3005, CarId=10008, ClientId=2004, StartDate=new DateTime(2025, 11,  5), Days=2, IsActive=false, TotalPrice = 2  * 380 },
            new() { Id = 3006, CarId=10004, ClientId=2007, StartDate=new DateTime(2025, 10, 21), Days=7, IsActive=false, TotalPrice = 7  * 190 }
        };

        return new AppState
        {
            Cars = cars,
            Clients = clients,
            Rentals = rentals,

            // Next IDs (următorul liber)
            NextCarId = 10011,
            NextClientId = 2008,
            NextRentalId = 3007
        };
    }
}
