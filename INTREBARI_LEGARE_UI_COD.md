# Întrebări și Răspunsuri - Legarea UI cu Codul (Code-Behind)

## 📚 Ghid Complet pentru Integrarea UI-Backend în Windows Forms

Acest document conține întrebări și răspunsuri detaliate despre cum am conectat interfața grafică (UI) cu logica aplicației (backend) în proiectul de închirieri mașini.

---

## PARTEA 1: ÎNȚELEGEREA STRUCTURII

### Q1: Ce înseamnă "legarea UI-ului cu codul" și de ce e importantă?

**Răspuns Complet:**

"Legarea UI-ului cu codul" înseamnă **conectarea elementelor vizuale (butoane, text box-uri, tabele) cu funcționalitatea efectivă a aplicației**.

**Exemplu concret din proiectul nostru:**
```
UI (ce vede utilizatorul)          →          Cod (ce se întâmplă în spate)
═══════════════════════════════════════════════════════════════════════════

[Button "Adaugă Mașină"]           →          btnAdaugaMasina_Click()
[TextBox pentru Brand]             →          txtBrand.Text (citire valoare)
[DataGridView pentru mașini]       →          dgvMasini.DataSource = listaMasini
```

**De ce e importantă?**
- ❌ **Fără legare**: Butonul nu face nimic când îl apeși
- ✅ **Cu legare**: Click pe buton → executare cod → salvare date → actualizare UI

---

### Q2: Care sunt cele două fișiere principale și ce rol are fiecare?

**Răspuns:**

În Windows Forms, avem **separarea în două fișiere folosind partial classes**:

#### **1. Form1.Designer.cs** - PARTEA DE UI
```csharp
partial class Form1
{
    // AICI: Definirea controalelor UI
    private Button btnAdaugaMasina;
    private TextBox txtBrand;
    private DataGridView dgvMasini;
    
    private void InitializeComponent()
    {
        // AICI: Crearea și poziționarea controalelor
        this.btnAdaugaMasina = new Button();
        this.btnAdaugaMasina.Text = "Adaugă";
        this.btnAdaugaMasina.Location = new Point(15, 145);
        
        this.txtBrand = new TextBox();
        this.txtBrand.PlaceholderText = "Brand";
        
        // ... etc
    }
}
```

**Rol**: Descrie CUM arată UI-ul (ce butoane există, unde sunt poziționate, ce text au).

#### **2. Form1.cs** - PARTEA DE LOGICĂ (Code-Behind)
```csharp
partial class Form1 : Form
{
    public Form1()
    {
        InitializeComponent(); // Construiește UI-ul
        
        // AICI: Legarea evenimentelor
        btnAdaugaMasina.Click += btnAdaugaMasina_Click;
        btnAfiseazaToate.Click += btnAfiseazaToate_Click;
        // ... etc
    }
    
    // AICI: Implementarea funcționalităților
    private void btnAdaugaMasina_Click(object sender, EventArgs e)
    {
        // Logica de adăugare mașină
    }
}
```

**Rol**: Descrie CE face aplicația (ce se întâmplă când apeși butoane).

**De ce două fișiere?**
- Visual Studio Designer generează automat `Form1.Designer.cs`
- Dacă am avea tot într-un fișier, Designer-ul ar suprascrie codul nostru
- Cu `partial class`, putem avea ambele fișiere care formează o singură clasă

---

### Q3: Ce este un "event handler" și cum funcționează?

**Răspuns Detaliat:**

Un **event handler** este o metodă care se execută automat când se întâmplă ceva (un "eveniment") în UI.

**Anatomia unui Event Handler:**

```csharp
// 1. DEFINIȚIA METODEI (în Form1.cs)
private void btnAdaugaMasina_Click(object sender, EventArgs e)
//       ↑ Nume descriptiv         ↑ Parametri standard
{
    // Cod care se execută la click
}

// 2. ÎNREGISTRAREA (conectarea) - în constructor sau InitializeComponent
btnAdaugaMasina.Click += btnAdaugaMasina_Click;
//     ↑ Controlul      ↑ Evenimentul  ↑ Handler-ul
```

**Flow complet:**
```
1. Utilizator → Click pe buton
                    ↓
2. Windows Forms → Detectează evenimentul
                    ↓
3. .NET Framework → Caută handler-ul înregistrat
                    ↓
4. btnAdaugaMasina_Click() → Se execută
                    ↓
5. Cod tău → Adaugă mașină, actualizează grid, etc.
```

