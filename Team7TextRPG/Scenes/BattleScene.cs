using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Team7TextRPG.Scenes.FieldScene;
using Team7TextRPG.Managers;
using static Team7TextRPG.Scenes.ShopScene;
using Team7TextRPG.UIs;
using System.Threading;

namespace Team7TextRPG.Scenes
{
    public class BattleScene : SceneBase
    {
        public enum BattleSceneType
        {
            None,
            Attack,
            Skill,
            UseItem,
            Run,
        }

        ////////////////임시로  몬스터를 만들어놨습니다////////////////////
        ///////////////////////////////////////////////////////////////////
        private Creature player;
        private Monster enemy;
        private Random random = new Random();
        private bool battleEnded = false; 

        public BattleScene()
        {
            this.player = new Creature("기본 플레이어", 100);
            this.enemy = new Monster("기본 몬스터", 100);
        }
        public BattleScene(Creature player, Monster enemy)
        {
            this.player = player;
            this.enemy = enemy;
        }

        // 전투 진행
        public override void Show()
        {
            Console.Clear();
            Console.WriteLine("=== 전투 시작 ===");
            Console.WriteLine($"{enemy.Name} 을(를) 발견했다!");

            // 턴제배틀
            while (!battleEnded && !player.IsDead && !enemy.IsDead)
            {
                //플레이어랑 몬스터랑 속도 비례 우선권 추가할 것
                PlayerTurn();


                if (battleEnded) return;

                // 몬스터가 죽었으면 전투 종료
                if (enemy.IsDead)
                {
                    Console.WriteLine($"{enemy.Name} 을(를) 물리쳤습니다!");
                    EndBattle();
                    return;
                }

                // 몬스터 턴
                MonsterTurn();

                // 플레이어가 죽었으면 전투 종료
                if (player.IsDead)
                {
                    Console.WriteLine("플레이어가 사망했습니다.");
                    Thread.Sleep(2000);
                    //사망하고나면 어떻게할지?
                    return;
                }
            }
        }

        // 플레이어 턴 처리
        private void PlayerTurn()
        {
            Console.WriteLine();
            Console.WriteLine("=== 당신의 턴 ===");
            WriteType<BattleSceneType>();
            BattleSceneType selection = InputManager.Instance.GetInputType<BattleSceneType>();

            switch (selection)
            {
                case BattleSceneType.Attack:
                    AttackEnemy(); // 플레이어가 몬스터를 공격
                    break;
                case BattleSceneType.Skill:
                    UseSkill(); // 스킬 사용
                    break;
                case BattleSceneType.UseItem:
                    UseItem(); // 아이템 사용
                    break;
                case BattleSceneType.Run:
                    RunFromBattle(); // 도망 시도
                    break;
            }
        }

        // 몬스터 턴 처리
        private void MonsterTurn()
        {
            Console.WriteLine();
            Console.WriteLine("=== 몬스터의 턴 ===");
            Thread.Sleep(1000);

            // 몬스터가 플레이어를 공격
            int damage = random.Next(5, 15);  // 몬스터의 데미지
            player.TakeDamage(damage);        // 플레이어에게 데미지 적용
            Console.WriteLine($"{enemy.Name} 이(가) {damage}의 피해를 입혔습니다!");

            Thread.Sleep(1000);
        }

        // 임시 플레이어가 몬스터를 공격
        private void AttackEnemy()
        {
            Console.WriteLine();
            Console.WriteLine($"{player.Name}이(가) {enemy.Name}을(를) 공격합니다.");
            int damage = random.Next(10, 20);  // 플레이어의 데미지
            enemy.TakeDamage(damage);          // 몬스터에게 데미지 적용
            Console.WriteLine($"{enemy.Name}에게 {damage}의 피해를 입혔습니다!");

            Thread.Sleep(1000);
        }

        // 임시 스킬 사용 (구현 필요)
        private void UseSkill()
        {
            Console.WriteLine();
            Console.WriteLine("스킬을 선택하세요: ");
            Console.WriteLine("아직 스킬 기능이 구현되지 않았습니다.");
            Thread.Sleep(1000);
        }

        // 임시 아이템 사용 (구현 필요)
        private void UseItem()
        {
            Console.WriteLine();
            Console.WriteLine("아이템을 선택하세요: ");
            Console.WriteLine("아직 아이템 기능이 구현되지 않았습니다.");
            Thread.Sleep(1000);
        }

        // 플레이어가 전투에서 도망치기
        private void RunFromBattle()
        {
            Console.WriteLine();
            int chance = random.Next(100);
            if (chance < 50)
            {
                Console.WriteLine("무사히 도망쳤습니다!");
                Thread.Sleep(1000);
                battleEnded = true; // 도망에 성공하면 전투가 끝나는판정
                SceneManager.Instance.LoadScene<FieldScene>(); 
            }
            else
            {
                Console.WriteLine("도망치기에 실패했습니다...");
                Thread.Sleep(1000);
            }
        }

        // 전투 종료 처리
        private void EndBattle()
        {
            Console.WriteLine("전투에서 승리했습니다!");
            Thread.Sleep(2000);
            battleEnded = true;
            SceneManager.Instance.LoadScene<FieldScene>();
        }

        // 선택지 텍스트 출력
        protected override string SceneTypeToText<T>(T type)
        {
            return type switch
            {
                BattleSceneType.Attack => "공격 하기",
                BattleSceneType.Skill => "스킬 사용",
                BattleSceneType.UseItem => "아이템 사용",
                BattleSceneType.Run => "도망가기",
                _ => "없음",
            };
        }
    }

    ////////////////임시로 몬스터 클래스를 만들어놨습니다////////////////////
    public class Creature
    {
        public string Name { get; set; }
        public int Hp { get; private set; }
        public int MaxHp { get; private set; }
        public bool IsDead => Hp <= 0;

        public Creature(string name, int maxHp)
        {
            Name = name;
            MaxHp = maxHp;
            Hp = maxHp;
        }

        // 데미지를 입었을 때 체력 감소
        public void TakeDamage(int damage)
        {
            Hp -= damage;
            if (Hp < 0) Hp = 0;
        }

        // 체력 회복
        public void Heal(int amount)
        {
            Hp += amount;
            if (Hp > MaxHp) Hp = MaxHp;
        }
    }

    public class Monster : Creature
    {
        public Monster(string name, int maxHp) : base(name, maxHp)
        {
        }
    }
}
