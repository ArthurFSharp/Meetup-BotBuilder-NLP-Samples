using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotLuisMeetup.Dialog.Helpers
{
    [Serializable]
    public abstract class BaseViewModel
    {
        public abstract bool IsValid { get; }
    }
}
