

Basic Quiz MVC Web Application (QuizApp)
Et grunnleggende webapplikasjonsprosjekt utviklet med ASP.NET Core MVC for å demonstrere bruk av Model-View-Controller-arkitekturen og Entity Framework Core for databasetilkobling. Prosjektet fungerer som en Quiz-applikasjon med fokus på å implementere et Minimum Viable Product for innholdsstyring.

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
Dette prosjektet har aktivt brukt AI til å lage en grunnleggende mappestruktur og navngivning etter å ha beskrevet MVP. AI er også brukt for feilsøking og bearbeiding av kode når vi har møtt på hindringer og at koden ikke kjører. AI var ikke brukt til å skrive README, men den gjorde slik at den så bedre ut i henhold til hvordan README ser ut i preview

