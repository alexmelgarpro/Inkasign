using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Inkasign.Models
{
    [Table("t_contacto")]
    public class Contacto
    {

       [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       [Column("id")]


       public int Id { get; set;}
       [Column("name")]
       public string Nombre { get; set;}

       [Column("email")]
       public string Correo { get; set;}
       [Column("subject")]
       
       public string Asunto { get; set;}
       [Column("comment")]
       
       public string Comentario { get; set;}
       
       
    }
}