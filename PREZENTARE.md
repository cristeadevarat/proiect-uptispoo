# Prezentare Proiect - Închiriere Mașini

## 📊 Informații Prezentare PowerPoint

Acest document conține informațiile necesare pentru slide-urile PPT cerute în proiect.

---

## Slide 1: Titlu

**SISTEM DE ÎNCHIRIERE MAȘINI**
- Proiect: Programare Orientată pe Obiecte
- Universitatea Politehnica Timișoara
- Anul: 2026

---

## Slide 2: Descrierea Proiectului

### Ce Face Aplicația?
Sistem complet de management pentru o firmă de închirieri auto:
- Gestionare vehicule (mașini și camioane)
- Gestionare clienți
- Creare și finalizare închirieri
- Calcul automat costuri
- Persistență date în JSON

### Tehnologii
- C# .NET 9.0
- Windows Forms (Desktop UI)
- Dependency Injection (GenericHost)
- JSON pentru persistență

---

## Slide 3: Cerințe Implementate

### ✅ Cerințe Nota ≤ 8
- [x] Model OO complet (4 clase principale, 3 servicii)
- [x] Încapsulare, Moștenire, Polimorfism, Compoziție
- [x] Salvare/încărcare din fișier JSON
- [x] Tratare erori la accesare fișiere (try-catch, logging)
- [x] Clase wrapper pentru dependențe externe
- [x] Utilizare GitHub cu commit-uri separate

### ✅ Cerințe Nota ≤ 10
- [x] .NET Core GenericHost pentru DI și configurare
- [x] ILogger pentru colectare informații despre erori
- [x] Logging structurat în toate componentele

### ✅ Cerințe Bonus
- [x] Expresii LINQ (15+ locații)
- [x] Arhitectură MVC (Model-View-Controller)
- [x] Design framework-like (reutilizabil)

---

## Slide 4: Structura Proiectului

### Organizare pe Module

```
📁 Models/          - Entități de business (4 clase)
📁 Services/        - Business logic (3 servicii)
📁 Infrastructure/  - File I/O, Logging, Wrappers
📁 Interfaces/      - Contracte (abstracții)
📁 UI/              - Windows Forms (MainForm)
```

### Clase Principale
1. **Vehicle** (abstract) → Car, Truck
2. **Customer** (entitate client)
3. **Rental** (entitate închiriere)
4. **ApplicationState** (persistență)

---

## Slide 5: Concepte POO - Moștenire

### Ierarhie de Clase

```
Vehicle (abstract base class)
    ├── Car (inherited)
    └── Truck (inherited)
```

### Caracteristici
- **Vehicle**: Clasă abstractă cu proprietăți comune
  - Brand, Model, Year, LicensePlate
  - Metodă abstractă: `CalculateRentalCost()`
  - Metodă virtuală: `GetVehicleInfo()`

- **Car**: Specializare pentru autoturisme
  - NumberOfDoors, FuelType, Transmission
  - Implementare specifică a calculului cost

- **Truck**: Specializare pentru camioane
  - CargoCapacity, NumberOfAxles
  - Cost include supliment pentru capacitate

---

## Slide 6: Concepte POO - Polimorfism

### Metodă Abstractă Implementată Diferit

```csharp
// Car - cost standard + supliment automatic
public override decimal CalculateRentalCost(int days)
{
    decimal baseCost = PricePerDay * days;
    if (Transmission == "automatic")
        baseCost += 10 * days;
    return baseCost;
}

// Truck - cost + supliment capacitate
public override decimal CalculateRentalCost(int days)
{
    decimal baseCost = PricePerDay * days;
    decimal capacitySurcharge = CargoCapacity * 5 * days;
    return baseCost + capacitySurcharge;
}
```

### Beneficii
- Fiecare tip de vehicul calculează costul specific
- Cod flexibil și extensibil
- Ușor de adăugat noi tipuri de vehicule

---

## Slide 7: Concepte POO - Compoziție

### Rental HAS-A Relationships

```csharp
public class Rental
{
    public Customer Customer { get; set; }  // HAS-A
    public Vehicle Vehicle { get; set; }    // HAS-A
    
    // Properties
    public DateTime StartDate { get; set; }
    public decimal TotalCost { get; set; }
    public RentalStatus Status { get; set; }
}
```

