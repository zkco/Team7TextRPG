using Team7TextRPG.Managers;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Scenes
{
    internal class ShopScene : SceneBase
    {
        public override void Show()
        {
            // 상점 안내 출력
            Console.Clear();
            WriteMessage("방문하려는 상점을 선택하세요.");
            // 1. 메뉴를 보여준다.
            WriteType<Defines.ShopType>();
            // 2. 사용자 입력 받는다.
            Defines.ShopType selection = InputManager.Instance.GetInputType<Defines.ShopType>();
            // 3. 사용자 입력에 따라 다음 화면으로 이동하거나 표시한다.
            if (selection == Defines.ShopType.Outshop)
            {
                WriteMessage("상점을 나갑니다.");
                SceneManager.Instance.LoadScene<TownScene>();
            }
            else
            {
                UIManager.Instance.ShopWrite(selection);
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
