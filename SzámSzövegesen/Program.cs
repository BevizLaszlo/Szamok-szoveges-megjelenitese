using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzámSzövegesen
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("A szám: ");
            string num = Console.ReadLine();
            string strNum = "";

            ConvertNum(num, ref strNum);

            Console.WriteLine(strNum);
            Console.ReadKey();
        }

        static void ConvertNum(string num, ref string strNum)
        {
            string[] egyesek = new string[] { "", "egy", "kettő", "három", "négy", "öt", "hat", "hét", "nyolc", "kilenc" };
            string[] tizesek = new string[] { "", "tíz", "húsz", "harminc", "negyven", "ötven", "hatvan", "hetven", "nyolcvan", "kilencven" };
            string[] egysegek = new string[] { "száz", "ezer", "millió", "milliárd", "billió", "billiárd", "trillió", "trilliárd", "kvadrillió", "kvadrilliárd", "kvintillió", "kvintilliárd", "szextillió", "szextilliárd", "szeptillió", "szeptilliárd", "oktillió", "oktilliárd", "nonillió", "nonilliárd", "decillió", "decilliárd", "undecillió", "undecilliárd", "bidecillió", "bidecilliárd", "tridecillió", "tridecilliárd", "kvadecillió", "kvadecilliárd", "kvintdecillió", "kvintdecilliárd", "szexdecillió", "szexdecilliárd" };


            // kivesszük a whitespace karaktereket, ha a felhasználó szóközökkel írná be a számot

            for (int i = 0; i < num.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(num[i].ToString()))
                {
                    num = num.Remove(i, 1);
                }
            }

            // ha 0 a beírt szám:

            if (num == "0")
            {
                strNum = "nulla";
            }

            // negatív számok esetén:

            if (num[0] == '-')
            {
                strNum += "mínusz ";
                num = num.Remove(0, 1);
            }


            // ha 2000-nél nagyobb a szám, akkor kötőjelezni kell az egységek között

            bool biggerThan2000 = false;
            if (num.Length > 4 || Convert.ToInt32(num.Substring(Math.Max(0, num.Length - 4))) > 2000)
            {
                biggerThan2000 = true;
            }


            // ha a maradék 0, akkor a százasok helyén vagyunk
            // ha a maradék 2, akkor a tizesek helyén vagyunk
            // ha a maradék 1, akkor az egyesek helyén vagyunk
            // ha a maradék 1, akkor oda kell írni a szám mellé az egységhez tartozó értéket, de csak akkor, ha van érték (isThereValue) a százasok, tizesek vagy egyesek helyén
            // a feltételek megvizsgálása után az első elemet, amit megvizsgáltunk kivesszük a stringből, és folytatjuk addig, amíg a string el nem fogy.

            bool isThereValue = false;
            while (num.Length > 0)
            {
                int egyseg = (num.Length - 1) / 3;
                int maradek = num.Length % 3;
                int currentNum = Convert.ToInt32(num[0].ToString());

                if (currentNum != 0) 
                {
                    isThereValue = true;
                }

                if (maradek == 0)
                {
                    if (currentNum == 1)
                    {
                        strNum += egysegek[0];
                    }
                    else if (currentNum != 0)
                    {
                        strNum += egyesek[currentNum] + egysegek[0];
                    }
                }
                else if (maradek == 2)
                {
                    if (currentNum == 1 && Convert.ToInt32(num[1].ToString()) != 0)
                    {
                        strNum += "tizen";
                    }
                    else if (currentNum == 2 && Convert.ToInt32(num[1].ToString()) != 0)
                    {
                        strNum += "huszon";
                    }
                    else
                    {
                        strNum += tizesek[currentNum];
                    }
                }
                else if (maradek == 1)
                {
                    strNum += egyesek[currentNum];
                    if (egyseg != 0 && isThereValue)
                    {
                        strNum += egysegek[egyseg];
                        if (biggerThan2000)
                        {
                            strNum += "-";
                        }
                    }
                    isThereValue = false;
                }

                num = num.Remove(0, 1);
            }

            // az utolsó karakter nem lehet kötőjel

            if (strNum[strNum.Length - 1] == '-')
            {
                strNum = strNum.Remove(strNum.Length - 1, 1);
            }
        }
    }
}
