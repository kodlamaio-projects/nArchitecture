using Application.Features.Transmissions.Dtos;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Domain.Entities;
using MediatR;
using static Application.Features.Transmissions.Constants.OperationClaims;
using static Domain.Constants.OperationClaims;

namespace Application.Features.Transmissions.Commands.DeleteTransmission;

public class DeleteTransmissionCommand : IRequest<DeletedTransmissionDto>, ISecuredRequest
{
    public int Id { get; set; }

    public string[] Roles => new[] { Admin, TransmissionDelete };

    public class DeleteTransmissionCommandHandler : IRequestHandler<DeleteTransmissionCommand, DeletedTransmissionDto>
    {
        private readonly ITransmissionRepository _transmissionRepository;
        private readonly IMapper _mapper;

        public DeleteTransmissionCommandHandler(ITransmissionRepository transmissionRepository, IMapper mapper)
        {
            _transmissionRepository = transmissionRepository;
            _mapper = mapper;
        }

        public async Task<DeletedTransmissionDto> Handle(DeleteTransmissionCommand request,
                                                         CancellationToken cancellationToken)
        {
            Transmission mappedTransmission = _mapper.Map<Transmission>(request);
            Transmission deletedTransmission = await _transmissionRepository.DeleteAsync(mappedTransmission);
            DeletedTransmissionDto deletedTransmissionDto = _mapper.Map<DeletedTransmissionDto>(deletedTransmission);
            return deletedTransmissionDto;
        }
    }
}