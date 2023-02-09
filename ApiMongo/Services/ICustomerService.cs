using ApiMongo.Models;
using MongoDB.Bson;
using System.Collections.Generic;

namespace ApiMongo.Services;

public interface ICustomerService
{
    long CreateCustomer(Customer customer);
    IEnumerable<Customer> GetAll();
    Customer GetById(long id);
    void Update(long id, Customer customer);
    void Delete(long id);
}