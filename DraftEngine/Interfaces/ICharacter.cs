using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftEngine.Interfaces
{
    public interface ICharacter
    {
       // IEnumerable<ICharacter> CharacterPool { get; set; }
        string Name { get; set; }

        bool IsSelected { get; set; }

        bool IsBanned { get; set; }

        string ModifiedBy { get; set; }
    }
}
