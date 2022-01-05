using System;
using Application.Dtos;
using ProEventos.Domain;

namespace ProEventos.Application.Dtos
{
    public class LoteDto
    {
        
        public int Id { get; set; }
        public string Nome { get; set; }
        public Decimal Preco { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
        public int Quantidade { get; set; }
        public int EventoId { get; set; }
        public EventoDto Evento { get; set; }
    }
}