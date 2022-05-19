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
<<<<<<< HEAD
       [Column("name")]
       public string Nombre { get; set;}

       [Column("email")]
       public string Correo { get; set;}
       [Column("subject")]
       
       public string Asunto { get; set;}
       [Column("comment")]
=======
       [Column("Nombre")]
       public string Nombre { get; set;}

       [Column("Correo")]
       public string Correo { get; set;}
       [Column("Asunto")]
       
       public string Asunto { get; set;}
       [Column("Comentario")]
>>>>>>> a76203a834c2bd4d9b5f45c9accbb88c699ab466
       
       public string Comentario { get; set;}
       
       
    }
}