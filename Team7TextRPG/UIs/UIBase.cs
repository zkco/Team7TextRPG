using Team7TextRPG.Utils;

namespace Team7TextRPG.UIs
{
    public abstract class UIBase
    {
        public abstract void Write();
        protected abstract string EnumTypeToText<T>(T type) where T : Enum;

        protected virtual void WriteType<T>() where T : Enum
        {
            Type t = typeof(T);
            string[]? types = Enum.GetNames(t);
            for (int i = 0; i < types?.Length; i++)
            {
                if (i == 0) continue;
                object obj = Enum.Parse(t, types[i]);
                TextHelper.ItContent($"{i}. {EnumTypeToText((T)obj)}");
            }
        }
    }
}
