using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Scenes;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

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
        public void ShopWrite(Defines.ShopType type)
        {
            ShopUI shopUI = new ShopUI(type);
            shopUI.Write();
        }

        public bool Confirm(string message, Action? onYes = null, Action? onNo = null)
        {
            return (_confirmUI ??= new ConfirmUI()).Confirm(message, onYes, onNo);
        }
    }
}
