namespace APIcloud
{
    partial class Main
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.twMenu = new System.Windows.Forms.TreeView();
            this.btnGetData = new System.Windows.Forms.Button();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.tbViewData = new System.Windows.Forms.TextBox();
            this.cbOrganizations = new System.Windows.Forms.ComboBox();
            this.cbTGroups = new System.Windows.Forms.ComboBox();
            this.cbOrderTypes = new System.Windows.Forms.ComboBox();
            this.cbTables = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnCreateDelivery = new System.Windows.Forms.Button();
            this.cbDiscounts = new System.Windows.Forms.ComboBox();
            this.checkDiscount = new System.Windows.Forms.CheckBox();
            this.tbSum = new System.Windows.Forms.TextBox();
            this.sum = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(76, 6);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(95, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Text = "1f9487e9-f30";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "API Token:";
            // 
            // twMenu
            // 
            this.twMenu.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.twMenu.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.twMenu.Location = new System.Drawing.Point(665, 32);
            this.twMenu.Name = "twMenu";
            this.twMenu.Size = new System.Drawing.Size(278, 587);
            this.twMenu.TabIndex = 2;
            this.twMenu.DoubleClick += new System.EventHandler(this.twMenu_DoubleClick);
            // 
            // btnGetData
            // 
            this.btnGetData.Location = new System.Drawing.Point(176, 6);
            this.btnGetData.Name = "btnGetData";
            this.btnGetData.Size = new System.Drawing.Size(75, 20);
            this.btnGetData.TabIndex = 3;
            this.btnGetData.Text = "GET";
            this.btnGetData.UseVisualStyleBackColor = true;
            this.btnGetData.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.Enabled = false;
            this.btnCreateOrder.Location = new System.Drawing.Point(256, 6);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(75, 20);
            this.btnCreateOrder.TabIndex = 4;
            this.btnCreateOrder.Text = "Create Order";
            this.btnCreateOrder.UseVisualStyleBackColor = true;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // tbViewData
            // 
            this.tbViewData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbViewData.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.tbViewData.Location = new System.Drawing.Point(10, 32);
            this.tbViewData.Multiline = true;
            this.tbViewData.Name = "tbViewData";
            this.tbViewData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbViewData.Size = new System.Drawing.Size(432, 587);
            this.tbViewData.TabIndex = 5;
            // 
            // cbOrganizations
            // 
            this.cbOrganizations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrganizations.FormattingEnabled = true;
            this.cbOrganizations.Location = new System.Drawing.Point(449, 64);
            this.cbOrganizations.Name = "cbOrganizations";
            this.cbOrganizations.Size = new System.Drawing.Size(211, 21);
            this.cbOrganizations.TabIndex = 6;
            // 
            // cbTGroups
            // 
            this.cbTGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTGroups.FormattingEnabled = true;
            this.cbTGroups.Location = new System.Drawing.Point(449, 90);
            this.cbTGroups.Name = "cbTGroups";
            this.cbTGroups.Size = new System.Drawing.Size(211, 21);
            this.cbTGroups.TabIndex = 7;
            this.cbTGroups.SelectionChangeCommitted += new System.EventHandler(this.cbTGroups_SelectionChangeCommitted);
            // 
            // cbOrderTypes
            // 
            this.cbOrderTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderTypes.FormattingEnabled = true;
            this.cbOrderTypes.Location = new System.Drawing.Point(449, 116);
            this.cbOrderTypes.Name = "cbOrderTypes";
            this.cbOrderTypes.Size = new System.Drawing.Size(211, 21);
            this.cbOrderTypes.TabIndex = 8;
            // 
            // cbTables
            // 
            this.cbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTables.Location = new System.Drawing.Point(449, 167);
            this.cbTables.Name = "cbTables";
            this.cbTables.Size = new System.Drawing.Size(211, 21);
            this.cbTables.TabIndex = 9;
            this.cbTables.SelectionChangeCommitted += new System.EventHandler(this.cbTables_SelectionChangeCommitted);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(446, 8);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 10;
            // 
            // btnCreateDelivery
            // 
            this.btnCreateDelivery.Enabled = false;
            this.btnCreateDelivery.Location = new System.Drawing.Point(337, 6);
            this.btnCreateDelivery.Margin = new System.Windows.Forms.Padding(2);
            this.btnCreateDelivery.Name = "btnCreateDelivery";
            this.btnCreateDelivery.Size = new System.Drawing.Size(105, 20);
            this.btnCreateDelivery.TabIndex = 11;
            this.btnCreateDelivery.Text = "Create Delivery";
            this.btnCreateDelivery.UseVisualStyleBackColor = true;
            this.btnCreateDelivery.Click += new System.EventHandler(this.btnCreateDelivery_Click);
            // 
            // cbDiscounts
            // 
            this.cbDiscounts.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDiscounts.FormattingEnabled = true;
            this.cbDiscounts.Location = new System.Drawing.Point(449, 141);
            this.cbDiscounts.Margin = new System.Windows.Forms.Padding(2);
            this.cbDiscounts.Name = "cbDiscounts";
            this.cbDiscounts.Size = new System.Drawing.Size(211, 21);
            this.cbDiscounts.TabIndex = 12;
            // 
            // checkDiscount
            // 
            this.checkDiscount.AutoSize = true;
            this.checkDiscount.Location = new System.Drawing.Point(449, 38);
            this.checkDiscount.Name = "checkDiscount";
            this.checkDiscount.Size = new System.Drawing.Size(86, 17);
            this.checkDiscount.TabIndex = 13;
            this.checkDiscount.Text = "use discount";
            this.checkDiscount.UseVisualStyleBackColor = true;
            // 
            // tbSum
            // 
            this.tbSum.Location = new System.Drawing.Point(600, 35);
            this.tbSum.Name = "tbSum";
            this.tbSum.Size = new System.Drawing.Size(59, 20);
            this.tbSum.TabIndex = 14;
            this.tbSum.Text = "0";
            // 
            // sum
            // 
            this.sum.AutoSize = true;
            this.sum.Location = new System.Drawing.Point(565, 38);
            this.sum.Name = "sum";
            this.sum.Size = new System.Drawing.Size(29, 13);
            this.sum.TabIndex = 15;
            this.sum.Text = "sum:";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(950, 624);
            this.Controls.Add(this.sum);
            this.Controls.Add(this.tbSum);
            this.Controls.Add(this.checkDiscount);
            this.Controls.Add(this.cbDiscounts);
            this.Controls.Add(this.btnCreateDelivery);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbTables);
            this.Controls.Add(this.cbOrderTypes);
            this.Controls.Add(this.cbTGroups);
            this.Controls.Add(this.cbOrganizations);
            this.Controls.Add(this.tbViewData);
            this.Controls.Add(this.btnCreateOrder);
            this.Controls.Add(this.btnGetData);
            this.Controls.Add(this.twMenu);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Main";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "API Cloud";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TreeView twMenu;
        private System.Windows.Forms.Button btnGetData;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.TextBox tbViewData;
        private System.Windows.Forms.ComboBox cbOrganizations;
        private System.Windows.Forms.ComboBox cbTGroups;
        private System.Windows.Forms.ComboBox cbOrderTypes;
        private System.Windows.Forms.ComboBox cbTables;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnCreateDelivery;
        private System.Windows.Forms.ComboBox cbDiscounts;
        private System.Windows.Forms.CheckBox checkDiscount;
        private System.Windows.Forms.TextBox tbSum;
        private System.Windows.Forms.Label sum;
    }
}

