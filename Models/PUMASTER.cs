using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//[Table("PUMASTER")]

public class PUMASTER

{
    public int ID { get; set; }
    [Required]
    public string NRO_EMPLEADO { get; set; }
    [Required]
    public string NOMBRE { get; set; }
    [Required]
    public int TURNO { get; set; }
    [Required]
    public DateTime FECHA { get; set; }
    [Required]
    public TimeSpan HORA { get; set; }
    [Required]
    public string EXTRUDER { get; set; }
    [Required]
    public string MANDRIL { get; set; }
    [Required]
    public string FAMILIA { get; set; }
    [Required]
    public string ID_A { get; set; }
    [Required]
    public string LONGITUD_A { get; set; }
    [Required]
    public string PARED12_A { get; set; }
    [Required]
    public string PARED3_A { get; set; }
    [Required]
    public string PARED6_A { get; set; }
    [Required]
    public string PARED9_A { get; set; }
    [Required]
    public string PITCH_A { get; set; }
    [Required]
    public string PARED_INTERNA_A { get; set; }
    [Required]
    public string PARED_EXTERNA_A { get; set; }
    [Required]
    public string LONGITUD_LEYENDA_A { get; set; }
    [Required]
    public string GROSOR_LEYENDA_A { get; set; }
    [Required]
    public string LOGO_A { get; set; }
    [Required]
    public string ID_B { get; set; }
    [Required]
    public string LONGITUD_B { get; set; }
    [Required]
    public string PARED12_B { get; set; }
    [Required]
    public string PARED3_B { get; set; }
    [Required]
    public string PARED6_B { get; set; }
    [Required]
    public string PARED9_B { get; set; }
    [Required]
    public string PITCH_B { get; set; }
    [Required]
    public string PARED_INTERNA_B { get; set; }
    [Required]
    public string PARED_EXTERNA_B { get; set; }
    [Required]
    public string LONGITUD_LEYENDA_B { get; set; }
    [Required]
    public string GROSOR_LEYENDA_B { get; set; }
    [Required]
    public string LOGO_B { get; set; }
    [Required]
    public string COMENTARIOS { get; set; }
}
