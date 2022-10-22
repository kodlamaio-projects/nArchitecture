using Core.Domain.Abstract;
using Core.Domain.Bases;

namespace Domain.Entities;

public class FindeksCreditRate : BaseEntity<int>, IEntity
{
    public int CustomerId { get; set; }
    public short Score { get; set; }

    public virtual Customer? Customer { get; set; }

    public FindeksCreditRate() { }

    public FindeksCreditRate(int id, int customerId, short score) : base(id)
    {
        CustomerId = customerId;
        Score = score;
    }
}