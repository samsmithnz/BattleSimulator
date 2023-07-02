﻿using Battle.Logic.Characters;
using Battle.Logic.Game;
using Battle.Logic.Map;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Numerics;

namespace Battle.Tests.Scenarios
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    [TestCategory("L1")]
    public class ToggleSwitchScenarioTest
    {
        [TestMethod]
        public void ToggleSwitchTest()
        {
            //Arrange
            Mission mission = new()
            {
                Map = MapCore.InitializeMap(50, 1, 50)
            };
            mission.Objectives[0] = new MissionObjective(MissionObjectiveType.ToggleSwitch, false, new Vector3(8, 0, 8));
            Character fred = CharacterPool.CreateFredHero(mission.Map, new(4, 0, 4));
            fred.ActionPointsCurrent = 3;
            Character harry = CharacterPool.CreateHarryHero(mission.Map, new(4, 0, 6));
            Character jeff = CharacterPool.CreateJeffHero(mission.Map, new(4, 0, 8));
            Team team1 = new(1)
            {
                Name = "Good guys",
                Characters = new() { fred, harry, jeff },
                Color = "Blue"
            };
            mission.Teams.Add(team1);
            Character jethro = CharacterPool.CreateJethroBaddie(mission.Map, new(14, 0, 4));
            Team team2 = new(0)
            {
                Name = "Bad guys",
                Characters = new() { jethro },
                Color = "Red"
            };
            mission.Teams.Add(team2);
            mission.StartMission();

            //Act
            bool missionComplete = mission.CheckIfMissionIsCompleted();
            List<CharacterAction> fredActions = fred.GetCurrentActions(mission.Map);
            List<CharacterAction> harryActions = harry.GetCurrentActions(mission.Map);

            //Assert
            Assert.IsFalse(missionComplete);
            Assert.AreEqual(3, fred.ActionPointsCurrent);
            Assert.AreEqual(4, fredActions.Count);
            Assert.AreEqual(3, harryActions.Count);


            //Act
            mission.MoveCharacter(fred,
                            team1,
                            team2,
                            new Vector3(8, 0, 7));
            bool missionComplete2 = mission.CheckIfMissionIsCompleted();
            List<CharacterAction> fredActions2 = fred.GetCurrentActions(mission.Map);
            List<CharacterAction> harryActions2 = harry.GetCurrentActions(mission.Map);

            //Assert
            Assert.IsFalse(missionComplete2);
            Assert.AreEqual(2, fred.ActionPointsCurrent);
            Assert.AreEqual(5, fredActions2.Count);
            Assert.AreEqual("_toggle", fredActions2[4].Name);
            Assert.AreEqual("Toggle switch", fredActions2[4].Caption);
            Assert.AreEqual("0", fredActions2[4].KeyBinding);
            Assert.AreEqual(3, harryActions2.Count);

            //Act
            mission.ToggleSwitch(fred);
            bool missionComplete3 = mission.CheckIfMissionIsCompleted();
            List<CharacterAction> fredActions3 = fred.GetCurrentActions(mission.Map);
            List<CharacterAction> harryActions3 = harry.GetCurrentActions(mission.Map);

            //Assert
            Assert.IsTrue(missionComplete3);
            Assert.AreEqual(1, fred.ActionPointsCurrent);
            Assert.AreEqual(4, fredActions3.Count);
            Assert.AreEqual(3, harryActions3.Count);
        }
    }
}
