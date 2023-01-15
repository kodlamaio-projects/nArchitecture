using PluralizeService.Core;

namespace Generator.Codes.CreatorCodes.Feature;

internal class QueryCreator : ICreatorCode
{
    public QueryCreator(Type type)
    {
        Type = type;
    }

    public Type Type { get; set; }

    public string GetByIdEntityQuery()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        return
            $@"// this file was created automatically.
using Application.Features.{plural}.Dtos;
using MediatR;

namespace Application.Features.{plural}.Queries.GetById{Type.Name};

public class GetById{Type.Name}Query : IRequest<{Type.Name}Dto>
{{

}}
";
    }

    public string GetByIdEntityQueryHandler()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        string letter = Type.Name[0].ToString().ToLower();
        return
            $@"// this file was created automatically.
using Application.Features.{plural}.Models;
using Application.Repositories;
using AutoMapper;
using {Type.Namespace};
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.{plural}.Queries.GetById{Type.Name};

public class GetById{Type.Name}QueryHandler : IRequestHandler<GetById{Type.Name}Query, {Type.Name}Dto>
{{
    private readonly I{Type.Name}Repository _{Type.Name.ToLower()}Repository;
    private readonly IMapper _mapper;
    private readonly {Type.Name}BusinessRules _{Type.Name.ToLower()}BusinessRules;

    public GetById{Type.Name}QueryHandler(I{Type.Name}Repository {Type.Name.ToLower()}Repository,{Type.Name}BusinessRules {Type.Name.ToLower()}BusinessRules, IMapper mapper)
    {{
        _{Type.Name.ToLower()}Repository = {Type.Name.ToLower()}Repository;
        _{Type.Name.ToLower()}BusinessRules = {Type.Name.ToLower()}BusinessRules;
        _mapper = mapper;
    }}

    public async Task<{Type.Name}Dto> Handle(GetById{Type.Name}Query request, CancellationToken cancellationToken)
    {{
        // await _{Type.Name.ToLower()}BusinessRules.{Type.Name}IdShouldExistWhenSelected(request.Id);

        {Type.Name}? {Type.Name.ToLower()} = await _{Type.Name.ToLower()}Repository.GetAsync({letter} => {letter} == {letter} );
            
        {Type.Name}Dto {Type.Name.ToLower()}Dto = _mapper.Map<{Type.Name}Dto>({Type.Name.ToLower()});
        return {Type.Name.ToLower()}Dto;
    }}
}}
";
    }

    public string GetListEntityQuery()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        return
            $@"// this file was created automatically.
using Application.Features.{plural}.Models;
using Core.Application.Requests;
using MediatR;

namespace Application.Features.{plural}.Queries.GetList{Type.Name};

public class GetList{Type.Name}Query : IRequest<{Type.Name}ListModel>
{{
    public PageRequest PageRequest {{ get; set; }}
}}
";
    }

    public string GetListEntityQueryHandler()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        return
            $@"// this file was created automatically.
using Application.Features.{plural}.Models;
using Application.Repositories;
using AutoMapper;
using {Type.Namespace};
using Core.Persistence.Paging;
using MediatR;

namespace Application.Features.{plural}.Queries.GetList{Type.Name};

public class GetList{Type.Name}QueryHandler : IRequestHandler<GetList{Type.Name}Query, {Type.Name}ListModel>
{{
    private readonly I{Type.Name}Repository _{Type.Name.ToLower()}Repository;
    private readonly IMapper _mapper;

    public GetList{Type.Name}QueryHandler(I{Type.Name}Repository {Type.Name.ToLower()}Repository, IMapper mapper)
    {{
        _{Type.Name.ToLower()}Repository = {Type.Name.ToLower()}Repository;
        _mapper = mapper;
    }}

    public async Task<{Type.Name}ListModel> Handle(GetList{Type.Name}Query request, CancellationToken cancellationToken)
    {{
        IPaginate<{Type.Name}> {plural.ToLower()} = await _{Type.Name.ToLower()}Repository.GetListAsync(index: request.PageRequest.Page,
                                                                        size: request.PageRequest.PageSize);
        {Type.Name}ListModel mapped{Type.Name}ListModel = _mapper.Map<{Type.Name}ListModel>({plural.ToLower()});
        return mapped{Type.Name}ListModel;
    }}
}}
";
    }

    public string GetListEntityByDynamicQuery()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        return
            $@"// this file was created automatically.
using Application.Features.{plural}.Models;
using Core.Application.Requests;
using Core.Persistence.Dynamic;
using MediatR;

namespace Application.Features.{plural}.Queries.GetList{Type.Name}ByDynamic;

public class GetList{Type.Name}ByDynamicQuery : IRequest<{Type.Name}ListModel>
{{
    public PageRequest PageRequest {{ get; set; }}
    public Dynamic Dynamic {{ get; set; }}
}}
";
    }

    public string GetListEntityByDynamicQueryHandler()
    {
        string plural = PluralizationProvider.Pluralize(Type.Name);

        string letter = Type.Name[0].ToString().ToLower();
        return
            $@"// this file was created automatically.
using Application.Features.{plural}.Models;
using Application.Repositories;
using AutoMapper;
using {Type.Namespace};
using Core.Persistence.Paging;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.{plural}.Queries.GetList{Type.Name}ByDynamic;

public class GetList{Type.Name}ByDynamicQueryHandler : IRequestHandler<GetList{Type.Name}ByDynamicQuery, {Type.Name}ListModel>
{{
    private readonly I{Type.Name}Repository _{Type.Name.ToLower()}Repository;
    private readonly IMapper _mapper;

    public async Task<{Type.Name}ListModel> Handle(GetList{Type.Name}ByDynamicQuery request, CancellationToken cancellationToken)
    {{
        IPaginate<{Type.Name}> {plural.ToLower()} = await _{Type.Name.ToLower()}Repository.GetListByDynamicAsync(
                                    request.Dynamic,
                                    {letter} => {letter}.Include({letter} => {letter})
                                                .Include({letter} => {letter}),                                   
                                    request.PageRequest.Page,
                                    request.PageRequest.PageSize);
        {Type.Name}ListModel mapped{Type.Name}ListModel = _mapper.Map<{Type.Name}ListModel>({plural.ToLower()});
        return mapped{Type.Name}ListModel;
    }}
}}
";
    }

}
