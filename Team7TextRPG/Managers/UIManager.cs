using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.UIs;

namespace Team7TextRPG.Managers
{
    public class UIManager
    {
        private static UIManager? _instance;
        public static UIManager Instance => _instance ??= new UIManager();

        private ConfirmUI? _confirmUI;

        public void Write<T>() where T : UIBase, new()
        {
            new T().Write();
        }

        public bool Confirm(string message, Action? onYes = null, Action? onNo = null)
        {
            return (_confirmUI ??= new ConfirmUI()).Confirm(message, onYes, onNo);
        }
    }
}
