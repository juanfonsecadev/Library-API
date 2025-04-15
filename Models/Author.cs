using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter até 100 caracteres.")]
        public string Name { get; set; }

        [Column(Order = 2)] // Garante que o sobrenome venha após o nome
        public string Surname { get; set; }

        [Column(Order = 3)] // Garante que a descrição venha após o sobrenome
        public string? Description { get; set; }

    }
}