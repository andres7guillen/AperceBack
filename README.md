# AperceBack
El estado de una tarea no es un campo editable, es una consecuencia de ejecutar una acción del dominio. Si expongo PUT /status, estoy permitiendo que el cliente intente cualquier transición y luego validarla. Con endpoints por intención, la API expresa el lenguaje del dominio, elimina estados inválidos desde el contrato y mantiene la consistencia dentro del aggregate.

# para ekecutar la app:
Abrir Package Manager Console apuntando al proyecto Data y ejecutar el comando: Update-Database -Migration initDb
o puede ejecutar el script de creacion de DB en el repositorio de Git.
# Pendientes:
Manejo de JSON en SQL Server
