namespace InchirieriMasini.Models;

/// <summary>
/// Customer entity with encapsulation
/// </summary>
public class Customer
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string DriverLicenseNumber { get; set; } = string.Empty;
    public DateTime RegistrationDate { get; set; }

    // Encapsulation: computed property
    public string FullName => $"{FirstName} {LastName}";

    public override string ToString()
    {
        return $"{FullName} ({Email})";
    }
}
