using System;
using System.IO;
using System.Text;

namespace NovaLox
{
    public class Lox
    {
        public static bool ErrorOccured { get; private set; }

        /// <summary>
        /// Determine if the user is trying to interpret a file or use an interactive REPL
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                Console.WriteLine("Usage: nova [script]");
                Environment.Exit(64);
            }
            else if (args.Length == 1)
            {
                RunFile(args[0]);
            }
            else
            {
                RunPrompt();
            }
        }

        /// <summary>
        /// Read bytes from a file and pass them to the executer
        /// </summary>
        /// <param name="filePath"></param>
        private static void RunFile(string filePath)
        {
            var bytes = File.ReadAllBytes(filePath);
            var file = System.Text.Encoding.UTF8.GetString(bytes);
            Run(file);

            if (ErrorOccured) Environment.Exit(65);
        }

        /// <summary>
        /// Keep reading lines from the command prompt and executing them
        /// </summary>
        private static void RunPrompt()
        {
            while (true)
            {
                Console.Write("> ");
                Run(Console.ReadLine());
                ErrorOccured = false;
            }
        }

        /// <summary>
        /// Execute the given source code
        /// </summary>
        /// <param name="source"></param>
        private static void Run(string source)
        {
            var scanner = new Scanner(source);
            var tokens = scanner.ScanTokens();
            var parser = new Parser(tokens);
            var expression = parser.Parse();

            // Stop if there was a syntax error
            if (ErrorOccured)
                return;

            Console.WriteLine(new AstPrinter().Print(expression));
        }

        private static void Report(int line, string where, string message)
        {
            Console.Error.WriteLine(String.Format("[line {0}] Error{1}: {2}", line, where, message));
            ErrorOccured = true;
        }

        public static void Error(int line, string message)
        {
            Report(line, "", message);
        }

        internal static void Error(Token token, string message)
        {
            if (token.Type == TokenType.EOF)
                Report(token.Line, " at end", message);
            else
                Report(token.Line, " at '" + token.Lexeme + "'", message);
        }
    }
}
