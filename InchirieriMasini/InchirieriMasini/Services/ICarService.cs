using InchirieriMasini.Models;
namespace InchirieriMasini.Services;

public interface ICarService
{
    IEnumerable<Car> GetAllCars(); //returneaza toate masinile intr.o colectie (lista)
    IEnumerable<Car> GetAvailableCars(); //returneaza doar masinile disponibile - lista
    Car? GetById(int id); //gaseste masina dupa id
    Car AddCar(string brand, string model, int year, double price); //adauga o noua masina
    string RemoveCar(int id); //sterge masina din lista
    string MarkRented(int id); //marcheaza o masina ca inchiriata
    string MarkReturned(int id); // returneaza o masina
}