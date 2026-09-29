///////////////////////////////////////////////////////////////////////////////
//
// (C) 2015 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Runtime.CompilerServices;

namespace Wisej.Web.Ext.JssorSlider
{
	/// <summary>
	/// Represents an image slider control based on the Jssor Slider library (<see href="https://www.jssor.com"/>).
	/// </summary>
	/// <remarks>
	/// The navigation elements of the slider are configured using <see cref="ArrowOptions"/>, <see cref="BulletOptions"/>
	/// and <see cref="ThumbnailOptions"/>.
	/// </remarks>
	public class JssorSlider : Widget
	{
		#region Properties

		/// <summary>
		/// Returns the list of packages (JavaScript libraries) loaded on the client before the slider is created.
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
						Name = "coolclock",
						Source = GetResourceURL("Wisej.Web.Ext.CoolClock.JavaScript.coolclock.js")
					});
				}

				return base.Packages;
			}
		}


		#endregion
	}
}
