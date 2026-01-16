using InchirieriMasini.Services;
using InchirieriMasini.Persistence;

namespace InchirieriMasini;

public partial class MainForm : Form
{
    private readonly CarService _carService;
    private readonly ClientService _clientService;
    private readonly RentalService _rentalService;
    private readonly AppController _appController;

    public MainForm()
    {
        InitializeComponent();

        // Initialize services
        _carService = new CarService();
        _clientService = new ClientService();
        _rentalService = new RentalService(_carService, _clientService);

        // Initialize storage and controller
        var storage = new JsonStorage("data.json");
        _appController = new AppController(storage, _carService, _clientService, _rentalService);

        // Load data from file
        try
        {
            _appController.Load();
        }
        catch
        {
            // If file doesn't exist or is invalid, start fresh
        }

        // Wire up event handlers
        WireUpEvents();

        // Add labels for NumericUpDown controls
        AddLabels();

        // Initial data load
        RefreshCarsGrid();
        RefreshClientsGrid();
        RefreshRentalsGrid();
    }

    private void WireUpEvents()
    {
        // Cars tab events
        btnAfiseazaToate.Click += BtnAfiseazaToate_Click;
        btnDisponibile.Click += BtnDisponibile_Click;
        btnAdaugaMasina.Click += BtnAdaugaMasina_Click;
        btnCauta.Click += BtnCauta_Click;

        // Clients tab events
        btnAdaugaClient.Click += BtnAdaugaClient_Click;
        btnStergeClient.Click += BtnStergeClient_Click;
        btnCautaClientId.Click += BtnCautaClientId_Click;
        btnCautaClientEmail.Click += BtnCautaClientEmail_Click;

        // Rentals tab events
        btnCreeazaInchiriere.Click += BtnCreeazaInchiriere_Click;
        btnReturnare.Click += BtnReturnare_Click;
        btnInchirieriClient.Click += BtnInchirieriClient_Click;
        btnZileRamase.Click += BtnZileRamase_Click;
        btnAfisareInchirieriActive.Click += BtnAfisareInchirieriActive_Click;

        // Save data on form closing
        this.FormClosing += MainForm_FormClosing;
    }

    private void AddLabels()
    {
        // Cars tab labels
        var lblBrand = new Label { Text = "Brand:", Location = new Point(595, 140), AutoSize = true };
        var lblModel = new Label { Text = "Model:", Location = new Point(595, 170), AutoSize = true };
        var lblYear = new Label { Text = "An:", Location = new Point(595, 200), AutoSize = true };
        var lblPrice = new Label { Text = "Pret/zi:", Location = new Point(595, 230), AutoSize = true };
        var lblSearchId = new Label { Text = "ID Masina:", Location = new Point(595, 330), AutoSize = true };

        grpAdaugaMasina.Controls.Add(lblBrand);
        grpAdaugaMasina.Controls.Add(lblModel);
        grpAdaugaMasina.Controls.Add(lblYear);
        grpAdaugaMasina.Controls.Add(lblPrice);
        grpCautaMasina.Controls.Add(lblSearchId);

        // Rentals tab labels
        var lblCarId = new Label { Text = "ID Masina:", Location = new Point(15, 25), Width = 260, AutoSize = false };
        var lblClientId = new Label { Text = "ID Client:", Location = new Point(15, 55), Width = 260, AutoSize = false };
        var lblStartDate = new Label { Text = "Data start:", Location = new Point(15, 85), Width = 260, AutoSize = false };
        var lblDays = new Label { Text = "Numar zile:", Location = new Point(15, 115), Width = 260, AutoSize = false };
        var lblRentalId = new Label { Text = "ID Inchiriere:", Location = new Point(15, 30), Width = 260, AutoSize = false };
        var lblClientIdRent = new Label { Text = "ID Client:", Location = new Point(15, 30), Width = 260, AutoSize = false };
        var lblRentalIdDays = new Label { Text = "ID Inchiriere:", Location = new Point(15, 30), Width = 120, AutoSize = false };

        grpCreeazaInchiriere.Controls.Add(lblCarId);
        grpCreeazaInchiriere.Controls.Add(lblClientId);
        grpCreeazaInchiriere.Controls.Add(lblStartDate);
        grpCreeazaInchiriere.Controls.Add(lblDays);
        grpReturnare.Controls.Add(lblRentalId);
        grpInchirieriClient.Controls.Add(lblClientIdRent);
        grpZileRamase.Controls.Add(lblRentalIdDays);
    }

