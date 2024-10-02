using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

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
            Save,
            Exit,
        }

        public override void Show()
        {
            Console.Clear();
            if (GameManager.Instance.IsGameEnd())
            {
                GameManager.Instance.GameEnd();
                InputManager.Instance.GetInputEnter();
                return;
            }

            TextHelper.BtHeader("마을");
            UIManager.Instance.CommonWriteBar();
            // UI Base 출력
            WriteType<TownSceneType>();
            string input = InputManager.Instance.GetInputKeyword();
            // 공통 UI 호출한 경우 볼일 마치가 다시 처음으로
            if (UIManager.Instance.CommonLoad(input))
            {
                SceneManager.Instance.LoadScene<TownScene>();
                return;
            }

            TownSceneType selection = InputManager.Instance.ParseInputType<TownSceneType>(input);

            switch (selection)
            {
                case TownSceneType.Event:
                    SceneManager.Instance.LoadScene<EventScene>();
                    return; // Scene 호출 뒤에는 while문을 빠져나가야 함.
                case TownSceneType.Inn:
                    UIManager.Instance.Write<RestUI>();
                    SceneManager.Instance.LoadScene<TownScene>();
                    return; // Scene 호출 뒤에는 while문을 빠져나가야 함.
                case TownSceneType.Shop:
                    SceneManager.Instance.LoadScene<ShopScene>();
                    return; // Scene 호출 뒤에는 while문을 빠져나가야 함.
                case TownSceneType.Save:
                    UIManager.Instance.Write<SaveGameUI>();
                    SceneManager.Instance.LoadScene<TownScene>();
                    return; // Scene 호출 뒤에는 while문을 빠져나가야 함.
                case TownSceneType.Exit:
                    SceneManager.Instance.LoadScene<FieldScene>();
                    return; // Scene 호출 뒤에는 while문을 빠져나가야 함.
            }
        }

        protected override string SceneTypeToText<T>(T type)
        {
            return type switch
            {
                TownSceneType.Event => "이벤트",
                TownSceneType.Inn => "휴식",
                TownSceneType.Shop => "상점",
                TownSceneType.Save => "저장",
                TownSceneType.Exit => "마을 나가기",
                _ => "없음",
            };
        }
    }
}
