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
	/// Xterm is a lightweight, free, and open source terminal viewer for the web.
	/// Built using HTML5, CSS3, JavaScript, and WebGL. See https://xtermjs.org/.
	/// </summary>
	[ToolboxBitmapAttribute(typeof(XTerm))]
	[Description("XTerm is a lightweight, free, and open source terminal for the web.")]
	public class XTerm : Widget
	{

		#region Properties

		/// <summary>
		/// Overridden to create our initialization script.
		/// </summary>
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
            script = script.Replace("$options", options.ToString());
            return script;
		}

        public bool DebugScript { get; set; }

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
