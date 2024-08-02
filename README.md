# ACME-School-Management

At the ACME school they need help with the management of their courses and students.
They have asked us for a program that is capable of the following:

* Register a student specifying their name and age (only adults can register).
* Register courses with a name, registration fee, start date, and end date.
* Allow a student to contract a course, after payment if applicable, of the registration fee
through a payment gateway.
* A list of courses that have occurred between a date range and their students.

<br>

Se implementa una arquitectura en capas cumpliendo los principios SOLID.
Al utilizar CQRS y el patron Mediator logramos separar las operaciones de consulta de los comandos que modifican el estado del dominio y a su vez desacoplar los componentes de la libreria.

### A mejorar
Se podrian incorporar algunos Unit Test más.
Hacer una implementacion de las entidades de response que contenga manejo de status y errores.

### Librerias
Para implementar el patron mediator se utilizó la libreria MediatR, para pruebas unitarias Moq, Xunit y FluentAssertions, para validaciones FluentValidation.
