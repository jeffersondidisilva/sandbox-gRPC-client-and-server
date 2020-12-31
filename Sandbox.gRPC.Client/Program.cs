using System;
using Sandbox.gRPC.Client.APIs;
using Sandbox.gRPC.Server;

namespace Sandbox.gRPC.Client
{
    class Program
    {
        private static readonly TodoApi TodoApi = new();
        
        static void Main()
        {
            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("TODO API: ");
                Console.WriteLine("[1]: GetAll");
                Console.WriteLine("[2]: GetById");
                Console.WriteLine("[3]: Create");
                Console.WriteLine("[4]: Update");
                Console.WriteLine("[5]: Delete");
                Console.WriteLine("[Other]: Exit");
                Console.WriteLine();

                var option = int.Parse(Console.ReadLine() ?? "1");

                switch (option)
                {
                    case 1: GetAll(); break;
                    case 2: GetById(); break;
                    case 3: Add(); break;
                    case 4: Update(); break;
                    case 5: Delete(); break;
                    default: return;
                }
            }
        }
        
        private static void GetById()
        {
            Console.Write("Todo id: ");
            var id = int.Parse(Console.ReadLine() ?? "1");
            var todo = TodoApi.GetById(id);
            Console.WriteLine($"{todo.Id};{todo.Name};{todo.Done}");
        }

        private static void GetAll()
        {
            TodoApi.Get().ForEach(e =>
            {
                Console.WriteLine($"{e.Id};{e.Name};{e.Done}");
            });
        }

        private static void Add()
        {
            Console.Write("Todo name: ");
            TodoApi.Save(new TodoCreateCommand
            {
                Name = Console.ReadLine()
            });
        }
        
        private static void Update()
        {
            Console.Write("Todo id: ");
            var id = int.Parse(Console.ReadLine() ?? "1");
            Console.Write("Todo name: ");
            var name = Console.ReadLine();
            Console.Write("Todo done: ");
            var done = bool.Parse(Console.ReadLine() ?? "true");
            
            TodoApi.Update(new TodoUpdateCommand
            {
               Id = id,
               Name = name,
               Done = done
            });
        }
        
        private static void Delete()
        {
            Console.Write("Todo id: ");
            var todoApi = new TodoApi();
            todoApi.Delete(int.Parse(Console.ReadLine() ?? "1"));
        }
    }
}