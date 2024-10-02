using Team7TextRPG.Managers;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Scenes
{
    internal class ShopScene : SceneBase
    {
        public override void Show()
        {
            Console.Clear();
            if (GameManager.Instance.IsGameEnd())
            {
                GameManager.Instance.GameEnd();
                InputManager.Instance.GetInputEnter();
                return;
            }

            TextHelper.BtHeader("상점");
            // [상태, 인벤토리, 스킬, 퀘스트]
            UIManager.Instance.CommonWriteBar();
            // 상점 안내 출력
            WriteMessage("방문하려는 상점을 선택하세요.");
            // 1. 메뉴를 보여준다.
            WriteType<Defines.ShopType>();
            // 2. 사용자 입력 받는다.
            string input = InputManager.Instance.GetInputKeyword();

            // 공통 UI 호출한 경우 볼일 마치가 다시 처음으로
            if (UIManager.Instance.CommonLoad(input))
            {
                SceneManager.Instance.LoadScene<ShopScene>();
                return;
            }

            Defines.ShopType selection = InputManager.Instance.ParseInputType<Defines.ShopType>(input);
            // 3. 사용자 입력에 따라 다음 화면으로 이동하거나 표시한다.
            if (selection == Defines.ShopType.Outshop)
            {
                WriteMessage("상점을 나갑니다.");
                SceneManager.Instance.LoadScene<TownScene>();
            }
            else
            {
                UIManager.Instance.ShopWrite(selection);
                SceneManager.Instance.LoadScene<ShopScene>();
            }
        }

        protected override string SceneTypeToText<T>(T t)
        {
            return t switch
            {
                Defines.ShopType.Potion => "잡화 상점",
                Defines.ShopType.Weapon => "무기 상점",
                Defines.ShopType.Armor => "방어구 상점",
                Defines.ShopType.Accessory => "악세서리 상점",
                Defines.ShopType.Blacksmith => "대장간",
                Defines.ShopType.Outshop => "상점 나가기",
                _ => String.Empty,
            };
        }
    }
}
