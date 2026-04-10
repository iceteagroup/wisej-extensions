///////////////////////////////////////////////////////////////////////////////
//
// (C) 2021 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text.Json.Serialization;
using Wisej.Core;

namespace Wisej.Web.Ext.ChartJS4.Models
{
	/// <summary>
	/// Base class for all chart options.
	/// Uses System.Text.Json for modern, flexible serialization.
	/// </summary>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class ChartOptions
	{
		private PluginsOptions? _plugins;
		private ScalesOptions? _scales;
		private InteractionOptions? _interaction;
		private object? _animations;
		private TransitionsOptions? _transitions;
		private LayoutOptions? _layout;
		private object? _elements;

		public ChartOptions()
		{
			// Nested options are NOT instantiated by default
			// They will be lazy-loaded on first access
		}

		/// <summary>
		/// Resizes the chart canvas when its container does.
		/// </summary>
		[JsonPropertyName("responsive")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Resizes the chart canvas when its container does.")]
		public bool Responsive { get; set; } = true;

		/// <summary>
		/// Determines whether the Responsive property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeResponsive() => Responsive != true;

		/// <summary>
		/// Resets the Responsive property to its default value.
		/// </summary>
		public void ResetResponsive() => Responsive = true;

		/// <summary>
		/// Maintain the original canvas aspect ratio (width / height) when resizing.
		/// </summary>
		[JsonPropertyName("maintainAspectRatio")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Maintain the original canvas aspect ratio (width / height) when resizing.")]
		public bool MaintainAspectRatio { get; set; } = true;

		/// <summary>
		/// Determines whether the MaintainAspectRatio property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeMaintainAspectRatio() => MaintainAspectRatio != true;

		/// <summary>
		/// Resets the MaintainAspectRatio property to its default value.
		/// </summary>
		public void ResetMaintainAspectRatio() => MaintainAspectRatio = true;

		/// <summary>
		/// Canvas aspect ratio (i.e., width / height, a value of 1 representing a square canvas).
		/// </summary>
		[JsonPropertyName("aspectRatio")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(2)]
		[Description("Canvas aspect ratio (i.e., width / height).")]
		public int? AspectRatio { get; set; } = 2;

		/// <summary>
		/// Determines whether the AspectRatio property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeAspectRatio() => AspectRatio != 2;

		/// <summary>
		/// Resets the AspectRatio property to its default value.
		/// </summary>
		public void ResetAspectRatio() => AspectRatio = 2;

		/// <summary>
		/// Delay the resize update by the given amount of milliseconds.
		/// </summary>
		[JsonPropertyName("resizeDelay")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Delay the resize update by milliseconds.")]
		public int? ResizeDelay { get; set; } = 0;

		/// <summary>
		/// Determines whether the ResizeDelay property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeResizeDelay() => ResizeDelay.HasValue && ResizeDelay.Value != 0;

		/// <summary>
		/// Resets the ResizeDelay property to its default value.
		/// </summary>
		public void ResetResizeDelay() => ResizeDelay = null;

		/// <summary>
		/// Override the window's default devicePixelRatio.
		/// </summary>
		[JsonPropertyName("devicePixelRatio")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Override the window's default devicePixelRatio.")]
		[DefaultValue(null)]
		public double? DevicePixelRatio { get; set; }

		/// <summary>
		/// Determines whether the DevicePixelRatio property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeDevicePixelRatio() => DevicePixelRatio.HasValue;

		/// <summary>
		/// Resets the DevicePixelRatio property to its default value.
		/// </summary>
		public void ResetDevicePixelRatio() => DevicePixelRatio = null;

		/// <summary>
		/// The chart's locale. Defaults to browser's locale.
		/// </summary>
		[JsonPropertyName("locale")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("The chart's locale.")]
		[DefaultValue(null)]
		public string? Locale { get; set; }

		/// <summary>
		/// Determines whether the Locale property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeLocale() => Locale != null;

		/// <summary>
		/// Resets the Locale property to its default value.
		/// </summary>
		public void ResetLocale() => Locale = null;

		[JsonPropertyName("plugins")]
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Plugins options.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public PluginsOptions? Plugins
		{
			get => _plugins ??= new PluginsOptions();
			set => _plugins = value;
		}

		/// <summary>
		/// Determines whether the Plugins property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializePlugins() => _plugins != null && !_plugins.IsDefault;

		/// <summary>
		/// Resets the Plugins property to its default value.
		/// </summary>
		public void ResetPlugins() => _plugins = null;

		[JsonPropertyName("plugins")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public PluginsOptions? PluginsForSerialization => _plugins;

		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Scales options.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public ScalesOptions? Scales
		{
			get => _scales ??= new ScalesOptions();
			set => _scales = value;
		}

		/// <summary>
		/// Determines whether the Scales property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeScales() => _scales != null && !_scales.IsDefault;

		/// <summary>
		/// Resets the Scales property to its default value.
		/// </summary>
		public void ResetScales() => _scales = null;

		[JsonPropertyName("scales")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ScalesOptions? ScalesForSerialization => _scales;

		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Interaction options.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public InteractionOptions? Interaction
		{
			get => _interaction ??= new InteractionOptions();
			set => _interaction = value;
		}

		/// <summary>
		/// Determines whether the Interaction property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeInteraction() => _interaction != null && !_interaction.IsDefault;

		/// <summary>
		/// Resets the Interaction property to its default value.
		/// </summary>
		public void ResetInteraction() => _interaction = null;

		[JsonPropertyName("interaction")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public InteractionOptions? InteractionForSerialization => _interaction;

		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Animations options for different data changes. Accepts an AnimationsOptions instance or an anonymous object.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public object? Animations
		{
			get => _animations;
			set => _animations = value;
		}

		/// <summary>
		/// Determines whether the Animations property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeAnimations() => _animations != null;

		/// <summary>
		/// Resets the Animations property to its default value.
		/// </summary>
		public void ResetAnimations() => _animations = null;

		[JsonPropertyName("animations")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object? AnimationsForSerialization => _animations;

		/// <summary>
		/// Global animation configuration. Accepts an object or anonymous type with Chart.js animation properties
		/// (e.g., <c>new { duration = 1000, easing = "linear" }</c>).
		/// Different from <see cref="Animations"/> which configures per-property animations.
		/// </summary>
		[JsonPropertyName("animation")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Global animation configuration (single animation config object).")]
		[DefaultValue(null)]
		public object? Animation { get; set; }

		/// <summary>
		/// Determines whether the Animation property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeAnimation() => Animation != null;

		/// <summary>
		/// Resets the Animation property to its default value.
		/// </summary>
		public void ResetAnimation() => Animation = null;

		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Transitions configuration.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public TransitionsOptions? Transitions
		{
			get => _transitions ??= new TransitionsOptions();
			set => _transitions = value;
		}

		/// <summary>
		/// Determines whether the Transitions property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeTransitions() => _transitions != null && !_transitions.IsDefault;

		/// <summary>
		/// Resets the Transitions property to its default value.
		/// </summary>
		public void ResetTransitions() => _transitions = null;

		[JsonPropertyName("transitions")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public TransitionsOptions? TransitionsForSerialization => _transitions;

		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Layout configuration.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public LayoutOptions? Layout
		{
			get => _layout ??= new LayoutOptions();
			set => _layout = value;
		}

		/// <summary>
		/// Determines whether the Layout property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeLayout() => _layout != null && !_layout.IsDefault;

		/// <summary>
		/// Resets the Layout property to its default value.
		/// </summary>
		public void ResetLayout() => _layout = null;

		[JsonPropertyName("layout")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public LayoutOptions? LayoutForSerialization => _layout;

		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Elements configuration. Accepts an ElementsOptions instance or an anonymous object.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public object? Elements
		{
			get => _elements;
			set => _elements = value;
		}

		/// <summary>
		/// Determines whether the Elements property should be serialized by the designer.
		/// </summary>
		public bool ShouldSerializeElements() => _elements != null;

		/// <summary>
		/// Resets the Elements property to its default value.
		/// </summary>
		public void ResetElements() => _elements = null;

		[JsonPropertyName("elements")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object? ElementsForSerialization => _elements;

		/// <summary>
		/// Additional custom properties that can be serialized to JSON.
		/// This allows for maximum flexibility when working with Chart.js options.
		/// </summary>
		[JsonExtensionData]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <summary>
		/// Overrides the chart type string (e.g., "customBubble") used in the top-level Chart.js config.
		/// Not serialized into the <c>options</c> object; consumed directly by the widget.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[DefaultValue(null)]
		[Description("Overrides the chart type string in the Chart.js config.")]
		public string? Type { get; set; }

		/// <summary>
		/// Sets raw JSON to merge into the top-level chart options (ExtensionData).
		/// Each top-level JSON key becomes an entry in <see cref="ExtensionData"/>.
		/// </summary>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string? CustomOptions
		{
			set
			{
				if (!string.IsNullOrWhiteSpace(value))
				{
					try
					{
						var doc = System.Text.Json.JsonDocument.Parse(value);
						if (doc.RootElement.ValueKind == System.Text.Json.JsonValueKind.Object)
						{
							ExtensionData ??= new System.Collections.Generic.Dictionary<string, object>();
							foreach (var prop in doc.RootElement.EnumerateObject())
								ExtensionData[prop.Name] = prop.Value.Clone();
						}
					}
					catch { /* ignore invalid JSON */ }
				}
			}
		}
	}

	internal class Converter : System.ComponentModel.ExpandableObjectConverter
	{
		public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
		{
			if (destinationType == typeof(string))
				return "(...)";

			return base.ConvertTo(context, culture, value, destinationType);
		}
	}
}
