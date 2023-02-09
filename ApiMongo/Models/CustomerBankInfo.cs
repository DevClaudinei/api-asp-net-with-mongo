//using System;

//namespace ApiMongo.Models;

//public class CustomerBankInfo : IEntity
//{
//    public CustomerBankInfo(long customerId)
//    {
//        CustomerId = customerId;
//        Account = Guid.NewGuid().ToString();
//    }

//    public long Id { get; set; }
//    public string Account { get; set; }
//    public decimal AccountBalance { get; set; } = 0;
//    public virtual Customer Customer { get; set; }
//    public long CustomerId { get; set; }
//    public DateTime CreatedAt { get; set; }
//    public DateTime? ModifiedAt { get; set; }
//}