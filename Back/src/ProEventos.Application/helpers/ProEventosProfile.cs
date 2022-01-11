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
            // CreateMap<Evento, EventoDto>();
            // CreateMap<EventoDto, Evento>();
            // CreateMap<Lote, LoteDto>();
            // CreateMap<LoteDto, Lote>();  
            // CreateMap<RedeSocial, RedeSocialDto>(); 
            // CreateMap<Palestrante, PalestranteDto>(); 
            CreateMap<Evento, EventoDto>().ReverseMap();
            CreateMap<Lote, LoteDto>().ReverseMap();
            CreateMap<RedeSocial, RedeSocialDto>().ReverseMap();
            CreateMap<Palestrante, PalestranteDto>().ReverseMap();

        }
    }
}