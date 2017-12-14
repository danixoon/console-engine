using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleEngine.Editor
{
    public static class GameEditor
    {
        public static void Init()
        {
            Application.EnableVisualStyles();
            Application.Run(new GameEditorForm()); // Запускаем формочку редактора
        }
    }
}
