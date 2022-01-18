using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.IO.Compression;

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

                if (Path.GetExtension(File_Location) != ".pkg")
                {
                    continue;
                }

                if (File.Exists(json))
                {
                    Console.WriteLine("this json has already been extracted");
                    continue;
                }

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
                            Protocol = text.Substring(From, To - From ); //This seems table, but, consider finding location of '}' before BSJB, and subtracting appropriate amount. Otherwise, consider using stacks (track {} and []) to evaluate when JSON ends
                        }
                        else
                        {
                            Protocol = text.Substring(From, To - From + 5);
                        }
                        Console.WriteLine("Json extracted from dll");
                        File.WriteAllText(json, JToken.Parse(Protocol).ToString());
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Json extracted from pkg");
                }
            }
        }
    }
}