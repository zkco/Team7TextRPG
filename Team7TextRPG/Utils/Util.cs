using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Datas;

namespace Team7TextRPG.Utils
{
    public static class Util
    {
        public static string SexTypeToString(Defines.SexType sexType)
        {
            return sexType switch
            {
                Defines.SexType.Male => "남성",
                Defines.SexType.Female => "여성",
                _ => "알 수 없음",
            };
        }
        public static string SpeciesTypeToString(Defines.SpeciesType speciesType)
        {
            return speciesType switch
            {
                Defines.SpeciesType.Human => "인간",
                Defines.SpeciesType.Orc => "오크",
                Defines.SpeciesType.Elf => "엘프",
                Defines.SpeciesType.Dragonian => "드래고니안",
                Defines.SpeciesType.Dwarf => "드워프",
                _ => "알 수 없음",
            };
        }
        public static string JobTypeToString(Defines.JobType jobType)
        {
            return jobType switch
            {
                Defines.JobType.Newbie => "초보자",
                Defines.JobType.Warrior => "전사",
                Defines.JobType.Mage => "마법사",
                _ => "알 수 없음",
            };
        }
        public static string ShopTypeToString(Defines.ShopType shopType)
        {
            return shopType switch
            {
                Defines.ShopType.Weapon => "무기",
                Defines.ShopType.Armor => "방어구",
                Defines.ShopType.Accessory => "악세서리",
                Defines.ShopType.Potion => "잡화",
                Defines.ShopType.Blacksmith => "대장간",
                Defines.ShopType.Outshop => "상점 나가기",
                _ => "알 수 없음",
            };
        }
        public static string PagingSelectionTypeToString(Defines.PagingSelectionType pagingType)
        {
            return pagingType switch
            {
                Defines.PagingSelectionType.NextPage => "다음 페이지",
                Defines.PagingSelectionType.PrevPage => "이전 페이지",
                Defines.PagingSelectionType.Exit => "나가기",
                _ => "알 수 없음"
            };
        }
        public static string CommonUITypeToString(Defines.CommonUIType commonUiType)
        {
            return commonUiType switch
            {
                Defines.CommonUIType.Status => "상태",
                Defines.CommonUIType.Inventory => "인벤토리",
                Defines.CommonUIType.Skill => "스킬",
                Defines.CommonUIType.Quest => "퀘스트",
                _ => "알 수 없음",
            };
        }
        public static string BattleTypeToString(Defines.BattleType battleType)
        {
            return battleType switch
            {
                Defines.BattleType.Dungeon_Easy => "슈퍼 겁쟁이들의 쉼터",
                Defines.BattleType.Dungeon_Normal => "겁쟁이들의 쉼터",
                Defines.BattleType.Dungeon_Hard => "상남자 클럽",
                Defines.BattleType.Field => "평화로운 필드",
                _ => "없음",
            };
        }
        public static int BattleTypeToClearCount(Defines.BattleType battleType)
        {
            return battleType switch
            {
                Defines.BattleType.Dungeon_Easy => 5,
                Defines.BattleType.Dungeon_Normal => 10,
                Defines.BattleType.Dungeon_Hard => 15,
                _ => 0,
            };
        }
        public static int GrowthValue(int level, int maxLevel, double growthRate = 1.2, double baseExp = 100)
        {
            return (int)(baseExp * Math.Pow(growthRate, level));
        }
        public static bool CheckUserName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                // 공백 X
                TextHelper.ItContent("공백은 허용되지 않습니다.");
                Thread.Sleep(1000);
                return false;
            }
            else if (name.Length < 2 || name.Length > 10)
            {
                // 길이 2~10
                TextHelper.ItContent("2~10자 이내로 입력해주세요.");
                Thread.Sleep(1000);
                return false;
            }
            else
            {
                // 특수문자 X, 영어, 숫자, 한글만 가능
                foreach (char c in name)
                {
                    if (!char.IsLetterOrDigit(c) && !char.GetUnicodeCategory(c).ToString().StartsWith("OtherLetter"))
                    {
                        TextHelper.ItContent("특수문자는 사용할 수 없습니다.");
                        Thread.Sleep(1000);
                        return false;
                    }
                }

                return true;
            }
        }
        public static string GetHpBar(int hp, int maxHp)
        {
            // 크리쳐의 체력 게이지
            int hpPercent = maxHp == 0 ? 0 : (int)Math.Ceiling((hp * 5.0) / maxHp);

            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            for (int i = 0; i < 5; i++)
            {
                if (i < hpPercent)
                    sb.Append("♥");
                else
                    sb.Append("♡");
            }
            sb.Append(")");
            return sb.ToString();
        }
    }
}