**Tipuri de evenimente comune:**
```csharp
// Click events
btnSave.Click += btnSave_Click;

// Text change events
txtSearch.TextChanged += txtSearch_TextChanged;

// Selection changed
dgvMasini.SelectionChanged += dgvMasini_SelectionChanged;

// Form load
this.Load += Form1_Load;
```

---

## PARTEA 2: CITIREA DATELOR DIN UI

### Q4: Cum citești valorile introduse de utilizator în controalele UI?

**Răspuns cu Exemple:**

Fiecare tip de control are proprietăți specifice pentru a accesa datele:

#### **TextBox - Text simplu**
```csharp
private void btnAdaugaMasina_Click(object sender, EventArgs e)
{
    // Citire valori din TextBox
    string brand = txtBrand.Text;
    string model = txtModel.Text;
    string email = txtEmail.Text;
    
    // Validare de bază
    if (string.IsNullOrWhiteSpace(brand))
    {
        MessageBox.Show("Brand-ul este obligatoriu!");
        return;
    }
}
```

#### **NumericUpDown - Valori numerice**
```csharp
private void btnAdaugaMasina_Click(object sender, EventArgs e)
{
    // .Value returnează decimal
    int year = (int)numYear.Value;
    decimal pricePerDay = numPrice.Value;
    int carId = (int)numSearchId.Value;
    
    // NumericUpDown are deja validare built-in
    // (nu poate fi mai mic decât Minimum sau mai mare decât Maximum)
}
```

#### **DateTimePicker - Date**
```csharp
private void btnCreeazaInchiriere_Click(object sender, EventArgs e)
{
    // .Value returnează DateTime
    DateTime startDate = dtpStartDate.Value;
    
    // Poți calcula date viitoare
    int days = (int)numDays.Value;
    DateTime endDate = startDate.AddDays(days);
    
    Console.WriteLine($"Închiriere: {startDate:dd/MM/yyyy} - {endDate:dd/MM/yyyy}");
}
```

#### **ComboBox - Selecție dintr-o listă** (dacă am adăuga)
```csharp
private void btnFilter_Click(object sender, EventArgs e)
{
    // Item-ul selectat
    string selectedBrand = cmbBrand.SelectedItem.ToString();
    
    // Sau valoarea asociată
    int selectedId = (int)cmbBrand.SelectedValue;
}
```

---

### Q5: Cum validezi datele înainte de a le folosi?

**Răspuns cu Strategii:**

**Validare în cascadă - verificări în ordine logică:**

```csharp
private void btnAdaugaMasina_Click(object sender, EventArgs e)
{
    // NIVEL 1: Verificare câmpuri goale
    if (string.IsNullOrWhiteSpace(txtBrand.Text))
    {
        MessageBox.Show("Brand-ul este obligatoriu!", 
                       "Eroare Validare", 
                       MessageBoxButtons.OK, 
                       MessageBoxIcon.Warning);
        txtBrand.Focus(); // Mută cursorul la câmpul greșit
        return;
    }
    
    if (string.IsNullOrWhiteSpace(txtModel.Text))
    {
        MessageBox.Show("Modelul este obligatoriu!", 
                       "Eroare Validare", 
                       MessageBoxButtons.OK, 
                       MessageBoxIcon.Warning);
        txtModel.Focus();
        return;
    }
    
    // NIVEL 2: Verificare format
    if (!txtEmail.Text.Contains("@"))
    {
        MessageBox.Show("Email-ul trebuie să conțină @!", 
                       "Eroare Validare", 
                       MessageBoxButtons.OK, 
                       MessageBoxIcon.Error);
        return;
    }
    
    // NIVEL 3: Verificare valori logice
    if (numPrice.Value <= 0)
    {
        MessageBox.Show("Prețul trebuie să fie mai mare decât 0!", 
                       "Eroare Validare", 
                       MessageBoxButtons.OK, 
                       MessageBoxIcon.Error);
        return;
    }
    
    // NIVEL 4: Verificare business rules
    if (!IsMasinaDisponibila(carId))
    {
        MessageBox.Show("Mașina nu este disponibilă pentru închiriere!", 
                       "Eroare Business", 
                       MessageBoxButtons.OK, 
                       MessageBoxIcon.Information);
        return;
    }
    
    // Toate validările au trecut → procedează
    AdaugaMasina();
}
```

