﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
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

        public static void PrintMessageEnterCard(ref RichTextBox box)
        {
            const string EnterCardMessage = "Введите номер карты";
            box.Text = EnterCardMessage;
        }

        public static void PrintMessageEnterPhone(ref RichTextBox box)
        {
            const string EnterPhoneMessage = "Введите номер телефона";
            box.Text = EnterPhoneMessage;
        }

        public static void PrintMessageCreationCard(ref RichTextBox box)
        {
            const string EnterPhoneMessage = "Для создания карты введите номер телефона и ФИО";
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
    }
}
