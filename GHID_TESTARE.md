# 🚀 Ghid Clonare și Testare Aplicație

## 📋 Cuprins
1. [Cerințe Preliminare](#cerințe-preliminare)
2. [Clonare Repository](#clonare-repository)
3. [Navigare la Folderul Corect](#navigare-la-folderul-corect)
4. [Build și Rulare Aplicație](#build-și-rulare-aplicație)
5. [Testare Funcționalități](#testare-funcționalități)
6. [Rezolvare Probleme](#rezolvare-probleme)

---

## 🔧 Cerințe Preliminare

Înainte de a clona și testa aplicația, asigură-te că ai instalate următoarele:

### Windows:
- ✅ **Git** - [Download Git](https://git-scm.com/download/win)
- ✅ **.NET 9.0 SDK** sau mai nou - [Download .NET](https://dotnet.microsoft.com/download)
- ✅ **JetBrains Rider** (recomandat pentru testare) - [Download Rider](https://www.jetbrains.com/rider/)
- ✅ **Visual Studio 2022** (alternativ) - [Download VS](https://visualstudio.microsoft.com/)

### Verificare instalare:
```bash
# Verifică Git
git --version

# Verifică .NET SDK
dotnet --version
```

---

## 📥 Clonare Repository

### Pasul 1: Deschide Terminal/Command Prompt

**Windows:**
- Apasă `Win + R`
- Tastează `cmd` și apasă Enter
- SAU deschide `Git Bash` (dacă ai instalat Git)

### Pasul 2: Navighează la Locația Dorită

```bash
# Exemplu: pe Desktop
cd Desktop

# SAU pe C:\Projects
cd C:\Projects
```

### Pasul 3: Clonează Repository-ul

```bash
git clone https://github.com/cristeadevarat/proiect-uptispoo.git
```

**Așteptare:** Descărcarea poate dura 10-30 secunde în funcție de conexiune.

### Pasul 4: Intră în Folderul Clonat

```bash
cd proiect-uptispoo
```

### Pasul 5: Schimbă pe Branch-ul Corect

```bash
# Schimbă pe branch-ul copilot/link-code-to-interface
git checkout copilot/link-code-to-interface

# Verifică că ești pe branch-ul corect
git branch
```

**Output așteptat:**
```
* copilot/link-code-to-interface
```

---

## 📂 Navigare la Folderul Corect

Aplicația funcțională se află în **`InchirieriMasini/InchirieriMasini/`**

```bash
# Din root-ul repository-ului
cd InchirieriMasini/InchirieriMasini
```

**Structura directoare:**
```
proiect-uptispoo/
└── InchirieriMasini/
    └── InchirieriMasini/          ← AICI este aplicația
        ├── Common/
        ├── Data/
        ├── Models/
        ├── Services/
        ├── MainForm.cs
        ├── MainForm.Designer.cs
        ├── Program.cs
        ├── InchirieriMasini.csproj
        └── README_MODIFICARI.md
```

---

## 🔨 Build și Rulare Aplicație

### Metoda 1: JetBrains Rider (Recomandat)

#### Pasul 1: Deschide Proiectul în Rider

**Opțiunea A - Din Rider:**
1. Deschide JetBrains Rider
2. Click pe `Open`
3. Navighează la `proiect-uptispoo/InchirieriMasini/InchirieriMasini/`
4. Selectează fișierul `InchirieriMasini.csproj`
5. Click `OK`

**Opțiunea B - Din Explorer:**
1. Navighează în Windows Explorer la `InchirieriMasini/InchirieriMasini/`
2. Click dreapta pe `InchirieriMasini.csproj`
3. Selectează `Open with JetBrains Rider`

**Opțiunea C - Din Command Line:**
```bash
# Din InchirieriMasini/InchirieriMasini/
rider InchirieriMasini.csproj
```

#### Pasul 2: Așteaptă Indexarea

**Prima dată când deschizi proiectul:**
- Rider va indexa fișierele (vezi progress bar jos)
- Va restaura pachetele NuGet automat
- Durează ~10-30 secunde

#### Pasul 3: Build în Rider

**Opțiuni pentru Build:**

**A. Folosind meniul:**
- `Build` → `Build Solution` (SAU apasă `Ctrl + Shift + B`)

**B. Folosind shortcut:**
- Apasă `Ctrl + Shift + B`

**C. Folosind toolbar:**
- Click pe iconița 🔨 (Build) din toolbar

**Output așteptat în Build Tool Window:**
```
Build succeeded.
    0 Error(s)
    1 Warning(s)
```

#### Pasul 4: Rulare în Rider

**Opțiuni pentru Run:**

**A. Run normal (fără debugging):**
- Click pe ▶️ (butonul verde Play) din toolbar
- SAU apasă `Ctrl + F5`

**B. Run cu debugging:**
- Click pe 🐞 (butonul Debug) din toolbar
- SAU apasă `Shift + F9`

**C. Din meniu:**
- `Run` → `Run 'InchirieriMasini'` (Ctrl + F5)
- SAU `Run` → `Debug 'InchirieriMasini'` (Shift + F9)

#### Pasul 5: Configurare Run (dacă e necesar)

Dacă nu vezi configurația de run:
1. Click pe dropdown-ul de configurații (lângă butoanele Run/Debug)
2. Selectează `Edit Configurations...`
3. Click pe `+` → `.NET Project`
4. Setează:
   - **Name:** InchirieriMasini
   - **Project:** InchirieriMasini
   - **Exe path:** (se completează automat)
5. Click `OK`

### Metoda 2: Command Line (Rapid)

```bash
# Asigură-te că ești în InchirieriMasini/InchirieriMasini/
cd InchirieriMasini/InchirieriMasini

# Build
dotnet build

# Rulare
dotnet run
```

**Output așteptat pentru build:**
```
Build succeeded.
    0 Error(s)
```

### Metoda 3: Visual Studio (Alternativ)

1. **Deschide Solution:**
   ```bash
   # Din InchirieriMasini/InchirieriMasini/
   start InchirieriMasini.csproj
   ```
   SAU navighează manual și deschide `InchirieriMasini.csproj` cu Visual Studio

2. **Build în Visual Studio:**
   - Apasă `Ctrl + Shift + B`
   - SAU meniu: `Build` → `Build Solution`

3. **Rulare:**
   - Apasă `F5` (cu debugging)
   - SAU `Ctrl + F5` (fără debugging)

### Metoda 4: Explorer

```bash
# Build mai întâi
dotnet build

# Navigare la executabil
cd bin/Debug/net9.0-windows/

# Rulare
InchirieriMasini.exe
```

---

## ✅ Testare Funcționalități

### Interfața Aplicației

Aplicația se deschide cu **3 tab-uri**:
1. **Mașini** 🚗
2. **Clienți** 👥
3. **Închirieri** 📋

### Test Complet - Pas cu Pas

#### 🚗 Tab 1: Mașini

**1.1. Adaugă o mașină:**
```
Câmpuri:
- Brand: BMW
- Model: X5
- An: 2022
- Preț/zi: 150

➡️ Click "Adaugă"
✅ Mesaj: "Masina adaugata cu succes! ID: 10001"
```

**1.2. Adaugă mai multe mașini:**
```
Mașină 2:
- Brand: Audi
- Model: A4
- An: 2021
- Preț/zi: 120

Mașină 3:
- Brand: Mercedes
- Model: C-Class
- An: 2023
- Preț/zi: 180

➡️ Click "Adaugă" pentru fiecare
```

**1.3. Testează filtrarea:**
```
➡️ Click "Afișează toate" - Vezi toate cele 3 mașini
➡️ Click "Afișează mașini disponibile" - Vezi toate (niciuna nu e închiriată încă)
```

**1.4. Testează căutarea:**
```
- Introdu ID: 10001
➡️ Click "Caută"
✅ Vezi doar BMW X5
```

#### 👥 Tab 2: Clienți

**2.1. Adaugă clienți:**
```
Client 1:
- Nume: Popescu
- Prenume: Ion
- Email: ion.popescu@email.com

➡️ Click "Adaugă Client"
✅ Mesaj: "Client adaugat cu succes! ID: 2000"

Client 2:
- Nume: Ionescu
- Prenume: Maria
- Email: maria.ionescu@email.com

➡️ Click "Adaugă Client"
✅ Mesaj: "Client adaugat cu succes! ID: 2001"
```

**2.2. Testează email duplicat:**
```
Client 3:
- Nume: Test
- Prenume: Duplicat
- Email: ion.popescu@email.com (același ca Client 1)

➡️ Click "Adaugă Client"
❌ Mesaj: "Exista deja un client cu acest email"
```

**2.3. Testează căutare după ID:**
```
- Introdu ID: 2000
➡️ Click "Caută" (din grupul "Cauta Client dupa ID")
✅ Vezi doar Ion Popescu
```

**2.4. Testează căutare după Email:**
```
- Introdu Email: maria.ionescu@email.com
➡️ Click "Caută" (din grupul "Cauta Client Dupa Email")
✅ Vezi doar Maria Ionescu
```

#### 📋 Tab 3: Închirieri

**3.1. Crează o închiriere:**
```
Închiriere 1:
- ID Mașină: 10001 (BMW X5)
- ID Client: 2000 (Ion Popescu)
- Data start: astăzi (selectează din calendar)
- Număr zile: 7

➡️ Click "Creează"
✅ Mesaj: "Inchiriere creata cu succes! ID: 3001, Pret total: 1050 RON"
   (7 zile × 150 RON/zi = 1050 RON)
```

**3.2. Verifică sincronizare disponibilitate:**
```
➡️ Mergi la Tab "Mașini"
➡️ Click "Afișează mașini disponibile"
✅ BMW X5 (ID: 10001) NU mai apare (este închiriată)
```

**3.3. Crează a doua închiriere:**
```
Închiriere 2:
- ID Mașină: 10002 (Audi A4)
- ID Client: 2001 (Maria Ionescu)
- Data start: astăzi
- Număr zile: 5

➡️ Click "Creează"
✅ Mesaj: "Inchiriere creata cu succes! ID: 3002, Pret total: 600 RON"
   (5 zile × 120 RON/zi = 600 RON)
```

**3.4. Afișează închirieri active:**
```
➡️ Click "Afișează închirieri active"
✅ Vezi cele 2 închirieri (ID: 3001 și 3002)
```

**3.5. Afișează închirierile unui client:**
```
- ID Client: 2000
➡️ Click "Afișează" (din grupul "Închirieri Client")
✅ Vezi doar închirierea lui Ion Popescu (BMW X5)
```

**3.6. Calculează zile rămase:**
```
- ID Închiriere: 3001
➡️ Click "Calculează" (din grupul "Zile Rămase Închiriere")
✅ Mesaj: "Zile ramase pentru inchirierea 3001: 7 zile"
```

**3.7. Returnează o mașină:**
```
- ID Închiriere: 3001
➡️ Click "Returnare"
✅ Mesaj: "Masina returnata cu succes pentru inchirierea 3001"
```

**3.8. Verifică că mașina e disponibilă din nou:**
```
➡️ Mergi la Tab "Mașini"
➡️ Click "Afișează mașini disponibile"
✅ BMW X5 (ID: 10001) APARE din nou (este disponibilă)
```

### 💾 Test Persistență Date

**4.1. Verifică salvare automată:**
```
➡️ Închide aplicația (X din colțul dreapta-sus)
➡️ Navighează în folderul InchirieriMasini/InchirieriMasini/bin/Debug/net9.0-windows/
✅ Vezi fișierul "data.json"
```

**4.2. Verifică încărcare automată:**
```
➡️ Redeschide aplicația (Run din Rider sau dotnet run)
➡️ Mergi prin toate tab-urile
✅ Toate datele (mașini, clienți, închirieri) sunt încărcate automat!
```

**4.3. Verifică conținut JSON:**
```bash
# Vizualizează conținutul (opțional)
cat bin/Debug/net9.0-windows/data.json
```

**Structură așteptată:**
```json
{
  "Cars": [
    {
      "Id": 10001,
      "Brand": "BMW",
      "Model": "X5",
      "Year": 2022,
      "PricePerDay": 150.0,
      "IsAvailable": true
    }
  ],
  "Clients": [...],
  "Rentals": [...],
  "NextCarId": 10004,
  "NextClientId": 2002,
  "NextRentalId": 3003
}
```

---

## ❗ Rezolvare Probleme

### Problema 1: "dotnet: command not found"

**Cauză:** .NET SDK nu este instalat sau nu este în PATH

**Soluție:**
1. Descarcă și instalează .NET SDK de la: https://dotnet.microsoft.com/download
2. Restart Rider/terminal/command prompt
3. Verifică: `dotnet --version`

### Problema 2: "git: command not found"

**Cauză:** Git nu este instalat

**Soluție:**
1. Descarcă și instalează Git de la: https://git-scm.com/download
2. Restart terminal
3. Verifică: `git --version`

### Problema 3: "Build FAILED - NETSDK1100"

**Cauză:** Încercare build pe Linux/Mac (aplicație Windows Forms)

**Soluție:**
- Aplicația este **Windows Forms** - rulează doar pe **Windows**
- Pe Linux/Mac: folosește Windows VM sau WSL cu X server

### Problema 4: "Branch 'copilot/link-code-to-interface' not found"

**Soluție:**
```bash
# Verifică branch-urile disponibile
git branch -a

# Dacă vezi remotes/origin/copilot/link-code-to-interface
git checkout -b copilot/link-code-to-interface origin/copilot/link-code-to-interface
```

### Problema 5: Aplicația nu pornește - eroare "Missing DLL"

**Soluție în Rider:**
1. Click dreapta pe proiect → `Clean`
2. Click dreapta pe proiect → `Rebuild`
3. Run din nou

**Soluție Command Line:**
```bash
# Clean și rebuild
dotnet clean
dotnet build
dotnet run
```

### Problema 6: Datele nu se salvează

**Verificare:**
1. Folderul `bin/Debug/net9.0-windows/` există?
2. Ai permisiuni de scriere în folder?
3. Verifică fișierul `data.json` există după închidere

**Soluție:**
```bash
# Rulează cu permisiuni de admin (Windows)
# Click dreapta pe Command Prompt → "Run as Administrator"
```

### Problema 7: Rider nu găsește .NET SDK

**Soluție:**
1. Deschide Rider
2. `File` → `Settings` → `Build, Execution, Deployment` → `.NET CLI`
3. Verifică că SDK-ul este detectat
4. Dacă nu: Click `+` și adaugă manual path-ul la SDK

**Path tipic SDK:**
```
C:\Program Files\dotnet\sdk\9.0.xxx\
```

### Problema 8: Debugging nu funcționează în Rider

**Soluție:**
1. Verifică că ai instalat **.NET Desktop Development Workload**
2. În Rider: `Run` → `Edit Configurations...`
3. Verifică că configurația este corectă
4. Asigură-te că build-ul este în modul `Debug` (nu `Release`)

---

## 🎯 Tips pentru Rider

### Shortcuts Utile:

- **Build:** `Ctrl + Shift + B`
- **Run:** `Ctrl + F5`
- **Debug:** `Shift + F9`
- **Stop:** `Ctrl + F2`
- **Rebuild:** `Ctrl + Shift + F9`

### Panouri Utile:

- **Build Tool Window:** `Alt + 0` - Vezi output-ul build-ului
- **Run Tool Window:** `Alt + 4` - Vezi output-ul aplicației
- **Solution Explorer:** `Alt + 1` - Vezi structura proiectului
- **Terminal:** `Alt + F12` - Terminal integrat

### Features Rider pentru Debugging:

1. **Breakpoints:** Click în marginea din stânga liniei de cod
2. **Watch Variables:** Click dreapta pe variabilă → `Add to Watches`
3. **Evaluate Expression:** `Alt + F8` în modul debug
4. **Step Over:** `F8`
5. **Step Into:** `F7`
6. **Continue:** `F9`

---

## 📞 Contact și Suport

Dacă întâmpini probleme care nu sunt acoperite în acest ghid:

1. **Verifică documentația detaliată:**
   - `InchirieriMasini/InchirieriMasini/README_MODIFICARI.md`

2. **Verifică build output în Rider:**
   - Deschide panoul `Build` (Alt + 0)
   - Caută detalii despre erori

3. **Verifică logs:**
   - Caută fișiere `.log` în `bin/Debug/net9.0-windows/`

---

## 📊 Checklist Verificare Finală

După ce ai rulat toate testele, verifică:

- [ ] Build-ul trece fără erori (0 Error(s)) în Rider
- [ ] Poți adăuga mașini noi
- [ ] Poți adăuga clienți noi
- [ ] Email duplicat este prevenit
- [ ] Poți crea închirieri
- [ ] Prețul total se calculează corect
- [ ] Mașina devine indisponibilă după închiriere
- [ ] Poți returna mașini
- [ ] Mașina devine disponibilă după returnare
- [ ] Datele se salvează la închidere
- [ ] Datele se încarcă la redeschidere
- [ ] Toate căutările funcționează
- [ ] Toate filtrările funcționează

---

## 🎉 Succes!

Dacă ai parcurs toate pașii și toate testele au trecut, **aplicația funcționează perfect**! 🚀

### Pași Rapidi Recap pentru Rider:
```bash
# 1. Clonare
git clone https://github.com/cristeadevarat/proiect-uptispoo.git
cd proiect-uptispoo

# 2. Branch corect
git checkout copilot/link-code-to-interface

# 3. Navigare
cd InchirieriMasini/InchirieriMasini

# 4. Deschide în Rider
rider InchirieriMasini.csproj

# 5. În Rider: Build (Ctrl+Shift+B) apoi Run (Ctrl+F5)
```

**Enjoy! 🚗💨**


---

## 📥 Clonare Repository

### Pasul 1: Deschide Terminal/Command Prompt

**Windows:**
- Apasă `Win + R`
- Tastează `cmd` și apasă Enter
- SAU deschide `Git Bash` (dacă ai instalat Git)

### Pasul 2: Navighează la Locația Dorită

```bash
# Exemplu: pe Desktop
cd Desktop

# SAU pe C:\Projects
cd C:\Projects
```

### Pasul 3: Clonează Repository-ul

```bash
git clone https://github.com/cristeadevarat/proiect-uptispoo.git
```

**Așteptare:** Descărcarea poate dura 10-30 secunde în funcție de conexiune.

### Pasul 4: Intră în Folderul Clonat

```bash
cd proiect-uptispoo
```

### Pasul 5: Schimbă pe Branch-ul Corect

```bash
# Schimbă pe branch-ul copilot/link-code-to-interface
git checkout copilot/link-code-to-interface

# Verifică că ești pe branch-ul corect
git branch
```

**Output așteptat:**
```
* copilot/link-code-to-interface
```

---

## 📂 Navigare la Folderul Corect

Aplicația funcțională se află în **`InchirieriMasini/InchirieriMasini/`**

```bash
# Din root-ul repository-ului
cd InchirieriMasini/InchirieriMasini
```

**Structura directoare:**
```
proiect-uptispoo/
└── InchirieriMasini/
    └── InchirieriMasini/          ← AICI este aplicația
        ├── Common/
        ├── Data/
        ├── Models/
        ├── Services/
        ├── MainForm.cs
        ├── MainForm.Designer.cs
        ├── Program.cs
        ├── InchirieriMasini.csproj
        └── README_MODIFICARI.md
```

---

## 🔨 Build și Rulare Aplicație

### Metoda 1: Command Line (Rapid)

```bash
# Asigură-te că ești în InchirieriMasini/InchirieriMasini/
cd InchirieriMasini/InchirieriMasini

# Build
dotnet build

# Rulare
dotnet run
```

**Output așteptat pentru build:**
```
Build succeeded.
    0 Error(s)
```

### Metoda 2: Visual Studio (Recomandat pentru debugging)

1. **Deschide Solution:**
   ```bash
   # Din InchirieriMasini/InchirieriMasini/
   start InchirieriMasini.csproj
   ```
   SAU navighează manual și deschide `InchirieriMasini.csproj` cu Visual Studio

2. **Build în Visual Studio:**
   - Apasă `Ctrl + Shift + B`
   - SAU meniu: `Build` → `Build Solution`

3. **Rulare:**
   - Apasă `F5` (cu debugging)
   - SAU `Ctrl + F5` (fără debugging)

### Metoda 3: Explorer

```bash
# Build mai întâi
dotnet build

# Navigare la executabil
cd bin/Debug/net9.0-windows/

# Rulare
InchirieriMasini.exe
```

---

## ✅ Testare Funcționalități

### Interfața Aplicației

Aplicația se deschide cu **3 tab-uri**:
1. **Mașini** 🚗
2. **Clienți** 👥
3. **Închirieri** 📋

### Test Complet - Pas cu Pas

#### 🚗 Tab 1: Mașini

**1.1. Adaugă o mașină:**
```
Câmpuri:
- Brand: BMW
- Model: X5
- An: 2022
- Preț/zi: 150

➡️ Click "Adaugă"
✅ Mesaj: "Masina adaugata cu succes! ID: 10001"
```

**1.2. Adaugă mai multe mașini:**
```
Mașină 2:
- Brand: Audi
- Model: A4
- An: 2021
- Preț/zi: 120

Mașină 3:
- Brand: Mercedes
- Model: C-Class
- An: 2023
- Preț/zi: 180

➡️ Click "Adaugă" pentru fiecare
```

**1.3. Testează filtrarea:**
```
➡️ Click "Afișează toate" - Vezi toate cele 3 mașini
➡️ Click "Afișează mașini disponibile" - Vezi toate (niciuna nu e închiriată încă)
```

**1.4. Testează căutarea:**
```
- Introdu ID: 10001
➡️ Click "Caută"
✅ Vezi doar BMW X5
```

#### 👥 Tab 2: Clienți

**2.1. Adaugă clienți:**
```
Client 1:
- Nume: Popescu
- Prenume: Ion
- Email: ion.popescu@email.com

➡️ Click "Adaugă Client"
✅ Mesaj: "Client adaugat cu succes! ID: 2000"

Client 2:
- Nume: Ionescu
- Prenume: Maria
- Email: maria.ionescu@email.com

➡️ Click "Adaugă Client"
✅ Mesaj: "Client adaugat cu succes! ID: 2001"
```

**2.2. Testează email duplicat:**
```
Client 3:
- Nume: Test
- Prenume: Duplicat
- Email: ion.popescu@email.com (același ca Client 1)

➡️ Click "Adaugă Client"
❌ Mesaj: "Exista deja un client cu acest email"
```

**2.3. Testează căutare după ID:**
```
- Introdu ID: 2000
➡️ Click "Caută" (din grupul "Cauta Client dupa ID")
✅ Vezi doar Ion Popescu
```

**2.4. Testează căutare după Email:**
```
- Introdu Email: maria.ionescu@email.com
➡️ Click "Caută" (din grupul "Cauta Client Dupa Email")
✅ Vezi doar Maria Ionescu
```

#### 📋 Tab 3: Închirieri

**3.1. Crează o închiriere:**
```
Închiriere 1:
- ID Mașină: 10001 (BMW X5)
- ID Client: 2000 (Ion Popescu)
- Data start: astăzi (selectează din calendar)
- Număr zile: 7

➡️ Click "Creează"
✅ Mesaj: "Inchiriere creata cu succes! ID: 3001, Pret total: 1050 RON"
   (7 zile × 150 RON/zi = 1050 RON)
```

**3.2. Verifică sincronizare disponibilitate:**
```
➡️ Mergi la Tab "Mașini"
➡️ Click "Afișează mașini disponibile"
✅ BMW X5 (ID: 10001) NU mai apare (este închiriată)
```

**3.3. Crează a doua închiriere:**
```
Închiriere 2:
- ID Mașină: 10002 (Audi A4)
- ID Client: 2001 (Maria Ionescu)
- Data start: astăzi
- Număr zile: 5

➡️ Click "Creează"
✅ Mesaj: "Inchiriere creata cu succes! ID: 3002, Pret total: 600 RON"
   (5 zile × 120 RON/zi = 600 RON)
```

**3.4. Afișează închirieri active:**
```
➡️ Click "Afișează închirieri active"
✅ Vezi cele 2 închirieri (ID: 3001 și 3002)
```

**3.5. Afișează închirierile unui client:**
```
- ID Client: 2000
➡️ Click "Afișează" (din grupul "Închirieri Client")
✅ Vezi doar închirierea lui Ion Popescu (BMW X5)
```

**3.6. Calculează zile rămase:**
```
- ID Închiriere: 3001
➡️ Click "Calculează" (din grupul "Zile Rămase Închiriere")
✅ Mesaj: "Zile ramase pentru inchirierea 3001: 7 zile"
```

**3.7. Returnează o mașină:**
```
- ID Închiriere: 3001
➡️ Click "Returnare"
✅ Mesaj: "Masina returnata cu succes pentru inchirierea 3001"
```

**3.8. Verifică că mașina e disponibilă din nou:**
```
➡️ Mergi la Tab "Mașini"
➡️ Click "Afișează mașini disponibile"
✅ BMW X5 (ID: 10001) APARE din nou (este disponibilă)
```

### 💾 Test Persistență Date

**4.1. Verifică salvare automată:**
```
➡️ Închide aplicația (X din colțul dreapta-sus)
➡️ Navighează în folderul InchirieriMasini/InchirieriMasini/bin/Debug/net9.0-windows/
✅ Vezi fișierul "data.json"
```

**4.2. Verifică încărcare automată:**
```
➡️ Redeschide aplicația (dotnet run)
➡️ Mergi prin toate tab-urile
✅ Toate datele (mașini, clienți, închirieri) sunt încărcate automat!
```

**4.3. Verifică conținut JSON:**
```bash
# Vizualizează conținutul (opțional)
cat bin/Debug/net9.0-windows/data.json
```

**Structură așteptată:**
```json
{
  "Cars": [
    {
      "Id": 10001,
      "Brand": "BMW",
      "Model": "X5",
      "Year": 2022,
      "PricePerDay": 150.0,
      "IsAvailable": true
    }
  ],
  "Clients": [...],
  "Rentals": [...],
  "NextCarId": 10004,
  "NextClientId": 2002,
  "NextRentalId": 3003
}
```

---

## ❗ Rezolvare Probleme

### Problema 1: "dotnet: command not found"

**Cauză:** .NET SDK nu este instalat sau nu este în PATH

**Soluție:**
1. Descarcă și instalează .NET SDK de la: https://dotnet.microsoft.com/download
2. Restart terminal/command prompt
3. Verifică: `dotnet --version`

### Problema 2: "git: command not found"

**Cauză:** Git nu este instalat

**Soluție:**
1. Descarcă și instalează Git de la: https://git-scm.com/download
2. Restart terminal
3. Verifică: `git --version`

### Problema 3: "Build FAILED - NETSDK1100"

**Cauză:** Încercare build pe Linux/Mac (aplicație Windows Forms)

**Soluție:**
- Aplicația este **Windows Forms** - rulează doar pe **Windows**
- Pe Linux/Mac: folosește Windows VM sau WSL cu X server

### Problema 4: "Branch 'copilot/link-code-to-interface' not found"

**Soluție:**
```bash
# Verifică branch-urile disponibile
git branch -a

# Dacă vezi remotes/origin/copilot/link-code-to-interface
git checkout -b copilot/link-code-to-interface origin/copilot/link-code-to-interface
```

### Problema 5: Aplicația nu pornește - eroare "Missing DLL"

**Soluție:**
```bash
# Clean și rebuild
dotnet clean
dotnet build
dotnet run
```

### Problema 6: Datele nu se salvează

**Verificare:**
1. Folderul `bin/Debug/net9.0-windows/` există?
2. Ai permisiuni de scriere în folder?
3. Verifică fișierul `data.json` există după închidere

**Soluție:**
```bash
# Rulează cu permisiuni de admin (Windows)
# Click dreapta pe Command Prompt → "Run as Administrator"
```

---

## 📞 Contact și Suport

Dacă întâmpini probleme care nu sunt acoperite în acest ghid:

1. **Verifică documentația detaliată:**
   - `InchirieriMasini/InchirieriMasini/README_MODIFICARI.md`

2. **Verifică build output:**
   ```bash
   dotnet build --verbosity detailed
   ```

3. **Verifică logs:**
   - Caută fișiere `.log` în `bin/Debug/net9.0-windows/`

---

## 📊 Checklist Verificare Finală

După ce ai rulat toate testele, verifică:

- [ ] Build-ul trece fără erori (0 Error(s))
- [ ] Poți adăuga mașini noi
- [ ] Poți adăuga clienți noi
- [ ] Email duplicat este prevenit
- [ ] Poți crea închirieri
- [ ] Prețul total se calculează corect
- [ ] Mașina devine indisponibilă după închiriere
- [ ] Poți returna mașini
- [ ] Mașina devine disponibilă după returnare
- [ ] Datele se salvează la închidere
- [ ] Datele se încarcă la redeschidere
- [ ] Toate căutările funcționează
- [ ] Toate filtrările funcționează

---

## 🎉 Succes!

Dacă ai parcurs toate pașii și toate testele au trecut, **aplicația funcționează perfect**! 🚀

### Pași Rapidi Recap:
```bash
# 1. Clonare
git clone https://github.com/cristeadevarat/proiect-uptispoo.git
cd proiect-uptispoo

# 2. Branch corect
git checkout copilot/link-code-to-interface

# 3. Navigare
cd InchirieriMasini/InchirieriMasini

# 4. Build & Run
dotnet build
dotnet run
```

**Enjoy! 🚗💨**
