using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace valasztas_BZ
{
    class valasztas
    {
        public int v_ker { get; private set; }
        public int szavazatok { get; private set; }
        public string nev { get; private set; }
        public string partok { get; private set; }

        public valasztas(string sor)
        {
            string[] db = sor.Split();
            v_ker = int.Parse(db[0]);
            szavazatok = int.Parse(db[1]);
            nev = db[2] + " " + db[3];
            partok = db[4];
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string[] partok = {"GYEP", "HEP", "TISZ", "ZEP", "-"};
            const int osszavazo = 12345;
            List<valasztas> v = new List<valasztas>();
            //1.feladat: beolvasás
            foreach (var i in File.ReadAllLines("szavazatok.txt"))
            {
                v.Add(new valasztas(i));
            }

            //2.feladat
            Console.WriteLine($"A helyhatósági választáson {v.Count} képviselőjelölt indult.");

            //3.feladat
            Console.Write("Kérem adja meg a képviselőjelölt nevét: ");
            string nev = Console.ReadLine();
            bool igaz = true;
            foreach (var i in v)
            {
                if (nev == i.nev)
                {
                    Console.WriteLine($"{i.szavazatok} kapott.");
                    igaz = false;
                    break;
                }
            }
            if (igaz)
            {
                Console.WriteLine("Ilyen nevű képviselőjelölt nem szerepel a nyilvántartásban!");
            }

            //4.feladat
            int osszes = 0;
            foreach (var i in v)
            {
                osszes += i.szavazatok;
            }
            double arany = osszes * 100.0 / osszavazo;
            Console.WriteLine($"A választáson {osszes} állampolgár, a jogosultak {arany.ToString("0.00")}%-a vett részt.");

            //5.feladat
            int[] partszavazatok = new int[partok.Length];
            for (int j = 0;j<partok.Length;j++)
            {
                int oszzes = 0;
                foreach (var i in v)
                {
                    if (partok[j] == i.partok)
                    {
                        osszes += i.szavazatok;
                    }
                }
                partszavazatok[j] = osszes;
            }
            Console.WriteLine($"Gyümölcsevők Pártja= {(partszavazatok[0]*100.0/osszavazo).ToString("0.00")}%");
            Console.WriteLine($"Húsevők Pártja= {(partszavazatok[1] * 100.0 / osszavazo).ToString("0.00")}%");
            Console.WriteLine($"Tejivók Szövetsége= {(partszavazatok[2] * 100.0 / osszavazo).ToString("0.00")}%");
            Console.WriteLine($"Zöldségevők Pártja= {(partszavazatok[3] * 100.0 / osszavazo).ToString("0.00")}%");
            Console.WriteLine($"Független jelöltek= {(partszavazatok[4] * 100.0 / osszavazo).ToString("0.00")}%");

            //6.feladat
            int max = 0;
            foreach (var i in v)
            {
                if (max < i.szavazatok)
                {
                    max = i.szavazatok;
                }
            }
            foreach (var i in v)
            {
                if (i.szavazatok == max)
                {
                    Console.WriteLine(i.partok == "-" ? $"{i.nev} független" : $"{i.nev} {i.partok}");
                }
            }

            //7.feladat
            StreamWriter ir = new StreamWriter("kepviselok.txt");
            for (int j = 1;j<=8;j++)
            {
                max = 0;
                nev = null;
                string part = null;
                foreach (var i in v)
                {
                    if (j == i.v_ker)
                    {
                        if (i.szavazatok > max)
                        {
                            max = i.szavazatok;
                            nev = i.nev;
                            part = i.partok;
                        }                       
                    }
                }
                ir.WriteLine(part == "-" ? $"{j} {nev} független" : $"{j} {nev} {part}");
            }
            ir.Close();
            Console.ReadKey();
        }
    }
}