    // Helper methods for refreshing DataGridViews
    private void RefreshCarsGrid()
    {
        var cars = _carService.GetAllCars().Select(c => new
        {
            ID = c.GetId(),
            Brand = c.GetBrand(),
            Model = c.GetModel(),
            Year = c.GetYear(),
            PretPeZi = c.GetPricePerDay(),
            Disponibil = c.GetIsAvailable() ? "Da" : "Nu"
        }).ToList();

        dgvMasini.DataSource = cars;
    }

    private void RefreshClientsGrid()
    {
        var clients = _clientService.GetAllClients().Select(c => new
        {
            ID = c.GetId(),
            Nume = c.GetName(),
            Telefon = c.GetPhone(),
            Email = c.GetEmail()
        }).ToList();

        dgvClienti.DataSource = clients;
    }

    private void RefreshRentalsGrid()
    {
        var rentals = _rentalService.GetAllRentals().Select(r => new
        {
            ID = r.GetId(),
            IDMasina = r.GetCarId(),
            IDClient = r.GetClientId(),
            DataStart = r.GetStartDate().ToShortDateString(),
            Durata = r.GetDays(),
            PretTotal = r.GetTotalPrice(),
            Activ = r.GetIsActive() ? "Da" : "Nu"
        }).ToList();

        dgvInchirieri.DataSource = rentals;
    }

    // Cars tab event handlers
    private void BtnAfiseazaToate_Click(object? sender, EventArgs e)
    {
        RefreshCarsGrid();
        lblStatus.Text = $"Afisate {_carService.GetAllCars().Count()} masini.";
    }

    private void BtnDisponibile_Click(object? sender, EventArgs e)
    {
        var availableCars = _carService.GetAvailableCars().Select(c => new
        {
            ID = c.GetId(),
            Brand = c.GetBrand(),
            Model = c.GetModel(),
            Year = c.GetYear(),
            PretPeZi = c.GetPricePerDay(),
            Disponibil = "Da"
        }).ToList();

        dgvMasini.DataSource = availableCars;
        lblStatus.Text = $"Afisate {availableCars.Count} masini disponibile.";
    }

