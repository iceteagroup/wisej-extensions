using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using Wisej.Core;
using Wisej.Design;
using Wisej.Web;

namespace Wisej.Web.Ext.Mermaid
{
	/// <summary>
	/// Integrates Mermaid (https://mermaid.js.org/) diagrams as a Wisej widget.
	/// </summary>
	[ToolboxItem(true)]
	[ToolboxBitmap(typeof(Mermaid))]
	[ApiCategory("Mermaid")]
	public class Mermaid : Widget
	{
		private const string DefaultMermaidCdn = "https://cdn.jsdelivr.net/npm/mermaid@10/dist/mermaid.min.js";

		private string _diagram = "";
		private Font _diagramFont;
		private bool _autoRender = true;
		private int _renderDebounceMs;
		private int _diagramPadding = 16;
		private string _overflow = "auto";
		private bool _useMaxWidth = true;
		private string _mermaidScriptSource;

		public Mermaid()
		{
			RefreshPackages();
			ApplyOptions();
		}

		/// <summary>
		/// Returns or sets the Mermaid diagram source.
		/// </summary>
		[DefaultValue("")]
		[DesignerActionList]
		[Editor("Wisej.Design.HtmlEditor, Wisej.Framework.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=17bef35e11b84171",
				"System.Drawing.Design.UITypeEditor, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string Diagram
		{
			get => _diagram;
			set
			{
				_diagram = value ?? "";
				ApplyOptions();
			}
		}

		/// <summary>
		/// Returns Mermaid configuration options passed to mermaid.initialize(...).
		/// </summary>
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public MermaidConfiguration Config { get; } = new MermaidConfiguration();

		/// <summary>
		/// Optional CSS font used to render text inside the diagram.
		/// </summary>
		[DefaultValue(null)]
		public Font DiagramFont
		{
			get => _diagramFont;
			set
			{
				_diagramFont = value;
				ApplyOptions();
			}
		}

		/// <summary>
		/// When true, the widget automatically renders when the diagram changes.
		/// </summary>
		[DefaultValue(true)]
		public bool AutoRender
		{
			get => _autoRender;
			set
			{
				_autoRender = value;
				ApplyOptions();
			}
		}

		/// <summary>
		/// Optional debounce (ms) applied before rendering (0 disables debounce).
		/// </summary>
		[DefaultValue(0)]
		public int RenderDebounceMs
		{
			get => _renderDebounceMs;
			set
			{
				_renderDebounceMs = value < 0 ? 0 : value;
				ApplyOptions();
			}
		}

		/// <summary>
		/// Padding (px) applied around the diagram.
		/// </summary>
		[DefaultValue(16)]
		public int DiagramPadding
		{
			get => _diagramPadding;
			set
			{
				_diagramPadding = value < 0 ? 0 : value;
				ApplyOptions();
			}
		}

		/// <summary>
		/// CSS overflow applied to the container element.
		/// </summary>
		[DefaultValue("auto")]
		public string Overflow
		{
			get => _overflow;
			set
			{
				_overflow = string.IsNullOrWhiteSpace(value) ? "auto" : value;
				ApplyOptions();
			}
		}

		/// <summary>
		/// When true, the diagram SVG is constrained to max-width: 100%.
		/// </summary>
		[DefaultValue(true)]
		public bool UseMaxWidth
		{
			get => _useMaxWidth;
			set
			{
				_useMaxWidth = value;
				ApplyOptions();
			}
		}

		/// <summary>
		/// Optional script URL used to load Mermaid; defaults to jsdelivr.
		/// </summary>
		[DefaultValue(null)]
		public string MermaidScriptSource
		{
			get => _mermaidScriptSource;
			set
			{
				_mermaidScriptSource = string.IsNullOrWhiteSpace(value) ? null : value;
				RefreshPackages();
			}
		}

		/// <summary>
		/// Overridden to return our list of script resources.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override System.Collections.Generic.List<Package> Packages
		{
			// disable inlining or we lose the calling assembly in GetResourceString()/GetResourceURL().
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return base.Packages;
			}
		}

		/// <summary>
		/// Overridden to create our initialization script.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get { return GetResourceString("Wisej.Web.Ext.Mermaid.JavaScript.startup.js"); }
			set { }
		}

		public void UpdateConfig(System.Action<MermaidConfiguration> update)
		{
			if (update == null)
				return;

			update(this.Config);
			ApplyOptions();
		}

		public void ApplyOptions()
		{
			ApplyFontToConfig();

			dynamic options = new DynamicObject();
			options.diagram = _diagram ?? "";
			options.config = this.Config?.ToOptions() ?? new DynamicObject();
			options.autoRender = _autoRender;
			options.renderDebounceMs = _renderDebounceMs;
			options.padding = _diagramPadding;
			options.overflow = _overflow;
			options.useMaxWidth = _useMaxWidth;

			this.Options = options;
		}

		private void RefreshPackages()
		{
			base.Packages.Clear();
			base.Packages.Add(new Package()
			{
				Name = "mermaid.js",
				Source = _mermaidScriptSource ?? GetResourceURL("Wisej.Web.Ext.Mermaid.JavaScript.mermaid.min.js")
			});
		}

		private void ApplyFontToConfig()
		{
			if (_diagramFont == null)
				return;

			this.Config.FontFamily = FontToCssFontFamily(_diagramFont);
		}

		private static string FontToCssFontFamily(Font font)
		{
			if (font?.FontFamily == null)
				return null;

			var name = font.FontFamily.Name;
			if (string.IsNullOrWhiteSpace(name))
				return null;

			name = name.Replace("\"", "\\\"");
			return name.Contains(" ") ? $"\"{name}\", sans-serif" : $"{name}, sans-serif";
		}
	}
}
