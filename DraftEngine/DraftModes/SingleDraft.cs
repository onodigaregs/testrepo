using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftEngine.DraftModes
{
    using DraftEngine.Enums;
    using DraftEngine.Interfaces;
    using DraftEngine.Models;

    public class SingleDraft : IDraftMode
    {
        public int NumOfPlayers { get; }

        private IEnumerable<ICharacter> CharacterPool { get; set; }

        private List<string> UserList { get; set; }

        // DraftAction
        // int = number of cases this needs to happen i.e. numOfPlayers
        private List<KeyValuePair<DraftAction, int>> DraftState;

        private IEnumerable<DraftAction> DraftOrder = new List<DraftAction>()
                                                          {
                                                              DraftAction.Pick,
                                                              DraftAction.Finished
                                                          };

        public IEnumerable<ICharacter> GetPool()
        {
            return this.CharacterPool;
        }

        public SingleDraft(IEnumerable<ICharacter> seed, List<string> userList)
        {
            this.NumOfPlayers = userList.Count();
            this.CharacterPool = seed;
            this.UserList = userList;
            this.DraftState = new List<KeyValuePair<DraftAction, int>>();
            foreach (var draftAction in this.DraftOrder)
            {
                this.DraftState.Add(new KeyValuePair<DraftAction, int>(draftAction, this.NumOfPlayers));
            }
        }
        public DraftResponse InitializeDraft()
        {
            return new DraftResponse(GetRandom(this.CharacterPool, 3), this.DraftState.First().Key, this.UserList[0]);
        }

        private IEnumerable<ICharacter> GetRandom(IEnumerable<ICharacter> seed, int num)
        {
            var rnd = new Random();
            var singleDraftHeroes = new List<ICharacter>();

            for (int i = 0; i < num; i++)
            {
                var candidate = seed.ToArray()[rnd.Next(seed.Count() - 1)];
                while (singleDraftHeroes.Contains(candidate))
                {
                    candidate = seed.ToArray()[rnd.Next(seed.Count() - 1)];
                }

                singleDraftHeroes.Add(candidate);
            }

            return singleDraftHeroes;
        }

        private void Pick(ICharacter character, string drafter)
        {
            character.IsSelected = true;
            character.ModifiedBy = drafter;
        }

        public DraftResponse ProcessDraft(string drafterName, DraftAction currentAction, string characterName)
        {
            var currUserIndex = this.UserList.IndexOf(drafterName);
            var nextUser = this.UserList.Count() - 1 == currUserIndex ? this.UserList[0] : this.UserList[currUserIndex + 1];
            var character = this.CharacterPool.First(c => c.Name == characterName);
            var availableHeroes = this.CharacterPool.Where(c => !c.IsSelected);


            switch (currentAction)
            {
                case DraftAction.Pick:
                    this.Pick(character, drafterName);
                    break;
            }

            // last of this type
            if (this.DraftState.First().Value == 1)
            {
                this.DraftState.RemoveAt(0);
            }
            else
            {
                var copy = this.DraftState.First();
                var newVal = new KeyValuePair<DraftAction, int>(copy.Key, copy.Value - 1);
                this.DraftState.RemoveAt(0);
                this.DraftState.Insert(0, newVal);
            }

            var heroPool = this.DraftState.First().Key == DraftAction.Finished ? this.CharacterPool : GetRandom(this.CharacterPool.Where(c => !c.IsSelected), 3);

            return new DraftResponse(heroPool, this.DraftState.First().Key, nextUser);
        }
    }
}
