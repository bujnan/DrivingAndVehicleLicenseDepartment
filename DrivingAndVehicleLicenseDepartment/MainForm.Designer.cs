namespace DrivingAndVehicleLicenseDepartment
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.msPeople = new System.Windows.Forms.MenuStrip();
            this.peopleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.usersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pbMainFormBackgound = new System.Windows.Forms.PictureBox();
            this.msPeople.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainFormBackgound)).BeginInit();
            this.SuspendLayout();
            // 
            // msPeople
            // 
            this.msPeople.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.msPeople.ImageScalingSize = new System.Drawing.Size(60, 60);
            this.msPeople.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.peopleToolStripMenuItem,
            this.usersToolStripMenuItem});
            this.msPeople.Location = new System.Drawing.Point(0, 0);
            this.msPeople.Name = "msPeople";
            this.msPeople.Size = new System.Drawing.Size(1112, 68);
            this.msPeople.TabIndex = 1;
            this.msPeople.Text = "menuStrip1";
            // 
            // peopleToolStripMenuItem
            // 
            this.peopleToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.peopleToolStripMenuItem.Image = global::DrivingAndVehicleLicenseDepartment.Properties.Resources.People_64;
            this.peopleToolStripMenuItem.Name = "peopleToolStripMenuItem";
            this.peopleToolStripMenuItem.Size = new System.Drawing.Size(142, 64);
            this.peopleToolStripMenuItem.Text = "People ";
            this.peopleToolStripMenuItem.Click += new System.EventHandler(this.peopleToolStripMenuItem_Click);
            // 
            // usersToolStripMenuItem
            // 
            this.usersToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.usersToolStripMenuItem.Image = global::DrivingAndVehicleLicenseDepartment.Properties.Resources.Users_2_64;
            this.usersToolStripMenuItem.Name = "usersToolStripMenuItem";
            this.usersToolStripMenuItem.Size = new System.Drawing.Size(131, 64);
            this.usersToolStripMenuItem.Text = "Users ";
            this.usersToolStripMenuItem.Click += new System.EventHandler(this.usersToolStripMenuItem_Click);
            // 
            // pbMainFormBackgound
            // 
            this.pbMainFormBackgound.BackColor = System.Drawing.Color.Black;
            this.pbMainFormBackgound.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbMainFormBackgound.Image = global::DrivingAndVehicleLicenseDepartment.Properties.Resources.Logo_Final;
            this.pbMainFormBackgound.Location = new System.Drawing.Point(0, 68);
            this.pbMainFormBackgound.Name = "pbMainFormBackgound";
            this.pbMainFormBackgound.Size = new System.Drawing.Size(1112, 576);
            this.pbMainFormBackgound.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbMainFormBackgound.TabIndex = 0;
            this.pbMainFormBackgound.TabStop = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 644);
            this.Controls.Add(this.pbMainFormBackgound);
            this.Controls.Add(this.msPeople);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msPeople;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Driving And Vehicle License Department";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.msPeople.ResumeLayout(false);
            this.msPeople.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbMainFormBackgound)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pbMainFormBackgound;
        private System.Windows.Forms.MenuStrip msPeople;
        private System.Windows.Forms.ToolStripMenuItem peopleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem usersToolStripMenuItem;
    }
}

