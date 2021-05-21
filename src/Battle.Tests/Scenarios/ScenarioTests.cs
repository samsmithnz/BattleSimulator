﻿using Battle.Logic.CharacterCover;
using Battle.Logic.Characters;
using Battle.Logic.Encounters;
using Battle.Logic.MainGame;
using Battle.Logic.Movement;
using Battle.Logic.PathFinding;
using Battle.Tests.Characters;
using Battle.Tests.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Numerics;

namespace Battle.Tests.Scenarios
{
    [TestClass]
    [TestCategory("L0")]
    public class ScenarioTests
    {
        [TestMethod]
        public void SimpleScenarioMultipleTurnsTest()
        {
            //Arrange
            Game game = new();
            game.TurnNumber = 1;
            game.Map = MapUtility.InitializeMap(50, 50);
            game.Map[6, 5] = CoverType.FullCover;
            game.Map[20, 11] = CoverType.FullCover;
            Character fred = CharacterPool.CreateFredHero();
            fred.Location = new(5, 0, 5);
            Team team1 = new()
            {
                Name = "Good guys",
                Characters = new() { fred }
            };
            game.Teams.Add(team1);
            Character jeff = CharacterPool.CreateJeffBaddie();
            jeff.Location = new(20, 0, 10);
            Team team2 = new()
            {
                Name = "Bad guys",
                Characters = new() { jeff }
            };
            game.Teams.Add(team2);
            Queue<int> diceRolls = new(new List<int> { 100, 100, 0, 0, 100, 100, 100 }); //Chance to hit roll, damage roll, critical chance roll


            //Assert - Setup
            Assert.AreEqual(1, game.TurnNumber);
            Assert.AreEqual(2, game.Teams.Count);
            Assert.AreEqual(50 * 50, game.Map.Length);
            Assert.AreEqual("Good guys", game.Teams[0].Name);
            Assert.AreEqual(1, game.Teams[0].Characters.Count);
            Assert.AreEqual("Bad guys", game.Teams[1].Name);
            Assert.AreEqual(1, game.Teams[1].Characters.Count);

            //Act

            //Turn 1 - Team 1 starts
            //Fred runs to cover
            PathResult pathResult = Path.FindPath(fred.Location, new(9, 0, 10), game.Map);
            CharacterMovement.MoveCharacter(fred, game.Map, pathResult.Path, diceRolls, null);

            //Fred aims at Jeff, who is behind high cover. 
            string mapString1 = fred.GetCharactersInViewMapString(game.Map, new List<Team> { team2 });
            List<Character> characters = fred.GetCharactersInView(game.Map, new List<Team>() { team2 });
            Assert.AreEqual(characters[0], jeff);
            string mapResult1 = @"
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o ■ □ □ □ □ o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o P o o o o o o o o o o P o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ 
o o o o o o ■ o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ 
o o o o o o □ o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ 
o o o o o □ o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ 
o o o o □ □ o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ 
o o o o □ o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ 
o o o □ □ o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ 
";
            Assert.AreEqual(mapResult1, mapString1);

            int chanceToHit = EncounterCore.GetChanceToHit(fred, fred.WeaponEquipped, jeff);
            int chanceToCrit = EncounterCore.GetChanceToCrit(fred, fred.WeaponEquipped, jeff, game.Map, false);
            DamageOptions damageOptions = EncounterCore.GetDamageRange(fred, fred.WeaponEquipped);
            Assert.AreEqual(80, chanceToHit);
            Assert.AreEqual(70, chanceToCrit);
            Assert.AreEqual(3, damageOptions.DamageLow);
            Assert.AreEqual(5, damageOptions.DamageHigh);

            //Fred shoots at Jeff, who is behind high cover. He hits him. 
            EncounterResult encounter1 = Encounter.AttackCharacter(fred,
                    fred.WeaponEquipped,
                    jeff,
                    game.Map,
                    diceRolls);
            string log1 = @"
Fred is attacking with Rifle, targeted on Jeff
Hit: Chance to hit: 80, (dice roll: 100)
Damage range: 3-5, (dice roll: 100)
Critical chance: 70, (dice roll: 0)
5 damage dealt to character Jeff, HP is now 7
10 XP added to character Fred, for a total of 10 XP
";
            Assert.AreEqual(log1, encounter1.LogString);

            //Turn 1 - Team 2 starts
            //Jeff aims back and misses
            List<Character> characters2 = jeff.GetCharactersInView(game.Map, new List<Team>() { team1 });
            Assert.AreEqual(characters2[0], fred);
            int chanceToHit2 = EncounterCore.GetChanceToHit(jeff, jeff.WeaponEquipped, fred);
            int chanceToCrit2 = EncounterCore.GetChanceToCrit(jeff, jeff.WeaponEquipped, jeff, game.Map, false);
            DamageOptions damageOptions2 = EncounterCore.GetDamageRange(jeff, jeff.WeaponEquipped);
            Assert.AreEqual(72, chanceToHit2);
            Assert.AreEqual(70, chanceToCrit2);
            Assert.AreEqual(3, damageOptions2.DamageLow);
            Assert.AreEqual(5, damageOptions2.DamageHigh);

            //Jeff shoots back and misses
            EncounterResult encounter2 = Encounter.AttackCharacter(jeff,
                    jeff.WeaponEquipped,
                    fred,
                    game.Map,
                    diceRolls);
            string log2 = @"
Jeff is attacking with Shotgun, targeted on Fred
Missed: Chance to hit: 72, (dice roll: 0)
0 XP added to character Jeff, for a total of 0 XP
";
            Assert.AreEqual(log2, encounter2.LogString);

            //Turn 2 - Team 1 starts
            //Fred shoots again, and kills Jeff.
            List<Character> characters3 = fred.GetCharactersInView(game.Map, new List<Team>() { team2 });
            Assert.AreEqual(characters3[0], jeff);
            int chanceToHit3 = EncounterCore.GetChanceToHit(fred, fred.WeaponEquipped, jeff);
            int chanceToCrit3 = EncounterCore.GetChanceToCrit(fred, fred.WeaponEquipped, jeff, game.Map, false);
            DamageOptions damageOptions3 = EncounterCore.GetDamageRange(fred, fred.WeaponEquipped);
            Assert.AreEqual(80, chanceToHit3);
            Assert.AreEqual(70, chanceToCrit3);
            Assert.AreEqual(3, damageOptions3.DamageLow);
            Assert.AreEqual(5, damageOptions3.DamageHigh);

            EncounterResult encounter3 = Encounter.AttackCharacter(fred,
                    fred.WeaponEquipped,
                    jeff,
                    game.Map,
                    diceRolls);
            string log3 = @"
Fred is attacking with Rifle, targeted on Jeff
Hit: Chance to hit: 80, (dice roll: 100)
Damage range: 3-5, (dice roll: 100)
Critical chance: 70, (dice roll: 100)
Critical damage range: 8-12, (dice roll: 100)
12 damage dealt to character Jeff, HP is now -5
Jeff is killed
100 XP added to character Fred, for a total of 110 XP
Fred is ready to level up
";
            Assert.AreEqual(log3, encounter3.LogString);

            //End of of battle

            //Assert
            Assert.AreEqual(-5, jeff.Hitpoints);
        }

