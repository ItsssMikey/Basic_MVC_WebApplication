

Basic Quiz MVC Web Application (QuizApp)
Et grunnleggende webapplikasjonsprosjekt utviklet med ASP.NET Core MVC for å demonstrere bruk av Model-View-Controller-arkitekturen og Entity Framework Core for databasetilkobling. Prosjektet fungerer som en Quiz-applikasjon med fokus på å implementere et Minimum Viable Product for innholdsstyring. Kan kjøres ved å åpne terminalen, cd <path til prosjekt>, dotnet run, og deretter gå inn på localhost link

MVP-Funksjonalitet
Denne versjonen av applikasjonen fokuserer på de grunnleggende CRUD-operasjonene (Create, Read, Update og Delete) for quiz-objekter.

Programmer og Teknologi Brukt
Rammeverket for prosjektet er bygd på ASP.NET Core MVC med C# som hovedspråk. Entity Framework Core er brukt for å administrere databasen, og frontend er standard webspråk slik som HTML og CSS (og noe JS).

Prosjektstruktur
QuizApp.Domain - Inneholder kjerneobjektene og forretningsreglene

QuizApp.Data - Ansvarlig for data-tilgang og inneholder DbContext, EF Core migrasjoner og repository-implementasjoner

QuizApp.Core - Kjører flyten mellom Data og Domain

QuizApp - Det som presenteres. Altså Views, Controllere og statiske filer

Refleksjon rundt AI-Bruk
Dette prosjektet har aktivt brukt AI til å lage en grunnleggende mappestruktur og navngivning etter å ha beskrevet MVP. AI er også brukt for feilsøking og bearbeiding av kode når vi har møtt på hindringer og at koden ikke kjører.
AI-bruk er litt av grunnen til at koden er på både norsk og engelsk, men det er hovedsaklig det at noen på gruppa skrev kode på norsk og andre på engelsk, da vi ikke hadde tenkt over å fastslå et spesifikt språk på navn av variabler og slikt. 
På selve det som kommer opp på skjermen til en bruker av applikasjonen, så skal alt kun være engelsk. Det er kun det interne språket som ikke er konsistent. Hele CSS filen er AI. CSS er noe vi pioriterte bort, og
dersom det er krav om egen CSS-fil, så skal vi så klart få gjort de nødvendige endringene.

