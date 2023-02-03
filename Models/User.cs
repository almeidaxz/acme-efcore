using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACME.Models
{
    [Table("users")]
    public class User
    {
        public User(int id, string? name, string? email, string? password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        [Key]
        public int Id { get; private set; }

        [Column("name")]
        [Required(ErrorMessage = "Informe o nome completo")]
        [MinLength(4, ErrorMessage = "O nome deve possuir no mínimo 4 caracteres")]
        [MaxLength(60, ErrorMessage = "O nome deve possuir no máximo 60 caracteres")]
        [RegularExpression(@"^((\b[A-zÀ-ú']{2,40}\b)\s*){2,}$", ErrorMessage = "Nome inválido")]
        public string? Name { get; private set; }

        [Column("email")]
        [Required(ErrorMessage = "Informe o email")]
        [RegularExpression(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$", ErrorMessage = "Informe um email válido")]
        public string? Email { get; private set; }

        [Column("password")]
        [Required(ErrorMessage = "Informe a senha")]
        [MinLength(8, ErrorMessage = "A senha deve possuir entre 8 e 16 caracteres")]
        [MaxLength(16, ErrorMessage = "A senha deve possuir entre 8 e 16 caracteres")]
        public string? Password { get; private set; }
    }
}