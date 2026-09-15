using AutoCheck.Domain;
using AutoCheck.Api.Data;
using AutoCheck.Api.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AutoCheck.Api.Repositories
{
    public class VeiculoRepository : IVeiculosRepository
    {
        private readonly AutoCheckDbContext _context;

        public VeiculoRepository(AutoCheckDbContext context)
        {
            _context = context;
        }

        public async Task<List<Veiculo>> ObterTodosAsync()
        {
            return await _context.Veiculos
                .Include(v => v.VistoriaRealizada)
                .ToListAsync();
        }

        public async Task<Veiculo?> ObterPorIdAsync(int id)
        {
            return await _context.Veiculos
                .Include(v => v.VistoriaRealizada)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task AdicionarAsync(Veiculo veiculo)
        {
            await _context.Veiculos.AddAsync(veiculo);
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}