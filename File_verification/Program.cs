using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace File_verification
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"c:\temp\MyTest.txt";
            string str = "Hello world";

            var crypt = new SHA256Managed();
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(str));
            string hash = String.Empty;

            foreach(byte b in crypto)
            {
                hash += b.ToString("X2");
            }
            Console.WriteLine(hash);

            try
            {
                File.WriteAllText(path,str);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.Write("Créate and Write done");

            //attente d une action, permet de modiier le fichier .txt pour les essais de verification
            Console.ReadKey();

            Console.WriteLine("Read file and hash");

            try
            {
                if (File.Exists(path))
                {
                    string text = File.ReadAllText(path);
                    Console.WriteLine("text :" + text);
                    byte[] cryptoText = crypt.ComputeHash(Encoding.ASCII.GetBytes(text));
                    string hashText = String.Empty;
                    foreach(byte b in cryptoText)
                    {
                        hashText += b.ToString("X2");
                    }

                    Console.Write("File read and hash done");

                    Console.WriteLine(hashText);

                    if (hash == hashText)
                    {
                        Console.WriteLine("authentic file ");
                    }
                    else
                    {
                        Console.Write("File has been modified");
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }




            Console.ReadKey();
        }
    }
}
