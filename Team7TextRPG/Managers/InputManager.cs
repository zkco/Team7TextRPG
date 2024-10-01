using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team7TextRPG.Utils;
using static Team7TextRPG.Utils.Defines;

namespace Team7TextRPG.Managers
{
    public class InputManager
    {
        private static InputManager? _instance;
        public static InputManager Instance => _instance ??= new InputManager();

        private string[] _keywords = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "I", "S", "K", "Q" };

        // string을 입력받아 반환하는 함수
        public string GetInputString(string message = "키워드를 입력하세요.")
        {
            Console.WriteLine(message);
            return Console.ReadLine() ?? String.Empty;
        }
        public void GetInputEnter(string message = "계속하려면 엔터를 누르세요.")
        {
            while (true)
            {
                Console.WriteLine(message);
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Enter)
                    break;
            }
        }
        // 입력받은 문자열을 대문자로 변환하고 _keyword에 해당하는지 확인해서 반환하는 함수
        public string GetInputKeyword(string message = "키워드를 입력하세요.")
        {
            while (true)
            {
                string input = GetInputString(message).ToUpper();
                if (_keywords.Contains(input) == false)
                {
                    TextHelper.DtContent("잘못 된 입력입니다.");
                    continue;
                }

                return input;
            }
        }
        // 입력받은 문자열을 int로 변환해서 반환하는 함수
        public int GetInputInt(string message = "숫자를 입력하세요.", int minValue = 0, int maxValue = 9)
        {
            while (true)
            {
                if (int.TryParse(GetInputString(message), out int value) == false)
                {
                    TextHelper.DtContent("잘못 된 입력입니다.");
                    continue;
                }

                if (value < minValue || value > maxValue)
                {
                    TextHelper.DtContent("잘못 된 입력입니다.");
                    continue;
                }

                return value;
            }
        }
        // 입력받은 문자열을 enum으로 변환해서 반환하는 함수
        public T GetInputType<T>(string message = "숫자를 입력하세요.", int defaultValue = 0) where T : Enum
        {
            try
            {
                int value = GetInputInt(message);
                return (T)Enum.ToObject(typeof(T), value);
            }
            catch
            {
                return (T)Enum.ToObject(typeof(T), defaultValue);
            }
        }

        public T ParseInputType<T>(string input) where T : Enum
        {
            if (int.TryParse(input, out int value) == false)
            {
                TextHelper.DtContent("잘못 된 입력입니다.");
                return (T)Enum.ToObject(typeof(T), 0);
            }

            T[] values = (T[])Enum.GetValues(typeof(T));
            if (values.Length <= value || value <= 0)
            {
                TextHelper.DtContent("잘못 된 입력입니다.");
                return (T)Enum.ToObject(typeof(T), 0);
            }

            return (T)Enum.ToObject(typeof(T), value);
        }
    }
}
