# DentallApp BackEnd

DentallApp is a web application with chatbot for appointment management, reminders and sending appointment cancellation messages for the dental clinic called [World Dental CO](https://www.tiktok.com/@worlddentalco).

## Index

- [Important](#important)
- [Technologies used](#technologies-used)
- [Software Engineering](#software-engineering)
- [Installation](#installation)
- [Plugin configuration](#plugin-configuration)
- [Diagrams](#diagrams)
  - [Relational model](#relational-model)
  - [General architecture](#general-architecture)

## Important

This application was developed as a degree project for the [University of Guayaquil](https://www.ug.edu.ec), however, it is not ready to run in a production environment. All requirements for this project were obtained through interviews with the owner dentist of [World Dental CO](https://www.facebook.com/worlddentalco).

In the end, this project was never deployed in that dental office for personal reasons of the authors. However, it was decided to publish the source code of this application so that the community can use it for learning purposes (learn from it or even improve it).

## Technologies used

List of frameworks and libraries used for this project:
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

Testing:
- [NUnit](https://github.com/nunit/nunit)
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions)
- [JustMock](https://github.com/telerik/JustMockLite)
- [Microsoft.Bot.Builder.Testing](https://www.nuget.org/packages/Microsoft.Bot.Builder.Testing)

List of own libraries for this project:
- [DotEnv.Core](https://github.com/MrDave1999/dotenv.core)
- [YeSql.Net](https://github.com/ose-net/yesql.net)
- [CPlugin.Net](https://github.com/MrDave1999/CPlugin.Net)
- [SimpleResults](https://github.com/MrDave1999/SimpleResults)

## Software Engineering

Software engineering concepts have been applied in this project:
- [Vertical Slice Architecture](https://garywoodfine.com/implementing-vertical-slice-architecture)
- [CQRS](https://en.wikipedia.org/wiki/Command_Query_Responsibility_Segregation)
- [Plugin Architecture](https://www.devleader.ca/2023/09/07/plugin-architecture-design-pattern-a-beginners-guide-to-modularity)
- [Open–closed principle](https://en.wikipedia.org/wiki/Open%E2%80%93closed_principle)
- [Modular programming](https://en.wikipedia.org/wiki/Modular_programming)
- [Separation of concerns](https://en.wikipedia.org/wiki/Separation_of_concerns)
- [Interface-based programming](https://en.wikipedia.org/wiki/Interface-based_programming)

## Installation

To run this application, it is recommended to install [Docker](https://docs.docker.com/get-docker), it is much easier to install the app with [Docker](https://docs.docker.com/get-docker).

- Clone the repository with this command:
```sh
git clone https://github.com/DentallApp/back-end.git
```
- Change directory:
```sh
cd back-end
```
- Copy the contents of `.env.example` to `.env`:
```sh
cp .env.example .env
# On Windows use the "xcopy" command.
```
- Build the image and initiate services:
```sh
docker-compose up --build -d
```
- Access the application with this URL:
```
http://localhost:5000/swagger
```
- If you wish to test the chatbot, you can do so with the [test client](https://github.com/DentallApp/webchat-client). Access this URL:
```
https://dentallapp.github.io/webchat-client
```

**NOTE:** Twilio.WhatsApp and SendGrid (these are plugins) are not loaded by default. So the app will use a fake provider that uses a console logger.

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

## Diagrams

### Relational model

<details>
<summary><b>More details</b></summary>

![relational-model](https://github.com/DentallApp/back-end/blob/docs/README/diagrams/relational-model.png)

</details>

### General architecture

<details>
<summary><b>More details</b></summary>

</details>