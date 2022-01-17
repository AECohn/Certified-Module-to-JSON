using System;
using System.IO;
using System.IO.Compression;

//using Newtonsoft.Json;

namespace Certified_Module_to_JSON
{
     class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Add pkg file");
            string File_Location = Console.ReadLine().Trim('"');

            string Zip_File = Path.ChangeExtension(File_Location, ".zip").Trim('"');

            File.Copy(File_Location, Zip_File);

            ZipFile.ExtractToDirectory(Zip_File, Path.GetDirectoryName(Zip_File));

            byte[] bytes = System.IO.File.ReadAllBytes(Path.ChangeExtension(File_Location, ".dll").Trim('"'));
            string text = System.Text.Encoding.UTF8.GetString(bytes);

            File.Delete(Zip_File);
            File.Delete(Path.ChangeExtension(Path.GetFullPath(File_Location), ".dll"));
            File.Delete(Path.ChangeExtension(Path.GetFullPath(File_Location), ".dat"));

            //Console.WriteLine(text);

            string Protocol = "";

            int From = text.IndexOf(@"{""CrestronSerialDeviceApi""");
            int To = text.IndexOf("}}]}}");

            if (From <= 0)

            {
                From = text.IndexOf("{");
                To = text.IndexOf("BSJB");
                Protocol = text.Substring(From, To-From-3);
            }
            else
            {
                Protocol = text.Substring(From, To - From + 6);
            }

            File.WriteAllText(Path.ChangeExtension(Path.GetFullPath(File_Location), "_Protocol.json"), Protocol);

            //Console.WriteLine(Protocol);
        }
    }
}