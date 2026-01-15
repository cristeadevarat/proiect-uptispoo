using InchirieriMasini.Models;
namespace InchirieriMasini.Services;

public interface IClientService
{
    IEnumerable<Client> GetAllClients(); //returneaza lista cu toti clientii
    
    Client? GetById(int id); //gaseste client dupa id
    
    Client? GetByEmail(string email); //gaseste client dup email
    
    Client AddClient(string name, string phone, string email);
    
    string RemoveClient(int id);
}