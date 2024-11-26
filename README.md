# Farmacia CRUD - Challenge Verifarma

## Descripción del Proyecto

Este proyecto consiste en una API para la gestión de farmacias, construida utilizando **.NET 8** y diseñada bajo un enfoque de Clean architecture. La solución permite realizar operaciones CRUD sobre la entidad de farmacia y permite traer farmacia por cercania.
El proyecto también incluye la configuración para ejecutarse mediante Docker, con un contenedor para la API y otro para la base de datos.

## Tecnologías Utilizadas

- **.NET 8**
- **Entity Framework Core**
- **SQL Server**
- **Docker** y **Docker Compose**
- **Serilog** para logging
- **Swagger** para documentacion
- **XUnit** para testing

## Requisitos 

Tener instalado de antes:

- [Docker](https://www.docker.com/) 
- [Docker Compose](https://docs.docker.com/compose/)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) o cualquier IDE compatible con .NET 8
- [.NET 8 SDK](https://dotnet.microsoft.com/)

## Instalación y Configuración

1. **Clonar el Repositorio:**

   ```bash
   git clone https://github.com/tuusuario/farmacia-api-dotnet.git
   cd FarmaciaVerifarmaChallenge
   ```

2. **Configurar la Base de Datos:**

   La aplicación utiliza SQL Server como base de datos. El archivo `docker-compose.yml` ya contiene la configuración necesaria para levantar una instancia de SQL Server. Pero si asi se desea se puede configurar desde ese mismo archivo o levantarlo localmente y configurar la connection string desde el appsettings

3. **Ejecutar Docker Compose:**

   Para iniciar los contenedores, es necesario el siguiente comando:

   ```bash
   sudo docker-compose up --build
   ```

   Este comando realiza las siguientes acciones:
   - **Construye y levanta dos contenedores**:
     - Un contenedor que ejecuta la API desarrollada en .NET 8.
     - Un contenedor que ejecuta SQL Server como base de datos "FarmaciaDB".
   - **Redirige los puertos configurados en el archivo `docker-compose.yml`**:
     - API: `http://localhost:8080`
     - Base de datos: Puerto 1433.
    
  Luego para finalizarlo con ctrol c y tambien:
  ```bash
  sudo docker-compose down
  ```

4. **Acceso a la API:**

   Una vez que los contenedores estén en ejecución, puedes acceder a los endpoints de la API en `http://localhost:8080` o a través de Swagger en `http://localhost:8080/swagger/index.html`.

## Endpoints Principales

| Método | Endpoint          | Descripción              |
|--------|-------------------|--------------------------|
| GET    | `/api/farmacias`  | Listar todas las farmacias |
| GET    | `/api/farmacias/{id}` | Obtener una farmacia por ID |
| POST   | `/api/farmacias`  | Crear una nueva farmacia |
| GET    | `/api/farmacias/{id}` | Obtener una farmacia por ID |

## Arquitectura

La aplicación sigue una **Clean architecture**, separando responsabilidades/capas en:

1. **Domain**: Define las entidades y reglas del dominio.
2. **Application**: Contiene la lógica de negocio y las interfaces.
3. **Infrastructure**: Implementa los repositorios, migraciones y la interacción con la base de datos utilizando EF.
4. **Web API**: Expone la lógica mediante endpoints REST.

## Configuración de Docker Compose

Ejemplo de `docker-compose.yml`:

```yaml

services:
  api:
    build:
      context: .
      dockerfile: src/FarmaciaVerifarmaChallenge.API/Dockerfile
    ports:
      - "5000:5000"
    depends_on:
      - db
    environment:
      - ConnectionStrings__DefaultConnection=Server=db;Database=FarmaciaDB;User=sa;Password=Your_password123;

  db:
    image: mcr.microsoft.com/mssql/server:2022-latest
    ports:
      - "1433:1433"
    environment:
      SA_PASSWORD: "Your_password123"
      ACCEPT_EULA: "Y"
    volumes:
      - dbdata:/var/opt/mssql

volumes:
  dbdata:
```
