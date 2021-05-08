﻿using Battle.Logic.Characters;
using Battle.Tests.Weapons;

namespace Battle.Tests.Characters
{
    public static class CharacterPool
    {
        public static Character CreateFred()
        {
            Character fred = new()
            {
                Name = "Fred",
                HP = 12,
                ChanceToHit = 70,
                Experience = 0,
                Level = 1,
                LevelUpIsReady = false,
                Location = new(0, 0, 0),
                ActionPoints = 2,
                Range = 10,
                WeaponEquiped = WeaponPool.CreateRifle(),
                InHalfCover = false,
                InFullCover = false
            };
            fred.Abilities.Add(new("Ability", AbilityTypeEnum.Unknown, 0));

            return fred;
        }

        public static Character CreateJeff()
        {
            Character fred = new()
            {
                Name = "Jeff",
                HP = 12,
                ChanceToHit = 70,
                Experience = 0,
                Level = 1,
                LevelUpIsReady = false,
                Location = new(8, 0, 8),
                ActionPoints = 2,
                Range = 10,
                WeaponEquiped = WeaponPool.CreateShotgun(),
                InHalfCover = false,
                InFullCover = false
            };
            return fred;
        }

        public static Character CreateHarry()
        {
            Character harry = new()
            {
                Name = "Harry",
                HP = 12,
                ChanceToHit = 70,
                Experience = 0,
                Level = 1,
                LevelUpIsReady = false,
                Location = new(5, 0, 5),
                ActionPoints = 2,
                Range = 10,
                WeaponEquiped = WeaponPool.CreateSniperRifle(),
                InHalfCover = true,
                InFullCover = false
            };
            return harry;
        }
    }
}
