///////////////////////////////////////////////////////////////////////////////
//
// (C) 2017 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Wisej.Core;

namespace Wisej.Web.Ext.XTerm
{
	/// <summary>
	/// Represents a terminal control based on xterm.js, a lightweight, free, and open source terminal for the web.
	/// See <see href="https://xtermjs.org/"/>.
	/// </summary>
	/// <remarks>
	/// The control only renders the terminal in the browser: it echoes the printable characters typed by the user
	/// and fires <see cref="OnLineFeed"/> with the collected line when the user presses Enter. It is up to the
	/// application to process the command and to write the output back using <see cref="Write"/>.
	/// </remarks>
	/// <example>
	/// A minimal command prompt:
	/// <code><![CDATA[
	/// private void Page1_Load(object sender, EventArgs e)
	/// {
	///     this.xTerm1.OnInit += (s, args) => this.xTerm1.Write("$ ");
	///     this.xTerm1.OnLineFeed += (s, command) =>
	///     {
	///         this.xTerm1.Write("\r\nYou typed: " + command + "\r\n$ ");
	///     };
	/// }
	/// ]]></code>
	/// </example>
	[ToolboxBitmapAttribute(typeof(XTerm))]
	[Description("XTerm is a lightweight, free, and open source terminal for the web.")]
    [ApiCategory("XTerm")]
    public class XTerm : Widget
	{

		#region Properties

		/// <summary>
		/// Overridden to create our initialization script.
		/// </summary>
		/// <remarks>
		/// The script is generated from the embedded startup.js resource and the value of
		/// <see cref="DebugScript"/>. Assigning a value to this property has no effect.
		/// </remarks>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string InitScript
		{
			get { return BuildInitScript(); }
			set { }
		}

        public event EventHandler<string> OnLineFeed;
        public event EventHandler OnInit;

        private StringBuilder sb = new StringBuilder();

        /// <summary>
        /// Overridden to return our list of script resources.
        /// </summary>
        /// <remarks>
        /// The list is populated on first access with xterm.js, xterm.css and the attach, fit, winptyCompat,
        /// search, fullscreen and webLinks add-ons, all loaded from the embedded resources of this assembly.
        /// </remarks>
        [Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override List<Package> Packages
		{
            // disable inlining or we lose the calling assembly in GetResourceString().
            [MethodImpl(MethodImplOptions.NoInlining)]
            get
            {
				if (base.Packages.Count == 0)
				{
					// initialize the loader with the required libraries.
					base.Packages.Add(new Package()
					{
						Name = "xterm.js",
						Source = GetResourceURL("Wisej.Web.Ext.XTerm.JavaScript.xterm.js")
					});
                    base.Packages.Add(new Package()
                    {
                        Name = "attach.js",
                        Source = GetResourceURL("Wisej.Web.Ext.XTerm.JavaScript.addons.attach.attach.js")
                    });
                    base.Packages.Add(new Package()
                    {
                        Name = "fit.js",
                        Source = GetResourceURL("Wisej.Web.Ext.XTerm.JavaScript.addons.fit.fit.js")
                    });
                    base.Packages.Add(new Package()
                    {
                        Name = "winptyCompat.js",
                        Source = GetResourceURL("Wisej.Web.Ext.XTerm.JavaScript.addons.winptyCompat.winptyCompat.js")
                    });
                    base.Packages.Add(new Package()
                    {
                        Name = "search.js",
                        Source = GetResourceURL("Wisej.Web.Ext.XTerm.JavaScript.addons.search.search.js")
                    });
                    base.Packages.Add(new Package()
                    {
                        Name = "fullscreen.js",
                        Source = GetResourceURL("Wisej.Web.Ext.XTerm.JavaScript.addons.fullscreen.fullscreen.js")
                    });
                    base.Packages.Add(new Package()
                    {
                        Name = "fullscreen.css",
                        Source = GetResourceURL("Wisej.Web.Ext.XTerm.JavaScript.addons.fullscreen.fullscreen.css")
                    });
                    base.Packages.Add(new Package()
                    {
                        Name = "weblinks.js",
                        Source = GetResourceURL("Wisej.Web.Ext.XTerm.JavaScript.addons.weblinks.weblinks.js")
                    });
                    base.Packages.Add(new Package()
					{
						Name = "xterm.css",
						Source = GetResourceURL("Wisej.Web.Ext.XTerm.JavaScript.xterm.css")
					});
				}

				return base.Packages;
			}
		}

        #endregion

        #region Wisej Implementation

        // disable inlining or we lose the calling assembly in GetResourceString().
        [MethodImpl(MethodImplOptions.NoInlining)]
        private string BuildInitScript()
		{
			IWisejControl me = this;
			dynamic options = new DynamicObject();
			string script = GetResourceString("Wisej.Web.Ext.XTerm.JavaScript.startup.js");
            options.DebugScript = DebugScript;
            script = script.Replace("$options", options.ToJSON(WisejSerializerOptions.CamelCase));
            return script;
		}

        /// <summary>
        /// Returns or sets a value indicating whether the xterm.js debug option is enabled.
        /// </summary>
        /// <remarks>
        /// The value is passed to the terminal through <see cref="InitScript"/> when the widget is initialized.
        /// When enabled, xterm.js logs diagnostic information to the browser console.
        /// </remarks>
        public bool DebugScript { get; set; }

        /// <summary>
        /// Writes the text to the terminal.
        /// </summary>
        /// <param name="message">The text to write. It may contain ANSI/VT100 escape sequences.</param>
        /// <remarks>
        /// The text is sent to the browser asynchronously and written as-is: to start a new line use "\r\n",
        /// since "\n" alone only moves the cursor down. The written text becomes part of the prompt and cannot be
        /// deleted by the user with the Backspace key.
        /// </remarks>
        /// <example>
        /// Writing colored output followed by a new prompt:
        /// <code><![CDATA[
        /// this.xTerm1.Write("\u001b[32mBuild succeeded\u001b[0m\r\n");
        /// this.xTerm1.Write("$ ");
        /// ]]></code>
        /// </example>
        public void Write(string message)
        {
            Call("termWrite", message);
        }

        protected override void OnWidgetEvent(WidgetEventArgs e)
        {
            switch(e.Type)
            {
                case "key":
                    lock (this)
                    {
                        if (e.Data == "\r")
                        {
                            string cmd = sb.ToString();
                            sb.Clear();
                            OnLineFeed?.Invoke(this, cmd);
                        }
                        else if (e.Data == "\u007f")
                        {
                            if (sb.Length > 0)
                            {
                                sb.Remove(sb.Length - 1, 1);
                            }
                        }
                        else
                        {
                            sb.Append(e.Data);
                        }
                    }
                    break;
                case "init":
                    {
                        OnInit?.Invoke(this, EventArgs.Empty);
                    }
                    break;
            }

            base.OnWidgetEvent(e);
        }

        #endregion
    }
}
