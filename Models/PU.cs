using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("PU")]

public class PU

{
    [Required]
    //[StringLength(50)]
    public string NroEmpleado { get; set; }
    [Required]
    //[StringLength(50)]
    public string Nombre { get; set; }
    [Required]
    public int Turno { get; set; }
    [Required]
    //[DataType(DataType.Date)]
    public DateTime Fecha { get; set; }
    [Required]
    ///[DataType(DataType.Time)]
    public TimeSpan Hora { get; set; }
    [Required]
    public string Extruder { get; set; }
    [Required]
    public string Mandril { get; set; }
    [Required]
    public string Proceso { get; set; }
    [Required]
    public string Id { get; set; }
    [Required]
    public string Longitud { get; set; }
    [Required]
    public string Pared12 { get; set; }
    [Required]
    public string Pared3 { get; set; }
    [Required]
    public string Pared6 { get; set; }
    [Required]
    public string Pared9 { get; set; }
    [Required]
    public string Pitch { get; set; }
    [Required]
    public string LongLeyenda { get; set; }
    [Required]
    public string GrosorLeyenda { get; set; }
    [Required]
    public string Logo { get; set; }
}
