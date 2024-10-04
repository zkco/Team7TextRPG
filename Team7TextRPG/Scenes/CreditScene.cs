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
            Console.BackgroundColor = ConsoleColor.Gray;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Clear();

            Defines.EndingType endingType = GameManager.Instance.GetEndingType();
            List<string> credits = new List<string>();
            switch (endingType)
            {
                case Defines.EndingType.Gold:
                    credits.AddRange(GetGoldEnding());
                    break;
                case Defines.EndingType.Death:
                    credits.AddRange(GetDeathEnding());
                    break;
                case Defines.EndingType.Chip:
                    credits.AddRange(GetChipEnding());
                    break;
                case Defines.EndingType.SpecialLottery:
                    credits.AddRange(GetSpecialEnding());
                    break;
                case Defines.EndingType.Bankruptcy:
                    credits.AddRange(GetBankruptcyEnding());
                    break;
                default:
                    break;
            }
            credits.AddRange(GetCreditLast());
            // 크레딧 작성
            DrawCredits(credits.ToArray());

            // 2. 기능
            EndCredits();

        }

        private string[] GetCreditLast()
        {
            return new string[]
           {
                "================= Team 7 RPG ==================",
                "",
                "총괄 개발:                             김태호",
                "Core(Contents,Creature,Datas,Manager): 김태호",
                "컨텐츠기획:                            박두산",
                 "",
                "-----------------------------------------------",
                 "",
                "상태보기 UI:                           길종혁",
                "상점 구매/판매 UI:             길종혁, 김태호",
                "퀘스트 UI:                     길종혁, 김태호",
                "캐릭터 생성 UI:                        김태호",
                "인벤토리 UI:                           김태호",
                "Confirm UI:                            김태호",
                "휴식 UI:                               김태호",
                "공통 UI:                               김태호",
                "퀘스트 UI:                             김태호",
                "탐색 UI:                       이민섭, 김태호",
                "이벤트 UI:                             박두산",
                 "",
                "-----------------------------------------------",
                 "",
                "타이틀 화면:                           김태호",
                "마을 화면:                             박두산",
                "던전 화면:                             박두산",
                "전투 화면:                     이민섭, 김태호",
                "상점 화면:                     길종혁, 김태호",
                "엔딩 화면:                             김태호",
                 "",
                "-----------------------------------------------",
                 "",
                "상점 목록:                             길종혁",
                "아이템 목록:                           박두산",
                "스킬 목록:                             길종혁",
                "퀘스트 목록:                           김태호",
                 "",
                "======= Signature =======",
                 "",
                "카지노(블랙잭,포커,슬롯머신,주사위):   박두산",
                "복권 시스템:                           박두산",
                 "",
                "세이브저장 시스템:                     김태호",
                "데이터저장 시스템:                     김태호",
                "강화 UI:                               김태호",
                "콘솔 출력 시스템:                      김태호",
                 "",
                 "",
                 "",
                "감사합니다!",
            };

        }
        private string[] GetGoldEnding()
        {
            return new string[]
            {
                "============= 복권 사냥 RPG =============",
                "",
                "드디어...",
                "",
                $"용사는 {GameManager.Instance.Gold} Gold 만큼 획득 하였습니다.",
                "",
                "더 이상 힘들게 몬스터를 처치하고",
                "",
                "아이템을 팔 필요가 없어졌습니다.",
                "",
                "지겨운 촌장도 더 이상 볼 이유도 없습니다.",
                "",
                "용사는 평화로운 노후를 위해",
                "",
                "휴양지로 떠나기로 결심했습니다.",
                "",
                "",
                "HAPPY ENDING!",
                "",
                "",
                "",
                "",
            };
        }
        private string[] GetChipEnding()
        {
            return new string[]
            {
                "============= 복권 사냥 RPG =============",
                "",
                "용사는 마침내 카지노에서 인생 역전을 실현합니다.",
                "",
                $"용사는 {GameManager.Instance.Chip} Chip 만큼 획득 하였습니다.",
                "",
                "더 이상 용사는 용사가 아닙니다.",
                "",
                "카지노를 운영할 정도의 Chip을 가졌으니",
                "",
                "이제는 용사 따위는 개나주고 카지노를 운영하기로 결심했습니다.",
                "",
                "HAPP ENDING?",
                "",
                "",
                "",
                "",
            };
        }
        private string[] GetSpecialEnding()
        {
            return new string[] {
                "============= 복권 사냥 RPG =============",
                "",
                "당신의 운빨은 미쳤습니다.",
                "",
                "지금 당장 이러고 있을 시간이 없습니다.",
                "",
                "자리에서 일어나 복권을 구매하러 가십시오.",
                "",
                "아마도 당신의 운이라면 이번에도 한번에 당첨 될지도 모릅니다.",
                "",
                "그렇다고 복권에 당첨되지 않은 걸 저희가 책임지진 않습니다.",
                "",
                "모두 당신의 손끝에 달린 일 입니다.",
                "",
                "무운을 빕니다.",
                "",
                "",
                "",
                "",
            };
        }
        private string[] GetDeathEnding()
        {
            string name = GameManager.Instance.Player?.Name ?? "Player";
            name = name.Length > 6 ? name.Substring(0, 6) : name;
            return new string[]
            {
                "============= 복권 사냥 RPG =============",
                "",
                "용사는 마지막 숨을 내쉬었습니다.",
                "",
                "용사는 더 이상 이 세계에 존재하지 않습니다.",
                "",
                "용사는 무덤 속에서도 복권을 찾는 꿈을 꾸었습니다.",
                "",
                "하지만 그 꿈은 결코 이루어지지 않을 것입니다.",
                "",
                "이미 당신은 죽었으니까..",
                "",
                "THE END",
                "",
                "  _____",
                " /     \\",
                "/        \\",
                "|  R.I.P |",
                $"| {name} |",
                "|        |",
                "|--------|",
                "",
                "",
                "",
                "",
            };
        }
        private string[] GetBankruptcyEnding()
        {
            return new string[]
            {
                "============= 복권 사냥 RPG =============",
                "",
                "용사는 마침내 파산을 선언합니다.",
                "",
                "용사는 더 이상 돈이 없습니다.",
                "",
                "더 이상 복권을 구매할 수도 없습니다.",
                "",
                "아무것도 살 수 없습니다.",
                "",
                "용사는 더 이상 용사가 아닙니다.",
                "",
                "용사는 치료가 시급해 보입니다...",
                "",
                "THE END",
                "",
                "도박 중독은 질병입니다.",
                "",
                "도박 중독 1336과 함께하면 끊을 수 있습니다.",
                "",
                "",
            };
        }

        private void DrawCredits(string[] credits)
        {
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            if (windowHeight <= 0 || windowWidth <= 0) return;

            for (int i = 0; i < credits.Length + windowHeight; i++)
            {
                Console.Clear();

                //int middleY = Math.Max(0, (windowHeight - credits.Length) / 2);
                int middleY = 1;

                for (int j = 0; j < windowHeight; j++)
                {
                    int creditIndex = i - (windowHeight - j);

                    if (creditIndex >= 0 && creditIndex < credits.Length)
                    {
                        int leftPosition = 2;

                        if (middleY + j < windowHeight)
                        {
                            Console.ForegroundColor = ConsoleColor.Black;
                            Console.SetCursorPosition(leftPosition, middleY + j);
                            Console.WriteLine(credits[creditIndex]);
                        }
                    }
                }

                Thread.Sleep(500);
            }
        }

        private void EndCredits()
        {
            Console.SetCursorPosition((Console.WindowWidth - "\n크레딧이 종료되었습니다. 메인 메뉴로 돌아갑니다...".Length) / 2, Console.WindowHeight / 2);
            Console.WriteLine("\n크레딧이 종료되었습니다. 메인 메뉴로 돌아갑니다...");
            InputManager.Instance.GetInputEnter();

            Console.ResetColor();

            SceneManager.Instance.LoadScene<TitleScene>();
        }

        protected override string SceneTypeToText<T>(T type)
        {
            return String.Empty;
        }
    }
}
