namespace Wisej.Web.Ext.ChatControl
{
	partial class TypingMessageControl
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
			this.loadingIndicator1 = new Wisej.Web.Ext.ChatControl.LoadingIndicator();
			this.SuspendLayout();
			// 
			// loadingIndicator1
			// 
			this.loadingIndicator1.Location = new System.Drawing.Point(16, 11);
			this.loadingIndicator1.Name = "loadingIndicator1";
			this.loadingIndicator1.Size = new System.Drawing.Size(47, 27);
			this.loadingIndicator1.TabIndex = 1;
			// 
			// TypingMessageControl
			// 
			this.Controls.Add(this.loadingIndicator1);
			this.Name = "TypingMessageControl";
			this.Size = new System.Drawing.Size(77, 48);
			this.ResumeLayout(false);

		}

		#endregion
		private LoadingIndicator loadingIndicator1;
	}
}
