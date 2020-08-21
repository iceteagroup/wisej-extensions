namespace Wisej.Web.Ext.Camera
{
	public partial class Camera
	{
		/// <summary>
		/// The facingMode property is a value indicating the direction in which the camera 
		/// producing the video track is currently facing.
		/// </summary>
		/// <remarks>See: https://developer.mozilla.org/en-US/docs/Web/API/MediaTrackSettings/facingMode </remarks>
		public enum VideoFacingMode
		{
			/// <summary>
			/// The video source is facing toward the user; this includes, 
			/// for example, the front-facing camera on a smartphone.
			/// </summary>
			User,

			/// <summary>
			/// The video source is facing away from the user, thereby viewing their environment. 
			/// This is the back camera on a smartphone.
			/// </summary>
			Environment,

			/// <summary>
			/// The video source is facing toward the user but to their left, 
			/// such as a camera aimed toward the user but over their left shoulder.
			/// </summary>
			Left,

			/// <summary>
			/// The video source is facing toward the user but to their right, 
			/// such as a camera aimed toward the user but over their right shoulder.
			/// </summary>
			Right
		}
	}
}
