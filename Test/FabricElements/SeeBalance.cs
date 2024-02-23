using CardsHandler.Enums;
using CardsHandler.Interfaces;
using CardsHandler.Server;
using Newtonsoft.Json;

namespace CardsHandler.FabricElements
{
    public class SeeBalance : IProcessCard
    {
        public void ProcessCard(
            FieldValues fields,
            out Card card,
            ServerInstance server)
        {
            int.TryParse(fields.CardNumber, out int cardNumber);

            string request = string.Format(
                $"{CardsOperation.SeeBalance};{SearchType.ByCard};{cardNumber}");

            server.ProcessCard(request, out card);
        }
    }
}