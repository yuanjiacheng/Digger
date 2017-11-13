namespace DiggerForm
{
    partial class DiggerData
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
            this.components = new System.ComponentModel.Container();
            this.Btn_ImportDiggerTemplate = new System.Windows.Forms.Button();
            this.Lab_TamplatePath = new System.Windows.Forms.Label();
            this.Lab_DiggerType = new System.Windows.Forms.Label();
            this.Lab_GetMoreDataType = new System.Windows.Forms.Label();
            this.Lab_DataUrl = new System.Windows.Forms.Label();
            this.Btn_BeginDig = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // Btn_ImportDiggerTemplate
            // 
            this.Btn_ImportDiggerTemplate.Location = new System.Drawing.Point(13, 13);
            this.Btn_ImportDiggerTemplate.Name = "Btn_ImportDiggerTemplate";
            this.Btn_ImportDiggerTemplate.Size = new System.Drawing.Size(99, 23);
            this.Btn_ImportDiggerTemplate.TabIndex = 0;
            this.Btn_ImportDiggerTemplate.Text = "导入挖掘模板";
            this.Btn_ImportDiggerTemplate.UseVisualStyleBackColor = true;
            this.Btn_ImportDiggerTemplate.Click += new System.EventHandler(this.Btn_ImportDiggerTemplate_Click);
            // 
            // Lab_TamplatePath
            // 
            this.Lab_TamplatePath.AutoSize = true;
            this.Lab_TamplatePath.Location = new System.Drawing.Point(118, 18);
            this.Lab_TamplatePath.Name = "Lab_TamplatePath";
            this.Lab_TamplatePath.Size = new System.Drawing.Size(0, 12);
            this.Lab_TamplatePath.TabIndex = 1;
            // 
            // Lab_DiggerType
            // 
            this.Lab_DiggerType.AutoSize = true;
            this.Lab_DiggerType.Location = new System.Drawing.Point(11, 49);
            this.Lab_DiggerType.Name = "Lab_DiggerType";
            this.Lab_DiggerType.Size = new System.Drawing.Size(65, 12);
            this.Lab_DiggerType.TabIndex = 2;
            this.Lab_DiggerType.Text = "挖掘类型：";
            // 
            // Lab_GetMoreDataType
            // 
            this.Lab_GetMoreDataType.AutoSize = true;
            this.Lab_GetMoreDataType.Location = new System.Drawing.Point(12, 74);
            this.Lab_GetMoreDataType.Name = "Lab_GetMoreDataType";
            this.Lab_GetMoreDataType.Size = new System.Drawing.Size(113, 12);
            this.Lab_GetMoreDataType.TabIndex = 3;
            this.Lab_GetMoreDataType.Text = "获得更多数据方式：";
            // 
            // Lab_DataUrl
            // 
            this.Lab_DataUrl.AutoSize = true;
            this.Lab_DataUrl.Location = new System.Drawing.Point(12, 97);
            this.Lab_DataUrl.Name = "Lab_DataUrl";
            this.Lab_DataUrl.Size = new System.Drawing.Size(107, 12);
            this.Lab_DataUrl.TabIndex = 4;
            this.Lab_DataUrl.Text = "获取Url数据地址：";
            // 
            // Btn_BeginDig
            // 
            this.Btn_BeginDig.Location = new System.Drawing.Point(13, 226);
            this.Btn_BeginDig.Name = "Btn_BeginDig";
            this.Btn_BeginDig.Size = new System.Drawing.Size(75, 23);
            this.Btn_BeginDig.TabIndex = 5;
            this.Btn_BeginDig.Text = "开始挖掘";
            this.Btn_BeginDig.UseVisualStyleBackColor = true;
            this.Btn_BeginDig.Click += new System.EventHandler(this.Btn_BeginDig_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            // 
            // DiggerData
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Btn_BeginDig);
            this.Controls.Add(this.Lab_DataUrl);
            this.Controls.Add(this.Lab_GetMoreDataType);
            this.Controls.Add(this.Lab_DiggerType);
            this.Controls.Add(this.Lab_TamplatePath);
            this.Controls.Add(this.Btn_ImportDiggerTemplate);
            this.Name = "DiggerData";
            this.Text = "挖数据";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_ImportDiggerTemplate;
        private System.Windows.Forms.Label Lab_TamplatePath;
        private System.Windows.Forms.Label Lab_DiggerType;
        private System.Windows.Forms.Label Lab_GetMoreDataType;
        private System.Windows.Forms.Label Lab_DataUrl;
        private System.Windows.Forms.Button Btn_BeginDig;
        private System.Windows.Forms.Timer timer1;
    }
}