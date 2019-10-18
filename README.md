### Forutsætning for å kjøre programmet
Disse må være installert for å kunne kjøre eller utvikle dette program:

1. https://github.com/dotnet/core-setup
2. https://github.com/dotnet/core-sdk

## Backend case for webutvikler 2019

Jeg har valgt å løse denne case i .NET Core, da .NET Core allerede har indbygget data validering og da data validering var opgavens hovedpunkt var det et naturlig valg.

I mit program har jeg 3 primære classes: InformationController.cs, Information.cs og AreaCodeAttribute.cs.

### Information.cs 
Er en simpel class som definerer de felter som brukeren må utfylle i skjemaet. Til nogle af disse punkter har jeg lagt til det som hedder "Validation Attribute" som gør at man ikke kan binde data til objektet, før at disse attributterne er opfyldt. En simpel attribut kan være [required] som siger at feltet må utfylles før objektet kan godkjennes.

De attributter som jeg har brugt er:
[required] - Felt må utfylles
[MinLength(n)] - Felt må være mer end n bogstaver
[MaxLength(n)] - Felt må ikke være mer end n bogstaver
[RegularExpression] - Felt må opfylle den regex som er gitt
[AreaCode] - Min egen attribut som forklares nedenfor

### AreaCodeAttribute.cs
Min egen attribut class som arver ValidationAttribute, som gjør at jeg kan definere min egen attribut. Jeg bruker her Brings API til at sjekke om det postnummer som er gitt er et rigtigt postnummer, hvis ikke postnummer er korret, vil attributten ikke godkjenne objektet.

### InformationController.cs
En controller class som har en route som tager imod et JSON objekt. Når et objekt kommer igennem denne route, bliver det automatisk bundet til en instans af Information og det er nå muligt å sjekke om det objekt som er gitt opfyller de attributterne som Information krever. Hvis ikke felterne er opgitt korrekt vil den svare med en 400 status code, ellers vil den fortsette.

Etter det har jeg valgt at sjekke om feltet "Honeypot" er fyldt ut. Skulle man lage en frontend til denne opgaven, kunne man ha et skjult felt i skjemaet som bare bots kan se, bots vil naturligvis utfylle feltet mens mennesker ikke ville. Da er det bare å sjekke om feltet er utfyldt, hvis det er, da er det kanskje bots som har utfylt og derfor spam.

Etter det sjekker jeg om to epoch tider er laget innafor to sekunder av hverandre. Igen er formålet at man skulle lage en frontend som har et skjult felt som notere tidpunktet der skjemaet blev loaded. Når brukeren sender inn skjemaet bliver det igjen notert et tidspunkt, da sjekkes det om der er mere eller mindre enn 2 sekunder forskjel. Da det bare er bots som kan utfylle et skjema på mindre enn 2 sekunder, må det være spam hvis der ikke er mere enn 2 sekunder forskjel. Jeg har valgt at comment out min løsning, da jeg ikke har laget en frontend ennå, og da jeg bruker Postman til å teste min route kan jeg ikke få det nuværende tidspunkt og det er derfor ikke muligt at lage en sammenligning. 

Om der ikke er gitt en status code 400, da er skjemaet sannsynligvis uttfylt korrekt og er ikke spam, og kan nå sendes videre i systemet.

### Næste skridt
Skulle man utvikle mere på dette program ville det være lurt å lage både en database forbindelse og lage unit tests. 