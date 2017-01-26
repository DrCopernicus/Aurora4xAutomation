# Aurora4xAutomation
Client/Server for (semi-)automatically playing Aurora.

Originally built in response to the "Shipping company xyz just made freighter 789! I'll stop the entire game, just for you!" event spam. It's evolved quite a bit and has served primarily as a test bed for using and abusing interfaces and other code constructs.

Can access the database, but you'll need Aurora's developer password. No, I will not give you the password.

This software actively clicks on the screen, moves around windows, and does all sorts of other crazy stuff. You will need to run it in adminstrator mode. You will need to have your selected game open (it won't figure that out for you, yet). I'm sure there's a million other environment specific issues you can run into. I like to run Aurora in a virtual machine with the server, and then run the client on my host machine.

## Prerequisites
* Determine which machine will host client and which will host the server. The server and client should be on two separate machines (such as a virtual machine and the host) to avoid interference. More than one client can theoretically connect at a time, but this is not tested currently - caveat emptor.
* Install Aurora (not the wrapper version, but it should work) on the server.
* Open a game in Aurora before running the server executable.

## Setup
* Download the Server and Client zip archives from the Releases tab.
* Extract the server archive on the server machine, and the client archive on the client machine. 
* Run Server.exe in the extracted Server folder under administrator mode.
* If the server machine and client machine are two separate machines, then ensure the server is correctly portforwarded.
* Test the connection to the server in a web browser by typing the server's address and port number into the URL bar. The server is on port `1234` by default.
 * For example, if your server's address is `192.168.1.2`, then type `http://192.168.1.2:1234/`
 * You should receive the following response: `Server is online!`.
* Run Client.exe on the client machine.
* Follow the prompts given by the client.
* Type `list` and press enter to receive a list of all commands supported by the server.
