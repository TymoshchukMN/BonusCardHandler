using CardsHandler.Enums;
using CardsHandler.Interfaces;
using CardsHandler.Server;

namespace CardsHandler.FabricElements
{
    public class CreateCard : IProcessCard
    {
        public void ProcessCard(
            FieldValues fieldsValues,
            out Card card,
            ServerInstance server)
        {
            string request = string.Format(
                $"{CardsOperation.Create};" +
                $"{fieldsValues.PhoneNumber};" +
                $"{fieldsValues.FirstName};" +
                $"{fieldsValues.MiddleName};" +
                $"{fieldsValues.LastName}");

            card = server.CreateCard(request);
        }
    }
}