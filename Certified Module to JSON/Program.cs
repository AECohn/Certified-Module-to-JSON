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
            string File_Location = Console.ReadLine();

            string new_file = Path.ChangeExtension(File_Location, ".zip");


            Console.WriteLine(File_Location);
            Console.WriteLine(File_Location.Trim());
            Console.WriteLine(new_file);    
            Console.WriteLine(new_file.Trim());


            //File.Move(File_Location, new_file);

            File.Copy(File_Location.Trim('"'), new_file.Trim('"'));
        }
    }
}
