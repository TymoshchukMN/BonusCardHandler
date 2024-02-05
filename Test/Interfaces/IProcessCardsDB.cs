using System.Data;

namespace CardsHandler.Interfaces
{
    internal interface IProcessCardsDB
    {
        bool CheckIfCardExist(int cardNumber);

        bool CheckIfPhone(string phoneNumber);

        void CreateCard(Card card);

        Card FindCardByPhone(string number);

        Card FindCardByCard(int number);

        ResultOperations Charge(Card card, int summ);

        ResultOperations AddBonus(Card card, int summ);

        ResultOperations GetAllCards(out DataTable dataTable);

        ResultOperations GetExpiredCards(out DataTable dataTable);
    }
}
