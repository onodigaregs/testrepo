using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftEngine.Factories
{
    using DraftEngine.DraftModes;
    using DraftEngine.Interfaces;

    public static class DraftModeFactory
    {
        public static IDraftMode Resolve(string draftModeName, IEnumerable<ICharacter> seed, List<string> drafters)
        {
            switch (draftModeName)
            {
                case "CaptainsMode":
                    return new CaptainsDraft(seed, drafters);
                    break;
                case "SingleDraft":
                    return new SingleDraft(seed, drafters);
                    break;
                default:
                    return new CaptainsDraft(seed, drafters);
                    break;
            }
        }
    }
}
