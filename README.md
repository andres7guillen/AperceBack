# AperceBack
El estado de una tarea no es un campo editable, es una consecuencia de ejecutar una acción del dominio. Si expongo PUT /status, estoy permitiendo que el cliente intente cualquier transición y luego validarla. Con endpoints por intención, la API expresa el lenguaje del dominio, elimina estados inválidos desde el contrato y mantiene la consistencia dentro del aggregate.

# para ejecutar la app:
Abrir Package Manager Console apuntando al proyecto Data y ejecutar el comando: Update-Database -Migration initDb
o puede ejecutar el script de creación de DB en el repositorio de Git.
en los local storage cambiar la cadena de conexión apuntando a la base de datos local que tenga.
Restaurar todos los paquetes Nuget de la solución.
# Pendientes:
Manejo de JSON en SQL Server
