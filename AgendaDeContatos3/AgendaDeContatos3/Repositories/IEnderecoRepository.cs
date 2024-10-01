namespace AgendaDeContatos.Repositories;

public interface IEnderecoRepository
{
    Task<Endereco> BuscarPorIdAsync(int id);

    Task SaveAsync();

    Task AtualizarAsync(Endereco endereco);

    Task ExcluirAsync(int id);

}