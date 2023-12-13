using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MVC.Models
{
    [Table("Clothes")]

    public class Clothes
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Mã quần áo không được để trống.")]
        public string? ClothesID { get; set; }
        [Required(ErrorMessage = "Tên quần áo không được để trống.")]
        public string? ClothesName { get; set; }
        [Required(ErrorMessage = "Số lượng không được để trống.")]
        public string? Number { get; set; }
        [Required(ErrorMessage = "Màu sắc không được để trống.")]
        public string? Color { get; set; }
        public int Status { get; set; }
    }
    
    public class ClothesViewModel
    {
        public int Id { get; set; }
        public string? ClothesID { get; set; }
        public string? ClothesName { get; set; }
        public string? Number { get; set; }
        public string? Color { get; set; }
        public int Status { get; set; }
        public bool ClearFilter { get; set; }
        public string? ColorName { get; set; }

    }
}
