using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FilterTree
{
    public class Node
    {
        internal const int b = 1000;
        public Hashtable children;
        public ArrayList words;
        public Point p;
        Node parent;
        int level;
        public int cardinality
        {
            get
            {
                if (children == null)
                    return 1;
                else return children.Count;
            }

        }
        public Node(ArrayList words, Node p_ = null)
        {
            this.words = words;
            children = null;
            parent = p_;
            if (p_ == null) level = 0;
            else level = p_.level + 1;
            divide();
        }
        public void add(String s)
        {
            this.words.Add(s);
        }
        public void divide()
        {
            if (level > 3) return;
            if (words.Count > b)
            {
                children = new Hashtable();
                foreach (string s in words)
                {
                    Word w = new Word(s);
                    Point k = w.getpoint(level);
                    k.word = w;
                    //k.parent = w.getpoint(level - 1);
                    if (children.ContainsKey(k))
                    {
                        Node n = (Node)children[k];
                        n.add(s);
                    }
                    else
                    {
                        Node n = new Node(this, k);
                        n.add(s);
                        children.Add(k, n);
                    }
                }
                words.Clear();
            }
            if (children != null)
                foreach (Node n in children.Values)
                {
                    n.divide();
                }
        }
        public Node(Node p_ = null, Point pp = null)
        {
            words = new ArrayList();
            children = null;
            parent = p_;
            if (p_ == null) level = 0;
            else level = p_.level + 1;
            p = pp;
        }
        public ArrayList getLeafs(int max)
        {
            ArrayList l = new ArrayList();
            if ((children == null) || (max == 0))
            {
                l.Add(this);
            }
            else
                foreach (Node n in children.Values)
                {
                    l.AddRange(n.getLeafs(max - 1));
                }

            return l;
        }

        public ArrayList join(Node x, int threshold)
        {
            ArrayList output = new ArrayList();
            Queue pairs = new Queue();
            pairs.Enqueue(new pair(this, x));

            while (pairs.Count > 0)
            {
                pair t = (pair)pairs.Dequeue();
                Node x1 = (Node)t.first;
                Node x2 = (Node)t.second;
                
                if (x1.children ==null) {
                    Node tmp=x1;
                    tmp=x2;
                    x2=x1;
                    x1=tmp;
                }

                if ((x1.children != null) && (x2.children != null))
                {
                    foreach (Node child in x1.children)
                    {
                        foreach (Node child2 in x2.children)
                        {
                            if (child2.p.diff(child.p) <= threshold)
                            {
                                pairs.Enqueue(new pair(child, child2));
                            }
                        }
                    }
                }
                else if ((x2.children == null) && (x1.children != null))
                {
                    foreach (Node child in x1.children)
                    {
                            if (x2.p.diff(child.p) <= threshold)
                            {
                                pairs.Enqueue(new pair(child, x2));
                            }
                    }
                }
                else
                {
                    output.Add(new pair(x1,x2));
                }
            }
            return output;
        }
    };
}
