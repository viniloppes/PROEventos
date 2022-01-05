using System.Threading.Tasks;
using Application.Dtos;

namespace ProEventos.Application.Contratos
{
    public interface IEventosService
    {
         Task<EventoDto> AddEventos(EventoDto model);
         Task<EventoDto> UpdateEventos(int eventoId, EventoDto model);
         Task<bool> DeleteEvento(int eventoId);
         
     
         //EVENTOS
         Task<EventoDto[]> GetAllEventosAsync( bool includePalestrantes = false);

         Task<EventoDto[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false);
         Task<EventoDto> GetEventoByIdAsync(int EventoId, bool includePalestrantes = false);
    }
}