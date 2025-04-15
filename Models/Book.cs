using System.ComponentModel.DataAnnotations;

namespace Library.API.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(100, ErrorMessage = "O nome deve ter até 100 caracteres.")]
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int PublishedYear { get; set; }
        public bool isBorrowed { get; set; }

        public int AuthorId { get; set; }
        public Author Author { get; set; }

    }
}
