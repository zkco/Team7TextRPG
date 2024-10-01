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
        public int Level; // 레벨
        public int MaxExp; // 최대 경험치
        public int Str; // 힘
        public int Dex; // 민첩
        public int Int; // 지능
        public int Luck; // 운
        public int BonusPoint; // 보너스 포인트
    }
    public class MonsterData
    {
        public int DataId; // 데이터 아이디 ex) 1
        public string? Name; // 몬스터 이름
        public string? Description; // 몬스터 설명
        public Defines.MonsterType MonsterType; // 몬스터 타입 (Normal, Elite, Boss)
        public int Level; // 레벨
        public int StatStr; // 힘
        public int StatDex; // 민첩
        public int StatInt; // 지능
        public int StatLuck; // 운
        public int MaxHp; // 최대 체력
        public int MaxMp; // 최대 마나
        public int Attack; // 공격력
        public int Defense; // 방어력
        public int Speed; // 속도
        public double DodgeChanceRate; // 회피 확률
        public double CriticalChanceRate; // 치명타 확률
        public int ExpReward; // 경험치 보상
        public int ItemReward; // 아이템 보상
        public int DropItemRate; // 드랍 확률
        public int SkillDataId1; // 스킬 데이터 아이디
        public int SkillDataId2; // 스킬 데이터 아이디
        public int SkillDataId3; // 스킬 데이터 아이디
    }
}
