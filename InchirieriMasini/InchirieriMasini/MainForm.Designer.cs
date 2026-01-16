using System.Runtime.CompilerServices;

namespace InchirieriMasini;

partial class MainForm
{
    
    private System.ComponentModel.IContainer components = null;
    
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code
    

    //Masini
    private DataGridView dgvMasini;
    
    private Button btnAfiseazaToate;
    private Button btnDisponibile;
    private Button btnAdaugaMasina;
    private Button btnCauta;
        
    private TextBox txtBrand;
    private TextBox txtModel;
        
    private NumericUpDown numYear;
    private NumericUpDown numPrice;
    private NumericUpDown numSearchId;
        
    private Label lblStatus;
        
    private GroupBox grpAdaugaMasina;
    private GroupBox grpCautaMasina;
        
    //Clienti
    private DataGridView dgvClienti;
        
    private Button btnAdaugaClient;
    private Button btnStergeClient;
    private Button btnCautaClientId;
    private Button btnCautaClientEmail;
        
    private TextBox txtNume;
    private TextBox txtPrenume;
    private TextBox txtEmail;  
    private TextBox txtSearchEmail;
    private TextBox txtIdClient;
        
    private NumericUpDown numClientId;
    
    private GroupBox grpAdaugaClient;
    private GroupBox grpStergeClient;
    private GroupBox grpCautaClientId;
    private GroupBox grpCautaClientEmail;

    private Label lblClientStatus;
    
    //Inchirieri
    private DataGridView dgvInchirieri;
    
    private Button btnAfisareInchirieriActive;
    private Button btnReturnare;
    private Button btnInchirieriClient;
    private Button btnCreeazaInchiriere;
    private Button btnZileRamase;
    
    private NumericUpDown numCreeazaCarId;
    private NumericUpDown numCreeazaClientId;
    private NumericUpDown numDays;
    private NumericUpDown numRentalId;
    private NumericUpDown numInchirieriClientId;
    private NumericUpDown numZileRentalId;
    private DateTimePicker dtpStartDate;
    
    private GroupBox grpCreeazaInchiriere;
    private GroupBox grpReturnare;
    private GroupBox grpInchirieriClient;
    private GroupBox grpZileRamase;
    
    private TabControl tabControl;
    private TabPage tabMasini;
    private TabPage tabClienti;
    private TabPage tabInchirieri;

