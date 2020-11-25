using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Balashowa.Lexer
{
    public class DataProvider
    {
        public Automation GetAutomation(string fileName)
        {
           Automation automation = null;
           using (StreamReader streamReader = new StreamReader(fileName))
            {
                string s = streamReader.ReadToEnd();
                automation = JsonConvert.DeserializeObject<Automation>(s);
            }
            return automation;
        }

        public Lexer GetLexer()
        {
            var result = new List<Automation>();
            var automationByIdObject = GetAutomation("idAutomation.txt");
            result.Add(automationByIdObject);

            var automationByIntObject = GetAutomation("intAutomation.txt");
            result.Add(automationByIntObject);

            var automationByrealObject = GetAutomation("realAutomation.txt");
            result.Add(automationByrealObject);

            var automationBySpaceObject = GetAutomation("spaceAutomation.txt");
            result.Add(automationBySpaceObject);

            var automationByOperatorObject = GetAutomation("operatorAutomation.txt");
            result.Add(automationByOperatorObject);

            var automationByBoolObject = GetAutomation("boolAutomation.txt");
            result.Add(automationByBoolObject);

            var automationByWordObject = GetAutomation("keywordAutomation.txt");
            result.Add(automationByWordObject);

            return new Lexer(result);
        }
    }
}