**Metodă de validare reutilizabilă:**
```csharp
private bool ValidateCarInput(out string errorMessage)
{
    errorMessage = "";
    
    if (string.IsNullOrWhiteSpace(txtBrand.Text))
    {
        errorMessage = "Brand-ul este obligatoriu!";
        return false;
    }
    
    if (string.IsNullOrWhiteSpace(txtModel.Text))
    {
        errorMessage = "Modelul este obligatoriu!";
        return false;
    }
    
    if (numPrice.Value <= 0)
    {
        errorMessage = "Prețul trebuie să fie pozitiv!";
        return false;
    }
    
    return true;
}

// Folosire:
private void btnAdaugaMasina_Click(object sender, EventArgs e)
{
    if (!ValidateCarInput(out string error))
    {
        MessageBox.Show(error, "Eroare Validare");
        return;
    }
    
    // Continuă cu salvarea
}
```

---

## PARTEA 3: AFIȘAREA DATELOR ÎN UI

### Q6: Cum afișezi o listă de obiecte într-un DataGridView?

**Răspuns Complet:**

**DataGridView** este controlul principal pentru afișarea datelor tabulare. Există 3 metode de populare:

#### **Metoda 1: DataSource cu List<T> (Recomandat)**
```csharp
// Clasa Car (modelul de date)
public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal DailyPrice { get; set; }
    public bool IsAvailable { get; set; }
}

// În Form1.cs
private List<Car> cars = new List<Car>();

private void btnAfiseazaToate_Click(object sender, EventArgs e)
{
    // Populare listă (simulare - în realitate din DB)
    cars = new List<Car>
    {
        new Car { Id = 1, Brand = "Dacia", Model = "Logan", Year = 2020, DailyPrice = 120, IsAvailable = true },
        new Car { Id = 2, Brand = "BMW", Model = "X5", Year = 2022, DailyPrice = 350, IsAvailable = false },
        new Car { Id = 3, Brand = "Mercedes", Model = "C-Class", Year = 2021, DailyPrice = 280, IsAvailable = true }
    };
    
    // Setare DataSource - DataGridView se populează automat!
    dgvMasini.DataSource = null; // Reset
    dgvMasini.DataSource = cars;
    
    // Personalizare coloane (opțional)
    dgvMasini.Columns["Id"].HeaderText = "ID";
    dgvMasini.Columns["Brand"].HeaderText = "Marcă";
    dgvMasini.Columns["Model"].HeaderText = "Model";
    dgvMasini.Columns["Year"].HeaderText = "An";
    dgvMasini.Columns["DailyPrice"].HeaderText = "Preț/zi (RON)";
    dgvMasini.Columns["IsAvailable"].HeaderText = "Disponibil";
    
    // Formatare preț
    dgvMasini.Columns["DailyPrice"].DefaultCellStyle.Format = "C2"; // Currency
    
    lblStatus.Text = $"Afișate {cars.Count} mașini";
}
```

#### **Metoda 2: Adăugare manuală rânduri**
```csharp
private void btnAfiseazaToate_Click(object sender, EventArgs e)
{
    // Configurare coloane (o singură dată)
    dgvMasini.Columns.Clear();
    dgvMasini.Columns.Add("Id", "ID");
    dgvMasini.Columns.Add("Brand", "Marcă");
    dgvMasini.Columns.Add("Model", "Model");
    dgvMasini.Columns.Add("Year", "An");
    dgvMasini.Columns.Add("DailyPrice", "Preț/zi");
    
    // Adăugare rânduri
    dgvMasini.Rows.Clear();
    dgvMasini.Rows.Add(1, "Dacia", "Logan", 2020, 120);
    dgvMasini.Rows.Add(2, "BMW", "X5", 2022, 350);
    dgvMasini.Rows.Add(3, "Mercedes", "C-Class", 2021, 280);
}
```

#### **Metoda 3: BindingSource (pentru operațiuni complexe)**
```csharp
private BindingSource bindingSource = new BindingSource();

public Form1()
{
    InitializeComponent();
    
    bindingSource.DataSource = cars;
    dgvMasini.DataSource = bindingSource;
}

private void btnFiltrare_Click(object sender, EventArgs e)
{
    // Filtrare dinamică
    bindingSource.Filter = "IsAvailable = true";
}

private void btnSortare_Click(object sender, EventArgs e)
{
    // Sortare
    bindingSource.Sort = "DailyPrice ASC";
}
```

