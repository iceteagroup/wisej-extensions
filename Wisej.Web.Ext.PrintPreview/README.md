PrintPreview
====

Adds support for previewing and printing [PrintDocument](https://docs.microsoft.com/en-us/dotnet/api/system.drawing.printing.printdocument) objects.

Supports two preview modes: PDF and WMF. When using the PDF mode, it generates a temp PDF file using the "Microsoft Print to PDF" driver and displays it using a built-in PDFJS viewer. When using the WMF mode, it uses the System.Drawing.Printing native images (Windows Meta File - WMF) in a custom viewer.

You can use the PrintPreviewDialog, or the PrintPreviewControl independently from the dialog in any container.


License
-------
<img src="http://iceteagroup.com/wp-content/uploads/2017/01/Square-64x64-trasp.png" height="20" align="top"> Copyright (C) ICE TEA GROUP LLC, All rights reserved.
