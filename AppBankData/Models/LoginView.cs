using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using IdentityManagement.Utilities;

namespace AppBankData.Models
{
    public class LoginView
    {
        [Display(Name = "NIK")]
        public string NIK { get; set; }

        //[Required(ErrorMessage = "Masukkan password anda ")]
        [Display(Name = "Password")]
        public string Password { get; set; }

    }
    public class RegistrationView
    {

        [Display(Name = "NIK")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string NIK { get; set; }

        [Display(Name = "Nama")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(100)]
        public string Nama { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Bagian")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        [StringLength(100)]
        public string Bagian { get; set; }

        [Display(Name = "Status")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Status { get; set; }

        [Display(Name = "Role")]
        [Required(ErrorMessage = "{0} harus diisi.")]
        public string Role { get; set; }
    }

    public class GantiPassword
    {

        public string NIK { get; set; }

        [Display(Name = "Password User")]
        //[Required(ErrorMessage = "Masukkan Password User")]
        //[RegularExpression(@"^(?=.*[0-9])(?=.*[!@#$%^&*])[0-9a-zA-Z!@#$%^&*0-9]{10,}$", ErrorMessage = "{0} harus mengandung angka, Huruf, dan Spesial Karakter.")]
        //[DataType(DataType.Password)]
        public string PasswordUser { get; set; }

        [Display(Name = "Password Lama")]
        //[Required(ErrorMessage = "Masukkan Password Lama")]
        //[RegularExpression(@"^(?=.*[0-9])(?=.*[!@#$%^&*])[0-9a-zA-Z!@#$%^&*0-9]{10,}$", ErrorMessage = "{0} harus mengandung angka, Huruf, dan Spesial Karakter.")]
        //[DataType(DataType.Password)]
        public string PasswordLama { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "Masukkan Password Baru")]
        //[RegularExpression(@"^(?=.*[0-9])(?=.*[!@#$%^&*])[0-9a-zA-Z!@#$%^&*0-9]{10,}$", ErrorMessage = "{0} harus mengandung angka, Huruf, dan Spesial Karakter.")]
        //[DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Masukkan konfirmasi password baru")]
        //[DataType(DataType.Password)]
        [Display(Name = "Konfirmasi Password")]
        [Compare("Password", ErrorMessage = "Error : Konfirmasi Password tidak sama dengan Password")]
        public string ConfirmPassword { get; set; }

    }
}
