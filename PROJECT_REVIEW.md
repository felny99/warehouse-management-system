# Warehouse project review

Review date: 2026-09-12  
Recommended canonical version: `Warehouse (8)`  
Reviewed solution root: `C:\Users\felix\Downloads\Warehouse (8)\Warehouse`

This is a recovery and development roadmap for an academic portfolio project. It is not a production-readiness assessment. No source changes are proposed as prerequisites beyond the small, understandable fixes listed below.

## Project overview

Warehouse is a server-side Blazor application for maintaining a small warehouse/order catalogue. It displays and edits customers, products, orders, and order lines in Razor components backed by an Entity Framework Core SQLite database. It also attempts to dispatch paid orders in date order, reduce product stock, and assign/display a future restocking date when stock is insufficient.

Main technologies:

- C# and ASP.NET Core Blazor Server
- Razor components and Razor Pages hosting
- Entity Framework Core with SQLite
- Bootstrap and Open Iconic assets
- `.NET Core 3.1` as the target framework

The solution currently has no Git repository, project README, `.gitignore`, migrations, automated tests, or documented setup procedure.

## Version comparison and base-version recommendation

Use **`Warehouse (8)` as the canonical recovery base**. It is the newer version (source changes from 2021-11-18 rather than 2021-10-28), and the changes form a coherent continuation of `Warehouse 3.1`. Both versions compiled successfully in a temporary copy using the locally preserved .NET 5.0.203 SDK and .NET Core 3.1 runtime, with zero build warnings and zero build errors. That confirms that the recommendation is based on completeness and intent rather than only buildability.

| Area | `Warehouse 3.1` | `Warehouse (8)` | Assessment |
| --- | --- | --- | --- |
| Product data access | `ProductService` | Renamed to `ProductRepo`; dependency injection and components updated | `(8)` is more consistent with the other repository class names |
| Product features | CRUD | CRUD plus out-of-stock filtering and restocking-date display work | `(8)` is more complete, although date display contains a bug described below |
| Order dispatch | Earlier counter-based experiment using `OrderLineCounter` | Rewritten stock calculation, restricted to paid and pending orders, plus restocking-date handling | `(8)` expresses the intended domain rule more clearly, but boundary cases and async persistence still need correction |
| Order UI | Basic status and CRUD | Adds a restocking-date column and removes editing of the order primary key | `(8)` is the more mature UI |
| Validation | Little model/form validation | Adds data annotations and validation summaries to customers/products | Useful progress, but one annotation probably prevents customer creation |
| Repository interfaces | Mostly commented placeholders | Product/order contracts partly activated and documented | `(8)` is further along, though the interface layer remains inconsistent |
| Local database | `WH-Base.db`, older binary contents | `WH-Base.db`, later binary contents | Do not merge databases; manually review and choose sanitized demo data |

`Warehouse (8)` therefore appears to supersede `Warehouse 3.1`. The older version is still useful as historical reference, but there is no large completed feature in it that should be copied wholesale.

Items worth reviewing manually in the older version:

- `Data/OrderLineRepo.cs` contains a small `GetSpecificLines` filtering helper removed from `(8)`. It may inspire a later query method, but it is unused and is not a reason to merge the old file.
- `Data/OrderLineCounter.cs` and the earlier `DispatchOrders` implementation document an abandoned counter-based approach. Preserve the old version in an archive/tag if its development history matters; do not combine both algorithms.
- `Data/ProductService.cs` contains older order-status filtering methods, but those methods are misplaced in a product service and appear unused. They add no clear missing product functionality.
- The older SQLite database differs from the newer one. Treat it as historical data, not source code to merge.

Likely template or supplied material:

