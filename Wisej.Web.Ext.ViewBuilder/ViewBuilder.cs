///////////////////////////////////////////////////////////////////////////////
//
// (C) 2023 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
//
// 
//
// ALL INFORMATION CONTAINED HEREIN IS, AND REMAINS
// THE PROPERTY OF ICE TEA GROUP LLC AND ITS SUPPLIERS, IF ANY.
// THE INTELLECTUAL PROPERTY AND TECHNICAL CONCEPTS CONTAINED
// HEREIN ARE PROPRIETARY TO ICE TEA GROUP LLC AND ITS SUPPLIERS
// AND MAY BE COVERED BY U.S. AND FOREIGN PATENTS, PATENT IN PROCESS, AND
// ARE PROTECTED BY TRADE SECRET OR COPYRIGHT LAW.
//
// DISSEMINATION OF THIS INFORMATION OR REPRODUCTION OF THIS MATERIAL
// IS STRICTLY FORBIDDEN UNLESS PRIOR WRITTEN PERMISSION IS OBTAINED
// FROM ICE TEA GROUP LLC.
//
///////////////////////////////////////////////////////////////////////////////


using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Wisej.Core;

namespace Wisej.Web.Ext.ViewBuilder
{
	/// <summary>
	/// Creates a view (a <see cref="ContainerControl"/> instance) or loads an existing one from a JSON representation
	/// or from an object model.
	/// </summary>
	/// <remarks>
	/// <para>
	/// Each object in the definition describes a component: the "_type" member contains the type name (a name without a namespace,
	/// i.e. "TextBox", is resolved in the Wisej.Web namespace of Wisej.Framework; otherwise use the full name, i.e. "MyApp.Controls.MyPanel"),
	/// and all the other members are assigned to the properties with the same name. Property names are matched ignoring case,
	/// therefore both camel casing ("labelText") and proper casing ("LabelText") work. The "_type" member is not needed on the root object
	/// when loading into an existing container with <see cref="LoadView(ContainerControl, string)"/>.
	/// </para>
	/// <para>
	/// Values are converted to the property type using the property's <see cref="TypeConverter"/>, i.e. "10,10" for a <see cref="System.Drawing.Point"/>
	/// or "Top" for an enum. Arrays are added to collection properties such as "controls". A string assigned to a property that
	/// refers to another object (i.e. "dataSource" or "acceptButton") is resolved after the whole view is loaded to the control
	/// or component with that name (see also <see cref="ResolveReference"/>). A string in the format
	/// <c>{Binding Member, Source=name, Format=format, OnFormat=handler, OnParse=handler, SourceUpdateMode=mode, ControlUpdateMode=mode}</c>
	/// creates a data binding; all the parts except the member are optional and the default data source is the root container.
	/// </para>
	/// <para>
	/// Members with the name of an event are attached to the event handler with the specified name. The handler is resolved using
	/// <see cref="ResolveEventHandler"/> first, then it's looked up among the methods declared by the class of the root container, and
	/// last it's resolved as a fully qualified static method name (i.e. "MyApp.Handlers.OnClick"). Event handlers are not compiled from code:
	/// to support code snippets, assign a custom <see cref="ResolveEventHandler"/>.
	/// </para>
	/// <para>
	/// The optional "components" array on the root object defines non-visual components (i.e. a ToolTip or an ErrorProvider) that are created
	/// first and are disposed together with the root container. Extender properties provided by these components are assigned using
	/// the component name and the property name separated by an underscore or a dot, i.e. "toolTip1_ToolTip". Declare the "components" array
	/// as the last member of the root object: the root members that follow it are not processed.
	/// </para>
	/// </remarks>
	/// <example>
	/// Loading a view defined in JSON into an existing form, with an event handler and a tool tip:
	/// <code><![CDATA[
	/// public partial class Form1 : Form
	/// {
	///     private void Form1_Load(object sender, EventArgs e)
	///     {
	///         this.LoadView(@"{
	///             ""text"": ""Customer"",
	///             ""size"": ""400,300"",
	///             ""controls"": [
	///                 {
	///                     ""_type"": ""TextBox"",
	///                     ""name"": ""textBox1"",
	///                     ""dock"": ""Top"",
	///                     ""labelText"": ""Name:"",
	///                     ""validating"": ""textBox1_Validating"",
	///                     ""toolTip1_ToolTip"": ""Enter the name""
	///                 },
	///                 {
	///                     ""_type"": ""Panel"",
	///                     ""dock"": ""Top"",
	///                     ""autoSize"": true,
	///                     ""controls"": [
	///                         {
	///                             ""_type"": ""TextBox"",
	///                             ""name"": ""textBox2"",
	///                             ""location"": ""10,10"",
	///                             ""labelText"": ""Last Name:""
	///                         }
	///                     ]
	///                 }
	///             ],
	///             ""components"": [
	///                 { ""_type"": ""ToolTip"", ""name"": ""toolTip1"" }
	///             ]
	///         }");
	///     }
	///
	///     private void textBox1_Validating(object sender, CancelEventArgs e)
	///     {
	///         e.Cancel = String.IsNullOrEmpty(((TextBox)sender).Text);
	///     }
	/// }
	/// ]]></code>
	/// Binding controls to a property of the root container (the default data source):
	/// <code><![CDATA[
	/// public partial class CustomerForm : Form
	/// {
	///     public Customer Customer { get; set; }
	///
	///     public void LoadCustomer(Customer customer)
	///     {
	///         this.Customer = customer;
	///         this.LoadView(@"{
	///             ""controls"": [
	///                 { ""_type"": ""TextBox"", ""dock"": ""Top"", ""text"": ""{Binding Customer.Name}"" },
	///                 { ""_type"": ""TextBox"", ""dock"": ""Top"", ""readOnly"": true, ""text"": ""{Binding Customer.Balance, Format=c}"" }
	///             ]
	///         }");
	///     }
	/// }
	/// ]]></code>
	/// </example>
	public static partial class ViewBuilder
	{
		#region Methods

