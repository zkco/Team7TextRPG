using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public static int GrowthValue(int level, int maxLevel, double growthRate = 1.2, double baseExp = 100)
        {
            return (int)(baseExp * Math.Pow(growthRate, level));
        }
    }
}
