using Core.Persistence.Repositories;

namespace Domain.Entities;

public class CorporateCustomer : Entity
{
    public int CustomerId { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }

    public virtual Customer Customer { get; set; }

    public CorporateCustomer()
    {
    }

    public CorporateCustomer(int id, int customerId, string companyName, string taxNo) : base(id)
    {
        CustomerId = customerId;
        CompanyName = companyName;
        TaxNo = taxNo;
    }
}