### Avantaje
- Relații clare între entități
- Separare responsabilități
- Flexibilitate în design

---

## Slide 8: Concepte POO - Încapsulare

### Properties și Computed Values

```csharp
public class Customer
{
    // Private backing fields (implicit)
    public string FirstName { get; set; }
    public string LastName { get; set; }
    
    // Computed property (read-only)
    public string FullName => $"{FirstName} {LastName}";
}

public class Rental
{
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    
    // Computed property
    public int ActualDays => EndDate.HasValue 
        ? (EndDate.Value - StartDate).Days 
        : (DateTime.Now - StartDate).Days;
}
```

---

## Slide 9: Dependency Injection

### Configurare în Program.cs

```csharp
Host.CreateDefaultBuilder()
    .ConfigureServices((context, services) =>
    {
        // Wrappers
        services.AddSingleton<IFileStorage, JsonFileStorage>();
        
        // Services
        services.AddSingleton<VehicleService>();
        services.AddSingleton<CustomerService>();
        services.AddSingleton<RentalService>();
        
        // UI
        services.AddTransient<MainForm>();
    });
```

### Beneficii
- Loose coupling
- Testabilitate
- Configurare centralizată

---

## Slide 10: Wrapper Pattern

### Izolare Dependențe Externe

```csharp
// Interface (abstracție)
public interface IFileStorage
{
    void Save<T>(string filePath, T data);
    T? Load<T>(string filePath);
}

// Implementare (concrete)
public class JsonFileStorage : IFileStorage
{
    public void Save<T>(string filePath, T data)
    {
        // JSON serialization + error handling
    }
}
```

### Avantaje
- Aplicația nu depinde direct de System.IO
- Ușor de testat (mock objects)
- Ușor de înlocuit implementarea

---

## Slide 11: LINQ Examples

### Operații Complexe pe Colecții

```csharp
// Filtrare și sortare
var available = vehicles
    .Where(v => v.IsAvailable)
    .OrderBy(v => v.PricePerDay);

// Type filtering (polimorfism)
var cars = vehicles
    .OfType<Car>()
    .OrderBy(c => c.Brand);

// Căutare complexă
var results = vehicles
    .Where(v => v.Brand.Contains(searchTerm) ||
                v.Model.Contains(searchTerm))
    .OrderBy(v => v.Brand);

// Agregare
var totalRevenue = rentals
    .Where(r => r.Status == RentalStatus.Completed)
    .Sum(r => r.TotalCost);
```

---

## Slide 12: Error Handling & Logging

### Tratarea Erorilor

```csharp
public void Save<T>(string filePath, T data)
{
    try
    {
        _logger.LogInformation("Saving to {FilePath}", filePath);
        
        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(filePath, json);
        
        _logger.LogInformation("Save successful");
    }
    catch (UnauthorizedAccessException ex)
    {
        _logger.LogError(ex, "Access denied");
        throw new InvalidOperationException($"Access denied", ex);
    }
    catch (IOException ex)
    {
        _logger.LogError(ex, "I/O error");
        throw new InvalidOperationException($"Failed to save", ex);
    }
}
```

---

## Slide 13: Arhitectura MVC

### Separare Responsabilități

**Model** (Models/):
- Vehicle, Car, Truck
- Customer
- Rental
- Doar date, fără logică de business

**Controller** (Services/):
- VehicleService
- CustomerService
- RentalService
- Business logic, validări, LINQ

**View** (MainForm):
- UI Controls (TextBox, ListBox, Button)
- Event handlers
- Data binding
- Fără business logic

---

## Slide 14: Evoluția Design-ului

### Iterație 1: Structura Inițială
- Creare clase de bază (Vehicle, Customer)
- UI simplu WinForms
- Hard-coded data

### Iterație 2: Adăugare Persistență
- Implementare file I/O
- JSON serialization
- Error handling

### Iterație 3: Dependency Injection
- Extragere interfaces
- Configurare GenericHost
- Constructor injection

### Iterație 4: Logging & Services
- Adăugare ILogger
- Separare business logic în servicii
- LINQ operations

### Iterație 5: Polisare
- UI îmbunătățit (tabs)
- Data seeder
- Documentație completă

