using ApiMongo.Infraestructure.Data.Context;
using ApiMongo.Models;
using DomainServices.Exceptions;
using MongoDB.UnitOfWork;
using System.Collections.Generic;

namespace ApiMongo.Services;

public class CustomerService : ICustomerService
{
    private readonly IMongoDbUnitOfWork _unitOfWork;
    private readonly IMongoDbRepositoryFactory _repositoryFactory;

    public CustomerService(IMongoDbUnitOfWork<ApplicationDbContext> unitOfWork, IMongoDbRepositoryFactory<ApplicationDbContext> repositoryFactory)
    {
        _unitOfWork = unitOfWork ?? throw new System.ArgumentNullException(nameof(unitOfWork));
        _repositoryFactory = repositoryFactory ?? throw new System.ArgumentNullException(nameof(repositoryFactory));
    }

    public long CreateCustomer(Customer customer)
    {
        var unitOfWork = _unitOfWork.Repository<Customer>();
        
        VerifyCustomerAlreadyExists(customer);
        //customer.CustomerBankInfo = CreateCustomerBankInfo(customer.Id);

        unitOfWork.InsertOne(customer);
        _unitOfWork.SaveChanges();

        return customer.Id;
    }

    private void VerifyCustomerAlreadyExists(Customer customer)
    {
        var repository = _repositoryFactory.Repository<Customer>();

        if (repository.Any(x => x.Email.Equals(customer.Email)))
            throw new BadRequestException($"Customer already exists for email: {customer.Email}");

        if (repository.Any(x => x.Cpf.Equals(customer.Cpf)))
            throw new BadRequestException($"Customer already exists for CPF: {customer.Cpf}");
    }

    public void Delete(long id)
    {
        var repository = _unitOfWork.Repository<Customer>();

        repository.DeleteOne(x => x.Id == id);

        _unitOfWork.SaveChanges();
    }

    public IEnumerable<Customer> GetAll()
    {
        var repository = _unitOfWork.Repository<Customer>();

        var query = repository.MultipleResultQuery();

        var customerFound = repository.Search(query);

        return customerFound;
    }

    public Customer GetById(long id)
    {
        var repository = _unitOfWork.Repository<Customer>();

        var query = repository.SingleResultQuery()
                              .AndFilter(x => x.Id == id);

        var customerResult = repository.SingleOrDefault(query);

        return customerResult;
    }

    public void Update(long id, Customer customer)
    {
        var repository = _unitOfWork.Repository<Customer>();

        VerifyCustomerAlreadyExists(customer);

        customer.Id = id;

        repository.ReplaceOne(x => x.Id == id, customer);

        _unitOfWork.SaveChanges();
    }
}