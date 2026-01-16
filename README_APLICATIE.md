# Aplicație Închiriere Mașini - Ghid Complet

## Prezentare

Aceasta este o aplicație Windows Forms pentru gestionarea unei afaceri de închiriere auto. Aplicația implementează concepte avansate de Programare Orientată pe Obiecte (POO) și respectă toate cerințele proiectului academic.

## 🎯 Funcționalități Principale

### Gestionare Mașini
- ✅ Adăugare mașini noi cu detalii complete (brand, model, an, preț/zi)
- ✅ Vizualizare toate mașinile sau doar cele disponibile
- ✅ Căutare mașini după ID
- ✅ Status automat de disponibilitate (actualizat la închiriere/returnare)

### Gestionare Clienți
- ✅ Adăugare clienți noi cu validare email
- ✅ Prevenire duplicate (email unic)
- ✅ Căutare după ID sau Email
- ✅ Ștergere clienți

### Gestionare Închirieri
- ✅ Creare închiriere nouă cu validări complete
- ✅ Calcul automat preț total (zile × preț_pe_zi)
- ✅ Returnare mașini (închidere închirieri)
- ✅ Vizualizare închirieri active
- ✅ Filtrare închirieri după client
- ✅ Calcul zile rămase pentru o închiriere activă

### Persistență Date
- ✅ Salvare automată la închidere aplicație
- ✅ Încărcare automată la pornire
- ✅ Format JSON human-readable
- ✅ Fișier: `data.json`

## 📁 Structura Proiectului

```
InchirieriMasini/
├── Models/              # Modele de date (Car, Client, Rental)
├── Services/            # Logica de business (CarService, ClientService, RentalService)
├── Data/               # Persistență (JsonStorage, AppController, AppState)
├── Common/             # Utilități (Result<T> pentru gestionare erori)
├── Tests/              # Teste automate pentru verificare funcționalitate
├── Form1.cs            # UI principal (3 tab-uri)
├── Form1.Designer.cs   # Design UI generat
├── Program.cs          # Entry point aplicație
└── CONECTARE_UI_BACKEND.md  # Documentație detaliată conectare
```

## 🔧 Cum să Rulezi Aplicația

### Cerințe Sistem
- Windows 10/11
- .NET 10.0 SDK sau mai nou
- Visual Studio 2022 (recomandat) sau VS Code

### Metoda 1: Visual Studio
1. Deschide `InchirieriMasini.csproj` în Visual Studio
2. Apasă `F5` sau click pe "Start"
3. Aplicația se va porni automat

### Metoda 2: Command Line
```bash
cd proiect-uptispoo
dotnet build
dotnet run
```

### Metoda 3: Executable
```bash
cd proiect-uptispoo
dotnet build
cd bin/Debug/net10.0-windows
./InchirieriMasini.exe
```

## 🧪 Cum să Rulezi Testele

### Opțiunea 1: Din cod (Program.cs)
Decomentează linia în `Program.cs`:
```csharp
Tests.SimpleTests.RunAll();
```

Apoi rulează aplicația. Testele vor rula înaintea pornirii UI.

### Opțiunea 2: Console separată
```bash
dotnet build
dotnet run
# Apasă Ctrl+C după ce testele se finalizează
```

Testele verifică:
- ✅ CarService (adăugare, listare, disponibilitate, căutare)
- ✅ ClientService (adăugare, duplicate, căutare)
- ✅ RentalService (creare, închidere, calcul preț)
- ✅ Persistence (salvare/încărcare JSON)

## 📖 Ghid Utilizare

### 1. Pornire Aplicație
La prima pornire, aplicația va crea un fișier `data.json` gol. Dacă există deja date, acestea vor fi încărcate automat.

### 2. Tab Mașini
**Adăugare mașină nouă:**
1. Completează: Brand (ex: "BMW"), Model (ex: "X5")
2. Selectează: An (1990-2030), Preț/zi (ex: 150)
3. Click "Adaugă"
4. Mesaj success va afișa ID-ul mașinii noi

**Vizualizare mașini:**
- "Afișează toate" - toate mașinile din sistem
- "Afișează mașini disponibile" - doar mașinile neînchiriate

**Căutare:**
- Introdu ID mașină și click "Caută"

### 3. Tab Clienți
**Adăugare client:**
1. Completează: Nume, Prenume, Email
2. Click "Adaugă Client"
3. Email-ul trebuie să fie unic (nu permite duplicate)

