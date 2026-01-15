# Arhitectura Aplicației - Explicație Detaliată

## 📚 Cum Sunt Conectate Componentele

Acest document explică cum cele trei părți ale proiectului (cod, WinForms, și integrare) sunt conectate împreună.

## 🔗 Fluxul de Conectare

### 1. Entry Point: Program.cs

```
Program.Main()
    ↓
CreateHostBuilder() - configurează DI Container
    ↓
Înregistrare servicii în DI
    ↓
Build host
    ↓
Seed date sample
    ↓
Obține MainForm din DI
    ↓
Rulează aplicația WinForms
```

### 2. Dependency Injection Container

Program.cs creează un container DI care știe cum să creeze toate dependențele:

```csharp
services.AddSingleton<IFileStorage, JsonFileStorage>();
services.AddSingleton<VehicleService>();
services.AddSingleton<CustomerService>();
services.AddSingleton<RentalService>();
services.AddTransient<MainForm>();
```

**Ce înseamnă asta?**
- Când cineva cere un `IFileStorage`, va primi o instanță de `JsonFileStorage`
- Când cineva cere un `VehicleService`, va primi aceeași instanță mereu (Singleton)
- Când cineva cere un `MainForm`, va primi o instanță nouă (Transient)

### 3. Constructor Injection în MainForm

```csharp
public MainForm(
    VehicleService vehicleService,
    CustomerService customerService,
    RentalService rentalService,
    ILogger<MainForm> logger)
{
    _vehicleService = vehicleService;
    _customerService = customerService;
    _rentalService = rentalService;
    _logger = logger;
    
    InitializeComponent();
    LoadData();
}
```

**Ce se întâmplă aici?**
1. DI Container-ul vede că MainForm cere 4 dependențe
2. Creează instanțe pentru fiecare (sau le refolosește dacă sunt Singleton)
3. Le pasează în constructor
4. MainForm acum are acces la toate serviciile

### 4. Constructor Injection în Services

```csharp
public VehicleService(IFileStorage fileStorage, ILogger<VehicleService> logger)
{
    _fileStorage = fileStorage;
    _logger = logger;
    // ...
}
```

**Ce se întâmplă aici?**
1. VehicleService cere IFileStorage și ILogger
2. DI Container-ul vede cererea
3. Creează JsonFileStorage (implementare IFileStorage)
4. Creează ILogger specific pentru VehicleService
5. Le pasează în constructor

## 🎯 Exemplu Concret: Adăugarea unui Vehicul

Să urmărim fluxul complet:

### Pasul 1: User Click în UI
```csharp
// MainForm.cs
private void btnAddVehicle_Click(object sender, EventArgs e)
{
    var car = new Car { /* date din form */ };
    _vehicleService.AddVehicle(car);  // Apel la serviciu
    RefreshVehicles();
}
```

### Pasul 2: Serviciul procesează
```csharp
// VehicleService.cs
public void AddVehicle(Vehicle vehicle)
{
    _logger.LogInformation("Adding vehicle...");  // Logging
    vehicle.Id = _state.NextVehicleId++;
    _state.Vehicles.Add(vehicle);
    SaveState();  // Salvează în fișier
}
```

### Pasul 3: Salvare în fișier
```csharp
// VehicleService.cs -> JsonFileStorage
private void SaveState()
{
    _fileStorage.Save(_dataFilePath, _state);  // Wrapper pattern
}

// JsonFileStorage.cs
public void Save<T>(string filePath, T data)
{
    _logger.LogInformation("Saving...");  // Logging
    try {
        // Serializare JSON
        var json = JsonSerializer.Serialize(data);
        File.WriteAllText(filePath, json);
    }
    catch (IOException ex) {
        _logger.LogError(ex, "Error saving");  // Error handling
        throw;
    }
}
```

### Pasul 4: Refresh UI
```csharp
// MainForm.cs
private void RefreshVehicles()
{
    lstVehicles.Items.Clear();
    var vehicles = _vehicleService.GetAllVehicles();  // LINQ
    foreach (var vehicle in vehicles)
    {
        lstVehicles.Items.Add(vehicle.GetVehicleInfo());  // Polimorfism
    }
}
```

## 🧩 Cum Componentele Comunică

### Model → Service
```
Car (Model) ← Creat în → VehicleService
    ↓
Stocat în ApplicationState
    ↓
Serializat prin JsonFileStorage
    ↓
Salvat în app_state.json
```

