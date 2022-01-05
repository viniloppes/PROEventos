using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProEventos.Application.Dtos;

namespace Application.Dtos
{
    public class EventoDto
    {
        public int Id { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatorio")]
        // [MinLength(3, ErrorMessage = "{0} deve ter no minimo 4 caracteres")]
        // [MaxLength(50, ErrorMessage = "{0} deve ter no maximo 50 caracteres")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Intervalo permitido de 3 a 50 caracteres")]
        public string Tema { get; set; }
        [Display(Name = "Qtd Pessoas")]
        [Range(1, 120000, ErrorMessage = "{0} não pode ser menor que 1 e maior que 120.000")]
        public int QtdPessoas { get; set; }
        [RegularExpression(@".*\.(gif|jpe?g|bmp|png)$", ErrorMessage = "Não é uma imagem valida. (gif,jpg, jpeg,bmp,png)")]
        public string ImagemURL { get; set; }
        [Phone]

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [Display(Name = "e-mail")]
        [EmailAddress(ErrorMessage = "É necessario ser um {0} válido")]
        public string Email { get; set; }
        public IEnumerable<LoteDto> Lotes { get; set; }
        public IEnumerable<RedeSocialDto> RedesSociais { get; set; }
        public IEnumerable<PalestranteDto> Palestrantes { get; set; }
    }
}