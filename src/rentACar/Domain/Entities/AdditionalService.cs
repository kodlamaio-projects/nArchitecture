using Core.Domain.Abstract;
using Core.Domain.Bases;

namespace Domain.Entities;

public class AdditionalService : BaseEntity<int>, IEntity
{
    public decimal DailyPrice { get; set; }

    public AdditionalService() { }

    public AdditionalService(int id, string name, decimal dailyPrice) : base(id)
    {
        Name = name;
        DailyPrice = dailyPrice;
    }
}