    private void BtnAdaugaMasina_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtBrand.Text) || string.IsNullOrWhiteSpace(txtModel.Text))
        {
            lblStatus.Text = "Brand si model sunt obligatorii!";
            return;
        }

        var result = _carService.TryAddCar(txtBrand.Text, txtModel.Text, (int)numYear.Value, (double)numPrice.Value);
        if (result.Success)
        {
            RefreshCarsGrid();
            lblStatus.Text = $"Masina adaugata cu succes! ID: {result.Data!.GetId()}";
            txtBrand.Clear();
            txtModel.Clear();
            numYear.Value = numYear.Minimum;
            numPrice.Value = 0;
        }
        else
        {
            lblStatus.Text = result.Message;
        }
    }

    private void BtnCauta_Click(object? sender, EventArgs e)
    {
        var car = _carService.GetById((int)numSearchId.Value);
        if (car != null)
        {
            var carList = new[] { new
            {
                ID = car.GetId(),
                Brand = car.GetBrand(),
                Model = car.GetModel(),
                Year = car.GetYear(),
                PretPeZi = car.GetPricePerDay(),
                Disponibil = car.GetIsAvailable() ? "Da" : "Nu"
            }}.ToList();

            dgvMasini.DataSource = carList;
            lblStatus.Text = $"Gasita masina: {car.GetBrand()} {car.GetModel()}";
        }
        else
        {
            lblStatus.Text = $"Nu exista masina cu ID-ul {numSearchId.Value}";
        }
    }

    // Clients tab event handlers
    private void BtnAdaugaClient_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtNume.Text) || string.IsNullOrWhiteSpace(txtPrenume.Text) || string.IsNullOrWhiteSpace(txtEmail.Text))
        {
            lblClientStatus.Text = "Toate campurile sunt obligatorii!";
            return;
        }

        var fullName = txtNume.Text + " " + txtPrenume.Text;
        var result = _clientService.TryAddClient(fullName, txtPrenume.Text, txtEmail.Text);
        
        if (result.Success)
        {
            RefreshClientsGrid();
            lblClientStatus.Text = $"Client adaugat cu succes! ID: {result.Data!.GetId()}";
            txtNume.Clear();
            txtPrenume.Clear();
            txtEmail.Clear();
        }
        else
        {
            lblClientStatus.Text = result.Message;
        }
    }

    private void BtnStergeClient_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtIdClient.Text) || !int.TryParse(txtIdClient.Text, out int clientId))
        {
            lblClientStatus.Text = "ID-ul clientului trebuie sa fie un numar valid!";
            return;
        }

        var message = _clientService.RemoveClient(clientId);
        RefreshClientsGrid();
        lblClientStatus.Text = message;
        txtIdClient.Clear();
    }

    private void BtnCautaClientId_Click(object? sender, EventArgs e)
    {
        var client = _clientService.GetById((int)numClientId.Value);
        if (client != null)
        {
            var clientList = new[] { new
            {
                ID = client.GetId(),
                Nume = client.GetName(),
                Telefon = client.GetPhone(),
                Email = client.GetEmail()
            }}.ToList();

            dgvClienti.DataSource = clientList;
            lblClientStatus.Text = $"Gasit client: {client.GetName()}";
        }
        else
        {
            lblClientStatus.Text = $"Nu exista client cu ID-ul {numClientId.Value}";
        }
    }

    private void BtnCautaClientEmail_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtSearchEmail.Text))
        {
            lblClientStatus.Text = "Introduceti un email!";
            return;
        }

        var client = _clientService.GetByEmail(txtSearchEmail.Text);
        if (client != null)
        {
            var clientList = new[] { new
            {
                ID = client.GetId(),
                Nume = client.GetName(),
                Telefon = client.GetPhone(),
                Email = client.GetEmail()
            }}.ToList();

            dgvClienti.DataSource = clientList;
            lblClientStatus.Text = $"Gasit client: {client.GetName()}";
        }
        else
        {
            lblClientStatus.Text = $"Nu exista client cu email-ul {txtSearchEmail.Text}";
            txtSearchEmail.Clear();
        }
    }

    // Rentals tab event handlers
    private void BtnCreeazaInchiriere_Click(object? sender, EventArgs e)
    {
        var result = _rentalService.TryCreateRental(
            (int)numCreeazaCarId.Value,
            (int)numCreeazaClientId.Value,
            dtpStartDate.Value,
            (int)numDays.Value
        );

        if (result.Success)
        {
            RefreshRentalsGrid();
            RefreshCarsGrid(); // Refresh cars to show updated availability
            lblInchirieriStatus.Text = $"Inchiriere creata cu succes! ID: {result.Data!.GetId()}, Pret total: {result.Data.GetTotalPrice()} RON";
        }
        else
        {
            lblInchirieriStatus.Text = result.Message;
        }
    }

    private void BtnReturnare_Click(object? sender, EventArgs e)
    {
        var result = _rentalService.TryCloseRental((int)numRentalId.Value);
        
        if (result.Success)
        {
            RefreshRentalsGrid();
            RefreshCarsGrid(); // Refresh cars to show updated availability
            lblInchirieriStatus.Text = $"Masina returnata cu succes pentru inchirierea {numRentalId.Value}";
        }
        else
        {
            lblInchirieriStatus.Text = result.Message;
        }
    }

    private void BtnInchirieriClient_Click(object? sender, EventArgs e)
    {
        var clientRentals = _rentalService.GetByClientId((int)numInchirieriClientId.Value).Select(r => new
        {
            ID = r.GetId(),
            IDMasina = r.GetCarId(),
            IDClient = r.GetClientId(),
            DataStart = r.GetStartDate().ToShortDateString(),
            Durata = r.GetDays(),
            PretTotal = r.GetTotalPrice(),
            Activ = r.GetIsActive() ? "Da" : "Nu"
        }).ToList();

        dgvInchirieri.DataSource = clientRentals;
        lblInchirieriStatus.Text = $"Afisate {clientRentals.Count} inchirieri active pentru clientul {numInchirieriClientId.Value}";
    }

    private void BtnZileRamase_Click(object? sender, EventArgs e)
    {
        var result = _rentalService.TryGetDaysRemaining((int)numZileRentalId.Value);
        
        if (result.Success)
        {
            lblInchirieriStatus.Text = $"Zile ramase pentru inchirierea {numZileRentalId.Value}: {result.Data} zile";
        }
        else
        {
            lblInchirieriStatus.Text = result.Message;
        }
    }

    private void BtnAfisareInchirieriActive_Click(object? sender, EventArgs e)
    {
        var activeRentals = _rentalService.GetActiveRentals().Select(r => new
        {
            ID = r.GetId(),
            IDMasina = r.GetCarId(),
            IDClient = r.GetClientId(),
            DataStart = r.GetStartDate().ToShortDateString(),
            Durata = r.GetDays(),
            PretTotal = r.GetTotalPrice(),
            Activ = "Da"
        }).ToList();

        dgvInchirieri.DataSource = activeRentals;
        lblInchirieriStatus.Text = $"Afisate {activeRentals.Count} inchirieri active.";
    }

    private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        try
        {
            _appController.Save();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Eroare la salvarea datelor: {ex.Message}", "Eroare", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}