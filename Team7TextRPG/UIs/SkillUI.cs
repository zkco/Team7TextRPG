using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents;
using Team7TextRPG.Managers;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public class SkillUI : UIBase
    {
        protected int pageSize = 5;
        protected int pageIndex = 0;

        private Skill? _selectedSkill;

        public void NextPage()
        {
            pageIndex = Math.Min(pageIndex + 1, (GameManager.Instance.PlayerItems.Count - 1) / pageSize);
        }
        public void PrevPage()
        {
            pageIndex = Math.Max(pageIndex - 1, 0);
        }

        public override void Write()
        {
            while (true)
            {
                Console.Clear();
                // 인벤토리 표시
                TextHelper.ItHeader("스킬");
                TextHelper.PageWrite($"현재 페이지: {pageIndex + 1}/{(GameManager.Instance.PlayerItems.Count - 1) / pageSize + 1}");
                TextHelper.ItContent("번호 | 이름 | 직업 | 소모MP | 설명");
                Skill[] skills = GameManager.Instance.PlayerSkills.Skip(pageIndex * pageSize).Take(pageSize).ToArray();
                // 1. 인벤토리 아이템 목록 표시
                for (int i = 0; i < 5; i++)
                {
                    if (i <= skills.Length - 1)
                    {
                        Skill skill = skills[i];
                        string jobText = skill.RequiredJobType == Defines.JobType.None ? "전체" : Util.JobTypeToString(skill.RequiredJobType);
                        TextHelper.ItContent($"{i + 1}. {skill.Name} | {jobText} | {skill.MpCost} | {skill.Description}");
                    }
                    else
                    {
                        TextHelper.ItContent($"{i + 1}. -");
                    }
                }
                ShowSelection();
                // 2. 아이템 사용 여부 확인
                // 3. 아이템 사용
                int input = InputManager.Instance.GetInputInt("번호를 입력하세요.",
                    (int)Defines.PagingSelectionType.None + 1,
                    (int)Defines.PagingSelectionType.Exit);

                switch ((Defines.PagingSelectionType)input)
                {
                    case Defines.PagingSelectionType.NextPage:
                        NextPage();
                        break;
                    case Defines.PagingSelectionType.PrevPage:
                        PrevPage();
                        break;
                    case Defines.PagingSelectionType.None:
                    case Defines.PagingSelectionType.Exit:
                        return;
                    default:
                        // 배틀에서만 사용
                        _selectedSkill = skills[input - 1];
                        return;
                }
            }
        }
        protected override string EnumTypeToText<T>(T type)
        {
            throw new NotImplementedException();
        }

        private void ShowSelection()
        {
            // 1. 스킬1
            // 2. 스킬2
            // 3. 스킬3
            // 4. 스킬4
            // 5. 스킬5
            // 6. 다음 페이지
            // 7. 이전 페이지
            // 8. 나가기
            foreach (var selection in Enum.GetValues(typeof(Defines.PagingSelectionType)).Cast<Defines.PagingSelectionType>())
            {
                if (selection == Defines.PagingSelectionType.None) continue;
                TextHelper.ItContent($"{(int)selection}. {Util.PagingSelectionTypeToString(selection)}");
            }
        }

        public Skill? Read()
        {
            Write();
            return _selectedSkill;
        }
    }
}
