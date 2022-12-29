namespace APIcloud
{
    partial class APICloud
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(APICloud));
            this.token = new System.Windows.Forms.TextBox();
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
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.tbCard = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // token
            // 
            this.token.Location = new System.Drawing.Point(76, 6);
            this.token.Name = "token";
            this.token.Size = new System.Drawing.Size(95, 20);
            this.token.TabIndex = 0;
            this.token.Text = "1f9487e9-f30";
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
            this.twMenu.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.twMenu.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.twMenu.Location = new System.Drawing.Point(316, 32);
            this.twMenu.Name = "twMenu";
            this.twMenu.Size = new System.Drawing.Size(370, 567);
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
            this.tbViewData.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbViewData.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold);
            this.tbViewData.Location = new System.Drawing.Point(5, 605);
            this.tbViewData.Multiline = true;
            this.tbViewData.Name = "tbViewData";
            this.tbViewData.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbViewData.Size = new System.Drawing.Size(681, 169);
            this.tbViewData.TabIndex = 5;
            // 
            // cbOrganizations
            // 
            this.cbOrganizations.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrganizations.FormattingEnabled = true;
            this.cbOrganizations.Location = new System.Drawing.Point(97, 32);
            this.cbOrganizations.Name = "cbOrganizations";
            this.cbOrganizations.Size = new System.Drawing.Size(211, 21);
            this.cbOrganizations.TabIndex = 6;
            this.cbOrganizations.SelectionChangeCommitted += new System.EventHandler(this.cbOrganizations_SelectionChangeCommitted);
            // 
            // cbTGroups
            // 
            this.cbTGroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTGroups.FormattingEnabled = true;
            this.cbTGroups.Location = new System.Drawing.Point(97, 59);
            this.cbTGroups.Name = "cbTGroups";
            this.cbTGroups.Size = new System.Drawing.Size(211, 21);
            this.cbTGroups.TabIndex = 7;
            this.cbTGroups.SelectionChangeCommitted += new System.EventHandler(this.cbTGroups_SelectionChangeCommitted);
            // 
            // cbOrderTypes
            // 
            this.cbOrderTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbOrderTypes.FormattingEnabled = true;
            this.cbOrderTypes.Location = new System.Drawing.Point(97, 86);
            this.cbOrderTypes.Name = "cbOrderTypes";
            this.cbOrderTypes.Size = new System.Drawing.Size(211, 21);
            this.cbOrderTypes.TabIndex = 8;
            // 
            // cbTables
            // 
            this.cbTables.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbTables.Location = new System.Drawing.Point(97, 138);
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
            this.cbDiscounts.Location = new System.Drawing.Point(97, 112);
            this.cbDiscounts.Margin = new System.Windows.Forms.Padding(2);
            this.cbDiscounts.Name = "cbDiscounts";
            this.cbDiscounts.Size = new System.Drawing.Size(211, 21);
            this.cbDiscounts.TabIndex = 12;
            // 
            // checkDiscount
            // 
            this.checkDiscount.AutoSize = true;
            this.checkDiscount.Location = new System.Drawing.Point(3, 536);
            this.checkDiscount.Name = "checkDiscount";
            this.checkDiscount.Size = new System.Drawing.Size(86, 17);
            this.checkDiscount.TabIndex = 13;
            this.checkDiscount.Text = "use discount";
            this.checkDiscount.UseVisualStyleBackColor = true;
            // 
            // tbSum
            // 
            this.tbSum.Location = new System.Drawing.Point(130, 533);
            this.tbSum.Name = "tbSum";
            this.tbSum.Size = new System.Drawing.Size(59, 20);
            this.tbSum.TabIndex = 14;
            this.tbSum.Text = "0";
            // 
            // sum
            // 
            this.sum.AutoSize = true;
            this.sum.Location = new System.Drawing.Point(95, 536);
            this.sum.Name = "sum";
            this.sum.Size = new System.Drawing.Size(29, 13);
            this.sum.TabIndex = 15;
            this.sum.Text = "sum:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 35);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Organizations:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(12, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Terminal groups:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 89);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Order types:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(12, 115);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Discounts:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(12, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "Tables:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(8, 586);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "Logs:";
            // 
            // progressBar1
            // 
            this.progressBar1.Cursor = System.Windows.Forms.Cursors.WaitCursor;
            this.progressBar1.Location = new System.Drawing.Point(3, 559);
            this.progressBar1.MarqueeAnimationSpeed = 20;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(305, 24);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 22;
            this.progressBar1.UseWaitCursor = true;
            this.progressBar1.Visible = false;
            // 
            // tbCard
            // 
            this.tbCard.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tbCard.Location = new System.Drawing.Point(3, 165);
            this.tbCard.Multiline = true;
            this.tbCard.Name = "tbCard";
            this.tbCard.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbCard.Size = new System.Drawing.Size(305, 362);
            this.tbCard.TabIndex = 23;
            // 
            // APICloud
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(693, 779);
            this.Controls.Add(this.tbCard);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
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
            this.Controls.Add(this.token);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "APICloud";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "API Cloud";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox token;
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
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.TextBox tbCard;
    }
}

