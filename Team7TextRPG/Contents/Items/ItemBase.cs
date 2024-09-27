using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7TextRPG.Contents.Items
{
    /// <summary>
    /// 아이템 정보를 미리 정의 해보는 추상 클래스입니다.
    /// </summary>
    public abstract class ItemBase
    {
        public virtual string? Name { get; protected set; }
        public virtual string? Description { get; protected set; }
        public virtual int Price { get; protected set; }

        public virtual void Use()
        {
            Console.WriteLine($"아이템 {Name}을 사용했습니다.");
        }

        public override string ToString()
        {
            return $"{Name} - {Description} - 가격: {Price}골드";
        }
    }
}
