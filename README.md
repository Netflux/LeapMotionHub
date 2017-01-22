# Leap Motion Hub
C# implementation for sending sensor data as serialized bytes (MsgPack) or XML strings.

### Installation
The project can be built using the included Visual Studio solution file. The project has been tested and built using Visual Studio 2010.

### Notes
 * By default, the WebSocket server runs on ```ws://localhost:4567```. The server automatically sends sensor data to all connected clients.
 * Data can be sent as either serialized bytes using MsgPack (faster) or as XML strings (slower).
 * The project is written in C#, built using Visual Studio 2010.
 * The project only supports Windows currently.
 * Networking server is handled using [websocket-sharp](https://github.com/sta/websocket-sharp).

### License
Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.