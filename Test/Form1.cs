using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using CardsHandler.Enums;
using CardsHandler.Server;

namespace CardsHandler
{
    public partial class FormHandlerCards : Form
    {
        private const string CreateCard = "Создать";
        private const string FindCard = "Найти";
        private const string Charge = "Изменить бонусы";
        private const string SeeBalance = "Помотреть баланс";
        private const string SearchByPhone = "Телефону";
        private const string SearchByCard = "Номеру карты";

        private readonly Color markerColor = Color.FromArgb(214, 254, 216);
        private SearchType _searchType;
        private FieldValues _fieldValues;

        private Controller _controller;

        public FormHandlerCards()
        {
            InitializeComponent();
            tbPhoneNumber.Enabled = false;
            tbCardNumber.Enabled = false;
            cbFindType.Enabled = false;
            tbChargeSum.Enabled = false;
            tbFirstName.Enabled = false;
            tbMiddleName.Enabled = false;
            tbLastName.Enabled = false;
            _controller = new Controller();
            _fieldValues = default(FieldValues);
        }

        private void BtProcess_Click(object sender, EventArgs e)
        {
            FillFieldValues();

            if (string.IsNullOrEmpty(cbOperations.Text))
            {
                UI.PrintErrorChoosedOperation();
            }
            else
            {
                Card card = _controller.Process(_fieldValues);
                if (card != null)
                {
                    FillResultTextBox(card);
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
                    _controller.CardOperation = CardsOperation.Create;
                    _searchType = SearchType.None;
                    cbFindType.Enabled = false;
                    gbCreation.Enabled = true;
                    gbSearch.Enabled = false;
                    gbCharge.Enabled = false;
                    tbFirstName.Enabled = true;
                    tbMiddleName.Enabled = true;
                    tbLastName.Enabled = true;
                    tbPhoneNumber.Enabled = true;
                    tbCardNumber.Text = string.Empty;
                    tbCardNumber.Enabled = false;
                    UI.PrintMessageCreationCard(ref tbResultForm);
                    UI.DryItems(tbFirstName, markerColor);
                    UI.DryItems(tbMiddleName, markerColor);
                    UI.DryItems(tbLastName, markerColor);
                    UI.DryItems(tbPhoneNumber, markerColor);
                    break;

                case FindCard:
                    _controller.CardOperation = CardsOperation.Find;
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

                    break;

                case Charge:
                    rbAddBonuses.Checked = true;
                    tbPhoneNumber.Enabled = false;
                    _controller.CardOperation = CardsOperation.Change;
                    _searchType = SearchType.None;
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
                    _controller.CardOperation = CardsOperation.SeeBalance;
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
                    _controller.BonusOperations = BonusOperations.None;

                    break;
            }
        }

        private void CbFindType_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (cbFindType.Text)
            {
                case SearchByPhone:
                    _controller.SearchType = SearchType.ByPhone;
                    _searchType = SearchType.ByPhone;
                    tbCardNumber.Enabled = false;
                    tbCardNumber.Text = string.Empty;
                    tbPhoneNumber.Enabled = true;
                    UI.DryItems(tbPhoneNumber, Color.FromArgb(214, 254, 216));
                    UI.DryItems(tbCardNumber, Color.White);
                    UI.PrintMessageEnterPhone(ref tbResultForm);
                    UI.DryItems(cbFindType, Color.White);
                    break;

                case SearchByCard:
                    _controller.SearchType = SearchType.ByCard;
                    _searchType = SearchType.ByCard;
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
            _controller.BonusOperations = BonusOperations.Remove;
            _fieldValues.BonusOperations = BonusOperations.Remove;
        }

        private void RbAddBonuses_CheckedChanged(object sender, EventArgs e)
        {
            _controller.BonusOperations = BonusOperations.Add;
            _fieldValues.BonusOperations = BonusOperations.Add;
        }

        private void BtGetAllCards_Click(object sender, EventArgs e)
        {
            dataGridView.Refresh();

            string request = string.Format(
                                   $"{CardsOperation.GetAllCards};");

            SrvConfig srvConfig = BL.GetServerConfig();
            ServerInstance server = new ServerInstance(
                srvConfig.Server,
                srvConfig.Port);

            DataTable data = server.GetDatatable(request);
            dataGridView.DataSource = data;
        }

        /// <summary>
        /// Заполнение структуры, содержащей значениея полей формы.
        /// </summary>
        private void FillFieldValues()
        {
            _fieldValues.FirstName = tbFirstName.Text;
            _fieldValues.MiddleName = tbMiddleName.Text;
            _fieldValues.LastName = tbLastName.Text;
            _fieldValues.PhoneNumber = tbPhoneNumber.Text;
            _fieldValues.CardNumber = tbCardNumber.Text;
            _fieldValues.Summ = tbChargeSum.Text;
            _fieldValues.SearchType = _searchType;
        }

        /// <summary>
        /// Заполнение окна резульата.
        /// </summary>
        /// <param name="card">Полученная Карта.</param>
        private void FillResultTextBox(Card card)
        {
            string message = $"Номер карты:\t{card.Cardnumber}\n" +
               $"Баланс:\t\t{card.Ballance}\n" +
               $"Истекает:\t{card.ExpirationDate.ToShortDateString()}\n" +
               $"Владелец:\t{card.LastName} {card.FirstName} {card.MiddleName}\n" +
               $"Номер телефона:\t{card.PhoneNumber}\n";

            tbResultForm.Text = message;
        }
    }
}
