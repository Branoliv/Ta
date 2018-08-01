using MongoDB.Driver;
using Tarefas.Domain.Entities;
using Tarefas.Shared;

namespace Tarefas.Infra.Persistence.Mongo
{
    public class TarefaContextMongo
    {
        public IMongoDatabase _database { get; }

        public TarefaContextMongo()
        {
            MongoClient client = new MongoClient(Settings.ConnectionStringMg);
            _database = client.GetDatabase("Tarefas");
        }

        public IMongoCollection<Usuario> Usuarios
        {
            get
            {
                return _database.GetCollection<Usuario>("Usuarios");
            }
        }
    }
}
