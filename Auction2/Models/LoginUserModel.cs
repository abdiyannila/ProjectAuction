using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Auction2.Models
{
    public class LoginUserModel
    {
        public int Id { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int IdKTP { get; set; }
        public string Pekerjaan { get; set; }
    }
}