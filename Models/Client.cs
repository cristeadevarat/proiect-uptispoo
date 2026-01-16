namespace InchirieriMasini.Models;

public class Client
{
    private int Id;
    private string Name;
    private string Phone;
    private string Email;

    public Client(int id, string name, string phone, string email)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nume invalid");
        if (string.IsNullOrWhiteSpace(phone)) throw new ArgumentException("Telefon invalid");
        if (string.IsNullOrWhiteSpace(email)) throw new ArgumentException("Email invalid");
        
        Id = id;
        Name = name;
        Phone = phone;
        Email = email;
    }

    public int GetId() => Id;
    public string GetName() => Name;
    public string GetPhone() => Phone;
    public string GetEmail() => Email;

    public override string ToString()
    {
        return $"{Id} - {Name}: {Phone}, {Email}";
    }
        
}