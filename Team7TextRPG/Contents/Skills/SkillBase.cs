using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Creatures;

namespace Team7TextRPG.Contents.Skills
{
    public abstract class SkillBase
    {
        public int DataId { get; protected set; }
        public CreatureBase Owner { get; protected set; }
        public virtual string? Name { get; protected set; }
        public virtual string? Description { get; protected set; }
        public virtual int Level { get; protected set; }

        public SkillBase(CreatureBase owner)
        {
            Owner = owner;
        }

        public virtual void Use(CreatureBase[] targets)
        {
            Console.WriteLine($"Used {Name} on {targets.Length} targets.");
        }


    }
}
