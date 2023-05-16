using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
 
 

namespace Apriori
{
    class Program
    {
        #region  string to determine Number Of Each Item In File 
        static Dictionary<string, int> BeforeFirstScann(string[] File)
        {
            Dictionary<string, int> ItemsSet = new Dictionary<string, int>();

            foreach (string Line in File)
            {
                foreach (string Item in Line.Trim().Split(' '))
                {


                    if (ItemsSet.ContainsKey(Item))
                        ItemsSet[Item] += 1;
                    else
                        ItemsSet.Add(Item, 1);
                }
            }
            return ItemsSet;
        }
        #endregion

        #region Number Of Each Item In File & Name[key]
        static String Items(String Transaction)
        {
            String[] Items = Transaction.Split(',');
            String re = "";
            Dictionary<String, int> _temp = new Dictionary<string, int>();
            foreach (String item in Items)
                if (!_temp.ContainsKey(item))
                    _temp.Add(item, 0);
            foreach (KeyValuePair<String, int> item in _temp)
                if (re == "")
                    re += item.Key;
                else
                    re += "," + item.Key;

            return re;
        }
        #endregion
       
        #region result of Iteration(scann)

        static Dictionary<string, int> MethodOfIteration(Dictionary<string, int> Var, int Num)
        {
            Dictionary<string, int> NewTable = new Dictionary<string, int>();

            #region Make Migration 

            for (int rows = 0; rows < Var.Count; rows++)
            {
                for (int col = rows + 1; col < Var.Count; col++) 
                {
                    String newItem = Items(Var.ElementAt(rows).Key + "," + Var.ElementAt(col).Key);

                    if (newItem.Split(',').Count() != Num)
                        continue;


                    if (!ItemExist(NewTable, newItem))
                    {
                        NewTable.Add(newItem, 0);
                    }

                }
            }
            #endregion

            Dictionary<string, int> _TempCount = new Dictionary<string, int>();
 
            foreach (KeyValuePair<String, int> Tran in NewTable)
            {

                foreach (String Line in FileContent)
                {
                    string LineItem = Line.Trim();
                    if (LineItem.Split(' ').Count() < Tran.Key.Split(',').Count())
                        continue;
                    int cptLine = 0;
                   
                    foreach (String ItemC in Tran.Key.Split(','))
                    {
                        foreach (String ItemL in Line.Trim().Split(' '))
                        {
                            if (ItemC.Equals(ItemL)) { cptLine++; continue; }
                        }

                    }
                    if (cptLine == Tran.Key.Split(',').Count())
                        if (_TempCount.ContainsKey(Tran.Key))
                            _TempCount[Tran.Key] += 1;
                        else
                            _TempCount.Add(Tran.Key, 1);

                }
            }
            foreach (KeyValuePair<String, int> Tran in _TempCount)
                NewTable[Tran.Key] = Tran.Value;

            return NewTable;
        }
       #endregion

        #region Producing first NewItemTable
        static Dictionary<string, int> ProducingfirstNewItemTable(Dictionary<string, int> ModifiedDic)
        {
            Dictionary<string, int> NewDic = new Dictionary<string, int>();

            for (int i = 0; i < ModifiedDic.Count; i++)
            {
                if (ModifiedDic.ElementAt(i).Value >= min_sup)
                    NewDic.Add(ModifiedDic.ElementAt(i).Key, ModifiedDic.ElementAt(i).Value);
            }
            return NewDic;
        }
        #endregion

        #region bool Val to determine That Val Is contain in  Migration Of Names [n1,n2,.....,n]
        static Boolean ItemExist(Dictionary<string, int> Dic, String newItem)
        {

            foreach (KeyValuePair<string, int> ReturnedDicOfWordsCollection in Dic)
            {
                int counter = 0;
                foreach (String Words in ReturnedDicOfWordsCollection.Key.Split(','))
                {
                    foreach (String Word in newItem.Trim().Split(','))
                    {
                        if (Words.Equals(Word)) { counter++; continue; }
                    }

                }
                if (counter == ReturnedDicOfWordsCollection.Key.Split(',').Count())
                    return true;
            }



            return false;
        }
        #endregion

