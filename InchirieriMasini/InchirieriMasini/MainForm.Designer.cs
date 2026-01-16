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
        this.Text = "Masini - Inchirieri Auto";
        this.WindowState = FormWindowState.Maximized;
        this.MinimumSize = new Size(1200, 700);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.AutoScaleMode = AutoScaleMode.Font;

        tabControl = new TabControl();
        tabControl.Dock = DockStyle.Fill;
        tabControl.Padding = new Point(15, 15);

        tabMasini = new TabPage("Masini");
        tabMasini.Padding = new Padding(10);
        tabClienti = new TabPage("Clienti");
        tabClienti.Padding = new Padding(10);
        tabInchirieri = new TabPage("Inchirieri");
        tabInchirieri.Padding = new Padding(10);
        
        tabControl.TabPages.Add(tabMasini);
        tabControl.TabPages.Add(tabClienti);
        tabControl.TabPages.Add(tabInchirieri);
        this.Controls.Add(tabControl);
        
        //MASINI - cu SplitContainer
        var splitMasini = new SplitContainer();
        splitMasini.Dock = DockStyle.Fill;
        splitMasini.SplitterDistance = 700;
        splitMasini.IsSplitterFixed = false;
        
        dgvMasini = new DataGridView();
        dgvMasini.Dock = DockStyle.Fill;
        dgvMasini.ReadOnly = true;
        dgvMasini.AllowUserToAddRows = false;
        dgvMasini.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvMasini.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        splitMasini.Panel1.Controls.Add(dgvMasini);
        
        var panelMasiniRight = new Panel();
        panelMasiniRight.Dock = DockStyle.Fill;
        panelMasiniRight.AutoScroll = true;
        
        btnAfiseazaToate = new Button() { Text = "Afiseaza toate", Location = new Point(10, 10), Size = new Size(280, 35) };
        btnAfiseazaToate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        btnDisponibile = new Button() { Text = "Afiseaza masini disponibile", Location = new Point(10, 55), Size = new Size(280, 35) };
        btnDisponibile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpAdaugaMasina = new GroupBox() { Text = "Adauga Masina", Location = new Point(10, 105), Size = new Size(280, 200) };
        grpAdaugaMasina.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtBrand = new TextBox() { Location = new Point(15, 35), Width = 240, PlaceholderText = "Brand" };
        txtBrand.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtModel = new TextBox() { Location = new Point(15, 70), Width = 240, PlaceholderText = "Model" };
        txtModel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numYear = new NumericUpDown() { Location = new Point(15, 105), Width = 240, Minimum = 1990, Maximum = 2030 };
        numYear.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numPrice = new NumericUpDown() { Location = new Point(15, 140), Width = 240, DecimalPlaces = 2, Maximum = 10000 };
        numPrice.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnAdaugaMasina = new Button() { Text = "Adaugă", Location = new Point(15, 170), Width = 240 };
        btnAdaugaMasina.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpAdaugaMasina.Controls.Add(txtBrand);
        grpAdaugaMasina.Controls.Add(txtModel);
        grpAdaugaMasina.Controls.Add(numYear);
        grpAdaugaMasina.Controls.Add(numPrice);
        grpAdaugaMasina.Controls.Add(btnAdaugaMasina);
        
        grpCautaMasina = new GroupBox() { Text = "Caută după ID", Location = new Point(10, 315), Size = new Size(280, 100) };
        grpCautaMasina.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numSearchId = new NumericUpDown() { Location = new Point(15, 30), Width = 140, Minimum = 1, Maximum = 1000000 };
        btnCauta = new Button() { Text = "Caută", Location = new Point(165, 30), Width = 90 };
        btnCauta.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        
        grpCautaMasina.Controls.Add(numSearchId);
        grpCautaMasina.Controls.Add(btnCauta);
        
        lblStatus = new Label() { Location = new Point(10, 425), AutoSize = true, Text = "-", MaximumSize = new Size(280, 0) };
        lblStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        panelMasiniRight.Controls.Add(btnAfiseazaToate);
        panelMasiniRight.Controls.Add(btnDisponibile);
        panelMasiniRight.Controls.Add(grpAdaugaMasina);
        panelMasiniRight.Controls.Add(grpCautaMasina);
        panelMasiniRight.Controls.Add(lblStatus);
        
        splitMasini.Panel2.Controls.Add(panelMasiniRight);
        tabMasini.Controls.Add(splitMasini);
        
        //CLIENTI - cu SplitContainer
        var splitClienti = new SplitContainer();
        splitClienti.Dock = DockStyle.Fill;
        splitClienti.SplitterDistance = 700;
        splitClienti.IsSplitterFixed = false;
        
        dgvClienti = new DataGridView();
        dgvClienti.Dock = DockStyle.Fill;
        dgvClienti.ReadOnly = true;
        dgvClienti.AllowUserToAddRows = false;
        dgvClienti.MultiSelect = false;
        dgvClienti.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvClienti.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        splitClienti.Panel1.Controls.Add(dgvClienti);

        var panelClientiRight = new Panel();
        panelClientiRight.Dock = DockStyle.Fill;
        panelClientiRight.AutoScroll = true;
        
        grpAdaugaClient = new GroupBox(){Text="Adauga Client",Location=new Point(10,10),Size=new Size(280,170)};
        grpAdaugaClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtNume = new TextBox() { Location = new Point(15, 30), Width = 240 ,PlaceholderText="Nume"};
        txtNume.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtPrenume = new TextBox() { Location = new Point(15, 65), Width = 240,PlaceholderText="Prenume" };
        txtPrenume.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtEmail = new TextBox() { Location = new Point(15, 100), Width = 240, PlaceholderText="Email" };
        txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnAdaugaClient = new Button() { Text = "Adaugă Client", Location = new Point(15, 135), Width = 240 };
        btnAdaugaClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        grpAdaugaClient.Controls.Add(txtNume);
        grpAdaugaClient.Controls.Add(txtPrenume);
        grpAdaugaClient.Controls.Add(txtEmail);
        grpAdaugaClient.Controls.Add(btnAdaugaClient);

        
        grpStergeClient = new GroupBox() 
            {Text="Sterge Client",Location=new Point(10,190),Size=new Size(280,105)};
        grpStergeClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtIdClient = new TextBox() { Location = new Point(15, 30), Width = 240 ,PlaceholderText="Id Client"};
        txtIdClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnStergeClient = new Button() { Text = "Șterge Client", Location = new Point(15, 65), Width = 240 };
        btnStergeClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        grpStergeClient.Controls.Add(txtIdClient);
        grpStergeClient.Controls.Add(btnStergeClient);

        
        grpCautaClientId = new GroupBox()
                   { Text = "Cauta Client dupa ID", Location = new Point(10, 305), Size = new Size(280, 100)}; 
        grpCautaClientId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numClientId = new NumericUpDown() { Location = new Point(15, 35),Width=130, Minimum = 1,Maximum=1000000};
        btnCautaClientId = new Button() { Text = "Caută", Location = new Point(155, 35),Width = 100 };
        btnCautaClientId.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        
        grpCautaClientId.Controls.Add(numClientId);
        grpCautaClientId.Controls.Add(btnCautaClientId);

        
        grpCautaClientEmail = new GroupBox()
            { Text = "Cauta Client Dupa Email", Location = new Point(10, 415), Size = new Size(280, 105) };
        grpCautaClientEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        txtSearchEmail = new TextBox() { Location = new Point(15, 30), Width = 240,PlaceholderText="email" };
        txtSearchEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnCautaClientEmail = new Button() { Text = "Caută", Location = new Point(15, 65), Width = 240 };
        btnCautaClientEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpCautaClientEmail.Controls.Add(txtSearchEmail);
        grpCautaClientEmail.Controls.Add(btnCautaClientEmail);

        lblClientStatus = new Label() { Location = new Point(10, 530), AutoSize = true, Text = "-", MaximumSize = new Size(280, 0) };
        lblClientStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        panelClientiRight.Controls.Add(grpAdaugaClient);
        panelClientiRight.Controls.Add(grpStergeClient);
        panelClientiRight.Controls.Add(grpCautaClientId);
        panelClientiRight.Controls.Add(grpCautaClientEmail);
        panelClientiRight.Controls.Add(lblClientStatus);
        
        splitClienti.Panel2.Controls.Add(panelClientiRight);
        tabClienti.Controls.Add(splitClienti);
        
        //INCHIRIERI - cu SplitContainer
        var splitInchirieri = new SplitContainer();
        splitInchirieri.Dock = DockStyle.Fill;
        splitInchirieri.SplitterDistance = 700;
        splitInchirieri.IsSplitterFixed = false;
        
        dgvInchirieri = new DataGridView();
        dgvInchirieri.Dock = DockStyle.Fill;
        dgvInchirieri.ReadOnly = true;
        dgvInchirieri.AllowUserToAddRows = false;
        dgvInchirieri.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        dgvInchirieri.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        splitInchirieri.Panel1.Controls.Add(dgvInchirieri);
        
        var panelInchirieriRight = new Panel();
        panelInchirieriRight.Dock = DockStyle.Fill;
        panelInchirieriRight.AutoScroll = true;
        
        grpCreeazaInchiriere = new GroupBox()
            {Text = "Creează Închiriere", Location = new Point(10, 10), Size = new Size(280, 215)};
        grpCreeazaInchiriere.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numCreeazaCarId = new NumericUpDown() {Location = new Point(15, 30), Minimum = 1,Maximum=1000000, Width = 240};
        numCreeazaCarId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numCreeazaClientId = new NumericUpDown() {Location = new Point(15, 65), Minimum = 1, Maximum=100000, Width = 240};
        numCreeazaClientId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        dtpStartDate = new DateTimePicker() {Location = new Point(15, 100), Width = 240};
        dtpStartDate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numDays = new NumericUpDown() {Location = new Point(15, 135), Minimum = 1, Maximum = 365, Width = 240};
        numDays.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnCreeazaInchiriere = new Button() {Text = "Creează", Location = new Point(15, 170), Width = 240, Height = 35};
        btnCreeazaInchiriere.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpCreeazaInchiriere.Controls.Add(numCreeazaCarId);
        grpCreeazaInchiriere.Controls.Add(numCreeazaClientId);
        grpCreeazaInchiriere.Controls.Add(dtpStartDate);
        grpCreeazaInchiriere.Controls.Add(numDays);
        grpCreeazaInchiriere.Controls.Add(btnCreeazaInchiriere);
        
        grpReturnare = new GroupBox()
            {Text = "Returnare", Location = new Point(10, 235), Size = new Size(280, 100)};
        grpReturnare.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numRentalId = new NumericUpDown() {Location = new Point(15, 30), Minimum = 1,Maximum=1000000, Width = 240};
        numRentalId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnReturnare = new Button() { Text = "Returnare", Location = new Point(15, 65), Width = 240 };
        btnReturnare.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpReturnare.Controls.Add(numRentalId);
        grpReturnare.Controls.Add(btnReturnare);

    
        grpInchirieriClient = new GroupBox()
            {Text = "Închirieri Client", Location = new Point(10, 345), Size = new Size(280, 100)};
        grpInchirieriClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numInchirieriClientId = new NumericUpDown() {Location = new Point(15, 30), Minimum = 1,Maximum=1000000, Width = 240};
        numInchirieriClientId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        btnInchirieriClient = new Button() {Text = "Afișează", Location = new Point(15, 65), Width = 240};
        btnInchirieriClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpInchirieriClient.Controls.Add(numInchirieriClientId);
        grpInchirieriClient.Controls.Add(btnInchirieriClient);

       
        grpZileRamase = new GroupBox() 
            {Text = "Zile Rămase Închiriere", Location = new Point(10, 455),Size = new Size(280, 100)};
        grpZileRamase.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        numZileRentalId = new NumericUpDown() {Location = new Point(15, 30), Minimum = 1,Maximum=1000000, Width = 130};
        btnZileRamase = new Button() {Text = "Calculează", Location = new Point(155, 30), Width = 100};
        btnZileRamase.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        
         grpZileRamase.Controls.Add(numZileRentalId);
         grpZileRamase.Controls.Add(btnZileRamase);
    
        btnAfisareInchirieriActive = new Button() 
            { Text = "Afișează închirieri active", Location = new Point(10, 565), Width = 280, Height = 35};
        btnAfisareInchirieriActive.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    
        lblInchirieriStatus = new Label() { Location = new Point(10, 610), AutoSize = true, Text = "-", MaximumSize = new Size(280, 0) };
        lblInchirieriStatus.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    
        panelInchirieriRight.Controls.Add(grpCreeazaInchiriere);
        panelInchirieriRight.Controls.Add(grpReturnare);
        panelInchirieriRight.Controls.Add(grpInchirieriClient);
        panelInchirieriRight.Controls.Add(grpZileRamase);
        panelInchirieriRight.Controls.Add(btnAfisareInchirieriActive);
        panelInchirieriRight.Controls.Add(lblInchirieriStatus);
        
        splitInchirieri.Panel2.Controls.Add(panelInchirieriRight);
        tabInchirieri.Controls.Add(splitInchirieri);
        }
        #endregion
}