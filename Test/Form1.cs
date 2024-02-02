using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
        private const string Charge = "Изменить бонусы";
        private const string SeeBalance = "Помотреть баланс";
        private const string SearchByPhone = "Телефону";
        private const string SearchByCard = "Номеру карты";
        private readonly Color markerColor = Color.FromArgb(214, 254, 216);
        private SearchType searchType;
        private CardsOperation cardsOperation;
        private BonusOperations bonusOperations;

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

        private static PostgresDB CreatePostrgesInstance()
        {
            DBConfigJSON dBConfig = BL.GetDBConfig();

            PostgresDB pgDB = new PostgresDB(
               dBConfig.DBConfig.Server,
               dBConfig.DBConfig.UserName,
               dBConfig.DBConfig.DBname,
               dBConfig.DBConfig.Port);
            return pgDB;
        }

        private void BtProcess_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(cbOperations.Text))
            {
                UI.PrintErrorChoosedOperation();
            }
            else
            {
                ResultOperations operResult;

                PostgresDB pgDB;

                switch (cardsOperation)
                {
                    #region СОЗДАНИЕ КАРТЫ

                    case CardsOperation.Create:
                        operResult = BL.CheckCreationCompliance(
                            tbPhoneNumber.Text,
                            tbFirstName.Text,
                            tbMiddleName.Text,
                            tbLastName.Text);

                        switch (operResult)
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

                                pgDB = CreatePostrgesInstance();
                                long.TryParse(
                                            tbPhoneNumber.Text,
                                            out long phoneNumber);

                                bool isPhoneExist =
                                    pgDB.CheckIfPhone(phoneNumber);

                                if (isPhoneExist)
                                {
                                    UI.PrintErrorPhoneExist();
                                    UI.PrintCardElements(
                                        ref tbResultForm,
                                        pgDB.FindCardByPhone(phoneNumber));
                                }
                                else
                                {
                                    UI.PrintProcessing(ref tbResultForm);

                                    bool isCardExist;
                                    int newCardNumber;

                                    // проверка, существует ли в БД карта с таким номером.
                                    do
                                    {
                                        BL.GenerateCardNumber(out newCardNumber);
                                        isCardExist = pgDB.CheckIfCardExist(newCardNumber);
                                    }
                                    while (isCardExist);

                                    Card card = new Card(
                                        newCardNumber,
                                        phoneNumber,
                                        tbFirstName.Text,
                                        tbMiddleName.Text,
                                        tbLastName.Text);

                                    pgDB.CreateCard(card);

                                    UI.PrintCardElements(ref tbResultForm, card);
                                    UI.PrintSuccess(cardsOperation);
                                }

                                break;
                        }

                        break;

                    #endregion СОЗДАНИЕ КАРТЫ

                    #region ПОИСК КАРТЫ/БАЛАНС на карте

                    case CardsOperation.Find:

                        operResult = BL.CheckSearchCompliance(
                            cbFindType.Text,
                            tbPhoneNumber.Text,
                            tbCardNumber.Text);

                        switch (operResult)
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

                                pgDB = CreatePostrgesInstance();
                                UI.PrintProcessing(ref tbResultForm);
                                switch (searchType)
                                {
                                    case SearchType.ByPhone:

                                        long.TryParse(
                                            tbPhoneNumber.Text,
                                            out long phoneNumber);

                                        bool isPhoneExist =
                                            pgDB.CheckIfPhone(phoneNumber);

                                        if (isPhoneExist)
                                        {
                                            Card card = pgDB.FindCardByPhone(phoneNumber);
                                            UI.PrintCardElements(ref tbResultForm, card);
                                            UI.PrintSuccess(cardsOperation);
                                        }
                                        else
                                        {
                                            UI.PrintErrorPhoneDoesntExist(
                                                ref tbResultForm,
                                                phoneNumber);
                                        }

                                        break;
                                    case SearchType.ByCard:

                                        SearchCard(pgDB);

                                        break;
                                }

                                break;
                        }

                        break;
                    #endregion ПОИСК КАРТЫ

                    #region ПРОСМОТР БАЛАНСА

                    case CardsOperation.SeeBalance:
                        operResult = BL.CheckKardCompliance(tbCardNumber.Text);

                        switch (operResult)
                        {
                            case ResultOperations.None:
                                pgDB = CreatePostrgesInstance();
                                UI.PrintProcessing(ref tbResultForm);
                                SearchCard(pgDB);

                                break;
                            case ResultOperations.WrongCard:
                                UI.ErrorWrongCard(ref tbResultForm);
                                break;
                            case ResultOperations.EmptyField:
                                UI.ErrorEptyFields(ref tbResultForm);
                                break;
                        }

                        break;

                    #endregion ПРОСМОТР БАЛАНСА

                    #region ИЗМЕНЕНИЕ БАЛАНСА

                    case CardsOperation.Change:

                        ResultOperations result =
                            BL.CheckChargeCompliance(
                                tbChargeSum.Text,
                                rbRemoveBonuses,
                                rbAddBonuses);

                        switch (result)
                        {
                            case ResultOperations.WrongSumm:

                                UI.PrintErrorProcessCard(
                                    ref tbResultForm,
                                    result);
                                break;

                            case ResultOperations.EmptyField:

                                UI.PrintErrorProcessCard(
                                    ref tbResultForm,
                                    result);
                                break;
                            case ResultOperations.NegativeDigit:

                                UI.PrintErrorProcessCard(
                                   ref tbResultForm,
                                   result);
                                break;
                            case ResultOperations.NotChangedWhatToDo:
                                UI.PrintErrorProcessCard(
                                  ref tbResultForm,
                                  result);
                                break;
                            case ResultOperations.None:

                                UI.PrintProcessing(ref tbResultForm);

                                int.TryParse(
                                    tbChargeSum.Text,
                                    out int changeSum);

                                pgDB = CreatePostrgesInstance();
                                int.TryParse(
                                    tbCardNumber.Text,
                                    out int cardnumber);

                                // проверка, существует ли в БД карта с
                                // таким номером.
                                if (pgDB.CheckIfCardExist(cardnumber))
                                {
                                    Card card = pgDB.FindCardByCard(cardnumber);

                                    switch (bonusOperations)
                                    {
                                        case BonusOperations.Add:

                                            operResult = pgDB.AddBonus(card, changeSum);

                                            switch (operResult)
                                            {
                                                case ResultOperations.None:

                                                    card = pgDB.FindCardByCard(cardnumber);
                                                    UI.PrintCardElements(
                                                        ref tbResultForm,
                                                        card);

                                                    UI.PrintSuccess(cardsOperation);
                                                    break;

                                                case ResultOperations.CardExpired:
                                                    UI.PrintErrorProcessCard(
                                                       ref tbResultForm,
                                                       operResult);
                                                    break;
                                            }

                                            // снова запрашиваем карту
                                            // для просмотра результатов спания.
                                            card = pgDB.FindCardByCard(cardnumber);
                                            UI.PrintCardElements(
                                                ref tbResultForm,
                                                card);

                                            break;
                                        case BonusOperations.Remove:
                                            operResult = pgDB.Charge(
                                               card,
                                               changeSum);

                                            switch (operResult)
                                            {
                                                case ResultOperations.ChargeError:

                                                    UI.PrintErrorCardDoesntExist(
                                                        ref tbResultForm,
                                                        searchType,
                                                        cardnumber);

                                                    break;
                                                case ResultOperations.CardExpired:

                                                    UI.PrintErrorProcessCard(
                                                        ref tbResultForm,
                                                        operResult);
                                                    break;

                                                case ResultOperations.None:

                                                    // снова запрашиваем карту
                                                    // для просмотра результатов спания.
                                                    card = pgDB.FindCardByCard(cardnumber);
                                                    UI.PrintCardElements(
                                                        ref tbResultForm,
                                                        card);

                                                    UI.PrintSuccess(cardsOperation);
                                                    break;
                                            }

                                            break;
                                    }
                                }
                                else
                                {
                                    UI.PrintErrorCardDoesntExist(
                                        ref tbResultForm,
                                        searchType,
                                        cardnumber);
                                }

                                break;
                        }

                        break;

                        #endregion ИЗМЕНЕНИЕ БАЛАНСА
                }
            }
        }

        private void SearchCard(PostgresDB pgDB)
        {
            int.TryParse(
                tbCardNumber.Text,
                out int cardNumber);

            // проверка, существует ли в БД карта с
            // таким номером.
            bool isCardExist =
                pgDB.CheckIfCardExist(cardNumber);

            if (isCardExist)
            {
                Card card = pgDB.FindCardByCard(cardNumber);
                UI.PrintCardElements(ref tbResultForm, card);
                UI.PrintSuccess(cardsOperation);
            }
            else
            {
                UI.PrintErrorCardDoesntExist(
                    ref tbResultForm,
                    searchType,
                    cardNumber);
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
                    bonusOperations = BonusOperations.None;
                    break;

                case FindCard:
                    cardsOperation = CardsOperation.Find;
                    cbFindType.Enabled = true;
                    gbSearch.Enabled = true;
                    gbCreation.Enabled = false;
                    gbCharge.Enabled = false;
                    tbFirstName.Enabled = false;
                    tbMiddleName.Enabled = false;
                    tbPhoneNumber.Enabled = true;
                    UI.DryItems(tbFirstName, Color.White);
                    UI.DryItems(tbMiddleName, Color.White);
                    UI.DryItems(tbLastName, Color.White);
                    UI.DryItems(tbCardNumber, Color.White);
                    UI.DryItems(tbChargeSum, Color.White);
                    UI.DryItems(cbFindType, markerColor);
                    UI.PrintMessageSearchingCard(ref tbResultForm);
                    bonusOperations = BonusOperations.None;

                    break;

                case Charge:
                    tbPhoneNumber.Enabled = false;
                    cardsOperation = CardsOperation.Change;
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

                case SeeBalance:
                    cardsOperation = CardsOperation.SeeBalance;
                    cbFindType.Enabled = false;
                    gbSearch.Enabled = true;
                    gbCreation.Enabled = false;
                    gbCharge.Enabled = false;
                    tbFirstName.Enabled = false;
                    tbMiddleName.Enabled = false;
                    tbPhoneNumber.Enabled = false;
                    tbCardNumber.Enabled = true;
                    UI.DryItems(tbFirstName, Color.White);
                    UI.DryItems(tbMiddleName, Color.White);
                    UI.DryItems(tbLastName, Color.White);
                    UI.DryItems(tbCardNumber, Color.White);
                    UI.DryItems(tbChargeSum, Color.White);
                    UI.DryItems(tbCardNumber, markerColor);
                    UI.PrintMessageEnterCard(ref tbResultForm);
                    bonusOperations = BonusOperations.None;

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
                    tbCardNumber.Text = string.Empty;
                    tbPhoneNumber.Enabled = true;
                    UI.DryItems(tbPhoneNumber, Color.FromArgb(214, 254, 216));
                    UI.DryItems(tbCardNumber, Color.White);
                    UI.PrintMessageEnterPhone(ref tbResultForm);
                    UI.DryItems(cbFindType, Color.White);
                    break;

                case SearchByCard:
                    searchType = SearchType.ByCard;
                    tbPhoneNumber.Enabled = false;
                    tbPhoneNumber.Text = string.Empty;
                    tbCardNumber.Enabled = true;
                    UI.DryItems(tbCardNumber, Color.FromArgb(214, 254, 216));
                    UI.DryItems(tbPhoneNumber, Color.White);
                    UI.PrintMessageEnterCard(ref tbResultForm);
                    UI.DryItems(cbFindType, Color.White);
                    break;
            }
        }

        private void RbRemoveBonuses_CheckedChanged(object sender, EventArgs e)
        {
            bonusOperations = BonusOperations.Remove;
        }

        private void RbAddBonuses_CheckedChanged(object sender, EventArgs e)
        {
            bonusOperations = BonusOperations.Add;
        }

        private void BtGetAllCards_Click(object sender, EventArgs e)
        {
            dataGridView.Refresh();
            DataTable data;
            PostgresDB pgDB = CreatePostrgesInstance();

            switch (pgDB.GetAllCards(out data))
            {
                case ResultOperations.None:
                    dataGridView.DataSource = data;
                    break;

                case ResultOperations.CannontConnectToDB:

                    UI.PrintErrorConnectionToDB(pgDB);
                    break;
            }
        }

        private void BtExpiredCards_Click(object sender, EventArgs e)
        {
            dataGridView.Refresh();

            PostgresDB pgDB = CreatePostrgesInstance();
            DataTable data = pgDB.GetExpiredCards();
            dataGridView.DataSource = data;
        }
    }
}
