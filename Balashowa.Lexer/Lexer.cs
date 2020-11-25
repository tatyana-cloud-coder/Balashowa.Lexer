using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balashowa.Lexer
{
    public class Lexer
    {
        List<Automation> automations;
        Dictionary<Automation, string> Tokens;
        public Lexer(List<Automation> automations)
        {
            Tokens = new Dictionary<Automation, string>();
            this.automations = automations;
            Tokens.Add(automations[0], "ID");
            Tokens.Add(automations[1], "int");
            Tokens.Add(automations[2], "real");
            Tokens.Add(automations[3], "space");
            Tokens.Add(automations[4], "operation");
            Tokens.Add(automations[5], "bool");
            Tokens.Add(automations[6], "keyword");

        } 
        public HashSet <KeyValuePair<string, string>> toRecognizeCode (string inputText, ref int skip)
        {
            var resultLexer = new HashSet<KeyValuePair<string, string>>();
            while (skip < inputText.Length)
            {
                int maxCount = -1;
                Automation automation = null;
                string s = "";
                bool isRecognize = false;
                foreach (var item in automations)
                {
                    var result = item.toRecognize(inputText, skip);
                    if (result.Value != 0)
                    {
                        isRecognize = true;
                        if (result.Value > maxCount)
                        {
                            maxCount = result.Value;
                            automation = item;
                            s = inputText.Substring(skip, result.Value);
                        }
                        if (result.Value == maxCount)
                        {
                            if (item.Priority > automation.Priority)
                            {
                                automation = item;
                                s = inputText.Substring(skip, result.Value);
                            }
                        }
                    }
                } 
                if (isRecognize)
                {
                    resultLexer.Add(new KeyValuePair<string, string>(s, Tokens[automation]));
                    skip += maxCount;
                } else
                {
                    skip++;
                }
            }
            return resultLexer;
        }
    }
}
