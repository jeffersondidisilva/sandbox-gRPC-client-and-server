using System.Collections.Generic;
using System.Linq;
using Sandbox.gRPC.Server.Entities;

namespace Sandbox.gRPC.Server.Repository
{
    public class TodoRepository
    {
        private IList<Todo> Todos { get; } = new List<Todo>();

        public IList<Todo> GetAll() => Todos;
        public Todo GetById(int id) => Todos.FirstOrDefault(e => e.Id == id);

        public Todo Save(Todo todo)
        {
            todo.Id = (Todos.OrderByDescending(e => e.Id).FirstOrDefault()?.Id ?? 0) + 1;
            Todos.Add(todo);
            return todo;
        }
        
        public Todo Update(Todo todo)
        {
            var result = Todos.FirstOrDefault(e => e.Id == todo.Id);

            if (result == null) return todo;
            result.Name = todo.Name;
            result.Done = todo.Done;
            return todo;
        }
        
        public Todo Delete(int id)
        {
            var todo = Todos.FirstOrDefault(e => e.Id == id);
            Todos.Remove(todo);
            return todo;
        }
    }
}