---

### Q7: Cum actualizezi UI-ul după o operațiune (adăugare, ștergere)?

**Răspuns cu Pattern:**

**Pattern-ul standard: Operațiune → Refresh UI → Feedback**

```csharp
private void btnAdaugaMasina_Click(object sender, EventArgs e)
{
    try
    {
        // PASUL 1: VALIDARE
        if (!ValidateCarInput(out string error))
        {
            MessageBox.Show(error, "Eroare");
            return;
        }
        
        // PASUL 2: CREARE OBIECT
        var newCar = new Car
        {
            Id = cars.Count + 1, // Sau generat de DB
            Brand = txtBrand.Text.Trim(),
            Model = txtModel.Text.Trim(),
            Year = (int)numYear.Value,
            DailyPrice = numPrice.Value,
            IsAvailable = true
        };
        
        // PASUL 3: SALVARE (în listă/DB)
        cars.Add(newCar);
        // Sau: carRepository.Add(newCar);
        
        // PASUL 4: REFRESH UI
        RefreshCarsGrid();
        
        // PASUL 5: CURĂȚARE FORMULAR
        ClearCarForm();
        
        // PASUL 6: FEEDBACK POZITIV
        lblStatus.Text = $"✓ Mașina {newCar.Brand} {newCar.Model} a fost adăugată!";
        lblStatus.ForeColor = Color.Green;
        
        // PASUL 7: FEEDBACK SONOR (opțional)
        SystemSounds.Asterisk.Play();
    }
    catch (Exception ex)
    {
        // PASUL 8: GESTIONARE ERORI
        MessageBox.Show($"Eroare la adăugare: {ex.Message}", 
                       "Eroare", 
                       MessageBoxButtons.OK, 
                       MessageBoxIcon.Error);
        lblStatus.Text = "✗ Eroare la adăugare mașină";
        lblStatus.ForeColor = Color.Red;
    }
}

// Metodă helper pentru refresh
private void RefreshCarsGrid()
{
    dgvMasini.DataSource = null;
    dgvMasini.DataSource = cars;
    
    // Re-aplicare formatare coloane
    if (dgvMasini.Columns.Count > 0)
    {
        dgvMasini.Columns["DailyPrice"].DefaultCellStyle.Format = "C2";
    }
}

// Metodă helper pentru curățare
private void ClearCarForm()
{
    txtBrand.Clear();
    txtModel.Clear();
    numYear.Value = DateTime.Now.Year;
    numPrice.Value = 0;
    txtBrand.Focus(); // Pregătește pentru următoarea introducere
}
```

---

### Q8: Cum gestionezi selecția unui rând în DataGridView pentru editare/ștergere?

**Răspuns:**