    private Label lblInchirieriStatus;
    private void InitializeComponent()
    {
        tabControl = new TabControl();
        tabControl.Location = new Point(10, 10);
        tabControl.Size = new Size(930, 630);

        tabMasini = new TabPage("Masini");
        tabClienti = new TabPage("Clienti");
        tabInchirieri = new TabPage("Inchirieri");
        
        tabControl.TabPages.Add(tabMasini);
        tabControl.TabPages.Add(tabClienti);
        tabControl.TabPages.Add(tabInchirieri);
        this.Controls.Add(tabControl);
        
        //MASINI
        
        this.Text = "Masini - Inchirieri Auto";
        this.ClientSize = new Size(950, 700);
        this.StartPosition = FormStartPosition.CenterScreen;

        dgvMasini = new DataGridView()
            { Location = new Point(10, 10),Size=new Size(550,300),ReadOnly=true,AllowUserToAddRows=false,
        AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill};

        btnAfiseazaToate = new Button() { Text = "Afiseaza toate", Location = new Point(580, 20), Size = new Size(280, 30) };
        btnDisponibile = new Button() { Text = "Afiseaza masini disponibile", Location = new Point(580, 60), Size = new Size(280, 30) };
        
        grpAdaugaMasina = new GroupBox() { Text = "Adauga Masina", Location = new Point(580, 110), Size = new Size(280, 170) };
        txtBrand = new TextBox() { Location = new Point(15, 30), Width = 250, PlaceholderText = "Brand" };
        txtModel = new TextBox() { Location = new Point(15, 60), Width = 250, PlaceholderText = "Model" };
        numYear = new NumericUpDown() { Location = new Point(15, 90), Minimum = 1990, Maximum = 2030 };
        numPrice = new NumericUpDown() { Location = new Point(15, 120), DecimalPlaces = 2, Maximum = 10000 };
        btnAdaugaMasina = new Button() { Text = "Adaugă", Location = new Point(15, 145), Width = 250 };
        
        grpAdaugaMasina.Controls.Add(txtBrand);
        grpAdaugaMasina.Controls.Add(txtModel);
        grpAdaugaMasina.Controls.Add(numYear);
        grpAdaugaMasina.Controls.Add(numPrice);
        grpAdaugaMasina.Controls.Add(btnAdaugaMasina);
        
        grpCautaMasina = new GroupBox() { Text = "Caută după ID", Location = new Point(580, 300), Size = new Size(280, 80) };
        numSearchId = new NumericUpDown() { Location = new Point(15, 30), Minimum = 1,Maximum=1000000};
        btnCauta = new Button() { Text = "Caută", Location = new Point(150, 30) };
        
        grpCautaMasina.Controls.Add(numSearchId);
        grpCautaMasina.Controls.Add(btnCauta);
        
        lblStatus = new Label() { Location = new Point(580, 410), AutoSize = true, Text = "-" };
        
        tabMasini.Controls.Add(dgvMasini);
        tabMasini.Controls.Add(btnAfiseazaToate);
        tabMasini.Controls.Add(btnDisponibile);
        tabMasini.Controls.Add(grpAdaugaMasina);
        tabMasini.Controls.Add(grpCautaMasina);
        tabMasini.Controls.Add(lblStatus);
        
        //CLIENTI

        dgvClienti = new DataGridView()
            { Location = new Point(10, 10),Size=new Size(550,300),ReadOnly=true,AllowUserToAddRows=false,MultiSelect=false};
        dgvClienti.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvClienti.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        
        grpAdaugaClient = new GroupBox(){Text="Adauga Client",Location=new Point(580,10),Size=new Size(280,150)};
        txtNume = new TextBox() { Location = new Point(15, 25), Width = 250 ,PlaceholderText="Nume"};
        txtPrenume = new TextBox() { Location = new Point(15, 55), Width = 250,PlaceholderText="Prenume" };
        txtEmail = new TextBox() { Location = new Point(15, 85), Width = 250, PlaceholderText="Email" };
        btnAdaugaClient = new Button() { Text = "Adaugă Client", Location = new Point(15, 115), Width = 250 };

        grpAdaugaClient.Controls.Add(txtNume);
        grpAdaugaClient.Controls.Add(txtPrenume);
        grpAdaugaClient.Controls.Add(txtEmail);
        grpAdaugaClient.Controls.Add(btnAdaugaClient);

        
        grpStergeClient = new GroupBox() 
            {Text="Sterge Client",Location=new Point(580,170),Size=new Size(280,100)};
        txtIdClient = new TextBox() { Location = new Point(15, 25), Width = 250 ,PlaceholderText="Id Client"};
        btnStergeClient = new Button() { Text = "Șterge Client", Location = new Point(15, 55), Width = 250 };

        grpStergeClient.Controls.Add(txtIdClient);
        grpStergeClient.Controls.Add(btnStergeClient);

        
        grpCautaClientId = new GroupBox()
                   { Text = "Cauta Client dupa ID", Location = new Point(580, 300), Size = new Size(280, 100)}; 
        numClientId = new NumericUpDown() { Location = new Point(15, 30),Width=120, Minimum = 1,Maximum=1000000};
        btnCautaClientId = new Button() { Text = "Caută", Location = new Point(150, 30),Width=110 };
        
        grpCautaClientId.Controls.Add(numClientId);
        grpCautaClientId.Controls.Add(btnCautaClientId);

        
        grpCautaClientEmail = new GroupBox()
            { Text = "Cauta Client Dupa Email", Location = new Point(580, 410), Size = new Size(280, 100) };
        txtSearchEmail = new TextBox() { Location = new Point(15, 30), Width = 250,PlaceholderText="email" };
        btnCautaClientEmail = new Button() { Text = "Caută", Location = new Point(15, 60), Width = 250 };
        
        grpCautaClientEmail.Controls.Add(txtSearchEmail);
        grpCautaClientEmail.Controls.Add(btnCautaClientEmail);

        lblClientStatus = new Label() { Location = new Point(580, 400), AutoSize = true, Text = "-" };
        
        tabClienti.Controls.Add(dgvClienti);
        tabClienti.Controls.Add(grpAdaugaClient);
        tabClienti.Controls.Add(grpStergeClient);
        tabClienti.Controls.Add(grpCautaClientId);
        tabClienti.Controls.Add(grpCautaClientEmail);
        tabClienti.Controls.Add(lblClientStatus);
        
        //INCHIRIERI
        dgvInchirieri = new DataGridView() 
            {Location=new Point(10,10),Size=new Size(550,550),ReadOnly=true,AllowUserToAddRows=false};
        dgvInchirieri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        
        btnAfisareInchirieriActive = new Button() 
            { Text = "Afișează închirieri active", Location = new Point(580, 520), Width = 230,Height=35};
        
        grpCreeazaInchiriere = new GroupBox()
            {Text = "Creează Închiriere", Location = new Point(580, 10), Size = new Size(300, 190)};
        numCreeazaCarId = new NumericUpDown() {Location = new Point(15, 25), Minimum = 1,Maximum=1000000, Width = 260};
        numCreeazaClientId = new NumericUpDown() {Location = new Point(15, 55), Minimum = 1, Maximum=100000, Width = 260};
        dtpStartDate = new DateTimePicker() {Location = new Point(15, 85), Width = 260};
        numDays = new NumericUpDown() {Location = new Point(15, 115), Minimum = 1, Maximum = 365, Width = 260};
        btnCreeazaInchiriere = new Button() {Text = "Creează", Location = new Point(15, 145), Width = 260};
        
        grpCreeazaInchiriere.Controls.Add(numCreeazaCarId);
        grpCreeazaInchiriere.Controls.Add(numCreeazaClientId);
        grpCreeazaInchiriere.Controls.Add(dtpStartDate);
        grpCreeazaInchiriere.Controls.Add(numDays);
        grpCreeazaInchiriere.Controls.Add(btnCreeazaInchiriere);
        
        grpReturnare = new GroupBox()
            {Text = "Returnare", Location = new Point(580, 210), Size = new Size(300, 90)};
        numRentalId = new NumericUpDown() {Location = new Point(15, 30), Minimum = 1,Maximum=1000000, Width = 260};
        btnReturnare = new Button() { Text = "Returnare", Location = new Point(15, 55), Width = 260 };
        
        grpReturnare.Controls.Add(numRentalId);
        grpReturnare.Controls.Add(btnReturnare);

    
        grpInchirieriClient = new GroupBox()
            {Text = "Închirieri Client", Location = new Point(580, 315), Size = new Size(300, 90)};
        numInchirieriClientId = new NumericUpDown() {Location = new Point(15, 30), Minimum = 1,Maximum=1000000, Width = 260};
        btnInchirieriClient = new Button() {Text = "Afișează", Location = new Point(15, 55), Width = 260};
        
        grpInchirieriClient.Controls.Add(numInchirieriClientId);
        grpInchirieriClient.Controls.Add(btnInchirieriClient);

       
        grpZileRamase = new GroupBox() 
            {Text = "Zile Rămase Închiriere", Location = new Point(580, 415),Size = new Size(300, 90)};
        numZileRentalId = new NumericUpDown() {Location = new Point(15, 30), Minimum = 1,Maximum=1000000, Width = 120};
        btnZileRamase = new Button() {Text = "Calculează", Location = new Point(150, 30), Width = 120};
        
         grpZileRamase.Controls.Add(numZileRentalId);
         grpZileRamase.Controls.Add(btnZileRamase);
    
        lblInchirieriStatus = new Label() { Location = new Point(580, 420), AutoSize = true, Text = "-" };
    
        tabInchirieri.Controls.Add(dgvInchirieri);
        tabInchirieri.Controls.Add(btnAfisareInchirieriActive);
        tabInchirieri.Controls.Add(grpCreeazaInchiriere);
        tabInchirieri.Controls.Add(grpReturnare);
        tabInchirieri.Controls.Add(grpInchirieriClient);
        tabInchirieri.Controls.Add(grpZileRamase);
        tabInchirieri.Controls.Add(lblInchirieriStatus);
        }
        #endregion
}