using EthioClass.Application.Schools.Dtos;
using MediatR;

namespace EthioClass.Application.Schools.Queries;

public record GetSchoolByIdQuery(int Id): IRequest<SchoolResponseDto?>;