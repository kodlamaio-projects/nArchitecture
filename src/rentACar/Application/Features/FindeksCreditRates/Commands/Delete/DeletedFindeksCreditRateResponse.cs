using Core.Application.Dtos;

namespace Application.Features.FindeksCreditRates.Commands.Delete;

public class DeletedFindeksCreditRateResponse : IDto
{
    public int Id { get; set; }
}