		/// <summary>
		/// Creates a new <see cref="ContainerControl"/> from the specified <paramref name="json"/>
		/// representation.
		/// </summary>
		/// <param name="json">JSON definition of the <see cref="ContainerControl"/> to create.</param>
		/// <returns>The new <see cref="ContainerControl"/> instance of the type specified in the "_type" member of the root object.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="json"/> is null.</exception>
		/// <exception cref="Exception">The "_type" member is missing or the type cannot be resolved.</exception>
		/// <exception cref="ArgumentException">A property cannot be assigned.</exception>
		/// <remarks>
		/// The root type must derive from <see cref="ContainerControl"/>, i.e. "Form", "Page" or "UserControl", and have a public
		/// parameterless constructor. Event handler names are looked up among the methods declared by the root type, therefore
		/// set "_type" to the full name of your own class (i.e. "MyApp.CustomerForm") to use its handlers.
		/// </remarks>
		/// <example>
		/// Creating and showing a form defined in JSON:
		/// <code><![CDATA[
		/// var form = (Form)ViewBuilder.Create(@"{
		///     ""_type"": ""Form"",
		///     ""text"": ""Hello"",
		///     ""size"": ""300,200"",
		///     ""controls"": [
		///         { ""_type"": ""Label"", ""text"": ""Hello World!"", ""dock"": ""Fill"" }
		///     ]
		/// }");
		/// form.Show();
		/// ]]></code>
		/// </example>
		public static ContainerControl Create(string json)
		{
			if (json == null)
				throw new ArgumentNullException(nameof(json));
			
			return Create(Parse(json));
		}

		/// <summary>
		/// Creates a new <see cref="ContainerControl"/> from the JSON representation read from the specified <paramref name="json"/> stream.
		/// </summary>
		/// <param name="json">Stream containing the JSON definition of the <see cref="ContainerControl"/> to create.</param>
		/// <returns>The new <see cref="ContainerControl"/> instance of the type specified in the "_type" member of the root object.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="json"/> is null.</exception>
		/// <exception cref="Exception">The "_type" member is missing or the type cannot be resolved.</exception>
		/// <exception cref="ArgumentException">A property cannot be assigned.</exception>
		/// <remarks>
		/// The stream is read but not closed.
		/// </remarks>
		/// <example>
		/// Creating a page from a JSON file deployed with the application:
		/// <code><![CDATA[
		/// using (var stream = File.OpenRead(Application.MapPath("Views/Dashboard.json")))
		/// {
		///     var page = (Page)ViewBuilder.Create(stream);
		///     page.Show();
		/// }
		/// ]]></code>
		/// </example>
		public static ContainerControl Create(Stream json)
		{
			if (json == null)
				throw new ArgumentNullException(nameof(json));


			return Create(Parse(json));
		}

