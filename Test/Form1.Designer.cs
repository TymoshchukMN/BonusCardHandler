namespace CardsHandler
{
    partial class FormHandlerCars
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
            this.lbCardNumber = new System.Windows.Forms.Label();
            this.tbCardNumber = new System.Windows.Forms.TextBox();
            this.lbPhoneNumber = new System.Windows.Forms.Label();
            this.tbPhoneNumber = new System.Windows.Forms.TextBox();
            this.lbSearchType = new System.Windows.Forms.Label();
            this.btProcess = new System.Windows.Forms.Button();
            this.cbOperations = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbFindType = new System.Windows.Forms.ComboBox();
            this.gbSearch = new System.Windows.Forms.GroupBox();
            this.gbCreation = new System.Windows.Forms.GroupBox();
            this.tbLastName = new System.Windows.Forms.TextBox();
            this.tbMiddleName = new System.Windows.Forms.TextBox();
            this.tbFirstName = new System.Windows.Forms.TextBox();
            this.lbLastName = new System.Windows.Forms.Label();
            this.lbMiddleName = new System.Windows.Forms.Label();
            this.lbFirstName = new System.Windows.Forms.Label();
            this.gbCharge = new System.Windows.Forms.GroupBox();
            this.tbChargeSum = new System.Windows.Forms.TextBox();
            this.lbChargeSum = new System.Windows.Forms.Label();
            this.tbResultForm = new System.Windows.Forms.RichTextBox();
            this.gbSearch.SuspendLayout();
            this.gbCreation.SuspendLayout();
            this.gbCharge.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbCardNumber
            // 
            this.lbCardNumber.AutoSize = true;
            this.lbCardNumber.Location = new System.Drawing.Point(8, 82);
            this.lbCardNumber.Name = "lbCardNumber";
            this.lbCardNumber.Size = new System.Drawing.Size(78, 13);
            this.lbCardNumber.TabIndex = 0;
            this.lbCardNumber.Text = "Номер карты:";
            // 
            // tbCardNumber
            // 
            this.tbCardNumber.Location = new System.Drawing.Point(144, 79);
            this.tbCardNumber.Name = "tbCardNumber";
            this.tbCardNumber.Size = new System.Drawing.Size(132, 20);
            this.tbCardNumber.TabIndex = 1;
            // 
            // lbPhoneNumber
            // 
            this.lbPhoneNumber.AutoSize = true;
            this.lbPhoneNumber.Location = new System.Drawing.Point(9, 48);
            this.lbPhoneNumber.Name = "lbPhoneNumber";
            this.lbPhoneNumber.Size = new System.Drawing.Size(93, 13);
            this.lbPhoneNumber.TabIndex = 2;
            this.lbPhoneNumber.Text = "Номер телефона";
            // 
            // tbPhoneNumber
            // 
            this.tbPhoneNumber.Location = new System.Drawing.Point(144, 45);
            this.tbPhoneNumber.Name = "tbPhoneNumber";
            this.tbPhoneNumber.Size = new System.Drawing.Size(132, 20);
            this.tbPhoneNumber.TabIndex = 3;
            // 
            // lbSearchType
            // 
            this.lbSearchType.AutoSize = true;
            this.lbSearchType.Location = new System.Drawing.Point(6, 31);
            this.lbSearchType.Name = "lbSearchType";
            this.lbSearchType.Size = new System.Drawing.Size(57, 13);
            this.lbSearchType.TabIndex = 4;
            this.lbSearchType.Text = "Поиск по:";
            // 
            // btProcess
            // 
            this.btProcess.Location = new System.Drawing.Point(355, 12);
            this.btProcess.Name = "btProcess";
            this.btProcess.Size = new System.Drawing.Size(125, 65);
            this.btProcess.TabIndex = 7;
            this.btProcess.Text = "Обработать";
            this.btProcess.UseVisualStyleBackColor = true;
            this.btProcess.Click += new System.EventHandler(this.BtProcess_Click);
            // 
            // cbOperations
            // 
            this.cbOperations.FormattingEnabled = true;
            this.cbOperations.Items.AddRange(new object[] {
            "Создать",
            "Найти",
            "Списать бонусы"});
            this.cbOperations.Location = new System.Drawing.Point(144, 12);
            this.cbOperations.Name = "cbOperations";
            this.cbOperations.Size = new System.Drawing.Size(132, 21);
            this.cbOperations.TabIndex = 4;
            this.cbOperations.SelectedIndexChanged += new System.EventHandler(this.CbOperations_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Вид операции";
            // 
            // cbFindType
            // 
            this.cbFindType.FormattingEnabled = true;
            this.cbFindType.Items.AddRange(new object[] {
            "Телефону",
            "Номеру карты"});
            this.cbFindType.Location = new System.Drawing.Point(69, 23);
            this.cbFindType.Name = "cbFindType";
            this.cbFindType.Size = new System.Drawing.Size(132, 21);
            this.cbFindType.TabIndex = 8;
            this.cbFindType.SelectedIndexChanged += new System.EventHandler(this.CbFindType_SelectedIndexChanged);
            // 
            // gbSearch
            // 
            this.gbSearch.Controls.Add(this.cbFindType);
            this.gbSearch.Controls.Add(this.lbSearchType);
            this.gbSearch.Location = new System.Drawing.Point(14, 119);
            this.gbSearch.Name = "gbSearch";
            this.gbSearch.Size = new System.Drawing.Size(241, 122);
            this.gbSearch.TabIndex = 9;
            this.gbSearch.TabStop = false;
            this.gbSearch.Text = "Поиск";
            // 
            // gbCreation
            // 
            this.gbCreation.Controls.Add(this.tbLastName);
            this.gbCreation.Controls.Add(this.tbMiddleName);
            this.gbCreation.Controls.Add(this.tbFirstName);
            this.gbCreation.Controls.Add(this.lbLastName);
            this.gbCreation.Controls.Add(this.lbMiddleName);
            this.gbCreation.Controls.Add(this.lbFirstName);
            this.gbCreation.Location = new System.Drawing.Point(279, 119);
            this.gbCreation.Name = "gbCreation";
            this.gbCreation.Size = new System.Drawing.Size(318, 122);
            this.gbCreation.TabIndex = 10;
            this.gbCreation.TabStop = false;
            this.gbCreation.Text = "Создание";
            // 
            // tbLastName
            // 
            this.tbLastName.Location = new System.Drawing.Point(120, 75);
            this.tbLastName.Name = "tbLastName";
            this.tbLastName.Size = new System.Drawing.Size(192, 20);
            this.tbLastName.TabIndex = 15;
            // 
            // tbMiddleName
            // 
            this.tbMiddleName.Location = new System.Drawing.Point(120, 46);
            this.tbMiddleName.Name = "tbMiddleName";
            this.tbMiddleName.Size = new System.Drawing.Size(192, 20);
            this.tbMiddleName.TabIndex = 14;
            // 
            // tbFirstName
            // 
            this.tbFirstName.Location = new System.Drawing.Point(120, 18);
            this.tbFirstName.Name = "tbFirstName";
            this.tbFirstName.Size = new System.Drawing.Size(192, 20);
            this.tbFirstName.TabIndex = 13;
            // 
            // lbLastName
            // 
            this.lbLastName.AutoSize = true;
            this.lbLastName.Location = new System.Drawing.Point(6, 78);
            this.lbLastName.Name = "lbLastName";
            this.lbLastName.Size = new System.Drawing.Size(56, 13);
            this.lbLastName.TabIndex = 12;
            this.lbLastName.Text = "Фамилия";
            // 
            // lbMiddleName
            // 
            this.lbMiddleName.AutoSize = true;
            this.lbMiddleName.Location = new System.Drawing.Point(6, 49);
            this.lbMiddleName.Name = "lbMiddleName";
            this.lbMiddleName.Size = new System.Drawing.Size(54, 13);
            this.lbMiddleName.TabIndex = 11;
            this.lbMiddleName.Text = "Отчество";
            // 
            // lbFirstName
            // 
            this.lbFirstName.AutoSize = true;
            this.lbFirstName.Location = new System.Drawing.Point(6, 20);
            this.lbFirstName.Name = "lbFirstName";
            this.lbFirstName.Size = new System.Drawing.Size(29, 13);
            this.lbFirstName.TabIndex = 9;
            this.lbFirstName.Text = "Имя";
            // 
            // gbCharge
            // 
            this.gbCharge.Controls.Add(this.tbChargeSum);
            this.gbCharge.Controls.Add(this.lbChargeSum);
            this.gbCharge.Location = new System.Drawing.Point(14, 256);
            this.gbCharge.Name = "gbCharge";
            this.gbCharge.Size = new System.Drawing.Size(290, 65);
            this.gbCharge.TabIndex = 11;
            this.gbCharge.TabStop = false;
            this.gbCharge.Text = "Списание";
            // 
            // tbChargeSum
            // 
            this.tbChargeSum.Location = new System.Drawing.Point(107, 13);
            this.tbChargeSum.Name = "tbChargeSum";
            this.tbChargeSum.Size = new System.Drawing.Size(155, 20);
            this.tbChargeSum.TabIndex = 17;
            // 
            // lbChargeSum
            // 
            this.lbChargeSum.AutoSize = true;
            this.lbChargeSum.Location = new System.Drawing.Point(6, 21);
            this.lbChargeSum.Name = "lbChargeSum";
            this.lbChargeSum.Size = new System.Drawing.Size(95, 13);
            this.lbChargeSum.TabIndex = 16;
            this.lbChargeSum.Text = "Сумма списания:";
            // 
            // tbResultForm
            // 
            this.tbResultForm.Location = new System.Drawing.Point(12, 365);
            this.tbResultForm.Name = "tbResultForm";
            this.tbResultForm.Size = new System.Drawing.Size(678, 183);
            this.tbResultForm.TabIndex = 12;
            this.tbResultForm.Text = "";
            // 
            // FormHandlerCars
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 560);
            this.Controls.Add(this.tbResultForm);
            this.Controls.Add(this.tbCardNumber);
            this.Controls.Add(this.gbCharge);
            this.Controls.Add(this.lbCardNumber);
            this.Controls.Add(this.gbCreation);
            this.Controls.Add(this.gbSearch);
            this.Controls.Add(this.tbPhoneNumber);
            this.Controls.Add(this.lbPhoneNumber);
            this.Controls.Add(this.btProcess);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbOperations);
            this.Name = "FormHandlerCars";
            this.Text = "Heandler cards";
            this.gbSearch.ResumeLayout(false);
            this.gbSearch.PerformLayout();
            this.gbCreation.ResumeLayout(false);
            this.gbCreation.PerformLayout();
            this.gbCharge.ResumeLayout(false);
            this.gbCharge.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbCardNumber;
        private System.Windows.Forms.TextBox tbCardNumber;
        private System.Windows.Forms.Label lbPhoneNumber;
        private System.Windows.Forms.TextBox tbPhoneNumber;
        private System.Windows.Forms.Label lbSearchType;
        private System.Windows.Forms.Button btProcess;
        private System.Windows.Forms.ComboBox cbOperations;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbFindType;
        private System.Windows.Forms.GroupBox gbSearch;
        private System.Windows.Forms.GroupBox gbCreation;
        private System.Windows.Forms.TextBox tbLastName;
        private System.Windows.Forms.TextBox tbMiddleName;
        private System.Windows.Forms.TextBox tbFirstName;
        private System.Windows.Forms.Label lbLastName;
        private System.Windows.Forms.Label lbMiddleName;
        private System.Windows.Forms.Label lbFirstName;
        private System.Windows.Forms.GroupBox gbCharge;
        private System.Windows.Forms.TextBox tbChargeSum;
        private System.Windows.Forms.Label lbChargeSum;
        private System.Windows.Forms.RichTextBox tbResultForm;
    }
}

