using InchirieriMasini.Infrastructure;
using InchirieriMasini.Interfaces;
using InchirieriMasini.Models;
using Microsoft.Extensions.Logging;

namespace InchirieriMasini.Services;

/// <summary>
/// Service for managing customers
/// </summary>
public class CustomerService
{
    private readonly IFileStorage _fileStorage;
    private readonly ILogger<CustomerService> _logger;
    private readonly string _dataFilePath;
    private ApplicationState _state;

    public CustomerService(IFileStorage fileStorage, ILogger<CustomerService> logger)
    {
        _fileStorage = fileStorage;
        _logger = logger;
        _dataFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data", "app_state.json");
        _state = LoadState();
    }

    private ApplicationState LoadState()
    {
        try
        {
            var state = _fileStorage.Load<ApplicationState>(_dataFilePath);
            return state ?? new ApplicationState();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error loading application state");
            return new ApplicationState();
        }
    }

    private void SaveState()
    {
        try
        {
            _fileStorage.Save(_dataFilePath, _state);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving application state");
            throw;
        }
    }

    public void AddCustomer(Customer customer)
    {
        _logger.LogInformation("Adding customer: {FullName}", customer.FullName);
        customer.Id = _state.NextCustomerId++;
        customer.RegistrationDate = DateTime.Now;
        _state.Customers.Add(customer);
        SaveState();
    }

    public void UpdateCustomer(Customer customer)
    {
        _logger.LogInformation("Updating customer with ID: {Id}", customer.Id);
        var index = _state.Customers.FindIndex(c => c.Id == customer.Id);
        if (index >= 0)
        {
            _state.Customers[index] = customer;
            SaveState();
        }
        else
        {
            throw new InvalidOperationException($"Customer with ID {customer.Id} not found");
        }
    }

    public void DeleteCustomer(int id)
    {
        _logger.LogInformation("Deleting customer with ID: {Id}", id);
        var customer = _state.Customers.FirstOrDefault(c => c.Id == id);
        if (customer != null)
        {
            _state.Customers.Remove(customer);
            SaveState();
        }
    }

    public Customer? GetCustomerById(int id)
    {
        return _state.Customers.FirstOrDefault(c => c.Id == id);
    }

    public IEnumerable<Customer> GetAllCustomers()
    {
        // LINQ operation
        return _state.Customers.OrderBy(c => c.LastName).ThenBy(c => c.FirstName);
    }

    public IEnumerable<Customer> SearchCustomers(string searchTerm)
    {
        // LINQ operation with complex filtering
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            return Enumerable.Empty<Customer>();
        }

        return _state.Customers
            .Where(c => c.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                       c.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                       c.Email.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
            .OrderBy(c => c.LastName);
    }

    public ApplicationState GetState() => _state;
}
