using AgendaDeContatos.Data;
using Microsoft.EntityFrameworkCore;

namespace AgendaDeContatos.Repositories;

public class EnderecoRepository : IEnderecoRepository
{
    private readonly AgendaContext _context;

    public EnderecoRepository(AgendaContext context)
    {
        _context = context;
    }

    public async Task<Endereco> BuscarPorIdAsync(int id)
    {
        return await _context.Enderecos
            .Include(e => e.Contato)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task AtualizarAsync(Endereco endereco)
    {
        _context.Enderecos.Update(endereco);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(int id)
    {
        var endereco = await _context.Enderecos.FindAsync(id);
        if (endereco != null)
        {
            _context.Enderecos.Remove(endereco);
            await _context.SaveChangesAsync();
        }
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}