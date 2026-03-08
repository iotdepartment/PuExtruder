using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PUMASTER
{
    public int ID { get; set; }

    // Datos generales y primera pieza obligatorios
    [Required] public string? NRO_EMPLEADO { get; set; }
    [Required] public string? NOMBRE { get; set; }
    [Required] public string? TURNO { get; set; }
    [Required] public DateTime FECHA { get; set; }
    [Required] public TimeSpan HORA { get; set; }
    [Required] public string? EXTRUDER { get; set; }
    [Required] public string? MANDRIL { get; set; }
    [Required] public string? FAMILIA { get; set; }

    [Required] public string? ID_A { get; set; }
    [Required] public string? LONGITUD_A { get; set; }
    [Required] public string? PARED12_A { get; set; }
    [Required] public string? PARED3_A { get; set; }
    [Required] public string? PARED6_A { get; set; }
    [Required] public string? PARED9_A { get; set; }
    [Required] public string? PITCH_A { get; set; }
    [Required] public string? PARED_INTERNA_A { get; set; }
    [Required] public string? PARED_EXTERNA_A { get; set; }
    [Required] public string? LONGITUD_LEYENDA_A { get; set; }
    [Required] public string? GROSOR_LEYENDA_A { get; set; }
    [Required] public string? PESO_A { get; set; }
    [Required] public string? LOGO_A { get; set; }

    // Última pieza opcional
    public string? ID_B { get; set; }
    public string? LONGITUD_B { get; set; }
    public string? PARED12_B { get; set; }
    public string? PARED3_B { get; set; }
    public string? PARED6_B { get; set; }
    public string? PARED9_B { get; set; }
    public string? PITCH_B { get; set; }
    public string? PARED_INTERNA_B { get; set; }
    public string? PARED_EXTERNA_B { get; set; }
    public string? LONGITUD_LEYENDA_B { get; set; }
    public string? GROSOR_LEYENDA_B { get; set; }
    public string? PESO_B { get; set; }
    public string? LOGO_B { get; set; }

    public string? COMENTARIOS { get; set; }
    public string? STATUS { get; set; }
}
