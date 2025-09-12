///////////////////////////////////////////////////////////////////////////////
//
// (C) 2024 ICE TEA GROUP LLC - ALL RIGHTS RESERVED
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

using System.Drawing;

namespace Wisej.Web.Ext.ChatControl
{
	/// <summary>
	/// Represents a user in the context of a <see cref="ChatBox"/>.
	/// </summary>
	public class User
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class.
		/// </summary>
		public User() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class with the specified ID, name, and image source.
		/// </summary>
		/// <param name="id">The unique identifier of the user.</param>
		/// <param name="name">The name of the user.</param>
		/// <param name="imageSource">The image source of the user.</param>
		public User(string id, string name, string? imageSource = null)
		{
			Id = id;
			Name = name;
			ImageSource = imageSource ?? "resource.wx/Wisej.Web.Ext.ChatControl/Images/person.svg";
		}

		#endregion

		/// <summary>
		/// Gets or sets the unique identifier of the user.
		/// </summary>
		public string Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the user.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the image source of the user.
		/// </summary>
		public string ImageSource { get; set; } = "resource.wx/Wisej.Web.Ext.ChatControl/Images/person.svg";

		/// <summary>
		/// Gets or sets the color of the bubble to display for this user.
		/// </summary>
		public Color? BubbleColor { get; set; }

		/// <summary>
		/// Determines whether the specified object is equal to the current object.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
		public override bool Equals(object obj) => obj is User other && Id == other.Id;

		/// <summary>
		/// Serves as the default hash function.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		public override int GetHashCode() => Id.GetHashCode();

		/// <summary>
		/// Determines whether two specified <see cref="User"/> objects have the same value.
		/// </summary>
		/// <param name="lhs">The first <see cref="User"/> to compare, or <see langword="null"/>.</param>
		/// <param name="rhs">The second <see cref="User"/> to compare, or <see langword="null"/>.</param>
		/// <returns><c>true</c> if the value of <paramref name="lhs"/> is the same as the value of <paramref name="rhs"/>; otherwise, <c>false</c>.</returns>
		public static bool operator ==(User lhs, User rhs) => Equals(lhs, rhs);

		/// <summary>
		/// Determines whether two specified <see cref="User"/> objects have different values.
		/// </summary>
		/// <param name="lhs">The first <see cref="User"/> to compare, or <see langword="null"/>.</param>
		/// <param name="rhs">The second <see cref="User"/> to compare, or <see langword="null"/>.</param>
		/// <returns><c>true</c> if the value of <paramref name="lhs"/> is different from the value of <paramref name="rhs"/>; otherwise, <c>false</c>.</returns>
		public static bool operator !=(User lhs, User rhs) => !Equals(lhs, rhs);
	}
}