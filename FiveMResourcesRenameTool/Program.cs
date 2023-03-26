using System;
using System.IO;
using System.Collections.Generic;

namespace FiveMResourcesRenameTool
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = "";
            string oldPrefix = "";
            string newPrefix = "";
            Console.Title = "FiveMResourcesRenameTool";

            while (string.IsNullOrEmpty(path) || !Directory.Exists(path))
            {
                Console.WriteLine("Enter the path of the \"resources\" folder:");
                path = Console.ReadLine();
                if (!Directory.Exists(path))
                {
                    Console.WriteLine("Invalid path. Please try again.");
                }
            }

            while (string.IsNullOrEmpty(oldPrefix))
            {
                Console.WriteLine("Enter the prefix to be replaced:");
                oldPrefix = Console.ReadLine();
            }

            string invalidChars = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
            while (string.IsNullOrEmpty(newPrefix) || newPrefix.IndexOfAny(invalidChars.ToCharArray()) >= 0)
            {
                Console.WriteLine("Enter the new prefix that will be added:");
                newPrefix = Console.ReadLine();
                if (newPrefix.IndexOfAny(invalidChars.ToCharArray()) >= 0)
                {
                    Console.WriteLine("The prefix contains invalid characters. Please try again.");
                    newPrefix = string.Empty;
                }
            }

            var foldersToRename = new List<string>();
            var renamedFolders = new Dictionary<string, string>();

            foreach (var dir in Directory.GetDirectories(path, "*", SearchOption.AllDirectories))
            {
                if (File.Exists(Path.Combine(dir, "fxmanifest.lua")) || File.Exists(Path.Combine(dir, "__resource.lua")))
                {
                    foldersToRename.Add(dir);
                }
            }

            foreach (var dir in foldersToRename)
            {
                var dirInfo = new DirectoryInfo(dir);
                if (dirInfo.Name.StartsWith(oldPrefix))
                {
                    int index = dirInfo.Name.IndexOf("-");
                    string newName;
                    if (index >= 0)
                    {
                        newName = newPrefix + dirInfo.Name.Substring(index);
                    }
                    else
                    {
                        newName = dirInfo.Name.Replace(oldPrefix, newPrefix);
                    }
                    var newPath = Path.Combine(dirInfo.Parent.FullName, newName);
                    Directory.Move(dir, newPath);
                    renamedFolders.Add(dirInfo.Name, newName);
                }
            }

            foreach (var file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
            {
                if (file.EndsWith(".lua") || file.EndsWith(".js"))
                {
                    var content = File.ReadAllText(file);
                    foreach (var entry in renamedFolders)
                    {
                        content = content.Replace(entry.Key, entry.Value);
                    }
                    File.WriteAllText(file, content);
                }
            }
        }
    }
}