using System.Collections.Generic;
using System.Linq;
using Google.Protobuf.WellKnownTypes;
using Sandbox.gRPC.Client.APIs.Common;
using Sandbox.gRPC.Server;

namespace Sandbox.gRPC.Client.APIs
{
    public class TodoApi
    {
        private TodoService.TodoServiceClient Client { get; }
        
        public TodoApi()
        {
            var channel = ChannelFactory.Create();
            Client = new TodoService.TodoServiceClient(channel);
        }

        public List<TodoResult> Get() => 
            Client.Get(new Empty()).Items.ToList();
        
        public TodoResult GetById(int id) => 
            Client.GetId(new Id { Id_ = id });
        
        public TodoResult Save(TodoCreateCommand command) => 
            Client.Save(command);
        
        public TodoResult Update(TodoUpdateCommand command) => 
            Client.Update(command);
        
        public TodoResult Delete(int id) => 
            Client.Delete(new Id { Id_ = id });
    }
}