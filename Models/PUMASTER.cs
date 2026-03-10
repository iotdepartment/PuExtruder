using Org.BouncyCastle.Math;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class PUMASTER
{
    //Id del registro
    public int ID { get; set; }

    //Datos generales del registro
    public string? NRO_EMPLEADO { get; set; }
    public string? NOMBRE { get; set; }
    public string? TURNO { get; set; }
    public DateTime FECHA { get; set; }
    public TimeSpan HORA { get; set; }
    public string? EXTRUDER { get; set; }
    public string? MANDRIL { get; set; }
    public string? FAMILIA { get; set; }

    //Datos Primera pieza
    public string? ID_A { get; set; }
    public string? LONGITUD_A { get; set; }
    public string? PARED12_A { get; set; }
    public string? PARED3_A { get; set; }
    public string? PARED6_A { get; set; }
    public string? PARED9_A { get; set; }
    public string? PITCH_A { get; set; }
    public string? PARED_INTERNA_A { get; set; }
    public string? PARED_EXTERNA_A { get; set; }
    public string? LONGITUD_LEYENDA_A { get; set; }
    public string? GROSOR_LEYENDA_A { get; set; }
    public string? PESO_A { get; set; }
    public string? LOGO_A { get; set; }

    //Datos Ultima pieza
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

    //Comentarios y estatus del registro
    public string? COMENTARIOS { get; set; }
    public string? STATUS { get; set; }
}
