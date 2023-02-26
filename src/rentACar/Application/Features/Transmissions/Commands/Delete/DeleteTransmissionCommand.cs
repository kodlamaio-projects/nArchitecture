using Application.Features.Transmissions.Constants;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Transmissions.Constants.TransmissionsOperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Transmissions.Commands.Delete;

public class DeleteTransmissionCommand : IRequest<DeletedTransmissionResponse>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Domain.Constants.OperationClaims.Admin, TransmissionsOperationClaims.Admin, Write, TransmissionsOperationClaims.Delete };

    public class DeleteTransmissionCommandHandler : IRequestHandler<DeleteTransmissionCommand, DeletedTransmissionResponse>
    {
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly IMapper _mapper;

        public DeleteTransmissionCommandHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<DeletedTransmissionResponse> Handle(DeleteTransmissionCommand request,
                                                         CancellationToken cancellationToken)
        {
            Transmission mappedTransmission = _mapper.Map<Transmission>(request);
            Transmission deletedTransmission = await _transmissionRepository.DeleteAsync(mappedTransmission);
            DeletedTransmissionResponse deletedTransmissionDto = _mapper.Map<DeletedTransmissionResponse>(deletedTransmission);
            return deletedTransmissionDto;
        }
    }
}