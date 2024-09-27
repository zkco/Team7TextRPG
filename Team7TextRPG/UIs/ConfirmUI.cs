using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public class ConfirmUI : UIBase
    {
        public override void Write()
        {
            // Confirm UI는 Write가 필요 없음
        }

        public bool Confirm(string message, Action? yesAction = null, Action? noAction = null)
        {
            Console.WriteLine(message);

            Console.WriteLine("1. 예");
            Console.WriteLine("2. 아니오");

            Defines.ConfirmType input = InputManager.Instance.GetInputType<Defines.ConfirmType>();
            switch (input)
            {
                case Defines.ConfirmType.Yes:
                    yesAction?.Invoke();
                    return true;
                case Defines.ConfirmType.No:
                    noAction?.Invoke();
                    return false;
            }

            return false;
        }
    }
}
