Achamenes ID3
=============

.NET library written in C# to read and write MP3 file ID3 tags, with support for ID3 versions 1, 2.2, 2.3 and 2.4.

I started this project while I was still in highschool (around 2004) and 
ended up abandoning it before I got to publish it anywhere, when I moved
to Canada to start university in 2005. This GitHub repository is the 
result of my finally getting around to cleaning up the projcect enough
to post it online. 

Backups
=======
The library is set to make backups of any file before it modifies them. If 
you want to disable this, undefine ``ACHAMENES_ID3_BACKUP_FILES_BEFORE_MODIFICATION``
in ``TagBase.cs``.

Compilation
===========
Compiles with Mono/.NET 2.0.


License
=======
The MIT License (MIT)

Copyright (c) <year> <copyright holders>

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
