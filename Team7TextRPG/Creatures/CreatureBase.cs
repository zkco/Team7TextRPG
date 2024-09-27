using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Creatures
{
    public abstract class CreatureBase
    {
        public string? Name { get; protected set; }
        public int Level { get; protected set; }

        public Defines.SpecisType SpecisType { get; protected set; }
        public Defines.SexType SexType { get; protected set; }
        public Defines.JobType JobType { get; protected set; } = Defines.JobType.None;
        public CreatureStat BaseStat { get; protected set; } = new CreatureStat();

        public virtual int StatStr => BaseStat.StatStr;
        public virtual int StatDex => BaseStat.StatDex;
        public virtual int StatInt => BaseStat.StatInt;
        public virtual int StatLuck => BaseStat.StatLuck;

        public virtual int MaxHp => BaseStat.MaxHp;
        public virtual int MaxMp => BaseStat.MaxMp;

        public virtual int Attack => BaseStat.Attack + CalcJobAttack();
        public virtual int Defense => BaseStat.Defense + CalcJobDefense();
        public virtual int Speed => BaseStat.Speed + CalcJobSpeed();
        public virtual double DodgeChanceRate => BaseStat.DodgeChanceRate + CalcJobDodgeChanceRate();
        public virtual double CriticalChanceRate => BaseStat.CriticalChanceRate + CalcJobCriticalChanceRate();

        protected Random random = new Random();

        public abstract void SetInfo(Defines.JobType job);
        public abstract void LevelUp();
        public abstract void OnDamaged(int damage);
        public abstract void OnHealed(int heal);
        public abstract void OnDead();

        public int CalcJobAttack()
        {
            return JobType switch
            {
                Defines.JobType.Warrior => StatStr * 2,
                Defines.JobType.Archer => StatDex * 2,
                Defines.JobType.Mage => StatInt * 2,
                _ => StatStr,
            };
        }
        public int CalcJobDefense()
        {
            return JobType switch
            {
                Defines.JobType.Warrior => StatStr + Level,
                Defines.JobType.Archer => StatStr + Level,
                Defines.JobType.Mage => StatStr + Level,
                _ => Level,
            };
        }
        public int CalcJobSpeed()
        {
            return JobType switch
            {
                Defines.JobType.Warrior => StatDex + Level,
                Defines.JobType.Archer => StatDex + Level,
                Defines.JobType.Mage => StatDex + Level,
                _ => Level,
            };
        }
        public double CalcJobDodgeChanceRate()
        {
            return JobType switch
            {
                Defines.JobType.Warrior => StatDex * 0.001,
                Defines.JobType.Archer => StatDex * 0.002,
                Defines.JobType.Mage => StatDex * 0.001,
                _ => 0.01,
            };
        }
        public double CalcJobCriticalChanceRate()
        {
            return JobType switch
            {
                Defines.JobType.Warrior => StatLuck * 0.002,
                Defines.JobType.Archer => StatLuck * 0.003,
                Defines.JobType.Mage => StatLuck * 0.002,
                _ => 0.01,
            };
        }
    }
}
