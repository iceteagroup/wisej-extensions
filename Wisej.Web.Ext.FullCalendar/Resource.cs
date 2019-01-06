///////////////////////////////////////////////////////////////////////////////
//
// (C) 2018 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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
using System.Drawing;
using Wisej.Core;

namespace Wisej.Web.Ext.FullCalendar
{
	/// <summary>
	/// Represents a resource in the <see cref="T:Wisej.Web.Ext.FullCalendar.FullCalendar"/> control
	/// when using the scheduler commercial plug in.
	/// </summary>
	public class Resource
	{

		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.Resource"/>.
		/// </summary>
		public Resource()
		{
		}

		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.Resource"/>.
		/// </summary>
		/// <param name="id">A string that represents the ID of this resource.</param>
		public Resource(string id)
		{
			if (String.IsNullOrEmpty(id))
				throw new ArgumentNullException("id");

			this._id = id;
		}

		/// <summary>
		/// Returns the <see cref="FullCalendar"/> that owns this resource.
		/// </summary>
		public FullCalendar Owner
		{
			get { return this._owner; }
			internal set { this._owner = value; }
		}
		private FullCalendar _owner;

		/// <summary>
		/// Returns or sets the unique ID for this resource. It's used
		/// by <see cref="Event.ResourceId"/> to associate the event to the resource.
		/// </summary>
		[DefaultValue(null)]
		[Description("Unique ID for this resource.")]
		public string Id
		{
			get { return this._id; }
			set
			{
				value = value == "" ? null : value;
				if (this._id != value)
				{
					this._id = value;
				}
			}
		}
		private string _id = null;

		/// <summary>
		/// Returns or sets the title of this resource.
		/// </summary>
		[DefaultValue("")]
		[Description("Title of this resource.")]
		public string Title
		{
			get { return this._title; }
			set
			{
				value = value == "" ? null : value;
				if (this._title != value)
				{
					this._title = value;
				}
			}
		}
		private string _title = "";

		/// <summary>
		/// Sets the background color for the events associated to this resource.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[Description("Background color for the events associated to this resource.")]
		public Color EventBackgroundColor
		{
			get { return this._backgroundColor; }
			set
			{
				if (this._backgroundColor != value)
				{
					this._backgroundColor = value;
					OnResourceChanged();
				}
			}
		}
		private Color _backgroundColor = Color.Empty;

		/// <summary>
		/// Sets the border color for the events associated to this resource.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[Description("Border color for the events associated to this resource.")]
		public Color EventBorderColor
		{
			get { return this._borderColor; }
			set
			{
				if (this._borderColor != value)
				{
					this._borderColor = value;
					OnResourceChanged();
				}
			}
		}
		private Color _borderColor = Color.Empty;

		/// <summary>
		/// Sets the text color for the events associated to this resource.
		/// </summary>
		[DefaultValue(typeof(Color), "")]
		[Description("Text color for the events associated to this resource.")]
		public Color EventTextColor
		{
			get { return this._textColor; }
			set
			{
				if (this._textColor != value)
				{
					this._textColor = value;
					OnResourceChanged();
				}
			}
		}
		private Color _textColor = Color.Empty;

		/// <summary>
		/// A CSS class (or array of classes) that will be attached to this event's element when
		/// it is associated to this resource.
		/// </summary>
		[DefaultValue("")]
		[Description("A CSS class name assigned to the events associated to this resource.")]
		public string EventClassName
		{
			get { return this._className; }
			set
			{
				if (this._className != value)
				{
					this._className = value;
					OnResourceChanged();
				}
			}
		}
		private string _className = string.Empty;

		/// <summary>
		/// Child resources.
		/// </summary>
		[DefaultValue(null)]
		[Description("Child resources.")]
		public Resource[] Children
		{
			get { return this._children; }
			set
			{
				if (this._children != value)
				{
					this._children = value;
					OnResourceChanged();
				}
			}
		}
		private Resource[] _children;

		private void OnResourceChanged()
		{
			this._owner?.OnResourceChanged(this);
		}

		/// <summary>
		/// Returns a dynamic object that can be used to store custom data.
		/// </summary>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public dynamic UserData
		{
			get { return _userData = _userData ?? new DynamicObject(); }
		}
		private dynamic _userData = null;

		/// <summary>
		/// Returns a string represenation of this object.
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return String.Concat(
				this.Id,
				": ",
				this.Title);
		}

	}
}
