using InchirieriMasini.Models;
using InchirieriMasini.Services;
using Microsoft.Extensions.Logging;

namespace InchirieriMasini;

public partial class MainForm : Form
{
    private readonly VehicleService _vehicleService;
    private readonly CustomerService _customerService;
    private readonly RentalService _rentalService;
    private readonly ILogger<MainForm> _logger;

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

    private void LoadData()
    {
        try
        {
            _logger.LogInformation("Loading application data");
            RefreshVehicles();
            RefreshCustomers();
            RefreshRentals();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading application data");
            MessageBox.Show($"Error loading data: {ex.Message}", "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void RefreshVehicles()
    {
        lstVehicles.Items.Clear();
        var vehicles = _vehicleService.GetAllVehicles();
        foreach (var vehicle in vehicles)
        {
            lstVehicles.Items.Add($"[{vehicle.Id}] {vehicle.GetVehicleInfo()} - ${vehicle.PricePerDay}/day {(vehicle.IsAvailable ? "✓" : "✗")}");
        }
    }

    private void RefreshCustomers()
    {
        lstCustomers.Items.Clear();
        var customers = _customerService.GetAllCustomers();
        foreach (var customer in customers)
        {
            lstCustomers.Items.Add($"[{customer.Id}] {customer.FullName} - {customer.Email}");
        }
    }

    private void RefreshRentals()
    {
        lstRentals.Items.Clear();
        var rentals = _rentalService.GetAllRentals();
        foreach (var rental in rentals)
        {
            var customerName = rental.Customer?.FullName ?? "Unknown";
            var vehicleInfo = rental.Vehicle?.GetVehicleInfo() ?? "Unknown";
            lstRentals.Items.Add($"[{rental.Id}] {customerName} - {vehicleInfo} ({rental.Status})");
        }
    }

    private void btnAddVehicle_Click(object sender, EventArgs e)
    {
        try
        {
            _logger.LogInformation("Adding new vehicle");
            
            // Simple example with Car
            var car = new Car
            {
                Brand = txtVehicleBrand.Text,
                Model = txtVehicleModel.Text,
                Year = int.Parse(txtVehicleYear.Text),
                LicensePlate = txtLicensePlate.Text,
                PricePerDay = decimal.Parse(txtPricePerDay.Text),
                NumberOfDoors = 4,
                FuelType = "Petrol",
                Transmission = "Manual",
                IsAvailable = true
            };

            _vehicleService.AddVehicle(car);
            RefreshVehicles();
            ClearVehicleForm();
            MessageBox.Show("Vehicle added successfully!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding vehicle");
            MessageBox.Show($"Error adding vehicle: {ex.Message}", "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnAddCustomer_Click(object sender, EventArgs e)
    {
        try
        {
            _logger.LogInformation("Adding new customer");
            
            var customer = new Customer
            {
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                PhoneNumber = txtPhone.Text,
                DriverLicenseNumber = txtDriverLicense.Text
            };

            _customerService.AddCustomer(customer);
            RefreshCustomers();
            ClearCustomerForm();
            MessageBox.Show("Customer added successfully!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding customer");
            MessageBox.Show($"Error adding customer: {ex.Message}", "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCreateRental_Click(object sender, EventArgs e)
    {
        try
        {
            _logger.LogInformation("Creating new rental");
            
            int customerId = int.Parse(txtRentalCustomerId.Text);
            int vehicleId = int.Parse(txtRentalVehicleId.Text);
            int days = int.Parse(txtRentalDays.Text);

            var rental = _rentalService.CreateRental(customerId, vehicleId, days);
            RefreshRentals();
            RefreshVehicles();
            ClearRentalForm();
            
            MessageBox.Show($"Rental created successfully!\nTotal cost: ${rental.TotalCost}", 
                "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating rental");
            MessageBox.Show($"Error creating rental: {ex.Message}", "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCompleteRental_Click(object sender, EventArgs e)
    {
        try
        {
            int rentalId = int.Parse(txtCompleteRentalId.Text);
            _rentalService.CompleteRental(rentalId);
            RefreshRentals();
            RefreshVehicles();
            txtCompleteRentalId.Clear();
            MessageBox.Show("Rental completed successfully!", "Success", 
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error completing rental");
            MessageBox.Show($"Error completing rental: {ex.Message}", "Error", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ClearVehicleForm()
    {
        txtVehicleBrand.Clear();
        txtVehicleModel.Clear();
        txtVehicleYear.Clear();
        txtLicensePlate.Clear();
        txtPricePerDay.Clear();
    }

    private void ClearCustomerForm()
    {
        txtFirstName.Clear();
        txtLastName.Clear();
        txtEmail.Clear();
        txtPhone.Clear();
        txtDriverLicense.Clear();
    }

    private void ClearRentalForm()
    {
        txtRentalCustomerId.Clear();
        txtRentalVehicleId.Clear();
        txtRentalDays.Clear();
    }
}