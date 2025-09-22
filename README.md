📝 Descripción del proyecto
UserManagment es una API RESTful construida con C# y .NET, diseñada para gestionar usuarios mediante operaciones CRUD (Crear, Leer, Actualizar, Borrar). El proyecto proporciona una solución simple y robusta para la administración de datos de usuarios en una base de datos.

✨ Funcionalidades
La API expone los siguientes endpoints, cada uno con una función específica:

1. Alta de usuario
Método: POST

Endpoint: /api/Users

Descripción: Crea un nuevo registro de usuario en la base de datos.

Validaciones:

Valida los datos de entrada como nombre, email, y los datos de domicilio si fueron cargados.

Almacena el usuario y el domicilio con la fecha de creación actual.

2. Búsqueda de usuario
Método: GET

Endpoint: /api/Users

Descripción: Permite buscar usuarios por nombre, provincia o ciudad (provistos en el body de la request).

Respuesta: Devuelve el nombre del usuario junto con los datos del domicilio si están cargados.

3. Baja de usuario
Método: DELETE

Endpoint: /api/Users/{id}

Descripción: Elimina un usuario existente de la base de datos.

Parámetro: Utiliza el ID del usuario como identificador para la eliminación.

4. Modificación de usuario
Método: PUT

Endpoint: /api/Users/{id}

Descripción: Modifica los datos del domicilio un usuario existente.

Parámetro: Utiliza el ID del usuario para identificar el registro y los nuevos datos de domicilio del body de la request.

Funcionalidad:

Permite editar y agregar datos del domicilio.

Validación: valida que todos los campos del domicilio estén completos.

🛠️ Tecnologías utilizadas
Lenguaje: C#

Framework: .NET 8

Base de datos: MySql

ORM: Entity Framework Core

🚀 Instalación y uso
Clona el repositorio:

Bash

git clone https://github.com/tu-usuario/UserManagment.git
Abre el proyecto en Visual Studio o Visual Studio Code:

Navega a la carpeta del proyecto.

Abre la solución (.sln).

Configura la cadena de conexión a la base de datos:

Modifica el archivo appsettings.json o appsettings.Development.json con tu cadena de conexión.

Ejecuta las migraciones (si usas Entity Framework Core):

Bash

dotnet ef database update
Ejecuta la aplicación:

Presiona F5 en Visual Studio o usa el siguiente comando en la terminal:

Bash

dotnet run
La API estará disponible en el puerto especificado en la configuración (el mismo aparece en la consola de salida).