# Proiect UPT - Închiriere Mașini (Car Rental System)

Aplicație C# WinForms pentru gestionarea închirierii de mașini, dezvoltată pentru cursul de Programare Orientată pe Obiecte.

## 🎯 Descriere

Acest proiect implementează un sistem complet de gestionare a închirierilor de vehicule, cu funcționalități pentru:
- Gestionarea vehiculelor (mașini și camioane)
- Gestionarea clienților
- Crearea și finalizarea închirierilor
- Calcularea automată a costurilor
- Persistența datelor în format JSON

## 🏗️ Arhitectura Aplicației

Proiectul respectă toate cerințele de la nota 10 și bonus:

### ✅ Concepte POO Implementate

1. **Încapsulare**: 
   - Toate clasele au proprietăți cu accesori corespunzători
   - Logica de business este izolată în servicii

2. **Moștenire**:
   - Clasa abstractă `Vehicle` → `Car`, `Truck`
   - Ierarhie clară de clase

3. **Polimorfism**:
   - Metoda abstractă `CalculateRentalCost()` implementată diferit în `Car` și `Truck`
   - Metoda virtuală `GetVehicleInfo()` suprascrisă în clasele derivate

4. **Compoziție**:
   - `Rental` HAS-A `Customer` și `Vehicle`
   - Relații clare între entități

### 🔧 Arhitectură și Patterns

#### Model-View-Controller (MVC)
- **Model**: `Models/` - entități de business
- **View**: `MainForm.cs`, `MainForm.Designer.cs` - interfața WinForms
- **Controller**: `Services/` - logica de business

#### Dependency Injection
Folosim `.NET Core GenericHost` pentru a gestiona dependențele:

```csharp
services.AddSingleton<IFileStorage, JsonFileStorage>();
services.AddSingleton<VehicleService>();
services.AddSingleton<CustomerService>();
services.AddSingleton<RentalService>();
```

#### Wrapper Pattern
Izolarea dependențelor externe:
- `IFileStorage` / `JsonFileStorage` - wrapper pentru System.IO
- `IConsoleWrapper` / `ConsoleWrapper` - wrapper pentru Console

### 📁 Structura Proiectului

```
InchirieriMasini/
├── Models/                    # Entități de business
│   ├── Vehicle.cs            # Clasă abstractă de bază
│   ├── Car.cs                # Moștenire: Vehicle → Car
│   ├── Truck.cs              # Moștenire: Vehicle → Truck
│   ├── Customer.cs           # Entitate Client
│   └── Rental.cs             # Entitate Închiriere (compoziție)
├── Services/                  # Business logic
│   ├── VehicleService.cs     # CRUD vehicule + LINQ
│   ├── CustomerService.cs    # CRUD clienți + LINQ
│   └── RentalService.cs      # Business logic închirieri
├── Infrastructure/            # Infrastructură
│   ├── JsonFileStorage.cs    # Implementare I/O cu error handling
│   ├── ConsoleWrapper.cs     # Wrapper pentru consolă
│   ├── ApplicationState.cs   # DTO pentru persistență
│   └── DataSeeder.cs         # Date de test
├── Interfaces/                # Contracte (abstracții)
│   ├── IFileStorage.cs       # Interface pentru file I/O
│   └── IConsoleWrapper.cs    # Interface pentru consolă
├── MainForm.cs               # UI Controller
├── MainForm.Designer.cs      # UI Design
└── Program.cs                # Entry point + DI setup
```

## 🚀 Funcționalități Implementate

### Cerințe Nota ≤ 8
- ✅ Model OO complet cu toate conceptele POO
- ✅ Salvare/încărcare din fișier JSON
- ✅ Tratarea erorilor la accesarea fișierelor (try-catch în JsonFileStorage)
- ✅ Clase wrapper pentru dependențe externe (IFileStorage, IConsoleWrapper)
- ✅ Utilizare GitHub cu commit-uri separate

### Cerințe Nota ≤ 10
- ✅ .NET Core GenericHost pentru DI și configurare
- ✅ ILogger în toate serviciile pentru logging
- ✅ Logging structurat în JsonFileStorage

