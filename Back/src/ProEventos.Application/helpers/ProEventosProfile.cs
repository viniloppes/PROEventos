using AutoMapper;
using Application.Dtos;
using ProEventos.Domain;
using ProEventos.Application.Dtos;

namespace ProEventos.API.helpers
{
    public class ProEventosProfile : Profile
    {
        public ProEventosProfile()
        {
            CreateMap<Evento, EventoDto>();
            CreateMap<EventoDto, Evento>();
            CreateMap<Lote, LoteDto>();
            // CreateMap<LoteDto, Lote>();  
            CreateMap<RedeSocial, RedeSocialDto>(); 
            // CreateMap<RedeSocialDto, RedeSocial>();  
            CreateMap<Palestrante, PalestranteDto>(); 
            // CreateMap<PalestranteDto, Palestrante>();
           
        }
    }
}