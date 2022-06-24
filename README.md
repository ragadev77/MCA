# MCA
.Net 5 WebAPI demo : Maker, Checker, Approval for table "Rule"

___DDL Approach___
Using EFCore migrations automation (cmd or powershell):
1. browse to folder APIMCA : cd APIMCA
2. dotnet ef database update 

___DDL scripts (w/o EFCore migrations)___
DDL_MCA.sql

___Build and Run___
1. Browse to folder APIMCA : cd APIMCA
2. dotnet run --project APIMCA.csproj 

___List API :___
On Swagger Page

___Start Page (Swagger UI) :___
https://localhost:5001/index.html

___DB Connections String___
check file : /APIMCA/appsettings.json

___Integration Test___
Swagger

___Dummy JSON data___ 
{
  "rul_name": "rule-444",
  "rul_desc": "ruledesc-444",
  "rul_condition": "444=444",
  "rul_output": "cabang444",
  "rul_type": 1,
  "rul_created_by" : "crm_admin",
  "rul_category": "categ444",
  "rul_applied": "apply444"
}

___Development Tools___
Visual Studio 2022 |
.Net 5.0.4 |
PostgreSql 9.6.24 |
Postman |
DBeaver Enterprise 22.1.0 
