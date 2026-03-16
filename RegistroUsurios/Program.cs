using RegistroUsurios.Data;
using RegistroUsurios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroUsurios
{
    internal class Program
    {
        static UsuarioDAL usuarioDAL = new UsuarioDAL();

        static void Main(string[] args)
        {
            int opcion = 0;

            do
            {
                Console.Clear();
                Console.WriteLine("====================================");
                Console.WriteLine("   SISTEMA DE REGISTRO DE USUARIOS  ");
                Console.WriteLine("====================================");
                Console.WriteLine("1. Listar usuarios");
                Console.WriteLine("2. Buscar usuario por ID");
                Console.WriteLine("3. Registrar usuario");
                Console.WriteLine("4. Actualizar usuario");
                Console.WriteLine("5. Eliminar usuario");
                Console.WriteLine("6. Salir");
                Console.Write("Seleccione una opción: ");

                if (!int.TryParse(Console.ReadLine(), out opcion))
                {
                    opcion = 0;
                }

                Console.Clear();

                switch (opcion)
                {
                    case 1:
                        ListarUsuarios();
                        break;
                    case 2:
                        BuscarUsuarioPorId();
                        break;
                    case 3:
                        RegistrarUsuario();
                        break;
                    case 4:
                        ActualizarUsuario();
                        break;
                    case 5:
                        EliminarUsuario();
                        break;
                    case 6:
                        Console.WriteLine("Saliendo del sistema...");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }

                if (opcion != 6)
                {
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    Console.ReadKey();
                }

            } while (opcion != 6);
        }

        static void ListarUsuarios()
        {
            // El estudiante debe:
            // 1. Llamar usuarioDAL.ObtenerTodos()
            var lista = usuarioDAL.ObtenerTodos();

            // 2. Guardar la lista en una variable
            var usuarios = lista;

            // 3. Validar si la lista está vacía
            if (lista.Count == 0)
            {
                Console.WriteLine("No hay usuarios registrados.");
                return;
            }

            // 4. Mostrar cada usuario en pantalla
            foreach (var u in lista)
            {
                Console.WriteLine($"ID: {u.Id}");
                Console.WriteLine($"Nombre: {u.Nombres} {u.Apellidos}");
                Console.WriteLine($"Correo: {u.Correo}");
                Console.WriteLine($"Edad: {u.Edad}");
                Console.WriteLine($"Activo: {u.Activo}");
                Console.WriteLine("-------------------------------");
            }
        }


        static void BuscarUsuarioPorId()
        {
            // El estudiante debe:
            // 1. Pedir el ID
            Console.Write("Ingrese ID del usuario: ");

            // 2. Validar que sea numérico
            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            // 3. Llamar usuarioDAL.ObtenerPorId(id)
            var usuario = usuarioDAL.ObtenerPorId(id);


            // 4. Mostrar el usuario si existe
            if (usuario != null)
            {
                Console.WriteLine($"ID: {usuario.Id}");
                Console.WriteLine($"Nombre: {usuario.Nombres} {usuario.Apellidos}");
                Console.WriteLine($"Correo: {usuario.Correo}");
                Console.WriteLine($"Edad: {usuario.Edad}");
            }
            else
            {

                // 5. Mostrar mensaje si no existe

                Console.WriteLine("Usuario no encontrado.");
            }
        }

        static void RegistrarUsuario()
        {
            // El estudiante debe:
            // 1. Crear un objeto Usuario
            Usuario usuario = new Usuario();

            // 2. Pedir nombres, apellidos, correo y edad
            Console.Write("Nombres: ");
            usuario.Nombres = Console.ReadLine();

            Console.Write("Apellidos: ");
            usuario.Apellidos = Console.ReadLine();

            Console.Write("Correo: ");
            usuario.Correo = Console.ReadLine();

            Console.Write("Edad: ");

            if (!int.TryParse(Console.ReadLine(), out int edad))
            {
                Console.WriteLine("Edad inválida.");
                return;
            }

            usuario.Edad = edad;

            // 3. Asignar Activo = true
            usuario.Activo = true;

            // 4. Llamar usuarioDAL.Insertar(usuario)
            bool resultado = usuarioDAL.Insertar(usuario);

            // 5. Mostrar mensaje según el resultado
            if (resultado)
                Console.WriteLine("Usuario registrado correctamente.");
            else
                Console.WriteLine("Error al registrar usuario.");
        }

        static void ActualizarUsuario()
        {
            // El estudiante debe:
            // 1. Pedir ID
            Console.Write("Ingrese ID del usuario: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            // 2. Buscar si el usuario existe con usuarioDAL.ObtenerPorId(id)
            var usuario = usuarioDAL.ObtenerPorId(id);

            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            // 3. Si existe, pedir nuevos datos
            Console.Write("Nuevo nombre: ");
            usuario.Nombres = Console.ReadLine();

            Console.Write("Nuevo apellido: ");
            usuario.Apellidos = Console.ReadLine();

            Console.Write("Nuevo correo: ");
            usuario.Correo = Console.ReadLine();

            Console.Write("Nueva edad: ");

            if (!int.TryParse(Console.ReadLine(), out int edad))
            {
                Console.WriteLine("Edad inválida.");
                return;
            }

            usuario.Edad = edad;

            // 4. Llamar usuarioDAL.Actualizar(usuario)
            bool resultado = usuarioDAL.Actualizar(usuario);

            // 5. Mostrar mensaje de éxito o error
            if (resultado)
                Console.WriteLine("Usuario actualizado correctamente.");
            else
                Console.WriteLine("Error al actualizar usuario.");
        }

        static void EliminarUsuario()
        {
            // El estudiante debe:
            // 1. Pedir ID
            Console.Write("Ingrese ID del usuario: ");

            if (!int.TryParse(Console.ReadLine(), out int id))
            {
                Console.WriteLine("ID inválido.");
                return;
            }

            // 2. Buscar si el usuario existe
            var usuario = usuarioDAL.ObtenerPorId(id);

            if (usuario == null)
            {
                Console.WriteLine("Usuario no encontrado.");
                return;
            }

            // 3. Confirmar eliminación
            Console.Write($"¿Seguro que desea eliminar a {usuario.Nombres}? (S/N): ");
            string confirmacion = Console.ReadLine();

            if (confirmacion.ToUpper() != "S")
            {
                Console.WriteLine("Operación cancelada.");
                return;
            }

            // 4. Llamar usuarioDAL.Eliminar(id)
            bool resultado = usuarioDAL.Eliminar(id);


            // 5. Mostrar mensaje según el resultado

            if (resultado)
                Console.WriteLine("Usuario eliminado correctamente.");
            else
                Console.WriteLine("Error al eliminar usuario.");
        }
    }
}