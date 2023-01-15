using PluralizeService.Core;

namespace Generator.Codes.CreatorCodes.Feature;

internal class CommandCreator : ICreatorCode
{
    public CommandCreator(Type type)
    {
        Type = type;
    }

    public Type Type { get; set; }


    public string CreateEntitiyCommand()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        return
$@"// this file was created automatically.
using Application.Features.{plural}.Dtos;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.{plural}.Commands.Create{Type.Name};

public class Create{Type.Name}Command : IRequest<Created{Type.Name}Dto>, ISecuredRequest
{{

    public string[] Roles => new[] {{ """" }};
}}
";
    }

    public string CreateEntitiyCommandHandler()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        return
$@"// this file was created automatically.
using Application.Features.{plural}.Dtos;
using Application.Features.{plural}.Rules;
using Application.Repositories;
using AutoMapper;
using {Type.Namespace};
using MediatR;

namespace Application.Features.{plural}.Commands.Create{Type.Name};
 
public class Create{Type.Name}CommandHandler : IRequestHandler<Create{Type.Name}Command, Created{Type.Name}Dto>
{{
    private readonly I{Type.Name}Repository _{Type.Name.ToLower()}Repository;
    private readonly IMapper _mapper;
    private readonly {Type.Name}BusinessRules _{Type.Name.ToLower()}BusinessRules;

    public Create{Type.Name}CommandHandler(I{Type.Name}Repository {Type.Name.ToLower()}Repository, IMapper mapper,
                                        {Type.Name}BusinessRules {Type.Name.ToLower()}BusinessRules)
    {{
        _{Type.Name.ToLower()}Repository = {Type.Name.ToLower()}Repository;
        _mapper = mapper;
        _{Type.Name.ToLower()}BusinessRules = {Type.Name.ToLower()}BusinessRules;
    }}

    public async Task<Created{Type.Name}Dto> Handle(Create{Type.Name}Command request, CancellationToken cancellationToken)
    {{
        // await _{Type.Name.ToLower()}BusinessRules.{Type.Name}NameCanNotBeDuplicatedWhenInserted(request.Name);

        {Type.Name} mapped{Type.Name} = _mapper.Map<{Type.Name}>(request);
        {Type.Name} created{Type.Name} = await _{Type.Name.ToLower()}Repository.AddAsync(mapped{Type.Name});
        Created{Type.Name}Dto created{Type.Name}Dto = _mapper.Map<Created{Type.Name}Dto>(created{Type.Name});
        return created{Type.Name}Dto;
    }}
}}
";
    }

    public string CreateEntitiyCommandValidator()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);
        return
$@"// this file was created automatically.
using FluentValidation;

namespace Application.Features.{plural}.Commands.Create{Type.Name};

public class Create{Type.Name}CommandValidator : AbstractValidator<Create{Type.Name}Command>
{{
    public Create{Type.Name}CommandValidator()
    {{
    }}
}}
";
    }

    public string DeleteEntitiyCommand()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        return
$@"// this file was created automatically.
using Application.Features.{plural}.Dtos;
using Core.Application.Pipelines.Authorization;
using MediatR;

using Core.Application.Pipelines.Authorization;

namespace Application.Features.{plural}.Commands.Delete{Type.Name};

public class Delete{Type.Name}Command : IRequest<Deleted{Type.Name}Dto>, ISecuredRequest
{{

    public string[] Roles =>new[] {{""""}};
}}
";
    }

