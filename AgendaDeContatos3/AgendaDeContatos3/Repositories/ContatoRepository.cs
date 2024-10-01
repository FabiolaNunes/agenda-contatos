using AgendaDeContatos.Data;
using Microsoft.EntityFrameworkCore;

namespace AgendaDeContatos.Repositories
{
    public class ContatoRepository : IContatoRepository
    {
        private readonly AgendaContext _context;

        public ContatoRepository(AgendaContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Contato contato)
        {
            await _context.Contatos.AddAsync(contato);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Contato>> ListarTodosAsync()
        {
            return await _context.Contatos
                .Include(c => c.Enderecos) 
                .ToListAsync();
        }

        public async Task<Contato> BuscarPorIdAsync(int id)
        {
            return await _context.Contatos
                .Include(c => c.Enderecos) 
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task ExcluirAsync(int id)
        {
            var contato = await _context.Contatos
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (contato != null)
            {                
                _context.Contatos.Remove(contato);
                await _context.SaveChangesAsync();
            }
        }
        public async Task SaveAsync()
        {
             await _context.SaveChangesAsync();
        }
    }
}