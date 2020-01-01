using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ImportantStuff
{
    public class PlayerObject : MonoBehaviour
    {
        public struct Winner
        {
            public string Name;
            public int Team;
            public bool WinnerYes;
            public Winner(string name, int team, bool winner)
            {
                Name = name;
                Team = team;
                WinnerYes = winner;
            }
        }
    }
}

