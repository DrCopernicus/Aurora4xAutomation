# Aurora4xAutomation
Client/Server for running Aurora.

Originally built in response to the "Shipping company xyz just made freighter 789! I'll stop the entire game, just for you!" spam. It's evolved quite a bit and has served primarily as a test bed for using and abusing interfaces.

Can access the database, but you'll need the password. No, I will not give you the password.

This software actively clicks on the screen, moves around windows, and does all sorts of other crazy stuff. You will need to run it in adminstrator mode. You may or may not need to play using the Aurora wrapper. You will need to have your selected game open (it won't figure that out for you, yet). I'm sure theres a million other environment specific issues you can run into. I like to run Aurora in a virtual machine with the server, and then run the client on my host machine.

## Roadmap

* Clean up current UI component tests, preferably making a scripting system so I can script which screen image shows up next.
* Integration test cases for the rest of the UI components.
* Use what I learned from the above to implement NSubstitute schenanigans on all my other test cases.
* Hopefully use the aforementioned scripting system to test the evaluators.
* Write tons of miscellaneous tests for the main (server) project.
* Write test cases for client.
* Look over everything and make sure I don't find any more isolated classes hanging around.
* Continue moving code out of the ___Commands classes into evaluators.
* Tidy things up a lot because it's probably getting gnarly at this point.
* Write more tests.
* Did I mention more tests???

* And only after all of that is done I can finally begin writing interesting code. I'd like to focus on interaction between the server and client, taking advantage of the RESTful features that Grapevine gives me.
