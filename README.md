# Outdated: A very old (and poorly made) tool thats not helpful to the average user.
# Neo4J_CSharp-Unity-Support-
* A tool that i made that extends the Neo4j Driver example, allows you to easily get data from neo4j. This is and internal server so it can be used with Unity.
# Important
* The example does not work with the Tool at the moment. i dont have time to fix it right now but the changes to fix it are simple.
* Because i really dont have time im just going to paste what the console would tell you if you started it normally:
* No arguments detected...
* Start this application (via System.Diagnostics or other means) with the following argument:
*  <port number>,<whether you want to console to show the login details on the screen (true or false)>
* Once you do this, you can then connect to the internal server (tcp socket bound to loopback and the port you gave it)
* You then need to setup the manager. to do this you send the following encoded as UTF8:
*  <Uri (string)>\<Username (string)>\<Password (string)>\<ShowConsole (bool)>\<PacketSize (int)>\
* You will then recive a responce that is 2 bytes large
* You can then send querys using the following format:
*  <Query (string)>\<Key (string)>\<Single (bool)>\<Id (int)>\
* Theres alot more information you can get from this moslty outdated but still important video:
* https://youtu.be/JHRZApGovok Or if your linkaphobic: How To Use Neo4J_CSharp To Pull Data From Neo4j.
* IMPORTANT! when you send a query is will give you the results asynchronously.
* The data you recive is now <The Data>\<The ID Of The Query>
* You cannot use the example directly anymore as it is most likly outdated"
