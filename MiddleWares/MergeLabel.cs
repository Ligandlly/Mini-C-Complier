using System;
using System.Collections.Generic;
using System.Text;

namespace MiddleWares
{
    public class MergeLabel
    {

        public static string Merge(string ir)
        {
            Dictionary<string, string> replace = new();
            var isDuplicate = false;
            var firstLabel = "";
            
            foreach (var line in ir.Split(new[] { '\n', '\r' })) 
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (line.EndsWith(':'))
                {
                    if (isDuplicate == false)
                    {
                        isDuplicate = true;
                        firstLabel = line[0..^1];
                    }
                    else
                    {
                        replace.Add(line[0..^1], firstLabel);
                    }
                }
                else
                    isDuplicate = false;
            }


            StringBuilder stringBuilder = new();
            isDuplicate = false;
            foreach (var line in ir.Split(new[] { '\r', '\n' }))
            {
                if (string.IsNullOrEmpty(line)) continue;
                if (line.EndsWith(':'))
                {
                    if (isDuplicate == false)
                    {
                        isDuplicate = true;
                        stringBuilder.AppendLine(line);
                    }
                    else
                    {
                        continue;
                    }
                }
                else
                {
                    isDuplicate = false;
                    var tmp = line;
                    foreach (var (key, value) in replace)
                    {
                      tmp = tmp.Replace(key, value);
                    }

                    stringBuilder.AppendLine(tmp);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
