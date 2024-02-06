///////////////////////////////
// Author : Tymoshchuk Maksym
// Created On : 26/01/202
// Last Modified On :
// Description: Messages for user.
// Project: CardsHandler
//////////////////////////////

using System.Drawing;
using System.Windows.Forms;
using CardsHandler.Database;
using CardsHandler.Enums;

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
        /// Вывод ошибки подключения к БД.
        /// </summary>
        /// <param name="pgInstance">Экземпляр объекта БД.</param>
        public static void PrintErrorConnectionToDB(PostgresDB pgInstance)
        {
            string message =
                       $"Не удается подключиться к БД {pgInstance.Server}\n" +
                       $"база {pgInstance.DBname}\n" +
                       $"порт:{pgInstance.Port}";
            const string CAPTION = "Attention";

            MessageBox.Show(
                  message,
                  CAPTION,
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
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
        /// Ошибка введен не правильный номер.
        /// </summary>
        /// <param name="box">окно вывода результата.</param>
        public static void ErrorInPhoneNumber(ref RichTextBox box)
        {
            const string MessageErrorInNUmber = "Ошибка в номере телефона.\n" +
                "формат ввода 380XXXXXXXXXX";

            box.Text = MessageErrorInNUmber;
        }

        /// <summary>
        /// Ошибка ввода имени.
        /// </summary>
        /// <param name="box">окно вывода результата.</param>
        public static void ErrorWrongName(ref RichTextBox box)
        {
            const string MessageErrorInNUmber = "Не правильное указано ФИО пользователя." +
                "Должны быть только БУКВЫ";
            box.Text = MessageErrorInNUmber;
        }

        /// <summary>
        /// Ошибка ввода карты.
        /// </summary>
        /// <param name="box">окно вывода результата.</param>
        public static void ErrorWrongCard(ref RichTextBox box)
        {
            const string MessageErrorInNUmber = "Не правильно указан номер карты. ";
            box.Text = MessageErrorInNUmber;
        }

        /// <summary>
        /// Ошибка ввода полей. Указано пустое поле.
        /// </summary>
        /// <param name="box">окно вывода результата.</param>
        public static void ErrorEptyFields(ref RichTextBox box)
        {
            const string MessageErrorInNUmber = "ОШИБКА. Не все поля заполнены.\n" +
                "Нужное поле подсвечено зеленым цветом.";
            box.Text = MessageErrorInNUmber;
        }

        public static void PrintSuccess(ref RichTextBox box)
        {
            const string MessageSuccess = "УСПЕХ";
            box.Text = MessageSuccess;
        }

        /// <summary>
        /// Вывод ошибки об отсутствии карты в базе.
        /// </summary>
        /// <param name="box">Окно вывода результата.</param>
        /// <param name="searchType">тип поиска.</param>
        /// <param name="number">номер карты или телефона.</param>
        public static void PrintErrorCardDoesntExist(
            ref RichTextBox box,
            SearchType searchType,
            int number)
        {
            string messageSuccess = string.Empty;
            switch (searchType)
            {
                case SearchType.ByPhone:
                    messageSuccess = $"В базе нет карты привязанных к номеру {number}.\n" +
                        $"Проверьте правильность ввода";
                    break;
                case SearchType.ByCard:
                    messageSuccess = $"В базе нет карты с номером {number}.\n" +
                      $"Проверьте правильность ввода";
                    break;
            }

            box.Text = messageSuccess;
        }

        /// <summary>
        /// Печать ошибки "Телефон не существует".
        /// </summary>
        /// <param name="box">окно вывода результата.</param>
        /// <param name="number">телефон.</param>
        public static void PrintErrorPhoneDoesntExist(
            ref RichTextBox box,
            string number)
        {
            string message = $"В базе нет клиентов с номером телефона " +
                $"{number}";
            box.Text = message;
        }

        /// <summary>
        /// Вывод информации о карте.
        /// </summary>
        /// <param name="box">Окнов вывода.</param>
        /// <param name="card">Карта.</param>
        public static void PrintCardElements(ref RichTextBox box, Card card)
        {
            string message = $"Номер карты:\t{card.Cardnumber}\n" +
                $"Баланс:\t\t{card.Ballance}\n" +
                $"Истекает:\t{card.ExpirationDate.ToShortDateString()}\n" +
                $"Владелец:\t{card.LastName} {card.FirstName} {card.MiddleName}\n" +
                $"Номер телефона:\t{card.PhoneNumber}\n";

            box.Text = message;
        }

        /// <summary>
        /// Печать ошибки о не верной сумме к списанию.
        /// </summary>
        /// <param name="box">окно для вывода результата.</param>
        /// <param name="result">Результат операции.</param>
        public static void PrintErrorProcessCard(
            ref RichTextBox box,
            ResultOperations result)
        {
            string message = string.Empty;
            switch (result)
            {
                case ResultOperations.WrongSumm:
                    message = "Не венрно указана сумма к списанию.";
                    break;
                case ResultOperations.EmptyField:
                    message = "Не указана сумма к списанию.";
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
                    message = "Карта не существует";
                    break;
            }

            const string CAPTION = "Ошибка";
            MessageBox.Show(
                  message,
                  CAPTION,
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);

            box.Text = message;
        }

        /// <summary>
        /// Печать сообщения о выполнении обработки.
        /// </summary>
        /// <param name="box">
        /// Окно для вывода.
        /// </param>
        public static void PrintProcessing(ref RichTextBox box)
        {
            string message = "Обработка....";
            box.Text = message;
        }

        /// <summary>
        /// Вывод окна об успешном выполнении перации.
        /// </summary>
        /// <param name="operation">
        /// Тип операции с картой.
        /// </param>
        public static void PrintSuccess(CardsOperation operation)
        {
            string message = string.Empty;

            switch (operation)
            {
                case CardsOperation.Create:
                    message = "Карта создана успешно.";
                    break;
                case CardsOperation.Find:
                    message = "Карта найдена.";
                    break;
                case CardsOperation.Change:
                    message = "Балан бонусов изменен.";
                    break;
                case CardsOperation.SeeBalance:
                    message = "Карта найдена. Баланс в окне вывода.";
                    break;
            }

            const string CAPTION = "Информация";

            MessageBox.Show(
                  message,
                  CAPTION,
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Information);
        }

        /// <summary>
        /// Вывод ошибки, что уже существует карта с таки номером тебефона.
        /// </summary>
        public static void PrintErrorPhoneExist()
        {
            const string Message = "В базе уже есть карта " +
                "с таким номером телефона";
            const string CAPTION = "Ошибка";

            MessageBox.Show(
                  Message,
                  CAPTION,
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Вывод ошибки, что уже существует карта с таки номером.
        /// </summary>
        /// <param name="str">Строка с ошибкой. </param>
        public static void PrintErrorAdintCard(string str)
        {
            const string CAPTION = "Ошибка";

            MessageBox.Show(
                  str,
                  CAPTION,
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Warning);
        }
    }
}
