using Tarefas.Infra.Persistence.EF;

namespace Tarefas.Infra.Transactions
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TarefasContext _context;

        public UnitOfWork(TarefasContext context)
        {
            _context = context;
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
