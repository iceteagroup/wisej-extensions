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
	/// Loads or created a view (a <see cref="ContainerControl"/> instance) from or a JSON representation.
	/// </summary>
	/// <remarks>
	/// <para>
	/// The JSON string is rendered and parsed using camel casing. When using dynamic C# objects (see example below)
	/// you can use the standard C# proper casing, Wisej.NET will serialize it into camel-cased JSON.
	/// </para>
	/// <para>
	/// Events are either attached to existing handler methods, or to a dynamically compiled method from the
	/// code snippet defined in the string representation.
	/// </para>
	/// <para>
	/// Example:
	/// </para>
	/// <code lang="cs">
	/// <![CDATA[
	///	this.form1.LoadView(new {
	///		mame = "Form1",
	///		size = "200,200",
	///		windowState = "Maximized",
	///		// _type = "Wisej.Web.Form", <- Not necessary when loading into an existing ContainerControl.
	///		
	///		controls = new [] {
	///			new {
	///				_type = "TextBox",
	///				name = "textBox1",
	///				dock = "Top",
	///				labelText = "Name:",
	///				enabled = true,
	///				validating = "textBox1_OnValidating",
	///				toolTip1_ToolTip = "Enter the name"
	///			},
	///			new {
	///				_type = "Panel",
	///				dock = "Top",
	///				autoSize = true,
	///				controls = new [] {
	///					_type = "TextBox",
	///					name = "textBox1",
	///					location = "10,10",
	///					labelText = "Last Name:",
	///					label = new {
	///						text = "Changed Text"
	///					},
	///					
	///				}
	///			}
	///		},
	///		
	///		components = new []	{
	///			new {
	///				_type = "ToolTip",
	///				name = "ToolTip1"
	///			}
	///		}
	///		
	/// });
	/// ]]>
	/// </code>
	/// </remarks>
	public static partial class ViewBuilder
	{
		#region Methods

		/// <summary>
		/// Create a new <see cref="ContainerControl"/> from the specified <paramref name="json"/>
		/// representation.
		/// </summary>
		/// <param name="json">JSON definition of the <see cref="ContainerControl"/> to create.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="json"/> is null.</exception>
		public static ContainerControl Create(string json)
		{
			if (json == null)
				throw new ArgumentNullException(nameof(json));
			
			return Create(Parse(json));
		}

		/// <summary>
		/// Create a new <see cref="ContainerControl"/> from the specified <paramref name="json"/>
		/// representation.
		/// </summary>
		/// <param name="json">JSON definition of the <see cref="ContainerControl"/> to create.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="json"/> is null.</exception>
		public static ContainerControl Create(Stream json)
		{
			if (json == null)
				throw new ArgumentNullException(nameof(json));


			return Create(Parse(json));
		}

		/// <summary>
		/// Create a new <see cref="ContainerControl"/> from the specified <paramref name="model"/>
		/// representation.
		/// </summary>
		/// <param name="model">Object model of the <see cref="ContainerControl"/> to create.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException"><paramref name="model"/> is null.</exception>
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
		/// <param name="json">SON representation of the controls to create.</param>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
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
		/// <param name="jsonStream">JSON representation of the controls to create.</param>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
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
		/// <param name="model">Object model representation of the controls to create.</param>
		/// <exception cref="ArgumentNullException"><paramref name="container"/> is null.</exception>
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