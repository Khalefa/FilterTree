using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilterTree
{
    class FilterTree
    {

        Node root;

        public FilterTree()
        {
            root = new Node();
        }
        public FilterTree(ArrayList words)
        {
            root = new Node(words);
        }

        public ArrayList getLeafs(int max = 1000)
        {
            ArrayList l = root.getLeafs(max);
            return l;
        }

        //here we emply 
        public pair join(FilterTree x, int threshold)
        {
            return root.join(x.root, threshold);
        }
        public long estimateCard()
        {
            ArrayList leafs = getLeafs(1);
            //save("a", t);
            long match = 0;
            for (int i = 0; i < leafs.Count; i++)
            {
                Node n = (Node)leafs[i];
                for (int j = i + 1; j < leafs.Count; j++)
                {
                    Node m = (Node)leafs[j];
                    int d = n.p.diff(m.p);
                    if (d <= 2) match += (n.cardinality * m.cardinality);
                }
            }
            return match;
        }
    }
}