		/// <summary>
		/// Creates a new <see cref="ContainerControl"/> from the specified <paramref name="model"/>
		/// representation.
		/// </summary>
		/// <param name="model">Object model of the <see cref="ContainerControl"/> to create.</param>
		/// <returns>The new <see cref="ContainerControl"/> instance of the type specified in the "_type" member of the root object.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is null.</exception>
		/// <exception cref="Exception">The "_type" member is missing or the type cannot be resolved.</exception>
		/// <exception cref="ArgumentException">A property cannot be assigned.</exception>
		/// <remarks>
		/// The model is accessed dynamically: the root object and every nested object that defines a component must support
		/// the string indexer used to read the "_type" member, like <see cref="DynamicObject"/> or the objects returned by
		/// <see cref="WisejSerializer.Parse(string)"/>.
		/// </remarks>
		/// <example>
		/// Creating a form from a <see cref="DynamicObject"/> model:
		/// <code><![CDATA[
		/// dynamic button = new DynamicObject();
		/// button._type = "Button";
		/// button.text = "OK";
		/// button.location = "10,10";
		///
		/// dynamic model = new DynamicObject();
		/// model._type = "Form";
		/// model.text = "Confirm";
		/// model.controls = new object[] { button };
		///
		/// var form = (Form)ViewBuilder.Create((object)model);
		/// form.ShowDialog();
		/// ]]></code>
		/// </example>
		public static ContainerControl Create(object model)
		{
			if (model == null)
				throw new ArgumentNullException(nameof(model));

			return (ContainerControl)CreateInstance(model);
		}

		/// <summary>
		/// Loads the controls specified in a <paramref name="json"/> representation into the
		/// existing <paramref name="container"/>.
		/// </summary>
		/// <param name="container">Container to load with the new controls.</param>
		/// <param name="json">JSON representation of the container properties and of the controls to create. When null or empty, nothing is loaded.</param>
		/// <returns>The <paramref name="container"/> instance.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
		/// <exception cref="Exception">The "_type" member of a child object is missing or the type cannot be resolved.</exception>
		/// <exception cref="ArgumentException">A property cannot be assigned.</exception>
		/// <remarks>
		/// The members of the root object are assigned to the <paramref name="container"/> and the new controls are added to
		/// its existing controls. Event handlers can refer to methods declared in the class of the <paramref name="container"/>,
		/// including private methods.
		/// </remarks>
		/// <example>
		/// Loading a view into a panel:
		/// <code><![CDATA[
		/// this.panel1.LoadView(@"{
		///     ""controls"": [
		///         { ""_type"": ""Button"", ""name"": ""buttonSave"", ""text"": ""Save"", ""dock"": ""Bottom"", ""click"": ""buttonSave_Click"" }
		///     ]
		/// }");
		/// ]]></code>
		/// </example>
		public static ContainerControl LoadView(this ContainerControl container, string json)
		{
			if (container == null)
				throw new ArgumentNullException(nameof(container));

			if (!String.IsNullOrEmpty(json))
				LoadView(container, Parse(json));

			return container;
		}

		/// <summary>
		/// Loads the controls specified in a <paramref name="jsonStream"/> representation into the
		/// existing <paramref name="container"/>.
		/// </summary>
		/// <param name="container">Container to load with the new controls.</param>
		/// <param name="jsonStream">Stream containing the JSON representation of the container properties and of the controls to create. When null, nothing is loaded.</param>
		/// <returns>The <paramref name="container"/> instance.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
		/// <exception cref="Exception">The "_type" member of a child object is missing or the type cannot be resolved.</exception>
		/// <exception cref="ArgumentException">A property cannot be assigned.</exception>
		/// <remarks>
		/// The stream is read but not closed.
		/// </remarks>
		/// <example>
		/// Loading the layout of a form from a JSON file:
		/// <code><![CDATA[
		/// private void Form1_Load(object sender, EventArgs e)
		/// {
		///     using (var stream = File.OpenRead(Application.MapPath("Views/Form1.json")))
		///     {
		///         this.LoadView(stream);
		///     }
		/// }
		/// ]]></code>
		/// </example>
		public static ContainerControl LoadView(this ContainerControl container, Stream jsonStream)
		{
			if (container == null)
				throw new ArgumentNullException(nameof(container));

			if (jsonStream != null)
				LoadView(container, Parse(jsonStream));

			return container;
		}

