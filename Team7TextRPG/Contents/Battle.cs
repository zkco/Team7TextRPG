using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Team7TextRPG.Creatures;
using Team7TextRPG.Managers;
using Team7TextRPG.Scenes;
using Team7TextRPG.UIs;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Contents
{
    public class Battle
    {
        private List<MonsterCreature> _monsters;
        private PlayerCreature _player;
        private bool _battleEnded;
        private bool _monsterFirstAttackMsg = false;
        private Random random = new Random();
        public enum BattleAction
        {
            None,
            Attack,
            Skill,
            Item,
            Run
        }

        public Battle(List<MonsterCreature> monsters, PlayerCreature player)
        {
            _monsters = monsters;
            _player = player;
        }
        
        public void BattleStart()
        {
            _battleEnded = false;

            // 전투 시작
            TextHelper.BtHeader("몬스터와 전투 시작");
            InputManager.Instance.GetInputEnter();



            while (_player.IsDead == false
                   && _monsters.Any(monster => monster.IsDead == false)) 
            {
                // 우선 턴 잡은 크리쳐가 먼저 공격 가능
                if (_player.Speed >= _monsters.Min(monster => monster.Speed))
                {
                    PlayerTurn();
                    if (_monsters.All(monster => monster.IsDead))
                        break;
                    MonsterTurn();
                }
                else
                {
                    // 몬스터가 먼저 공격할 때 처음에만 메시지를 출력
                    if (!_monsterFirstAttackMsg)
                    {
                        Console.WriteLine("\n몬스터가 먼저 공격합니다!");
                        _monsterFirstAttackMsg = true;
                        Thread.Sleep(2000);
                    }
                    MonsterTurn();
                    if (_player.IsDead) break; 
                    PlayerTurn();
                }
            }

            void ShowBattleStatus()
            {
                Console.Clear();
                Console.WriteLine("--- 전투 상태 ---");

                // 플레이어 상태바 출력
                UIManager.Instance.CommonStatusBar();

                Console.WriteLine("\n--------------------\n");

                // 몬스터 상태바 출력
                foreach (var monster in _monsters.Where(monster => !monster.IsDead))
                {
                    EnemyStatusBar(monster);
                }
                Console.WriteLine("\n--------------------\n");
            }

            //    // 몬스터 턴
            void MonsterTurn()
            {
                Console.Clear();
                ShowBattleStatus();
                Console.WriteLine();
                Console.WriteLine("=== 몬스터의 턴 ===");

                foreach (var monster in _monsters.Where(monster => !monster.IsDead))
                {
                    Thread.Sleep(500);

                    bool isDodged = random.NextDouble() < _player.DodgeChanceRate;
                    if (isDodged)
                    {
                        Console.ForegroundColor = ConsoleColor.White; 
                        Console.WriteLine($"{_player.Name}이(가) {monster.Name}의 공격을 회피했습니다!");
                        Console.ResetColor(); 
                        Thread.Sleep(500);
                        continue;
                    }

                    bool isCriticalHit = random.NextDouble() < monster.CriticalChanceRate;
                    int damage = isCriticalHit ? (int)(monster.Attack * 1.5) : monster.Attack;

                    damage = Math.Max(damage - _player.Defense, 1); 

                    _player.OnDamaged(damage); 

                    if (isCriticalHit)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; 
                        Console.WriteLine($"*** 치명타! *** {monster.Name}이(가) {damage}의 피해를 입혔습니다!");
                        Console.ResetColor(); 
                    }
                    else
                    {
                        Console.WriteLine($"{monster.Name}이(가) {damage}의 피해를 입혔습니다!");
                    }

                    if (_player.IsDead)
                    {
                        Console.WriteLine($"{_player.Name}이(가) 전투에 패배했습니다.");
                        break;
                    }

                    Thread.Sleep(500);
                }
                Thread.Sleep(1000);
            }

            //    // 플레이어 턴인 경우 공격, 스킬, 아이템, 도망 중 선택 가능
            void PlayerTurn()
            {
                Console.Clear();
                ShowBattleStatus();
                Console.WriteLine();
                Console.WriteLine("=== 당신의 턴 ===");

                

                // 플레이어가 선택할 수 있는 메뉴 출력
                BattleAction playerAction = GetPlayerAction();
                switch (playerAction)
                {
                    case BattleAction.Attack:
                        Attack();
                        break;
                    case BattleAction.Skill:
                        UseSkill();
                        break;
                    case BattleAction.Item:
                        UseItem();
                        break;
                    case BattleAction.Run:
                        Runaway();
                        break;
                    default:
                        Console.WriteLine("잘못된 선택입니다. 다시 선택해주세요.");
                        PlayerTurn(); 
                        break;
                }


                // 전투가 끝났는지 체크
                if (_monsters.All(monster => monster.IsDead))
                {
                    _battleEnded = true;
                }

            }

            BattleAction GetPlayerAction()
            {
                Console.WriteLine("행동을 선택하세요:");
                Console.WriteLine("1. 공격");
                Console.WriteLine("2. 스킬 사용");
                Console.WriteLine("3. 아이템 사용");
                Console.WriteLine("4. 도망가기");

                string input = Console.ReadLine() ?? string.Empty;

                return input switch
                {
                    "1" => BattleAction.Attack,
                    "2" => BattleAction.Skill,
                    "3" => BattleAction.Item,
                    "4" => BattleAction.Run,
                    _ => BattleAction.None,
                };
            }

            // 플레이어가 공격할 몬스터를 선택
            MonsterCreature SelectTarget()
            {
                Console.WriteLine("\n공격할 타겟을 선택하세요:");
                for (int i = 0; i < _monsters.Count; i++)
                {
                    if (!_monsters[i].IsDead)
                    {
                        Console.WriteLine($"{i + 1}. {_monsters[i].Name} - HP: {_monsters[i].Hp}/{_monsters[i].MaxHp}");
                    }
                }

                int selectedIndex;
                while (!int.TryParse(Console.ReadLine(), out selectedIndex) || selectedIndex < 1 || selectedIndex > _monsters.Count || _monsters[selectedIndex - 1].IsDead)
                {
                    Console.WriteLine("잘못된 선택입니다. 다시 선택해주세요.");
                }

                return _monsters[selectedIndex - 1];
            }

            // 공격
            void Attack()
            {
                {
                    MonsterCreature targetMonster = SelectTarget();
                    bool isCriticalHit = random.NextDouble() < _player.CriticalChanceRate;
                    int damage = isCriticalHit ? (int)(_player.Attack * 1.5) : _player.Attack;
                    damage = Math.Max(damage - targetMonster.Defense, 1); 
                    targetMonster.OnDamaged(damage);

                    if (isCriticalHit)
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow; 
                        Console.WriteLine($"*** 치명타! *** {_player.Name}이(가) {targetMonster.Name}에게 {damage}의 피해를 입혔습니다!");
                        Console.ResetColor(); 
                    }
                    else
                    {
                        Console.WriteLine($"{_player.Name}이(가) {targetMonster.Name}에게 {damage}의 피해를 입혔습니다!");
                    }

                    Thread.Sleep(1000);

                    if (targetMonster.IsDead)
                    {
                        Console.WriteLine($"{targetMonster.Name}이(가) 쓰러졌습니다.");
                        Thread.Sleep(1000);
                    }
                }
            }

            // 스킬 사용
            void UseSkill()
            {
                Skill? selectedSkill = UIManager.Instance.SkillRead();

                if (selectedSkill == null)
                {
                    Console.WriteLine("스킬을 선택하지 않았습니다.");
                    return;
                }

                if (_player.Mp < selectedSkill.MpCost)
                {
                    Console.WriteLine($"{_player.Name}의 MP가 부족합니다. (필요 MP: {selectedSkill.MpCost})");
                    return;
                }

                MonsterCreature targetMonster = SelectTarget();
                bool skillSuccess = selectedSkill.Use(new CreatureBase[] { targetMonster });

                if (skillSuccess)
                {
                    _player.UseMp(selectedSkill.MpCost);
                    Console.WriteLine($"{_player.Name}이(가) {selectedSkill.Name} 스킬을 사용했습니다!");

                    if (selectedSkill.SkillType == Defines.SkillType.Attack)
                    {
                        int totalDamage = selectedSkill.ValueType == Defines.SkillValueType.Absolute
                            ? selectedSkill.Value
                            : (int)(_player.Attack * (selectedSkill.Value / 100.0));

                        Console.WriteLine($"{targetMonster.Name}에게 {totalDamage}의 피해를 입혔습니다.");
                    }
                    else if (selectedSkill.SkillType == Defines.SkillType.Heal)
                    {
                        int healAmount = selectedSkill.ValueType == Defines.SkillValueType.Absolute
                            ? selectedSkill.Value
                            : (int)(_player.MaxHp * (selectedSkill.Value / 100.0));

                        Console.WriteLine($"{_player.Name}이(가) {healAmount}만큼 치유되었습니다.");
                    }
                }
                else
                {
                    Console.WriteLine($"{selectedSkill.Name} 사용에 실패했습니다.");
                }

                Thread.Sleep(1000);
            }

            //아이템 사용

            void UseItem()
            {
                UIManager.Instance.Write<InventoryUI>();
            }

            // 도망
            void Runaway()
            {
                Console.WriteLine();
                Random random = new Random();
                int chance = random.Next(100);
                if (chance < 50)
                {
                    _battleEnded = true; 
                }
                else
                {
                    Console.WriteLine("도망치기에 실패했습니다...");
                    Thread.Sleep(1000);
                }
            }


 
            //   if (_monsters.All(monster => monster.IsDead))
            //    {

            //        Thread.Sleep(2000);

            //        // 전투 승리 시 보상 획득
            //        foreach (var monster in _monsters)
            //        {
            //          monster.OnDead(); 
            //        }


            void EnemyStatusBar(MonsterCreature monster)
            {
                Console.WriteLine($"{monster.Name} - HP: {monster.Hp}/{monster.MaxHp}");
            }
        }
    }
}
