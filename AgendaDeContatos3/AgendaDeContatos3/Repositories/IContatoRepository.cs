namespace AgendaDeContatos.Repositories;

public interface IContatoRepository
{
    Task AdicionarAsync(Contato contato);

    Task<IEnumerable<Contato>> ListarTodosAsync();

    Task<Contato> BuscarPorIdAsync(int id);

    Task ExcluirAsync(int id);

    Task SaveAsync();
}