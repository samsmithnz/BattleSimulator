﻿using Battle.Logic.Characters;
using Battle.Logic.Game;
using Battle.Logic.Map;
using Battle.Tests.Characters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Numerics;

namespace Battle.Tests.Map
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    [TestCategory("L0")]
    public class CharacterFieldOfViewTests
    {
        [TestMethod]
        public void FredCanSeeJethroTest()
        {
            //Arrange
            string[,,] map = MapCore.InitializeMap(10, 1, 10);
            Character fred = CharacterPool.CreateFredHero(map, new Vector3(0, 0, 0));
            Character jethro = CharacterPool.CreateJethroBaddie(map, new Vector3(8, 0, 8));
            Team teamBaddie = new Team();
            teamBaddie.Characters.Add(jethro);

            //Act
            string mapString = fred.GetCharactersInViewMapString(map, new List<Team> { teamBaddie });
            List<Character> characters = fred.GetCharactersInRangeWithCurrentWeapon(map, new List<Team> { teamBaddie });

            //Assert
            Assert.IsTrue(characters != null);
            Assert.AreEqual(1, characters.Count);
            Assert.AreEqual("Jethro", characters[0].Name);
            string mapResult = @"
o o o o o o o o o o 
o o o o o o o o P o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
P o o o o o o o o o 
";
            Assert.AreEqual(mapResult, mapString);
        }

        [TestMethod]
        public void FredCanSeeJethroInAngleTest()
        {
            //Arrange
            string[,,] map = MapCore.InitializeMap(10, 1, 10);
            map[7, 0, 7] = CoverType.FullCover;
            map[8, 0, 7] = CoverType.FullCover;
            map[9, 0, 7] = CoverType.FullCover;
            Character fred = CharacterPool.CreateFredHero(map, new Vector3(0, 0, 0));
            Character jethro = CharacterPool.CreateJethroBaddie(map, new Vector3(8, 0, 8));
            Team teamBaddie = new Team();
            teamBaddie.Characters.Add(jethro);

            //Act
            string mapString = fred.GetCharactersInViewMapString(map, new List<Team> { teamBaddie });
            List<Character> characters = fred.GetCharactersInRangeWithCurrentWeapon(map, new List<Team> { teamBaddie });
            string fovMapString = MapCore.GetMapStringWithMapMask(map, fred.FOVMap);

            //Assert
            Assert.IsTrue(characters != null);
            Assert.AreEqual(1, characters.Count);
            string mapResult = @"
o o o o o o o o o . 
o o o o o o o o P . 
o o o o o o o ■ ■ ■ 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
P o o o o o o o o o 
";
            Assert.AreEqual(mapResult, mapString);

            string expectedFOV = @"
. . . . . . . . . ▓ 
. . . . . . . . ▓ ▓ 
. . . . . . . ■ ■ ■ 
. . . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . . . 
";
            Assert.AreEqual(expectedFOV, fovMapString);
        }

        [TestMethod]
        public void FredCannotSeeJethroTest()
        {
            //Arrange
            string[,,] map = MapCore.InitializeMap(10, 1, 10);
            map[7, 0, 7] = CoverType.FullCover;
            map[8, 0, 7] = CoverType.FullCover;
            map[9, 0, 7] = CoverType.FullCover;
            Character fred = CharacterPool.CreateFredHero(map, new Vector3(8, 0, 0));
            Character jethro = CharacterPool.CreateJethroBaddie(map, new Vector3(8, 0, 8));
            Team teamBaddie = new Team();
            teamBaddie.Characters.Add(jethro);

            //Act
            string mapString = fred.GetCharactersInViewMapString(map, new List<Team> { teamBaddie });
            List<Character> characters = fred.GetCharactersInRangeWithCurrentWeapon(map, new List<Team> { teamBaddie });
            string fovMapString = MapCore.GetMapStringWithMapMask(map, fred.FOVMap);

            //Assert
            Assert.IsTrue(characters != null);
            Assert.AreEqual(0, characters.Count);
            string expected = @"
o o o o o o o . . . 
o o o o o o o . P . 
o o o o o o o ■ ■ ■ 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o P o 
";
            Assert.AreEqual(expected, mapString);

            string expectedFOV = @"
. . . . . . . ▓ ▓ ▓ 
. . . . . . . ▓ ▓ ▓ 
. . . . . . . ■ ■ ■ 
. . . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . P . 
";
            Assert.AreEqual(expectedFOV, fovMapString);
        }

        [TestMethod]
        public void FredCanSeeJethroInNorthCoverTest()
        {
            //Arrange
            string[,,] map = MapCore.InitializeMap(10, 1, 10);
            map[8, 0, 7] = CoverType.FullCover;
            map[9, 0, 7] = CoverType.FullCover;
            Character fred = CharacterPool.CreateFredHero(map, new Vector3(8, 0, 0));
            Character jethro = CharacterPool.CreateJethroBaddie(map, new Vector3(8, 0, 8));
            Team teamBaddie = new Team();
            teamBaddie.Characters.Add(jethro);

            //Act
            string mapString = fred.GetCharactersInViewMapString(map, new List<Team> { teamBaddie });
            List<Character> characters = fred.GetCharactersInRangeWithCurrentWeapon(map, new List<Team> { teamBaddie });

            //Assert
            Assert.IsTrue(characters != null);
            Assert.AreEqual(1, characters.Count);
            string mapResult = @"
o o o o o o o o . . 
o o o o o o o o P . 
o o o o o o o o ■ ■ 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o P o 
";
            Assert.AreEqual(mapResult, mapString);
        }



        [TestMethod]
        public void FredCanSeeJethroInSouthCoverTest()
        {
            //Arrange
            string[,,] map = MapCore.InitializeMap(10, 1, 10);
            map[8, 0, 2] = CoverType.FullCover;
            map[9, 0, 2] = CoverType.FullCover;
            Character fred = CharacterPool.CreateFredHero(map, new Vector3(8, 0, 8));
            Character jethro = CharacterPool.CreateJethroBaddie(map, new Vector3(8, 0, 1));
            Team teamBaddie = new Team();
            teamBaddie.Characters.Add(jethro);

            //Act
            string mapString = fred.GetCharactersInViewMapString(map, new List<Team> { teamBaddie });
            List<Character> characters = fred.GetCharactersInRangeWithCurrentWeapon(map, new List<Team> { teamBaddie });

            //Assert
            Assert.IsTrue(characters != null);
            Assert.AreEqual(1, characters.Count);
            string mapResult = @"
o o o o o o o o o o 
o o o o o o o o P o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o ■ ■ 
o o o o o o o o P . 
o o o o o o o o . . 
";
            Assert.AreEqual(mapResult, mapString);
        }



        [TestMethod]
        public void FredCanSeeJethroInEastCoverTest()
        {
            //Arrange
            string[,,] map = MapCore.InitializeMap(10, 1, 10);
            map[8, 0, 7] = CoverType.FullCover;
            Character fred = CharacterPool.CreateFredHero(map, new Vector3(1, 0, 7));
            Character jethro = CharacterPool.CreateJethroBaddie(map, new Vector3(9, 0, 7));
            Team teamBaddie = new Team();
            teamBaddie.Characters.Add(jethro);

            //Act
            string mapString = fred.GetCharactersInViewMapString(map, new List<Team> { teamBaddie });
            List<Character> characters = fred.GetCharactersInRangeWithCurrentWeapon(map, new List<Team> { teamBaddie });

            //Assert
            Assert.IsTrue(characters != null);
            Assert.AreEqual(1, characters.Count);
            string mapResult = @"
o o o o o o o o o o 
o o o o o o o o o o 
o P o o o o o o ■ P 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
";
            Assert.AreEqual(mapResult, mapString);
        }


        [TestMethod]
        public void FredCanSeeJethroInWestCoverTest()
        {
            //Arrange
            string[,,] map = MapCore.InitializeMap(10, 1, 10);
            map[1, 0, 7] = CoverType.FullCover;
            Character fred = CharacterPool.CreateFredHero(map, new Vector3(9, 0, 7));
            Character jethro = CharacterPool.CreateJethroBaddie(map, new Vector3(0, 0, 7));
            Team teamBaddie = new Team();
            teamBaddie.Characters.Add(jethro);

            //Act
            string mapString = fred.GetCharactersInViewMapString(map, new List<Team> { teamBaddie });
            List<Character> characters = fred.GetCharactersInRangeWithCurrentWeapon(map, new List<Team> { teamBaddie });

            //Assert
            Assert.IsTrue(characters != null);
            Assert.AreEqual(1, characters.Count);
            string mapResult = @"
o o o o o o o o o o 
o o o o o o o o o o 
P ■ o o o o o o o P 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
";
            Assert.AreEqual(mapResult, mapString);
        }

        [TestMethod]
        public void JethroCannotSeeFredTest()
        {
            //Arrange
            string[,,] map = MapCore.InitializeMap(10, 1, 10);
            map[7, 0, 7] = CoverType.FullCover;
            map[8, 0, 7] = CoverType.FullCover;
            map[9, 0, 7] = CoverType.FullCover;
            Character fred = CharacterPool.CreateFredHero(map, new Vector3(0, 0, 0));
            Character jethro = CharacterPool.CreateJethroBaddie(map, new Vector3(8, 0, 8));
            Team teamGood = new Team();
            teamGood.Characters.Add(fred);

            //Act
            string mapString = jethro.GetCharactersInViewMapString(map, new List<Team> { teamGood });
            List<Character> characters = jethro.GetCharactersInRangeWithCurrentWeapon(map, new List<Team> { teamGood });

            //Assert
            Assert.IsTrue(characters != null);
            Assert.AreEqual(0, characters.Count);
            string mapResult = @"
o o o o o o o o o o 
o o o o o o o o P o 
o o o o o o o ■ ■ ■ 
o o o o o . . . . . 
o o . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . . . 
. . . . . . . . . . 
P . . . . . . . . . 
";
            Assert.AreEqual(mapResult, mapString);
        }

        [TestMethod]
        public void JethroCanSeeFredTest()
        {
            //Arrange
            string[,,] map = MapCore.InitializeMap(10, 1, 10);
            map[7, 0, 7] = CoverType.HalfCover;
            map[8, 0, 7] = CoverType.FullCover;
            map[9, 0, 7] = CoverType.FullCover;
            Character fred = CharacterPool.CreateFredHero(map, new Vector3(0, 0, 0));
            Character jethro = CharacterPool.CreateJethroBaddie(map, new Vector3(8, 0, 8));
            Team teamGood = new Team();
            teamGood.Characters.Add(fred);

            //Act
            string mapString = jethro.GetCharactersInViewMapString(map, new List<Team> { teamGood });
            List<Character> characters = jethro.GetCharactersInRangeWithCurrentWeapon(map, new List<Team> { teamGood });

            //Assert
            Assert.IsTrue(characters != null);
            Assert.AreEqual(1, characters.Count);
            string mapResult = @"
o o o o o o o o o o 
o o o o o o o o P o 
o o o o o o o □ ■ ■ 
o o o o o o o o . . 
o o o o o o o . . . 
o o o o o o o . . . 
o o o o o o . . . . 
o o o o o o . . . . 
o o o o o . . . . . 
P o o o o . . . . . 
";
            Assert.AreEqual(mapResult, mapString);
        }

        [TestMethod]
        public void FredShouldSeeJethroInCoverTest()
        {
            //Arrange
            string[,,] map = MapCore.InitializeMap(10, 1, 10);
            map[2, 0, 5] = CoverType.FullCover;
            Character fred = CharacterPool.CreateFredHero(map, new Vector3(2, 0, 2));
            Character jethro = CharacterPool.CreateJethroBaddie(map, new Vector3(2, 0, 6));
            Team teamBaddie = new Team();
            teamBaddie.Characters.Add(jethro);

            //Act
            string mapString = fred.GetCharactersInViewMapString(map, new List<Team> { teamBaddie });
            List<Character> characters = fred.GetCharactersInRangeWithCurrentWeapon(map, new List<Team> { teamBaddie });

            //Assert
            Assert.IsTrue(characters != null);
            Assert.AreEqual(1, characters.Count);
            string mapResult = @"
o . . . o o o o o o 
o . . . o o o o o o 
o o . o o o o o o o 
o o P o o o o o o o 
o o ■ o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o P o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
";
            Assert.AreEqual(mapResult, mapString);
        }

        [TestMethod]
        public void FredSimpleRange3Test()
        {
            //Arrange
            string[,,] map = MapCore.InitializeMap(11, 1, 11);
            Character fred = CharacterPool.CreateFredHero(map, new Vector3(5, 0, 5));
            fred.ShootingRange = 3;

            //Act
            string mapString = fred.GetCharactersInViewMapString(map, new List<Team>());
            List<Character> characters = fred.GetCharactersInRangeWithCurrentWeapon(map, new List<Team>());

            //Assert
            Assert.IsTrue(characters != null);
            Assert.AreEqual(0, characters.Count);
            string mapResult = @"
. . . . . . . . . . . 
. . . . . . . . . . . 
. . . o o o o o . . . 
. . o o o o o o o . . 
. . o o o o o o o . . 
. . o o o P o o o . . 
. . o o o o o o o . . 
. . o o o o o o o . . 
. . . o o o o o . . . 
. . . . . . . . . . . 
. . . . . . . . . . . 
";
            Assert.AreEqual(mapResult, mapString);
        }

        [TestMethod]
        public void FredCanFlankJethroInNorthCoverTest()
        {
            //Arrange
            string[,,] map = MapCore.InitializeMap(10, 1, 10);
            map[2, 0, 2] = CoverType.FullCover;
            map[4, 0, 2] = CoverType.FullCover;
            Character fred = CharacterPool.CreateFredHero(map, new Vector3(5, 0, 2));
            Character jethro = CharacterPool.CreateJethroBaddie(map, new Vector3(2, 0, 1));
            Team teamBaddie = new Team();
            teamBaddie.Characters.Add(jethro);

            //Act
            string mapString = fred.GetCharactersInViewMapString(map, new List<Team> { teamBaddie });
            List<Character> characters = fred.GetCharactersInRangeWithCurrentWeapon(map, new List<Team> { teamBaddie });

            //Assert
            Assert.IsTrue(characters != null);
            Assert.AreEqual(1, characters.Count);
            string mapResult = @"
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
o o o o o o o o o o 
. o o o o o o o o o 
. . . o o o o o o o 
. . ■ . ■ P o o o o 
. . P o o o o o o o 
. o o o o o o o o o 
";
            Assert.AreEqual(mapResult, mapString);

            //Now test cover for each character
            
            //Act
            CoverState coverStateResultPlayer = CharacterCover.CalculateCover(map, fred.Location, new List<Vector3>() { jethro.Location });
            CoverState coverStateResultEnemy = CharacterCover.CalculateCover(map, jethro.Location, new List<Vector3>() { fred.Location });

            //Assert
            Assert.IsTrue(coverStateResultPlayer.InFullCover);
            Assert.IsTrue(coverStateResultPlayer.InWestFullCover);
            Assert.IsTrue(!coverStateResultEnemy.InFullCover);
            Assert.IsTrue(!coverStateResultEnemy.InSouthFullCover);
        }
    }
}