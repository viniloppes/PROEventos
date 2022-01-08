using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contratos
{
    public interface ILotePersist
    {



        /// <summary>
        ///  Método get que retornara uma lista de lotes por eventoId
        /// </summary>
        /// <param name="eventoId">Código chave da tabela evento</param>
        /// <returns></returns>
        Task<Lote[]> GetLotesByEventoIdAsync(int eventoId);
        /// <summary>
        ///  Método get que retornara apenas 1 lote
        /// </summary>
        /// <param name="eventoId">Código chave da tabela evento</param>
        /// <param name="loteId">Código chave do meu lote</param>
        /// <returns>Apenas 1 Lote</returns>
        Task<Lote> GetLoteByIdsAsync(int eventoId, int loteId);


    }
}