using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardsHandler.Database;
using CardsHandler.JSON;

namespace CardsHandler
{
    public partial class FormHandlerCars : Form
    {
        private const string CreateCard = "Создать";
        private const string FindCard = "Найти";
        private const string Charge = "Списать бонусы";
        private const string SearchByPhone = "Телефону";
        private const string SearchByCard = "Номеру карты";

        private readonly Color markerColor = Color.FromArgb(214, 254, 216);

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

        private void BtProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbOperations.Text))
            {
                UI.PrintErrorChoosedOperation();
            }
            else
            {
                WrongData checkinfResult;

                switch (cbOperations.Text)
                {
                    #region СОЗДАНИЕ КАРТЫ

                    case CreateCard:
                        checkinfResult = BL.CheckCreationCompliance(
                            tbPhoneNumber.Text,
                            tbFirstName.Text,
                            tbMiddleName.Text,
                            tbLastName.Text);

                        switch (checkinfResult)
                        {
                            case WrongData.EmptyField:
                                UI.ErrorEptyFields(ref tbResultForm);
                                break;

                            case WrongData.WrongPhone:
                                UI.ErrorInPhoneNumber(ref tbResultForm);
                                break;

                            case WrongData.WrongName:
                                UI.ErrorWrongName(ref tbResultForm);
                                break;

                            case WrongData.None:

                                DBConfigJSON dBConfig = BL.GetDBConfig();

                                PostgresDB pgDB = new PostgresDB(
                                   dBConfig.DBConfig.Server,
                                   dBConfig.DBConfig.UserName,
                                   dBConfig.DBConfig.DBname,
                                   dBConfig.DBConfig.Port);


                                bool isCardExist = false;
                                int newCardNumber;

                                // проверка, существует ли в БД карта с таким номером.
                                do
                                {
                                    BL.GenerateCardNumber(out newCardNumber);
                                    isCardExist = pgDB.CheckIfCardExist(tbCardNumber.Text);

                                } while (!isCardExist);

                                long.TryParse(tbPhoneNumber.Text, out long phoneNumber);

                                Card card = new Card(
                                    newCardNumber,
                                    phoneNumber,
                                    tbFirstName.Text,
                                    tbMiddleName.Text,
                                    tbLastName.Text);

                                pgDB.CreateCard(card);

                                UI.PrintSuccess(ref tbResultForm);

                                // создаем карту
                                break;
                        }

                        break;

                    #endregion СОЗДАНИЕ КАРТЫ

                    #region ПОИСК КАРТЫ

                    case FindCard:

                        checkinfResult = BL.CheckSearchCompliance(
                            cbFindType.Text,
                            tbPhoneNumber.Text,
                            tbCardNumber.Text);

                        switch (checkinfResult)
                        {
                            case WrongData.EmptyField:
                                UI.ErrorEptyFields(ref tbResultForm);
                                break;

                            case WrongData.WrongPhone:
                                UI.ErrorInPhoneNumber(ref tbResultForm);
                                break;

                            case WrongData.WrongCard:
                                UI.ErrorWrongCard(ref tbResultForm);
                                break;

                            case WrongData.None:
                                UI.PrintSuccess(ref tbResultForm);

                                // ищем карту
                                break;
                        }

                        break;

                    #endregion ПОИСК КАРТЫ

                    #region СПИСАНИЕ

                    case Charge:

                        break;

                    #endregion СПИСАНИЕ
                }
            }
        }

        private void CbOperations_SelectedIndexChanged(
            object sender,
            EventArgs e)
        {
            tbCardNumber.BackColor = Color.White;
            tbPhoneNumber.BackColor = Color.White;

            switch (cbOperations.Text)
            {
                case CreateCard:
                    cbFindType.Enabled = false;
                    gbCreation.Enabled = true;
                    gbSearch.Enabled = false;
                    gbCharge.Enabled = false;
                    tbFirstName.Enabled = true;
                    tbMiddleName.Enabled = true;
                    tbLastName.Enabled = true;
                    tbPhoneNumber.Enabled = true;
                    UI.PrintMessageCreationCard(ref tbResultForm);
                    UI.DryItems(tbFirstName, markerColor);
                    UI.DryItems(tbMiddleName, markerColor);
                    UI.DryItems(tbLastName, markerColor);
                    UI.DryItems(tbPhoneNumber, markerColor);

                    break;

                case FindCard:
                    cbFindType.Enabled = true;
                    gbSearch.Enabled = true;
                    gbCreation.Enabled = false;
                    gbCharge.Enabled = false;
                    tbFirstName.Enabled = false;
                    tbMiddleName.Enabled = false;
                    tbLastName.Enabled = false;
                    UI.DryItems(tbFirstName, Color.White);
                    UI.DryItems(tbMiddleName, Color.White);
                    UI.DryItems(tbLastName, Color.White);
                    UI.DryItems(tbCardNumber, Color.White);
                    UI.DryItems(tbChargeSum, Color.White);
                    UI.DryItems(cbFindType, markerColor);
                    UI.PrintMessageSearchingCard(ref tbResultForm);

                    break;

                case Charge:
                    tbCardNumber.Enabled = true;
                    gbCharge.Enabled = true;
                    tbChargeSum.Enabled = true;
                    cbFindType.Enabled = false;
                    gbCreation.Enabled = false;
                    gbSearch.Enabled = false;
                    tbFirstName.Enabled = false;
                    tbMiddleName.Enabled = false;
                    tbLastName.Enabled = false;
                    UI.DryItems(cbFindType, Color.White);
                    UI.DryItems(tbFirstName, Color.White);
                    UI.DryItems(tbMiddleName, Color.White);
                    UI.DryItems(tbLastName, Color.White);
                    UI.DryItems(tbCardNumber, markerColor);
                    UI.DryItems(tbChargeSum, markerColor);
                    UI.PrintMessageCharhingCard(ref tbResultForm);

                    break;
            }
        }

        private void CbFindType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFindType.Text)
            {
                case SearchByPhone:
                    tbCardNumber.Enabled = false;
                    tbPhoneNumber.Enabled = true;
                    UI.DryItems(tbPhoneNumber, Color.FromArgb(214, 254, 216));
                    UI.DryItems(tbCardNumber, Color.White);
                    UI.PrintMessageEnterPhone(ref tbResultForm);
                    UI.DryItems(cbFindType, Color.White);
                    break;

                case SearchByCard:
                    tbPhoneNumber.Enabled = false;
                    tbCardNumber.Enabled = true;
                    UI.DryItems(tbCardNumber, Color.FromArgb(214, 254, 216));
                    UI.DryItems(tbPhoneNumber, Color.White);
                    UI.PrintMessageEnterCard(ref tbResultForm);
                    UI.DryItems(cbFindType, Color.White);
                    break;
            }
        }
    }
}
