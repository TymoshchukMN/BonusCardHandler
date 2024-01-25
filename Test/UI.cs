using System;
using System.Collections.Generic;
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
    }
}
