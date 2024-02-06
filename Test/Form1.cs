using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CardsHandler.Database;
using CardsHandler.Enums;
using CardsHandler.Interfaces;
using CardsHandler.JSON;
using CardsHandler.Server;

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
        private static FormHandlerCars _instance;
        private readonly Color markerColor = Color.FromArgb(214, 254, 216);
        private SearchType searchType;
        private CardsOperation cardsOperation;
        private BonusOperations bonusOperations;

        //
        // !!!!!!!ветка[serverJson] создана от[develope]>, мерджить в[develope]>
        // 

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

        public static FormHandlerCars GetInstance()
        {
            if (_instance == null)
            {
                _instance = new FormHandlerCars();
            }

            return _instance;
        }

        private static PostgresDB CreatePostrgesInstance()
        {
            DBConfigJSON dBConfig = BL.GetDBConfig();

            PostgresDB pgDB = PostgresDB.GetInstance(
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

                IProcessCardsDB pgDB;
                SrvConfig srvConfig = BL.GetServerConfig();
                ServerInstance server = new ServerInstance(srvConfig.Server, srvConfig.Port);
                //ServerInstance server = new ServerInstance();

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

                                string request = string.Format(
                                    $"{cardsOperation};" +
                                    $"{tbPhoneNumber.Text};" +
                                    $"{tbFirstName.Text};" +
                                    $"{tbMiddleName.Text};" +
                                    $"{tbLastName.Text}");

                                Card card = server.CreateCard(request);

                                UI.PrintCardElements(ref tbResultForm, card);
                                UI.PrintSuccess(cardsOperation);

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

                                        string request = string.Format(
                                          $"{cardsOperation};" +
                                          $"{searchType};" +
                                          $"{tbPhoneNumber.Text};");

                                        operResult = server.ProcessCard(request, out Card card);
;
                                        switch (operResult)
                                        {
                                            case ResultOperations.None:
                                                UI.PrintCardElements(ref tbResultForm, card);
                                                UI.PrintSuccess(cardsOperation);

                                                break;
                                            case ResultOperations.PhoneDoesnEsixt:
                                                UI.PrintErrorPhoneDoesntExist(
                                                    ref tbResultForm,
                                                    tbPhoneNumber.Text);

                                                break;
                                        }

                                        break;

                                    case SearchType.ByCard:
                                        int.TryParse(
                                           tbCardNumber.Text,
                                           out int cardNumber);

                                        request = string.Format(
                                            $"{cardsOperation};" +
                                            $"{searchType};" +
                                            $"{cardNumber};");

                                        operResult = server.ProcessCard(request, out card);
                                        switch (operResult)
                                        {
                                            case ResultOperations.None:
                                                UI.PrintCardElements(ref tbResultForm, card);
                                                UI.PrintSuccess(cardsOperation);
                                                break;
                                            case ResultOperations.CardDoesnExist:

                                                UI.PrintErrorCardDoesntExist(
                                                   ref tbResultForm,
                                                   searchType,
                                                   cardNumber);

                                                break;
                                        }

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

                                int.TryParse(tbCardNumber.Text, out int cardNumber);

                                string request = string.Format(
                                    $"{cardsOperation};{cardNumber}");

                                operResult = server.ProcessCard(request, out Card card);
                                switch (operResult)
                                {
                                    case ResultOperations.None:
                                        UI.PrintCardElements(ref tbResultForm, card);
                                        UI.PrintSuccess(cardsOperation);
                                        break;
                                    case ResultOperations.CardDoesnExist:

                                        UI.PrintErrorProcessCard(
                                           ref tbResultForm,
                                           operResult);

                                        break;
                                }

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
                                    out int summ);

                                if (rbAddBonuses.Checked)
                                {
                                    bonusOperations = BonusOperations.Add;
                                }
                                else
                                {
                                    if (rbRemoveBonuses.Checked)
                                    {
                                        bonusOperations = BonusOperations.Remove;
                                    }
                                }

                                switch (result)
                                {
                                    case ResultOperations.None:

                                        switch (bonusOperations)
                                        {
                                            case BonusOperations.Add:

                                                int.TryParse(tbCardNumber.Text, out int cardNumber);

                                                string request = string.Format(
                                                    $"{cardsOperation};{bonusOperations};{cardNumber};{summ}");

                                                operResult = server.ProcessCard(request, out Card card);

                                                switch (operResult)
                                                {
                                                    case ResultOperations.None:

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
                                                    case ResultOperations.CardDoesnExist:
                                                        UI.PrintErrorProcessCard(
                                                            ref tbResultForm,
                                                            operResult);

                                                        break;
                                                }

                                                break;

                                            case BonusOperations.Remove:
                                                int.TryParse(tbCardNumber.Text, out cardNumber);


                                                request = string.Format(
                                                    $"{cardsOperation};{bonusOperations};{cardNumber};{summ}");

                                                operResult = server.ProcessCard(request, out card);

                                                switch (operResult)
                                                {
                                                    case ResultOperations.ChargeError:

                                                        UI.PrintErrorProcessCard(
                                                            ref tbResultForm,
                                                            operResult);

                                                        break;
                                                    case ResultOperations.CardExpired:

                                                        UI.PrintErrorProcessCard(
                                                            ref tbResultForm,
                                                            operResult);
                                                        break;

                                                    case ResultOperations.None:

                                                        UI.PrintCardElements(
                                                            ref tbResultForm,
                                                            card);
                                                        UI.PrintSuccess(cardsOperation);
                                                        break;
                                                }

                                                break;
                                        }

                                        break;

                                    case ResultOperations.CardDoesnExist:

                                        UI.PrintErrorProcessCard(
                                            ref tbResultForm,
                                            result);

                                        break;
                                }

                                break;
                        }

                        break;

                        #endregion ИЗМЕНЕНИЕ БАЛАНСА
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
            PostgresDB pgDB = CreatePostrgesInstance();

            switch (pgDB.GetAllCards(out DataTable data))
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

            switch (pgDB.GetExpiredCards(out DataTable data))
            {
                case ResultOperations.None:
                    dataGridView.DataSource = data;
                    break;

                case ResultOperations.CannontConnectToDB:

                    UI.PrintErrorConnectionToDB(pgDB);
                    break;
            }

            dataGridView.DataSource = data;
        }
    }
}
