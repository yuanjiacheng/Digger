namespace DiggerForm
{
    partial class CreateDiggerTemplateForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.btn_GetSignResult = new System.Windows.Forms.Button();
            this.Btn_StartSign = new System.Windows.Forms.Button();
            this.Cb_DiggerType = new System.Windows.Forms.ComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.chromeWebBrowser1 = new Sashulin.ChromeWebBrowser();
            this.panel3 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.Lab_LoadMoreDataType = new System.Windows.Forms.Label();
            this.Cb_LoadMoreDataType = new System.Windows.Forms.ComboBox();
            this.Btn_Go = new System.Windows.Forms.Button();
            this.Txt_Url = new System.Windows.Forms.TextBox();
            this.btn_SelectUrlDataPath = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Control;
            this.panel1.Controls.Add(this.btn_SelectUrlDataPath);
            this.panel1.Controls.Add(this.btn_GetSignResult);
            this.panel1.Controls.Add(this.Btn_StartSign);
            this.panel1.Controls.Add(this.Cb_DiggerType);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(105, 661);
            this.panel1.TabIndex = 3;
            // 
            // btn_GetSignResult
            // 
            this.btn_GetSignResult.Location = new System.Drawing.Point(14, 76);
            this.btn_GetSignResult.Name = "btn_GetSignResult";
            this.btn_GetSignResult.Size = new System.Drawing.Size(75, 23);
            this.btn_GetSignResult.TabIndex = 2;
            this.btn_GetSignResult.Text = "保存模板";
            this.btn_GetSignResult.UseVisualStyleBackColor = true;
            this.btn_GetSignResult.Click += new System.EventHandler(this.btn_GetSignResult_Click);
            // 
            // Btn_StartSign
            // 
            this.Btn_StartSign.Location = new System.Drawing.Point(14, 46);
            this.Btn_StartSign.Name = "Btn_StartSign";
            this.Btn_StartSign.Size = new System.Drawing.Size(75, 23);
            this.Btn_StartSign.TabIndex = 1;
            this.Btn_StartSign.Text = "标记模板";
            this.Btn_StartSign.UseVisualStyleBackColor = true;
            this.Btn_StartSign.Click += new System.EventHandler(this.Btn_StartSign_Click);
            // 
            // Cb_DiggerType
            // 
            this.Cb_DiggerType.FormattingEnabled = true;
            this.Cb_DiggerType.Location = new System.Drawing.Point(5, 19);
            this.Cb_DiggerType.Name = "Cb_DiggerType";
            this.Cb_DiggerType.Size = new System.Drawing.Size(95, 20);
            this.Cb_DiggerType.TabIndex = 0;
            this.Cb_DiggerType.SelectedValueChanged += new System.EventHandler(this.Cb_DiggerType_SelectedValueChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel4);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(105, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(779, 661);
            this.panel2.TabIndex = 4;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.chromeWebBrowser1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(0, 50);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(779, 611);
            this.panel4.TabIndex = 3;
            // 
            // chromeWebBrowser1
            // 
            this.chromeWebBrowser1.AutoSize = true;
            this.chromeWebBrowser1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.chromeWebBrowser1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.chromeWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chromeWebBrowser1.Location = new System.Drawing.Point(0, 0);
            this.chromeWebBrowser1.Name = "chromeWebBrowser1";
            this.chromeWebBrowser1.Size = new System.Drawing.Size(779, 611);
            this.chromeWebBrowser1.TabIndex = 0;
            this.chromeWebBrowser1.BrowserPreviewKeyDown += new Sashulin.PreviewKeyDownEventHandler(this.chromeWebBrowser1_BrowserPreviewKeyDown);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(779, 50);
            this.panel3.TabIndex = 2;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.Lab_LoadMoreDataType);
            this.groupBox1.Controls.Add(this.Cb_LoadMoreDataType);
            this.groupBox1.Controls.Add(this.Btn_Go);
            this.groupBox1.Controls.Add(this.Txt_Url);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(779, 50);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据源网址";
            // 
            // Lab_LoadMoreDataType
            // 
            this.Lab_LoadMoreDataType.AutoSize = true;
            this.Lab_LoadMoreDataType.Location = new System.Drawing.Point(502, 22);
            this.Lab_LoadMoreDataType.Name = "Lab_LoadMoreDataType";
            this.Lab_LoadMoreDataType.Size = new System.Drawing.Size(113, 12);
            this.Lab_LoadMoreDataType.TabIndex = 3;
            this.Lab_LoadMoreDataType.Text = "加载更多数据方式：";
            // 
            // Cb_LoadMoreDataType
            // 
            this.Cb_LoadMoreDataType.FormattingEnabled = true;
            this.Cb_LoadMoreDataType.Location = new System.Drawing.Point(621, 18);
            this.Cb_LoadMoreDataType.Name = "Cb_LoadMoreDataType";
            this.Cb_LoadMoreDataType.Size = new System.Drawing.Size(146, 20);
            this.Cb_LoadMoreDataType.TabIndex = 2;
            this.Cb_LoadMoreDataType.SelectedIndexChanged += new System.EventHandler(this.Cb_LoadMoreDataType_SelectedIndexChanged);
            // 
            // Btn_Go
            // 
            this.Btn_Go.Location = new System.Drawing.Point(434, 17);
            this.Btn_Go.Name = "Btn_Go";
            this.Btn_Go.Size = new System.Drawing.Size(38, 23);
            this.Btn_Go.TabIndex = 1;
            this.Btn_Go.Text = "Go";
            this.Btn_Go.UseVisualStyleBackColor = true;
            this.Btn_Go.Click += new System.EventHandler(this.Btn_Go_Click);
            // 
            // Txt_Url
            // 
            this.Txt_Url.Location = new System.Drawing.Point(18, 18);
            this.Txt_Url.Name = "Txt_Url";
            this.Txt_Url.Size = new System.Drawing.Size(400, 21);
            this.Txt_Url.TabIndex = 0;
            this.Txt_Url.Text = "http://www.huxiu.com";
            // 
            // btn_SelectUrlDataPath
            // 
            this.btn_SelectUrlDataPath.Location = new System.Drawing.Point(8, 105);
            this.btn_SelectUrlDataPath.Name = "btn_SelectUrlDataPath";
            this.btn_SelectUrlDataPath.Size = new System.Drawing.Size(91, 23);
            this.btn_SelectUrlDataPath.TabIndex = 3;
            this.btn_SelectUrlDataPath.Text = "Url列表文件";
            this.btn_SelectUrlDataPath.UseVisualStyleBackColor = true;
            this.btn_SelectUrlDataPath.Click += new System.EventHandler(this.btn_SelectUrlDataPath_Click);
            // 
            // CreateDiggerTemplateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(884, 661);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "CreateDiggerTemplateForm";
            this.Text = "新建挖掘模板";
            this.Load += new System.EventHandler(this.Main_Load);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Sashulin.ChromeWebBrowser chromeWebBrowser1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button Btn_Go;
        private System.Windows.Forms.TextBox Txt_Url;
        private System.Windows.Forms.ComboBox Cb_DiggerType;
        private System.Windows.Forms.Label Lab_LoadMoreDataType;
        private System.Windows.Forms.ComboBox Cb_LoadMoreDataType;
        private System.Windows.Forms.Button Btn_StartSign;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button btn_GetSignResult;
        private System.Windows.Forms.Button btn_SelectUrlDataPath;
    }
}