    public string DeleteEntitiyCommandHandler()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        return
$@"// this file was created automatically.
using Application.Features.{plural}.Dtos;
using Application.Features.{plural}.Rules;
using Application.Repositories;
using AutoMapper;
using {Type.Namespace};
using MediatR;

namespace Application.Features.{plural}.Commands.Delete{Type.Name};
 
public class Delete{Type.Name}CommandHandler : IRequestHandler<Delete{Type.Name}Command, Deleted{Type.Name}Dto>
{{
    private readonly I{Type.Name}Repository _{Type.Name.ToLower()}Repository;
    private readonly IMapper _mapper;
    private readonly {Type.Name}BusinessRules _{Type.Name.ToLower()}BusinessRules;

    public Delete{Type.Name}CommandHandler(I{Type.Name}Repository {Type.Name.ToLower()}Repository, IMapper mapper,
                                        {Type.Name}BusinessRules {Type.Name.ToLower()}BusinessRules)
    {{
        _{Type.Name.ToLower()}Repository = {Type.Name.ToLower()}Repository;
        _mapper = mapper;
        _{Type.Name.ToLower()}BusinessRules = {Type.Name.ToLower()}BusinessRules;
    }}

    public async Task<Deleted{Type.Name}Dto> Handle(Delete{Type.Name}Command request, CancellationToken cancellationToken)
    {{
        // await _{Type.Name.ToLower()}BusinessRules.{Type.Name}NameCanNotBeDuplicatedWhenInserted(request.Name);

        {Type.Name} mapped{Type.Name} = _mapper.Map<{Type.Name}>(request);
        {Type.Name} created{Type.Name} = await _{Type.Name.ToLower()}Repository.AddAsync(mapped{Type.Name});
        Deleted{Type.Name}Dto created{Type.Name}Dto = _mapper.Map<Deleted{Type.Name}Dto>(created{Type.Name});
        return created{Type.Name}Dto;
    }}
}}
";
    }

    public string DeleteEntitiyCommandValidator()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);
        return
$@"// this file was created automatically.
using FluentValidation;

namespace Application.Features.{plural}.Commands.Delete{Type.Name};

public class Delete{Type.Name}CommandValidator : AbstractValidator<Delete{Type.Name}Command>
{{
    public Delete{Type.Name}CommandValidator()
    {{
    }}
}}
";
    }

    public string UpdateEntitiyCommand()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        return
$@"// this file was created automatically.
using Application.Features.{plural}.Dtos;
using Core.Application.Pipelines.Authorization;
using MediatR;

namespace Application.Features.{plural}.Commands.Update{Type.Name};

public class Update{Type.Name}Command : IRequest<Updated{Type.Name}Dto>, ISecuredRequest
{{

    public string[] Roles =>new[] {{""""}};
}}
";
    }

    public string UpdateEntitiyCommandHandler()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        return
$@"// this file was created automatically.
using Application.Features.{plural}.Dtos;
using Application.Features.{plural}.Rules;
using Application.Repositories;
using AutoMapper;
using {Type.Namespace};
using MediatR;

namespace Application.Features.{plural}.Commands.Update{Type.Name};
 
public class Update{Type.Name}CommandHandler : IRequestHandler<Update{Type.Name}Command, Updated{Type.Name}Dto>
{{
    private readonly I{Type.Name}Repository _{Type.Name.ToLower()}Repository;
    private readonly IMapper _mapper;
    private readonly {Type.Name}BusinessRules _{Type.Name.ToLower()}BusinessRules;

    public Update{Type.Name}CommandHandler(I{Type.Name}Repository {Type.Name.ToLower()}Repository, IMapper mapper,
                                        {Type.Name}BusinessRules {Type.Name.ToLower()}BusinessRules)
    {{
        _{Type.Name.ToLower()}Repository = {Type.Name.ToLower()}Repository;
        _mapper = mapper;
        _{Type.Name.ToLower()}BusinessRules = {Type.Name.ToLower()}BusinessRules;
    }}

    public async Task<Updated{Type.Name}Dto> Handle(Update{Type.Name}Command request, CancellationToken cancellationToken)
    {{
        // await _{Type.Name.ToLower()}BusinessRules.{Type.Name}NameCanNotBeDuplicatedWhenInserted(request.Name);

        {Type.Name} mapped{Type.Name} = _mapper.Map<{Type.Name}>(request);
        {Type.Name} created{Type.Name} = await _{Type.Name.ToLower()}Repository.AddAsync(mapped{Type.Name});
        Updated{Type.Name}Dto created{Type.Name}Dto = _mapper.Map<Updated{Type.Name}Dto>(created{Type.Name});
        return created{Type.Name}Dto;
    }}
}}
";
    }

    public string UpdateEntitiyCommandValidator()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);
        return
$@"// this file was created automatically.
using FluentValidation;

namespace Application.Features.{plural}.Commands.Update{Type.Name};

public class Update{Type.Name}CommandValidator : AbstractValidator<Update{Type.Name}Command>
{{
    public Update{Type.Name}CommandValidator()
    {{
    }}
}}
";
    }
}