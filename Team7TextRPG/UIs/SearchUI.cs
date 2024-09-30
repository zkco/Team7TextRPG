using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;

namespace Team7TextRPG.UIs
{
    public class SearchUI : UIBase
    {
        public override void Write()
        {
            SearchField();
        }


        //필드 탐색하기
        private void SearchField()
        {
            Console.WriteLine("필드를 탐색하는 중..");
            Thread.Sleep(1000); 



            Random random = new Random();
            int encounter = random.Next(0, 100);
            if (encounter < 60)
            {
                EncounterMonster();

            }
            else if (encounter < 10)
            {
                FindTreasureChest();
                Ask();
            }
            else
            {
                Console.WriteLine("아무것도 발견하지 못했습니다.");
                Ask();
            }

            // 탐험을 계속할지 묻는 기능
            

            //최대5번까지 탐험가능 5번이넘으면 하루가 지남


        }


        private void Ask()
        {
            UIManager.Instance.Confirm("계속 탐색하시겠습니까?",
            () =>
            {
                // 사용자가 탐험을 계속하도록 선택
                SearchField();
            });
        }



        //몬스터랑 만났을 경우
        private void EncounterMonster()
        {
            Console.Clear();
            Console.WriteLine("몬스터와 만났습니다! 전투를 시작합니다.");
            //전투화면으로
            SceneManager.Instance.LoadScene<BattleScene>();
            
        }


        //보물상자 발견했을경우 
        private void FindTreasureChest()
        {
            Console.WriteLine("숨겨진 보물상자를 발견했습니다!");
            //보물획득처리 시스템으로 UI? 화면?
        }

        protected override string EnumTypeToText<T>(T type)
        {
            throw new NotImplementedException();
        }
    }
}
