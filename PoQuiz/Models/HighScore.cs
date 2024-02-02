using System.ComponentModel.DataAnnotations;

namespace PoQuiz.Models
{
    public class HighScore
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        [Required]
        public int Score { get; set; }
    }
}
