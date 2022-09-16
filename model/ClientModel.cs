using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FinalTestMVC.Models
{
    public class ClientModel
    {
        public int UserID { get; set; }

        [Required(ErrorMessage = "Enter User Name")]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Enter PassWord")]
        [DisplayName("PassWord")]
        [DataType(DataType.Password)]
        public string PassCode { get; set; }

        [Required(ErrorMessage = "Enter Confirm PassWord")]
        [DisplayName("Confirm PassWord")]
        [DataType(DataType.Password)]
        [Compare("PassCode")]
        public string ConfirmPassCode { get; set; }

        [Required(ErrorMessage = "Enter Salary")]
        [DisplayName("Salary")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Enter Email")]
        [DisplayName("Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter Gender")]
        [DisplayName("Gender")]
        public string Gender { get; set; }
        public string CityName { get; set; }
        public string StateName { get; set; }
        public int StateID { get; set; }

        public int CityID { get; set; }

        public string type { get; set; }

    }
}