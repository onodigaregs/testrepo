using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftEngine.Models
{
    using DraftEngine.Enums;
    using DraftEngine.Interfaces;

    public class DraftResponse
    {
        public DraftResponse(IEnumerable<ICharacter> heroPool, DraftAction action, string nextDrafter)
        {
            this.HeroPool = heroPool;
            this.NextAction = action;
            this.NextDrafter = nextDrafter;
        }
        public IEnumerable<ICharacter> HeroPool { get; set; }

        public string NextDrafter { get; set; }

        public DraftAction NextAction { get; set; }
    }
}
