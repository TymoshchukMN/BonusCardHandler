using CardsHandler.Enums;
using CardsHandler.Interfaces;

namespace CardsHandler.FabricElements
{
    /// <summary>
    /// Класс для возвражения объекта в соостветствии с типом операции.
    /// </summary>
    internal class CommandFactory
    {
        public static IProcessCard GetClass(CardsOperation operation)
        {
            switch (operation)
            {
                case CardsOperation.Create:

                    return new CreateCard();

                case CardsOperation.Find:

                    return new FindCard();

                case CardsOperation.Change:

                    return new ChangeBallance();

                case CardsOperation.SeeBalance:

                    return new SeeBalance();
            }

            return null;
        }
    }
}
