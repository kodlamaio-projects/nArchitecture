using Application.Features.Transmissions.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Transmissions.Constants.TransmissionsOperationClaims;

namespace Application.Features.Transmissions.Commands.Update;

public class UpdateTransmissionCommand : IRequest<UpdatedTransmissionResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, TransmissionsOperationClaims.Update };

    public class UpdateTransmissionCommandHandler : IRequestHandler<UpdateTransmissionCommand, UpdatedTransmissionResponse>
    {
        private ITransmissionRepository _transmissionRepository { get; }
        private IMapper _mapper { get; }

        public UpdateTransmissionCommandHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<UpdatedTransmissionResponse> Handle(UpdateTransmissionCommand request, CancellationToken cancellationToken)
        {
            Transmission mappedTransmission = _mapper.Map<Transmission>(request);
            Transmission updatedTransmission = await _transmissionRepository.UpdateAsync(mappedTransmission);
            UpdatedTransmissionResponse updatedTransmissionDto = _mapper.Map<UpdatedTransmissionResponse>(updatedTransmission);
            return updatedTransmissionDto;
        }
    }
}
