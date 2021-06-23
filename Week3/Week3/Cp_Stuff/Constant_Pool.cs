using System;
using System.Collections.Generic;
using System.Text;

namespace Week3.Cp_Stuff
{
    public class Constant_Pool
    {
        public CP_Info[] cp_info;
        int index = 0;

        public Constant_Pool(int length)
        {
            cp_info = new CP_Info[length];
        }

        public void Set(CP_Info info)
        {
            cp_info[index] = info;
            index++;
        }
    }
}