using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DraftEngine.Interfaces;
using DraftEngine.BaseClasses;

namespace DraftEngine.Models
{
    public class Hero : ICharacter//CharacterBase,
    {
        public Hero(string heroName)
        {
            this.Name = heroName;
        }
        public string Name { get; set; }

        public string ModifiedBy { get; set; }

        public bool IsSelected { get; set; }

        public string Icon { get; set; }

        public bool IsBanned { get; set; }
    }
}
