namespace Wisej.Web.Ext.ChatControl
{
	partial class MessageContainer
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Wisej Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.animation1 = new Wisej.Web.Animation(this.components);
			this.SuspendLayout();
			// 
			// MessageContainer
			// 
			this.animation1.GetAnimation(this).Duration = 150;
			this.animation1.GetAnimation(this).Event = "appear";
			this.animation1.GetAnimation(this).Name = "slideUpIn";
			this.BackColor = System.Drawing.Color.Transparent;
			this.Name = "MessageContainer";
			this.Padding = new Wisej.Web.Padding(16);
			this.Size = new System.Drawing.Size(445, 62);
			this.ResumeLayout(false);

		}

		#endregion
		private Animation animation1;
	}
}
