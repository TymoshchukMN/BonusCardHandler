using System.Data;
using CardsHandler.Enums;
using CardsHandler.Interfaces;
using CardsHandler.Server;

namespace CardsHandler.FabricElements
{
    public class CreateCard : IProcessCard
    {
        public void ProcessCard(
            FieldValues fieldsValues,
            out DataTable dataTable,
            ServerInstance server)
        {
            string request = string.Format(
                $"{CardsOperation.Create};" +
                $"{fieldsValues.PhoneNumber};" +
                $"{fieldsValues.FirstName};" +
                $"{fieldsValues.MiddleName};" +
                $"{fieldsValues.LastName}");

            dataTable = server.CreateCard(request);
        }
    }
}