- `Pages/Counter.razor`, `Pages/FetchData.razor`, `Data/WeatherForecast.cs`, `Data/WeatherForecastService.cs`, and `Shared/SurveyPrompt.razor` closely match the standard Blazor Server starter template.
- Much of `App.razor`, `_Imports.razor`, the host/error pages, layout, navigation shell, launch settings, and default CSS also originated from project scaffolding, although the navigation was adapted for Warehouse.
- Bootstrap and Open Iconic are third-party assets. Their existing license files should remain with any retained assets.
- Some comments in the warehouse-specific files appear to refer to adapting an example or another person's service pattern. That is not enough evidence to decide authorship. Before presenting the work as solely individual, check the course brief, group membership, supplied starter code, and any attribution requirements.
- Generated `obj` metadata contains historical local account paths. That is not reliable evidence of source authorship and should not be committed.

## Current architecture

### Solution and startup

- `Warehouse.sln` contains one web project.
- `Warehouse/Warehouse.csproj` targets `netcoreapp3.1` and references Entity Framework Core, SQLite, SQL Server, EF tooling, and ASP.NET scaffolding packages.
- `Warehouse/Program.cs` creates the ASP.NET Core host.
- `Warehouse/Startup.cs` registers Razor Pages, Blazor Server, the EF `Context`, and concrete repository/service classes. It maps the Blazor hub and falls back to `Pages/_Host.cshtml`.

The normal execution path is:

1. `Program.Main` builds and runs the host.
2. `Startup.ConfigureServices` registers Blazor, Entity Framework, and data-access classes.
3. `Context` opens the relative SQLite file `WH-Base.db`, calls `EnsureCreated`, and defines seed data.
4. `App.razor` resolves a route into a component under `Pages`.
5. The component calls an injected repository/service, which queries or mutates tracked EF entities and calls `SaveChangesAsync`.

### Domain and persistence

`Warehouse/Data` currently mixes domain models, EF configuration, repositories, services, interfaces, and starter-template classes:

- `Customer`: name, phone, email, primary key, and a collection of orders.
- `Product`: name, price, stock, description, restocking date, and primary key.
- `Order`: customer relationship, date, delivery address, payment status, dispatch status, and order lines.
- `OrderLine`: joins an order and product and stores quantity.
- `Context`: exposes four `DbSet` properties, relies mainly on EF conventions for relationships, and seeds example records.
- `CustomerService`, `ProductRepo`, `OrderRepo`, and `OrderLineRepo`: data access plus some presentation/domain filtering and dispatch rules.
- `IRepository` and the entity-specific interfaces: intended abstractions, but several are empty or only partially describe their concrete implementations.

Persistence uses a local SQLite file through a hard-coded relative connection string in `Startup.cs`. `Database.EnsureCreated()` creates a database when none exists. There is no migration history, schema versioning, or external configuration of the database location.

### User interface

- `Pages/Index.razor`: product list and a second product-creation form.
- `Pages/ProductPage.razor`: product CRUD, restocking-date display, and out-of-stock filter.
- `Pages/CustomerPage.razor`: customer CRUD and customer-specific order filters.
- `Pages/OrderPage.razor`: order CRUD/status filters, navigation to order-line editing, dispatch action, and restocking status.
- `Pages/OrderLinePage.razor`: shows and adds product quantities for a route-selected order.
- `Shared/NavMenu.razor`: navigation to the main warehouse pages.

## Current status

### Appears functional or substantially implemented

- The solution structure is intact and both candidate versions compile in the preserved legacy toolchain.
- EF models and relationships cover the core customer-product-order-order-line domain.
- SQLite persistence, first-run database creation, and example seeding are present.
- Product, customer, order, and order-line list/add workflows exist.
- Product, customer, and order update/delete operations are represented in the UI and data layer.
- Orders can be filtered by dispatch status; customers can show their pending/dispatched orders; products can be filtered for zero stock.
- The later version includes a recognizable dispatch workflow and restocking-date concept.

### Partial, fragile, or unverified

