using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;
using static Team7TextRPG.Scenes.DungeonScene;

namespace Team7TextRPG.UIs
{
    public class DungeonDiffUI : UIBase
    {
        Defines.BattleType _battleType = Defines.BattleType.None;

        public override void Write()
        {
            Console.Clear();
            TextHelper.DtContent("던전 난이도를 선택하세요.");
            WriteType<Defines.BattleType>();

            _battleType = InputManager.Instance.GetInputType<Defines.BattleType>();
        }

        public Defines.BattleType Read()
        {
            return _battleType;
        }


        protected override string EnumTypeToText<T>(T type)
        {
            return type switch
            {
                Defines.BattleType.Dungeon_Easy => "슈퍼 겁쟁이들의 쉼터",
                Defines.BattleType.Dungeon_Normal => "겁쟁이들의 쉼터",
                Defines.BattleType.Dungeon_Hard => "상남자 클럽",
                Defines.BattleType.Field => "필드로 돌아간다.",
                _ => "없음",
            };
        }
    }
}