---

## Slide 15: Decizii Principale de Design

### 1. JSON vs Database
**Decizie**: JSON pentru persistență
**Motivație**: Simplitate, cerințe proiect, portabilitate

### 2. Singleton pentru Services
**Decizie**: Services ca Singleton în DI
**Motivație**: Partajare stare, performanță

### 3. Abstract Vehicle Class
**Decizie**: Vehicle ca clasă abstractă
**Motivație**: Comportament comun, polimorfism necesar

### 4. Composition pentru Rental
**Decizie**: Rental conține Customer și Vehicle
**Motivație**: Relație naturală, separation of concerns

### 5. Wrapper Pattern
**Decizie**: Interfaces pentru dependențe externe
**Motivație**: Testabilitate, loose coupling, SOLID

### 6. MVC Architecture
**Decizie**: Separare strictă Model-View-Controller
**Motivație**: Maintainability, scalability

---

## Slide 16: Demo Flow

### Scenarii de Utilizare

1. **Adăugare Vehicul**
   - Introducere date în form
   - Salvare în sistem
   - Refresh listă

2. **Adăugare Client**
   - Înregistrare client nou
   - Validare date
   - Persistență

3. **Creare Închiriere**
   - Selectare client (ID)
   - Selectare vehicul disponibil (ID)
   - Calcul automat cost
   - Marcare vehicul indisponibil

4. **Finalizare Închiriere**
   - Introducere ID închiriere
   - Marcare completată
   - Eliberare vehicul

---

## Slide 17: Statistici Proiect

### Cod Scris
- **17 fișiere .cs**
- **~1,500 linii de cod**
- **4 modele** (Vehicle, Car, Truck, Customer, Rental)
- **3 servicii** (Vehicle, Customer, Rental)
- **2 interfaces** (IFileStorage, IConsoleWrapper)
- **2 implementări** (JsonFileStorage, ConsoleWrapper)

### Caracteristici
- **15+ operații LINQ**
- **Constructor injection** în 7 clase
- **Error handling** în toate operațiile I/O
- **Logging** în toate componentele
- **Sample data** (6 vehicule, 4 clienți)

---

## Slide 18: Lecții Învățate

### Tehnică
1. Dependency Injection simplifică foarte mult testarea
2. Logging este esențial pentru debugging
3. LINQ face codul mai concis și expresiv
4. Wrapper pattern izolează aplicația de framework

### Design
1. Separarea în layers face codul mai ușor de înțeles
2. Interface-urile oferă flexibilitate
3. Composition over inheritance (unde e posibil)
4. SOLID principles se aplică practic

### Colaborare
1. Git branching și commits separate
2. Documentație clară ajută echipa
3. Code review îmbunătățește calitatea

---

## Slide 19: Extensii Viitoare

### Posibile Îmbunătățiri

**Funcționalități**:
- Autentificare utilizatori
- Rapoarte și statistici
- Notificări email pentru întârzieri
- Istoric închirieri detaliat

**Tehnică**:
- Migrare la SQL Database
- API REST pentru acces remote
- Teste unitare cu xUnit
- UI modern cu WPF sau Blazor

**Business**:
- Pricing dinamic bazat pe cerere
- Program de fidelitate clienți
- Asigurări și damage reports
- Multi-location support

---

## Slide 20: Concluzie

### Obiective Atinse ✅

✅ **Cerințe nota 8**: Toate implementate
✅ **Cerințe nota 10**: Toate implementate
✅ **Bonus**: LINQ, MVC, arhitectură framework

### Concepte Aplicate
- Încapsulare, Moștenire, Polimorfism, Compoziție
- Dependency Injection
- Logging
- Error Handling
- LINQ
- SOLID Principles
- Design Patterns (Wrapper, MVC)

### Rezultat
Aplicație funcțională, extensibilă, și bine documentată care demonstrează înțelegerea profundă a conceptelor POO și best practices în C#.

---

## Resurse Utile

- **Cod Sursă**: https://github.com/cristeadevarat/proiect-uptispoo
- **README**: Documentație completă cu exemple
- **ARHITECTURA.md**: Explicații detaliate despre conectarea componentelor
- **Commits**: Evoluție pas cu pas a proiectului

---

**Întrebări?**
