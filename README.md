# Utilizando Self-hosting Windows Communication Foundation y PostgreSQL

Windows Communication Foundation es la parte del .NET Framework que define un modelo de programación común y una API unificada basada en estándares industriales de comunicación para construir aplicaciones distribuidas interoperables que se comunican entre sí mediante el intercambio de mensajes y así cubrir la mayor parte de los requerimientos en computación distribuida.

WCF incluye todas las tecnologías de comunicación abiertas como XML, SOAP y JSON y propietarias de Microsoft como MSMQ, COM+ y .NET Remoting.

Aunque en teoría se pueden construir servicios sin WCF, en la práctica utilizar WCF para construir servicios es mucho más práctico que hacerlo desde cero (start from scratch).

En resumen WCF consiste de un número de bibliotecas .NET que se utilizan para aplicaciones orientadas a servicios.

Antes de empezar con el ejemplo es importante tener claras algunas definiciones que son necesarias para entender el funcionamiento de WCF:

Endpoint: En los sistemas orientados a servicios, es el lugar en donde los mensajes van y vienen cada endpoint requiere de tres elementos: Address, Binding y Contract. Pueden ser especificados de forma imperativa o declarativa.
Address: En donde el servicio se encuentra disponible. Es un URL que se puede formar de 4 partes: Schema, machine, port y path.
Binding: Como se puede acceder al servicio.
Contract: Que operaciones ofrece el servicio.
Servicios: Son procesos independientes definidos por su interfaz.
Componentes: son los bloques de construcción a nivel plataforma tales como las bibliotecas (.dll) que se ensamblan en tiempo de diseño o en tiempo de ejecución.
Objetos: Son los bloques de construcción del nivel más bajo que se encuentran dentro de los componentes y los servicios.
Hay que tener en cuenta que no importa que protocolo se use para el intercambio de mensajes desde las aplicaciones hasta el servicio, WCF siempre representará el mensaje como un objeto Message.

En resumen los servicios WCF son solo objetos que exponen una serie de operaciones que las aplicaciones clientes necesitan invocar. Cuando se construye un servicio se describen estas operaciones mediante un contrato (una interfaz) que debe tener una implementación (clase).

Para que el servicio esté disponible a los clientes se debe de instalar en un entorno de ejecución que hospede el servicio. Hay varias formas en las que puedes hospedar el servicio, puedes usar Internet Information Services (IIS), Windows Activation Services (WAS) y self-host managed applications como una consola de windows o un servicio de Windows.

En el siguiente ejemplo mostraré como utilizar los plantillas de Visual Studio para construir una aplicación con la técnica de self-hosting en la cual el desarrollador es responsable de implementar y administrar el ciclo de vida del proceso de alojamiento.

Este ejemplo utiliza un servicio WCF que realiza la búsqueda del nombre de un producto en una tabla de productos dentro base de datos postgreSQL.

Aquí el script de la tabla de productos



1-.Abrimos Visual Studio y en el explorador de soluciones agregamos un proyecto del tipo WCF Service Library.

2-. Nombramos este proyecto como: Test.WCF.Services



3-.A este proyecto le agregamos dos archivos: la interfaz IProductsContract y la clase ProductsImplementation.

Aquí el código de IProductsContract



Aquí el código de ProductsImplementation.



En este código utilizamos unos ensamblados llamados Tests.WCF.Services.Objects y Tests.WCF.Services.Data que contienen el código de datos y un DTO, este código lo omito en la explicación para centrarme en la construcción del WCF, sin embargo el proyecto completo lo incluyo en el código fuente de la solución.

4-. Agregamos un proyecto del tipo aplicación de consola a nuestra solución, con el nombre: ConsoleHostingService



5-. Tecleamos el siguiente código en la clase Program:



Este código muestra la construcción de un Self-Hosted Service , en la línea siguiente creo el Uniform Resource identifier representado por la clase URI.

 Uri baseAddress = new Uri("http://localhost:8004/");
Después asigno el tipo de objeto de la implementación a una variable.

 Type service = typeof(Tests.WCF.Services.ProductsImplementation);
Ahora hago una instancia de la clase ServiceHost que es la clase que proporciona WCF para alojar endpoints de WCF en las aplicaciones .NET, una vez teniendo la instancia hay que decirle el tipo del servicio que debe inicializarse para recibir las peticiones así como establecerle el endpoint para que los cliente sepan donde enviar las peticiones.

ServiceHost host = new ServiceHost(service, baseAddress);
Bien ahora hay que invocar el método host.Open(); para cargar el runtime de WCF y empezar a escuchar las peticiones.

using (host)
      {
        Type contract = typeof(Tests.WCF.Services.IProductsContract);
        host.AddServiceEndpoint(contract, new WSHttpBinding(), "Products");
        host.Open();
        Console.WriteLine("Products service running.Press  to quit.");
        Console.ReadLine();
        host.Close();
     }
La flexibilidad de self-hosting aplica cuando las aplicaciones son (in-proc), esto es que las aplicaciones están en el mismo proceso que el cliente.

Aquí el código del programa cliente.


Es importante que antes de ejecutar el programa cliente, se ejecute primero el programa que contiene el proceso ServiceHost esto lo podemos configurar en VS, en el submenú Set Startup Projects debajo del menú Debug en VS.


Aquí la búsqueda en la tabla utilizando el Query Management de PgAdmin


Ahora ejecutamos la misma búsqueda, solo que remotamente con la solución de VS, primero ejecutamos el proyecto ConsoleHostingService.

Inmediatamente después ejecutamos el proyecto ConsoleHostingClient.


Iniciamos con la búsqueda de productos, tecleamos la coincidencia a buscar y pulsamos ENTER, esto hará que se mande la petición al servicio WCF.

