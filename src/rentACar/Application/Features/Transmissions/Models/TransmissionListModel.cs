using Application.Features.Transmissions.Dtos;
using Core.Persistence.Paging;

namespace Application.Features.Transmissions.Models;

public class TransmissionListModel : BasePageableModel
{
    public IList<TransmissionListDto> Items { get; set; }
}