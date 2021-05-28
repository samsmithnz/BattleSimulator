﻿using Battle.Logic.AbilitiesAndEffects;
using Battle.Logic.Characters;
using Battle.Tests.Items;

namespace Battle.Tests.Characters
{
    public static class CharacterPool
    {
        public static Character CreateFredHero()
        {
            Character fred = new()
            {
                Name = "Fred",
                HitpointsMax = 12,
                HitpointsCurrent = 12,
                ArmorPointsMax = 0,
                ArmorPointsCurrent = 0,
                ActionPointsMax = 2,
                ActionPointsCurrent = 2,
                ChanceToHit = 70,
                Experience = 0,
                Level = 1,
                LevelUpIsReady = false,
                Speed = 10,
                Location = new(0, 0, 0),
                MovementRange = 8,
                ShootingRange = 30,
                WeaponEquipped = WeaponPool.CreateRifle(),
                UtilityWeaponEquipped = WeaponPool.CreateGrenade(),
                UtilityItemEquipped = ItemPool.CreateMedKit(),
                InHalfCover = false,
                InFullCover = false,
                InOverwatch = false,
                HunkeredDown = false
            };
            fred.Abilities.Add(new("Ability", AbilityType.Unknown, 0));
            fred.Effects.Add(
                new()
                {
                    Name = "Fire",
                    Type = AbilityType.FireDamage,
                    Adjustment = 1,
                    TurnExpiration = 2
                }
             );

            return fred;
        }

        public static Character CreateJeffBaddie()
        {
            Character fred = new()
            {
                Name = "Jeff",
                HitpointsCurrent = 12,
                ArmorPointsCurrent = 0,
                ChanceToHit = 70,
                Experience = 0,
                Level = 1,
                LevelUpIsReady = false,
                Speed = 11,
                Location = new(8, 0, 8),
                ActionPointsCurrent = 2,
                MovementRange = 8,
                ShootingRange = 30,
                WeaponEquipped = WeaponPool.CreateShotgun(),
                UtilityWeaponEquipped = WeaponPool.CreateGrenade(),
                InHalfCover = false,
                InFullCover = false,
                InOverwatch = false,
                HunkeredDown = false
            };
            return fred;
        }

        public static Character CreateHarryHeroSidekick()
        {
            Character harry = new()
            {
                Name = "Harry",
                HitpointsCurrent = 12,
                ArmorPointsCurrent = 1,
                ChanceToHit = 70,
                Experience = 0,
                Level = 1,
                LevelUpIsReady = false,
                Speed = 12,
                Location = new(5, 0, 5),
                ActionPointsCurrent = 2,
                MovementRange = 8,
                ShootingRange = 30,
                WeaponEquipped = WeaponPool.CreateSniperRifle(),
                UtilityWeaponEquipped = WeaponPool.CreateGrenade(),
                InHalfCover = true,
                InFullCover = false,
                InOverwatch = false,
                HunkeredDown = false
            };
            return harry;
        }
    }
}
