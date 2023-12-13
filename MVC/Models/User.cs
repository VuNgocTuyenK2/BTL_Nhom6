// Quach Kieu Trang lam chuc nang dang nhap admin
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace MVC.Models
{
    [Table("User")]

    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên tài khoản không được để trống.")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống.")]
        public string? PassWord { get; set; }
        [Required(ErrorMessage = "Họ tên không được để trống.")]
        public string? Name { get; set; }
        [Required(ErrorMessage = "Email không được để trống.")]
        public string? Email { get; set; }
    }
}
