﻿using System;
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
        }
    }
}
