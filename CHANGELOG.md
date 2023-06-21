 # Changelogs : 6/21/23
 **Implemented design enhancements**
- Created a new file directory called *Services*. Created UserServices class and extracted its interface.
- Created Repository for the UserServices. Extracted its interface
- Took care of Dependency injection.
- **Thinning the fat controller** Ensured that no implementation is being done inside the controller itself. All the implementation goes to service class
- **Restricting DBContext access** Made sure no other layer except the data access layer have access to the dbContext . Had to go through a few workarounds and crazy hacks, about which i'm kinda insecure.
-  Now the controllers have access to The data request models, and they talk to the services through these models. Services have access to the main models, and talk to the repository using these models.
-   Only the repository can access the dbContext