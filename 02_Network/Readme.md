# Solutions for Networking in Game
## TCP Transport (Async/SingleThreaded)
**Features**:
* AsyncConnect
* Disconnect
* AsyncSend
* AsyncReceive

**Project Dependencies**:
* Nito.AsyncEx nuget package

**Usage Example** 

Contains Test Server and Winforms App that operates as Client. They started together.  
Test Server uses Akka.Net technology stack.  
Client can be setup to repeatedly (in for loop) send random string with specified length.  
Encoding.UTF8 is used as serialization to simply see what was sent/received.
