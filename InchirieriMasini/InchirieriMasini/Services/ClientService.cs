using InchirieriMasini.Models;
using InchirieriMasini.Common;
using InchirieriMasini.Persistence;
namespace InchirieriMasini.Services;

public class ClientService: IClientService
{
    private readonly List<Client> clients = new();
    private int nextIdClient=2000;

    public IEnumerable<Client> GetAllClients() => clients;

    public Client? GetById(int id)
    {
        foreach (var c in clients)
            if (c.GetId() == id) return c;
        return null;
    }

    public Client? GetByEmail(string email)
    {
        foreach (var c in clients)
            if (c.GetEmail() == email) return c;
        return null;
    }
    //AddClient pentru logica de baza. !!!! poate arunca exceptii. pentru UI folositi TryAddClient
    public Client AddClient(string name, string phone, string email)
    {
        var client = new Client(nextIdClient, name, phone, email);
        nextIdClient++;
        clients.Add(client);
        return client;
    }

    public string RemoveClient(int id)
    {
        var client = GetById(id);
        if (client is null) return "Clientul pe care doriti sa il stergeti nu exista.";
        clients.Remove(client);
        return "Clientul a fost eliminat cu succes.";
    }
    
    //TryAddClient nu arunca exceptii, returneaza Result (uita.te in fisier Common) - pentru UI si legare butoane
    public Result<Client> TryAddClient(string name, string phone, string email)
    {
        try
        {
            if (GetByEmail(email) != null)
                return new Result<Client>(false, "Exista deja un client cu acest email", null);

            var c = AddClient(name, phone, email);
            return new Result<Client>(true, "Client adaugat", c);
        }
        catch (Exception e)
        {
            return new Result<Client>(false, e.Message, null);
        }
    }
    
    
    

    public List<ClientDto> Export()
    {
        return clients.Select(c => new ClientDto
        {
            Id = c.GetId(),
            Name = c.GetName(),
            Phone = c.GetPhone(),
            Email = c.GetEmail()
        }).ToList();
    }

    public void Import(List<ClientDto> data, int nextId)
    {
        clients.Clear();
        foreach (var d in data)
        {
            var client = new Client(d.Id, d.Name, d.Phone, d.Email);
            clients.Add(client);
        }
        nextIdClient = nextId;
    }

    public int GetNextId() => nextIdClient;
}