using CardsHandler.Enums;
using CardsHandler.Interfaces;
using CardsHandler.Server;

namespace CardsHandler.FabricElements
{
    public class FindCard : IProcessCard
    {
        public void ProcessCard(
            FieldValues fields,
            out Card card,
            ServerInstance server)
        {
            card = null;
            string request = string.Empty;
            switch (fields.SearchType)
            {
                case SearchType.ByPhone:

                    request = string.Format(
                        $"{CardsOperation.Find};" +
                        $"{fields.SearchType};" +
                        $"{fields.PhoneNumber};");

                    break;
                case SearchType.ByCard:

                    request = string.Format(
                       $"{CardsOperation.Find};" +
                       $"{fields.SearchType};" +
                       $"{fields.CardNumber};");

                    break;
            }

            if (!string.IsNullOrEmpty(request))
            {
                server.ProcessCard(request, out card);
            }
        }
    }
}