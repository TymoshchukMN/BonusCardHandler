using CardsHandler.Server;

namespace CardsHandler.Interfaces
{
    public interface IProcessCard
    {
        void ProcessCard(
            FieldValues fields,
            out Card card,
            ServerInstance server);
    }
}
