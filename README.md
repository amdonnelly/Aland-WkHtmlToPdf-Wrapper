.Net Pdf Generator
==================

This is a simple .NET PDF generator written in C# that allows you to create PDFs of multiple URL's or direct HTML/Text input.

The component is free to use and is basically a wrapper for WkHtmlToPdf which allows you to pass through command line arguments to the tool.

Multiple instances of the component can be used and can be configured through the web.config to avoid having to recompile every time you want to change the PDF's being created.  

Find out more about how to use it [here](http://www.amdonnelly.co.uk/things/pdf-generator.aspx)


###Getting Started
Download the binary (AlanD.wkhtmltopdf.dll) and include them in your project and add a reference.

You'll also need to install WkHtmlToPdf, you can download the latest version from http://wkhtmltopdf.org/downloads.html.


###Basic Example
```c#
PdfGenerator _pdfGenerator = new PdfGenerator();
_pdfGenerator.Config.SetExeLocation("C:/Program Files/wkhtmltopdf/bin/wkhtmltopdf.exe").SetExeWorkingDirectory("~/pdf-output/temp").SetExeOutputDirectory("~/pdf-output");
_pdfGenerator.AddSource("http://www.example.com").AddHTMLSource("some HTML");
 
byte[] _result = _pdfGenerator.GeneratePDF("demo-1-single.pdf");
```

###Configuration
Instances of the component can be configured in code or using sections in the web.config.

You can find out more about configuration [here](http://www.amdonnelly.co.uk/things/pdf-generator/configuration.aspx)

###Methods
The component has several ways to pass command line arguments through to WkHtmlToPdf.

It also allows you to add multiple sources as Url's or direct HTML/Text input. 

List of available methods [here](http://www.amdonnelly.co.uk/things/pdf-generator/methods.aspx)

[Alan Donnelly](http://www.amdonnelly.co.uk)