- A clean compile was verified; full browser workflows and database mutations were not exhaustively exercised.
- The application process could start in the isolated review environment, but a page-level smoke test was inconclusive because that environment denied ASP.NET access to Windows data-protection/event-log locations. This is not treated as an application defect.
- The dispatch operation exists but has correctness and persistence issues.
- Form validation is inconsistent and may block customer submission.
- UI collections and refresh paths are fragile around asynchronous loading.
- Database creation works only as an initial schema strategy; it does not support controlled schema evolution.
- Navigation properties are obtained through EF tracking/fix-up rather than explicit loading, making component assumptions difficult to understand.

## Must fix

1. **Create a safe Git baseline.** Add a suitable `.gitignore` before the first public commit. Do not commit `.vs`, `bin`, `obj`, `Warehouse.csproj.user`, or an unreviewed local database. The current folder is not a Git repository.

2. **Manually review all example/local data before publication.** `Warehouse/Data/Context.cs` contains contact- and address-like seed fields. `Warehouse/WH-Base.db` may contain persisted customer/contact/order data from previous runs. Even if the records are fictional, confirm that manually and replace them with unmistakably synthetic demo data when you are ready to edit source.

3. **Document or update the legacy runtime requirement.** The project targets the unsupported `netcoreapp3.1` framework. It builds on this machine because a legacy SDK/runtime is installed, but a fresh developer machine or current CI runner will commonly lack it. First preserve a buildable historical baseline; then either document the exact legacy prerequisite or make a separate, deliberate framework-upgrade commit.

4. **Resolve package-version inconsistency.** Core/SQLite packages are version 5.0.11 while SQL Server/tools packages are 3.1.x, and SQL Server does not appear to be used. Decide which provider is actually required and keep one compatible EF Core line. Do this separately from feature work so regressions are understandable.

5. **Fix customer-form validation.** `Customer.Orders` is marked `[Required]`, but a new customer's collection is not initialized because the constructor is commented out. `CustomerPage` uses `OnValidSubmit`, so creating a customer can be rejected for an invisible navigation-property validation error. Navigation collections should not be required UI inputs.

6. **Correct and make dispatch persistence awaitable.** `OrderRepo.DispatchOrders` is synchronous but calls `SaveChangesAsync` without awaiting it; the component also does not await the dispatch operation or reload its lists. Failures can be lost and the UI can remain stale.

7. **Specify and correct dispatch boundary cases before presenting the feature.** The current check treats stock becoming exactly zero as insufficient (`<= 0`), can mark an order with no lines as dispatched, and does not clearly reject an order line whose product is missing. Write down the intended rules, then correct the smallest cases one at a time.

8. **Remove or guard null-before-load failures.** Components call `.Any()` or iterate list fields that are not initialized. Asynchronous component initialization can render before those fields are populated. Initialize collections or render explicit loading states.

9. **Add a root README with reproducible instructions.** Explain the academic context, features, legacy framework requirement, database behavior, how to build/run, what is template-derived, known limitations, and the improvement roadmap. Without this, a recruiter cannot reliably evaluate or run the project.

## Should fix

1. **Make database configuration predictable.** Move the SQLite connection setting out of code and make the database path relative to a known application/content directory. The current `Data Source = WH-Base.db` depends on the process working directory and may create/use different files depending on how the app is launched.

2. **Choose a reproducible database lifecycle.** `EnsureCreated` plus dynamic seed values is convenient for a prototype but gives no migration history. For a portfolio project, use either a clearly documented disposable demo database or a small initial migration—not both an opaque committed database and implicit creation.

3. **Fix async event handling and refresh behavior.** Several component handlers are `async void`; several service calls to `SaveChangesAsync` are not awaited; `CustomerPage.UpdateGUI` discards the returned list. Use task-returning handlers and explicitly replace displayed collections after mutations.

4. **Clarify data-access abstractions.** `IRepository` is empty; `ICustomerRepository` and `IOrderlineRepository` contain only commented method ideas; the other interfaces omit some concrete methods; and components inject concrete classes. Either complete and use the small interfaces or remove the unused abstraction in a focused cleanup. No larger architecture is needed.

