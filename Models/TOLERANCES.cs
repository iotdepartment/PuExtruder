using System;

namespace WebApplication4.Models
{
    public class TOLERANCES
    {
        public int ID { get; set; }
        public string? EXTRUDER { get; set; }
        public string? MANDRIL { get; set; }
        public string? FAMILIA { get; set; }


        public Double? ID_ { get; set; }
        public Double? ID_TOL { get; set; }


        public Double? LONGITUD_CORTE { get; set; }
        public Double? LONGITUD_CORTE_TOL { get; set; }


        public Double? PARED { get; set; }
        public Double? PARED_TOL { get; set; }


        public Double? PITCH { get; set; }
        public Double? PITCH_TOL { get; set; }


        public Double? INNER_YARN { get; set; }
        public Double? INNER_YARN_TOL { get; set; }


        public Double? OUTER_YARN { get; set; }
        public Double? OUTER_YARN_TOL { get; set; }

        public Double? LONGITUD_LEYENDA { get; set; }
        public Double? LONGITUD_LEYENDA_TOL { get; set; }


        public Double? GROSOR_LEYENDA { get; set; }
        public Double? GROSOR_LEYENDA_TOL { get; set; }

    }
}
