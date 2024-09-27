using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;

namespace Team7TextRPG.Scenes
{
    public abstract class SceneBase
    {
        protected abstract string SceneTypeToText<T>(T type) where T : Enum;
        public abstract void Show();

        protected virtual void WriteType<T>() where T : Enum
        {
            Type t = typeof(T);
            string[]? types = Enum.GetNames(t);
            for (int i = 0; i < types?.Length; i++)
            {
                if (i == 0) continue;
                object obj = Enum.Parse(t, types[i]);
                TextHelper.ItContent($"{i}. {SceneTypeToText((T)obj)}");
            }
        }
        protected virtual void WriteMessage(string message)
        {
            Console.WriteLine(message);
        }

    }
}
