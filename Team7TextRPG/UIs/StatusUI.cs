using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Contents;
using Team7TextRPG.Creatures;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;
using static Team7TextRPG.Scenes.TitleScene;
using static Team7TextRPG.Scenes.TownScene;
using static Team7TextRPG.Utils.Defines;

namespace Team7TextRPG.UIs
{
    public class StatusUI : UIBase
    {
        public enum statPlus
        {
            None,
            StatStr,
            StatDex,
            StatInt,
            StatLuck,
            back
        }
        public override void Write()
        {
            PlayerCreature? player = GameManager.Instance.Player;
            if (player == null ) return;
            Console.Clear();
            TextHelper.BtHeader("상태창");
            TextHelper.ItHeader("기본");
            Console.WriteLine($"이름: {GameManager.Instance.Player.Name}");
            Console.WriteLine($"종족: {GameManager.Instance.Player.SpecisType}");
            Console.WriteLine($"레벨: {GameManager.Instance.Player.Level}");
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
            Console.WriteLine($"힘: {GameManager.Instance.Player.StatStr}+{GameManager.Instance.Player.StatPointStr}");
            Console.WriteLine($"민첩: {GameManager.Instance.Player.StatDex}+{GameManager.Instance.Player.StatPointDex}");
            Console.WriteLine($"지력: {GameManager.Instance.Player.StatInt}+{GameManager.Instance.Player.StatPointInt}");
            Console.WriteLine($"운: {GameManager.Instance.Player.StatLuck}+{GameManager.Instance.Player.StatPointLuck}");
            Console.WriteLine("");
            TextHelper.BtHeader("스탯투자");
            WriteType<statPlus>();
            Console.WriteLine($"남은 스탯 포인트: {GameManager.Instance.Player.StatPoint}");

            string input = InputManager.Instance.GetInputKeyword();

            statPlus selection = InputManager.Instance.ParseInputType<statPlus>(input);
            
            switch (selection)
            {
                case statPlus.StatStr:
                    GameManager.Instance.Player?.AddStatPoint(Utils.Defines.StatPointType.StatStr);
                    UIManager.Instance.Write<StatusUI>();
                    return; // Scene 호출 뒤에는 while문을 빠져나가야 함.
                case statPlus.StatDex:
                    GameManager.Instance.Player?.AddStatPoint(Utils.Defines.StatPointType.StatDex);
                    UIManager.Instance.Write<StatusUI>();
                    return; // Scene 호출 뒤에는 while문을 빠져나가야 함.
                case statPlus.StatInt:
                    GameManager.Instance.Player?.AddStatPoint(Utils.Defines.StatPointType.StatInt);
                    UIManager.Instance.Write<StatusUI>();
                    return; // Scene 호출 뒤에는 while문을 빠져나가야 함.
                case statPlus.StatLuck:
                    GameManager.Instance.Player?.AddStatPoint(Utils.Defines.StatPointType.StatLuck);
                    UIManager.Instance.Write<StatusUI>();
                    return; // Scene 호출 뒤에는 while문을 빠져나가야 함.
                case statPlus.back:
                    SceneManager.Instance.LoadScene<TownScene>();
                    return; // Scene 호출 뒤에는 while문을 빠져나가야 함.
            }



        }
        protected override string EnumTypeToText<T>(T type)
        {
            return type switch
            {
                statPlus.StatStr => "힘",
                statPlus.StatDex => "민첩",
                statPlus.StatInt => "지력",
                statPlus.StatLuck => "운",
                statPlus.back => "상태창 끄기",
                _ => "없음",
            };
        }
    }
}
