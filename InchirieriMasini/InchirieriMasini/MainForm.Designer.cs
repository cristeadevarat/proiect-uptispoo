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
    private TextBox txtTelefon;
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
        panelMasiniRight.Padding = new Padding(15);
        
        btnAfiseazaToate = new Button() { Text = "Afiseaza toate", Location = new Point(15, 15), Size = new Size(280, 40) };
        btnAfiseazaToate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        btnDisponibile = new Button() { Text = "Afiseaza masini disponibile", Location = new Point(15, 65), Size = new Size(280, 40) };
        btnDisponibile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpAdaugaMasina = new GroupBox() { Text = "Adauga Masina", Location = new Point(15, 125), Size = new Size(280, 290) };
        grpAdaugaMasina.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpAdaugaMasina.Padding = new Padding(15, 10, 15, 15);
        
        var lblBrand = new Label() { Text = "Brand:", Location = new Point(15, 30), AutoSize = true };
        txtBrand = new TextBox() { Location = new Point(15, 52), Width = 240, Height = 28, PlaceholderText = "Ex: Toyota" };
        txtBrand.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        var lblModel = new Label() { Text = "Model:", Location = new Point(15, 95), AutoSize = true };
        txtModel = new TextBox() { Location = new Point(15, 117), Width = 240, Height = 28, PlaceholderText = "Ex: Corolla" };
        txtModel.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        var lblYear = new Label() { Text = "An fabricație:", Location = new Point(15, 160), AutoSize = true };
        numYear = new NumericUpDown() { Location = new Point(15, 182), Width = 240, Height = 28, Minimum = 1990, Maximum = 2030 };
        numYear.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        var lblPrice = new Label() { Text = "Preț/zi (lei):", Location = new Point(15, 225), AutoSize = true };
        numPrice = new NumericUpDown() { Location = new Point(15, 247), Width = 240, Height = 28, DecimalPlaces = 2, Maximum = 10000 };
        numPrice.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        btnAdaugaMasina = new Button() { Text = "Adaugă Mașină", Location = new Point(15, 290), Width = 240, Height = 35 };
        btnAdaugaMasina.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpAdaugaMasina.Controls.Add(lblBrand);
        grpAdaugaMasina.Controls.Add(txtBrand);
        grpAdaugaMasina.Controls.Add(lblModel);
        grpAdaugaMasina.Controls.Add(txtModel);
        grpAdaugaMasina.Controls.Add(lblYear);
        grpAdaugaMasina.Controls.Add(numYear);
        grpAdaugaMasina.Controls.Add(lblPrice);
        grpAdaugaMasina.Controls.Add(numPrice);
        grpAdaugaMasina.Controls.Add(btnAdaugaMasina);
        
        grpCautaMasina = new GroupBox() { Text = "Caută după ID", Location = new Point(15, 445), Size = new Size(280, 125) };
        grpCautaMasina.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpCautaMasina.Padding = new Padding(15, 10, 15, 15);
        
        var lblSearchId = new Label() { Text = "ID mașină:", Location = new Point(15, 30), AutoSize = true };
        numSearchId = new NumericUpDown() { Location = new Point(15, 52), Width = 240, Height = 28, Minimum = 1, Maximum = 1000000 };
        numSearchId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        btnCauta = new Button() { Text = "Caută", Location = new Point(15, 93), Width = 240, Height = 35 };
        btnCauta.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpCautaMasina.Controls.Add(lblSearchId);
        grpCautaMasina.Controls.Add(numSearchId);
        grpCautaMasina.Controls.Add(btnCauta);
        
        lblStatus = new Label() { Location = new Point(15, 600), AutoSize = true, Text = "-", MaximumSize = new Size(280, 0), Padding = new Padding(0, 10, 0, 10) };
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
        panelClientiRight.Padding = new Padding(15);
        
        grpAdaugaClient = new GroupBox(){Text="Adauga Client",Location=new Point(15,15),Size=new Size(280,315)};
        grpAdaugaClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpAdaugaClient.Padding = new Padding(15, 10, 15, 15);
        
        var lblNume = new Label() { Text = "Nume:", Location = new Point(15, 30), AutoSize = true };
        txtNume = new TextBox() { Location = new Point(15, 52), Width = 240, Height = 28, PlaceholderText="Ex: Popescu"};
        txtNume.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        var lblPrenume = new Label() { Text = "Prenume:", Location = new Point(15, 95), AutoSize = true };
        txtPrenume = new TextBox() { Location = new Point(15, 117), Width = 240, Height = 28, PlaceholderText="Ex: Ion" };
        txtPrenume.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        var lblTelefon = new Label() { Text = "Telefon:", Location = new Point(15, 160), AutoSize = true };
        txtTelefon = new TextBox() { Location = new Point(15, 182), Width = 240, Height = 28, PlaceholderText="Ex: 0712345678" };
        txtTelefon.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        var lblEmail = new Label() { Text = "Email:", Location = new Point(15, 225), AutoSize = true };
        txtEmail = new TextBox() { Location = new Point(15, 247), Width = 240, Height = 28, PlaceholderText="Ex: ion@email.com" };
        txtEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        btnAdaugaClient = new Button() { Text = "Adaugă Client", Location = new Point(15, 290), Width = 240, Height = 35 };
        btnAdaugaClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        grpAdaugaClient.Controls.Add(lblNume);
        grpAdaugaClient.Controls.Add(txtNume);
        grpAdaugaClient.Controls.Add(lblPrenume);
        grpAdaugaClient.Controls.Add(txtPrenume);
        grpAdaugaClient.Controls.Add(lblTelefon);
        grpAdaugaClient.Controls.Add(txtTelefon);
        grpAdaugaClient.Controls.Add(lblEmail);
        grpAdaugaClient.Controls.Add(txtEmail);
        grpAdaugaClient.Controls.Add(btnAdaugaClient);

        
        grpStergeClient = new GroupBox() 
            {Text="Șterge Client",Location=new Point(15,360),Size=new Size(280,125)};
        grpStergeClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpStergeClient.Padding = new Padding(15, 10, 15, 15);
        
        var lblIdClient = new Label() { Text = "ID Client:", Location = new Point(15, 30), AutoSize = true };
        txtIdClient = new TextBox() { Location = new Point(15, 52), Width = 240, Height = 28, PlaceholderText="ID-ul clientului"};
        txtIdClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        btnStergeClient = new Button() { Text = "Șterge Client", Location = new Point(15, 93), Width = 240, Height = 35 };
        btnStergeClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;

        grpStergeClient.Controls.Add(lblIdClient);
        grpStergeClient.Controls.Add(txtIdClient);
        grpStergeClient.Controls.Add(btnStergeClient);

        
        grpCautaClientId = new GroupBox()
                   { Text = "Caută Client după ID", Location = new Point(15, 515), Size = new Size(280, 125)}; 
        grpCautaClientId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpCautaClientId.Padding = new Padding(15, 10, 15, 15);
        
        var lblNumClientId = new Label() { Text = "ID Client:", Location = new Point(15, 30), AutoSize = true };
        numClientId = new NumericUpDown() { Location = new Point(15, 52), Width=240, Height = 28, Minimum = 1,Maximum=1000000};
        numClientId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        btnCautaClientId = new Button() { Text = "Caută", Location = new Point(15, 93), Width = 240, Height = 35 };
        btnCautaClientId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpCautaClientId.Controls.Add(lblNumClientId);
        grpCautaClientId.Controls.Add(numClientId);
        grpCautaClientId.Controls.Add(btnCautaClientId);

        
        grpCautaClientEmail = new GroupBox()
            { Text = "Caută Client după Email", Location = new Point(15, 670), Size = new Size(280, 125) };
        grpCautaClientEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpCautaClientEmail.Padding = new Padding(15, 10, 15, 15);
        
        var lblSearchEmail = new Label() { Text = "Email:", Location = new Point(15, 30), AutoSize = true };
        txtSearchEmail = new TextBox() { Location = new Point(15, 52), Width = 240, Height = 28, PlaceholderText="email@domain.com" };
        txtSearchEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        btnCautaClientEmail = new Button() { Text = "Caută", Location = new Point(15, 93), Width = 240, Height = 35 };
        btnCautaClientEmail.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpCautaClientEmail.Controls.Add(lblSearchEmail);
        grpCautaClientEmail.Controls.Add(txtSearchEmail);
        grpCautaClientEmail.Controls.Add(btnCautaClientEmail);

        lblClientStatus = new Label() { Location = new Point(15, 825), AutoSize = true, Text = "-", MaximumSize = new Size(280, 0), Padding = new Padding(0, 10, 0, 10) };
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
        panelInchirieriRight.Padding = new Padding(15);
        
        grpCreeazaInchiriere = new GroupBox()
            {Text = "Creează Închiriere", Location = new Point(15, 15), Size = new Size(280, 370)};
        grpCreeazaInchiriere.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpCreeazaInchiriere.Padding = new Padding(15, 10, 15, 15);
        
        var lblCarId = new Label() { Text = "ID Mașină:", Location = new Point(15, 30), AutoSize = true };
        numCreeazaCarId = new NumericUpDown() {Location = new Point(15, 52), Minimum = 1, Maximum=1000000, Width = 240, Height = 28};
        numCreeazaCarId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        var lblClientId = new Label() { Text = "ID Client:", Location = new Point(15, 100), AutoSize = true };
        numCreeazaClientId = new NumericUpDown() {Location = new Point(15, 122), Minimum = 1, Maximum=100000, Width = 240, Height = 28};
        numCreeazaClientId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        var lblStartDate = new Label() { Text = "Data început:", Location = new Point(15, 170), AutoSize = true };
        dtpStartDate = new DateTimePicker() {Location = new Point(15, 192), Width = 240, Height = 28, Format = DateTimePickerFormat.Short};
        dtpStartDate.MaxDate = DateTime.Today;
        dtpStartDate.Value = DateTime.Today;
        dtpStartDate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        var lblDays = new Label() { Text = "Număr zile:", Location = new Point(15, 240), AutoSize = true };
        numDays = new NumericUpDown() {Location = new Point(15, 262), Minimum = 1, Maximum = 365, Width = 240, Height = 28};
        numDays.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        btnCreeazaInchiriere = new Button() {Text = "Creează Închiriere", Location = new Point(15, 310), Width = 240, Height = 40};
        btnCreeazaInchiriere.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpCreeazaInchiriere.Controls.Add(lblCarId);
        grpCreeazaInchiriere.Controls.Add(numCreeazaCarId);
        grpCreeazaInchiriere.Controls.Add(lblClientId);
        grpCreeazaInchiriere.Controls.Add(numCreeazaClientId);
        grpCreeazaInchiriere.Controls.Add(lblStartDate);
        grpCreeazaInchiriere.Controls.Add(dtpStartDate);
        grpCreeazaInchiriere.Controls.Add(lblDays);
        grpCreeazaInchiriere.Controls.Add(numDays);
        grpCreeazaInchiriere.Controls.Add(btnCreeazaInchiriere);
        
        grpReturnare = new GroupBox()
            {Text = "Returnare Mașină", Location = new Point(15, 425), Size = new Size(280, 135)};
        grpReturnare.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpReturnare.Padding = new Padding(15, 10, 15, 15);
        
        var lblRentalId = new Label() { Text = "ID Închiriere:", Location = new Point(15, 30), AutoSize = true };
        numRentalId = new NumericUpDown() {Location = new Point(15, 52), Minimum = 1, Maximum=1000000, Width = 240, Height = 28};
        numRentalId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        btnReturnare = new Button() { Text = "Returnare Mașină", Location = new Point(15, 100), Width = 240, Height = 35 };
        btnReturnare.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpReturnare.Controls.Add(lblRentalId);
        grpReturnare.Controls.Add(numRentalId);
        grpReturnare.Controls.Add(btnReturnare);

    
        grpInchirieriClient = new GroupBox()
            {Text = "Închirieri Client", Location = new Point(15, 600), Size = new Size(280, 135)};
        grpInchirieriClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpInchirieriClient.Padding = new Padding(15, 10, 15, 15);
        
        var lblInchClientId = new Label() { Text = "ID Client:", Location = new Point(15, 30), AutoSize = true };
        numInchirieriClientId = new NumericUpDown() {Location = new Point(15, 52), Minimum = 1, Maximum=1000000, Width = 240, Height = 28};
        numInchirieriClientId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        btnInchirieriClient = new Button() {Text = "Afișează Închirieri", Location = new Point(15, 100), Width = 240, Height = 35};
        btnInchirieriClient.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        grpInchirieriClient.Controls.Add(lblInchClientId);
        grpInchirieriClient.Controls.Add(numInchirieriClientId);
        grpInchirieriClient.Controls.Add(btnInchirieriClient);

       
        grpZileRamase = new GroupBox() 
            {Text = "Zile Rămase Închiriere", Location = new Point(15, 775), Size = new Size(280, 135)};
        grpZileRamase.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        grpZileRamase.Padding = new Padding(15, 10, 15, 15);
        
        var lblZileRentalId = new Label() { Text = "ID Închiriere:", Location = new Point(15, 30), AutoSize = true };
        numZileRentalId = new NumericUpDown() {Location = new Point(15, 52), Minimum = 1, Maximum=1000000, Width = 240, Height = 28};
        numZileRentalId.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
        btnZileRamase = new Button() {Text = "Calculează Zile", Location = new Point(15, 100), Width = 240, Height = 35};
        btnZileRamase.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        
         grpZileRamase.Controls.Add(lblZileRentalId);
         grpZileRamase.Controls.Add(numZileRentalId);
         grpZileRamase.Controls.Add(btnZileRamase);
    
        btnAfisareInchirieriActive = new Button() 
            { Text = "Afișează Închirieri Active", Location = new Point(15, 950), Width = 280, Height = 40};
        btnAfisareInchirieriActive.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    
        lblInchirieriStatus = new Label() { Location = new Point(15, 1030), AutoSize = true, Text = "-", MaximumSize = new Size(280, 0), Padding = new Padding(0, 10, 0, 10) };
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