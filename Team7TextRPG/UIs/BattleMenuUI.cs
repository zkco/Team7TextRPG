using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents.Shops;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;
using static Team7TextRPG.Scenes.BattleScene;

using Team7TextRPG.Creatures;
using Team7TextRPG.Contents;

namespace Team7TextRPG.UIs
{
    public class BattleMenuUI : UIBase
    {
        private MonsterCreature? enemy;


        public BattleMenuUI(MonsterCreature enemy)
        {
            this.enemy = enemy;  // BattleManager에서 전달받은 몬스터를 사용
        }



        public BattleScene? Battle { get; private set; }
        public enum BattleAction
        {
            None,
            Attack,
            Skill,
            Item,
            Run
        }



        public override void Write()
        {
            WriteType<BattleAction>();
            BattleAction selection = InputManager.Instance.GetInputType<BattleAction>();
            

            switch (selection)
            {
                case BattleAction.Attack:
                    BattleManager.Instance.AttackEnemy();   //플레이어 공격 실행
                    break;
                case BattleAction.Skill:
                    Skill? skill =  UIManager.Instance.SkillRead(); //스킬UI로
                    skill?.Use(new CreatureBase[] { enemy });
                    break;
                case BattleAction.Item:
                    UIManager.Instance.Write<InventoryUI>(); //인벤토리UI로
                    break;
                case BattleAction.Run:
                    Console.Clear();
                    UIManager.Instance.Write<CommonUI>();
                    BattleManager.Instance.Runaway();
                    Console.WriteLine("성공적으로 도망쳤습니다...");

                    Thread.Sleep(1000);
                    SceneManager.Instance.LoadScene<FieldScene>(); //탐색UI로 돌아가기
                    return; // 도망 후 종료


                default:
                    Console.WriteLine("잘못된 입력입니다.");
                    break;
            }
        }


        public BattleAction PlayerAction()
        {
            WriteType<BattleAction>(); // 행동 선택지 출력
            return InputManager.Instance.GetInputType<BattleAction>();
        }


        protected override string EnumTypeToText<T>(T type)
        {
            return type switch
            {
                BattleAction.Attack => "공격",
                BattleAction.Skill => "스킬 사용",
                BattleAction.Item => "아이템 사용.",
                BattleAction.Run => "도망가기.",
                _ => "없음",
            };
        }
    }
}
