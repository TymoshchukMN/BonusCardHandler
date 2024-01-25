using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Test
{
    public partial class FormHandlerCars : Form
    {
        private const string CreateCard = "Создать";
        private const string FindCard = "Найти";
        private const string Charge = "Списать бонусы";
        private const string SearchByPhone = "Телефону";
        private const string SearchByCard = "Номеру карты";

        public FormHandlerCars()
        {
            InitializeComponent();
            tbPhoneNumber.Enabled = false;
            tbCardNumber.Enabled = false;
            cbFindType.Enabled = false;
            tbChargeSum.Enabled = false;
            tbFirstName.Enabled = false;
            tbMiddleName.Enabled = false;
            tbLastName.Enabled = false;
        }

        /// <summary>
        /// Обработка элементов формы.
        /// </summary>
        private void ProcessFormItems()
        {

        }

        private void BtProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbOperations.Text))
            {
                UI.PrintErrorChoosedOperation();
            }
        }

        private void CbOperations_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            switch (cbOperations.Text)
            {
                case CreateCard:
                    cbFindType.Enabled = false;
                    break;

                case FindCard:
                    cbFindType.Enabled = true;
                    break;

                case Charge:
                    cbFindType.Enabled = false;
                    break;
            }
        }

        private void cbFindType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFindType.Text)
            {
                case SearchByPhone:
                    tbCardNumber.Enabled = false;
                    tbPhoneNumber.Enabled = true;
                    tbPhoneNumber.BackColor = Color.FromArgb(214,254,216);
                    tbCardNumber.BackColor = Color.White;
                    break;

                case SearchByCard:
                    tbPhoneNumber.Enabled = false;
                    tbCardNumber.Enabled = true;
                    tbCardNumber.BackColor = Color.FromArgb(214, 254, 216);
                    tbPhoneNumber.BackColor = Color.White;
                    break;
            }
        }
    }
}
