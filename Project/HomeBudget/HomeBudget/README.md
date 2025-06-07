# Expenses App – system monitorowania wydatków domowych
ASP.NET Core 8 · MVC · Entity Framework Core · Identity + SendGrid

---

## Spis treści
1. [Opis projektu](#opis-projektu)  
2. [Zaimplementowane funkcjonalności](#zaimplementowane-funkcjonalności)  
3. [Wymagania](#wymagania)  
4. [Uruchamianie aplikacji](#uruchamianie-aplikacji)  
5. [Struktura katalogów](#struktura-katalogów)  
6. [Zewnętrzne biblioteki](#zewnętrzne-biblioteki)  
7. [Autor](#autor)

---

## Opis projektu
**Expenses App** pozwala użytkownikowi:

* rejestrować wydatki według **kategorii, kwoty i daty**,
* przeglądać listę z **sortowaniem** (kategroria, kwota, data) i **filtrowaniem** (zakres dat, kategoria),
* dodawać / edytować / usuwać wpisy (wyłącznie jako zalogowany użytkownik),
* zakładać konto, logować się oraz **resetować hasło e-mailem (SendGrid)**,
* podglądać sumę wydatków danego widoku pod tabelą.

Projekt bazuje na wzorcu **Model – View – Controller (MVC)** oraz gotowym scaffolding-u Identity.

---

## Zaimplementowane funkcjonalności

| # | Funkcja | Pliki / klasy |
|---|---------|---------------|
| 1 | CRUD wydatków | `ExpensesController`, `Views/Expenses/*` |
| 2 | Tabela z sortowaniem ▲▼ i filtrami From/To/Category | `Index.cshtml` + LINQ `switch` |
| 3 | ASP.NET Core Identity – rejestracja, logowanie | `Areas/Identity/*` |
| 4 | Reset hasła przez SendGrid | `ForgotPassword*`, `ResetPassword*`, `SendGridEmailSender.cs` |
| 5 | Walidacja danych (DataAnnotations) | `Models/Expense.cs` |
| 6 | Seeding przykładowej bazy | `Data/SeedData.cs` |

---

## Wymagania

| Narzędzie | Wersja testowana |
|-----------|------------------|
| .NET SDK  | 8.0 LTS |
| SQL Server | LocalDB (15.x) |
| IDE       | Visual Studio 2022 Community |
| SendGrid  | darmowy plan *API & Marketing Essentials* |

---

## Uruchamianie aplikacji
```
bash

git clone https://github.com/K-Lisicka/MVC.git
cd <repo>

#wygenerowanie klucza https://sendgrid.com/en-us 
dodanie klucza SendGrid: Project/HomeBudget/HomeBudget/appsettings.json 
,
  "SendGrid": {
    //"ApiKey": "miejsce na klucz",
    "FromEmail": "expensesappnotification@gmail.com",
    "FromName": "Expenses App"
  }

# migracje baz danych
dotnet ef migrations add InitialCreate --context HomeBudget.Data.HomeBudgetContext
dotnet ef database update --context HomeBudget.Data.HomeBudgetContext

dotnet ef migrations add InitialCreate --context HomeBudget.Areas.Identity.Data.HomeBudgetIdentityContext
dotnet ef database update --context HomeBudget.Areas.Identity.Data.HomeBudgetIdentityContext

# build i run
dotnet run    
# użytkownik może korzystać z funkcji wyświetlania danych, sortowania i filtrowania

# rejestracja użytkownika za pomocą maila
potwierdzenie rejestracji linkiem aktywacyjnym

# logowanie przy użyciu maila i ustawionego hasła
użytkownik może korzystać z funkcji dodawania nowych rekordów, ich edytowania oraz kasowania
```
---

## Struktura katalogów
```
HomeBudget
├─ Areas
│  └─ Identity
│     ├─ Data
│     │  ├─ HomeBudgetIdentityContext.cs
│     │  └─ IdentityUser.cs
│     └─ Pages
│        └─ Account
│           ├─ _ViewImports.cshtml
│           ├─ ForgotPassword.cshtml
│           ├─ ForgotPasswordConfirmation.cshtml
│           ├─ Login.cshtml
│           ├─ Logout.cshtml
│           ├─ Register.cshtml
│           ├─ RegisterConfirmation.cshtml
│           ├─ ResetPassword.cshtml
│           ├─ ResetPasswordConfirmation.cshtml
│           ├─ _ValidationScriptsPartial.cshtml
│           ├─ _ViewImports.cshtml
│           └─ _ViewStart.cshtml
├─ Controllers
│  ├─ ExpensesController.cs
│  └─ HomeController.cs
├─ Data
│  ├─ HomeBudgetContext.cs
│  ├─ SeedData.cs
│  └─ Migrations
│     ├─ HomeBudgetIdentity
│     │  ├─ 20250601170010_InitialCreate.cs
│     │  └─ HomeBudgetContextModelSnapshot.cs
├─ Models
│  ├─ ErrorViewModel.cs
│  ├─ Expense.cs
│  ├─ ExpenseCategoryViewModel.cs
│  └─ SeedData.cs
├─ Services
│  └─ SendGridEmailSender.cs
├─ Views
│  ├─ Expenses
│  │  ├─ Create.cshtml
│  │  ├─ Delete.cshtml
│  │  ├─ Details.cshtml
│  │  ├─ Edit.cshtml
│  │  └─ Index.cshtml
│  ├─ Home
│  │  └─ Index.cshtml
│  └─ Shared
│     ├─ _Layout.cshtml
│     ├─ _LoginPartial.cshtml
│     └─ _ValidationScriptsPartial.cshtml
├─ appsettings.json
├─ appsettings.Development.json
├─ Program.cs
├─ README.md
└─ ScaffoldingReadMe.txt
```
---

## Zewnętrzne biblioteki

| Pakiet NuGet                                | Zastosowanie                  |
|---------------------------------------------|-------------------------------|
| Microsoft.EntityFrameworkCore.SqlServer     | ORM + SQL Server              |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore | Tabele Identity        |
| SendGrid                                    | Wysyłka e-mail reset-linków   |
| Bootstrap 5, Bootstrap Icons                | Interfejs i ikonki            |

---

## Autor

**Katarzyna Lisicka**  
Student kierunku Informatyka  
Uniwersytet Vizja, 2025



---




# Expenses App – home budget monitoring system  
ASP.NET Core 8 · MVC · Entity Framework Core · Identity + SendGrid

---

## Table of contents
1. [Project description](#project-description)  
2. [Implemented features](#implemented-features)  
3. [Requirements](#requirements)  
4. [Running the application](#running-the-application)  
5. [Directory structure](#directory-structure)  
6. [External libraries](#external-libraries)  
7. [Author](#author)

---

## Project description
**Expenses App** allows a user to:

* record expenses by **category, amount and date**  
* browse the list with **sorting** (category, amount, date) and **filtering** (date range, category)  
* add / edit / delete entries (only as a logged-in user)  
* create an account, sign in and **reset a password via e-mail (SendGrid)**  
* view the total of currently displayed expenses under the table  

The project is built with the **Model – View – Controller (MVC)** pattern and the scaffolded Identity UI.

---

## Implemented features

| # | Feature | Files / classes |
|---|---------|-----------------|
| 1 | Expense CRUD | `ExpensesController`, `Views/Expenses/*` |
| 2 | Table with ▲▼ sorting and From/To/Category filters | `Index.cshtml` + LINQ `switch` |
| 3 | ASP.NET Core Identity – registration & login | `Areas/Identity/*` |
| 4 | Password reset via SendGrid | `ForgotPassword*`, `ResetPassword*`, `SendGridEmailSender.cs` |
| 5 | Data validation (DataAnnotations) | `Models/Expense.cs` |
| 6 | Sample database seeding | `Data/SeedData.cs` |

---

## Requirements

| Tool | Tested version |
|------|----------------|
| .NET SDK | 8.0 LTS |
| SQL Server | LocalDB (15.x) |
| IDE | Visual Studio 2022 Community |
| SendGrid | free *API & Marketing Essentials* plan |

---

## Running the application

```bash
git clone https://github.com/K-Lisicka/MVC.git
cd <repo>

#Generate API Key at https://sendgrid.com/en-us 
add SendGrid key: Project/HomeBudget/HomeBudget/appsettings.json 
,
  "SendGrid": {
    //"ApiKey": "put the key here",
    "FromEmail": "expensesappnotification@gmail.com",
    "FromName": "Expenses App"
  }

# database migrations
dotnet ef migrations add InitialCreate --context HomeBudget.Data.HomeBudgetContext
dotnet ef database update --context HomeBudget.Data.HomeBudgetContext

dotnet ef migrations add InitialCreate --context HomeBudget.Areas.Identity.Data.HomeBudgetIdentityContext
dotnet ef database update --context HomeBudget.Areas.Identity.Data.HomeBudgetIdentityContext

# build & run
dotnet run
# the user can browse data, sort and filter

# user registration via e-mail
# account confirmation with an activation link

# login with e-mail and chosen password
# the user can add new records, edit and delete them
```
---

## Directory structure
```
HomeBudget
├─ Areas
│  └─ Identity
│     ├─ Data
│     │  ├─ HomeBudgetIdentityContext.cs
│     │  └─ IdentityUser.cs
│     └─ Pages
│        └─ Account
│           ├─ _ViewImports.cshtml
│           ├─ ForgotPassword.cshtml
│           ├─ ForgotPasswordConfirmation.cshtml
│           ├─ Login.cshtml
│           ├─ Logout.cshtml
│           ├─ Register.cshtml
│           ├─ RegisterConfirmation.cshtml
│           ├─ ResetPassword.cshtml
│           ├─ ResetPasswordConfirmation.cshtml
│           ├─ _ValidationScriptsPartial.cshtml
│           ├─ _ViewImports.cshtml
│           └─ _ViewStart.cshtml
├─ Controllers
│  ├─ ExpensesController.cs
│  └─ HomeController.cs
├─ Data
│  ├─ HomeBudgetContext.cs
│  ├─ SeedData.cs
│  └─ Migrations
│     ├─ HomeBudgetIdentity
│     │  ├─ 20250601170010_InitialCreate.cs
│     │  └─ HomeBudgetContextModelSnapshot.cs
├─ Models
│  ├─ ErrorViewModel.cs
│  ├─ Expense.cs
│  ├─ ExpenseCategoryViewModel.cs
│  └─ SeedData.cs
├─ Services
│  └─ SendGridEmailSender.cs
├─ Views
│  ├─ Expenses
│  │  ├─ Create.cshtml
│  │  ├─ Delete.cshtml
│  │  ├─ Details.cshtml
│  │  ├─ Edit.cshtml
│  │  └─ Index.cshtml
│  ├─ Home
│  │  └─ Index.cshtml
│  └─ Shared
│     ├─ _Layout.cshtml
│     ├─ _LoginPartial.cshtml
│     └─ _ValidationScriptsPartial.cshtml
├─ appsettings.json
├─ appsettings.Development.json
├─ Program.cs
├─ README.md
└─ ScaffoldingReadMe.txt
```
---

## External libraries

| NuGet package                                    | Purpose                              |
|--------------------------------------------------|--------------------------------------|
| Microsoft.EntityFrameworkCore.SqlServer          | ORM + SQL Server                     |
| Microsoft.AspNetCore.Identity.EntityFrameworkCore| Identity tables                      |
| SendGrid                                         | Sending password-reset e-mails       |
| Bootstrap 5, Bootstrap Icons                     | UI framework and icon set            |

---

## Author

**Katarzyna Lisicka**  
Computer Science student  
Vizja University, 2025
