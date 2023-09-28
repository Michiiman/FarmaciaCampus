using System.ComponentModel.DataAnnotations;

namespace ApiFarmacia.Dtos;

    public class RegisterDto
    {
        [Required]
        public string Email{ get; set; }
        [Required]
        public string Username{ get; set; }
        [Required]
        public string Password{ get; set; }
    }
