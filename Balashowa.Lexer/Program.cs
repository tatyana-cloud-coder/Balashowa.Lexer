using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balashowa.Lexer
{
    class Program
    {
        static void Main(string[] args)
        {
            DataProvider dataProvider = new DataProvider();
            Lexer lexer = dataProvider.GetLexer();
            using (StreamReader streamReader = new StreamReader("code.txt"))
               {
                   int skip = 0;
                   var result = lexer.toRecognizeCode(streamReader.ReadToEnd(), ref skip);
                   foreach (var item in result)
                   {
                       Console.WriteLine(item);
                   }
               }
            Console.ReadLine();
            //код для первой таски
            /* DataProvider dataProvider = new DataProvider();
             var automation = dataProvider.GetAutomation("idAutomation.txt");
             while (true)
             {
                 Console.WriteLine("Введите текст для распознавания:");
                 string text = Console.ReadLine();
                 Console.WriteLine("Укажите смещение:");
                 if (int.TryParse(Console.ReadLine(), out var skip) && skip >= 0)
                 {
                     var result = automation.toRecognize(text, skip);
                     Console.WriteLine(result);
                     if (result.Key)
                     {
                         Console.WriteLine(text.Substring(skip, result.Value));
                     }
                 }
                 else
                 {
                     Console.WriteLine("Некоррректное смещение!");
                 }
                 Console.WriteLine("Для продолжения нажмите любую клавишу...");
                 Console.ReadKey(); */
        }
    }
}