        //Enemy is behind cover and not visible.
        //Throw grenade to destory cover and make him visible.
        [TestMethod]
        public void JeffIsHidingBehindCoverScenarioTest()
        {
            //Arrange
            Game game = new();
            game.TurnNumber = 1;
            game.Map = MapUtility.InitializeMap(50, 50);
            game.Map[5, 6] = CoverType.FullCover;
            game.Map[14, 5] = CoverType.FullCover;
            game.Map[14, 6] = CoverType.FullCover;
            game.Map[14, 7] = CoverType.FullCover;
            game.Map[14, 8] = CoverType.FullCover;
            game.Map[14, 9] = CoverType.FullCover;
            game.Map[14, 10] = CoverType.FullCover;
            game.Map[14, 11] = CoverType.FullCover;
            game.Map[14, 12] = CoverType.FullCover;
            game.Map[14, 13] = CoverType.FullCover;
            game.Map[14, 14] = CoverType.FullCover;
            Character fred = CharacterPool.CreateFredHero();
            fred.Location = new(5, 0, 5);
            Team team1 = new()
            {
                Name = "Good guys",
                Characters = new() { fred }
            };
            game.Teams.Add(team1);
            Character jeff = CharacterPool.CreateJeffBaddie();
            jeff.Location = new(15, 0, 10);
            Team team2 = new()
            {
                Name = "Bad guys",
                Characters = new() { jeff }
            };
            game.Teams.Add(team2);
            Queue<int> diceRolls = new(new List<int> { 100, 100, 100, 100, 100 }); //Chance to hit roll, damage roll, critical chance roll


            //Assert - Setup
            Assert.AreEqual(1, game.TurnNumber);
            Assert.AreEqual(2, game.Teams.Count);
            Assert.AreEqual(50 * 50, game.Map.Length);
            Assert.AreEqual("Good guys", game.Teams[0].Name);
            Assert.AreEqual(1, game.Teams[0].Characters.Count);
            Assert.AreEqual("Bad guys", game.Teams[1].Name);
            Assert.AreEqual(1, game.Teams[1].Characters.Count);

            //Act

            //Turn 1 - Team 1 starts
            //Fred cannot see Jeff, who is hiding behind cover
            string mapString1 = fred.GetCharactersInViewMapString(game.Map, new List<Team> { team2 });
            List<Character> characters = fred.GetCharactersInView(game.Map, new List<Team>() { team2 });
            Assert.AreEqual(0, characters.Count);
            string mapResult1 = @"
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o □ □ □ □ □ □ □ □ □ o o o o ■ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o □ □ □ □ □ □ □ o o o o o ■ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o □ □ □ □ □ □ □ o o o o o ■ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o □ □ □ □ □ o o o o o o ■ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o □ □ □ □ □ o o o o o o ■ P □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o □ □ □ o o o o o o o ■ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o □ □ □ o o o o o o o ■ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o □ o o o o o o o o ■ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o ■ o o o o o o o o ■ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o P o o o o o o o o ■ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
";
            Assert.AreEqual(mapResult1, mapString1);

            //Throw grenade in front of wall
            Vector3 targetThrowingLocation = new(13, 0, 10);
            EncounterResult encounter1 = Encounter.AttackCharacterWithAreaOfEffect(fred, fred.UtilityItemEquipped, team2.Characters, game.Map, diceRolls, targetThrowingLocation);
            string log1 = @"
Fred is attacking with area effect Grenade aimed at <13, 0, 10>
Characters in affected area: Jeff
Damage range: 3-4, (dice roll: 100)
Critical chance: 0, (dice roll: 100)
Critical damage range: 3-4, (dice roll: 100)
4 damage dealt to character Jeff, HP is now 8
10 XP added to character Fred, for a total of 10 XP
Cover removed from <14, 0, 10>
Cover removed from <14, 0, 8>
Cover removed from <14, 0, 9>
Cover removed from <14, 0, 11>
Cover removed from <14, 0, 12>
Cover removed from <14, 0, 7>
Cover removed from <14, 0, 13>
";
            Assert.AreEqual(log1, encounter1.LogString);

            string mapString2 = fred.GetCharactersInViewMapString(game.Map, new List<Team> { team2 });
            List<Character> characters2 = fred.GetCharactersInView(game.Map, new List<Team>() { team2 });
            Assert.AreEqual(1, characters2.Count);
            string mapResult2 = @"
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o o o □ o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o o □ o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o o □ o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o □ o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o o □ o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o □ o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o □ o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ o o o o o o o □ o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ □ o o o o o o □ o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ o o o o o o □ o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ □ o o o o o □ o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
□ □ □ □ □ □ □ □ □ □ o o o o o □ o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o □ □ □ □ □ □ □ □ □ o o o o ■ o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o □ □ □ □ □ □ □ o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o □ □ □ □ □ □ □ o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o □ □ □ □ □ o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o □ □ □ □ □ o o o o o o o P o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o □ □ □ o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o □ □ □ o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o □ o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o ■ o o o o o o o o ■ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o P o o o o o o o o ■ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o □ □ □ □ □ □ □ □ □ □ □ □ □ □ 
";
            Assert.AreEqual(mapResult2, mapString2);

            //End of of battle

            //Assert
        }

    }
}

