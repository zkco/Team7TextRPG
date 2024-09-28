using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team7TextRPG.Utils
{
    public static class TextHelper
    {
        public enum TextType
        {
            None,
            Commentary, // 해설
            Dialogue, // 대화
            Interface, // 인터페이스 정보
            Banner, // 배너
        }

        public static void Write(string message, params object?[]? args)
        {
            Console.Write(message, args);
        }

        public static void WriteLine(string message, params object?[]? args)
        {
            Write(message, args);
        }

        private static void SetType(TextType type)
        {
            switch (type)
            {
                case TextType.Commentary:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case TextType.Dialogue:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case TextType.Interface:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case TextType.Banner:
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                default:
                    Console.ResetColor();
                    break;
            }
        }

        // TextType에 따라 출력되는 글자 색이 다르게 표시 됩니다.
        // Header 는 TextType에 따라 출력되는 글자가 차이가 있습니다.
        public static void Header(TextType type, string message)
        {
            SetType(type);
            switch (type)
            {
                case TextType.Commentary:
                    Console.WriteLine($"( {message} )");
                    break;
                case TextType.Dialogue:
                    Console.WriteLine($"{{ {message} }}");
                    break;
                case TextType.Interface:
                    Console.WriteLine($"[ {message} ]");
                    break;
                case TextType.Banner:
                    Console.WriteLine($"! {message} !");
                    break;
                default:
                    Console.WriteLine(message);
                    break;
            }
        }
        // CommentaryTextHeader = CtHeader
        public static void CtHeader(string message) => Header(TextType.Commentary, message);
        // DialogueTextHeader = DtHeader
        public static void DtHeader(string message) => Header(TextType.Dialogue, message);
        // InterfaceTextHeader = ItHeader
        public static void ItHeader(string message) => Header(TextType.Interface, message);
        // BannerTextHeader = BtHeader
        public static void BtHeader(string message) => Header(TextType.Banner, message);

        // Content는 TextType에 따라 출력되는 글자 색이 다르게 표시 됩니다.
        public static void Content(TextType type, string message, params object?[]? args)
        {
            SetType(type);
            Console.WriteLine($"{message}", args);
            Console.ResetColor();
        }
        // CommentaryTextContent = CtContent
        public static void CtContent(string message, params object?[]? args) => Content(TextType.Commentary, message, args);
        // DialogueTextContent = DtContent
        public static void DtContent(string message, params object?[]? args) => Content(TextType.Dialogue, message, args);
        // InterfaceTextContent = ItContent
        public static void ItContent(string message, params object?[]? args) => Content(TextType.Interface, message, args);
        // BannerTextContent = BtContent
        public static void BtContent(string message, params object?[]? args) => Content(TextType.Banner, message, args);

        public static void PageWrite(string text)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[ {text} ]");
            Console.ResetColor();
        }
        public static void StatusBar(string text)
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"[ {text} ]");
            Console.ResetColor();
        }
    }
}
