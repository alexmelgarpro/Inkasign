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
       [Column("Nombre")]
       public string Nombre { get; set;}

       [Column("Correo")]
       public string Correo { get; set;}
       [Column("Asunto")]
       
       public string Asunto { get; set;}
       [Column("Comentario")]
       
       public string Comentario { get; set;}
       
       
    }
}