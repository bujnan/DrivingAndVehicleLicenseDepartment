namespace DrivingAndVehicleLicenseDepartment.License.Control
{
    partial class ucLicenseInfoWithFilter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.txbLicenseId = new System.Windows.Forms.TextBox();
            this.gbFilter = new System.Windows.Forms.GroupBox();
            this.ucLicenseInfo1 = new DrivingAndVehicleLicenseDepartment.License.Control.ucLicenseInfo();
            this.btnFindLicense = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.gbFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "License Id:";
            // 
            // txbLicenseId
            // 
            this.txbLicenseId.Location = new System.Drawing.Point(153, 33);
            this.txbLicenseId.Name = "txbLicenseId";
            this.txbLicenseId.Size = new System.Drawing.Size(197, 26);
            this.txbLicenseId.TabIndex = 1;
            this.txbLicenseId.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txbLicenseId_KeyPress);
            this.txbLicenseId.Validating += new System.ComponentModel.CancelEventHandler(this.txbLicenseId_Validating);
            // 
            // gbFilter
            // 
            this.gbFilter.Controls.Add(this.label1);
            this.gbFilter.Controls.Add(this.btnFindLicense);
            this.gbFilter.Controls.Add(this.txbLicenseId);
            this.gbFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.Location = new System.Drawing.Point(261, 3);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(414, 72);
            this.gbFilter.TabIndex = 3;
            this.gbFilter.TabStop = false;
            this.gbFilter.Text = "Filter:";
            // 
            // ucLicenseInfo1
            // 
            this.ucLicenseInfo1.Location = new System.Drawing.Point(1, 81);
            this.ucLicenseInfo1.Name = "ucLicenseInfo1";
            this.ucLicenseInfo1.Size = new System.Drawing.Size(934, 298);
            this.ucLicenseInfo1.TabIndex = 4;
            // 
            // btnFindLicense
            // 
            this.btnFindLicense.BackColor = System.Drawing.Color.White;
            this.btnFindLicense.Image = global::DrivingAndVehicleLicenseDepartment.Properties.Resources.search_person;
            this.btnFindLicense.Location = new System.Drawing.Point(362, 28);
            this.btnFindLicense.Name = "btnFindLicense";
            this.btnFindLicense.Size = new System.Drawing.Size(34, 32);
            this.btnFindLicense.TabIndex = 2;
            this.btnFindLicense.UseVisualStyleBackColor = false;
            this.btnFindLicense.Click += new System.EventHandler(this.btnFindLicense_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // ucLicenseInfoWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucLicenseInfo1);
            this.Controls.Add(this.gbFilter);
            this.Name = "ucLicenseInfoWithFilter";
            this.Size = new System.Drawing.Size(936, 375);
            this.gbFilter.ResumeLayout(false);
            this.gbFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txbLicenseId;
        private System.Windows.Forms.Button btnFindLicense;
        private System.Windows.Forms.GroupBox gbFilter;
        private ucLicenseInfo ucLicenseInfo1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
    }
}
