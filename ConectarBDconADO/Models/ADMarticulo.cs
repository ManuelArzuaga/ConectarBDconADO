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

		public List <Articulo> TraerTodo()
		{
			Conectar();
			List<Articulo> articulos = new List<Articulo>();
			SqlCommand sentencia = new SqlCommand("select Codigo,Descripcion,Precio from Articulos", Conexion);

			Conexion.Open();

			SqlDataReader registros = sentencia.ExecuteReader();

			while (registros.Read())
			{
				//por cada articulo que lee crea un articulo nuevo
				Articulo articulo = new Articulo
				{
					Codigo = int.Parse(registros["Codigo"].ToString()),
					Descripcion = registros["Descripcion"].ToString(),
					Precio = float.Parse(registros["Precio"].ToString())
				};

				articulos.Add(articulo); //guarda el articulo en la lista de articulos
			}

			Conexion.Close();

			return articulos;
		}

		public Articulo TraerArticulo(int pCodigo)
		{
			Conectar();

			SqlCommand sentencia = new SqlCommand("select Codigo,Descripcion,Precio from Articulos where Codigo = @Codigo", Conexion);

			//agregar logica a la query va con @ y se usa sentencia.Parameters.Add para el tipo de dato y sentencia.Parameters[@dato].value para asignar el dato

			sentencia.Parameters.Add("@Codigo", SqlDbType.Int);
			sentencia.Parameters["@Codigo"].Value = pCodigo;

			Conexion.Open();

			SqlDataReader registros = sentencia.ExecuteReader();

			Articulo articulo = new Articulo();

			if (registros.Read())
			{
				articulo.Codigo = int.Parse(registros["Codigo"].ToString());
				articulo.Descripcion = registros["Descripcion"].ToString();
				articulo.Precio = float.Parse(registros["Precio"].ToString());
			}

			Conexion.Close();

			return articulo;

		}

		public int Modificar(Articulo pArticulo)
		{
			Conectar();

			SqlCommand sentencia = new SqlCommand("update Articulos set Descripcion = @Descripcion,Precio = @Precio where Codigo=@Codigo", Conexion);

			sentencia.Parameters.Add("@Descripcion", SqlDbType.VarChar);
			sentencia.Parameters.Add("@Precio", SqlDbType.Float);
			sentencia.Parameters.Add("@Codigo", SqlDbType.Int);

			sentencia.Parameters["@Descripcion"].Value = pArticulo.Descripcion;
			sentencia.Parameters["@Precio"].Value = pArticulo.Precio;
			sentencia.Parameters["@Codigo"].Value = pArticulo.Codigo;

			Conexion.Open();

			int i = sentencia.ExecuteNonQuery();

			Conexion.Close();

			return i;
		}

		public int Borrar(int pCodigo)
		{
			Conectar();

			SqlCommand sentencia = new SqlCommand("delete from Articulos where Codigo = @Codigo", Conexion);

			//cuando pones un valor en la query con @ tenes que hacer el sentencia.parameters.add y el sentencia.parameters["@dato"].value
			sentencia.Parameters.Add("@Codigo", SqlDbType.Int);
			sentencia.Parameters["@Codigo"].Value = pCodigo;

			Conexion.Open();

			int i = sentencia.ExecuteNonQuery();

			Conexion.Close();

			return i;
		}
	
	}
}