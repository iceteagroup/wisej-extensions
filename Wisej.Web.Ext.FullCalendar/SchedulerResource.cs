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
	/// <remarks>
	/// Assign the resources to <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.Resources"/> and associate the events
	/// to a resource using <see cref="P:Wisej.Web.Ext.FullCalendar.Event.ResourceId"/>.
	/// The resources are used only when <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.SchedulerLicenseKey"/> is set.
	/// </remarks>
	/// <example>
	/// Creating a hierarchy of resources:
	/// <code><![CDATA[
	/// this.fullCalendar1.SchedulerLicenseKey = "GPL-My-Project-Is-Open-Source";
	/// this.fullCalendar1.Resources = new[] {
	///     new SchedulerResource("building1")
	///     {
	///         Title = "Building 1",
	///         Children = new[] {
	///             new SchedulerResource("room101") { Title = "Room 101" },
	///             new SchedulerResource("room102") { Title = "Room 102" }
	///         }
	///     }
	/// };
	/// this.fullCalendar1.View = ViewType.TimelineWeek;
	/// ]]></code>
	/// </example>
	[ApiCategory("FullCalendar")]
	public partial class SchedulerResource
	{

		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.SchedulerResource"/>.
		/// </summary>
		public SchedulerResource()
		{
		}

		/// <summary>
		/// Constructs a new instance of <see cref="T:Wisej.Web.Ext.FullCalendar.SchedulerResource"/>.
		/// </summary>
		/// <param name="id">A string that represents the ID of this resource.</param>
		/// <exception cref="T:System.ArgumentNullException"><paramref name="id"/> is null or empty.</exception>
		public SchedulerResource(string id)
		{
			if (String.IsNullOrEmpty(id))
				throw new ArgumentNullException("id");

			this._id = id;
		}

		/// <summary>
		/// Returns the <see cref="FullCalendar"/> that owns this resource.
		/// </summary>
		/// <remarks>
		/// The owner is set when the resource is assigned to <see cref="P:Wisej.Web.Ext.FullCalendar.FullCalendar.Resources"/>.
		/// Resources in <see cref="Children"/> don't have an owner.
		/// </remarks>
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
		/// <remarks>
		/// An empty string is converted to null.
		/// </remarks>
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
		/// <remarks>
		/// The title is displayed in the resource area of the timeline views. An empty string is converted to null.
		/// </remarks>
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
		/// Returns or sets the background color for the events associated to this resource.
		/// </summary>
		/// <remarks>
		/// Changing the value fires <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.ResourceChanged"/> on the owner calendar
		/// but doesn't redraw it: call <c>Update()</c> on the calendar to show the change.
		/// </remarks>
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
		/// Returns or sets the border color for the events associated to this resource.
		/// </summary>
		/// <remarks>
		/// Changing the value fires <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.ResourceChanged"/> on the owner calendar
		/// but doesn't redraw it: call <c>Update()</c> on the calendar to show the change.
		/// </remarks>
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
		/// Returns or sets the text color for the events associated to this resource.
		/// </summary>
		/// <remarks>
		/// Changing the value fires <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.ResourceChanged"/> on the owner calendar
		/// but doesn't redraw it: call <c>Update()</c> on the calendar to show the change.
		/// </remarks>
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
		/// Returns or sets the CSS class name (or several names separated by spaces) that will be attached to the elements
		/// of the events associated to this resource.
		/// </summary>
		/// <remarks>
		/// Changing the value fires <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.ResourceChanged"/> on the owner calendar
		/// but doesn't redraw it: call <c>Update()</c> on the calendar to show the change.
		/// </remarks>
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
		/// Returns or sets the child resources.
		/// </summary>
		/// <remarks>
		/// Child resources are displayed nested under this resource in the timeline views.
		/// Changing the value fires <see cref="E:Wisej.Web.Ext.FullCalendar.FullCalendar.ResourceChanged"/> on the owner calendar
		/// but doesn't redraw it: call <c>Update()</c> on the calendar to show the change.
		/// </remarks>
		/// <example>
		/// Adding child resources:
		/// <code><![CDATA[
		/// var team = new SchedulerResource("team") { Title = "Support team" };
		/// team.Children = new[] {
		///     new SchedulerResource("alice") { Title = "Alice" },
		///     new SchedulerResource("bob") { Title = "Bob" }
		/// };
		/// this.fullCalendar1.Resources = new[] { team };
		/// ]]></code>
		/// </example>
		[DefaultValue(null)]
		[Description("Child resources.")]
		public SchedulerResource[] Children
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
		private SchedulerResource[] _children;

		private void OnResourceChanged()
		{
			this._owner?.OnResourceChanged(this);
		}

		/// <summary>
		/// Returns a dynamic object that can be used to store custom data.
		/// </summary>
		/// <remarks>
		/// The object is created the first time the property is read.
		/// </remarks>
		/// <example>
		/// Storing the employee number of the resource:
		/// <code><![CDATA[
		/// var resource = new SchedulerResource("alice") { Title = "Alice" };
		/// resource.UserData.EmployeeNumber = "E-1024";
		/// ]]></code>
		/// </example>
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public dynamic UserData
		{
			get { return _userData = _userData ?? new DynamicObject(); }
		}
		private dynamic _userData = null;

		/// <summary>
		/// Returns a string representation of this object.
		/// </summary>
		/// <returns>A string in the format "Id: Title".</returns>
		/// <example>
		/// Logging a resource:
		/// <code><![CDATA[
		/// var resource = new SchedulerResource("room1") { Title = "Room 1" };
		/// Console.WriteLine(resource.ToString()); // room1: Room 1
		/// ]]></code>
		/// </example>
		public override string ToString()
		{
			return String.Concat(
				this.Id,
				": ",
				this.Title);
		}

	}
}
