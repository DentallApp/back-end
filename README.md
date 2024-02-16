# back-end

DentallApp is a web application with chatbot for appointment management, reminders and sending appointment cancellation messages for the dental office called [World Dental CO](https://www.tiktok.com/@worlddentalco).

> The maintainer of this repository is [Dave Roman](https://github.com/MrDave1999).
>
> This project has been improved too much since its [first alpha version](https://github.com/DentallApp/back-end/tree/v0.1.0). 

## Index

- [Important](#important)
- [Motivations](#motivations)
- [Technologies used](#technologies-used)
  - [Softwares](#softwares)
  - [Frameworks and libraries](#frameworks-and-libraries)
  - [Testing](#testing)
  - [Own libraries](#own-libraries)
- [Software Engineering](#software-engineering)
- [Installation](#installation)
- [Plugin configuration](#plugin-configuration)
- [Credentials](#credentials)
- [Diagrams](#diagrams)
  - [General architecture](#general-architecture)
  - [Relational model](#relational-model)
- [Direct Line API](#direct-line-api)

## Important

This application was developed as a degree project for the [University of Guayaquil](https://www.ug.edu.ec), however, it is not ready to run in a production environment. All requirements for this project were obtained through interviews with the owner dentist of [World Dental CO](https://www.facebook.com/worlddentalco).

In the end, this project was never deployed in that dental office for personal reasons of the authors. However, it was decided to publish the source code of this application so that the community can use it for learning purposes (learn from it or even improve it).

## Motivations

I have continued to maintain this project because I have been experimenting with plugin-based architecture and I love it.

I have not found any .NET project that has applied this architecture and I don't mean a sample project, but one that solves a problem. For that reason I decided to apply it in this project, I'm sure many will find it useful as knowledge.

Another of my reasons is that what I learn about software engineering, I like to share with the community. That's why I have been inspired to improve it.

> The best way to learn things is to do projects.

## Technologies used

### Softwares
- [.NET CLI](https://learn.microsoft.com/en-us/dotnet/core/tools)
- [Docker](https://github.com/docker)
- [Postman](https://www.postman.com)
- [InDirectLine](https://github.com/newbienewbie/InDirectLine)
- [MariaDB](https://github.com/mariadb)
- [BotFramework-Emulator](https://github.com/microsoft/BotFramework-Emulator)
- [Git](https://git-scm.com)
- [draw.io](https://app.diagrams.net)

### Frameworks and libraries
- [ASP.NET Core](https://github.com/dotnet/aspnetcore)
- [Microsoft Bot Framework](https://github.com/microsoft/botframework-sdk)
- [AdaptiveCards](https://github.com/microsoft/AdaptiveCards)
- [Twilio](https://github.com/twilio/twilio-csharp)
- [SendGrid](https://github.com/sendgrid/sendgrid-csharp)
- [Quartz.NET](https://github.com/quartznet/quartznet)
- [Swashbuckle.AspNetCore](https://github.com/domaindrivendev/Swashbuckle.AspNetCore)
- [Scrutor](https://github.com/khellang/Scrutor)
- [efcore](https://github.com/dotnet/efcore)
- [Pomelo.EntityFrameworkCore.MySql](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql)
- [EFCore.NamingConventions](https://github.com/efcore/EFCore.NamingConventions)
- [linq2db.EntityFrameworkCore](https://github.com/linq2db/linq2db.EntityFrameworkCore)
- [EntityFramework.Exceptions](https://github.com/Giorgi/EntityFramework.Exceptions)
- [EFCore.CustomQueryPreprocessor](https://github.com/MrDave1999/EFCore.CustomQueryPreprocessor)
- [DelegateDecompiler](https://github.com/hazzik/DelegateDecompiler)
- [Dapper](https://github.com/DapperLib/Dapper)
- [Microsoft.AspNetCore.Authentication.JwtBearer](https://www.nuget.org/packages/Microsoft.AspNetCore.Authentication.JwtBearer)
- [Microsoft.IdentityModel.Tokens](https://www.nuget.org/packages/Microsoft.IdentityModel.Tokens)
- [System.IdentityModel.Tokens.Jwt](https://www.nuget.org/packages/System.IdentityModel.Tokens.Jwt)
- [BCrypt.Net-Next](https://github.com/BcryptNet/bcrypt.net)
- [Scriban](https://github.com/scriban/scriban)
- [itext7.pdfhtml](https://github.com/itext/i7n-pdfhtml)
- [File.TypeChecker](https://github.com/AJMitev/FileTypeChecker)
- [FluentValidation](https://github.com/FluentValidation/FluentValidation)

### Testing
- [NUnit](https://github.com/nunit/nunit)
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions)
- [JustMock](https://github.com/telerik/JustMockLite)
- [Microsoft.Bot.Builder.Testing](https://www.nuget.org/packages/Microsoft.Bot.Builder.Testing)

### Own libraries
- [DotEnv.Core](https://github.com/MrDave1999/dotenv.core)
- [YeSql.Net](https://github.com/ose-net/yesql.net)
- [CPlugin.Net](https://github.com/MrDave1999/CPlugin.Net)
- [SimpleResults](https://github.com/MrDave1999/SimpleResults)

## Software Engineering

Software engineering concepts have been applied in this project:
- [Vertical Slice Architecture](https://garywoodfine.com/implementing-vertical-slice-architecture)
- [CQRS](https://en.wikipedia.org/wiki/Command_Query_Responsibility_Segregation)
- [Plugin Architecture](https://www.linkedin.com/pulse/plugin-architecture-design-pattern-beginners-guide-nick-cosentino)
- [Open–closed principle](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle)
- [Modular programming](https://en.wikipedia.org/wiki/Modular_programming)
- [Separation of concerns](https://en.wikipedia.org/wiki/Separation_of_concerns)
- [Interface-based programming](https://en.wikipedia.org/wiki/Interface-based_programming)

## Installation

To run this application, it is recommended to install [Docker](https://docs.docker.com/get-docker), it is much easier to install the app with [Docker](https://docs.docker.com/get-docker).

- Clone the repository with this command.
```sh
git clone https://github.com/DentallApp/back-end.git
```
- Change directory.
```sh
cd back-end
```
- Copy the contents of `.env.example` to `.env`.
```sh
cp .env.example .env
# On Windows use the "xcopy" command.
```
- You must specify the [time zone](https://en.wikipedia.org/wiki/List_of_tz_database_time_zones) for the Docker container. This is necessary for the calculation of available hours for a medical appointment to return consistent results.
The logical thing to do would be to choose the time zone in which the dental clinic is located (which in this case would be **America/Guayaquil**).
```sh
echo -e '\nTZ=America/Guayaquil' >> .env
```
- Build the image and initiate services.
```sh
docker-compose up --build -d
```
- Access the application with this URL.
```
http://localhost:5000/swagger
```
- If you wish to test the chatbot, you can do so with the [test client](https://github.com/DentallApp/webchat-client). Access this URL.
```
https://dentallapp.github.io/webchat-client
```

**NOTE:** Twilio.WhatsApp and SendGrid (these are plugins) are not loaded by default. So the app will use a fake provider that uses a console logger (useful for a development environment).

## Plugin configuration

By default only two plugins are loaded:
- `Plugin.ChatBot.dll`
- `Plugin.AppointmentReminders.dll`

You can add other plugins by modifying the [PLUGINS](https://github.com/DentallApp/back-end/blob/d2dfdbd2a75b14be0ff87f531abc367040d87691/.env.example#L10-L13) key from the .env file:
```.env
PLUGINS="
Plugin.ChatBot.dll
Plugin.AppointmentReminders.dll
Plugin.Twilio.WhatsApp.dll
Plugin.SendGrid.dll
"
```
Of course, for this to work, you will need to create an account on [Twilio](https://www.twilio.com/en-us) and [SendGrid](https://sendgrid.com/en-us), generate the necessary credentials and then add it to the .env file.

You can also remove all plugins. The host application will work without any problems. 

## Credentials

The following table shows the default credentials for authentication from the application.

| Username                | Password                    |
|-------------------------|-----------------------------|
| basic_user@hotmail.com  | 123456                      |
| secretary@hotmail.com   | 123456                      |
| dentist@hotmail.com     | 123456                      |
| admin@hotmail.com       | 123456                      |
| superadmin@hotmail.com  | 123456                      |

Use this route for authentication:
```
POST - /api/user/login
```
Request body:
```json
{
  "userName": "basic_user@hotmail.com",
  "password": "123456"
}
```

## Diagrams

### General architecture

<details>
<summary><b>More details</b></summary>

![general-architecture](https://github.com/DentallApp/back-end/blob/dev/diagrams/general-architecture.png)

</details>

#### Overview of each component
- **Host Application.** Contains everything needed to run the application. It represents the entry point of the application.
  This layer performs other tasks such as:
  - Load plugins from a configuration file (.env) using the library called [CPlugin.Net](https://github.com/MrDave1999/CPlugin.Net).
  - Finds the types that implement the interfaces shared between the host application and the plugins to create instances of those types.
  - Add services to the service collection, register middleware, load SQL files, load the .env file, among other things.
- **Shared Layer.** It contains common classes and interfaces between many components. 
  - This layer contains the interfaces that allow communication between the host application and the plugins.
  - This layer does not contain the implementation of a functional requirement.
  - It contains other things such as:
    - Extension classes
    - Classes mapped to the database schema (entities)
    - Data models
    - Value objects
    - Objects that represent error and success messages
    - Language resources 
    - Custom validators
    - Repository and service interfaces
- **Core Layer.** Contains the essential features of the application, it is like the heart of the system where the functional requirements are implemented. 
  - Each feature represents a functional requirement of what the app should do. 
  - A feature contains the minimum code to execute a functional requirement. 
  - The purpose of grouping related elements of a feature is to increase cohesion.
- **Infrastructure Layer.** Contains the implementation (concrete classes) of an interface defined in the shared layer. 
  - The purpose of this layer is to hide external dependencies that you do not have control over.
  - This layer is useful because it avoids exposing third party dependencies to other components, so if the dependency is changed/removed it should not affect any other component.
  - Not all third party dependencies are added in this layer. For example, Entity Framework Core is used directly in the features to avoid introducing more complexity.
- **ChatBot.** It is an plugin that allows a basic user to schedule appointments from a chatbot.
- **Appointment Reminders.** It is a plugin that allows to send appointment reminders to patients through a background service.
- **SendGrid Email.** It is a plugin that allows to send emails in cases such as:
  - When a customer registers in the application, an email is sent to confirm the user's email address.
  - When a user wants to reset their password, an email is sent with the security token.
- **Twilio WhatsApp.** It is a plugin that allows to send messages by whatsapp in cases such as:
  - When an appointment is scheduled from the chatbot, the user is sent the appointment information to whatsapp.
  - When an employee needs to cancel an appointment, he/she should notify patients by whatsapp.

### Relational model

<details>
<summary><b>More details</b></summary>

![relational-model](https://github.com/DentallApp/back-end/blob/dev/diagrams/relational-model.png)

</details>

## Direct Line API

[Direct Line API](https://learn.microsoft.com/en-us/azure/bot-service/rest-api/bot-framework-rest-direct-line-3-0-api-reference) allows your client application to communicate with the bot. It acts as a bridge between the client and the bot.

For development and test environments you can use [InDirectLine](https://github.com/newbienewbie/InDirectLine) to avoid having to use Azure. [InDirectLine](https://github.com/newbienewbie/InDirectLine) is a bridge that implements the Direct Line API, but should not be used for production.

By default, the configuration file (.env) contains a key called `DIRECT_LINE_BASE_URL`.
```.env
DIRECT_LINE_BASE_URL=http://indirectline:3000/
```
The provider called [InDirectLine](https://github.com/newbienewbie/InDirectLine) is used by default.

In production, the value of this key must be changed to:
```.env
DIRECT_LINE_BASE_URL=https://directline.botframework.com/
```
In that case the provider to use will be the Direct Line channel of Azure Bot. The backend application is able to switch providers just by reading the URL.