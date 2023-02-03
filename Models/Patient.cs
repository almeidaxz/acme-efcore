using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ACME.Models
{
    [Table("patients")]
    public class Patient
    {
        public Patient(
            string? name,
            string? birthDate,
            string? cpf,
            string? gender,
            string? addressLine,
            string? addresNumber,
            string? district,
            string? city,
            string? state,
            string? zipCode
            )
        {
            Name = name;
            BirthDate = birthDate;
            Cpf = cpf;
            Gender = gender;
            AddressLine = addressLine;
            AddresNumber = addresNumber;
            District = district;
            City = city;
            State = state;
            ZipCode = zipCode;
            Active = true;
        }

        [Key]
        public int Id { get; set; }

        [Column("name")]
        [Required(ErrorMessage = "Informe o Nome")]
        [MinLength(4, ErrorMessage = "O nome deve possuir no mínimo 4 caracteres")]
        [MaxLength(60, ErrorMessage = "O nome deve possuir no máximo 60 caracteres")]
        [RegularExpression(@"^((\b[A-zÀ-ú']{2,40}\b)\s*){2,}$", ErrorMessage = "Nome inválido")]
        public string? Name { get; private set; }

        [Column("birth_date")]
        [Required(ErrorMessage = "Informe a data de nascimento no formato dd/mm/yyy")]
        public string? BirthDate { get; private set; }

        [Column("cpf")]
        [Required(ErrorMessage = "Informe o CPF")]
        [StringLength(11, ErrorMessage = "Informe o CPF com 11 dígitos")]
        public string? Cpf { get; private set; }

        [Column("gender")]
        [Required(ErrorMessage = "Informe o gênero")]
        public string? Gender { get; private set; }

        [Column("address_line")]
        [Required(ErrorMessage = "Informe a rua")]
        public string? AddressLine { get; private set; }

        [Column("address_number")]
        [Required(ErrorMessage = "Informe o número")]
        public string? AddresNumber { get; private set; }

        [Column("district")]
        [Required(ErrorMessage = "Informe o bairro")]
        public string? District { get; private set; }

        [Column("city")]
        [Required(ErrorMessage = "Informe a cidade")]
        public string? City { get; private set; }

        [Column("state")]
        [Required(ErrorMessage = "Informe o estado")]
        [StringLength(2, ErrorMessage = "Informe a sigla do estado com 2 dígitos")]
        public string? State { get; private set; }

        [Column("zip_code")]
        [Required(ErrorMessage = "Informe o CEP")]
        [StringLength(8, ErrorMessage = "O CEP deve conter 8 dígitos")]
        public string? ZipCode { get; private set; }

        [Column("active")]
        public bool Active { get; private set; }

        public void UpdateValues(
            string? name,
            string? birthDate,
            string? cpf,
            string? gender,
            string? addressLine,
            string? addresNumber,
            string? district,
            string? city,
            string? state,
            string? zipCode
            )
        {
            Name = name;
            BirthDate = birthDate;
            Cpf = cpf;
            Gender = gender;
            AddressLine = addressLine;
            AddresNumber = addresNumber;
            District = district;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public void Inactivate()
        {
            Active = false;
        }

        public void Reactivate()
        {
            Active = true;
        }
    }
}