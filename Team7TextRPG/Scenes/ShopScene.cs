using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.UIs;

namespace Team7TextRPG.Scenes
{
    internal class ShopScene : SceneBase
    {
        public enum ShopSceneType
        {
            Potion,     //1. 잡화 상점
            Weapon,     //2. 무기 상점
            Armor,      //3. 방어구 상점
            Accessory,  //4. 악세서리 상점
            Blacksmith, //5. 대장간
            Outshop     //6. 상점 나가기
        }

        public override void Show()
        {
            Console.Clear();
            // 상점 안내 출력
            WriteMessage("방문하려는 상점을 선택하세요.");
            // 1. 메뉴를 보여준다.
            WriteType<ShopSceneType>();
            // 2. 사용자 입력 받는다.
            ShopSceneType selection = InputManager.Instance.GetInputType<ShopSceneType>();
            // 이 부분은 InputManager로 옮김.

            // 3. 사용자 입력에 따라 다음 화면으로 이동하거나 표시한다.
            switch (selection)
            {
                case ShopSceneType.Potion:
                    SceneManager.Instance.LoadScene<BuySellScene>();
                    break;
                case ShopSceneType.Weapon:
                    SceneManager.Instance.LoadScene<BuySellScene>();
                    break;
                case ShopSceneType.Armor:
                    SceneManager.Instance.LoadScene<BuySellScene>();
                    break;
                case ShopSceneType.Accessory:
                    SceneManager.Instance.LoadScene<BuySellScene>();
                    break;
                case ShopSceneType.Blacksmith:
                    SceneManager.Instance.LoadScene<BuySellScene>();
                    break;
                case ShopSceneType.Outshop:
                    SceneManager.Instance.LoadScene<TownScene>();
                    break;
            }
        }

        protected override string SceneTypeToText<T>(T t)
        {
            return t switch
            {
                ShopSceneType.Potion => "잡화 상점",
                ShopSceneType.Weapon => "무기 상점",
                ShopSceneType.Armor => "방어구 상점",
                ShopSceneType.Accessory => "악세서리 상점",
                ShopSceneType.Blacksmith => "대장간",
                ShopSceneType.Outshop => "상점 나가기",
                _ => String.Empty,
            };
        }
    }
}
