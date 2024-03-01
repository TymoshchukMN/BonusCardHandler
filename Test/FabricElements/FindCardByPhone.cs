using System.Data;
using CardsHandler.Server;

namespace CardsHandler.FabricElements
{
    internal class FindCardByPhone
    {
        public static void ProcessCard(
            string request, ServerInstance server)
        {
            DataTable data = server.GetDatatable(request);
            UI.PrintCardsFindedByPhone(data);
        }
    }
}
