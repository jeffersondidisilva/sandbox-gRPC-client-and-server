using AutoMapper;
using Sandbox.gRPC.Server.Entities;

namespace Sandbox.gRPC.Server.Mappers
{
    public class MappingProfile  : Profile
    {
        public MappingProfile()
        {
            CreateMap<TodoResult, Todo>();
            CreateMap<Todo, TodoResult>();
            CreateMap<TodoCreateCommand, Todo>();
            CreateMap<TodoUpdateCommand, Todo>();
        }
    }
}