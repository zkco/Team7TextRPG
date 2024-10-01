using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Scenes
{
    public class CreditScene : SceneBase
    {
        public override void Show()
        {
            Console.Clear();

            // 1. 기능
            string[] credits = new string[]
           {
                "======= Team 7 RPG =======",
                "",
                "총괄 개발: 김태호",
                "Core(Contents,Creature,Datas,Manager): 김태호",
                "컨텐츠: 박두산",
                 "",
                "-----------------------------------------------",
                 "",
                "상태보기 UI: 길종혁",
                "상점 구매/판매 UI: 길종혁, 김태호",
                "퀘스트 UI: 길종혁, 김태호",
                "캐릭터 생성 UI: 김태호",
                "인벤토리 UI: 김태호",
                "Confirm UI: 김태호",
                "휴식 UI: 김태호",
                "공통 UI: 김태호",
                "퀘스트 UI: 김태호",
                "탐색 UI: 이민섭, 김태호",
                "이벤트 UI: 박두산",
                 "",
                "-----------------------------------------------",
                 "",
                "타이틀 화면: 김태호",
                "마을 화면: 박두산",
                "던전 화면: 박두산",
                "전투 화면: 이민섭, 김태호",
                "상점 화면: 길종혁, 김태호",
                 "",
                "-----------------------------------------------",
                 "",
                "상점 목록: 길종혁",
                "아이템 목록: 김태호, 박두산",
                "스킬 목록: 김태호",
                "퀘스트 목록: 김태호",
                 "",
                "======= Signature =======",
                 "",
                "카지노 시스템(블랙잭,포커,슬롯머신,주사위게임): 박두산",
                 "",
                "세이브저장 시스템: 김태호",
                "데이터저장 시스템: 김태호",
                "강화 UI: 김태호",
                "콘솔 출력 시스템: 김태호",
                 "",
                 "",
                 "",
                "감사합니다!",

           };

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            for (int i = 0; i < credits.Length + Console.WindowHeight; i++)
            {
                Console.Clear();


                for (int j = 0; j < Console.WindowHeight; j++)
                {
                    int creditIndex = i - (Console.WindowHeight - j);


                    if (creditIndex >= 0 && creditIndex < credits.Length)
                    {
                        Console.SetCursorPosition((Console.WindowWidth - credits[creditIndex].Length) / 2, j);
                        Console.WriteLine(credits[creditIndex]);
                    }
                }

                Thread.Sleep(500);
            }

            // 2. 기능
            EndCredits();

        }

        private void EndCredits()
        {
            Console.SetCursorPosition((Console.WindowWidth - "\n크레딧이 종료되었습니다. 메인 메뉴로 돌아갑니다...".Length) / 2, Console.WindowHeight / 2);
            Console.WriteLine("\n크레딧이 종료되었습니다. 메인 메뉴로 돌아갑니다...");
            Thread.Sleep(3000);

            Console.ResetColor();

            SceneManager.Instance.LoadScene<TitleScene>();
        }

        protected override string SceneTypeToText<T>(T type)
        {
            return String.Empty;
        }
    }
}