```csharp
// Configurare în constructor sau Designer
public Form1()
{
    InitializeComponent();
    
    // Setări importante pentru selecție
    dgvMasini.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
    dgvMasini.MultiSelect = false;
    dgvMasini.ReadOnly = true;
}

// METODA 1: Ștergere pe bază de selecție
private void btnStergeMasina_Click(object sender, EventArgs e)
{
    // Verificare dacă e selectat un rând
    if (dgvMasini.SelectedRows.Count == 0)
    {
        MessageBox.Show("Selectează o mașină pentru ștergere!", "Atenție");
        return;
    }
    
    // Obținere rând selectat
    DataGridViewRow selectedRow = dgvMasini.SelectedRows[0];
    
    // Extragere ID din rând
    int carId = (int)selectedRow.Cells["Id"].Value;
    
    // Confirmare
    var result = MessageBox.Show(
        $"Sigur dorești să ștergi mașina cu ID {carId}?",
        "Confirmare Ștergere",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question
    );
    
    if (result == DialogResult.Yes)
    {
        // Ștergere din listă
        var carToRemove = cars.FirstOrDefault(c => c.Id == carId);
        if (carToRemove != null)
        {
            cars.Remove(carToRemove);
            RefreshCarsGrid();
            lblStatus.Text = $"Mașina cu ID {carId} a fost ștearsă";
        }
    }
}

// METODA 2: Populare formular pentru editare
private void btnEditMasina_Click(object sender, EventArgs e)
{
    if (dgvMasini.SelectedRows.Count == 0)
    {
        MessageBox.Show("Selectează o mașină pentru editare!");
        return;
    }
    
    DataGridViewRow row = dgvMasini.SelectedRows[0];
    
    // Populare formular cu datele selectate
    numSearchId.Value = (int)row.Cells["Id"].Value;
    txtBrand.Text = row.Cells["Brand"].Value.ToString();
    txtModel.Text = row.Cells["Model"].Value.ToString();
    numYear.Value = (int)row.Cells["Year"].Value;
    numPrice.Value = (decimal)row.Cells["DailyPrice"].Value;
    
    // Focus pe primul câmp pentru editare
    txtBrand.Focus();
    txtBrand.SelectAll();
}

// METODA 3: Double-click pe rând pentru detalii
private void dgvMasini_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
{
    if (e.RowIndex < 0) return; // Click pe header
    
    int carId = (int)dgvMasini.Rows[e.RowIndex].Cells["Id"].Value;
    var car = cars.FirstOrDefault(c => c.Id == carId);
    
    if (car != null)
    {
        string details = $"Detalii Mașină:\n\n" +
                        $"ID: {car.Id}\n" +
                        $"Brand: {car.Brand}\n" +
                        $"Model: {car.Model}\n" +
                        $"An: {car.Year}\n" +
                        $"Preț/zi: {car.DailyPrice:C2}\n" +
                        $"Status: {(car.IsAvailable ? "Disponibilă" : "Închiriată")}";
        
        MessageBox.Show(details, "Detalii Mașină", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
```

---

## PARTEA 4: INTEGRAREA COMPLETĂ UI-BACKEND

### Q9: Care este flow-ul complet de la click pe buton până la actualizare UI?

**Răspuns cu Diagrama Completă:**

```
┌─────────────────────────────────────────────────────────────────────┐
│                    FLOW COMPLET: ADĂUGARE MAȘINĂ                    │
└─────────────────────────────────────────────────────────────────────┘

1. UTILIZATOR
   ↓ [Click pe buton "Adaugă Mașină"]
   
2. WINDOWS FORMS FRAMEWORK
   ↓ [Detectează eveniment Click]
   
3. EVENT HANDLER (btnAdaugaMasina_Click)
   ├─ [Citește date din UI]
   │  ├─ txtBrand.Text
   │  ├─ txtModel.Text
   │  ├─ numYear.Value
   │  └─ numPrice.Value
   │
   ├─ [Validează date]
   │  ├─ Brand nu e gol? ✓
   │  ├─ Model nu e gol? ✓
   │  └─ Preț > 0? ✓
   │
   ├─ [Creează obiect Car]
   │  └─ new Car { Brand = ..., Model = ... }
   │
   ├─ [Apelează Business Logic Layer]
   │  └─ carRepository.Add(newCar)
   │
   ├─ [Business Logic salvează]
   │  └─ cars.Add(newCar) sau INSERT INTO database
   │
   ├─ [Actualizează UI]
   │  ├─ RefreshCarsGrid()
   │  │  └─ dgvMasini.DataSource = cars
   │  ├─ ClearCarForm()
   │  └─ lblStatus.Text = "Mașină adăugată!"
   │
   └─ [Feedback utilizator]
      ├─ MessageBox.Show("Succes!")
      └─ SystemSounds.Asterisk.Play()

4. UTILIZATOR
   ↓ [Vede mașina nouă în grid]
   └─ [Formular gol, gata pentru altă introducere]
```

---

### Q10: Cum implementezi search/filtrare în timp real?

**Răspuns cu Implementare:**

```csharp
// Event handler pentru TextChanged
private void txtSearch_TextChanged(object sender, EventArgs e)
{
    string searchTerm = txtSearch.Text.ToLower();
    
    if (string.IsNullOrWhiteSpace(searchTerm))
    {
        // Afișează toate
        dgvMasini.DataSource = null;
        dgvMasini.DataSource = cars;
    }
    else
    {
        // Filtrare LINQ
        var filtered = cars.Where(c => 
            c.Brand.ToLower().Contains(searchTerm) ||
            c.Model.ToLower().Contains(searchTerm)
        ).ToList();
        
        dgvMasini.DataSource = null;
        dgvMasini.DataSource = filtered;
        
        lblStatus.Text = $"Găsite {filtered.Count} mașini";
    }
}

// Filtrare după multiple criterii
private void ApplyFilters()
{
    var filtered = cars.AsEnumerable();
    
    // Filtru brand
    if (cmbBrandFilter.SelectedIndex > 0)
    {
        string brand = cmbBrandFilter.SelectedItem.ToString();
        filtered = filtered.Where(c => c.Brand == brand);
    }
    
    // Filtru disponibilitate
    if (chkOnlyAvailable.Checked)
    {
        filtered = filtered.Where(c => c.IsAvailable);
    }
    
    // Filtru preț maxim
    if (numMaxPrice.Value > 0)
    {
        filtered = filtered.Where(c => c.DailyPrice <= numMaxPrice.Value);
    }
    
    // Afișare
    dgvMasini.DataSource = filtered.ToList();
}
```

