namespace DrivingAndVehicleLicenseDepartment.Tests
{
    partial class frmScheduleTest
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
            this.btnClose = new System.Windows.Forms.Button();
            this.ucScheduleTestType1 = new DrivingAndVehicleLicenseDepartment.Tests.ucScheduleTestType();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Image = global::DrivingAndVehicleLicenseDepartment.Properties.Resources.Close_32;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(322, 604);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(89, 39);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucScheduleTestType1
            // 
            this.ucScheduleTestType1.Location = new System.Drawing.Point(1, 1);
            this.ucScheduleTestType1.Name = "ucScheduleTestType1";
            this.ucScheduleTestType1.Size = new System.Drawing.Size(525, 656);
            this.ucScheduleTestType1.TabIndex = 0;
            this.ucScheduleTestType1.TestType = DVLD_Business.clsTest.enTestType.Vision;
            this.ucScheduleTestType1.Load += new System.EventHandler(this.ucScheduleTestType1_Load);
            // 
            // frmScheduleTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(529, 667);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.ucScheduleTestType1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmScheduleTest";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Schedule Test";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmScheduleTest_FormClosed);
            this.ResumeLayout(false);

        }

        #endregion

        private ucScheduleTestType ucScheduleTestType1;
        private System.Windows.Forms.Button btnClose;
    }
}