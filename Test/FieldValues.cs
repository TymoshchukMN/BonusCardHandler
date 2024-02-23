using CardsHandler.Enums;

namespace CardsHandler
{
    public struct FieldValues
    {
        private string _firstName;

        private string _middleName;

        private string _lastName;

        private string _phoneNumber;

        private string _cardNumber;

        private string _summ;

        private SearchType _searchType;

        private BonusOperations _bonusOperations;

        public BonusOperations BonusOperations
        {
            get { return _bonusOperations; }
            set { _bonusOperations = value; }
        }

        public SearchType SearchType
        {
            get { return _searchType; }
            set { _searchType = value; }
        }

        public string Summ
        {
            get { return _summ; }
            set { _summ = value; }
        }

        public string CardNumber
        {
            get { return _cardNumber; }
            set { _cardNumber = value; }
        }

        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set { _phoneNumber = value; }
        }

        public string LastName
        {
            get { return _lastName; }
            set { _lastName = value; }
        }

        public string MiddleName
        {
            get { return _middleName; }
            set { _middleName = value; }
        }

        public string FirstName
        {
            get { return _firstName; }
            set { _firstName = value; }
        }
    }
}
