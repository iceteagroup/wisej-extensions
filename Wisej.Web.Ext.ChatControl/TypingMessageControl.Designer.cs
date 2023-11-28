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
			this.label1 = new Wisej.Web.Label();
			this.loadingIndicator1 = new Wisej.Web.Ext.ChatControl.LoadingIndicator();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("default", 13F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
			this.label1.ForeColor = System.Drawing.Color.FromName("@activeCaptionText");
			this.label1.Location = new System.Drawing.Point(16, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(51, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Typing...";
			// 
			// loadingIndicator1
			// 
			this.loadingIndicator1.Location = new System.Drawing.Point(76, 11);
			this.loadingIndicator1.Name = "loadingIndicator1";
			this.loadingIndicator1.Size = new System.Drawing.Size(47, 27);
			this.loadingIndicator1.TabIndex = 1;
			// 
			// TypingMessageControl
			// 
			this.Controls.Add(this.loadingIndicator1);
			this.Controls.Add(this.label1);
			this.Name = "TypingMessageControl";
			this.Size = new System.Drawing.Size(133, 48);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private Label label1;
		private LoadingIndicator loadingIndicator1;
	}
}
