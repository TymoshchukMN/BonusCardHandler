///////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 26/01/202
// Last Modified On :
// Description: Messages for user.
// Project: CardsHandler
//////////////////////////////

using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace CardsHandler
{
    internal class UI
    {
        /// <summary>
        /// печать об ошибке, не выбрана операция над картой.
        /// </summary>
        public static void PrintErrorChoosedOperation()
        {
            const string MESSAGE =
                       "Не выбрана операция над картой";
            const string CAPTION = "Attention";

            MessageBox.Show(
                  MESSAGE,
                  CAPTION,
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Подсвечиваем поле для ввода.
        /// </summary>
        /// <param name="box">
        /// Поле для окраса.
        /// </param>
        /// <param name="color">
        /// Цвет заливки.
        /// </param>
        public static void DryItems(TextBox box, Color color)
        {
            box.BackColor = color;
        }

        /// <summary>
        /// Подсвечиваем поле для ввода.
        /// </summary>
        /// <param name="box">
        /// Поле для окраса.
        /// </param>
        /// <param name="color">
        /// Цвет заливки.
        /// </param>
        public static void DryItems(ComboBox box, Color color)
        {
            box.BackColor = color;
        }

        /// <summary>
        /// Печать ошибки о не верной сумме к списанию.
        /// </summary>
        /// <param name="result">Результат операции.</param>
        public static void PrintResultProcessCard(ResultOperations result)
        {
            string message = string.Empty;
            string caption = "Ошибка";
            switch (result)
            {
                case ResultOperations.None:
                    message = "Операция выполнена успешно.";
                    caption = "Инфо.";
                    break;
                case ResultOperations.WrongName:
                    message = "Не верно указан ФИО.";
                    break;
                case ResultOperations.WrongPhone:
                    message = "Не верно указан номер телефона.";
                    break;
                case ResultOperations.WrongSumm:
                    message = "Не венрно указана сумма к списанию.";
                    break;
                case ResultOperations.EmptyField:
                    message = "Заполнены не все поля.";
                    break;
                case ResultOperations.ChargeError:
                    message = "Нельзя списать такое количество бонусов.";
                    break;
                case ResultOperations.NegativeDigit:
                    message = "Введено отрицательное число";
                    break;
                case ResultOperations.CardExpired:
                    message = "Списание/зачисление не возможно. " +
                        "Срок действия карты истек.";
                    break;
                case ResultOperations.NotChangedWhatToDo:
                    message = "Не указано что сделать с картой";
                    break;
                case ResultOperations.CardDoesnExist:
                    message = "Карта с таким нномером не существует";
                    break;
                case ResultOperations.WrongCard:
                    message = "не верно указан номер карты";
                    break;
                case ResultOperations.PhoneDoesnEsixt:
                    message = "Карты с таким номером телефона не найдены.";
                    break;
            }

            MessageBox.Show(
                  message,
                  caption,
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
        }

        public static void PrintError(string message)
        {
            const string CAPTION = "Ошибка";
            MessageBox.Show(
                      message,
                      CAPTION,
                      MessageBoxButtons.OK,
                      MessageBoxIcon.Error);
        }
    }
}
