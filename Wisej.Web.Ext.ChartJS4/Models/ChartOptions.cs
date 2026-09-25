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
	/// <remarks>
	/// An instance is exposed by <see cref="ChartJS4.ChartOptions"/> and serialized to the Chart.js <c>options</c> object.
	/// Nested option objects (<see cref="Plugins"/>, <see cref="Scales"/>, <see cref="Interaction"/>, <see cref="Transitions"/>,
	/// <see cref="Layout"/>) are created lazily on first access, and only non-default values are sent to the client.
	/// Changing any property refreshes the owning chart. Options not exposed as properties can be supplied through
	/// <see cref="ExtensionData"/> or <see cref="CustomOptions"/>.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// var chart = new ChartJS4();
	/// chart.ChartOptions.Plugins.Title.Display = true;
	/// chart.ChartOptions.Plugins.Title.Text = "Monthly Sales";
	/// chart.ChartOptions.Plugins.Legend.Position = "bottom";
	/// chart.ChartOptions.Scales.Y.Min = 0;
	/// ]]></code>
	/// </example>
	[ApiCategory("ChartJS4")]
	[TypeConverter(typeof(Converter))]
	public class ChartOptions : ChartModelBase
	{
		private bool _responsive = true;
		private bool _maintainAspectRatio = true;
		private int? _aspectRatio = 2;
		private int? _resizeDelay;
		private double? _devicePixelRatio;
		private string? _locale;
		private PluginsOptions? _plugins;
		private ScalesOptions? _scales;
		private InteractionOptions? _interaction;
		private object? _animations;
		private object? _animation;
		private TransitionsOptions? _transitions;
		private LayoutOptions? _layout;
		private object? _elements;
		private string? _type;

		/// <summary>
		/// Initializes a new instance of the <see cref="ChartOptions"/> class.
		/// </summary>
		/// <remarks>
		/// Nested option objects are not created by the constructor; they are instantiated lazily the first time
		/// the corresponding property is read.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var options = new ChartOptions { Responsive = true, AspectRatio = 1 };
		/// chart.ChartOptions = options;
		/// ]]></code>
		/// </example>
		public ChartOptions()
		{
			// Nested options are NOT instantiated by default
			// They will be lazy-loaded on first access
		}

		/// <summary>
		/// Returns or sets whether the chart canvas is resized when its container is.
		/// </summary>
		/// <value>
		/// <c>true</c> to resize the canvas with its container; otherwise <c>false</c>. The default is <c>true</c>.
		/// Maps to the Chart.js <c>responsive</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// bool responsive = chart.ChartOptions.Responsive;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("responsive")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Resizes the chart canvas when its container does.")]
		public bool Responsive
		{
			get => _responsive;
			set => SetProperty(ref _responsive, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Responsive"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Responsive"/> is not <c>true</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeResponsive())
		///     options.ResetResponsive();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeResponsive() => Responsive != true;

		/// <summary>
		/// Resets the <see cref="Responsive"/> property to its default value of <c>true</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetResponsive();
		/// ]]></code>
		/// </example>
		public void ResetResponsive() => Responsive = true;

		/// <summary>
		/// Returns or sets whether the original canvas aspect ratio (width / height) is maintained when resizing.
		/// </summary>
		/// <value>
		/// <c>true</c> to keep the aspect ratio defined by <see cref="AspectRatio"/>; <c>false</c> to fill the container.
		/// The default is <c>true</c>. Maps to the Chart.js <c>maintainAspectRatio</c> option.
		/// </value>
		/// <remarks>
		/// A value of <c>false</c> lets the chart fill the whole client area of the control. Note that <c>false</c>
		/// is currently not sent to the client, because default values are omitted during serialization, so
		/// Chart.js keeps its default of <c>true</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4 { Dock = DockStyle.Fill };
		/// bool keepRatio = chart.ChartOptions.MaintainAspectRatio;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("maintainAspectRatio")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(true)]
		[Description("Maintain the original canvas aspect ratio (width / height) when resizing.")]
		public bool MaintainAspectRatio
		{
			get => _maintainAspectRatio;
			set => SetProperty(ref _maintainAspectRatio, value);
		}

		/// <summary>
		/// Determines whether the <see cref="MaintainAspectRatio"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="MaintainAspectRatio"/> is not <c>true</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeMaintainAspectRatio())
		///     options.ResetMaintainAspectRatio();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeMaintainAspectRatio() => MaintainAspectRatio != true;

		/// <summary>
		/// Resets the <see cref="MaintainAspectRatio"/> property to its default value of <c>true</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetMaintainAspectRatio();
		/// ]]></code>
		/// </example>
		public void ResetMaintainAspectRatio() => MaintainAspectRatio = true;

		/// <summary>
		/// Returns or sets the canvas aspect ratio (i.e., width / height, a value of 1 representing a square canvas).
		/// </summary>
		/// <value>
		/// A nullable <see cref="int"/>. The default is <c>2</c>. Maps to the Chart.js <c>aspectRatio</c> option.
		/// </value>
		/// <remarks>
		/// Only applies when <see cref="MaintainAspectRatio"/> is <c>true</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.AspectRatio = 1; // square chart
		/// ]]></code>
		/// </example>
		[JsonPropertyName("aspectRatio")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(2)]
		[Description("Canvas aspect ratio (i.e., width / height).")]
		public int? AspectRatio
		{
			get => _aspectRatio;
			set => SetProperty(ref _aspectRatio, value);
		}

		/// <summary>
		/// Determines whether the <see cref="AspectRatio"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="AspectRatio"/> is not <c>2</c> (its default value); otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeAspectRatio())
		///     options.ResetAspectRatio();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeAspectRatio() => AspectRatio != 2;

		/// <summary>
		/// Resets the <see cref="AspectRatio"/> property to its default value of <c>2</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetAspectRatio();
		/// ]]></code>
		/// </example>
		public void ResetAspectRatio() => AspectRatio = 2;

		/// <summary>
		/// Returns or sets the delay, in milliseconds, of the resize update.
		/// </summary>
		/// <value>
		/// A nullable <see cref="int"/> in milliseconds. The default is <c>null</c> (Chart.js default <c>0</c>, resize immediately).
		/// Maps to the Chart.js <c>resizeDelay</c> option.
		/// </value>
		/// <remarks>
		/// Delaying the resize can improve performance when the container is resized frequently.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.ResizeDelay = 200;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("resizeDelay")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[DefaultValue(0)]
		[Description("Delay the resize update by milliseconds.")]
		public int? ResizeDelay
		{
			get => _resizeDelay;
			set => SetProperty(ref _resizeDelay, value);
		}

		/// <summary>
		/// Determines whether the <see cref="ResizeDelay"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="ResizeDelay"/> has a value other than <c>0</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeResizeDelay())
		///     options.ResetResizeDelay();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeResizeDelay() => ResizeDelay.HasValue && ResizeDelay.Value != 0;

		/// <summary>
		/// Resets the <see cref="ResizeDelay"/> property to its default value of <c>null</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetResizeDelay();
		/// ]]></code>
		/// </example>
		public void ResetResizeDelay() => ResizeDelay = null;

		/// <summary>
		/// Returns or sets a value that overrides the window's default devicePixelRatio.
		/// </summary>
		/// <value>
		/// A nullable <see cref="double"/>. The default is <c>null</c> (use the browser's <c>window.devicePixelRatio</c>).
		/// Maps to the Chart.js <c>devicePixelRatio</c> option.
		/// </value>
		/// <remarks>
		/// Setting a higher value renders the canvas at a higher resolution, e.g. to obtain sharper images from
		/// <see cref="ChartJS4.GetImageAsync"/> or when printing.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.DevicePixelRatio = 2.0;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("devicePixelRatio")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("Override the window's default devicePixelRatio.")]
		[DefaultValue(null)]
		public double? DevicePixelRatio
		{
			get => _devicePixelRatio;
			set => SetProperty(ref _devicePixelRatio, value);
		}

		/// <summary>
		/// Determines whether the <see cref="DevicePixelRatio"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="DevicePixelRatio"/> has a value; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeDevicePixelRatio())
		///     options.ResetDevicePixelRatio();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeDevicePixelRatio() => DevicePixelRatio.HasValue;

		/// <summary>
		/// Resets the <see cref="DevicePixelRatio"/> property to its default value of <c>null</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetDevicePixelRatio();
		/// ]]></code>
		/// </example>
		public void ResetDevicePixelRatio() => DevicePixelRatio = null;

		/// <summary>
		/// Returns or sets the chart's locale. Defaults to browser's locale.
		/// </summary>
		/// <value>
		/// A BCP 47 language tag such as <c>"en-US"</c> or <c>"de-DE"</c>. The default is <c>null</c> (browser locale).
		/// Maps to the Chart.js <c>locale</c> option.
		/// </value>
		/// <remarks>
		/// The locale is used by Chart.js to format numbers (e.g. tick labels and tooltips).
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Locale = "de-DE";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("locale")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull | JsonIgnoreCondition.WhenWritingDefault)]
		[Description("The chart's locale.")]
		[DefaultValue(null)]
		public string? Locale
		{
			get => _locale;
			set => SetProperty(ref _locale, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Locale"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Locale"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeLocale())
		///     options.ResetLocale();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeLocale() => Locale != null;

		/// <summary>
		/// Resets the <see cref="Locale"/> property to its default value of <c>null</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetLocale();
		/// ]]></code>
		/// </example>
		public void ResetLocale() => Locale = null;

		/// <summary>
		/// Returns or sets the plugins options (legend, title, subtitle, tooltip, decimation, filler and data labels).
		/// </summary>
		/// <value>
		/// A <see cref="PluginsOptions"/> instance. The getter never returns <c>null</c>: a new instance is created on first access.
		/// Serialized as the Chart.js <c>plugins</c> option through <see cref="PluginsForSerialization"/>.
		/// </value>
		/// <remarks>
		/// Because the object is created lazily, nested options can be set directly, e.g.
		/// <c>chart.ChartOptions.Plugins.Legend.Position = "bottom"</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Plugins.Title.Display = true;
		/// chart.ChartOptions.Plugins.Title.Text = "Quarterly Revenue";
		/// chart.ChartOptions.Plugins.Legend.Position = "bottom";
		/// ]]></code>
		/// </example>
		[JsonPropertyName("plugins")]
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Plugins options.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public PluginsOptions? Plugins
		{
			get
			{
				if (_plugins == null)
					_plugins = new PluginsOptions { Chart = Chart };
				return _plugins;
			}
			set => SetProperty(ref _plugins, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Plugins"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the plugins options were created and contain non-default values; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property. Does not create the lazy instance.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializePlugins())
		///     options.ResetPlugins();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializePlugins() => _plugins != null && !_plugins.IsDefault;

		/// <summary>
		/// Resets the <see cref="Plugins"/> property by discarding the current plugins options.
		/// </summary>
		/// <remarks>
		/// A new default <see cref="PluginsOptions"/> instance is created on the next access to <see cref="Plugins"/>.
		/// Used by the Visual Studio designer when the user resets the property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetPlugins();
		/// ]]></code>
		/// </example>
		public void ResetPlugins() => SetProperty(ref _plugins, null);

		/// <summary>
		/// Returns the plugins options for JSON serialization only.
		/// </summary>
		/// <value>
		/// The backing <see cref="PluginsOptions"/> instance, or <c>null</c> if <see cref="Plugins"/> was never accessed.
		/// </value>
		/// <remarks>
		/// Serialization-only backing property written as the Chart.js <c>plugins</c> option. Unlike <see cref="Plugins"/>,
		/// it does not create the instance lazily, so unused plugins options are omitted from the JSON.
		/// It is hidden from the designer and IntelliSense; use <see cref="Plugins"/> in code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var json = System.Text.Json.JsonSerializer.Serialize(chart.ChartOptions.PluginsForSerialization);
		/// ]]></code>
		/// </example>
		[JsonPropertyName("plugins")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public PluginsOptions? PluginsForSerialization => _plugins;

		/// <summary>
		/// Returns or sets the scales (axes) options.
		/// </summary>
		/// <value>
		/// A <see cref="ScalesOptions"/> instance. The getter never returns <c>null</c>: a new instance is created on first access.
		/// Serialized as the Chart.js <c>scales</c> option through <see cref="ScalesForSerialization"/>.
		/// </value>
		/// <remarks>
		/// Configures the <c>x</c> and <c>y</c> axes of cartesian charts; additional axes can be added through
		/// <see cref="ScalesOptions.ExtensionData"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Scales.Y.Min = 0;
		/// chart.ChartOptions.Scales.Y.Max = 100;
		/// chart.ChartOptions.Scales.X.Title.Display = true;
		/// chart.ChartOptions.Scales.X.Title.Text = "Month";
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Scales options.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public ScalesOptions? Scales
		{
			get
			{
				if (_scales == null)
					_scales = new ScalesOptions { Chart = Chart };
				return _scales;
			}
			set => SetProperty(ref _scales, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Scales"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the scales options were created and contain non-default values; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property. Does not create the lazy instance.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeScales())
		///     options.ResetScales();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeScales() => _scales != null && !_scales.IsDefault;

		/// <summary>
		/// Resets the <see cref="Scales"/> property by discarding the current scales options.
		/// </summary>
		/// <remarks>
		/// A new default <see cref="ScalesOptions"/> instance is created on the next access to <see cref="Scales"/>.
		/// Used by the Visual Studio designer when the user resets the property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetScales();
		/// ]]></code>
		/// </example>
		public void ResetScales() => SetProperty(ref _scales, null);

		/// <summary>
		/// Returns the scales options for JSON serialization only.
		/// </summary>
		/// <value>
		/// The backing <see cref="ScalesOptions"/> instance, or <c>null</c> if <see cref="Scales"/> was never accessed.
		/// </value>
		/// <remarks>
		/// Serialization-only backing property written as the Chart.js <c>scales</c> option. Unlike <see cref="Scales"/>,
		/// it does not create the instance lazily, so unused scales options are omitted from the JSON.
		/// It is hidden from the designer and IntelliSense; use <see cref="Scales"/> in code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// bool hasScales = chart.ChartOptions.ScalesForSerialization != null;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("scales")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ScalesOptions? ScalesForSerialization => _scales;

		/// <summary>
		/// Returns or sets the interaction options that control how the chart reacts to mouse and touch events.
		/// </summary>
		/// <value>
		/// An <see cref="InteractionOptions"/> instance. The getter never returns <c>null</c>: a new instance is created on first access.
		/// Serialized as the Chart.js <c>interaction</c> option through <see cref="InteractionForSerialization"/>.
		/// </value>
		/// <remarks>
		/// These settings are shared by hover and tooltip interactions.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Interaction.Mode = "index";
		/// chart.ChartOptions.Interaction.Intersect = false;
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Interaction options.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public InteractionOptions? Interaction
		{
			get
			{
				if (_interaction == null)
					_interaction = new InteractionOptions { Chart = Chart };
				return _interaction;
			}
			set => SetProperty(ref _interaction, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Interaction"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the interaction options were created and contain non-default values; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property. Does not create the lazy instance.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeInteraction())
		///     options.ResetInteraction();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeInteraction() => _interaction != null && !_interaction.IsDefault;

		/// <summary>
		/// Resets the <see cref="Interaction"/> property by discarding the current interaction options.
		/// </summary>
		/// <remarks>
		/// A new default <see cref="InteractionOptions"/> instance is created on the next access to <see cref="Interaction"/>.
		/// Used by the Visual Studio designer when the user resets the property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetInteraction();
		/// ]]></code>
		/// </example>
		public void ResetInteraction() => SetProperty(ref _interaction, null);

		/// <summary>
		/// Returns the interaction options for JSON serialization only.
		/// </summary>
		/// <value>
		/// The backing <see cref="InteractionOptions"/> instance, or <c>null</c> if <see cref="Interaction"/> was never accessed.
		/// </value>
		/// <remarks>
		/// Serialization-only backing property written as the Chart.js <c>interaction</c> option. Unlike <see cref="Interaction"/>,
		/// it does not create the instance lazily, so unused interaction options are omitted from the JSON.
		/// It is hidden from the designer and IntelliSense; use <see cref="Interaction"/> in code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// bool hasInteraction = chart.ChartOptions.InteractionForSerialization != null;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("interaction")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public InteractionOptions? InteractionForSerialization => _interaction;

		/// <summary>
		/// Returns or sets the per-property animation configurations used when data changes.
		/// Accepts an <see cref="AnimationsOptions"/> instance or an anonymous object.
		/// </summary>
		/// <value>
		/// An <see cref="AnimationsOptions"/> instance, an anonymous object whose properties are animation names (e.g. <c>x</c>,
		/// <c>y</c>, <c>colors</c>, <c>numbers</c>) with Chart.js animation configurations as values, or <c>null</c>.
		/// The default is <c>null</c>. Serialized as the Chart.js <c>animations</c> option through <see cref="AnimationsForSerialization"/>.
		/// </value>
		/// <remarks>
		/// Unlike the other nested options, this property is not created lazily. For a single global animation
		/// configuration use <see cref="Animation"/> instead.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Animations = new
		/// {
		///     y = new { duration = 2000, easing = "easeOutBounce" }
		/// };
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Animations options for different data changes. Accepts an AnimationsOptions instance or an anonymous object.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public object? Animations
		{
			get => _animations;
			set => SetProperty(ref _animations, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Animations"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Animations"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeAnimations())
		///     options.ResetAnimations();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeAnimations() => _animations != null;

		/// <summary>
		/// Resets the <see cref="Animations"/> property to its default value of <c>null</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetAnimations();
		/// ]]></code>
		/// </example>
		public void ResetAnimations() => SetProperty(ref _animations, null);

		/// <summary>
		/// Returns the animations configuration for JSON serialization only.
		/// </summary>
		/// <value>
		/// The value of <see cref="Animations"/>, or <c>null</c> when not set.
		/// </value>
		/// <remarks>
		/// Serialization-only backing property written as the Chart.js <c>animations</c> option.
		/// It is hidden from the designer and IntelliSense; use <see cref="Animations"/> in code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// bool hasAnimations = chart.ChartOptions.AnimationsForSerialization != null;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("animations")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object? AnimationsForSerialization => _animations;

		/// <summary>
		/// Returns or sets the global animation configuration. Accepts an object or anonymous type with Chart.js animation properties
		/// (e.g., <c>new { duration = 1000, easing = "linear" }</c>).
		/// Different from <see cref="Animations"/> which configures per-property animations.
		/// </summary>
		/// <value>
		/// An object with Chart.js animation properties (<c>duration</c>, <c>easing</c>, <c>delay</c>, <c>loop</c>),
		/// an <see cref="AnimationsOptions"/> instance, <c>false</c> to disable all animations, or <c>null</c>.
		/// The default is <c>null</c> (Chart.js defaults). Maps to the Chart.js <c>animation</c> option.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Animation = new AnimationsOptions { Duration = 500, Easing = "easeInOutQuart" };
		///
		/// // Disable animations:
		/// chart.ChartOptions.Animation = false;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("animation")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Description("Global animation configuration (single animation config object).")]
		[DefaultValue(null)]
		public object? Animation
		{
			get => _animation;
			set => SetProperty(ref _animation, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Animation"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Animation"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeAnimation())
		///     options.ResetAnimation();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeAnimation() => Animation != null;

		/// <summary>
		/// Resets the <see cref="Animation"/> property to its default value of <c>null</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetAnimation();
		/// ]]></code>
		/// </example>
		public void ResetAnimation() => Animation = null;

		/// <summary>
		/// Returns or sets the transitions configuration, i.e. the animations applied for specific modes such as
		/// <c>active</c>, <c>resize</c>, <c>show</c> and <c>hide</c>.
		/// </summary>
		/// <value>
		/// A <see cref="TransitionsOptions"/> instance. The getter never returns <c>null</c>: a new instance is created on first access.
		/// Serialized as the Chart.js <c>transitions</c> option through <see cref="TransitionsForSerialization"/>.
		/// </value>
		/// <remarks>
		/// The transition modes are supplied through <see cref="TransitionsOptions.ExtensionData"/>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Transitions.ExtensionData = new Dictionary<string, object>
		/// {
		///     ["active"] = new { animation = new { duration = 400 } }
		/// };
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Transitions configuration.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public TransitionsOptions? Transitions
		{
			get
			{
				if (_transitions == null)
					_transitions = new TransitionsOptions { Chart = Chart };
				return _transitions;
			}
			set => SetProperty(ref _transitions, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Transitions"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the transitions options were created and contain non-default values; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property. Does not create the lazy instance.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeTransitions())
		///     options.ResetTransitions();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeTransitions() => _transitions != null && !_transitions.IsDefault;

		/// <summary>
		/// Resets the <see cref="Transitions"/> property by discarding the current transitions options.
		/// </summary>
		/// <remarks>
		/// A new default <see cref="TransitionsOptions"/> instance is created on the next access to <see cref="Transitions"/>.
		/// Used by the Visual Studio designer when the user resets the property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetTransitions();
		/// ]]></code>
		/// </example>
		public void ResetTransitions() => SetProperty(ref _transitions, null);

		/// <summary>
		/// Returns the transitions options for JSON serialization only.
		/// </summary>
		/// <value>
		/// The backing <see cref="TransitionsOptions"/> instance, or <c>null</c> if <see cref="Transitions"/> was never accessed.
		/// </value>
		/// <remarks>
		/// Serialization-only backing property written as the Chart.js <c>transitions</c> option. Unlike <see cref="Transitions"/>,
		/// it does not create the instance lazily, so unused transitions options are omitted from the JSON.
		/// It is hidden from the designer and IntelliSense; use <see cref="Transitions"/> in code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// bool hasTransitions = chart.ChartOptions.TransitionsForSerialization != null;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("transitions")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public TransitionsOptions? TransitionsForSerialization => _transitions;

		/// <summary>
		/// Returns or sets the layout configuration (padding around the chart area).
		/// </summary>
		/// <value>
		/// A <see cref="LayoutOptions"/> instance. The getter never returns <c>null</c>: a new instance is created on first access.
		/// Serialized as the Chart.js <c>layout</c> option through <see cref="LayoutForSerialization"/>.
		/// </value>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Layout.Padding.Top = 20;
		/// chart.ChartOptions.Layout.Padding.Bottom = 10;
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Layout configuration.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public LayoutOptions? Layout
		{
			get
			{
				if (_layout == null)
					_layout = new LayoutOptions { Chart = Chart };
				return _layout;
			}
			set => SetProperty(ref _layout, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Layout"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if the layout options were created and contain non-default values; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property. Does not create the lazy instance.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeLayout())
		///     options.ResetLayout();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeLayout() => _layout != null && !_layout.IsDefault;

		/// <summary>
		/// Resets the <see cref="Layout"/> property by discarding the current layout options.
		/// </summary>
		/// <remarks>
		/// A new default <see cref="LayoutOptions"/> instance is created on the next access to <see cref="Layout"/>.
		/// Used by the Visual Studio designer when the user resets the property.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetLayout();
		/// ]]></code>
		/// </example>
		public void ResetLayout() => SetProperty(ref _layout, null);

		/// <summary>
		/// Returns the layout options for JSON serialization only.
		/// </summary>
		/// <value>
		/// The backing <see cref="LayoutOptions"/> instance, or <c>null</c> if <see cref="Layout"/> was never accessed.
		/// </value>
		/// <remarks>
		/// Serialization-only backing property written as the Chart.js <c>layout</c> option. Unlike <see cref="Layout"/>,
		/// it does not create the instance lazily, so unused layout options are omitted from the JSON.
		/// It is hidden from the designer and IntelliSense; use <see cref="Layout"/> in code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// bool hasLayout = chart.ChartOptions.LayoutForSerialization != null;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("layout")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public LayoutOptions? LayoutForSerialization => _layout;

		/// <summary>
		/// Returns or sets the default configuration of the chart elements (points, lines, bars and arcs).
		/// Accepts an <see cref="ElementsOptions"/> instance or an anonymous object.
		/// </summary>
		/// <value>
		/// An <see cref="ElementsOptions"/> instance, an anonymous object with <c>point</c>, <c>line</c>, <c>bar</c> and/or
		/// <c>arc</c> members, or <c>null</c>. The default is <c>null</c>. Serialized as the Chart.js <c>elements</c> option
		/// through <see cref="ElementsForSerialization"/>.
		/// </value>
		/// <remarks>
		/// Unlike the other nested options, this property is not created lazily. Element options apply to all datasets
		/// unless overridden by the dataset properties.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4();
		/// chart.ChartOptions.Elements = new
		/// {
		///     line = new { tension = 0.3 },
		///     point = new { radius = 0 }
		/// };
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[Description("Elements configuration. Accepts an ElementsOptions instance or an anonymous object.")]
		[NotifyParentProperty(true)]
		[DefaultValue(null)]
		public object? Elements
		{
			get => _elements;
			set => SetProperty(ref _elements, value);
		}

		/// <summary>
		/// Determines whether the <see cref="Elements"/> property should be serialized by the designer.
		/// </summary>
		/// <returns><c>true</c> if <see cref="Elements"/> is not <c>null</c>; otherwise <c>false</c>.</returns>
		/// <remarks>Used by the Visual Studio designer to decide whether to generate code for the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// if (options.ShouldSerializeElements())
		///     options.ResetElements();
		/// ]]></code>
		/// </example>
		public bool ShouldSerializeElements() => _elements != null;

		/// <summary>
		/// Resets the <see cref="Elements"/> property to its default value of <c>null</c>.
		/// </summary>
		/// <remarks>Used by the Visual Studio designer when the user resets the property.</remarks>
		/// <example>
		/// <code><![CDATA[
		/// chart.ChartOptions.ResetElements();
		/// ]]></code>
		/// </example>
		public void ResetElements() => SetProperty(ref _elements, null);

		/// <summary>
		/// Returns the elements configuration for JSON serialization only.
		/// </summary>
		/// <value>
		/// The value of <see cref="Elements"/>, or <c>null</c> when not set.
		/// </value>
		/// <remarks>
		/// Serialization-only backing property written as the Chart.js <c>elements</c> option.
		/// It is hidden from the designer and IntelliSense; use <see cref="Elements"/> in code.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// bool hasElements = chart.ChartOptions.ElementsForSerialization != null;
		/// ]]></code>
		/// </example>
		[JsonPropertyName("elements")]
		[JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object? ElementsForSerialization => _elements;

		/// <summary>
		/// Returns or sets additional custom properties that can be serialized to JSON.
		/// This allows for maximum flexibility when working with Chart.js options.
		/// </summary>
		/// <value>
		/// A dictionary of option names and values, or <c>null</c>. The default is <c>null</c>.
		/// </value>
		/// <remarks>
		/// Marked with <see cref="JsonExtensionDataAttribute"/>: every entry is written as an additional top-level property of
		/// the Chart.js <c>options</c> object, next to the typed properties. Use it for any Chart.js or plugin option that is
		/// not exposed as a property (e.g. <c>indexAxis</c> or <c>events</c>).
		/// <see cref="CustomOptions"/> fills this dictionary from a JSON string. Changes to the dictionary do not refresh
		/// the chart automatically.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4 { ChartType = ChartType.Bar };
		/// chart.ChartOptions.ExtensionData = new Dictionary<string, object>
		/// {
		///     ["indexAxis"] = "y",
		///     ["events"] = new[] { "click", "mousemove" }
		/// };
		/// ]]></code>
		/// </example>
		[JsonExtensionData]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public System.Collections.Generic.Dictionary<string, object>? ExtensionData { get; set; }

		/// <summary>
		/// Returns or sets a string that overrides the chart type (e.g., "customBubble") used in the top-level Chart.js config.
		/// Not serialized into the <c>options</c> object; consumed directly by the widget.
		/// </summary>
		/// <value>
		/// A Chart.js chart type name, or <c>null</c>. The default is <c>null</c>, in which case the lower-cased
		/// <see cref="ChartJS4.ChartType"/> is used.
		/// </value>
		/// <remarks>
		/// Use this property to select a custom chart type (controller) registered on the client, or a built-in
		/// type name not covered by the <see cref="ChartType"/> enumeration.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4 { ChartType = ChartType.Custom };
		/// chart.ChartOptions.Type = "customBubble";
		/// ]]></code>
		/// </example>
		[JsonIgnore(Condition = JsonIgnoreCondition.Always)]
		[DefaultValue(null)]
		[Description("Overrides the chart type string in the Chart.js config.")]
		public string? Type
		{
			get => _type;
			set => SetProperty(ref _type, value);
		}

		/// <summary>
		/// Sets raw JSON to merge into the top-level chart options (ExtensionData).
		/// Each top-level JSON key becomes an entry in <see cref="ExtensionData"/>.
		/// </summary>
		/// <value>
		/// A JSON object string, e.g. <c>{ "indexAxis": "y" }</c>. This property is write-only.
		/// </value>
		/// <remarks>
		/// The JSON is parsed and each top-level property is added to (or replaces the existing entry in)
		/// <see cref="ExtensionData"/>, which creates the dictionary if needed; the chart is then refreshed.
		/// Null or whitespace values are ignored, and invalid JSON or JSON that is not an object is silently ignored
		/// (the chart is still refreshed). The typed properties are not affected.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var chart = new ChartJS4 { ChartType = ChartType.Bar };
		/// chart.ChartOptions.CustomOptions = @"{
		///     ""indexAxis"": ""y"",
		///     ""events"": [""click"", ""mousemove""]
		/// }";
		/// ]]></code>
		/// </example>
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

					Update();
				}
			}
		}

		/// <inheritdoc/>
		protected override void OnChartChanged()
		{
			if (_plugins != null)
				_plugins.Chart = Chart;
			if (_scales != null)
				_scales.Chart = Chart;
			if (_interaction != null)
				_interaction.Chart = Chart;
			if (_transitions != null)
				_transitions.Chart = Chart;
			if (_layout != null)
				_layout.Chart = Chart;

			if (_animations is ChartModelBase animationsModel)
				animationsModel.Chart = Chart;
			if (_animation is ChartModelBase animationModel)
				animationModel.Chart = Chart;
			if (_elements is ChartModelBase elementsModel)
				elementsModel.Chart = Chart;
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
