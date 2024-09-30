using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Managers;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;
using static Team7TextRPG.Utils.Defines;

namespace Team7TextRPG.UIs
{
    public class StatusUI : UIBase
    {
        public override void Write()
        {
            Console.Clear();
            TextHelper.BtHeader("상태창");

            Console.WriteLine($"이름: {GameManager.Instance.Player.Name}");
            Console.WriteLine($"종족: {GameManager.Instance.Player.SpecisType}");
            Console.WriteLine($"HP: {GameManager.Instance.Player.MaxHp}");
            Console.WriteLine($"MP: {GameManager.Instance.Player.MaxMp}");
            Console.WriteLine("");
            TextHelper.ItHeader("주능력치");
            Console.WriteLine($"공격력: {GameManager.Instance.Player.Attack}");
            Console.WriteLine($"방어력: {GameManager.Instance.Player.Defense}");
            Console.WriteLine($"속도: {GameManager.Instance.Player.Speed}");
            Console.WriteLine($"도망확률: {GameManager.Instance.Player.DodgeChanceRate}");
            Console.WriteLine($"치명타확률: {GameManager.Instance.Player.CriticalChanceRate}");
            Console.WriteLine("");
            TextHelper.ItHeader("부능력치");
            Console.WriteLine($"힘: {GameManager.Instance.Player.StatStr}");
            Console.WriteLine($"민첩: {GameManager.Instance.Player.StatDex}");
            Console.WriteLine($"지력: {GameManager.Instance.Player.StatInt}");
            Console.WriteLine($"운: {GameManager.Instance.Player.StatLuck}");
            Console.WriteLine("");



        }
        protected override string EnumTypeToText<T>(T type)
        {
            throw new NotImplementedException();
        }
    }
}
