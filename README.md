# Portafolio - Backend y Frontend

Este repositorio contiene el código fuente para el backend en ASP.NET Core y el frontend en React para un portafolio personal.



Primero crear la base de datos

CREATE DATABASE test4;
CREATE TABLE Profile (
    ProfileId INT PRIMARY KEY,
    Name NVARCHAR(255),
    Lastname NVARCHAR(255),
    Email NVARCHAR(255),
    City NVARCHAR(255),
    Country NVARCHAR(255),
    Summary NVARCHAR(MAX),
    Instagram NVARCHAR(255),
    Facebook NVARCHAR(255),
    YearsOld INT
);

CREATE TABLE Framework (
    FrameworkId INT PRIMARY KEY,
    Name NVARCHAR(255),
    Level NVARCHAR(255),
    Year INT,
    Quantity INT,
    ProfileId INT FOREIGN KEY REFERENCES Profile(ProfileId)
);

CREATE TABLE Hobby (
    HobbyId INT PRIMARY KEY,
    Name NVARCHAR(255),
    Description NVARCHAR(MAX),
    ProfileId INT FOREIGN KEY REFERENCES Profile(ProfileId)
);


Posteriormente cambiar el archivo appsettings.json

Yagregar datos a la base de datos.

## Configuración del Backend (ASP.NET Core)

1. Abre el proyecto en tu entorno de desarrollo preferido (Visual Studio, Visual Studio Code, etc.).

2. Asegúrate de tener [.NET SDK](https://dotnet.microsoft.com/download) instalado.

3. Restaura las dependencias del proyecto:

    ```bash
    dotnet restore
    ```

4. Ejecuta la aplicación:

    ```bash
    dotnet run
    ```

5. La API estará disponible en `http://localhost:5065/swagger/index.html`.

## Configuración del Frontend (React)

1. Abre la carpeta del frontend en tu terminal.

    ```bash
    cd frontend
    ```

2. Asegúrate de tener [Node.js](https://nodejs.org/) y [npm](https://www.npmjs.com/) instalados.

3. Instala las dependencias del proyecto:

    ```bash
    npm install
    ```

4. Inicia la aplicación React:

    ```bash
    npm start
    ```

5. El frontend estará disponible en `http://localhost:3000` por defecto.

## Uso

### Endpoint de la API

El backend proporciona un endpoint:

- **URL:** `http://localhost:5000/api/profile`
- **Método:** GET
- **Respuesta Esperada:** Un JSON con información del perfil, que incluye nombre, apellido, email, ciudad, país, resumen, frameworks y hobbies.

### Interacción con el Frontend

El frontend consume el endpoint de la API y muestra la información del perfil en la URL `/portfolio`. Accede a esta URL en tu navegador para ver el portafolio personal.