**Căutare client:**
- După ID: Introdu ID și click "Caută"
- După Email: Introdu email și click "Caută"

**Ștergere client:**
- Introdu ID client și click "Șterge Client"

### 4. Tab Închirieri
**Creare închiriere nouă:**
1. ID Mașină: ID-ul mașinii de închiriat (trebuie să fie disponibilă)
2. ID Client: ID-ul clientului
3. Data Start: Alege din calendar
4. Număr zile: Câte zile (1-365)
5. Click "Creează"
6. Prețul total se calculează automat: zile × preț_pe_zi

**Returnare mașină:**
1. Introdu ID Închiriere
2. Click "Returnare"
3. Mașina devine disponibilă din nou

**Alte funcții:**
- "Închirieri Client" - vezi toate închirierile active ale unui client
- "Zile Rămase" - calculează zile rămase pentru o închiriere
- "Afișează închirieri active" - toate închirierile active din sistem

### 5. Închidere Aplicație
La închidere, toate datele sunt salvate automat în `data.json`.

## 🏗️ Concepte POO Implementate

### 1. Încapsulare
```csharp
// Toate câmpurile sunt private
private readonly int Id;
private readonly string Brand;

// Acces doar prin metode Get/Set
public int GetId() => Id;
public string GetBrand() => Brand;
```

### 2. Moștenire & Interfețe
```csharp
// Interfețe pentru servicii
public interface ICarService { ... }
public interface IClientService { ... }

// Implementare
public class CarService : ICarService { ... }
```

### 3. Polimorfism
```csharp
// Suprascriere ToString()
public override string ToString() => ...;

// Returnare tipuri diferite
public Result<Car> TryAddCar(...) { ... }
public Result<Client> TryAddClient(...) { ... }
```

### 4. Compoziție
```csharp
// RentalService conține referințe la alte servicii
public class RentalService
{
    private readonly ICarService carService;
    private readonly IClientService clientService;
    ...
}
```

## 🔐 Validări Implementate

### Mașini
- Brand și Model nu pot fi goale
- An între 1990-2030
- Preț între 0-10000

### Clienți
- Nume, Prenume, Email obligatorii
- Email valid (format)
- Email unic (fără duplicate)

### Închirieri
- Mașina trebuie să existe
- Clientul trebuie să existe
- Mașina trebuie să fie disponibilă
- Nu poate exista deja o închiriere activă pentru mașină
- Durată între 1-365 zile

## 💾 Structura Fișier JSON

```json
{
  "Cars": [
    {
      "Id": 10001,
      "Brand": "BMW",
      "Model": "X5",
      "Year": 2022,
      "PricePerDay": 150.0,
      "IsAvailable": false
    }
  ],
  "Clients": [
    {
      "Id": 2000,
      "Name": "Ion Popescu",
      "Phone": "0723456789",
      "Email": "ion@email.com"
    }
  ],
  "Rentals": [
    {
      "Id": 3001,
      "CarId": 10001,
      "ClientId": 2000,
      "StartDate": "2024-01-16T00:00:00",
      "Days": 7,
      "TotalPrice": 1050.0,
      "IsActive": true
    }
  ],
  "NextCarId": 10002,
  "NextClientId": 2001,
  "NextRentalId": 3002
}
```

## 🐛 Troubleshooting

### Aplicația nu pornește
- Verifică că ai .NET 10.0 SDK instalat: `dotnet --version`
- Verifică că ești pe Windows (aplicație Windows Forms)
- Rulează `dotnet build` pentru erori de compilare

### Date nu se salvează
- Verifică permisiuni folder (read/write)
- Verifică că `data.json` nu e read-only
- Verifică logs pentru erori

### Mașină nu devine disponibilă după returnare
- Verifică că închiderea închirierii a avut succes
- Reîncarcă aplicația
- Verifică `data.json` manual

### Erori la adăugare închiriere
Posibile cauze:
- ID mașină inexistent
- ID client inexistent
- Mașină deja închiriată
- Date invalide

## 📚 Documentație Suplimentară

Pentru detalii complete despre arhitectură și flux de date, vezi:
- [CONECTARE_UI_BACKEND.md](./CONECTARE_UI_BACKEND.md) - Documentație tehnică detaliată

## 👥 Contribuitori

Acest proiect a fost dezvoltat ca parte din cursul de Programare Orientată pe Obiecte (POO) la Universitatea Politehnica Timișoara.

## 📝 Licență

Proiect academic - Folosire doar în scopuri educaționale.
