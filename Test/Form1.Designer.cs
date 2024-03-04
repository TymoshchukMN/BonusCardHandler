namespace CardsHandler
{
    partial class FormHandlerCards
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
            this.tabSingleOper = new System.Windows.Forms.TabPage();
            this.btGetAllCards = new System.Windows.Forms.Button();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.gbCharge = new System.Windows.Forms.GroupBox();
            this.rbAddBonuses = new System.Windows.Forms.RadioButton();
            this.rbRemoveBonuses = new System.Windows.Forms.RadioButton();
            this.tbChargeSum = new System.Windows.Forms.TextBox();
            this.lbChargeSum = new System.Windows.Forms.Label();
            this.cbOperations = new System.Windows.Forms.ComboBox();
            this.tbCardNumber = new System.Windows.Forms.TextBox();
            this.tbPhoneNumber = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btProcess = new System.Windows.Forms.Button();
            this.lbCardNumber = new System.Windows.Forms.Label();
            this.lbPhoneNumber = new System.Windows.Forms.Label();
            this.gbCreation = new System.Windows.Forms.GroupBox();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.tbMiddleName = new System.Windows.Forms.TextBox();
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.lbLastName = new System.Windows.Forms.Label();
            this.lbMiddleName = new System.Windows.Forms.Label();
            this.lbFirstName = new System.Windows.Forms.Label();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.cbFindType = new System.Windows.Forms.ComboBox();
            this.lbSearchType = new System.Windows.Forms.Label();
            this.tabs = new System.Windows.Forms.TabControl();
            this.tabSingleOper.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.gbCharge.SuspendLayout();
            this.gbCreation.SuspendLayout();
            this.gbSearch.SuspendLayout();
            this.tabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabSingleOper
            // 
            this.tabSingleOper.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabSingleOper.Controls.Add(this.btGetAllCards);
            this.tabSingleOper.Controls.Add(this.dataGridView);
            this.tabSingleOper.Controls.Add(this.gbCharge);
            this.tabSingleOper.Controls.Add(this.cbOperations);
            this.tabSingleOper.Controls.Add(this.tbCardNumber);
            this.tabSingleOper.Controls.Add(this.tbPhoneNumber);
            this.tabSingleOper.Controls.Add(this.label1);
            this.tabSingleOper.Controls.Add(this.btProcess);
            this.tabSingleOper.Controls.Add(this.lbCardNumber);
            this.tabSingleOper.Controls.Add(this.lbPhoneNumber);
            this.tabSingleOper.Controls.Add(this.gbCreation);
            this.tabSingleOper.Controls.Add(this.gbSearch);
            this.tabSingleOper.Location = new System.Drawing.Point(4, 22);
            this.tabSingleOper.Name = "tabSingleOper";
            this.tabSingleOper.Padding = new System.Windows.Forms.Padding(3);
            this.tabSingleOper.Size = new System.Drawing.Size(831, 574);
            this.tabSingleOper.TabIndex = 0;
            this.tabSingleOper.Text = "Одиночные операции";
            // 
            // btGetAllCards
            // 
            this.btGetAllCards.Location = new System.Drawing.Point(592, 21);
            this.btGetAllCards.Name = "btGetAllCards";
            this.btGetAllCards.Size = new System.Drawing.Size(183, 23);
            this.btGetAllCards.TabIndex = 14;
            this.btGetAllCards.Text = "Получить все карты в базе";
            this.btGetAllCards.UseVisualStyleBackColor = true;
            this.btGetAllCards.Click += new System.EventHandler(this.BtGetAllCards_Click);
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(1, 331);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.ReadOnly = true;
            this.dataGridView.Size = new System.Drawing.Size(829, 247);
            this.dataGridView.TabIndex = 13;
            // 
            // gbCharge
            // 
            this.gbCharge.Controls.Add(this.rbAddBonuses);
            this.gbCharge.Controls.Add(this.rbRemoveBonuses);
            this.gbCharge.Controls.Add(this.tbChargeSum);
            this.gbCharge.Controls.Add(this.lbChargeSum);
            this.gbCharge.Location = new System.Drawing.Point(386, 128);
            this.gbCharge.Name = "gbCharge";
            this.gbCharge.Size = new System.Drawing.Size(290, 197);
            this.gbCharge.TabIndex = 11;
            this.gbCharge.TabStop = false;
            this.gbCharge.Text = "Операции с бонусами";
            // 
            // rbAddBonuses
            // 
            this.rbAddBonuses.AutoSize = true;
            this.rbAddBonuses.Location = new System.Drawing.Point(13, 47);
            this.rbAddBonuses.Name = "rbAddBonuses";
            this.rbAddBonuses.Size = new System.Drawing.Size(84, 17);
            this.rbAddBonuses.TabIndex = 21;
            this.rbAddBonuses.TabStop = true;
            this.rbAddBonuses.Text = "Зачислисть";
            this.rbAddBonuses.UseVisualStyleBackColor = true;
            this.rbAddBonuses.CheckedChanged += new System.EventHandler(this.RbAddBonuses_CheckedChanged);
            // 
            // rbRemoveBonuses
            // 
            this.rbRemoveBonuses.AutoSize = true;
            this.rbRemoveBonuses.Location = new System.Drawing.Point(13, 21);
            this.rbRemoveBonuses.Name = "rbRemoveBonuses";
            this.rbRemoveBonuses.Size = new System.Drawing.Size(67, 17);
            this.rbRemoveBonuses.TabIndex = 20;
            this.rbRemoveBonuses.TabStop = true;
            this.rbRemoveBonuses.Text = "Списать";
            this.rbRemoveBonuses.UseVisualStyleBackColor = true;
            this.rbRemoveBonuses.CheckedChanged += new System.EventHandler(this.RbRemoveBonuses_CheckedChanged);
            // 
            // tbChargeSum
            // 
            this.tbChargeSum.Location = new System.Drawing.Point(75, 95);
            this.tbChargeSum.Name = "tbChargeSum";
            this.tbChargeSum.Size = new System.Drawing.Size(155, 20);
            this.tbChargeSum.TabIndex = 17;
            // 
            // lbChargeSum
            // 
            this.lbChargeSum.AutoSize = true;
            this.lbChargeSum.Location = new System.Drawing.Point(10, 102);
            this.lbChargeSum.Name = "lbChargeSum";
            this.lbChargeSum.Size = new System.Drawing.Size(41, 13);
            this.lbChargeSum.TabIndex = 16;
            this.lbChargeSum.Text = "Сумма";
            // 
            // cbOperations
            // 
            this.cbOperations.FormattingEnabled = true;
            this.cbOperations.Items.AddRange(new object[] {
            "Создать",
            "Найти",
            "Изменить бонусы",
            "Помотреть баланс"});
            this.cbOperations.Location = new System.Drawing.Point(154, 21);
            this.cbOperations.Name = "cbOperations";
            this.cbOperations.Size = new System.Drawing.Size(132, 21);
            this.cbOperations.TabIndex = 4;
            this.cbOperations.SelectedIndexChanged += new System.EventHandler(this.CbOperations_SelectedIndexChanged);
            // 
            // tbCardNumber
            // 
            this.tbCardNumber.Location = new System.Drawing.Point(154, 88);
            this.tbCardNumber.Name = "tbCardNumber";
            this.tbCardNumber.Size = new System.Drawing.Size(132, 20);
            this.tbCardNumber.TabIndex = 1;
            // 
            // tbPhoneNumber
            // 
            this.tbPhoneNumber.Location = new System.Drawing.Point(154, 54);
            this.tbPhoneNumber.Name = "tbPhoneNumber";
            this.tbPhoneNumber.Size = new System.Drawing.Size(132, 20);
            this.tbPhoneNumber.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Вид операции";
            // 
            // btProcess
            // 
            this.btProcess.BackColor = System.Drawing.Color.Lime;
            this.btProcess.Location = new System.Drawing.Point(365, 21);
            this.btProcess.Name = "btProcess";
            this.btProcess.Size = new System.Drawing.Size(125, 65);
            this.btProcess.TabIndex = 7;
            this.btProcess.Text = "Обработать";
            this.btProcess.UseVisualStyleBackColor = false;
            this.btProcess.Click += new System.EventHandler(this.BtProcess_Click);
            // 
            // lbCardNumber
            // 
            this.lbCardNumber.AutoSize = true;
            this.lbCardNumber.Location = new System.Drawing.Point(18, 91);
            this.lbCardNumber.Name = "lbCardNumber";
            this.lbCardNumber.Size = new System.Drawing.Size(78, 13);
            this.lbCardNumber.TabIndex = 0;
            this.lbCardNumber.Text = "Номер карты:";
            // 
            // lbPhoneNumber
            // 
            this.lbPhoneNumber.AutoSize = true;
            this.lbPhoneNumber.Location = new System.Drawing.Point(19, 57);
            this.lbPhoneNumber.Name = "lbPhoneNumber";
            this.lbPhoneNumber.Size = new System.Drawing.Size(93, 13);
            this.lbPhoneNumber.TabIndex = 2;
            this.lbPhoneNumber.Text = "Номер телефона";
            // 
            // gbCreation
            // 
            this.gbCreation.Controls.Add(this.tbLastName);
            this.gbCreation.Controls.Add(this.tbMiddleName);
            this.gbCreation.Controls.Add(this.tbFirstName);
            this.gbCreation.Controls.Add(this.lbLastName);
            this.gbCreation.Controls.Add(this.lbMiddleName);
            this.gbCreation.Controls.Add(this.lbFirstName);
            this.gbCreation.Location = new System.Drawing.Point(24, 203);
            this.gbCreation.Name = "gbCreation";
            this.gbCreation.Size = new System.Drawing.Size(337, 122);
            this.gbCreation.TabIndex = 10;
            this.gbCreation.TabStop = false;
            this.gbCreation.Text = "Создание";
            // 
            // tbLastName
            // 
            this.tbLastName.Location = new System.Drawing.Point(101, 82);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(192, 20);
            this.tbLastName.TabIndex = 15;
            // 
            // tbMiddleName
            // 
            this.tbMiddleName.Location = new System.Drawing.Point(101, 53);
            this.tbMiddleName.Name = "tbMiddleName";
            this.tbMiddleName.Size = new System.Drawing.Size(192, 20);
            this.tbMiddleName.TabIndex = 14;
            // 
            // tbFirstName
            // 
            this.tbFirstName.Location = new System.Drawing.Point(101, 25);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(192, 20);
            this.tbFirstName.TabIndex = 13;
            // 
            // lbLastName
            // 
            this.lbLastName.AutoSize = true;
            this.lbLastName.Location = new System.Drawing.Point(6, 89);
            this.lbLastName.Name = "lbLastName";
            this.lbLastName.Size = new System.Drawing.Size(56, 13);
            this.lbLastName.TabIndex = 12;
            this.lbLastName.Text = "Фамилия";
            // 
            // lbMiddleName
            // 
            this.lbMiddleName.AutoSize = true;
            this.lbMiddleName.Location = new System.Drawing.Point(6, 60);
            this.lbMiddleName.Name = "lbMiddleName";
            this.lbMiddleName.Size = new System.Drawing.Size(54, 13);
            this.lbMiddleName.TabIndex = 11;
            this.lbMiddleName.Text = "Отчество";
            // 
            // lbFirstName
            // 
            this.lbFirstName.AutoSize = true;
            this.lbFirstName.Location = new System.Drawing.Point(6, 31);
            this.lbFirstName.Name = "lbFirstName";
            this.lbFirstName.Size = new System.Drawing.Size(29, 13);
            this.lbFirstName.TabIndex = 9;
            this.lbFirstName.Text = "Имя";
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.cbFindType);
            this.gbSearch.Controls.Add(this.lbSearchType);
            this.gbSearch.Location = new System.Drawing.Point(22, 126);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(337, 66);
            this.gbSearch.TabIndex = 9;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "Поиск";
            // 
            // cbFindType
            // 
            this.cbFindType.FormattingEnabled = true;
            this.cbFindType.Items.AddRange(new object[] {
            "Телефону",
            "Номеру карты"});
            this.cbFindType.Location = new System.Drawing.Point(101, 20);
            this.cbFindType.Name = "cbFindType";
            this.cbFindType.Size = new System.Drawing.Size(155, 21);
            this.cbFindType.TabIndex = 8;
            this.cbFindType.SelectedIndexChanged += new System.EventHandler(this.CbFindType_SelectedIndexChanged);
            // 
            // lbSearchType
            // 
            this.lbSearchType.AutoSize = true;
            this.lbSearchType.Location = new System.Drawing.Point(15, 25);
            this.lbSearchType.Name = "lbSearchType";
            this.lbSearchType.Size = new System.Drawing.Size(57, 13);
            this.lbSearchType.TabIndex = 4;
            this.lbSearchType.Text = "Поиск по:";
            // 
            // tabs
            // 
            this.tabs.Controls.Add(this.tabSingleOper);
            this.tabs.Location = new System.Drawing.Point(12, 13);
            this.tabs.Name = "tabs";
            this.tabs.SelectedIndex = 0;
            this.tabs.Size = new System.Drawing.Size(839, 600);
            this.tabs.TabIndex = 13;
            // 
            // FormHandlerCards
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 647);
            this.Controls.Add(this.tabs);
            this.Name = "FormHandlerCards";
            this.Text = "Heandler cards";
            this.tabSingleOper.ResumeLayout(false);
            this.tabSingleOper.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.gbCharge.ResumeLayout(false);
            this.gbCharge.PerformLayout();
            this.gbCreation.ResumeLayout(false);
            this.gbCreation.PerformLayout();
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.tabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabPage tabSingleOper;
        private System.Windows.Forms.Button btGetAllCards;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.GroupBox gbCharge;
        private System.Windows.Forms.RadioButton rbAddBonuses;
        private System.Windows.Forms.RadioButton rbRemoveBonuses;
        private System.Windows.Forms.TextBox tbChargeSum;
        private System.Windows.Forms.Label lbChargeSum;
        private System.Windows.Forms.ComboBox cbOperations;
        private System.Windows.Forms.TextBox tbCardNumber;
        private System.Windows.Forms.TextBox tbPhoneNumber;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btProcess;
        private System.Windows.Forms.Label lbCardNumber;
        private System.Windows.Forms.Label lbPhoneNumber;
        private System.Windows.Forms.GroupBox gbCreation;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.TextBox tbMiddleName;
        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.Label lbLastName;
        private System.Windows.Forms.Label lbMiddleName;
        private System.Windows.Forms.Label lbFirstName;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.ComboBox cbFindType;
        private System.Windows.Forms.Label lbSearchType;
        private System.Windows.Forms.TabControl tabs;
    }
}

