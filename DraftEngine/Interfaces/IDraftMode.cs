using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftEngine.Interfaces
{
    using DraftEngine.Enums;
    using DraftEngine.Models;

    public interface IDraftMode
    {
        int NumOfPlayers { get; }

        IEnumerable<ICharacter> GetPool();
        DraftResponse InitializeDraft();
        DraftResponse ProcessDraft(string drafterName, DraftAction currentAction, string characterName);
    }
}