5. **Use consistent naming and placement.** `CustomerService` sits beside three `*Repo` classes, and all models/repositories/template services share `Data`. Pick one straightforward naming convention and separate domain models from data access only if it improves navigation. Correct visible and API spelling such as `DeliveryAdress` carefully because renaming an EF property affects the database schema.

6. **Remove obsolete duplicate code after preserving history.** `ProductPage.razor` contains a complete older implementation inside an HTML comment, plus smaller commented experiments. `Index.razor` duplicates part of the product-list/add workflow. Preserve the original in Git history, then remove dead blocks and decide whether the home page should be a dashboard or link to the product page.

7. **Review navigation loading.** `OrderLinePage` reads `OrderLine.Product.Name`, but repository queries do not explicitly include related entities. EF tracking may fix this up because products are loaded into the same context, but the behavior is implicit and fragile. Make required relationships explicit in the query or presentation model.

8. **Improve input validation.** Add only domain-relevant validation: positive quantities, required customer/order selections, sensible stock and price ranges, email formatting, and clear restocking-date input. Avoid validating EF navigation properties as form fields.

9. **Review money and date modeling.** `double` is a poor fit for currency; restocking and order dates depend on local `DateTime.Now`; and the UI binds dates through text/number inputs. Choose simple types and formats that match the academic domain.

10. **Fix restocking-date rendering.** `ProductPage` formats a date with slashes but compares it to a string containing hyphens in the active branch, so default dates are not classified as intended. Compare date values rather than formatted display strings.

11. **Simplify exception handling and remove stale comments/usings.** Several `catch` blocks only rethrow, some caught variables are unused, and comments still say code has errors even though it builds. Retain comments that explain domain decisions, not abandoned debugging conversations.

12. **Review the lifetime of `DbContext` in Blazor Server.** The current scoped context can live for an entire user circuit and accumulate tracked entities. This may be acceptable for the original exercise, but understand the behavior before expanding concurrent workflows. A small context-per-operation approach can be considered later; it does not require enterprise layering.

## Nice to have

- Add a few focused tests for dispatch rules: exact depletion, insufficient stock, multiple lines for the same product, unpaid orders, missing products, and empty orders.
- Add small repository/component tests only for bugs you actually fix; a broad test framework is unnecessary.
- Replace the starter home page with a concise dashboard or project explanation.
- Add screenshots or a short GIF to the README after the main workflows are stable.
- Add a simple domain diagram showing Customer → Order → OrderLine → Product.
- Add accessible labels, clearer validation messages, loading/empty states, and confirmation before destructive UI actions.
- Add a clearly labeled demo-data reset command or documented procedure.
- Consider a later framework upgrade in its own branch/commit after preserving the original version and behavior.

## GitHub cleanup

Probably exclude from Git:

- `.vs/`
- `**/bin/`
- `**/obj/`
- `*.csproj.user`, `*.suo`, and other per-user IDE files
- `Warehouse/WH-Base.db` until it has been manually reviewed and you have decided whether a sanitized demo database is necessary
- local logs, temporary database journal/WAL files, and future user-secret files

Keep as source unless deliberately replacing them:

- `Warehouse.sln`, `Warehouse/Warehouse.csproj`, C# files, Razor files, configuration templates, and required web assets
- Open Iconic license/readme files while its assets are included
- Bootstrap/Open Iconic assets needed by the current legacy UI; note their third-party origin rather than presenting them as original work

The generated directories currently occupy much more space than the handwritten project and include compiled assemblies, caches, generated Razor C#, package metadata, and absolute build paths. They are rebuildable and should not be part of the recovered public history.

## Security/privacy review

The precautionary scan did **not** find an `.env` file, credential file, private key material, obvious API-key/token/password assignment, or secret-bearing application configuration. No absolute Windows path was found in handwritten source.

