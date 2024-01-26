﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CardsHandler.Database;
using CardsHandler.Enums;
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
        private SearchType searchType;
        private CardsOperation cardsOperation;

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
                ResultOperations checkinfResult;

                switch (cardsOperation)
                {
                    #region СОЗДАНИЕ КАРТЫ

                    case CardsOperation.Create:
                        checkinfResult = BL.CheckCreationCompliance(
                            tbPhoneNumber.Text,
                            tbFirstName.Text,
                            tbMiddleName.Text,
                            tbLastName.Text);

                        switch (checkinfResult)
                        {
                            case ResultOperations.EmptyField:
                                UI.ErrorEptyFields(ref tbResultForm);
                                break;

                            case ResultOperations.WrongPhone:
                                UI.ErrorInPhoneNumber(ref tbResultForm);
                                break;

                            case ResultOperations.WrongName:
                                UI.ErrorWrongName(ref tbResultForm);
                                break;

                            case ResultOperations.None:

                                PostgresDB pgDB = CreatePostrgesInstance();

                                bool isCardExist = true;
                                int newCardNumber;

                                // проверка, существует ли в БД карта с таким номером.
                                do
                                {
                                    BL.GenerateCardNumber(out newCardNumber);
                                    isCardExist = pgDB.CheckIfCardExist(newCardNumber);
                                }
                                while (isCardExist);

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

                    case CardsOperation.Find:

                        checkinfResult = BL.CheckSearchCompliance(
                            cbFindType.Text,
                            tbPhoneNumber.Text,
                            tbCardNumber.Text);

                        switch (checkinfResult)
                        {
                            case ResultOperations.EmptyField:
                                UI.ErrorEptyFields(ref tbResultForm);
                                break;

                            case ResultOperations.WrongPhone:
                                UI.ErrorInPhoneNumber(ref tbResultForm);
                                break;

                            case ResultOperations.WrongCard:
                                UI.ErrorWrongCard(ref tbResultForm);
                                break;

                            case ResultOperations.None:

                                PostgresDB pgDB = CreatePostrgesInstance();

                                switch (searchType)
                                {
                                    case SearchType.ByPhone:

                                        int.TryParse(
                                            tbPhoneNumber.Text,
                                            out int phoneNumber);

                                        bool isPhoneExist =
                                            pgDB.CheckIfPhone(phoneNumber);

                                        if (isPhoneExist)
                                        {

                                        }
                                        else
                                        {
                                            UI.PrintErrorPhoneDoesntExist(
                                                ref tbResultForm,
                                                phoneNumber);
                                        }

                                        break;
                                    case SearchType.ByCard:
                                        // поиск по номеру карты
                                        bool isCardExist = true;
                                        int.TryParse(
                                            tbCardNumber.Text,
                                            out int cardNumber);

                                        // проверка, существует ли в БД карта с таким номером.
                                        isCardExist =
                                            pgDB.CheckIfCardExist(cardNumber);

                                        if (isCardExist)
                                        {

                                        }
                                        else
                                        {
                                            UI.PrintErrorCardDoesntExist(
                                                ref tbResultForm,
                                                searchType,
                                                cardNumber);
                                        }

                                        break;
                                }

                                // ищем карту
                                break;
                        }

                        break;

                    #endregion ПОИСК КАРТЫ

                    #region СПИСАНИЕ

                    case CardsOperation.Charge:

                        break;

                    #endregion СПИСАНИЕ
                }
            }

            PostgresDB CreatePostrgesInstance()
            {
                DBConfigJSON dBConfig = BL.GetDBConfig();

                PostgresDB pgDB = new PostgresDB(
                   dBConfig.DBConfig.Server,
                   dBConfig.DBConfig.UserName,
                   dBConfig.DBConfig.DBname,
                   dBConfig.DBConfig.Port);
                return pgDB;
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
                    cardsOperation = CardsOperation.Create;
                    searchType = SearchType.None;
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
                    cardsOperation = CardsOperation.Find;
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
                    cardsOperation = CardsOperation.Charge;
                    searchType = SearchType.None;
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
                    searchType = SearchType.ByPhone;
                    tbCardNumber.Enabled = false;
                    tbPhoneNumber.Enabled = true;
                    UI.DryItems(tbPhoneNumber, Color.FromArgb(214, 254, 216));
                    UI.DryItems(tbCardNumber, Color.White);
                    UI.PrintMessageEnterPhone(ref tbResultForm);
                    UI.DryItems(cbFindType, Color.White);
                    break;

                case SearchByCard:
                    searchType = SearchType.ByCard;
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
