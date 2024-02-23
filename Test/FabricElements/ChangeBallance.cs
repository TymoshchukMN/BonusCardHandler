using CardsHandler.Enums;
using CardsHandler.Interfaces;
using CardsHandler.Server;

namespace CardsHandler.FabricElements
{
    public class ChangeBallance : IProcessCard
    {
        public void ProcessCard(
            FieldValues fields,
            out Card card,
            ServerInstance server)
        {
            int.TryParse(fields.Summ, out int summ);
            int.TryParse(fields.CardNumber, out int cardNumber);
            string request = string.Format(
                     $"{CardsOperation.Change};" +
                     $"{fields.BonusOperations};" +
                     $"{cardNumber};{summ}");

            ResultOperations operations =
                server.ProcessCard(request, out card);

            if (operations != ResultOperations.None)
            {
                UI.PrintErrorProcessCard(operations);
            }
        }
    }
}