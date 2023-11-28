namespace Wisej.Web.Ext.ChatControl
{
	partial class LoadingIndicator
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
			this.shape1 = new Wisej.Web.Shape();
			this.shape2 = new Wisej.Web.Shape();
			this.shape3 = new Wisej.Web.Shape();
			this.animation1 = new Wisej.Web.Animation(this.components);
			this.animation2 = new Wisej.Web.Animation(this.components);
			this.SuspendLayout();
			// 
			// shape1
			// 
			this.shape1.Anchor = Wisej.Web.AnchorStyles.None;
			this.shape1.BackColor = System.Drawing.Color.FromArgb(244, 244, 244);
			this.shape1.BorderStyle.Radius = 25;
			this.shape1.BorderStyleBottom.Radius = 25;
			this.shape1.BorderStyleLeft.Radius = 25;
			this.shape1.BorderStyleRight.Radius = 25;
			this.shape1.BorderStyleTop.Radius = 25;
			this.shape1.Location = new System.Drawing.Point(7, 9);
			this.shape1.Name = "shape1";
			this.shape1.Size = new System.Drawing.Size(8, 8);
			// 
			// shape2
			// 
			this.shape2.Anchor = Wisej.Web.AnchorStyles.None;
			this.shape2.BackColor = System.Drawing.Color.FromArgb(244, 244, 244);
			this.shape2.BorderStyle.Radius = 25;
			this.shape2.BorderStyleBottom.Radius = 25;
			this.shape2.BorderStyleLeft.Radius = 25;
			this.shape2.BorderStyleRight.Radius = 25;
			this.shape2.BorderStyleTop.Radius = 25;
			this.shape2.Location = new System.Drawing.Point(22, 9);
			this.shape2.Name = "shape2";
			this.shape2.Size = new System.Drawing.Size(8, 8);
			// 
			// shape3
			// 
			this.shape3.Anchor = Wisej.Web.AnchorStyles.None;
			this.shape3.BackColor = System.Drawing.Color.FromArgb(244, 244, 244);
			this.shape3.BorderStyle.Radius = 25;
			this.shape3.BorderStyleBottom.Radius = 25;
			this.shape3.BorderStyleLeft.Radius = 25;
			this.shape3.BorderStyleRight.Radius = 25;
			this.shape3.BorderStyleTop.Radius = 25;
			this.shape3.Location = new System.Drawing.Point(37, 9);
			this.shape3.Name = "shape3";
			this.shape3.Size = new System.Drawing.Size(8, 8);
			// 
			// LoadingIndicator
			// 
			this.animation1.GetAnimation(this).Duration = 1000;
			this.animation1.GetAnimation(this).Event = "appear";
			this.animation1.GetAnimation(this).Name = "fadeIn";
			this.animation1.GetAnimation(this).Repeat = 10000;
			this.animation2.GetAnimation(this).Delay = 1000;
			this.animation2.GetAnimation(this).Duration = 1000;
			this.animation2.GetAnimation(this).Event = "appear";
			this.animation2.GetAnimation(this).Name = "fadeOut";
			this.animation2.GetAnimation(this).Repeat = 10000;
			this.Controls.Add(this.shape3);
			this.Controls.Add(this.shape2);
			this.Controls.Add(this.shape1);
			this.Name = "LoadingIndicator";
			this.Size = new System.Drawing.Size(53, 27);
			this.ResumeLayout(false);

		}

		#endregion

		private Shape shape1;
		private Shape shape2;
		private Shape shape3;
		private Animation animation1;
		private Animation animation2;
	}
}
