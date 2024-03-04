using System.Data;
using CardsHandler.Enums;
using CardsHandler.Interfaces;
using CardsHandler.Server;

namespace CardsHandler.FabricElements
{
    public class FindCard : IProcessCard
    {
        public void ProcessCard(FieldValues fields, out DataTable dataTable, ServerInstance server)
        {
            string request = string.Empty;
            dataTable = null;

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
                ResultOperations operations =
                    server.ProcessCard(request, out dataTable);

                if (operations != ResultOperations.None)
                {
                    UI.PrintResultProcessCard(operations);
                }
            }
        }
    }
}