        #region get ave of elements 
        static float getAverage(Dictionary<string, int> DiictionaryOFAllElements)
        {
            int Num = 0;
            foreach (KeyValuePair<string, int> Item in DiictionaryOFAllElements)
            {
                  Num  += Item.Value;
            }
            return Num / DiictionaryOFAllElements.Count();
        }
        #endregion




        static int min_sup = 0;
        static float confidence = 0;
        static void Apriori()
        {

            if (FileContent.Count() == 0)
            {
                Console.WriteLine("Empty file..!");
                return;

            }
            
            Dictionary<string, int> TempTable = BeforeFirstScann(FileContent);
            Console.WriteLine("Support Count Average = " + getAverage(TempTable));
            Console.Write("Enter a minimum support = ");
            min_sup = int.Parse(Console.ReadLine());
            //first produced table
            Dictionary<string, int> NewTempTable = ProducingfirstNewItemTable(TempTable);
            Console.WriteLine("--------scann One----------");
            foreach (KeyValuePair<string, int> i in NewTempTable)
            {
                Console.WriteLine("{ " + i.Key + " } >->->-->->-> " + i.Value);
               
            }
            Dictionary<string, int> OLD_TempTable = TempTable;
            Dictionary<string, int> OLD_NewTempTable = NewTempTable;

            Dictionary<string, int> NEW_C;
            Dictionary<string, int> NEW_L;
            int NOFIteration = 2;
            int cptL = 1;
            while (true)
            {
                NEW_C = MethodOfIteration(OLD_NewTempTable, NOFIteration);
                NEW_L = ProducingfirstNewItemTable(NEW_C);
                Console.WriteLine("--------Iteration [ " + NOFIteration + " ]----------");
                foreach (KeyValuePair<string, int> i in NEW_L)
                {
                    Console.WriteLine("{ " + i.Key + " } ----> " + i.Value);
                }
                if (NEW_L.Count() == 0)
                {
                   
                    confidence = float.Parse(Console.ReadLine());
                    
                    break;

                }
                else
                {
                    OLD_TempTable = NEW_C;
                    OLD_NewTempTable = NEW_L;


                    cptL++;
                    NOFIteration++;
                }


            }

        }


        #region Simulation
        public static void GenerationOfThread() 
        {
            Console.Write("Processin ");
            System.Threading.Thread.Sleep(750);
            Console.Write('.');
            System.Threading.Thread.Sleep(750);
            Console.Write('.');
            System.Threading.Thread.Sleep(750);
            Console.Write('.');
            System.Threading.Thread.Sleep(750);
            Console.Write('.');
            System.Threading.Thread.Sleep(750);
            Console.Write('.');
            Console.WriteLine("\n");
        }
        #endregion

        static string[] FileContent;
        static void Main(string[] args)
        {
            try
            {

                FileContent = File.ReadAllLines(@"C:\Users\PCM\Desktop\DrAmrAprioralTask\DrAmrAprioralTask\car_test.txt");
                //FileContent = File.ReadAllLines(@"C:\Users\PCM\Desktop\DrAmrAprioralTask\car_test.txt");

                if (FileContent.Count() < 1)
                {
                    Console.WriteLine("Mession Failed ,, \" You can try Agin After checking Errors \"!");
                    Console.ReadKey();
                    return;
                }
                for (int i = 0; i < FileContent.Count(); i++)

                    FileContent[i] = FileContent[i].Trim();

                Console.Write("Do you wish to apply preprocessing ? (gY): ");

                if (Console.ReadLine().Equals("Y", StringComparison.OrdinalIgnoreCase))

                {
                ret:
                    if (true)
                    {
                        try
                        {
                            Console.Clear();

                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            Console.WriteLine("-----------------------");
                            Console.WriteLine("| Apriori Algorithm   |");
                            Console.WriteLine("-----------------------");
                            GenerationOfThread();
                            Apriori();
                        }

                        catch (Exception z)
                        {
                            Console.WriteLine(z.Message);
                            goto ret;
                            Console.ReadKey();
                            return;
                        }
                    }

                }

            }
            catch
            {
                Console.WriteLine("Mession Failed ,, \" You can try Agin After checking Errors \"!");
            }
            Console.ReadKey();

        }
    }
}
