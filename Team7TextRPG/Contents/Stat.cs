using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7TextRPG.Contents
{
    /// <summary>
    /// 생명체의 스탯을 절대값으로 나타냅니다.
    /// </summary>
    public class Stat
    {
        public int StatStr { get; set; } // 힘 ex) 5
        public int StatDex { get; set; } // 민첩 ex) 5
        public int StatInt { get; set; } // 지능 ex) 5
        public int StatLuck { get; set; } // 운 ex) 5

        public int MaxHp { get; set; } // 최대 체력 ex) 100
        public int MaxMp { get; set; } // 최대 마나 ex) 100

        public int Attack { get; set; } // 공격력 ex) 10
        public int Defense { get; set; } // 방어력 ex) 5
        public int Speed { get; set; } // 속도 ex) 5
        public double DodgeChanceRate { get; set; } // 회피 확률 ex) 0.1 (10%)
        public double CriticalChanceRate { get; set; } // 치명타 확률 ex) 0.5 (50%)

        public override string ToString()
        {
            string text = "없음";
            if (StatStr != 0)
                text = $"힘: {StatStr}";
            if (StatDex != 0)
                text += $", 민첩: {StatDex}";
            if (StatInt != 0)
                text += $", 지능: {StatInt}";
            if (StatLuck != 0)
                text += $", 운: {StatLuck}";
            if (MaxHp != 0)
                text += $", 체력: {MaxHp}";
            if (MaxMp != 0)
                text += $", 마나: {MaxMp}";
            if (Attack != 0)
                text += $", 공격: {Attack}";
            if (Defense != 0)
                text += $", 방어: {Defense}";
            if (Speed != 0)
                text += $", 속도: {Speed}";
            if (DodgeChanceRate != 0)
                text += $", 회피: {DodgeChanceRate * 100:0.#}%";
            if (CriticalChanceRate != 0)
                text += $", 치명타: {CriticalChanceRate * 100:0.#}%";
            return text;
        }
    }
}
