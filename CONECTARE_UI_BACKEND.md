# Documentație Conectare UI - Backend

## Prezentare Generală

Această aplicație de închiriere mașini (InchirieriMasini) are acum interfața grafică (UI) complet conectată la serviciile backend. Toate funcționalitățile cerute sunt implementate și funcționale.

## Arhitectura Aplicației

### 1. **Nivel Model (Models/)**
- **Car.cs**: Model pentru mașini cu proprietăți (ID, Brand, Model, An, Preț/zi, Disponibilitate)
- **Client.cs**: Model pentru clienți cu proprietăți (ID, Nume, Telefon, Email)
- **Rental.cs**: Model pentru închirieri cu proprietăți (ID, ID Mașină, ID Client, Dată Start, Durată, Preț Total, Status Activ)

### 2. **Nivel Servicii (Services/)**
- **CarService.cs**: Gestionează operațiuni cu mașini
  - Adăugare, ștergere, căutare mașini
  - Listare toate mașinile / mașini disponibile
  - Marcare mașină ca închiriată/returnată
  
- **ClientService.cs**: Gestionează operațiuni cu clienți
  - Adăugare, ștergere, căutare clienți
  - Validare email duplicat
  - Listare toți clienții
  
- **RentalService.cs**: Gestionează operațiuni de închiriere
  - Creare închiriere nouă
  - Returnare mașină (închidere închiriere)
  - Listare închirieri active
  - Calcul zile rămase
  - Calcul preț total

### 3. **Nivel Persistență (Data/)**
- **JsonStorage.cs**: Salvează și încarcă date din fișier JSON
- **AppState.cs**: Structură de date pentru serializare JSON (DTOs)
- **AppController.cs**: Orchestrează salvarea/încărcarea datelor și sincronizează starea

### 4. **Nivel UI (Form1.cs)**
- Formular principal cu 3 tab-uri: Masini, Clienti, Inchirieri
- Event handlers pentru toate butoanele
- Refresh automat al DataGridView-urilor după operațiuni
- Mesaje de status pentru feedback utilizator

### 5. **Utilități (Common/)**
- **Result.cs**: Pattern pentru gestionare erori fără excepții către UI

## Flux de Date

### Inițializare Aplicație
```
Program.cs (Main)
    ↓
Form1 Constructor
    ↓
Inițializare Servicii (CarService, ClientService, RentalService)
    ↓
Inițializare JsonStorage și AppController
    ↓
AppController.Load() - încarcă date din data.json
    ↓
Sincronizare disponibilitate mașini cu închirieri active
    ↓
Refresh DataGridView-uri (afișare date inițiale)
```

### Exemplu: Adăugare Mașină Nouă
```
Utilizator: Completează form (Brand, Model, An, Preț) → Click "Adaugă"
    ↓
Form1.BtnAdaugaMasina_Click()
    ↓
Validare câmpuri (Brand, Model obligatorii)
    ↓
CarService.TryAddCar()
    ↓
Creare obiect Car cu ID automat (10001+)
    ↓
Returnare Result<Car> (Success=true, Data=car)
    ↓
RefreshCarsGrid() - actualizare DataGridView
    ↓
Afișare mesaj success cu ID-ul mașinii
    ↓
Clear formular
```

### Exemplu: Creare Închiriere
```
Utilizator: Selectează Car ID, Client ID, Data Start, Durată → Click "Creează"
    ↓
Form1.BtnCreeazaInchiriere_Click()
    ↓
RentalService.TryCreateRental()
    ↓
Validări:
  - Mașina există?
  - Clientul există?
  - Mașina e disponibilă?
  - Nu există închiriere activă pentru mașină?
    ↓
Creare Rental cu calcul preț total automat (zile × preț_pe_zi)
    ↓
Marcare mașină ca indisponibilă
    ↓
Returnare Result<Rental> (Success=true, Data=rental)
    ↓
RefreshRentalsGrid() + RefreshCarsGrid()
    ↓
Afișare mesaj cu ID închiriere și preț total
```

### Salvare Date (la închidere aplicație)
```
Utilizator: Închide fereastra
    ↓
Form1.FormClosing event
    ↓
AppController.Save()
    ↓
Export date din servicii (Cars, Clients, Rentals)
    ↓
Creare AppState cu date + next IDs
    ↓
JsonStorage.Save() - serializare JSON cu formatare
    ↓
Scriere în data.json
```

## Funcționalități UI

### Tab 1: Mașini
1. **Afișează toate mașinile** - Listează toate mașinile din sistem
2. **Afișează mașini disponibile** - Filtrare doar mașini neînchiriate
3. **Adaugă mașină** - Form cu Brand, Model, An (1990-2030), Preț/zi
4. **Caută după ID** - Găsește o mașină specifică după ID
5. **Status label** - Feedback pentru toate operațiunile

### Tab 2: Clienți
1. **Adaugă client** - Form cu Nume, Prenume, Email (validare duplicat)
2. **Șterge client** - Ștergere după ID
3. **Caută după ID** - Găsește client după ID
4. **Caută după Email** - Găsește client după email
5. **Status label** - Feedback pentru operațiuni

### Tab 3: Închirieri
1. **Creează închiriere** - Form cu ID Mașină, ID Client, Data Start, Durată (zile)
2. **Returnare** - Închide închiriere și eliberează mașina
3. **Închirieri client** - Afișează toate închirierile active ale unui client
4. **Zile rămase** - Calculează zile rămase pentru o închiriere activă
5. **Afișează închirieri active** - Listează toate închirierile active
6. **Status label** - Feedback și informații

## Validări Implementate

### Mașini
- Brand și Model sunt obligatorii
- An între 1990-2030
- Preț între 0-10000

