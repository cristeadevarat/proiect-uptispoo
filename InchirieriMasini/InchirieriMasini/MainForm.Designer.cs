namespace InchirieriMasini;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.components = new System.ComponentModel.Container();
        
        // Form setup
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1200, 700);
        this.Text = "Car Rental Management System - Inchirieri Masini";
        this.StartPosition = FormStartPosition.CenterScreen;

        // Create tab control
        var tabControl = new TabControl
        {
            Dock = DockStyle.Fill
        };

        // Vehicles Tab
        var tabVehicles = new TabPage("Vehicles");
        CreateVehiclesTab(tabVehicles);
        tabControl.TabPages.Add(tabVehicles);

        // Customers Tab
        var tabCustomers = new TabPage("Customers");
        CreateCustomersTab(tabCustomers);
        tabControl.TabPages.Add(tabCustomers);

        // Rentals Tab
        var tabRentals = new TabPage("Rentals");
        CreateRentalsTab(tabRentals);
        tabControl.TabPages.Add(tabRentals);

        this.Controls.Add(tabControl);
    }

    // Vehicle controls
    private ListBox lstVehicles = null!;
    private TextBox txtVehicleBrand = null!;
    private TextBox txtVehicleModel = null!;
    private TextBox txtVehicleYear = null!;
    private TextBox txtLicensePlate = null!;
    private TextBox txtPricePerDay = null!;
    private Button btnAddVehicle = null!;

    // Customer controls
    private ListBox lstCustomers = null!;
    private TextBox txtFirstName = null!;
    private TextBox txtLastName = null!;
    private TextBox txtEmail = null!;
    private TextBox txtPhone = null!;
    private TextBox txtDriverLicense = null!;
    private Button btnAddCustomer = null!;

    // Rental controls
    private ListBox lstRentals = null!;
    private TextBox txtRentalCustomerId = null!;
    private TextBox txtRentalVehicleId = null!;
    private TextBox txtRentalDays = null!;
    private TextBox txtCompleteRentalId = null!;
    private Button btnCreateRental = null!;
    private Button btnCompleteRental = null!;

    private void CreateVehiclesTab(TabPage tab)
    {
        var splitContainer = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal
        };

        // Top panel - list of vehicles
        lstVehicles = new ListBox
        {
            Dock = DockStyle.Fill
        };
        splitContainer.Panel1.Controls.Add(lstVehicles);

        // Bottom panel - add vehicle form
        var formPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
        var yPos = 10;

        formPanel.Controls.Add(new Label { Text = "Add New Vehicle:", Location = new Point(10, yPos), AutoSize = true, Font = new Font(Font, FontStyle.Bold) });
        yPos += 30;

        formPanel.Controls.Add(new Label { Text = "Brand:", Location = new Point(10, yPos), AutoSize = true });
        txtVehicleBrand = new TextBox { Location = new Point(120, yPos - 3), Width = 200 };
        formPanel.Controls.Add(txtVehicleBrand);

        formPanel.Controls.Add(new Label { Text = "Model:", Location = new Point(340, yPos), AutoSize = true });
        txtVehicleModel = new TextBox { Location = new Point(450, yPos - 3), Width = 200 };
        formPanel.Controls.Add(txtVehicleModel);
        yPos += 35;

        formPanel.Controls.Add(new Label { Text = "Year:", Location = new Point(10, yPos), AutoSize = true });
        txtVehicleYear = new TextBox { Location = new Point(120, yPos - 3), Width = 100 };
        formPanel.Controls.Add(txtVehicleYear);

        formPanel.Controls.Add(new Label { Text = "License Plate:", Location = new Point(240, yPos), AutoSize = true });
        txtLicensePlate = new TextBox { Location = new Point(350, yPos - 3), Width = 150 };
        formPanel.Controls.Add(txtLicensePlate);

        formPanel.Controls.Add(new Label { Text = "Price/Day:", Location = new Point(520, yPos), AutoSize = true });
        txtPricePerDay = new TextBox { Location = new Point(600, yPos - 3), Width = 100 };
        formPanel.Controls.Add(txtPricePerDay);
        yPos += 40;

        btnAddVehicle = new Button { Text = "Add Vehicle", Location = new Point(10, yPos), Width = 120 };
        btnAddVehicle.Click += btnAddVehicle_Click;
        formPanel.Controls.Add(btnAddVehicle);

        splitContainer.Panel2.Controls.Add(formPanel);
        tab.Controls.Add(splitContainer);
    }

    private void CreateCustomersTab(TabPage tab)
    {
        var splitContainer = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal
        };

        // Top panel - list of customers
        lstCustomers = new ListBox
        {
            Dock = DockStyle.Fill
        };
        splitContainer.Panel1.Controls.Add(lstCustomers);

        // Bottom panel - add customer form
        var formPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
        var yPos = 10;

        formPanel.Controls.Add(new Label { Text = "Add New Customer:", Location = new Point(10, yPos), AutoSize = true, Font = new Font(Font, FontStyle.Bold) });
        yPos += 30;

        formPanel.Controls.Add(new Label { Text = "First Name:", Location = new Point(10, yPos), AutoSize = true });
        txtFirstName = new TextBox { Location = new Point(120, yPos - 3), Width = 200 };
        formPanel.Controls.Add(txtFirstName);

        formPanel.Controls.Add(new Label { Text = "Last Name:", Location = new Point(340, yPos), AutoSize = true });
        txtLastName = new TextBox { Location = new Point(450, yPos - 3), Width = 200 };
        formPanel.Controls.Add(txtLastName);
        yPos += 35;

        formPanel.Controls.Add(new Label { Text = "Email:", Location = new Point(10, yPos), AutoSize = true });
        txtEmail = new TextBox { Location = new Point(120, yPos - 3), Width = 250 };
        formPanel.Controls.Add(txtEmail);

        formPanel.Controls.Add(new Label { Text = "Phone:", Location = new Point(390, yPos), AutoSize = true });
        txtPhone = new TextBox { Location = new Point(450, yPos - 3), Width = 200 };
        formPanel.Controls.Add(txtPhone);
        yPos += 35;

        formPanel.Controls.Add(new Label { Text = "Driver License:", Location = new Point(10, yPos), AutoSize = true });
        txtDriverLicense = new TextBox { Location = new Point(120, yPos - 3), Width = 200 };
        formPanel.Controls.Add(txtDriverLicense);
        yPos += 40;

        btnAddCustomer = new Button { Text = "Add Customer", Location = new Point(10, yPos), Width = 120 };
        btnAddCustomer.Click += btnAddCustomer_Click;
        formPanel.Controls.Add(btnAddCustomer);

        splitContainer.Panel2.Controls.Add(formPanel);
        tab.Controls.Add(splitContainer);
    }

    private void CreateRentalsTab(TabPage tab)
    {
        var splitContainer = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal
        };

        // Top panel - list of rentals
        lstRentals = new ListBox
        {
            Dock = DockStyle.Fill
        };
        splitContainer.Panel1.Controls.Add(lstRentals);

        // Bottom panel - rental operations
        var formPanel = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10) };
        var yPos = 10;

        formPanel.Controls.Add(new Label { Text = "Create New Rental:", Location = new Point(10, yPos), AutoSize = true, Font = new Font(Font, FontStyle.Bold) });
        yPos += 30;

        formPanel.Controls.Add(new Label { Text = "Customer ID:", Location = new Point(10, yPos), AutoSize = true });
        txtRentalCustomerId = new TextBox { Location = new Point(120, yPos - 3), Width = 100 };
        formPanel.Controls.Add(txtRentalCustomerId);

        formPanel.Controls.Add(new Label { Text = "Vehicle ID:", Location = new Point(240, yPos), AutoSize = true });
        txtRentalVehicleId = new TextBox { Location = new Point(330, yPos - 3), Width = 100 };
        formPanel.Controls.Add(txtRentalVehicleId);

        formPanel.Controls.Add(new Label { Text = "Days:", Location = new Point(450, yPos), AutoSize = true });
        txtRentalDays = new TextBox { Location = new Point(500, yPos - 3), Width = 100 };
        formPanel.Controls.Add(txtRentalDays);

        btnCreateRental = new Button { Text = "Create Rental", Location = new Point(620, yPos - 3), Width = 120 };
        btnCreateRental.Click += btnCreateRental_Click;
        formPanel.Controls.Add(btnCreateRental);
        yPos += 50;

        formPanel.Controls.Add(new Label { Text = "Complete Rental:", Location = new Point(10, yPos), AutoSize = true, Font = new Font(Font, FontStyle.Bold) });
        yPos += 30;

        formPanel.Controls.Add(new Label { Text = "Rental ID:", Location = new Point(10, yPos), AutoSize = true });
        txtCompleteRentalId = new TextBox { Location = new Point(120, yPos - 3), Width = 100 };
        formPanel.Controls.Add(txtCompleteRentalId);

        btnCompleteRental = new Button { Text = "Complete Rental", Location = new Point(240, yPos - 3), Width = 140 };
        btnCompleteRental.Click += btnCompleteRental_Click;
        formPanel.Controls.Add(btnCompleteRental);

        splitContainer.Panel2.Controls.Add(formPanel);
        tab.Controls.Add(splitContainer);
    }

    #endregion
}