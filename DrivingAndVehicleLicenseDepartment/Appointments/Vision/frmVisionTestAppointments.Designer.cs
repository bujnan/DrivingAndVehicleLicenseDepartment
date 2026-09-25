namespace DrivingAndVehicleLicenseDepartment.Appointments.Vision
{
    partial class frmVisionTestAppointments
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.ucDrivingLicenseInfo1 = new DrivingAndVehicleLicenseDepartment.Applications.Controls.ucDrivingLicenseInfo();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvAppointments = new System.Windows.Forms.DataGridView();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRecord = new System.Windows.Forms.Label();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAddNewAppointment = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DrivingAndVehicleLicenseDepartment.Properties.Resources.vision_test_512;
            this.pictureBox1.Location = new System.Drawing.Point(421, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 112);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label1.Location = new System.Drawing.Point(273, 127);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(428, 50);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vision Test Appointments";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucDrivingLicenseInfo1
            // 
            this.ucDrivingLicenseInfo1.Location = new System.Drawing.Point(26, 180);
            this.ucDrivingLicenseInfo1.Name = "ucDrivingLicenseInfo1";
            this.ucDrivingLicenseInfo1.Size = new System.Drawing.Size(923, 369);
            this.ucDrivingLicenseInfo1.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(29, 573);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 18);
            this.label2.TabIndex = 3;
            this.label2.Text = "Appointments:";
            // 
            // dgvAppointments
            // 
            this.dgvAppointments.AllowUserToAddRows = false;
            this.dgvAppointments.AllowUserToDeleteRows = false;
            this.dgvAppointments.AllowUserToOrderColumns = true;
            this.dgvAppointments.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAppointments.BackgroundColor = System.Drawing.Color.White;
            this.dgvAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAppointments.Location = new System.Drawing.Point(29, 605);
            this.dgvAppointments.Name = "dgvAppointments";
            this.dgvAppointments.ReadOnly = true;
            this.dgvAppointments.RowHeadersVisible = false;
            this.dgvAppointments.RowHeadersWidth = 51;
            this.dgvAppointments.RowTemplate.Height = 24;
            this.dgvAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAppointments.Size = new System.Drawing.Size(916, 141);
            this.dgvAppointments.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(29, 774);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 18);
            this.label3.TabIndex = 3;
            this.label3.Text = "# Records:";
            // 
            // lblRecord
            // 
            this.lblRecord.AutoSize = true;
            this.lblRecord.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRecord.Location = new System.Drawing.Point(118, 774);
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.Size = new System.Drawing.Size(35, 18);
            this.lblRecord.TabIndex = 3;
            this.lblRecord.Text = "???";
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DrivingAndVehicleLicenseDepartment.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(859, 764);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(86, 39);
            this.btnClose.TabIndex = 5;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAddNewAppointment
            // 
            this.btnAddNewAppointment.BackColor = System.Drawing.Color.White;
            this.btnAddNewAppointment.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.btnAddNewAppointment.FlatAppearance.BorderSize = 0;
            this.btnAddNewAppointment.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewAppointment.Image = global::DrivingAndVehicleLicenseDepartment.Properties.Resources.Calendar_32;
            this.btnAddNewAppointment.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnAddNewAppointment.Location = new System.Drawing.Point(904, 552);
            this.btnAddNewAppointment.Name = "btnAddNewAppointment";
            this.btnAddNewAppointment.Size = new System.Drawing.Size(41, 39);
            this.btnAddNewAppointment.TabIndex = 5;
            this.btnAddNewAppointment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddNewAppointment.UseVisualStyleBackColor = false;
            // 
            // frmVisionTestAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(967, 815);
            this.Controls.Add(this.btnAddNewAppointment);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.dgvAppointments);
            this.Controls.Add(this.lblRecord);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ucDrivingLicenseInfo1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MinimizeBox = false;
            this.Name = "frmVisionTestAppointments";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vision Test Appointments";
            this.Load += new System.EventHandler(this.frmVisionTestAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAppointments)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private Applications.Controls.ucDrivingLicenseInfo ucDrivingLicenseInfo1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvAppointments;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRecord;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnAddNewAppointment;
    }
}