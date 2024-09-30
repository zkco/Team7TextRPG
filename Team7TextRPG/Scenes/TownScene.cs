using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.UIs;

namespace Team7TextRPG.Scenes
{
    public class TownScene : SceneBase
    {
        public enum TownSceneType
        {
            None,
            Event,
            Inn,
            Shop,
            Exit,
        }

        public override void Show()
        {
            Console.Clear();
            // UI Base 출력
            WriteType<TownSceneType>();
            TownSceneType selection = InputManager.Instance.GetInputType<TownSceneType>();

            switch (selection)
            {
                case TownSceneType.Event:
                    WriteMessage("이벤트 씬으로 이동");
                    SceneManager.Instance.LoadScene<TownScene>();
                    break;
                case TownSceneType.Inn:
                    WriteMessage("휴식 씬으로 이동");
                    SceneManager.Instance.LoadScene<TownScene>();
                    break;
                case TownSceneType.Shop:
                    WriteMessage("상점 씬으로 이동");
                    SceneManager.Instance.LoadScene<ShopScene>();
                    break;
                case TownSceneType.Exit:
                    WriteMessage("필드 씬으로 이동");
                    UIManager.Instance.Write<SearchUI>();
                    break;
            }
        }

        protected override string SceneTypeToText<T>(T type)
        {
            return type switch
            {
                TownSceneType.Event => "이벤트",
                TownSceneType.Inn => "휴식",
                TownSceneType.Shop => "상점",
                TownSceneType.Exit => "마을 나가기",
                _ => "없음",
            };
        }
    }
}
