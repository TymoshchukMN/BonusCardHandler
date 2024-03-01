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
        /// Запрос ввода карты.
        /// </summary>
        /// <param name="box">окно вывода результата.</param>
        public static void PrintMessageEnterCard(ref RichTextBox box)
        {
            const string EnterCardMessage = "Введите номер карты";
            box.Text = EnterCardMessage;
        }

        /// <summary>
        /// Запрос ввода телефона.
        /// </summary>
        /// <param name="box">окно вывода результата.</param>
        public static void PrintMessageEnterPhone(ref RichTextBox box)
        {
            const string EnterPhoneMessage = "Введите номер телефона\nв формате 380XXXXXXXXXX";
            box.Text = EnterPhoneMessage;
        }

        /// <summary>
        /// Запрос данных для создания карты.
        /// </summary>
        /// <param name="box">окно вывода результата.</param>
        public static void PrintMessageCreationCard(ref RichTextBox box)
        {
            const string EnterPhoneMessage = "Для создания карты введите номер телефона и ФИО\n" +
                "!!!!! телефон указывать в формате 380XXXXXXXXXX";
            box.Text = EnterPhoneMessage;
        }

        /// <summary>
        /// Запрос критериев поиска карты.
        /// </summary>
        /// <param name="box">окно вывода результата.</param>
        public static void PrintMessageSearchingCard(ref RichTextBox box)
        {
            const string EnterPhoneMessage = "Выберите критерий поиска карты";
            box.Text = EnterPhoneMessage;
        }

        /// <summary>
        /// Запрос ввода суммы для списания..
        /// </summary>
        /// <param name="box">окно вывода результата.</param>
        public static void PrintMessageCharhingCard(ref RichTextBox box)
        {
            const string EnterPhoneMessage = "Укажите сумму для списания бонусов и номер карты";
            box.Text = EnterPhoneMessage;
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
        public static void PrintErrorProcessCard(ResultOperations result)
        {
            string message = string.Empty;
            switch (result)
            {
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

            const string CAPTION = "Ошибка";
            MessageBox.Show(
                  message,
                  CAPTION,
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

        public static void PrintCardsFindedByPhone(DataTable datatable)
        {
            StringBuilder stringBuilder = new StringBuilder();
            const int CardNumIndex = 0;
            const int ExpirationDateOndex = 1;
            const int BallanceIndex = 2;
            const int FirstNameIndex = 3;
            const int MiddleNameIndex = 4;
            const int LastNameIndex = 5;
            const int PhoneNumberIndex = 6;

            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                stringBuilder.Append($"Номер карты:\t{datatable.Rows[i].ItemArray[CardNumIndex].ToString()}\n");
                stringBuilder.Append($"Активна до:\t{datatable.Rows[i].ItemArray[ExpirationDateOndex].ToString()}\n");
                stringBuilder.Append($"Баланс:\t\t{datatable.Rows[i].ItemArray[BallanceIndex].ToString()}\n");
                stringBuilder.Append($"Имя:\t\t{datatable.Rows[i].ItemArray[FirstNameIndex].ToString()}\n");
                stringBuilder.Append($"Отчество:\t{datatable.Rows[i].ItemArray[MiddleNameIndex].ToString()}\n");
                stringBuilder.Append($"Фамилия:\t{datatable.Rows[i].ItemArray[LastNameIndex].ToString()}\n");
                stringBuilder.Append($"Телефон:\t{datatable.Rows[i].ItemArray[PhoneNumberIndex].ToString()}\n");
                stringBuilder.Append($"{new string('=',30)}\n");
            }

            FormHandlerCards.Instance.Controls[0].Controls[0].Controls["tbResultForm"].Text =
                stringBuilder.ToString();
        }
    }
}
