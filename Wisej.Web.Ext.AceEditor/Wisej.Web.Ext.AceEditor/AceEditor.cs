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

using System.Collections.Generic;
using System.ComponentModel;
using Wisej.Design;

namespace Wisej.Web.Ext.AceEditor
{
	/// <summary>
	/// Integrates the AceEditor (<see href="https://ace.c9.io/">) widget.
	/// </summary>
	/// <remarks>
	/// Ace is an embeddable code editor written in JavaScript. 
	/// It matches the features and performance of native editors such as Sublime, Vim and TextMate. 
	/// </remarks>
	public class AceEditor : Widget
    {
		private const string ACE_MODE = "ace/mode/";
		private const string ACE_THEME = "ace/theme/";

		public AceEditor()
		{
			this.ShowGutter = true;
			this.ShowLineNumbers = true;
			this.ShowPrintMargin = true;
			this.DisplayIndentGuides = true;
			this.PrintMarginColumn = 80;
		}

		#region Properties

		/// <summary>
		/// Returns or sets the options to use for this instance of the AceEditor.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override dynamic Options
		{
			get { return base.Options; }
			set { base.Options = value; }
		}

		/// <summary>
		/// Overridden to return our list of script resources.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override List<Package> Packages
		{
			get
			{
				if (base.Packages.Count == 0)
				{
					// initialize the loader with the required libraries.
					base.Packages.Add(new Package()
					{
						Name = "ace.js",
						Source = $"resource.wx/Wisej.Web.Ext.AceEditor/Wisej.Web.Ext.AceEditor.JavaScript.src.ace.js"
					});
				}

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
			get { return this.GetResourceString("Wisej.Web.Ext.AceEditor.JavaScript.startup.js"); }
			set { }
		}

		/// <summary>
		/// Returns or sets the theme.
		/// </summary>
		[DesignerActionList]
		[DefaultValue("")]
		public string Theme
		{
			get
			{
				var theme = this.Options.theme ?? "";
				if (theme.Length >= ACE_THEME.Length)
					theme = theme.Substring(ACE_THEME.Length);

				return theme;
			}
			set { this.Options.theme = $"{ACE_THEME}{value}"; }
		}

		/// <summary>
		/// Returns or sets the language.
		/// </summary>
		[DesignerActionList]
		[DefaultValue("")]
		public string Language
		{
			get
			{
				var theme = this.Options.mode ?? "";
				if (theme.Length >= ACE_MODE.Length)
					theme = theme.Substring(ACE_MODE.Length);

				return theme;
			}
			set { this.Options.mode = $"{ACE_MODE}{value}"; }
		}

		/// <summary>
		/// If readOnly is true, then the editor is set to read-only mode, and none of the content can change.
		/// </summary>
		[DefaultValue(false)]
		public bool ReadOnly
		{
			get { return this.Options.readOnly ?? false; }
			set { this.Options.readOnly = value; }
		}

		/// <summary>
		/// Shows or hides the line numbers in the gutter.
		/// </summary>
		[DefaultValue(true)]
		public bool ShowLineNumbers
		{
			get { return this.Options.showLineNumbers ?? true; }
			set { this.Options.showLineNumbers = value; }
		}

		/// <summary>
		/// Shows or hides the gutter.
		/// </summary>
		[DefaultValue(true)]
		public bool ShowGutter
		{
			get { return this.Options.showGutter ?? true; }
			set { this.Options.showGutter = value; }
		}

		/// <summary>
		/// Shows or hides the indent guides.
		/// </summary>
		[DefaultValue(true)]
		public bool DisplayIndentGuides
		{
			get { return this.Options.displayIndentGuides ?? true; }
			set { this.Options.displayIndentGuides = value; }
		}

		/// <summary>
		/// Shows or hides the print margin.
		/// </summary>
		[DefaultValue(true)]
		public bool ShowPrintMargin
		{
			get { return this.Options.showPrintMargin ?? true; }
			set { this.Options.showPrintMargin = value; }
		}
		/// <summary>
		/// Returns or sets the print margin column position.
		/// </summary>
		[DefaultValue(80)]
		public int PrintMarginColumn
		{
			get { return this.Options.printMarginColumn; }
			set { this.Options.printMarginColumn = value; }
		}

		/// <summary>
		/// Returns or sets the text displayed in the editor.
		/// </summary>
		public override string Text
		{
			get { return base.Text; }
			set
			{
				if (base.Text != value)
				{
					base.Text = value;
					this.Instance.setValue(value);
				}
			}
		}

		/// <summary>
		/// If readOnly is true, then the editor is set to read-only mode, and none of the content can change.
		/// </summary>
		[DefaultValue(null)]
		public float? FontSize
		{
			get { return this.Options.fontSize; }
			set { this.Options.fontSize = value; }
		}

		#endregion

		#region Event Handlers

		protected override void OnWidgetEvent(WidgetEventArgs e)
		{
			switch (e.Type)
			{
				case "blur":
					ProcessWidgetBlurEvent(e);
					break;

			}

			base.OnWidgetEvent(e);
		}

		private void ProcessWidgetBlurEvent(WidgetEventArgs e)
		{
			var text = e.Data ?? "";
			base.Text = text;
		}

		#endregion

		#region Methods

		/// <summary>
		/// Perform an undo operation on the document, reverting the last change.
		/// </summary>
		public void Undo()
		{
			this.Instance.undo();
		}

		/// <summary>
		/// Perform a redo operation on the document, reimplementing the last change.
		/// </summary>
		public void Redo()
		{
			this.Instance.redo();
		}

		#endregion
	}
}
