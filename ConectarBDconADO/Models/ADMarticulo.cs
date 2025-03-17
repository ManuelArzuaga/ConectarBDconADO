using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ConectarBDconADO.Models
{
	public class ADMarticulo
	{
		private SqlConnection Conexion;

		private void Conectar()
		{
			string stringConexion = ConfigurationManager.ConnectionStrings["Conexion"].ToString();
			Conexion = new SqlConnection(stringConexion);
		}

		public int Alta(Articulo pArticulo)
		{
			Conectar();
			SqlCommand sentencia = new SqlCommand("Insert into Articulos(Codigo, Descripcion, Precio) values (@codigo, @descripcion, @precio)", Conexion);
			
			//tipos de datos que se van a agregar en la bd
			sentencia.Parameters.Add("@Codigo", SqlDbType.Int);
			sentencia.Parameters.Add("@Descripcion", SqlDbType.VarChar);
			sentencia.Parameters.Add("@Precio", SqlDbType.Float);

			//preparo los datos a agregar en la bd
			sentencia.Parameters["@Codigo"].Value = pArticulo.Codigo;
			sentencia.Parameters["@Descripcion"].Value = pArticulo.Descripcion;
			sentencia.Parameters["@Precio"].Value = pArticulo.Precio;

			//abro la conexion con la bd
			Conexion.Open();

			int i = sentencia.ExecuteNonQuery();

			//cerrar la conexion con la bd
			Conexion.Close();

			return i;
		}
	
	}
}