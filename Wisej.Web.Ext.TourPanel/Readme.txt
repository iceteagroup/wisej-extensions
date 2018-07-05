
How to install the templates
-----------------------------
Replace {User} with the logged in user.
Replace {VisualStudio} with the version of Visual Studio: Visual Studio 2015, Visual Studio 2015=7, ...

1. Copy
	\Templates\Visual Basic\TourPanel 
	to 
	{User}\Documents\{VisualStudio}\Templates\ItemTemplates\Visual Basic\Wisej

2. Copy
	\Templates\Visual C#\TourPanel 
	to 
	{User}\Documents\{VisualStudio}\Templates\ItemTemplates\Visual C#\Wisej

Using the template
------------------

Right click on the project, select Add->New Item...
Select the language and \Wisej.
Select TourPanel.

When using the template you'll probably get an error because the Wisej.Web.Ext.TourPanel assembly cannot be located.
You can add the reference before using the template, or copy the assembly to the same location as Wisej.Core.dll in:

C:\Program Files\IceTeaGroup\Wisej\bin

Using the extension without the template
----------------------------------------

Add the Wisej.Web.Ext.TourPanel.dll assembly to the references.
Create a new UserPanel control and change the base class to Wisej.Web.Ext.TourPanel.
Delete everything inside the InitializeComponent() method.
Open the control in the designer.


