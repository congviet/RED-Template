using de4dot.code;
using dnlib.DotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RED_Template
{
    public abstract class REDBase
    {
        protected abstract void DO(ModuleDefMD module);
    }
}
