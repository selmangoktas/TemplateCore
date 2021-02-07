using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace TemplateCore.DAL.Entities
{
    [Table("Admin")]
    public class Admin
    {
        public int ID { get; set; }

        [StringLength(100),Column(TypeName = "varchar(100)"),Required(ErrorMessage ="Mail Adresi boş geçilemez"),Display(Name = "Mail Adresi"),EmailAddress(ErrorMessage ="Geçersiz Mail Adresi")]
        public string EmailAddress { get; set; }

        [StringLength(32), Column(TypeName = "varchar(32)"),DataType(DataType.Password),Required(ErrorMessage = "Parola boş geçilemez"), Display(Name = "Parola")]
        public string Password { get; set; }

        //MD5 SHA1 SHA2

        [StringLength(30), Column(TypeName = "varchar(30)"), Display(Name = "Yönetici Adı")]
        public string Name { get; set; }

        [StringLength(30), Column(TypeName = "varchar(30)"), Display(Name = "Yönetici Soyadı")]
        public string Surname { get; set; }
    }
}