### Clienți
- Nume, Prenume, Email obligatorii
- Email unic (nu permite duplicate)
- Validare format email în model

### Închirieri
- Verificare existență mașină
- Verificare existență client
- Verificare disponibilitate mașină
- Verificare că mașina nu are deja închiriere activă
- Durată între 1-365 zile
- Calcul automat preț total

## Persistență Date

### Fișier: data.json
Structura JSON:
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

### Generare ID-uri Automate
- **Mașini**: Start 10001, increment automat
- **Clienți**: Start 2000, increment automat
- **Închirieri**: Start 3001, increment automat

## Concepte POO Folosite

### 1. **Încapsulare**
- Toate câmpurile private în modele
- Acces prin metode Get/Set
- Validare în constructori

### 2. **Moștenire & Interfețe**
- ICarService, IClientService, IRentalService (interfețe)
- Implementare în CarService, ClientService, RentalService

### 3. **Polimorfism**
- Suprascriere ToString() în modele
- Metode Try* returnează Result<T> pentru gestionare erori

### 4. **Compoziție**
- RentalService conține referințe la ICarService și IClientService
- AppController compune Storage + toate Serviciile
- Form1 compune toate serviciile

## Gestionare Erori

### Pattern Result<T>
În loc de excepții către UI, serviciile returnează:
```csharp
public record Result(bool Success, string Message);
public record Result<T>(bool Success, string Message, T? Data);
```

Exemple:
```csharp
// Succes
return new Result<Car>(true, "", car);

// Eroare
return new Result<Car>(false, "Mașina nu există", null);
```

### Avantaje
- UI primește întotdeauna un răspuns valid
- Mesaje de eroare clare pentru utilizator
- Fără catch blocks în UI
- Cod mai curat și mai sigur

## Cum să Rulezi Aplicația

### Cerințe
- .NET 10.0 SDK
- Windows OS (pentru Windows Forms)
- Visual Studio 2022 sau VS Code cu C# extension

### Pași
1. Deschide terminalul în folderul proiectului
2. Rulează `dotnet build` pentru a compila
3. Rulează `dotnet run` pentru a lansa aplicația
4. SAU deschide `InchirieriMasini.csproj` în Visual Studio și apasă F5

### Testare
1. Adaugă câteva mașini (ex: BMW X5, Audi A4)
2. Adaugă câțiva clienți
3. Creează închirieri pentru mașini disponibile
4. Verifică că mașinile devin indisponibile
5. Returnează o mașină
6. Verifică că mașina devine din nou disponibilă
7. Închide aplicația și redeschide-o
8. Verifică că datele au fost salvate în data.json

## Structura Codului

### Form1.cs - Metode Principale

#### Inițializare
- `Form1()` - Constructor, inițializare servicii + încărcare date
- `WireUpEvents()` - Conectare event handlers la butoane
- `AddLabels()` - Adăugare etichete pentru controale

#### Refresh DataGridViews
- `RefreshCarsGrid()` - Actualizare tabel mașini
- `RefreshClientsGrid()` - Actualizare tabel clienți
- `RefreshRentalsGrid()` - Actualizare tabel închirieri

#### Event Handlers Mașini (9 metode)
- `BtnAfiseazaToate_Click()` - Afișează toate mașinile
- `BtnDisponibile_Click()` - Filtrare mașini disponibile
- `BtnAdaugaMasina_Click()` - Adăugare mașină nouă
- `BtnCauta_Click()` - Căutare după ID

#### Event Handlers Clienți (4 metode)
- `BtnAdaugaClient_Click()` - Adăugare client nou
- `BtnStergeClient_Click()` - Ștergere client
- `BtnCautaClientId_Click()` - Căutare după ID
- `BtnCautaClientEmail_Click()` - Căutare după email

#### Event Handlers Închirieri (5 metode)
- `BtnCreeazaInchiriere_Click()` - Creare închiriere nouă
- `BtnReturnare_Click()` - Returnare mașină
- `BtnInchirieriClient_Click()` - Afișare închirieri client
- `BtnZileRamase_Click()` - Calcul zile rămase
- `BtnAfisareInchirieriActive_Click()` - Afișare toate închirierile active

#### Cleanup
- `Form1_FormClosing()` - Salvare date la închidere

## Îmbunătățiri Viitoare Posibile

1. **Validări UI mai avansate**
   - Validare format email în real-time
   - Autocomplete pentru ID-uri mașini/clienți
   - Calendar picker cu zile indisponibile

2. **Rapoarte**
   - Export la Excel/PDF
   - Statistici închirieri
   - Top mașini închiriate

3. **Search avansat**
   - Căutare mașini după brand/model
   - Filtrare după preț
   - Sortare coloane în DataGridView

4. **LINQ Extensions**
   - Folosire LINQ pentru filtrări complexe
   - Query optimization

5. **Logging**
   - ILogger pentru erori și operațiuni
   - Audit trail pentru modificări

6. **Teste Unitare**
   - xUnit/NUnit pentru servicii
   - Mock objects pentru isolate testing

7. **Bază de Date**
   - Entity Framework Core
   - SQL Server / PostgreSQL / MongoDB

8. **Arhitectură MVC**
   - Separare Controllers
   - ViewModels pentru UI
   - Dependency Injection Container

## Concluzie

Aplicația este acum **complet funcțională** cu:
✅ UI complet conectat la backend
✅ Toate operațiuni CRUD implementate
✅ Persistență date în JSON
✅ Validări comprehensive
✅ Gestionare erori robustă
✅ Feedback utilizator clar
✅ Cod bine structurat și documentat
✅ Respectare principii POO

Aplicația poate fi folosită imediat pentru gestionarea unei afaceri de închiriere auto!
