using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using System.Windows;
namespace FilterTree
{
  public  class Word/*: CSharpQuadTree.IQuadObject*/
    {
        const int ALPHABET_SIZE = 27;
        public string w;
        int[] hist; //represting the histogram
        Point p;
        public Word(string w)
        {
            this.w = w.ToLower();
            buildHistogram();
            p = getPoint();
            //   bounds = new Rect(p.coordinates[0], p.coordinates[1], 0.1, 0.1);
        }
        bool isalpha(char c)
        {
            if ((c >= 'a') && (c <= 'z')) return true;
            return false;
        }
        void buildHistogram()
        {
            hist = new int[ALPHABET_SIZE];
            for (int i = 0; i < w.Length; i++)
            {
                char c = w[i];
                if (isalpha(c))
                {
                    hist[c - 'a']++;
                }
                else
                    hist[ALPHABET_SIZE - 1]++;
            }
        }
        public Point getPoint()
        {
            int x, y;
            x = y = 0;
            for (int i = 0; i < ALPHABET_SIZE / 2; i++)
            {
                x += hist[i];
                y += hist[ALPHABET_SIZE / 2 + i];
            }
            Point xx = new Point(x, y);
            xx.level = 0;
            return xx;
        }
        public Point getpoint(int l)
        {
            int x;
            x = 0;
            
            double d = Math.Pow(2, l + 1);
            if (d > ALPHABET_SIZE) d = ALPHABET_SIZE;
            double range = Math.Floor(ALPHABET_SIZE / d);
            int r = (int)range;
            int n = (int)(Math.Ceiling((double)ALPHABET_SIZE / r));


            int[] a = new int[n];
            int j = 0;
            int k = 0;
            for (int i = 0; i < ALPHABET_SIZE; i++)
            {
                x += hist[i];
                j++;
                if ((j >= r) ||(i==ALPHABET_SIZE-1))
                {
                    j = 0;
                    a[k++] = x;
                    x = 0;
                }
            }
            return new Point(a, n);

        }
        public Point getPoint(int l)
        {
            int x, y;
            x = y = 0;
            int d = (int)Math.Pow(2, l + 1);
            int range = ALPHABET_SIZE / d;
            for (int i = 0; i < range; i++)
            {
                x += hist[i];
                y += hist[ALPHABET_SIZE / 2 + i];
            }
            Point xx = new Point(x, y);
            xx.level = l;
            return xx;
        }
        
    }
}
