using System;
using System.Data;
using CardsHandler.Enums;
using CardsHandler.FabricElements;
using CardsHandler.Interfaces;
using CardsHandler.Server;

namespace CardsHandler
{
    internal class Controller
    {
        private SearchType _searchType;
        private CardsOperation _cardsOperation;
        private BonusOperations _bonusOperations;

        #region PROPERTIES

        public SearchType SearchType
        {
            get
            {
                return _searchType;
            }

            set
            {
                _searchType = value;
            }
        }

        public CardsOperation CardOperation
        {
            get
            {
                return _cardsOperation;
            }

            set
            {
                _cardsOperation = value;
            }
        }

        public BonusOperations BonusOperations
        {
            get
            {
                return _bonusOperations;
            }

            set
            {
                _bonusOperations = value;
            }
        }

        #endregion PROPERTIES

        public DataTable Process(FieldValues fieldValues)
        {
            DataTable dataTable = null;
            ServerInstance server = new ServerInstance();
            ResultOperations operResult =
              Compliance.CheckCompliance(_cardsOperation, fieldValues, server);

            if (operResult == ResultOperations.None)
            {
                IProcessCard cardCommand = CommandFactory.GetClass(_cardsOperation);
                try
                {
                    cardCommand.ProcessCard(fieldValues, out dataTable, server);
                }
                catch (Exception ex)
                {
                    UI.PrintError(ex.Message);
                }
            }
            else
            {
                UI.PrintResultProcessCard(operResult);
            }

            return dataTable;
        }
    }
}
