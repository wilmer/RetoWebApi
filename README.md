"# ProductWebApi" 

Este proyecto es un trabajo para crear una Web Api usando NetCore y Carter con una arquitectura N capas
# Technologías y patrones usados

- API
  - [NetCore with .NET 8]
  - [Vertical Slice Architecture]
  - CQRS with [MediatR]
  - [FluentValidation]
  - [AutoMapper]
  - [Entity Framework Core 8]
  - Swagger with Code generation using [NSwag]
  - Logging with [Serilog]
  - [Decorator](https://refactoring.guru/design-patterns/decorator) pattern using PipelineBehaviors

- Pruebas
  - [NUnit](https://nunit.org/)
  - [FluentAssertions](https://fluentassertions.com/)
  - [Respawn](https://github.com/jbogard/Respawn)


# Principios de diseño común

- Separation of concerns
- Encapsulation
- Explicit dependencies
- Single responsibility
- Persistence ignorance*


# Inicio

The easiest way to get started is using Visual Studio 2022 y Base de datos Sql Server.

# Database Migrations

Ejecutamos el comando Update-DataBase en la capa Application, verificar previamente la cadena de conexión de base de datos.


# Capas de la aplicación

## API

Se aloja la aplicación y conecta todas las dependencias

## Application

Contiene el Core y la infraestructura.

### Domain

Contiene todas las entidades, enumeraciones, excepciones, interfaces, tipos y lógica específicos de la capa de dominio (esta capa se comparte entre todas las funciones).

Podemos tener eventos de dominio, lógica empresarial, objetos de valor, etc.

### Infrastructure

Esta capa contiene clases para acceder a recursos externos.

### Features

Esta carpeta contiene toda la funcionalidad. 

#Tests

Contiene test unitarios y test de integración.