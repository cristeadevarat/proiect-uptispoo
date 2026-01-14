using InchirieriMasini.Models;
namespace InchirieriMasini.Services;

public interface IRentalService
{
    IEnumerable<Rental> GetAllRentals(); //lista inchirieri
    IEnumerable<Rental> GetActiveRentals(); //lista inchirieri active

    Rental? GetById(int id); //gasire inchiriere dupa id
    Rental? GetByCarId(int carId); //gasire inchiriere activa dupa masina inchiriata
    IEnumerable<Rental> GetByClientId(int clientId); // gasire inchirieri active dupa client (pot fi mai multe)

    Rental CreateRental(int carId, int clientId, DateTime startDate, int days); //creeare inchiriere
    string CloseRental(int rentalId); //finalizare inchiriere (close)
    
    int GetDaysRemaining(int rentalId, DateTime today); //cate zile mai e valabila inchirierea 
    DateTime GetClosingDate(int rentalId); //la ce data expira inchirierea
    double? GetTotalPrice(int rentalId); //calculeaza pretul unei inchirieri
}