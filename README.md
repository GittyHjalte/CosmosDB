<p>Hjalte Bording</p>


<h1>Projektets formål</h1>

<p>
  At lære at bruge API og Azure databaser sammen — at gemme beskeder i CosmosDB. Selve hjemmesiden understøtter henvendelser i beskedform og læsning af henvendelser
</p>

<h2>Oprettelse af CosmosDB database:</h2>

<p> 
1. Az login <br>
2. group create --name ibas-rg --location centralsweden # Havde problemer med de andre servers
3. az cosmosdb create <br>
  --name ACCOUNT \<br>
  --resource-group ibas-rg \<br>
  --kind GlobalDocumentDB

4. az cosmosdb NoSQL database create \ # Her kan man så vælge SQL vs NoSQL
  --account-name ACCOUNT \
  --resource-group ibas-rg \
  --name IBasSupportDB

5. az cosmosdb sql container create \
  --account-name ACCOUNT \
  --resource-group ibas-rg \
  --database-name IBasSupportDB \
  --name ibassupport \
  --partition-key-path "/category"
</p>
   <p># Jeg havde nok allerstørst problemer med git bash her (windows)</p>


<h3>Status:</h3>

- Oprettet database & tilhørende container
- Modelklasse til SupportMessage (henvendelsen)
- API (controller + service) til forbindelse med CosmosDB
- Blazor-sider (Opret hendvendelser, se alle henvendelser) til input og læsning af databasen.

<h3>Status:</h3>
Hvad nåede jeg/hvad er næste trin?:
- Jeg fik lavet det minimale vel. Tog mig alt for langt tid, fordi jeg havde problemer med både CosmosDB og Blazor-delen.
- At lave login-system og begrænse adgang til beskeder. Henvendelser burde ikke være tilgængelige for andre — kun admins. Så måske en inbakke i stedet for en hel liste med ALLE beskeder.
- Jeg fik heller ikke fikset validation helt. Klassen har ikke [Required] f.eks..