### Service → View
```
VehicleService
    ↓
GetAllVehicles() returnează IEnumerable<Vehicle>
    ↓
MainForm primește datele
    ↓
Afișează în ListBox (lstVehicles)
```

### View → Service → Model
```
User input în TextBox
    ↓
btnAddVehicle_Click event handler
    ↓
Creează new Car() din date
    ↓
Apelează VehicleService.AddVehicle()
    ↓
Service salvează în ApplicationState
    ↓
Persistat pe disk prin IFileStorage
```

## 🔄 Ciclul Complet de Viață

```
[START] Program.Main()
    ↓
[CONFIG] CreateHostBuilder() - setup DI
    ↓
[INIT] DataSeeder.SeedData() - date inițiale
    ↓
[CREATE] DI creează MainForm
    ↓
[INJECT] DI injectează Services în MainForm
    ↓
[INJECT] DI injectează IFileStorage în Services
    ↓
[INJECT] DI injectează ILogger în toate clasele
    ↓
[LOAD] MainForm.LoadData() - citește din fișier
    ↓
[DISPLAY] UI afișează datele
    ↓
[INTERACTION] User interacționează cu UI
    ↓
[BUSINESS] Services procesează operații
    ↓
[PERSIST] JsonFileStorage salvează pe disk
    ↓
[LOG] ILogger înregistrează evenimente
    ↓
[REFRESH] UI se actualizează
    ↓
[END] User închide aplicația
```

## 🎓 Concepte Cheie

### 1. Separation of Concerns
- **Models**: Nu știu de Services sau UI
- **Services**: Nu știu de UI, folosesc doar Models
- **UI (MainForm)**: Nu știu de file I/O, folosesc doar Services

### 2. Dependency Inversion Principle
```
MainForm → depinde de → VehicleService (abstracție)
VehicleService → depinde de → IFileStorage (interface)
JsonFileStorage → implementează → IFileStorage
```

UI și Services depind de abstracții, nu de implementări concrete.

### 3. Single Responsibility
- **JsonFileStorage**: Doar file I/O
- **VehicleService**: Doar business logic pentru vehicule
- **MainForm**: Doar UI și event handling

### 4. Inversion of Control (IoC)
Container-ul DI controlează crearea obiectelor, nu clasele în sine:
```
// NU așa (tight coupling):
var storage = new JsonFileStorage(new Logger());
var service = new VehicleService(storage, new Logger());
var form = new MainForm(service, ...);

// CI așa (loose coupling cu DI):
var form = host.Services.GetRequiredService<MainForm>();
// DI creează automat toate dependențele
```

## 📊 Diagrama Dependențelor

```
Program.cs (Entry Point)
    ↓
    ├── IHostBuilder (DI Container)
    │       ↓
    │       ├── IFileStorage → JsonFileStorage
    │       ├── ILogger → Logger Factory
    │       ├── VehicleService
    │       ├── CustomerService
    │       ├── RentalService
    │       └── DataSeeder
    │
    └── MainForm (UI)
            ↓
            ├── VehicleService
            ├── CustomerService
            ├── RentalService
            └── ILogger
                    ↓
                    ├── VehicleService uses IFileStorage
                    ├── CustomerService uses IFileStorage
                    └── RentalService uses IFileStorage
```

## ✅ Checklist Integrare

Pentru a conecta codul și WinForms:

1. ✅ Creat modele (Car, Customer, Rental)
2. ✅ Creat servicii (VehicleService, CustomerService, RentalService)
3. ✅ Creat interfaces (IFileStorage, IConsoleWrapper)
4. ✅ Implementat wrappers (JsonFileStorage, ConsoleWrapper)
5. ✅ Configurat DI în Program.cs
6. ✅ Actualizat MainForm cu constructor injection
7. ✅ Implementat event handlers în MainForm
8. ✅ Conectat UI controls la servicii
9. ✅ Adăugat logging peste tot
10. ✅ Implementat error handling

## 🎯 Concluzie

Aplicația este construită pe principii SOLID:
- **S**ingle Responsibility: Fiecare clasă are un singur scop
- **O**pen/Closed: Extensibil prin moștenire (Vehicle → Car, Truck)
- **L**iskov Substitution: Car și Truck pot înlocui Vehicle
- **I**nterface Segregation: Interface-uri mici și specifice
- **D**ependency Inversion: Dependențe pe abstracții

Toate componentele sunt **loose coupled** (cuplaj slab) și **highly cohesive** (coeziune mare).
