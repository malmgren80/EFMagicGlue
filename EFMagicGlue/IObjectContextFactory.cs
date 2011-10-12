using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFMagicGlue
{
    public interface IObjectContextFactory
    {
        IObjectContext CreateObjectContextForKey(string key);
    }
}