### Cerințe Bonus
- ✅ **LINQ**: Operații complexe în toate serviciile (Where, OrderBy, Sum, OfType, etc.)
- ✅ **MVC Pattern**: Separare clară Model-View-Controller
- ✅ **Framework-like**: Arhitectură reutilizabilă pentru alte domenii

## 💻 Tehnologii Utilizate

- **.NET 9.0** - Framework modern
- **WinForms** - Interfață grafică desktop
- **Microsoft.Extensions.Hosting** - GenericHost pentru DI
- **Microsoft.Extensions.Logging** - Logging structurat
- **System.Text.Json** - Serializare/deserializare

## 🎮 Cum să Rulezi Aplicația

### Prerequisite
- .NET 9.0 SDK sau mai nou
- Windows (pentru WinForms)
- Visual Studio 2022 sau JetBrains Rider

### Pași

1. Clonează repository-ul:
```bash
git clone https://github.com/cristeadevarat/proiect-uptispoo.git
cd proiect-uptispoo/InchirieriMasini
```

2. Restaurează pachetele NuGet:
```bash
dotnet restore
```

3. Build-uiește proiectul:
```bash
dotnet build
```

4. Rulează aplicația:
```bash
dotnet run --project InchirieriMasini/InchirieriMasini.csproj
```

## 📊 Exemple de Utilizare LINQ

### Filtrare vehicule disponibile
```csharp
var available = _vehicleService.GetAvailableVehicles();
// Echivalent: vehicles.Where(v => v.IsAvailable).OrderBy(v => v.PricePerDay)
```

### Căutare cu condiții multiple
```csharp
var results = _vehicleService.SearchVehicles("BMW");
// Folosește Where cu StringComparison pentru căutare case-insensitive
```

### Filtrare pe tip (polimorfism)
```csharp
var cars = _vehicleService.GetAllCars();
// Folosește OfType<Car>() pentru a extrage doar mașinile
```

### Agregare
```csharp
var totalRevenue = _rentalService.GetTotalRevenue();
// Folosește Sum() pentru a calcula venit total
```

## 🔐 Gestionarea Erorilor

Toate operațiile de I/O sunt protejate cu try-catch și logging:

```csharp
try 
{
    _fileStorage.Save(filePath, data);
}
catch (UnauthorizedAccessException ex)
{
    _logger.LogError(ex, "Access denied");
    throw new InvalidOperationException($"Access denied to file: {filePath}", ex);
}
catch (IOException ex)
{
    _logger.LogError(ex, "I/O error");
    throw new InvalidOperationException($"Failed to save file: {filePath}", ex);
}
```

## 📦 Persistența Datelor

Datele sunt salvate în format JSON în fișierul:
```
{AppDirectory}/Data/app_state.json
```

Structura JSON:
```json
{
  "Vehicles": [...],
  "Customers": [...],
  "Rentals": [...],
  "NextVehicleId": 1,
  "NextCustomerId": 1,
  "NextRentalId": 1
}
```

## 🧪 Date de Test

La prima rulare, aplicația generează automat date de test:
- 4 mașini (Toyota, Honda, BMW, Volkswagen)
- 2 camioane (Mercedes, Ford)
- 4 clienți cu date românești

## 👥 Echipa

- **Dezvoltator 1**: Business logic (Models, Services, Infrastructure)
- **Dezvoltator 2**: WinForms UI (MainForm, Designer)
- **Dezvoltator 3**: Integrare (DI, Program.cs, conectare componente)

## 📝 Design Decisions

1. **Singleton pentru Services**: Serviciile sunt singleton pentru a partaja aceeași stare
2. **JSON vs Database**: JSON pentru simplitate și cerințele proiectului
3. **Tabs în UI**: Organizare logică pentru fiecare entitate
4. **Auto-increment IDs**: Gestionare simplă a ID-urilor unice

## 🔮 Evoluție Viitoare

Posibile îmbunătățiri:
- [ ] Bază de date SQL Server / SQLite
- [ ] Teste unitare cu xUnit / NUnit
- [ ] API REST pentru remote access
- [ ] Autentificare utilizatori
- [ ] Rapoarte PDF
- [ ] Dashboard cu statistici

## 📄 Licență

Proiect educațional - UPT ISPOO

---

**Nota**: Acest proiect respectă toate cerințele pentru nota 10 și implementează funcționalități bonus (LINQ, MVC pattern, arhitectură framework-like).

