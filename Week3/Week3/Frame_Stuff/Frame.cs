using System;
using System.Collections.Generic;
using System.Text;

namespace Week3.Frame_Stuff
{
    public abstract class Frame
    {
        public virtual byte frame_type { get; private set; }
    }
}