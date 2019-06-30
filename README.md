# PokeClinic

### Requirements
 * DotNET core 2.2
 * MySQL
 
### Setup

 * (Windows) Install MySQL community edition
 * (Linux) Install mariadb
 * Install DotNET core 2.2 SDK
 * Run sql scripts in "Scripts" directory of repo
 * Run `dotnet restore` in the root of the repository
 * Run `dotnet run` - Test url (https://localhost:5001/api/v1/user)

### Postman collection
 * Feel free to add stuff to the collection (PokeClinic.postman_collection.json)

 ### Debugger

 * To setup the debugger, open the launch.json file and replace the "Program" line with this -> `"program": "${workspaceFolder}/bin/Debug/netcoreapp2.2/Pokeclinic.dll`
