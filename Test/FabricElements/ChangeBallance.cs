using System.Data;
using CardsHandler.Enums;
using CardsHandler.Interfaces;
using CardsHandler.Server;

namespace CardsHandler.FabricElements
{
    public class ChangeBallance : IProcessCard
    {
        public void ProcessCard(
            FieldValues fields, out DataTable dataTable, ServerInstance server)
        {
            int.TryParse(fields.Summ, out int summ);
            int.TryParse(fields.CardNumber, out int cardNumber);
            string request = string.Format(
                     $"{CardsOperation.Change};" +
                     $"{fields.BonusOperations};" +
                     $"{cardNumber};{summ}");

            ResultOperations operations =
                server.ProcessCard(request, out dataTable);

            if (operations != ResultOperations.None)
            {
                UI.PrintResultProcessCard(operations);
            }
        }
    }
}