using InchirieriMasini.Models;
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
}