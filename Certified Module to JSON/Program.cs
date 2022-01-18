using Newtonsoft.Json;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;

namespace Certified_Module_to_JSON
{
    internal class Program
    {
    
        private static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("Please drag .pkg file into console window to extract protocol");
                string File_Location = Console.ReadLine().Trim('"');

                string zip = Path.ChangeExtension(File_Location, ".zip").Trim('"');
                string dll = Path.ChangeExtension(File_Location, ".dll").Trim('"');
                string dat = Path.ChangeExtension(File_Location, ".dat").Trim('"');
                string pdf = Path.ChangeExtension(File_Location, ".pdf").Trim('"');
                string json = Path.ChangeExtension(File_Location, ".json").Trim('"');

                File.Copy(File_Location, zip);

                ZipFile.ExtractToDirectory(zip, Path.GetDirectoryName(zip));

                byte[] bytes = System.IO.File.ReadAllBytes(dll);
                string text = System.Text.Encoding.UTF8.GetString(bytes);

                File.Delete(zip);
                File.Delete(dll);
                File.Delete(dat);
                if (File.Exists(pdf))
                {
                    File.Delete(pdf);
                }

                if (!File.Exists(json))
                {
                    try
                    {
                        string Protocol;

                        int From = text.IndexOf(@"{""CrestronSerialDeviceApi""");
                        int To = text.IndexOf("}}]}}");

                        if (From <= 0)

                        {
                            From = text.IndexOf("{");
                            To = text.IndexOf("BSJB");
                            Protocol = text.Substring(From, To - From - 3);
                        }
                        else
                        {
                            Protocol = text.Substring(From, To - From + 5);
                        }
                        Console.WriteLine("Json extracted from dll");
                        File.WriteAllText(json, FormatJson(Protocol));
                    }
                    catch (Exception ex)
                    { 
                        Console.WriteLine(ex.Message); 
                    }
                }
            }
        }

        public static string FormatJson(string json, string indent = "  ") //Created by yallie (https://stackoverflow.com/a/57100143)
        {
            var indentation = 0;
            var quoteCount = 0;
            var escapeCount = 0;

            var result =
                from ch in json ?? string.Empty
                let escaped = (ch == '\\' ? escapeCount++ : escapeCount > 0 ? escapeCount-- : escapeCount) > 0
                let quotes = ch == '"' && !escaped ? quoteCount++ : quoteCount
                let unquoted = quotes % 2 == 0
                let colon = ch == ':' && unquoted ? ": " : null
                let nospace = char.IsWhiteSpace(ch) && unquoted ? string.Empty : null
                let lineBreak = ch == ',' && unquoted ? ch + Environment.NewLine + string.Concat(Enumerable.Repeat(indent, indentation)) : null
                let openChar = (ch == '{' || ch == '[') && unquoted ? ch + Environment.NewLine + string.Concat(Enumerable.Repeat(indent, ++indentation)) : ch.ToString()
                let closeChar = (ch == '}' || ch == ']') && unquoted ? Environment.NewLine + string.Concat(Enumerable.Repeat(indent, --indentation)) + ch : ch.ToString()
                select colon ?? nospace ?? lineBreak ?? (
                    openChar.Length > 1 ? openChar : closeChar
                );

            return string.Concat(result);
        }
    }
}