using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.UIs;

namespace Team7TextRPG.Scenes
{
    internal class BuySellScene : SceneBase
    {
        public enum BuySellSceneType
        {
            None,
            Buy,     //1. 구매
            Sell,    //2. 판매
            Out      //3. 나가기
        }

        public override void Show()
        {
            Console.Clear();
            
            WriteType<BuySellSceneType>();
            BuySellSceneType selection = InputManager.Instance.GetInputType<BuySellSceneType>();
            
            switch (selection)
            {
                case BuySellSceneType.Buy:
                    UIManager.Instance.Write<ShopBuyUI>();
                    break;
                case BuySellSceneType.Sell:
                    UIManager.Instance.Write<ShopSellUI>();
                    break;
                case BuySellSceneType.Out:
                    SceneManager.Instance.LoadScene<ShopScene>();
                    break;
            }
        }

        protected override string SceneTypeToText<T>(T t)
        {
            return t switch
            {
                BuySellSceneType.Buy => "구매하시겠습니까?",
                BuySellSceneType.Sell => "판매하시겠습니다?",
                BuySellSceneType.Out => "나가시겠습니다?",
                _ => String.Empty,
            };
        }
    }
}
