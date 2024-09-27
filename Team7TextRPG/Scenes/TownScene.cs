using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;

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
            // 이벤트,
            // 휴식,
            // 상점 -> 상점 무기, 방어, 잡화 ,-> 상점 화면
            // 마을 나가기
            

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
