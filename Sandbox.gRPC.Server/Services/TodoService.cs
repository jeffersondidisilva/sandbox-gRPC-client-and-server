using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.AspNetCore.Authorization;
using Sandbox.gRPC.Server.Entities;
using Sandbox.gRPC.Server.Repository;

namespace Sandbox.gRPC.Server.Services
{
    [Authorize]
    public class TodoService: Server.TodoService.TodoServiceBase
    {
        private readonly TodoRepository _repository;
        private readonly IMapper _mapper;

        public TodoService(
            TodoRepository repository, 
            IMapper mapper
        )
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<TodoListResult> Get(Empty request, ServerCallContext context)
        {
            var result = new TodoListResult();
            var items = _mapper.Map<IList<TodoResult>>(_repository.GetAll());
            result.Items.AddRange(items);
            return Task.FromResult(result);
        }
        
        public override Task<TodoResult> GetId(Id request, ServerCallContext context)
        {
            var todo = _repository.GetById(request.Id_);
            return Task.FromResult(_mapper.Map<TodoResult>(todo));
        }

        public override Task<TodoResult> Save(TodoCreateCommand request, ServerCallContext context)
        {
            var todo = _repository.Save(_mapper.Map<Todo>(request));
            return Task.FromResult(_mapper.Map<TodoResult>(todo));
        }

        public override Task<TodoResult> Update(TodoUpdateCommand request, ServerCallContext context)
        {
            var todo = _repository.Update(_mapper.Map<Todo>(request));
            return Task.FromResult(_mapper.Map<TodoResult>(todo));
        }

        public override Task<TodoResult> Delete(Id request, ServerCallContext context)
        {
            var todo = _repository.Delete(request.Id_);
            return Task.FromResult(_mapper.Map<TodoResult>(todo));
        }
    }
}