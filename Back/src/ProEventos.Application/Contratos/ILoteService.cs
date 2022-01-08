using System.Threading.Tasks;
using Application.Dtos;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface ILoteService
    {
  
         Task<LoteDto[]> SaveLotes(int eventoId, LoteDto[] models);
         Task<bool> DeleteLote(int eventoId, int loteId);
         
     
         //EVENTOS

         Task<LoteDto[]> GetLotesByEventoIdAsync(int eventoId);
         Task<LoteDto> GetLoteByIdAsync(int EventoId, int loteId);
    }
}