using CardsHandler.Database;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public static void PrintErrorConnectionToDB(PostgresDB pgInstance)
        {
            string message =
                       $"Не удается подключиться к БД {pgInstance.Server}\n" +
                       $"база {pgInstance.DBname}";
            const string CAPTION = "Attention";

            MessageBox.Show(
                  message,
                  CAPTION,
                  MessageBoxButtons.OK,
                  MessageBoxIcon.Error);
        }

        public static void PrintMessageEnterCard(ref RichTextBox box)
        {
            const string EnterCardMessage = "Введите номер карты";
            box.Text = EnterCardMessage;
        }

        public static void PrintMessageEnterPhone(ref RichTextBox box)
        {
            const string EnterPhoneMessage = "Введите номер телефона\nв формате 380XXXXXXXXXX";
            box.Text = EnterPhoneMessage;
        }

        public static void PrintMessageCreationCard(ref RichTextBox box)
        {
            const string EnterPhoneMessage = "Для создания карты введите номер телефона и ФИО\n" +
                "!!!!! телефон указывать в формате 380XXXXXXXXXX";
            box.Text = EnterPhoneMessage;
        }

        public static void PrintMessageSearchingCard(ref RichTextBox box)
        {
            const string EnterPhoneMessage = "Выберите критерий поиска карты";
            box.Text = EnterPhoneMessage;
        }

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

        public static void ErrorInPhoneNumber(ref RichTextBox box)
        {
            const string MessageErrorInNUmber = "Ошибка в номере телефона.\n" +
                "формат ввода 380XXXXXXXXXX";

            box.Text = MessageErrorInNUmber;
        }

        public static void ErrorWrongName(ref RichTextBox box)
        {
            const string MessageErrorInNUmber = "Не правильное указано ФИО пользователя." +
                "Должны быть только символы";
            box.Text = MessageErrorInNUmber;
        }

        public static void ErrorWrongCard(ref RichTextBox box)
        {
            const string MessageErrorInNUmber = "Не правильно указан номер карты. ";
            box.Text = MessageErrorInNUmber;
        }

        public static void ErrorEptyFields(ref RichTextBox box)
        {
            const string MessageErrorInNUmber = "ОШИБКА. Не все поля заполнены";
            box.Text = MessageErrorInNUmber;
        }

        public static void PrintSuccess(ref RichTextBox box)
        {
            const string MessageSuccess = "УСПЕХ";
            box.Text = MessageSuccess;
        }
    }
}
