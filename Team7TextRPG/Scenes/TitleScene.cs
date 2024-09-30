using Team7TextRPG.UIs;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Scenes
{
    public class TitleScene : SceneBase
    {
        public enum TitleSceneType
        {
            None,
            NewGame,
            Credit,
            LoadGame,
            Exit
        }

        public override void Show()
        {
            Console.Clear();
            // 로고 출력
            TextHelper.BtContent("Team7 Text RPG");
            // 1. 메뉴를 보여준다.
            WriteType<TitleSceneType>();
            // 2. 사용자 입력 받는다.
            TitleSceneType selection = InputManager.Instance.GetInputType<TitleSceneType>();
            // 3. 사용자 입력에 따라 다음 화면으로 이동하거나 표시한다.
            switch (selection)
            {
                case TitleSceneType.NewGame:
                    UIManager.Instance.Write<NewGameUI>();
                    break;
                case TitleSceneType.Credit:
                    SceneManager.Instance.LoadScene<CreditScene>();
                    break;
                case TitleSceneType.LoadGame:
                    UIManager.Instance.Write<LoadGameUI>();
                    break;
                case TitleSceneType.Exit:
                    Environment.Exit(0);
                    break;
            }
        }

        protected override string SceneTypeToText<T>(T t)
        {
            return t switch
            {
                TitleSceneType.NewGame => "새 게임",
                TitleSceneType.Credit => "Credit",
                TitleSceneType.LoadGame => "불러오기",
                TitleSceneType.Exit => "종료",
                _ => String.Empty,
            };
        }
    }
}
