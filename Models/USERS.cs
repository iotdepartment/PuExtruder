using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class USERS
    {
    [Required]
    public int ID { get; set; }
    [Required]
    public string NOMBRE { get; set; }
    [Required]
    public string ROL { get; set; }

}