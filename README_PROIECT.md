# Proiect UPTISPOO - Sistem de Închirieri Mașini 🚗

## Despre Proiect

Acesta este un **sistem complet de management pentru închirierea mașinilor**, dezvoltat în C# folosind Windows Forms (.NET 10.0). Aplicația oferă o interfață grafică intuitivă pentru gestionarea operațiunilor zilnice ale unei firme de rent-a-car.

## 📋 Cuprins
1. [Prezentare Generală](#prezentare-generală)
2. [Arhitectura Aplicației](#arhitectura-aplicației)
3. [Funcționalități](#funcționalități)
4. [Tehnologii Utilizate](#tehnologii-utilizate)
5. [Concepte OOP Aplicate](#concepte-oop-aplicate)
6. [Design Patterns](#design-patterns)
7. [Structura Proiectului](#structura-proiectului)
8. [Cum să Rulezi Aplicația](#cum-să-rulezi-aplicația)

---

## Prezentare Generală

### Ce Rezolvă Această Aplicație?

Aplicația automatizează procesele unei firme de închirieri auto:
- **Gestionarea Parcului Auto**: Adăugare, vizualizare și căutare mașini
- **Management Clienți**: Înregistrare clienți, ștergere și căutare după multiple criterii
- **Procesare Închirieri**: Creare închirieri, returnări, monitorizare perioadă de închiriere

### Cazuri de Utilizare Reale

1. **Operator Rent-a-Car**: Verifică disponibilitatea mașinilor și creează o nouă închiriere
2. **Manager**: Monitorizează toate închirierile active și clienții fideli
3. **Recepționer**: Procesează returnarea unei mașini și calculează zilele de întârziere

---

## Arhitectura Aplicației

### Principii Arhitecturale

Aplicația respectă **separarea responsabilităților** (Separation of Concerns):

```
┌─────────────────────────────────────────┐
│         PRESENTATION LAYER              │
│    (Windows Forms - UI Controls)        │
│  - DataGridView pentru afișare date     │
│  - Butoane pentru acțiuni utilizator    │
│  - GroupBox pentru organizare logică    │
└───────────────┬─────────────────────────┘
                │
                ↓
┌─────────────────────────────────────────┐
│         BUSINESS LOGIC LAYER            │
│    (Logica aplicației - in Form1.cs)    │
│  - Validare date                         │
│  - Procesare operațiuni business        │
│  - Coordonare între UI și Date          │
└───────────────┬─────────────────────────┘
                │
                ↓
┌─────────────────────────────────────────┐
│         DATA ACCESS LAYER               │
│    (Gestionare date - potențial)        │
│  - CRUD operations                       │
│  - Persistență date (DB/Files)          │
└─────────────────────────────────────────┘
```

### Stratul de Prezentare (UI Layer)

**Fișier**: `Form1.Designer.cs`

Acest strat conține toată interfața grafică organizată în **3 taburi principale**:

1. **Tab Mașini** - Gestionarea inventarului de vehicule
2. **Tab Clienți** - Managementul bazei de clienți
3. **Tab Închirieri** - Operațiuni de închiriere și returnare

**Principii de Design UI**:
- **Consistență**: Toate formularele urmează același pattern (GroupBox + controale + buton acțiune)
- **Feedback vizual**: Label-uri de status pentru fiecare tab (`lblStatus`, `lblClientStatus`, `lblInchirieriStatus`)
- **Usability**: Placeholder text pentru câmpuri, validare prin NumericUpDown pentru ID-uri

### Stratul de Logică Business

**Fișier**: `Form1.cs`

Acest strat ar trebui să conțină:
- **Event Handlers**: Metode care răspund la acțiuni utilizator (click butoane)
- **Validare**: Verificarea corectitudinii datelor introduse
- **Reguli Business**: 
  - O mașină nu poate fi închiriată dacă este deja în uz
  - Un client trebuie să existe pentru a crea o închiriere
  - Calculul costului total = prețul pe zi × numărul de zile

### Stratul de Date (Data Layer)

**Ce ar trebui să conțină**:
- **Clase Model**: `Car`, `Client`, `Rental`
- **Repository Pattern**: Clase care gestionează CRUD-ul
- **Persistență**: Bază de date (SQLite/SQL Server) sau fișiere JSON/XML

---

## Funcționalități

### 1️⃣ Modul MAȘINI (Tab 1)

#### Afișare Mașini
```csharp
// Exemplu conceptual de funcționalitate
private void btnAfiseazaToate_Click(object sender, EventArgs e)
{
    // Încarcă toate mașinile din baza de date
    // Afișează în dgvMasini
    lblStatus.Text = "Afișate 15 mașini";
}
```

**Controale Disponibile**:
- `dgvMasini` - DataGridView pentru afișarea listei de mașini
- `btnAfiseazaToate` - Afișează toate mașinile din sistem
- `btnDisponibile` - Filtrează doar mașinile disponibile (neînchiriate)

#### Adăugare Mașină Nouă
**Câmpuri necesare**:
- `txtBrand` - Marca mașinii (ex: "Dacia", "BMW", "Mercedes")
- `txtModel` - Modelul (ex: "Logan", "X5", "C-Class")
- `numYear` - Anul fabricației (1990-2030)
- `numPrice` - Prețul pe zi (0.00 - 10,000.00 RON)

**Validări Necesare**:
- Brand și Model nu pot fi goale
- Anul trebuie să fie realist
- Prețul trebuie să fie > 0

#### Căutare Mașină
- `numSearchId` - Căutare după ID unic
- Rezultat afișat în grid sau mesaj de eroare

### 2️⃣ Modul CLIENȚI (Tab 2)

#### Operațiuni CRUD
**Create (Adăugare)**:
- Nume, Prenume, Email
- Validare: Email trebuie să aibă format valid (@)

**Read (Căutare)**:
- După ID: `numClientId` + `btnCautaClientId`
- După Email: `txtSearchEmail` + `btnCautaClientEmail`

**Delete (Ștergere)**:
- `txtIdClient` + `btnStergeClient`
- **Atenție**: Trebuie verificat dacă clientul are închirieri active!

**Update**: Nu este implementat în UI, dar ar putea fi adăugat

#### DataGridView pentru Clienți
```
| ID | Nume      | Prenume | Email                 | Nr. Închirieri |
|----|-----------|---------|----------------------|----------------|
| 1  | Popescu   | Ion     | ion.popescu@mail.com | 3              |
| 2  | Ionescu   | Maria   | maria.i@mail.com     | 1              |
```

### 3️⃣ Modul ÎNCHIRIERI (Tab 3)

#### Creare Închiriere Nouă
**Câmpuri**:
- `numCreeazaCarId` - ID-ul mașinii de închiriat
- `numCreeazaClientId` - ID-ul clientului
- `dtpStartDate` - Data de început
- `numDays` - Durata în zile (1-365)

**Proces**:
1. Verifică dacă mașina există și e disponibilă
2. Verifică dacă clientul există
3. Creează închirierea cu status "Active"
4. Calculează data de returnare: StartDate + Days
5. Actualizează status mașină la "Rented"

#### Returnare Mașină
- `numRentalId` - ID închiriere
- **Acțiuni**:
  - Marchează închirierea ca "Completed"
  - Setează data de returnare efectivă
  - Eliberează mașina (status = "Available")
  - Calculează costuri suplimentare dacă e întârziere

#### Raportare
- **Închirieri Active**: Afișează toate închirierile în curs
- **Închirieri Client**: Istoric pentru un client specific
- **Zile Rămase**: Calculează câte zile mai sunt până la returnare

---

## Tehnologii Utilizate

### .NET 10.0 Windows Forms
**De ce Windows Forms?**
- ✅ Simplu pentru aplicații desktop
- ✅ Drag-and-drop UI designer în Visual Studio
- ✅ Performanță bună pentru operațiuni locale
- ✅ Nu necesită browser (spre deosebire de aplicații web)

**Alternativa modernă**: WPF (Windows Presentation Foundation) sau .NET MAUI pentru cross-platform

### C# Language Features

#### 1. Implicit Usings
```xml
<ImplicitUsings>enable</ImplicitUsings>
```
Elimină necesitatea de `using System;`, `using System.Windows.Forms;` etc.

#### 2. Nullable Reference Types
```xml
<Nullable>enable</Nullable>
```
Previne erori de tip `NullReferenceException`:
```csharp
// Fără nullable:
string email;  // Poate fi null, compilatorul nu avertizează

// Cu nullable:
string email;   // Nu poate fi null
string? emailOptional;  // Poate fi null, trebuie verificat
```

#### 3. File-Scoped Namespaces
```csharp
namespace InchirieriMasini;  // Fără { }

public class Form1 : Form
{
    // ...
}
```

### Componente Windows Forms Utilizate

| Component | Scop | Exemplu Utilizare |
|-----------|------|-------------------|
| `DataGridView` | Afișare date tabulare | Listă mașini, clienți, închirieri |
| `Button` | Acțiuni utilizator | Adaugă, Șterge, Caută |
| `TextBox` | Input text liber | Nume, Email, Brand |
| `NumericUpDown` | Input numeric validat | ID-uri, An, Preț, Zile |
| `DateTimePicker` | Selectare dată | Data de început închiriere |
| `GroupBox` | Organizare logică | Grupare controale înrudite |
| `TabControl` | Navigare multiplu | Separare Mașini/Clienți/Închirieri |
| `Label` | Afișare informații | Status, Feedback utilizator |

---

## Concepte OOP Aplicate

### 1. Encapsulation (Încapsulare)

**Definiție**: Ascunderea detaliilor de implementare și expunerea doar a interfeței necesare.

**În proiect**:
```csharp
public class Car
{
    // Câmpuri private - nu pot fi accesate direct din exterior
    private int id;
    private string brand;
    private decimal dailyPrice;
    private bool isAvailable;
    
    // Proprietăți publice - controlează accesul la câmpuri
    public int Id 
    { 
        get => id; 
        private set => id = value;  // Set-ul e privat
    }
    
    public string Brand 
    { 
        get => brand; 
        set 
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Brand-ul nu poate fi gol");
            brand = value;
        }
    }
    
    // Metodă publică care folosește logică internă
    public void Rent()
    {
        if (!isAvailable)
            throw new InvalidOperationException("Mașina e deja închiriată");
        isAvailable = false;
    }
}
```

**Avantaje**:
- ✅ Validare centralizată în properties
- ✅ Imposibilitatea de a seta valori invalide
- ✅ Ușor de modificat implementarea fără a afecta codul existent

### 2. Inheritance (Moștenire)

**În proiect**: `Form1 : Form`

```csharp
// Clasa de bază din .NET Framework
public class Form 
{
    public void Show() { ... }
    public void Close() { ... }
    protected virtual void OnLoad(EventArgs e) { ... }
}

// Clasa noastră moștenește toate funcționalitățile
public partial class Form1 : Form
{
    // Avem automat toate metodele de la Form
    // + putem adăuga propriile noastre
    
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);  // Apelăm logica din clasa părinte
        // + adăugăm logica noastră
        LoadInitialData();
    }
}
```

**Exemplu conceptual pentru entități**:
```csharp
// Clasa de bază
public abstract class Vehicle
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public decimal DailyPrice { get; set; }
    
    public abstract decimal CalculateRentalCost(int days);
}

// Clasă derivată - mașină
public class Car : Vehicle
{
    public int NumberOfSeats { get; set; }
    
    public override decimal CalculateRentalCost(int days)
    {
        return DailyPrice * days;
    }
}

// Clasă derivată - SUV (preț special weekend)
public class SUV : Vehicle
{
    public bool Has4WD { get; set; }
    
    public override decimal CalculateRentalCost(int days)
    {
        decimal cost = DailyPrice * days;
        // Discount 10% pentru închirieri > 7 zile
        if (days > 7)
            cost *= 0.9m;
        return cost;
    }
}
```

### 3. Polymorphism (Polimorfism)

**Definiție**: Capacitatea unui obiect de a lua multiple forme.

```csharp
// Avem o listă de vehicule diferite
List<Vehicle> fleet = new List<Vehicle>
{
    new Car { DailyPrice = 100 },
    new SUV { DailyPrice = 150 },
    new Van { DailyPrice = 120 }
};

// Polimorfism: apelăm aceeași metodă pe obiecte diferite
foreach (var vehicle in fleet)
{
    decimal cost = vehicle.CalculateRentalCost(5);
    // Fiecare tip calculează diferit, dar interfața e aceeași!
    Console.WriteLine($"Cost: {cost}");
}
```

**Tipuri de polimorfism**:

**Compile-time (Method Overloading)**:
```csharp
public class RentalService
{
    // Aceeași metodă, parametri diferiți
    public Rental CreateRental(int carId, int clientId, int days)
    {
        return CreateRental(carId, clientId, DateTime.Now, days);
    }
    
    public Rental CreateRental(int carId, int clientId, DateTime startDate, int days)
    {
        // Implementare completă
    }
}
```

**Runtime (Method Overriding)**:
```csharp
public class BaseClient
{
    public virtual decimal GetDiscount()
    {
        return 0m;  // Fără discount implicit
    }
}

public class LoyalClient : BaseClient
{
    public override decimal GetDiscount()
    {
        return 0.15m;  // 15% discount
    }
}
```

### 4. Abstraction (Abstractizare)

**Definiție**: Expunerea doar a caracteristicilor esențiale, ascunzând complexitatea.

```csharp
public interface IRentalRepository
{
    // Contract: orice implementare TREBUIE să aibă aceste metode
    Rental GetById(int id);
    List<Rental> GetActiveRentals();
    void Create(Rental rental);
    void MarkAsReturned(int rentalId);
}

// Implementare cu bază de date
public class DatabaseRentalRepository : IRentalRepository
{
    public Rental GetById(int id)
    {
        // SELECT * FROM Rentals WHERE Id = @id
    }
}

// Implementare cu fișiere
public class FileRentalRepository : IRentalRepository
{
    public Rental GetById(int id)
    {
        // Citește din JSON file
    }
}

// Cod care folosește repository-ul
public class RentalService
{
    private readonly IRentalRepository _repo;
    
    // Dependency Injection: nu știm ce implementare primim
    public RentalService(IRentalRepository repo)
    {
        _repo = repo;  // Poate fi orice implementare!
    }
    
    public void ProcessReturn(int rentalId)
    {
        var rental = _repo.GetById(rentalId);  // Nu ne interesează UNDE e stocat
        // ... logică
        _repo.MarkAsReturned(rentalId);
    }
}
```

---

## Design Patterns

### 1. Partial Classes Pattern

**Fișiere**: `Form1.cs` + `Form1.Designer.cs`

**De ce?**
- Separă codul generat de Visual Studio (Designer) de codul scris manual
- Designer-ul poate regenera codul fără a suprascrie logica noastră

```csharp
// Form1.Designer.cs - generat automat
partial class Form1
{
    private void InitializeComponent()
    {
        // Inițializare controale UI
    }
}

// Form1.cs - cod manual
partial class Form1 : Form
{
    private void btnAdaugaMasina_Click(object sender, EventArgs e)
    {
        // Logica noastră
    }
}
```

### 2. Repository Pattern (Recomandat)

```csharp
public interface IRepository<T>
{
    T GetById(int id);
    List<T> GetAll();
    void Add(T entity);
    void Update(T entity);
    void Delete(int id);
}

public class CarRepository : IRepository<Car>
{
    private List<Car> _cars = new List<Car>();  // Simulare DB
    
    public Car GetById(int id)
    {
        return _cars.FirstOrDefault(c => c.Id == id);
    }
    
    public List<Car> GetAll()
    {
        return _cars.ToList();
    }
    
    public void Add(Car car)
    {
        car.Id = _cars.Count + 1;
        _cars.Add(car);
    }
}
```

**Avantaje**:
- Separare logică business de acces date
- Ușor de testat (mock repositories)
- Ușor de schimbat sursa de date

### 3. Model-View-Controller (MVC) - Adaptat pentru WinForms

```
MODEL               VIEW              CONTROLLER
(Classes)           (Form1.Designer)  (Form1.cs)
   │                     │                  │
   │                     │                  │
Car.cs ────────────> DataGridView ────> Event Handlers
   │                     │                  │
   │                     └──────────────────┘
   │                    User clicks button
   │                           │
   └───────────────────────────┘
        Controller updates Model
```

### 4. Event-Driven Programming

**Whole aplicația se bazează pe evenimente**:

```csharp
// Event: utilizatorul dă click
btnAdaugaMasina.Click += btnAdaugaMasina_Click;

// Event Handler: codul care răspunde
private void btnAdaugaMasina_Click(object sender, EventArgs e)
{
    // 1. Validează input
    if (string.IsNullOrEmpty(txtBrand.Text))
    {
        MessageBox.Show("Brand-ul este obligatoriu!");
        return;
    }
    
    // 2. Creează obiect
    var car = new Car
    {
        Brand = txtBrand.Text,
        Model = txtModel.Text,
        Year = (int)numYear.Value,
        DailyPrice = numPrice.Value
    };
    
    // 3. Salvează în repository
    _carRepository.Add(car);
    
    // 4. Actualizează UI
    LoadCarsGrid();
    lblStatus.Text = $"Mașina {car.Brand} {car.Model} a fost adăugată!";
    
    // 5. Curăță formular
    ClearCarForm();
}
```

### 5. Dependency Injection (Recomandat pentru Testare)

```csharp
public class Form1 : Form
{
    private readonly ICarRepository _carRepo;
    private readonly IClientRepository _clientRepo;
    private readonly IRentalService _rentalService;
    
    // Constructor injection
    public Form1(
        ICarRepository carRepo,
        IClientRepository clientRepo,
        IRentalService rentalService)
    {
        InitializeComponent();
        _carRepo = carRepo;
        _clientRepo = clientRepo;
        _rentalService = rentalService;
    }
}

// In Program.cs
static void Main()
{
    // Configurează dependencies
    var carRepo = new CarRepository();
    var clientRepo = new ClientRepository();
    var rentalService = new RentalService(carRepo, clientRepo);
    
    Application.Run(new Form1(carRepo, clientRepo, rentalService));
}
```

---

## Structura Proiectului

```
proiect-uptispoo/
│
├── Form1.cs                    # Logica principală (event handlers)
├── Form1.Designer.cs           # UI design (generat automat)
├── Program.cs                  # Entry point al aplicației
├── InchirieriMasini.csproj     # Configurare proiect .NET
│
├── Models/                     # (Recomandat) Clase de date
│   ├── Car.cs
│   ├── Client.cs
│   └── Rental.cs
│
├── Repositories/               # (Recomandat) Acces la date
│   ├── IRepository.cs
│   ├── CarRepository.cs
│   ├── ClientRepository.cs
│   └── RentalRepository.cs
│
├── Services/                   # (Recomandat) Logică business
│   ├── RentalService.cs
│   └── ValidationService.cs
│
├── Data/                       # (Opțional) Bază de date sau fișiere
│   ├── rentals.db
│   └── migrations/
│
└── Resources/                  # (Opțional) Imagini, icoane
    └── car-icon.png
```

---

## Cum să Rulezi Aplicația

### Cerințe de Sistem

- **Windows 10/11** (Windows Forms nu rulează pe macOS/Linux)
- **.NET 10.0 SDK**: [Download](https://dotnet.microsoft.com/download/dotnet/10.0)
- **Visual Studio 2022** (recomandat) sau Visual Studio Code

### Pași de Rulare

#### Opțiunea 1: Visual Studio
```bash
1. Deschide InchirieriMasini.sln în Visual Studio
2. Build > Build Solution (sau Ctrl+Shift+B)
3. Debug > Start Debugging (sau F5)
```

#### Opțiunea 2: Command Line
```bash
cd proiect-uptispoo
dotnet build
dotnet run
```

#### Opțiunea 3: Executabil
```bash
cd bin/Debug/net10.0-windows
InchirieriMasini.exe
```

### Troubleshooting

**Eroare: "SDK-ul .NET 10.0 nu este instalat"**
```bash
# Verifică versiunea instalată
dotnet --list-sdks

# Instalează .NET 10.0 de pe site-ul oficial Microsoft
```

**Eroare: "Windows Forms nu este disponibil"**
- Asigură-te că ai `<UseWindowsForms>true</UseWindowsForms>` în `.csproj`
- Rulezi pe Windows, nu pe Linux/macOS

**Aplicația pornește dar e goală**
- Verifică că `InitializeComponent()` este apelat în constructor
- Verifică că nu există erori în `Form1.Designer.cs`

---

## Demonstrație Flow Complet

### Scenariul: Închiriere Mașină pentru Client Nou

**Pasul 1: Adăugare Client**
```
TAB: Clienți
→ txtNume: "Popescu"
→ txtPrenume: "Ion"
→ txtEmail: "ion.popescu@email.com"
→ Click: "Adaugă Client"
→ Rezultat: Client creat cu ID = 5
```

**Pasul 2: Verificare Mașini Disponibile**
```
TAB: Mașini
→ Click: "Afișează mașini disponibile"
→ Rezultat în grid:
   | ID | Brand | Model | An   | Preț/zi | Status     |
   |----|-------|-------|------|---------|------------|
   | 3  | Dacia | Logan | 2020 | 120 RON | Disponibil |
   | 7  | BMW   | X5    | 2022 | 350 RON | Disponibil |
```

**Pasul 3: Creare Închiriere**
```
TAB: Închirieri
→ numCreeazaCarId: 3
→ numCreeazaClientId: 5
→ dtpStartDate: 21/01/2026
→ numDays: 7
→ Click: "Creează"
→ Rezultat: Închiriere ID=15, Cost estimat: 840 RON
```

**Pasul 4: (După 7 zile) Returnare**
```
TAB: Închirieri
→ numRentalId: 15
→ Click: "Returnare"
→ Rezultat: Închiriere închisă, Mașina ID=3 disponibilă din nou
```

---

## Extensii Posibile ale Proiectului

### Nivel Beginner
1. ✅ Adăugare logo firmă în aplicație
2. ✅ Export listă mașini în CSV
3. ✅ Sortare mașini după preț
4. ✅ Filtrare după brand

### Nivel Intermediate
5. 🔶 Bază de date SQLite pentru persistență
6. 🔶 Sistem de autentificare (Login)
7. 🔶 Calcul automat al costului total
8. 🔶 Rapoarte PDF pentru închirieri

### Nivel Advanced
9. 🔥 Trimitere email confirmare la client
10. 🔥 Notificări pentru închirieri care expiră mâine
11. 🔥 Dashboard cu statistici (grafice)
12. 🔥 API REST pentru aplicație mobilă

---

## Întrebări Frecvente (FAQ)

### Q1: De ce Windows Forms și nu WPF?
**R**: WinForms e mai simplu pentru începători, dar WPF oferă UI mai modern.

### Q2: Unde sunt stocate datele?
**R**: Momentan, datele sunt în memorie (se pierd la închidere). Trebuie adăugată persistență.

### Q3: Cum testez aplicația fără bază de date?
**R**: Folosește liste in-memory (`List<Car>`) ca store temporar.

### Q4: Aplicația poate rula pe Linux?
**R**: Nu direct. Alternativă: Avalonia UI sau .NET MAUI (cross-platform).

### Q5: Cum adaug imagini pentru mașini?
**R**: Adaugă coloană `ImagePath` în model + PictureBox în form pentru afișare.

---

## Licență și Autor

**Proiect Academic**: UPTISPOO 2026  
**Scop**: Învățare concepte OOP și dezvoltare aplicații desktop C#

---

## Referințe și Resurse

### Documentație Oficială
- [Microsoft .NET Docs](https://docs.microsoft.com/dotnet/)
- [Windows Forms Documentation](https://docs.microsoft.com/dotnet/desktop/winforms/)
- [C# Programming Guide](https://docs.microsoft.com/dotnet/csharp/)

### Tutoriale Recomandate
- [Windows Forms Tutorial - Derek Banas](https://www.youtube.com/watch?v=Vb1p6wCXcWE)
- [C# OOP Concepts - Mosh Hamedani](https://www.youtube.com/watch?v=pTB0EiLXUC8)

### Cărți
- **"C# 12 in a Nutshell"** - Joseph Albahari
- **"Head First Design Patterns"** - Freeman & Robson
- **"Clean Code"** - Robert C. Martin

---

**Mult succes la prezentare! 🚀**
