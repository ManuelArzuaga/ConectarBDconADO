using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ConectarBDconADO.Models
{
	public class Articulo
	{
        //3 campos que tiene la tabla articulos en la bd
        public int Codigo { get; set; }
        public string Descripcion { get; set; }
        public float Precio { get; set; }
    }
}