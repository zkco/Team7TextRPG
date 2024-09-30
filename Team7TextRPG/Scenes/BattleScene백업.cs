//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static Team7TextRPG.Scenes.FieldScene;
//using Team7TextRPG.Managers;
//using static Team7TextRPG.Scenes.ShopScene;
//using Team7TextRPG.UIs;
//using System.Threading;

//namespace Team7TextRPG.Scenes
//{
//    public class BattleScene : SceneBase
//    {

//        public enum BattleSceneType
//        {
//            None,
//            Attack,
//            Skill,
//            UseItem,
//            Run,
//        }

       



//        public override void Show()
//        {

//            WriteType<BattleSceneType>();
//            BattleSceneType selection = InputManager.Instance.GetInputType<BattleSceneType>();


//            switch(selection)
//            {
//                case BattleSceneType.Attack:
//                    UIManager.Instance.Write<BattleMenuUI>();   //공격UI로
//                    break;
//                case BattleSceneType.Skill:
//                    UIManager.Instance.Write<SkillUI>();  //스킬UI로
//                    break;
//                case BattleSceneType.UseItem:
//                    UIManager.Instance.Write<InventoryUI>(); //인벤토리UI로
//                    break;
//                case BattleSceneType.Run:
//                    Console.Clear();
//                    Console.WriteLine("전투에서 도망쳤습니다.");
//                    Thread.Sleep(1000);
//                    SceneManager.Instance.LoadScene<FieldScene>(); //탐색UI로 돌아가기
//                    break;
//            }
//            }





//        // 선택지 
//        protected override string SceneTypeToText<T>(T type)
//        {
//           return type switch
//            {
//                BattleSceneType.Attack => "공격 하기",
//                BattleSceneType.Skill => "스킬 사용",
//                BattleSceneType.UseItem => "아이템 사용",
//                BattleSceneType.Run => "도망가기",
//                _ => "없음",
//            };
//        }
//    }
//}
