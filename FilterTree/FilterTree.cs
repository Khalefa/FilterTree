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

        public ArrayList getLeafs(int max=1000)
        {
            ArrayList l = root.getLeafs(max);
            return l;
        }

        //here we emply 
        public ArrayList join(FilterTree x)
        {
            ArrayList pairs = new ArrayList();
            //
            Node r1=root;
            Node r2=x.root;
            for (int i = 0; i < r1.children.Keys.Count; i++)
            {
                
            }
            return pairs;
        }
    }
}
