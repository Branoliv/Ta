namespace Tarefas.Infra.Transactions
{
    public interface IUnitOfWork
    {
        void Commit();
    }
}
