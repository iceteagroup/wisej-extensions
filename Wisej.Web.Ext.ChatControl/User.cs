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
	/// <remarks>
	/// Users are compared by <see cref="Id"/>: two <see cref="User"/> instances with the same <see cref="Id"/>
	/// are considered equal. The <see cref="ChatBox"/> aligns messages from its own <see cref="ChatBox.User"/>
	/// to the right and messages from other users to the left.
	/// </remarks>
	/// <example>
	/// <code><![CDATA[
	/// chatBox.User = new User("1", "Alice");
	/// var bot = new User("bot", "Assistant", "Images/bot.png")
	/// {
	///     BubbleColor = Color.LightGray
	/// };
	/// chatBox.DataSource.Add(new Message("Hi Alice!", null, bot));
	/// ]]></code>
	/// </example>
	public class User
	{
		#region Constructors

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class.
		/// </summary>
		/// <example>
		/// <code><![CDATA[
		/// var user = new User { Id = "42", Name = "Alice" };
		/// chatBox.User = user;
		/// ]]></code>
		/// </example>
		public User() { }

		/// <summary>
		/// Initializes a new instance of the <see cref="User"/> class with the specified ID, name, and image source.
		/// </summary>
		/// <param name="id">The unique identifier of the user.</param>
		/// <param name="name">The name of the user.</param>
		/// <param name="imageSource">The image source of the user's avatar. When <c>null</c>, the default
		/// <c>resource.wx/Wisej.Web.Ext.ChatControl/Images/person.svg</c> image is used.</param>
		/// <example>
		/// <code><![CDATA[
		/// var alice = new User("1", "Alice");
		/// var bob = new User("2", "Bob", "Images/bob.png");
		/// ]]></code>
		/// </example>
		public User(string id, string name, string? imageSource = null)
		{
			Id = id;
			Name = name;
			ImageSource = imageSource ?? "resource.wx/Wisej.Web.Ext.ChatControl/Images/person.svg";
		}

		#endregion

		/// <summary>
		/// Returns or sets the unique identifier of the user.
		/// </summary>
		/// <value>The user's identifier; the default is <c>null</c>.</value>
		/// <remarks>
		/// The identifier is used for equality (<see cref="Equals(object)"/>, <c>==</c>, <c>!=</c>), to align messages
		/// and to select the default bubble color. Always assign a value: <see cref="GetHashCode"/> throws when it is <c>null</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var user = new User();
		/// user.Id = Guid.NewGuid().ToString();
		/// ]]></code>
		/// </example>
		public string Id { get; set; }

		/// <summary>
		/// Returns or sets the name of the user.
		/// </summary>
		/// <value>The display name shown above the user's messages; the default is <c>null</c>.</value>
		/// <example>
		/// <code><![CDATA[
		/// var user = new User("1", "Alice");
		/// user.Name = "Alice Smith";
		/// ]]></code>
		/// </example>
		public string Name { get; set; }

		/// <summary>
		/// Returns or sets the image source of the user's avatar.
		/// </summary>
		/// <value>An image source (URL, resource or theme image); the default is
		/// <c>resource.wx/Wisej.Web.Ext.ChatControl/Images/person.svg</c>.</value>
		/// <remarks>
		/// The avatar is displayed next to the user's messages when <see cref="ChatBox.AvatarVisible"/> is <c>true</c>.
		/// </remarks>
		/// <example>
		/// <code><![CDATA[
		/// var user = new User("1", "Alice");
		/// user.ImageSource = "Images/alice.png";
		/// ]]></code>
		/// </example>
		public string ImageSource { get; set; } = "resource.wx/Wisej.Web.Ext.ChatControl/Images/person.svg";

		/// <summary>
		/// Returns or sets the color of the bubble to display for this user.
		/// </summary>
		/// <value>The bubble background color, or <c>null</c> (default) to use the theme colors:
		/// <c>@highlight</c> for the <see cref="ChatBox.User"/> and <c>@controlDark</c> for other users.</value>
		/// <example>
		/// <code><![CDATA[
		/// var bot = new User("bot", "Assistant");
		/// bot.BubbleColor = Color.LightSteelBlue;
		/// ]]></code>
		/// </example>
		public Color? BubbleColor { get; set; }

		/// <summary>
		/// Determines whether the specified object is a <see cref="User"/> with the same <see cref="Id"/> as the current user.
		/// </summary>
		/// <param name="obj">The object to compare with the current object.</param>
		/// <returns><c>true</c> if the specified object is equal to the current object; otherwise, <c>false</c>.</returns>
		/// <example>
		/// <code><![CDATA[
		/// var a = new User("1", "Alice");
		/// var b = new User("1", "Alice (mobile)");
		/// bool same = a.Equals(b); // true, same Id.
		/// ]]></code>
		/// </example>
		public override bool Equals(object obj) => obj is User other && Id == other.Id;

		/// <summary>
		/// Returns a hash code for the current user, based on its <see cref="Id"/>.
		/// </summary>
		/// <returns>A hash code for the current object.</returns>
		/// <exception cref="System.NullReferenceException"><see cref="Id"/> is <c>null</c>.</exception>
		/// <example>
		/// <code><![CDATA[
		/// var users = new Dictionary<User, int>();
		/// users[new User("1", "Alice")] = 3;
		/// ]]></code>
		/// </example>
		public override int GetHashCode() => Id.GetHashCode();

		/// <summary>
		/// Determines whether two specified <see cref="User"/> objects have the same value (the same <see cref="Id"/>).
		/// </summary>
		/// <param name="lhs">The first <see cref="User"/> to compare, or <see langword="null"/>.</param>
		/// <param name="rhs">The second <see cref="User"/> to compare, or <see langword="null"/>.</param>
		/// <returns><c>true</c> if the value of <paramref name="lhs"/> is the same as the value of <paramref name="rhs"/>; otherwise, <c>false</c>.</returns>
		/// <example>
		/// <code><![CDATA[
		/// if (message.User == chatBox.User)
		///     message.BubbleVisible = true;
		/// ]]></code>
		/// </example>
		public static bool operator ==(User lhs, User rhs) => Equals(lhs, rhs);

		/// <summary>
		/// Determines whether two specified <see cref="User"/> objects have different values (different <see cref="Id"/>s).
		/// </summary>
		/// <param name="lhs">The first <see cref="User"/> to compare, or <see langword="null"/>.</param>
		/// <param name="rhs">The second <see cref="User"/> to compare, or <see langword="null"/>.</param>
		/// <returns><c>true</c> if the value of <paramref name="lhs"/> is different from the value of <paramref name="rhs"/>; otherwise, <c>false</c>.</returns>
		/// <example>
		/// <code><![CDATA[
		/// if (message.User != chatBox.User)
		///     AlertBox.Show("New message from " + message.User.Name);
		/// ]]></code>
		/// </example>
		public static bool operator !=(User lhs, User rhs) => !Equals(lhs, rhs);
	}
}