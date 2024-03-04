using System.Data;
using CardsHandler.Server;

namespace CardsHandler.Interfaces
{
    public interface IProcessCard
    {
        void ProcessCard(
            FieldValues fields,
            out DataTable dataTable,
            ServerInstance server);
    }
}
