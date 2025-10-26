Et grunnleggende webapplikasjonsprosjekt utviklet med ASP.NET Core MVC for å demonstrere bruk av 
Model-View-Controller-arkitekturen og Entity Framework Core for databasetilkobling.
Prosjektet fungerer som en Quiz-applikasjon med fokus på å implementere et Minimum Viable Product 
for innholdsstyring.

MVP-Funksjonalitet
Denne versjonen av applikasjonen fokuserer på de grunnleggende CRUD-operasjonene 
(Create, Read, Update og delete) for quiz-objekter.

Programmer og teknologi brukt
Rammeverket for prosjektet er bygd på ASP.NET Core MVC med c# som hovedspråk. Entity Framework Core
er brukt for å administrere databasen og frontend er standard webspråk slik som html og css (og noe JS)

Prosjektstruktur
QuizApp.Domain - Inneholder kjerneobjektene og foretningsreglene
QuizApp.Data - Ansvarlig for data tilgang og innheholder DbContext, EF Core migrasjoner og repository implementasjoner
QuizApp.Core - Kjører flyten mellom Data og Domain
QuizApp - Det som presenteres. Altså Views, Controllere og statiske filer

Refleksjon rundt AI-bruk
Dette prosjektet har aktivt brukt AI til å lage en grunnleggende mappestruktur og navngivning etter å ha beskrevet MVP. 
AI er også brukt for feilsøking og bearbeiding av kode når vi har møtt på hindringer og at koden ikke kjører.

