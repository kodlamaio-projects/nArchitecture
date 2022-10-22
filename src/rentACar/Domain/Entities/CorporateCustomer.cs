using Core.Domain.Abstract;
using Core.Domain.Bases;

namespace Domain.Entities;

public class CorporateCustomer : BaseEntity<int>, IEntity
{
    public int CustomerId { get; set; }
    public string CompanyName { get; set; }
    public string TaxNo { get; set; }

    public virtual Customer Customer { get; set; }

    public CorporateCustomer() { }

    public CorporateCustomer(int id, int customerId, string companyName, string taxNo) : base(id)
    {
        CustomerId = customerId;
        CompanyName = companyName;
        TaxNo = taxNo;
    }
}