Manual review is still required for:

- `Warehouse/Data/Context.cs` — contains person/contact/address-like seed fields; verify that every record is synthetic and suitable for publication.
- `Warehouse/WH-Base.db` — local SQLite database; may contain persisted contact and order data not visible in source.
- `Warehouse/obj/` — generated metadata includes historical absolute local paths/account identifiers; exclude the entire directory.
- `Warehouse.csproj.user` and `.vs/` — per-user IDE state; exclude even if no secret is apparent.
- `Warehouse/Startup.cs` — contains a hard-coded SQLite connection setting. It is not a credential, but it belongs in configuration for clarity and portability.
- `appsettings*.json` and `Properties/launchSettings.json` — currently appear to contain ordinary logging/host/profile settings only; re-scan whenever connection strings or deployment settings are added.

The application has no authentication or authorization. That is acceptable for a local academic demo, but it should not be deployed as a public writable service with real or persistent data. If a live portfolio demo is later desired, use synthetic disposable data and restrict mutations or add minimal access control.

## Suggested development roadmap

1. Copy `Warehouse (8)` into the intended `C:\Users\felix\Projects` portfolio location and preserve `Warehouse 3.1` unchanged as an offline archive. Do this manually after reviewing this document.
2. Initialize Git in the copied canonical root and add `.gitignore` before staging anything. Confirm that only source, licenses, and documentation are tracked.
3. Manually audit `WH-Base.db` and the seed data. Exclude the historical database; decide on clearly synthetic demo seed data and a predictable first-run database workflow.
4. Add a README that records the original academic scope, template-derived parts, build prerequisites, current capabilities, and known limitations.
5. Reproduce the clean build from a fresh clone. Decide whether the first milestone preserves the legacy runtime or upgrades the target; keep that decision in one focused commit.
6. Align the EF Core packages/provider and verify database creation from an empty state.
7. Fix customer creation validation and initialize UI collections/loading states.
8. Convert mutation handlers to awaitable tasks, await every database save, and reliably refresh the displayed state.
9. Define dispatch rules in plain language and add focused tests for them before correcting boundary cases.
10. Fix the dispatch and restocking-date behavior, then manually exercise product → customer → order → order-line → payment → dispatch as one end-to-end scenario.
11. Remove template demo pages and commented duplicate implementations that do not support Warehouse, relying on Git history for preservation.
12. Normalize repository/service naming and interfaces only as far as needed to make the small codebase understandable.
13. Polish validation, text, screenshots, and portfolio documentation.

## Suggested Git commit sequence

Each commit should build where applicable and describe one understandable change:

1. `chore: recover original Warehouse academic project`
2. `chore: ignore build artifacts, IDE state, and local database files`
3. `docs: document project scope, attribution, setup, and known limitations`
4. `chore: align legacy Entity Framework dependencies`
5. `chore: make SQLite configuration and demo database setup reproducible`
6. `fix: allow valid customer creation and initialize component state`
7. `fix: await database mutations and refresh Blazor views`
8. `test: capture warehouse dispatch rules`
9. `fix: correct stock dispatch and restocking behavior`
10. `refactor: remove obsolete template and duplicate component code`
11. `refactor: clarify repository naming and contracts`
12. `docs: add screenshots and portfolio walkthrough`

If you choose to upgrade from .NET Core 3.1, insert a single dedicated `chore: upgrade target framework and dependencies` commit after the historical baseline and before feature fixes. Do not mix that upgrade with domain-logic changes.

## Review limitations

- This review compared the two requested candidates and briefly checked nearby Warehouse copies for chronology/context. Those nearby copies did not displace `Warehouse (8)` as the latest coherent candidate.
- Compilation was verified in disposable copies so the originals were not changed. The temporary copies were removed afterward.
- No source file, database, configuration file, or directory was modified, moved, renamed, or deleted during the review. This `PROJECT_REVIEW.md` file is the only intended project change.
