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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Wisej.Web.Ext.ViewBuilder
{
	public static partial class ViewBuilder
	{
		internal class Parser
		{
			private List<BindingAssignment> _bindings = new List<BindingAssignment>();
			private List<ReferenceAssignment> _referenceAssignments = new List<ReferenceAssignment>();

			#region Context

			private Stack _context = new Stack();

			private ContainerControl Root { get; set; }

			private object PopContext()
				=> _context.Pop();

			private void PushContext(object instance)
				=> _context.Push(instance);

			private object Context
				=> _context.Count > 0 ? _context.Peek() : this.Root;

			#endregion

			#region Methods

			public object Parse(dynamic model, bool root, params object[] args)
			{
				Debug.Assert((object)model != null);

				// resolve the type.
				string typeName = model["_type"] ?? model["_Type"];
				if (String.IsNullOrEmpty(typeName))
					throw new Exception($"Cannot create a control without a type name.");

				// default to Wisej.Web if the namespace is not defined.
				if (!typeName.Contains("."))
					typeName = "Wisej.Web." + typeName + ", Wisej.Framework";

				var type = FindType(typeName);
				if (type == null)
					throw new Exception($"Cannot resolve type:{typeName}");

				var target = Activator.CreateInstance(type, args);

				if (root)
				{
					this.Root = (ContainerControl)target;
					CreateComponents(model.components ?? model.Components);
					AssignProperties(target, model);
					ResolveReferences();
					ResolveBindings();
				}
				else
				{
					AssignProperties(target, model);
				}

				return target;
			}

			internal object Load(ContainerControl container, dynamic model)
			{
				Debug.Assert(container != null);
				Debug.Assert((object)model != null);

				this.Root = container;
				CreateComponents(model.components ?? model.Components);
				AssignProperties(container, model);
				ResolveReferences();
				ResolveBindings();

				return container;
			}

			#endregion

			#region Implementation

			private void ResolveReferences()
			{
				var root = this.Root;
				var container = root.UserData.Container as IContainer;
				foreach (var r in _referenceAssignments)
				{
					var value = ViewBuilder.ResolveReference?.Invoke(root, r.Target, r.Property.Name, r.ReferenceName);
					if (value == null)
						value = root.Controls.Find(r.ReferenceName, true).FirstOrDefault();
					if (value == null && container != null)
						value = container.Components[r.ReferenceName];

					r.Property.SetValue(r.Target, value);
				}
			}

			private void ResolveBindings()
			{
				var root = this.Root;

				foreach (var b in _bindings)
				{
					var dataSource = ResolveDataSource(b.Target, b.PropertyName, b.DataSourcePath);

					var binding = new Binding(b.PropertyName, dataSource, b.MemberPath);
					binding.FormatString = b.Format;
					binding.ControlUpdateMode = b.ControlUpdateMode;
					binding.DataSourceUpdateMode = b.DataSourceUpdateMode;
					binding.FormattingEnabled = !String.IsNullOrEmpty(b.Format);

					var eventDescriptors = TypeDescriptor.GetEvents(binding);
					var parseEvent = eventDescriptors.Find("Parse", false);
					var formatEvent = eventDescriptors.Find("Format", false);

					var parseHandler = ResolveEventHandler(b.Target, parseEvent, b.OnFormatHandler);
					var formatHandler = ResolveEventHandler(b.Target, formatEvent, b.OnFormatHandler);

					if (formatHandler != null)
					{
						if (formatHandler.IsStatic)
							formatEvent.AddEventHandler(binding, formatHandler.CreateDelegate(formatEvent.EventType, null));
						else
							formatEvent.AddEventHandler(binding, formatHandler.CreateDelegate(formatEvent.EventType, root));
					}

					if (parseHandler != null)
					{
						if (parseHandler.IsStatic)
							parseEvent.AddEventHandler(binding, parseHandler.CreateDelegate(parseEvent.EventType, null));
						else
							parseEvent.AddEventHandler(binding, parseHandler.CreateDelegate(parseEvent.EventType, root));
					}

					b.Target.DataBindings.Add(binding);
				}
			}

			private object ResolveDataSource(object target, string propertyName, string path)
			{
				// if empty == this
				if (string.IsNullOrEmpty(path))
					return this.Root;

				var root = this.Root;
				var container = root.UserData.Container as IContainer;

				var value = ViewBuilder.ResolveReference?.Invoke(root, target, propertyName, path);
				if (value == null)
					value = root.Controls.Find(path, true).FirstOrDefault();
				if (value == null && container != null)
					value = container.Components[path];
				if (value == null)
					value = FindProperty(root, path);
				if (value == null)
					value = FindType(path);

				return value;
			}

			private object FindProperty(object target, string path)
			{
				object value = null;
				var fields = path.Split('.');
				for (var i = 0; i < fields.Length; i++)
				{
					var f = fields[i];

					if (i == 0 && f == "this")
					{
						value = target;
						continue;
					}

					var property = TypeDescriptor.GetProperties(value)[f];
					if (property == null)
						return null;

					value = property.GetValue(value);
				}

				return value;
			}

			private Type FindType(string fullName)
			{
				lock (TypeCache)
				{
					Type type = null;
					if (TypeCache.TryGetValue(fullName, out type))
						return type;

					type = Type.GetType(fullName);

					if (type == null)
						type = AppDomain.CurrentDomain.GetAssemblies()
									.Where(a => !a.IsDynamic)
									.SelectMany(a => a.GetTypes())
									.FirstOrDefault(t => t.FullName.Equals(fullName));

					TypeCache[fullName] = type;

					return type;
				}
			}
			private static Dictionary<string, Type> TypeCache = new Dictionary<string, Type>();

			private object AssignProperties(
				object component,
				dynamic model)
			{
				Debug.Assert(component != null);
				Debug.Assert((object)model != null);

				var componentEvents = TypeDescriptor.GetEvents(component);
				var sourceProperties = TypeDescriptor.GetProperties(model);
				var componentProperties = TypeDescriptor.GetProperties(component);

				// convert and assign all properties.
				foreach (PropertyDescriptor sourceProperty in sourceProperties)
				{
					// skip system names.
					if (sourceProperty.Name.StartsWith("_"))
						continue;

					// skip top-level "components" property.
					if (String.Equals(sourceProperty.Name, "components", StringComparison.InvariantCultureIgnoreCase) && this.Context == this.Root)
						return false;

					var eventDescriptor = componentEvents.Find(sourceProperty.Name, true);
					if (eventDescriptor != null)
						AssignEventHandler(component, model, eventDescriptor, sourceProperty);
					else
						AssignProperty(component, model, componentProperties, sourceProperty.Name, sourceProperty);
				}

				return component;
			}

			private bool AssignEventHandler(
					object component,
					dynamic source,
					EventDescriptor eventDescriptor,
					PropertyDescriptor sourceProperty)
			{
				Debug.Assert(component != null);
				Debug.Assert(eventDescriptor != null);
				Debug.Assert(sourceProperty != null);

				var root = this.Root;
				var eventType = eventDescriptor.EventType;
				var sourceValue = sourceProperty.GetValue(source);

				if (sourceValue is string eventMethodName && !String.IsNullOrEmpty(eventMethodName))
				{
					var method = ResolveEventHandler(component, eventDescriptor, eventMethodName);

					if (method != null)
					{
						if (method.IsStatic)
							eventDescriptor.AddEventHandler(component, method.CreateDelegate(eventType, null));
						else
							eventDescriptor.AddEventHandler(component, method.CreateDelegate(eventType, root));

						return true;
					}
				}

				return false;
			}

			private MethodInfo ResolveHandler(
				string handlerName,
				Type handlerType)
			{
				var path = handlerName.Split('.');
				if (path.Length < 2)
					return null;

				var typeName = String.Join(".", path, 0, path.Length - 1);

				var type = FindType(typeName);

				if (type != null)
				{
					var eventMethodName = path[path.Length - 1];
					return type.GetMethod(
						eventMethodName,
							BindingFlags.NonPublic | BindingFlags.Public |
							BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
				}

				return null;
			}

			private bool AssignProperty(
				object component,
				dynamic source,
				PropertyDescriptorCollection componentProperties,
				string propertyName,
				PropertyDescriptor sourceProperty)
			{
				Debug.Assert(component != null);
				Debug.Assert(propertyName != null);
				Debug.Assert(sourceProperty != null);
				Debug.Assert(componentProperties != null);

				try
				{
					if (propertyName.IndexOf('.') > 0)
					{
						// assign a property path.
						return AssignPropertyField(component, source, componentProperties, propertyName, sourceProperty);
					}
					else
					{
						var targetProperty = componentProperties.Find(propertyName, true);
						if (targetProperty == null)
						{
							// could be a property from an IExtenderProvider.
							return AssignExtenderProperty(component, source, propertyName, sourceProperty);
						}
						else
						{
							return AssignComponentProperty(component, source, targetProperty, sourceProperty);
						}
					}
				}
				catch (Exception ex)
				{
					throw new ArgumentException($"Error setting the property {propertyName} on {component}.", ex);
				}
			}

			private void CreateComponents(dynamic components)
			{
				if ((object)components == null)
					return;

				var root = this.Root;
				IContainer container = root.UserData.Container;

				if (container == null)
				{
					container = new Container();
					root.UserData.Container = container;
					root.Disposed += this.Container_Disposed;
				}

				foreach (dynamic model in components)
				{
					var component = Parse(model, false);
					var name =
						model.name
						?? model.Name
						?? component.GetType().Name;

					container.Add(component, name);
				}
			}

			private void Container_Disposed(object sender, EventArgs e)
			{
				var container = ((Control)sender).UserData.Container as IDisposable;
				if (container != null)
					container.Dispose();
			}

			private bool AssignPropertyField(
				object component,
				dynamic source,
				PropertyDescriptorCollection componentProperties,
				string propertyName,
				PropertyDescriptor sourceProperty)
			{
				Debug.Assert(component != null);
				Debug.Assert(propertyName != null);
				Debug.Assert(sourceProperty != null);
				Debug.Assert(componentProperties != null);

				var path = propertyName.Split('.');
				if (path.Length < 2)
					throw new ArgumentException($"Invalid property {propertyName}.");

				var targetName = path[0];
				var targetProperty = componentProperties.Find(targetName, true);
				if (targetProperty == null)
				{
					// try the extenders.
					if (AssignExtenderProperty(component, source, propertyName, sourceProperty))
						return true;

					throw new ArgumentException($"Unknown property {targetName} on {component.GetType().FullName}");
				}

				var target = targetProperty.GetValue(component);
				var targetProperties = TypeDescriptor.GetProperties(target);
				var fieldName = String.Join(".", path, 1, path.Length - 1);

				return AssignProperty(target, source, targetProperties, fieldName, sourceProperty);
			}

			private bool AssignExtenderProperty(
				object component,
				dynamic source,
				string propertyName,
				PropertyDescriptor sourceProperty)
			{
				Debug.Assert(component != null);
				Debug.Assert(propertyName != null);
				Debug.Assert(sourceProperty != null);

				bool simpleProperty = false;
				IExtenderProvider extender = null;

				// simple property? parent is an extender?
				if (propertyName.IndexOf('.') == -1 && propertyName.IndexOf('_') == -1)
				{
					simpleProperty = true;
					extender = this.Context as IExtenderProvider;
				}
				else
				{
					// find the extender.
					var path = propertyName.Split('.');
					if (path.Length == 1)
						path = propertyName.Split('_');

					simpleProperty = path.Length == 2;
					propertyName = String.Join(".", path, 1, path.Length - 1);
					IContainer container = this.Root.UserData.Container;
					if (container != null)
						extender = container.Components[path[0]] as IExtenderProvider;
				}

				if (extender != null)
				{
					object value = sourceProperty.GetValue(source);

					if (simpleProperty)
						AssignExtenderSimpleProperty(extender, component, propertyName, value);
					else
						AssignExtenderComplexProperty(extender, component, propertyName, value);

					return true;
				}

				return false;
			}

			private void AssignExtenderSimpleProperty(
				IExtenderProvider extender,
				object component,
				string propertyName,
				object value)
			{

				var methods = extender.GetType().GetMethods();
				var setter = methods.FirstOrDefault(m => String.Equals(m.Name, $"Set{propertyName}", StringComparison.InvariantCultureIgnoreCase));
				var getter = methods.FirstOrDefault(m => String.Equals(m.Name, $"Get{propertyName}", StringComparison.InvariantCultureIgnoreCase));
				if (setter != null)
				{
					var property = new ExtenderPropertyDescriptor(propertyName, extender, setter, getter);
					AssignProperty(component, value, property);
					return;
				}

				throw new ArgumentException($"Invalid extender property {extender}.{propertyName}.");
			}

			private object CoerceArgument(Type parameterType, object value)
			{
				if (value == null || parameterType == null)
					return value;

				if (value is Array array)
				{
					var type = parameterType.GetElementType() ?? array.GetType().GetElementType();
					var argArray = Array.CreateInstance(type, array.Length);

					int i = 0;
					foreach (var item in array)
					{
						var itemType = item?.GetType();
						if (itemType != null && (!itemType.IsClass || itemType == typeof(string)))
							argArray.SetValue(item, i++);
						else
							argArray.SetValue(Parse(item, false), i++);
					}

					return argArray;
				}
				else
				{
					return Convert.ChangeType(value, parameterType);
				}
			}

			private void AssignExtenderComplexProperty(
				IExtenderProvider extender,
				object component,
				string propertyName,
				object value)
			{
				var path = propertyName.Split('.');
				var getter = extender.GetType().GetMethod($"Get{path[0]}");
				if (getter != null)
				{
					var target = getter.Invoke(extender, new object[] { component });
					if (target != null)
					{
						var property = TypeDescriptor.GetProperties(target)[path[1]];
						if (property != null)
						{
							AssignProperty(target, value, property);
							return;
						}
					}
				}

				throw new ArgumentException($"Invalid extender property {extender}.{propertyName}.");
			}

			private bool AssignComponentProperty(
				object target,
				object source,
				PropertyDescriptor targetProperty,
				PropertyDescriptor sourceProperty)
			{
				Debug.Assert(target != null);
				Debug.Assert(source != null);
				Debug.Assert(targetProperty != null);
				Debug.Assert(sourceProperty != null);

				object value = sourceProperty.GetValue(source);
				AssignProperty(target, value, targetProperty);

				return true;
			}

			private void AssignProperty(
				object target,
				object value,
				PropertyDescriptor targetProperty)
			{
				if (value is IList)
				{
					var targetList = (IList)targetProperty.GetValue(target);
					if (targetList != null && !targetList.IsReadOnly)
					{
						AssignList(target, (IList)value, targetList, targetProperty);
						return;
					}
				}

				if (target is IBindableComponent &&
						 value is string binding &&
						 binding.EndsWith(BINDING_END) && 
						 binding.StartsWith(BINDING_START, StringComparison.InvariantCultureIgnoreCase))
				{
					AssignBinding((IBindableComponent)target, targetProperty, binding);
					return;
				}

				// treat the property as if it was a DataSource, it most-likely is!
				if (targetProperty.PropertyType == typeof(object))
				{
					if (value is string referenceName)
					{
						// delayed assignment in case the property is referring to another component.
						_referenceAssignments.Add(new ReferenceAssignment
						{
							Target = target,
							Property = targetProperty,
							ReferenceName = referenceName
						});

						return;
					}
				}

				var converter = targetProperty.Converter;
				if (converter != null && value != null && converter.CanConvertFrom(value.GetType()))
				{
					targetProperty.SetValue(
						target,
						converter.ConvertFrom(value));
					return;
				}

				try
				{
					targetProperty.SetValue(
						target,
						CoerceArgument(targetProperty.PropertyType, value));
				}
				catch (InvalidCastException)
				{
					if (value is string referenceName && (targetProperty.PropertyType.IsInterface || targetProperty.PropertyType.IsClass))
					{
						// delayed assignment in case the property is referring to another component.
						_referenceAssignments.Add(new ReferenceAssignment
						{
							Target = target,
							Property = targetProperty,
							ReferenceName = referenceName
						});

						return;
					}

					throw;
				}
			}

			private const string BINDING_END = "}";
			private const string BINDING_START = "{Binding ";
			private const string BINDING_SOURCE = "Source=";
			private const string BINDING_FORMAT = "Format=";
			private const string BINDING_ONPARSE = "OnParse=";
			private const string BINDING_ONFORMAT = "OnFormat=";
			private const string BINDING_SOURCEUPDATEMODE = "SourceUpdateMode=";
			private const string BINDING_CONTROLUPDATEMODE = "ControlUpdateMode=";

			private void AssignBinding(
				IBindableComponent component,
				PropertyDescriptor targetProperty,
				string bindingDescriptor)
			{
				// Syntax:
				//	{Binding} => binds to this
				//  {Binding Name} => binds to this.Name
				//  {Binding Product.Name} => binds to this.Product.Name
				//  {Binding Product.Name, Source=DataContext} => binds to this.DataContext.Product.Name
				//  {Binding Product.Name, Source=App.Model.Persons} => binds to Product.Name using typeof(App.Model.Persons), if Persons is a class.
				//  {Binding Product.Value, Source=bsData1, Format=Amount: c} => binds to this.Product.Value and formats it as "Amount: $123.00"
				//  {Binding Product.Value, OnFormat=Format_Handler, OnParse=Parse_Handler} => binds to this.Product.Value and attaches the OnFormat and OnParse handlers.
				//  {Binding Product.Name, Source=DataContext, ControlUpdateMode=Never, SourceUpdateMode=OnPropertChange} => binds to this.DataContext.Product.Name

				var propertyName = ""; // the control's property name to bind to.
				var memberPath = ""; // the path to the field in the data source. empty = the data source itself.
				var sourcePath = ""; // the path to the data source. empty = this => the container.
				var onFormatHandler = ""; // name of the format event handler.
				var onParseHandler = ""; // name of the parse event handler.
				var format = ""; // formatting string.
				var sourceUpdateMode = "OnValidation"; // DataSourceUpdateMode
				var controlUpdateMode = "OnPropertyChanged"; // ControlUpdateMode

				var parts = bindingDescriptor
							.Substring(
								BINDING_START.Length, 
								bindingDescriptor.Length - BINDING_END.Length - BINDING_START.Length)
							.Split(',');

				propertyName = targetProperty.Name;

				for (var i = 0; i < parts.Length; i++)
				{
					var value = parts[i].Trim();

					if (value.StartsWith(BINDING_SOURCE, StringComparison.InvariantCultureIgnoreCase))
						sourcePath = value.Substring(BINDING_SOURCE.Length);
					else if (value.StartsWith(BINDING_FORMAT, StringComparison.InvariantCultureIgnoreCase))
						format = value.Substring(BINDING_FORMAT.Length);
					else if (value.StartsWith(BINDING_ONFORMAT, StringComparison.InvariantCultureIgnoreCase))
						onFormatHandler = value.Substring(BINDING_ONFORMAT.Length);
					else if (value.StartsWith(BINDING_ONPARSE, StringComparison.InvariantCultureIgnoreCase))
						onParseHandler = value.Substring(BINDING_ONPARSE.Length);
					else if (value.StartsWith(BINDING_SOURCEUPDATEMODE, StringComparison.InvariantCultureIgnoreCase))
						sourceUpdateMode = value.Substring(BINDING_SOURCEUPDATEMODE.Length);
					else if (value.StartsWith(BINDING_CONTROLUPDATEMODE, StringComparison.InvariantCultureIgnoreCase))
						controlUpdateMode = value.Substring(BINDING_CONTROLUPDATEMODE.Length);
					else if (i == 0)
						memberPath = value;
				}

				var binding = new BindingAssignment { 
					Target = component,
					PropertyName= propertyName,
					MemberPath = memberPath,
					DataSourcePath = sourcePath,
					Format = format,
					OnFormatHandler= onFormatHandler,
					OnParseHandler= onParseHandler,
					DataSourceUpdateMode = (DataSourceUpdateMode)Enum.Parse(typeof(DataSourceUpdateMode), sourceUpdateMode),
					ControlUpdateMode = (ControlUpdateMode)Enum.Parse(typeof(ControlUpdateMode), controlUpdateMode)
				};

				_bindings.Add(binding);
			}

			private MethodInfo ResolveEventHandler(object component, EventDescriptor eventDescriptor, string eventMethodName)
			{
				if (eventDescriptor == null)
					return null;

				if (String.IsNullOrEmpty(eventMethodName))
					return null;

				// ask the ViewManager.
				var method = ViewBuilder.ResolveEventHandler?.Invoke(component, eventDescriptor, eventMethodName);

				// handler in root?
				if (method == null)
					method = this.Root.GetType().GetMethod(
						eventMethodName,
							BindingFlags.NonPublic | BindingFlags.Public |
							BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);

				// resolve handler reference.
				if (method == null)
					method = ResolveHandler(eventMethodName, eventDescriptor.EventType);

				return method;
			}

			private void AssignList(
				object target,
				IList list,
				IList targetList,
				PropertyDescriptor targetProperty)
			{
				Debug.Assert(list != null);
				Debug.Assert(target != null);
				Debug.Assert(targetProperty != null);

				PushContext(target);
				try
				{
					foreach (var item in list)
					{
						var itemType = item?.GetType();
						if (itemType != null && (!itemType.IsClass || itemType == typeof(string)))
							targetList.Add(item);
						else
							targetList.Add(CoerceArgument(targetProperty.PropertyType.GetElementType(), Parse(item, false)));
					}
				}
				finally
				{
					PopContext();
				}
			}

			#endregion
		}
	}

	internal class ReferenceAssignment
	{
		internal object Target;
		internal string ReferenceName;
		internal PropertyDescriptor Property;
	}

	internal class BindingAssignment
	{
		internal IBindableComponent Target;
		internal string PropertyName;
		internal string Format;
		internal string MemberPath;
		internal string DataSourcePath;
		internal string OnFormatHandler;
		internal string OnParseHandler;
		internal ControlUpdateMode ControlUpdateMode;
		internal DataSourceUpdateMode DataSourceUpdateMode;
	}

	internal class ExtenderPropertyDescriptor : PropertyDescriptor
	{
		private MethodInfo _setter;
		private MethodInfo _getter;
		private IExtenderProvider _extender;

		public ExtenderPropertyDescriptor(
			string name,
			IExtenderProvider extender,
			MethodInfo setter,
			MethodInfo getter) : base(name, null)
		{
			this._setter = setter;
			this._getter = getter;
			this._extender = extender;
		}

		public override Type ComponentType
			=> _extender.GetType();

		public override bool IsReadOnly
			=> false;

		public override Type PropertyType
			=> _setter.GetParameters()[1].ParameterType;

		public override bool CanResetValue(object component)
			=> false;

		public override object GetValue(object component)
			=> _getter == null ? null : _getter.Invoke(_extender, new object[] { component });

		public override void ResetValue(object component)
			=> _setter.Invoke(_extender, new object[] { component, null });

		public override void SetValue(object component, object value)
			=> _setter.Invoke(_extender, new object[] { component, value });

		public override bool ShouldSerializeValue(object component)
			=> true;
	}
}