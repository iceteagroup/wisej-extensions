namespace Wisej.Web.Ext.Camera
{
    public partial class Camera
    {
        /// <summary>
        /// The CSS object-fit property is used to specify how the video should be resized to fit its container.
        /// </summary>
        /// <remarks>See: https://www.w3schools.com/css/css3_object-fit.asp  </remarks>
        public enum ObjectFitMode
        {
            /// <summary>
            /// This is the default value. The replaced content is sized to fill the element's content box. If necessary, the object will be stretched or squished to fit
            /// </summary>
            Fill,
            /// <summary>
            /// The replaced content is scaled to maintain its aspect ratio while fitting within the element's content box
            /// </summary>
            Contain,
            /// <summary>
            /// The replaced content is sized to maintain its aspect ratio while filling the element's entire content box. The object will be clipped to fit
            /// </summary>
            Cover,
            /// <summary>
            /// The content is sized as if none or contain were specified (would result in a smaller concrete object size)
            /// </summary>
            ScaleDown,
            /// <summary>
            /// The replaced content is not resized
            /// </summary>
            None
        }
    }
}

