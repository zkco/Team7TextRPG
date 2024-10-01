using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Team7TextRPG.Utils.Defines;
using static Team7TextRPG.Creatures.PlayerCreature;
using Team7TextRPG.Utils;
using Team7TextRPG.Managers;
using System.Numerics;
using Team7TextRPG.Creatures;
using System.Collections;

namespace Team7TextRPG.UIs
{
    public class CommonUI : UIBase
    {
        public override void Write()
        {
            // 공통 UI 표시
            StatusBar();
            InterfaceBar();
        }

        public void StatusBar(bool isHpNumber = true)
        {
            if (GameManager.Instance.Player == null)
            {
                TextHelper.WriteLine("플레이어 정보가 없습니다.");
                return;
            }

            var player = GameManager.Instance.Player;

            string playerInfo = $"이름: {player.Name} | 레벨: {player.Level} | 직업: {player.JobType} | 경험치: {player.Exp}/{player.MaxExp}";

            int gold = GameManager.Instance.PlayerGold;
            int hp = GameManager.Instance.Player.Hp;
            int maxHp = GameManager.Instance.Player.MaxHp;
            string hpBar = Util.GetHpBar(hp, maxHp);
            string hpNumber = isHpNumber ? $"({hp}/{maxHp})" : "";
 
            int mp = GameManager.Instance.Player.Mp;
            int maxMp = GameManager.Instance.Player.MaxMp;
            string mpBar = Util.GetMpBar(mp, maxMp);
            string mpNumber = isHpNumber ? $"({mp}/{maxMp})" : "";

            int attack = player.Attack;
            int defense = player.Defense;

            TextHelper.StatusBar(playerInfo);
            TextHelper.StatusBar(
                $"체력: {hpBar} {hpNumber} | " +
                $"마나: {mpBar} {mpNumber} | " +
                $"공격력: {attack} | 방어력: {defense} | 소지금: {gold}G"
            );
        }

        public void InterfaceBar()
        {
            TextHelper.ItHeader(
                $"{EnumTypeToText(Defines.CommonUIType.Status)} : S | " +
                $"{EnumTypeToText(Defines.CommonUIType.Inventory)} : I | " +
                $"{EnumTypeToText(Defines.CommonUIType.Skill)} : K | " +
                $"{EnumTypeToText(Defines.CommonUIType.Quest)} : Q"
            );
        }

        protected override string EnumTypeToText<T>(T type)
        {
            return type switch
            {
                Defines.CommonUIType.Status => "상태",
                Defines.CommonUIType.Inventory => "인벤토리",
                Defines.CommonUIType.Skill => "스킬",
                Defines.CommonUIType.Quest => "퀘스트",
                _ => "알 수 없음",
            };
        }
    }
}
