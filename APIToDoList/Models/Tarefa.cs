using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace APIToDoList.Models;

public class Tarefa
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "O título é obrigatório.")]
    [StringLength(60, MinimumLength = 5, ErrorMessage ="O Título deve conter entre 5 e 60 caracteres")]
    public string? Titulo { get; set; }

    [Required(ErrorMessage = "A descrição é obrigatória.")]
    [StringLength(100, MinimumLength = 10, ErrorMessage = "A descrição deve conter entre 10 e 100 caracteres")]
    public string? Descricao { get; set; }

    [Required(ErrorMessage = "A data de conclusão é obrigatória.")]
    public bool Status { get; set; } = false;

    public DateTime DataCriacao { get; set; } = DateTime.Now;
    public DateTime? DataConclusao { get; set; }
}
