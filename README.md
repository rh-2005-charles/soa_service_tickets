# Proyecto IC Tienda

Este es un sistema de gestión para una tienda en línea.

## Requisitos

- .NET 8.0
- MySQL

## Instalación

1. Clona el repositorio:

   ```bash
   git clone https://github.com/rh-2005-charles/soa_service_tickets.git
   ```

2. Restaura los paquetes NuGet:

   ```bash
   dotnet restore
   ```

3. Copila todo el proyecto:

   ```bash
   dotnet build
   ```

4. Crear un archivo

   wwwroot/images

## Comandos Utiles

1. Instalación de dotnet ef:

   ```bash
   dotnet tool install --global dotnet-ef
   ```

2. Para realizar una migración:

   ```bash
   dotnet ef migrations add NombreMigracion
   ```

3. Para actualizar la base de datos:

   ```bash
   dotnet ef database update
   ```

4. Para ejecutar:

   ```bash
   dotnet watch run --launch-profile https
   ```

   ```bash
   dotnet watch run
   ```

   ```bash
   dotnet  run
   ```

## Comandos para usar Secrets (ejecutar donde esta el program)

1. Iniciar y crear archivo secrets.json

   ```bash
   dotnet user-secrets init
   ```

2. Para listar los secrets

   ```bash
   dotnet user-secrets list
   ```

3. Para agregar secretos

   ```bash
   dotnet user-secrets set "Email:Host" "smtp.gmail.com"
   ```

4. Para Eliminar secretos

   ```bash
   dotnet user-secrets remove "Email:Password"
   ```

## Comandos para Docker Desktop

1. Listar docker containers active

   ```bash
    docker ps
   ```

2. Listar docker containers desactive

   ```bash
    docker ps -a
   ```

3. Detener el contenedor (si está corriendo)

   ```bash
    docker stop <container_name_or_id>
   ```

4. Eliminar el contenedor

   ```bash
    docker rm <container_name_or_id>
   ```

5. Para Volúmenes

   ```bash
    docker volume ls  # Lista todos los volúmenes
   ```

   ```bash
    docker volume rm <volume_name>  # Elimina un volumen específico
   ```

6. Para subir nuevo

   ```bash
    docker compose up -d
   ```

7. Para eliminar mis contenedores

   ```bash
    docker compose down
   ```

8. Para levantar

   ```bash
    docker compose up --build
   ```
9. 
