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
        public static int GrowthValue(int level, int maxLevel, double growthRate = 1.2, double baseExp = 100)
        {
            return (int)(baseExp * Math.Pow(growthRate, level));
        }
    }
}
