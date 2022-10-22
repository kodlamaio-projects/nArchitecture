using Core.Domain.Abstract;
using Core.Domain.Bases;

namespace Core.Domain.Concrete.Security.Entities;

public class OperationClaim : BaseEntity<int>, IEntity
{
    public OperationClaim() { }

    public OperationClaim(int id, string name) : base(id)
    {
        Name = name;
    }
}