---

## PARTEA 5: BEST PRACTICES ȘI OPTIMIZĂRI

### Q11: Care sunt best practices pentru legarea UI-backend?

**Răspuns:**

#### **1. Separarea Responsabilităților**
```csharp
// ❌ RĂU: Tot în event handler
private void btnAdaugaMasina_Click(object sender, EventArgs e)
{
    var car = new Car { Brand = txtBrand.Text };
    cars.Add(car);
    dgvMasini.DataSource = null;
    dgvMasini.DataSource = cars;
    txtBrand.Clear();
}

// ✅ BUN: Separare în metode
private void btnAdaugaMasina_Click(object sender, EventArgs e)
{
    if (!ValidateInput()) return;
    
    var car = CreateCarFromInput();
    SaveCar(car);
    RefreshUI();
    ShowSuccessMessage(car);
}
```

#### **2. Validare Consistentă**
```csharp
// ✅ Metodă centralizată
private bool ValidateInput()
{
    var errors = new List<string>();
    
    if (string.IsNullOrWhiteSpace(txtBrand.Text))
        errors.Add("Brand-ul este obligatoriu");
        
    if (numPrice.Value <= 0)
        errors.Add("Prețul trebuie să fie pozitiv");
    
    if (errors.Any())
    {
        MessageBox.Show(string.Join("\n", errors), "Erori de validare");
        return false;
    }
    
    return true;
}
```

#### **3. Try-Catch pentru Operațiuni Critice**
```csharp
private void btnAdaugaMasina_Click(object sender, EventArgs e)
{
    try
    {
        // Operațiuni
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Eroare: {ex.Message}", "Eroare");
        LogError(ex); // Logging
    }
}
```

#### **4. Feedback Constant**
```csharp
private void UpdateStatusLabel(string message, bool isSuccess)
{
    lblStatus.Text = message;
    lblStatus.ForeColor = isSuccess ? Color.Green : Color.Red;
}
```

---

### Q12: Cum gestionezi operațiuni asincrone pentru a nu bloca UI-ul?

**Răspuns:**

```csharp
// Pentru operațiuni lungi (ex: încărcare din DB)
private async void btnLoadData_Click(object sender, EventArgs e)
{
    try
    {
        // Disable butoane în timpul încărcării
        btnLoadData.Enabled = false;
        lblStatus.Text = "Se încarcă datele...";
        
        // Operațiune asincronă
        var cars = await LoadCarsFromDatabaseAsync();
        
        // Actualizare UI pe thread-ul principal
        dgvMasini.DataSource = cars;
        lblStatus.Text = $"Încărcate {cars.Count} mașini";
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Eroare: {ex.Message}");
    }
    finally
    {
        // Re-enable butoane
        btnLoadData.Enabled = true;
    }
}

private async Task<List<Car>> LoadCarsFromDatabaseAsync()
{
    // Simulare încărcare din DB
    await Task.Delay(2000); // 2 secunde
    
    // În realitate: await dbContext.Cars.ToListAsync()
    return new List<Car> { /* ... */ };
}
```

---

## PARTEA 6: DEBUGGING ȘI TROUBLESHOOTING

### Q13: Cum debuguiești problemele de legare UI-cod?

**Răspuns:**

#### **Tehnici de Debugging:**

