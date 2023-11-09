# Changelogs :7/16/23
## Added
- Role-based authorization for StampController
- Role-based authorization for BidController
- Role-based authorization for ListingController
- Role-based authorization for UserController
- Added dependencies for [Npgsql](https://github.com/npgsql/efcore.pg) nuget package - entity framework core provider for postgresql
- Services for ListingController
- Services for BidController
- Repository for ListingService
- Repository for BidService
## Fixed
- Program.cs , fixed JWTBearer configuration. (Code breaking issue with signing keys)
## Changed
- Switched internal db from Microsoft Sql server to postgres
- Modified BidController(made it thinner).
- Modified ListingController(made it thinner).
- Updated the application configurations(appsettings.json), database context and the migrations.
- Dependency injection for the newly added services

# Changelogs : 6/22/23
**Chores**
- Created services for stamp controller.
- Created repositories for stamp services.
- Stamp controller is now much more lightweight.

# Changelogs : 6/21/23
 **Implemented design enhancements**
- Created a new file directory called *Services*. Created UserServices class and extracted its interface.
- Created Repository for the UserServices. Extracted its interface
- Took care of Dependency injection.
- **Thinning the fat controller** Ensured that no implementation is being done inside the controller itself. All the implementation goes to service class
- **Restricting DBContext access** Made sure no other layer except the data access layer have access to the dbContext .
-  Now the controllers have access to The data request models, and they talk to the services through these models. Services have access to the main models, and talk to the repository using these models.