using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Creatures;
using Team7TextRPG.Managers;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents
{
    public class Battle
    {
        private List<MonsterCreature> _monsters;
        private PlayerCreature _player;
        public Battle(List<MonsterCreature> monsters, PlayerCreature player)
        {
            _monsters = monsters;
            _player = player;
        }

        public void BattleStart()
        {
            // 전투 시작
            TextHelper.BtHeader("몬스터와 전투 시작");
            InputManager.Instance.GetInputEnter();

            //while (true)
            //{
            //    // 우선 턴 잡은 크리쳐가 먼저 공격 가능

            //    // 몬스터 턴인 경우 몬스터가 플레이어 공격

            //    // 플레이어 턴인 경우 공격, 스킬, 아이템, 도망 중 선택 가능

            //    // 누군가 사망하면 전투 종료 (몬스터는 전부 사망)
            //}

            // 전투 승리 시 보상 획득
        }
    }
}
