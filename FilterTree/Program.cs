using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;
//Here is the once-per-application setup information
[assembly: log4net.Config.XmlConfigurator(Watch = true)]
namespace FilterTree
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        static ArrayList readinput(string file)
        {
            ArrayList words = new ArrayList();

            StreamReader r = new StreamReader(file);
            while (!r.EndOfStream)
            {
                words.Add(r.ReadLine().ToLower());
            }
            r.Close();
            return words;
        }
        static Hashtable process(ArrayList words, int level)
        {
            Hashtable t = new Hashtable();
            foreach (string s in words)
            {
                Word w = new Word(s);
                object key = w.getpoint(level);
                if (t.ContainsKey(key))
                {
                    ArrayList a = (ArrayList)t[key];
                    a.Add(s);
                }
                else
                {
                    ArrayList a = new ArrayList();
                    a.Add(s);
                    t.Add(key, a);
                }
            }

            return t;
        }
        static void save(String filename, Hashtable t)
        {
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\tt\" + filename);
            save(t, file);
            file.Close();
        }
        static void save( Hashtable t, StreamWriter file, int tab=0)
        {
         
            foreach (object x in t.Keys)
            {
                if (t[x] is ArrayList)
                {
                    ArrayList b = (ArrayList)t[x];
                    string s = "";
                    string tabs="";
                    for (int i = 0; i < tab; i++) tabs = tabs + "\t";
                    foreach (String tt in b)
                        s = s + " " + tt;
                    file.WriteLine(tabs+ b.Count + "\t" + x.ToString() + "\t" + s);
                }
                else
                    save((Hashtable) t[x], file,tab+1); 
            }
            

        }
        static Hashtable processR(ArrayList words, int level=0)
        {
           
            Hashtable t = process(words, 0);
            //int level = 1;

            for (; ; )
            {
                ArrayList toprocess = new ArrayList();
                ArrayList ids = new ArrayList();

                foreach (Point x in t.Keys)
                {
                    if (t[x] is ArrayList)
                    {
                        ArrayList b = (ArrayList)t[x];
                        if (b.Count > 100)
                        {
                            toprocess.Add(b);
                            ids.Add(x);
                        }
                    }
                }
                if (ids.Count == 0 || level > 5) break;
                for (int j = 0; j < toprocess.Count; j++)
                {
                    ArrayList m = (ArrayList)toprocess[j];
                    Point p = (Point)ids[j];
                    Hashtable t0 = processR(m, level+1);
                    
                    //t.Remove(p);
                    t[p] = t0;
                    // add t0 to t
                    //foreach (Point x in t0.Keys)
                    //{
                      //  Point xx = new Point(x);
                       // xx.parent = p;
                      //  t.Add(xx, t0[x]);
                    //}
                }
                //level++;

            }
            return t;
        }

        static void x()
        {
            String s = "sty"; //efferent overfeed seepweed tenderee underfed unreefed unseeded weepered			
            String s1 = "stz";
            Word w = new Word(s);
            Word w1 = new Word(s1);

            Point x = w.getpoint(0);
            Point xx = w.getpoint(0);
            bool aa = xx.Equals(x);
            //Point y = w1.getpoint(3);
            //int d = x.diff(y);
            //RTree<Word> tree = new RTree.RTree<Word>();
            //Hashtable t = processR(words);
            
        }
        static void Main(string[] args)
        {
            ArrayList words = readinput("c:\\data\\web2.txt");
            double ec = words.Count ;
            DateTime st = DateTime.Now;
            FilterTree ft = new FilterTree(words);
            pair xx1= ft.join(ft,1);
            pair xx2 = ft.join(ft, 2);
            DateTime et = DateTime.Now;
            Console.WriteLine(et - st);
            long c1 = (long)xx1.second;
            long c2 = (long)xx2.second;
            Console.WriteLine(ec);
            Console.WriteLine(c1/ec/ec);
            Console.WriteLine(c2/ ec/ec);

        }
    }
}
