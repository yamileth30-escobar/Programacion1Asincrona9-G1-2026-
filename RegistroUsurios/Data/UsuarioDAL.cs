using RegistroUsurios.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsurios.Data
{
    public class UsuarioDAL
    {
        string conexion = "Server=YAMILETH-ESCOBA\\SQLExpress;Database=CrudUsuariosDB;Trusted_Connection=True;";

        // ============================
        // MÉTODOS QUE EL ESTUDIANTE DEBE CREAR
        // ============================

        // 1. Crear un método para LISTAR todos los usuarios
        // Este método debe devolver una lista de tipo List<Usuario>
        // Sugerencia de nombre: ObtenerTodos()
        public List<Usuario> ObtenerTodos()
        {
            // Aquí el estudiante debe:
            // 1. Crear la lista de usuarios
            List<Usuario> lista = new List<Usuario>();

            // 2. Abrir conexión a la base de datos
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();

                // 3. Ejecutar un SELECT * FROM Usuarios
                string query = "SELECT * FROM Usuarios";

                SqlCommand cmd = new SqlCommand(query, conn);

                // 4. Recorrer los resultados con SqlDataReader
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    // 5. Guardar cada registro en un objeto Usuario
                    Usuario usuario = new Usuario();

                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nombres = reader["Nombres"].ToString();
                    usuario.Apellidos = reader["Apellidos"].ToString();
                    usuario.Correo = reader["Correo"].ToString();
                    usuario.Edad = Convert.ToInt32(reader["Edad"]);
                    usuario.Activo = Convert.ToBoolean(reader["Activo"]);

                    // 6. Agregar cada usuario a la lista
                    lista.Add(usuario);
                }
            }

            // 7. Retornar la lista
            return lista;
        }

        // 2. Crear un método para BUSCAR un usuario por ID
        // Este método debe devolver un objeto Usuario
        // Sugerencia de nombre: ObtenerPorId(int id)
        public Usuario ObtenerPorId(int id)
        {
            Usuario usuario = null;

            // Aquí el estudiante debe:
            // 1. Abrir conexión
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();

                // 2. Ejecutar un SELECT con WHERE Id = @Id
                string query = "SELECT * FROM Usuarios WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);

                // 3. Usar parámetros
                cmd.Parameters.AddWithValue("@Id", id);

                // 4. Leer el resultado
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // 5. Si encuentra el registro, devolver un objeto Usuario
                    usuario = new Usuario();

                    usuario.Id = Convert.ToInt32(reader["Id"]);
                    usuario.Nombres = reader["Nombres"].ToString();
                    usuario.Apellidos = reader["Apellidos"].ToString();
                    usuario.Correo = reader["Correo"].ToString();
                    usuario.Edad = Convert.ToInt32(reader["Edad"]);
                    usuario.Activo = Convert.ToBoolean(reader["Activo"]);
                }
            }

            // 6. Si no lo encuentra, devolver null
            return usuario;
        }

        // 3. Crear un método para INSERTAR un usuario
        // Este método debe devolver true si se insertó correctamente
        // Sugerencia de nombre: Insertar(Usuario usuario)
        public bool Insertar(Usuario usuario)
        {
            // Aquí el estudiante debe:
            // 1. Abrir conexión
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();

                // 2. Crear un INSERT INTO Usuarios(...)
                string query = @"INSERT INTO Usuarios
                                (Nombres, Apellidos, Correo, Edad, Activo)
                                VALUES
                                (@Nombres,@Apellidos,@Correo,@Edad,@Activo)";

                SqlCommand cmd = new SqlCommand(query, conn);

                // 3. Usar parámetros con AddWithValue
                cmd.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@Edad", usuario.Edad);
                cmd.Parameters.AddWithValue("@Activo", usuario.Activo);

                // 4. Ejecutar ExecuteNonQuery()
                int filas = cmd.ExecuteNonQuery();

                // 5. Retornar true si filas afectadas > 0
                return filas > 0;
            }
        }

        // 4. Crear un método para ACTUALIZAR un usuario
        // Este método debe devolver true si se actualizó correctamente
        // Sugerencia de nombre: Actualizar(Usuario usuario)
        public bool Actualizar(Usuario usuario)
        {
            // Aquí el estudiante debe:
            // 1. Abrir conexión
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();

                // 2. Crear un UPDATE Usuarios SET ...
                string query = @"UPDATE Usuarios SET
                                Nombres=@Nombres,
                                Apellidos=@Apellidos,
                                Correo=@Correo,
                                Edad=@Edad,
                                Activo=@Activo
                                WHERE Id=@Id";

                SqlCommand cmd = new SqlCommand(query, conn);

                // 3. Actualizar Nombres, Apellidos, Correo, Edad y Activo
                cmd.Parameters.AddWithValue("@Id", usuario.Id);
                cmd.Parameters.AddWithValue("@Nombres", usuario.Nombres);
                cmd.Parameters.AddWithValue("@Apellidos", usuario.Apellidos);
                cmd.Parameters.AddWithValue("@Correo", usuario.Correo);
                cmd.Parameters.AddWithValue("@Edad", usuario.Edad);
                cmd.Parameters.AddWithValue("@Activo", usuario.Activo);

                // 5. Ejecutar ExecuteNonQuery()
                int filas = cmd.ExecuteNonQuery();

                // 6. Retornar true si filas afectadas > 0
                return filas > 0;
            }
        }

        // 5. Crear un método para ELIMINAR un usuario por ID
        // Este método debe devolver true si se eliminó correctamente
        // Sugerencia de nombre: Eliminar(int id)
        public bool Eliminar(int id)
        {
            // Aquí el estudiante debe:
            // 1. Abrir conexión
            using (SqlConnection conn = new SqlConnection(conexion))
            {
                conn.Open();

                // 2. Crear un DELETE FROM Usuarios WHERE Id = @Id
                string query = "DELETE FROM Usuarios WHERE Id = @Id";

                SqlCommand cmd = new SqlCommand(query, conn);

                // 3. Usar parámetro
                cmd.Parameters.AddWithValue("@Id", id);

                // 4. Ejecutar ExecuteNonQuery()
                int filas = cmd.ExecuteNonQuery();

                // 5. Retornar true si filas afectadas > 0
                return filas > 0;
            }
        }
    }
}
