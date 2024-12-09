using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MacroTrackerCore.Entities
{
    public class User
    {
        required public string Username { get; set; }
        required public string EncryptedPassword { get; set; }
    }
}
