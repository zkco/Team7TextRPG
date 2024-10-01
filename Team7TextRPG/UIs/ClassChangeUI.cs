using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public class ClassChangeUI : UIBase
    {
        public override void Write()
        {
            Console.Clear();
            TextHelper.BtHeader("전직의 제단");
            TextHelper.DtContent("전직의 제단에 오신 것을 환영합니다.");
            Thread.Sleep(1000);
            TextHelper.DtContent("전직을 하기 위해서는 충분한 자격을 갖춰야합니다.");
            Thread.Sleep(1000);
            TextHelper.DtContent("자격을 갖추지 못한 경우 전직을 할 수 없습니다.");
            Thread.Sleep(1000);
            TextHelper.DtContent("전직을 원하시면 직업을 선택해주세요.");
            Thread.Sleep(1000);
            TextHelper.BtContent("※ 전직 가능 조건 Level 5 이상, 현재 직업 초보자 ※");

            if(GameManager.Instance.Player?.JobType != Defines.JobType.Newbie)
            {
                TextHelper.DtContent("전직가능한 상태가 아닙니다. 이전 화면으로 돌아갑니다.");
                InputManager.Instance.GetInputEnter();
                return;
            }

            TextHelper.ItContent($"1. {Util.JobTypeToString(Defines.JobType.Warrior)}");
            TextHelper.ItContent($"2. {Util.JobTypeToString(Defines.JobType.Mage)}");
            TextHelper.ItContent($"3. {Util.JobTypeToString(Defines.JobType.Archer)}");

            int input = InputManager.Instance.GetInputInt("직업을 선택 하세요.", 1, 3);

            Defines.JobType selection = (Defines.JobType)(input + 1);
            if (UIManager.Instance.Confirm("직업을 선택하면 더 이상 돌이킬 수 없습니다. 전직 하시겠습니까?") == false)
            {
                TextHelper.DtContent("전직을 취소 하셨습니다. 이전 화면으로 돌아갑니다.");
                InputManager.Instance.GetInputEnter();
                return;
            }

            TextHelper.CtContent($"{Util.JobTypeToString(selection)}의 길로 향하는 중입니다.");
            switch (selection)
            {
                case Defines.JobType.Warrior:
                    if (UIManager.Instance.Confirm("전사로 전직하시겠습니까?"))
                    {
                        Thread.Sleep(1000);
                        TextHelper.CtContent("힘의 영향으로 강력해지는 것이 느껴집니다.");
                        Thread.Sleep(1000);
                        TextHelper.CtContent("전직을 축하드립니다.");
                        GameManager.Instance.Player?.SetJob(selection);
                    }
                    break;
                case Defines.JobType.Mage:
                    if (UIManager.Instance.Confirm("마법사로 전직하시겠습니까?"))
                    {
                        Thread.Sleep(1000);
                        TextHelper.CtContent("지능의 영향으로 강력해지는 것이 느껴집니다.");
                        Thread.Sleep(1000);
                        TextHelper.CtContent("전직을 축하드립니다.");
                        GameManager.Instance.Player?.SetJob(selection);
                    }
                    break;
                case Defines.JobType.Archer:
                    if (UIManager.Instance.Confirm("궁수로 전직하시겠습니까?"))
                    {
                        Thread.Sleep(1000);
                        TextHelper.CtContent("민첩의 영향으로 강력해지는 것이 느껴집니다.");
                        Thread.Sleep(1000);
                        TextHelper.CtContent("전직을 축하드립니다.");
                        GameManager.Instance.Player?.SetJob(selection);
                    }
                    break;
            }
            InputManager.Instance.GetInputEnter();
        }

        protected override string EnumTypeToText<T>(T type)
        {
            return type switch
            {
                Defines.JobType.Warrior => "전사",
                Defines.JobType.Mage => "마법사",
                Defines.JobType.Archer => "궁수",
                _ => "없음",
            };
        }
    }
}
