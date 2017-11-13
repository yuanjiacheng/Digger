namespace DiggerForm
{
    partial class ScheduledTask
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
            this.Btn_ImportDiggerTemplate = new System.Windows.Forms.Button();
            this.tb_Taskname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_StartTime = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_Interval = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_Save = new System.Windows.Forms.Button();
            this.Lab_TamplatePath = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // Btn_ImportDiggerTemplate
            // 
            this.Btn_ImportDiggerTemplate.Location = new System.Drawing.Point(41, 12);
            this.Btn_ImportDiggerTemplate.Name = "Btn_ImportDiggerTemplate";
            this.Btn_ImportDiggerTemplate.Size = new System.Drawing.Size(99, 23);
            this.Btn_ImportDiggerTemplate.TabIndex = 1;
            this.Btn_ImportDiggerTemplate.Text = "选择挖掘模板";
            this.Btn_ImportDiggerTemplate.UseVisualStyleBackColor = true;
            this.Btn_ImportDiggerTemplate.Click += new System.EventHandler(this.Btn_ImportDiggerTemplate_Click);
            // 
            // tb_Taskname
            // 
            this.tb_Taskname.Location = new System.Drawing.Point(117, 48);
            this.tb_Taskname.Name = "tb_Taskname";
            this.tb_Taskname.Size = new System.Drawing.Size(100, 21);
            this.tb_Taskname.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "任务名:";
            // 
            // tb_StartTime
            // 
            this.tb_StartTime.Location = new System.Drawing.Point(117, 76);
            this.tb_StartTime.Name = "tb_StartTime";
            this.tb_StartTime.Size = new System.Drawing.Size(100, 21);
            this.tb_StartTime.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 79);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 5;
            this.label2.Text = "开始时间:";
            // 
            // tb_Interval
            // 
            this.tb_Interval.Location = new System.Drawing.Point(117, 104);
            this.tb_Interval.Name = "tb_Interval";
            this.tb_Interval.Size = new System.Drawing.Size(100, 21);
            this.tb_Interval.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(41, 108);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 7;
            this.label3.Text = "间隔时间:";
            // 
            // btn_Save
            // 
            this.btn_Save.Location = new System.Drawing.Point(43, 159);
            this.btn_Save.Name = "btn_Save";
            this.btn_Save.Size = new System.Drawing.Size(75, 23);
            this.btn_Save.TabIndex = 8;
            this.btn_Save.Text = "保存";
            this.btn_Save.UseVisualStyleBackColor = true;
            this.btn_Save.Click += new System.EventHandler(this.btn_Save_Click);
            // 
            // Lab_TamplatePath
            // 
            this.Lab_TamplatePath.AutoSize = true;
            this.Lab_TamplatePath.Location = new System.Drawing.Point(146, 17);
            this.Lab_TamplatePath.Name = "Lab_TamplatePath";
            this.Lab_TamplatePath.Size = new System.Drawing.Size(0, 12);
            this.Lab_TamplatePath.TabIndex = 9;
            // 
            // ScheduledTask
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Controls.Add(this.Lab_TamplatePath);
            this.Controls.Add(this.btn_Save);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_Interval);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_StartTime);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_Taskname);
            this.Controls.Add(this.Btn_ImportDiggerTemplate);
            this.Name = "ScheduledTask";
            this.Text = "新建计划任务";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Btn_ImportDiggerTemplate;
        private System.Windows.Forms.TextBox tb_Taskname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_StartTime;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tb_Interval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_Save;
        private System.Windows.Forms.Label Lab_TamplatePath;
    }
}