```csharp
private void btnAdaugaMasina_Click(object sender, EventArgs e)
{
    // 1. Breakpoint aici în Visual Studio (F9)
    
    // 2. Verifică valorile
    string brand = txtBrand.Text; // Hover pentru valoare
    
    // 3. Console logging
    Console.WriteLine($"Brand: {brand}, Model: {txtModel.Text}");
    
    // 4. Debug.WriteLine (se vede în Output window)
    Debug.WriteLine($"Attempting to add car: {brand}");
    
    // 5. MessageBox pentru debug quick
    MessageBox.Show($"Brand: {brand}\nModel: {txtModel.Text}");
    
    // 6. Try-catch cu detalii
    try
    {
        // Cod
    }
    catch (Exception ex)
    {
        Debug.WriteLine($"Error: {ex}");
        MessageBox.Show($"Eroare:\n{ex.Message}\n\nStack:\n{ex.StackTrace}");
    }
}
```

#### **Probleme Comune și Soluții:**

**Problema 1: Butonul nu răspunde la click**
```csharp
// Verifică:
// 1. Event handler este înregistrat?
public Form1()
{
    InitializeComponent();
    btnAdaugaMasina.Click += btnAdaugaMasina_Click; // ✓
}

// 2. Butonul este Enabled?
btnAdaugaMasina.Enabled = true;

// 3. Butonul este vizibil?
btnAdaugaMasina.Visible = true;
```

**Problema 2: DataGridView nu se actualizează**
```csharp
// Soluție: Reset DataSource
dgvMasini.DataSource = null;
dgvMasini.DataSource = cars;

// SAU: Refresh explicit
dgvMasini.Refresh();
```

**Problema 3: NullReferenceException**
```csharp
// Verifică inițializare
if (dgvMasini == null)
{
    MessageBox.Show("DataGridView nu e inițializat!");
    return;
}

// Verifică listă
if (cars == null)
{
    cars = new List<Car>();
}
```

---

## PARTEA 7: EXEMPLE PRACTICE DIN PROIECT

### Q14: Prezintă implementarea completă pentru modulul MAȘINI

**Răspuns - Cod Complet:**

