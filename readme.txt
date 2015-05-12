This is a small program which automatically shut down your PC.
It should run on Windows Vista and Windows 7.
Before shutting down, it checks whether the user is idle (at least 30 minutes), and whether a backup (with Macrium Reflect) is running.
The program can be extended with more rules by implementing the IShutdownRule interface and adding it to the ShutdownSafetyProtocol in MainWindow().

Author: Jonny Best
Contact: shutdown@jbxf.de 
Clone from: https://github.com/jonnybest/SmartShutdown

---------- Legal ----------

The MIT License (MIT)

Copyright (c) 2015 jonnybest

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
