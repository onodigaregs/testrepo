namespace DraftEngine.DraftModes
{
    using DraftEngine.Enums;
    using DraftEngine.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using DraftEngine.Models;

    public class CaptainsDraft : IDraftMode
    {
        private IEnumerable<ICharacter> CharacterPool { get; set; }

        private List<string> UserList { get; set; }

        // DraftAction
        // int = number of cases this needs to happen i.e. numOfPlayers
        private List<KeyValuePair<DraftAction, int>> DraftState;

        private IEnumerable<DraftAction> DraftOrder = new List<DraftAction>()
                                                          {
                                                              DraftAction.Ban,
                                                              DraftAction.Pick,
                                                              DraftAction.Ban,
                                                              DraftAction.Pick,
                                                              DraftAction.Ban,
                                                              DraftAction.Pick,
                                                              DraftAction.Finished
                                                          };

        public int NumOfPlayers { get; private set; }

        public CaptainsDraft(IEnumerable<ICharacter> seed, List<string> userList)
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
            return new DraftResponse(this.CharacterPool, this.DraftState.First().Key, this.UserList[0]);
        }
        private void Ban(ICharacter character, string drafter)
        {
            character.IsBanned = true;
            character.ModifiedBy = drafter;
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

            switch (currentAction)
            {
                case DraftAction.Ban:
                    this.Ban(character, drafterName);
                    break;
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


            return new DraftResponse(this.CharacterPool, this.DraftState.First().Key, nextUser);
        }


       
        public IEnumerable<ICharacter> GetPool()
        {
            return this.CharacterPool;
        }

    }
}
