using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CardsHandler
{
    internal class BL
    {
        /// <summary>
        /// Метод для проверки валидности введенных данных на создание карты.
        /// </summary>
        /// <param name="phone">Номер телефона.</param>
        /// <param name="firstName"> Имя.</param>
        /// <param name="middleName">Отчество.</param>
        /// <param name="lastName">Фамилия.</param>
        /// <returns>истина/ложь.</returns>
        public static WrongData CheckCreationCompliance(
            TextBox phone,
            TextBox firstName,
            TextBox middleName,
            TextBox lastName)
        {
            WrongData isCompliante = WrongData.None;

            if (IsPhoneCorrect(phone.Text))
            {
                if (IsNameCorrect(firstName.Text))
                {
                    if (IsNameCorrect(middleName.Text))
                    {
                        if (!IsNameCorrect(lastName.Text))
                        {
                            return WrongData.WrongName;
                        }
                    }
                    else
                    {
                        return WrongData.WrongName;
                    }
                }
                else
                {
                    return WrongData.WrongName;
                }
            }
            else
            {
                return WrongData.WrongPhone;
            }

            return isCompliante;
        }

        private static bool IsPhoneCorrect(string phoneNumber)
        {
            bool isCorrect = true;

            // стандартная длина номера телефона.
            // например, 380930453119
            const ushort StandartLenth = 12;

            if (phoneNumber.Length == StandartLenth)
            {
                for (ushort i = 0; i < phoneNumber.Length; ++i)
                {
                    if (!char.IsDigit(phoneNumber[i]))
                    {
                        isCorrect = false;
                    }
                }
            }

            return isCorrect;
        }

        private static bool IsNameCorrect(string name)
        {
            bool isCorrect = true;

            for (ushort i = 0; i < name.Length; ++i)
            {
                if (!char.IsLetter(name[i]))
                {
                    isCorrect = false;
                    return isCorrect;
                }
            }

            return isCorrect;
        }
    }
}
