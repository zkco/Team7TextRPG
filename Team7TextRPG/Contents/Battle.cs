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
        private bool battleEnded;

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
            battleEnded = false;

            // 전투 시작
            TextHelper.BtHeader("몬스터와 전투 시작");
            InputManager.Instance.GetInputEnter();

            //while (true)
            while (battleEnded == false
                && _player.IsDead == false
                && _monsters.Any(monster => monster.IsDead == false))
            {
                //    // 우선 턴 잡은 크리쳐가 먼저 공격 가능
                if (_player.Speed >= _monsters.Min(monster => monster.Speed))
                {
                    PlayerTurn(); 
                    if (_monsters.All(monster => monster.IsDead)) 
                        break; 
                    MonsterTurn(); 
                }
                else
                {
                    MonsterTurn(); // 몬스터 턴 먼저 진행
                    if (_player.IsDead) break; // 플레이어 사망 시 전투 종료
                    PlayerTurn(); // 플레이어 턴 진행
                }
            }

            // 전투 종료 처리
            EndBattle();



            //    // 몬스터 턴인 경우 몬스터가 플레이어 공격
            void MonsterTurn()
            {
                Console.WriteLine();
                Console.WriteLine("=== 몬스터의 턴 ===");

                // 살아있는 몬스터만 턴을 가짐
                foreach (var monster in _monsters.Where(monster => !monster.IsDead)) 
                {
                    EnemyStatusBar(monster); // 개별 몬스터 상태 출력

                    Thread.Sleep(1000);

                    // 각 몬스터의 공격력
                    int damage = monster.Attack; 
                    _player.OnDamaged(damage); // 플레이어에게 데미지 적용

                    Console.WriteLine($"\n{monster.Name} 이(가) {damage}의 피해를 입혔습니다!");

                    if (_player.IsDead)
                    {
                        Console.WriteLine($"{_player.Name} 이(가) 쓰러졌습니다.");
                        break; 
                    }

                    Thread.Sleep(2000);
                }

            }

            //    // 플레이어 턴인 경우 공격, 스킬, 아이템, 도망 중 선택 가능
            void PlayerTurn()
            {
                Console.WriteLine();
                Console.WriteLine("=== 당신의 턴 ===");

                UIManager.Instance.CommonStatusBar();

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
                    battleEnded = true;
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
                Console.WriteLine("공격할 타겟을 선택하세요:");
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
                MonsterCreature targetMonster = SelectTarget();

                int damage = _player.Attack;
                targetMonster.OnDamaged(damage);

                Console.WriteLine($"{_player.Name}이(가) {targetMonster.Name}에게 {damage}의 피해를 입혔습니다!");

                if (targetMonster.IsDead)
                {
                    Console.WriteLine($"{targetMonster.Name}이(가) 쓰러졌습니다.");
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

                Thread.Sleep(2000);
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
                    battleEnded = true; 
                }
                else
                {
                    Console.WriteLine("도망치기에 실패했습니다...");
                    Thread.Sleep(1000);
                }
            }


            //// 누군가 사망하면 전투 종료 (몬스터는 전부 사망)
            void EndBattle()
            {
                if (_player.IsDead)
                {
                    Console.WriteLine("플레이어가 사망했습니다.");
                    Thread.Sleep(2000);
                }
                else if (_monsters.All(monster => monster.IsDead))
                {
                    Console.WriteLine($"\n\n모든 몬스터를 물리쳤습니다!\n");
                    Thread.Sleep(2000);
                    // 전투 보상 처리
                }
                battleEnded = true;
                SceneManager.Instance.LoadScene<FieldScene>(); // 필드로 돌아가기
            }

            void EnemyStatusBar(MonsterCreature monster)
            {
                Console.WriteLine($"{monster.Name} - HP: {monster.Hp}/{monster.MaxHp}");
            }

            // 전투 승리 시 보상 획득



        }
    }
}
