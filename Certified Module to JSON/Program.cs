using System;
using System.IO;
using System.IO.Compression;
//using Newtonsoft.Json;
 

namespace Certified_Module_to_JSON
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Add pkg file");
            string File_Location = Console.ReadLine().Trim('"');

            string new_file = Path.ChangeExtension(File_Location, ".zip").Trim('"');

            File.Copy(File_Location, new_file);

            ZipFile.ExtractToDirectory(new_file, Path.GetDirectoryName(new_file));

            byte[] bytes = System.IO.File.ReadAllBytes(Path.ChangeExtension(File_Location, ".dll").Trim('"'));
            string text = System.Text.Encoding.UTF8.GetString(bytes);

            //Console.WriteLine(text);

            string Protocol;

            int From = text.IndexOf(@"{""CrestronSerialDeviceApi""");

            int To = text.IndexOf("}}]}}");

            //int To = text.IndexOf("\x02\x00\x00\x00");

            Protocol = text.Substring(From, To-From+6);

            //Console.WriteLine(Path.GetDirectoryName(new_file));

            File.WriteAllText(Path.ChangeExtension(Path.GetFullPath(File_Location), ".json"), Protocol);
            

            //Console.WriteLine(test);

        }
    }
}