		/// <summary>
		/// Loads the controls specified in a <paramref name="model"/> representation into the
		/// existing <paramref name="container"/>.
		/// </summary>
		/// <param name="container">Container to load with the new controls.</param>
		/// <param name="model">Object model representation of the container properties and of the controls to create. When null, nothing is loaded.</param>
		/// <returns>The <paramref name="container"/> instance.</returns>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
		/// <exception cref="Exception">The "_type" member of a child object is missing or the type cannot be resolved.</exception>
		/// <exception cref="ArgumentException">A property cannot be assigned.</exception>
		/// <remarks>
		/// The model is accessed dynamically: every nested object that defines a control must support the string indexer used to
		/// read the "_type" member, like <see cref="DynamicObject"/> or the objects returned by <see cref="WisejSerializer.Parse(string)"/>.
		/// </remarks>
		/// <example>
		/// Adding a text box to the current form from a <see cref="DynamicObject"/> model:
		/// <code><![CDATA[
		/// dynamic textBox = new DynamicObject();
		/// textBox._type = "TextBox";
		/// textBox.name = "textBoxEmail";
		/// textBox.labelText = "Email:";
		/// textBox.dock = "Top";
		///
		/// dynamic model = new DynamicObject();
		/// model.controls = new object[] { textBox };
		///
		/// this.LoadView((object)model);
		/// ]]></code>
		/// </example>
		public static ContainerControl LoadView(this ContainerControl container, object model)
		{
			if (container == null)
				throw new ArgumentNullException(nameof(container));

			if (model != null)
				new ViewBuilder.Parser().Load(container, model);

			return container;
		}

		///// <summary>
		///// Serialized the <paramref name="container"/> to its JSON representation saved to the specified <paramref name="stream"/>.
		///// </summary>
		///// <param name="container">Container to serialize/</param>
		///// <param name="stream">Stream that receives the JSON representation.</param>
		///// <exception cref="ArgumentNullException"><paramref name="container"/> or <paramref name="stream"/> are null.</exception>
		//public static void SaveView(this ContainerControl container, Stream stream)
		//{
		//	if (container == null)
		//		throw new ArgumentNullException(nameof(container));

		//	if (stream == null)
		//		throw new ArgumentNullException(nameof(stream));

		//	using (var writer = new StreamWriter(stream))
		//	{
		//		writer.Write(WisejSerializer.Serialize(new ViewManager.Serializer().Serialize(container)));
		//	}
		//}

		#endregion

		#region Implementation

		private static object Parse(object json)
		{
			Debug.Assert(json != null);

			if (json is string text)
			{
				return WisejSerializer.Parse(text);
			}

			if (json is Stream stream)
			{
				return WisejSerializer.Parse(stream);
			}

			return null;
		}

		private static object CreateInstance(object model)
		{
			Debug.Assert(model != null);

			return new ViewBuilder.Parser().Parse(model, true);
		}

		#endregion

		#region Resolvers

		/// <summary>
		/// Resolves the name of component to the corresponding instance. It's invoked by the
		/// parser after the view has been loaded and names assigned to control properties need to be
		/// resolved.
		/// </summary>
		/// <remarks>
		/// <para>
		/// For example, the definition: `{targetLabel: "label1"}` is processed after the view has
		/// been loaded with all the controls in order to be able to resolve "label1" to the corresponding
		/// control instance.
		/// </para>
		/// <para>
		/// The default implementation searches all child controls staring from the top-level container.
		/// </para>
		/// </remarks>
		/// <param name="container">Top level container being loaded.</param>
		/// <param name="component">Component being assigned.</param>
		/// <param name="propertyName">Name of the property that will receive the returned value.</param>
		/// <param name="name">Name of the reference to resolve.</param>
		/// <returns>The value of the resolved reference or null.</returns>
		public static Func<ContainerControl, object, string, string, object> ResolveReference;

		/// <summary>
		/// Resolves the value in <paramref name="name"/> to a <see cref="MethodInfo"/> that
		/// can be attached to the event defined as <paramref name="descriptor"/>.
		/// </summary>
		/// <remarks>
		/// <para>
		/// The default implementation searches the top-level container first. Then it tries to resolve the
		/// fully qualified method name in any loaded assembly.
		/// </para>
		/// <para>
		/// A custom implementation could, for example, generate code on the fly and return a custom <see cref="MethodInfo"/>
		/// generated from a C# or VB.NET string; or it could also attach a custom method that executes javascript either on the
		/// client or on the server using the V8 engine.
		/// </para>
		/// </remarks>
		/// <param name="component">Component being attached to.</param>
		/// <param name="descriptor"><see cref="EventDescriptor"/> of the event to attach to.</param>
		/// <param name="name">Name or description of the event handler.</param>
		/// <returns></returns>
		public static Func<object, EventDescriptor, string, MethodInfo> ResolveEventHandler;

		#endregion
	}
}