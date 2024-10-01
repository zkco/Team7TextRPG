using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;

namespace Team7TextRPG.Scenes
{
    public class CreditScene : SceneBase
    {
        public override void Show()
        {
            Console.Clear();
            // 1. 기능
            // 2. 기능
            Console.WriteLine("엔딩 크레딧 미구현");
            SceneManager.Instance.LoadScene<TitleScene>();
        }

        protected override string SceneTypeToText<T>(T type)
        {
            return String.Empty;
        }
    }
}
