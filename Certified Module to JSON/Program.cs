using System;
using System.IO;
using System.IO.Compression;

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

            Console.WriteLine(text);
        }
    }
}
