using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Datas
{
    public class LevelData
    {
        public int Level;
        public int MaxExp;
        public int Str;
        public int Dex;
        public int Int;
        public int Luck;
        public int BonusPoint;
    }
    public class MonsterData
    {
        public int DataId;
        public string Name;
        public string Description;
        public Defines.MonsterType MonsterType;
        public int Level;
        public int StatStr;
        public int StatDex;
        public int StatInt;
        public int StatLuck;
        public int MaxHp;
        public int MaxMp;
        public int Attack;
        public int Defense;
        public int Speed;
        public int DodgeChanceRate;
        public int CriticalChanceRate;
        public int ExpReward;
        public int ItemReward;
        public int DropItemRate;
        public string? SkillDataIds;
    }
}
