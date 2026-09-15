using AutoCheck.Domain;

namespace AutoCheck.Api.Interfaces
{
    public interface IVeiculosRepository
    {
        Task<List<Veiculo>> ObterTodosAsync();
        Task<Veiculo?> ObterPorIdAsync(int id);
        Task AdicionarAsync(Veiculo veiculo);
        Task SalvarAlteracoesAsync();
    }
}