﻿using Battle.Logic.Characters;
using Battle.Logic.Utility;
using System.Collections.Generic;
using System.Numerics;

namespace Battle.Logic.Game
{
    public class Team
    {
        public Team()
        {
            Characters = new List<Character>();
        }

        public string Name { get; set; }
        public bool IsAITeam { get; set; }
        public int CurrentCharacterIndex { get; set; }
        public List<Character> Characters { get; set; }
        public string Color { get; set; }
        public string[,,] FOVMap { get; set; }
        public HashSet<Vector3> FOVHistory { get; set; }

        public Character GetNextCharacter()
        {
            (int, Character) result = TeamUtility.GetNextCharacter(CurrentCharacterIndex, Characters);
            if (result.Item1 >= 0)
            {
                CurrentCharacterIndex = result.Item1;
                return result.Item2;
            }
            else
            {
                return null;
            }
        }

        public Character GetPreviousCharacter()
        {
            (int, Character) result = TeamUtility.GetPreviousCharacter(CurrentCharacterIndex, Characters);
            if (result.Item1 >= 0)
            {
                CurrentCharacterIndex = result.Item1;
                return result.Item2;
            }
            else
            {
                return null;
            }
        }

    }
}