```csharp
public partial class Form1 : Form
{
    // Date în memorie (în realitate ar fi din DB)
    private List<Car> cars = new List<Car>();
    
    public Form1()
    {
        InitializeComponent();
        InitializeEvents();
        LoadInitialData();
    }
    
    // Înregistrare evenimente
    private void InitializeEvents()
    {
        btnAfiseazaToate.Click += btnAfiseazaToate_Click;
        btnDisponibile.Click += btnDisponibile_Click;
        btnAdaugaMasina.Click += btnAdaugaMasina_Click;
        btnCauta.Click += btnCauta_Click;
    }
    
    // Date inițiale (demo)
    private void LoadInitialData()
    {
        cars = new List<Car>
        {
            new Car { Id = 1, Brand = "Dacia", Model = "Logan", Year = 2020, DailyPrice = 120, IsAvailable = true },
            new Car { Id = 2, Brand = "BMW", Model = "X5", Year = 2022, DailyPrice = 350, IsAvailable = false },
            new Car { Id = 3, Brand = "Mercedes", Model = "C-Class", Year = 2021, DailyPrice = 280, IsAvailable = true },
            new Car { Id = 4, Brand = "Audi", Model = "A4", Year = 2023, DailyPrice = 300, IsAvailable = true },
            new Car { Id = 5, Brand = "Volkswagen", Model = "Golf", Year = 2019, DailyPrice = 150, IsAvailable = false }
        };
    }
    
    // EVENT HANDLERS
    
    private void btnAfiseazaToate_Click(object sender, EventArgs e)
    {
        RefreshCarsGrid(cars);
        lblStatus.Text = $"Afișate toate cele {cars.Count} mașini";
    }
    
    private void btnDisponibile_Click(object sender, EventArgs e)
    {
        var available = cars.Where(c => c.IsAvailable).ToList();
        RefreshCarsGrid(available);
        lblStatus.Text = $"Afișate {available.Count} mașini disponibile";
    }
    
    private void btnAdaugaMasina_Click(object sender, EventArgs e)
    {
        try
        {
            // Validare
            if (!ValidateCarInput(out string error))
            {
                MessageBox.Show(error, "Eroare Validare", 
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            // Creare mașină
            var newCar = new Car
            {
                Id = cars.Max(c => c.Id) + 1,
                Brand = txtBrand.Text.Trim(),
                Model = txtModel.Text.Trim(),
                Year = (int)numYear.Value,
                DailyPrice = numPrice.Value,
                IsAvailable = true
            };
            
            // Salvare
            cars.Add(newCar);
            
            // Refresh UI
            RefreshCarsGrid(cars);
            ClearCarForm();
            
            // Feedback
            lblStatus.Text = $"✓ Mașina {newCar.Brand} {newCar.Model} a fost adăugată!";
            lblStatus.ForeColor = Color.Green;
            
            MessageBox.Show($"Mașina a fost adăugată cu succes!\nID: {newCar.Id}", 
                           "Succes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            lblStatus.Text = "✗ Eroare la adăugare";
            lblStatus.ForeColor = Color.Red;
            MessageBox.Show($"Eroare: {ex.Message}", "Eroare", 
                           MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private void btnCauta_Click(object sender, EventArgs e)
    {
        int searchId = (int)numSearchId.Value;
        var car = cars.FirstOrDefault(c => c.Id == searchId);
        
        if (car != null)
        {
            RefreshCarsGrid(new List<Car> { car });
            lblStatus.Text = $"Găsită mașina: {car.Brand} {car.Model}";
        }
        else
        {
            MessageBox.Show($"Nu există mașină cu ID {searchId}", "Nu s-a găsit", 
                           MessageBoxButtons.OK, MessageBoxIcon.Information);
            lblStatus.Text = $"Nu s-a găsit mașina cu ID {searchId}";
        }
    }
    
    // HELPER METHODS
    
    private bool ValidateCarInput(out string errorMessage)
    {
        errorMessage = "";
        
        if (string.IsNullOrWhiteSpace(txtBrand.Text))
        {
            errorMessage = "Brand-ul este obligatoriu!";
            txtBrand.Focus();
            return false;
        }
        
        if (string.IsNullOrWhiteSpace(txtModel.Text))
        {
            errorMessage = "Modelul este obligatoriu!";
            txtModel.Focus();
            return false;
        }
        
        if (numYear.Value < 1990 || numYear.Value > DateTime.Now.Year + 1)
        {
            errorMessage = $"Anul trebuie să fie între 1990 și {DateTime.Now.Year + 1}!";
            return false;
        }
        
        if (numPrice.Value <= 0)
        {
            errorMessage = "Prețul trebuie să fie mai mare decât 0!";
            return false;
        }
        
        return true;
    }
    
    private void RefreshCarsGrid(List<Car> carsToDisplay)
    {
        dgvMasini.DataSource = null;
        dgvMasini.DataSource = carsToDisplay;
        
        if (dgvMasini.Columns.Count > 0)
        {
            dgvMasini.Columns["Id"].HeaderText = "ID";
            dgvMasini.Columns["Brand"].HeaderText = "Marcă";
            dgvMasini.Columns["Model"].HeaderText = "Model";
            dgvMasini.Columns["Year"].HeaderText = "An";
            dgvMasini.Columns["DailyPrice"].HeaderText = "Preț/zi (RON)";
            dgvMasini.Columns["DailyPrice"].DefaultCellStyle.Format = "N2";
            dgvMasini.Columns["IsAvailable"].HeaderText = "Disponibil";
        }
    }
    
    private void ClearCarForm()
    {
        txtBrand.Clear();
        txtModel.Clear();
        numYear.Value = DateTime.Now.Year;
        numPrice.Value = 0;
        txtBrand.Focus();
    }
}

// MODEL CLASS
public class Car
{
    public int Id { get; set; }
    public string Brand { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }
    public decimal DailyPrice { get; set; }
    public bool IsAvailable { get; set; }
}
```

---

## RECAPITULARE FINALĂ

### Cei 10 Pași Esențiali pentru Legarea UI-Backend:

1. **Definire UI** în `Form1.Designer.cs` (Visual Studio Designer)
2. **Creare clase Model** (Car, Client, Rental)
3. **Inițializare controale** în constructor Form1
4. **Înregistrare event handlers** (buton.Click += metodă)
5. **Citire date din UI** (textbox.Text, numeric.Value)
6. **Validare input** (verificări înainte de procesare)
7. **Procesare logică** (creare obiecte, salvare în listă/DB)
8. **Actualizare UI** (refresh DataGridView, update labels)
9. **Feedback utilizator** (MessageBox, status labels, culori)
10. **Gestionare erori** (try-catch, logging, mesaje clare)

---

**Succes la prezentare! Acest document acoperă tot ce trebuie să știi despre legarea UI-ului cu codul! 🚀**
