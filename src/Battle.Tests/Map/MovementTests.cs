﻿using Battle.Logic.Characters;
using Battle.Logic.Encounters;
using Battle.Logic.Map;
using Battle.Logic.Utility;
using Battle.Tests.Characters;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Numerics;

namespace Battle.Tests.Map
{
    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    [TestClass]
    [TestCategory("L0")]
    public class MovementTests
    {
        [TestMethod]
        public void MovementWithNoOverwatchTest()
        {
            //Arrange
            string[,,] map = MapUtility.InitializeMap(10, 1, 10);
            Vector3 destination = new Vector3(8, 0, 0);
            Character fred = CharacterPool.CreateFredHero(map);
            Queue<int> diceRolls = new Queue<int>(new List<int> { 65, 100, 100 }); //Chance to hit roll, damage roll, critical chance roll

            //Act
            List<Vector3> movementPossibileTiles = MovementPossibileTiles.GetMovementPossibileTiles(map, fred.Location, fred.MobilityRange);
            Vector3 destinationCheck = Vector3.Zero;
            foreach (Vector3 item in movementPossibileTiles)
            {
                if (item == destination)
                {
                    destinationCheck = item;
                }
            }
            Assert.AreEqual(destination, destinationCheck);
            PathFindingResult pathFindingResult = PathFinding.FindPath(fred.Location, destination, map);
            List<ActionResult> movementResults = CharacterMovement.MoveCharacter(fred, map, pathFindingResult, diceRolls);

            //Assert
            Assert.IsTrue(pathFindingResult != null);
            Assert.AreEqual(destination, fred.Location);
            Assert.AreEqual(8, pathFindingResult.Path.Count);
            Assert.AreEqual(8, movementResults.Count);
            Assert.AreEqual(new Vector3(0, 0, 0), movementResults[0].StartLocation);
            Assert.AreEqual(new Vector3(1, 0, 0), movementResults[0].EndLocation);
            Assert.AreEqual("Fred is moving from <0, 0, 0> to <1, 0, 0>", movementResults[0].Log[0]);


            string log = @"
Fred is moving from <0, 0, 0> to <1, 0, 0>
Fred is moving from <1, 0, 0> to <2, 0, 0>
Fred is moving from <2, 0, 0> to <3, 0, 0>
Fred is moving from <3, 0, 0> to <4, 0, 0>
Fred is moving from <4, 0, 0> to <5, 0, 0>
Fred is moving from <5, 0, 0> to <6, 0, 0>
Fred is moving from <6, 0, 0> to <7, 0, 0>
Fred is moving from <7, 0, 0> to <8, 0, 0>
";
            Assert.AreEqual(log, ActionResultLog.LogString(movementResults));
        }

        [TestMethod]
        public void MovementInRangeTest()
        {
            //Arrange
            string[,,] map = MapUtility.InitializeMap(10, 1, 10);
            Vector3 destination = new Vector3(8, 0, 0);
            Character fred = CharacterPool.CreateFredHero(map);

            //Act
            List<Vector3> movementPossibileTiles = MovementPossibileTiles.GetMovementPossibileTiles(map, fred.Location, fred.MobilityRange);
            Vector3 destinationCheck = Vector3.Zero;
            foreach (Vector3 item in movementPossibileTiles)
            {
                if (item == destination)
                {
                    destinationCheck = item;
                }
            }

            //Assert
            Assert.AreEqual(destination, destinationCheck);
        }

        [TestMethod]
        public void MovementOutOfRangeTest()
        {
            //Arrange
            string[,,] map = MapUtility.InitializeMap(10, 1, 10);
            Vector3 destination = new Vector3(8, 0, 1);
            Character fred = CharacterPool.CreateFredHero(map);

            //Act
            List<Vector3> movementPossibileTiles = MovementPossibileTiles.GetMovementPossibileTiles(map, fred.Location, fred.MobilityRange);
            Vector3 destinationCheck = Vector3.Zero;
            foreach (Vector3 item in movementPossibileTiles)
            {
                if (item == destination)
                {
                    destinationCheck = item;
                }
            }

            //Assert
            Assert.AreEqual(Vector3.Zero, destinationCheck);
        }

        [TestMethod]
        public void MovementJustInRangeTest()
        {
            //Arrange
            string[,,] map = MapUtility.InitializeMap(10, 1, 10);
            Vector3 destination = new Vector3(7, 0, 1);
            Character fred = CharacterPool.CreateFredHero(map);

            //Act
            List<Vector3> movementPossibileTiles = MovementPossibileTiles.GetMovementPossibileTiles(map, fred.Location, fred.MobilityRange);
            Vector3 destinationCheck = Vector3.Zero;
            foreach (Vector3 item in movementPossibileTiles)
            {
                if (item == destination)
                {
                    destinationCheck = item;
                }
            }

            //Assert
            Assert.AreEqual(destination, destinationCheck);
        }

        [TestMethod]
        public void MovementRange8Test()
        {
            //Arrange
            string[,,] map = MapUtility.InitializeMap(40, 1, 40);
            Vector3 destination = new Vector3(12, 0, 20);
            Character fred = CharacterPool.CreateFredHero(map);
            fred.SetLocation(new Vector3(20, 0, 20), map);

            //Act
            List<Vector3> movementPossibileTiles = MovementPossibileTiles.GetMovementPossibileTiles(map, fred.Location, fred.MobilityRange);
            Vector3 destinationCheck = Vector3.Zero;
            foreach (Vector3 item in movementPossibileTiles)
            {
                if (item == destination)
                {
                    destinationCheck = item;
                }
            }
            Assert.AreEqual(destination, destinationCheck);
            PathFindingResult pathFindingResult = PathFinding.FindPath(fred.Location, destination, map);
            List<ActionResult> movementResults = CharacterMovement.MoveCharacter(fred, map, pathFindingResult, null);

            string mapResult = MapCore.GetMapStringWithItems(map, movementPossibileTiles);
            string mapExpected = @"
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . o . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . o o o o o . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . o o o o o o o o o . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . o o o o o o o o o o o . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . o o o o o o o o o o o o o . . . . . . . . . . . . . 
. . . . . . . . . . . . . . o o o o o o o o o o o o o . . . . . . . . . . . . . 
. . . . . . . . . . . . . o o o o o o o o o o o o o o o . . . . . . . . . . . . 
. . . . . . . . . . . . . o o o o o o o o o o o o o o o . . . . . . . . . . . . 
. . . . . . . . . . . . P o o o o o o o . o o o o o o o o . . . . . . . . . . . 
. . . . . . . . . . . . . o o o o o o o o o o o o o o o . . . . . . . . . . . . 
. . . . . . . . . . . . . o o o o o o o o o o o o o o o . . . . . . . . . . . . 
. . . . . . . . . . . . . . o o o o o o o o o o o o o . . . . . . . . . . . . . 
. . . . . . . . . . . . . . o o o o o o o o o o o o o . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . o o o o o o o o o o o . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . o o o o o o o o o . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . o o o o o . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . o . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
";

            //Assert
            Assert.AreEqual(mapExpected, mapResult);
            Assert.AreEqual(1, fred.ActionPointsCurrent);
            Assert.AreEqual(8, movementResults.Count);
            string log = @"
Fred is moving from <20, 0, 20> to <19, 0, 20>
Fred is moving from <19, 0, 20> to <18, 0, 20>
Fred is moving from <18, 0, 20> to <17, 0, 20>
Fred is moving from <17, 0, 20> to <16, 0, 20>
Fred is moving from <16, 0, 20> to <15, 0, 20>
Fred is moving from <15, 0, 20> to <14, 0, 20>
Fred is moving from <14, 0, 20> to <13, 0, 20>
Fred is moving from <13, 0, 20> to <12, 0, 20>
";
            Assert.AreEqual(log, ActionResultLog.LogString(movementResults));

        }

        [TestMethod]
        public void MovementRange16Test()
        {
            //Arrange
            string[,,] map = MapUtility.InitializeMap(40, 1, 40);
            Vector3 destination = new Vector3(6, 0, 20);
            Character fred = CharacterPool.CreateFredHero(map);
            fred.SetLocation (new Vector3(20, 0, 20),map);

            //Act
            List<Vector3> movementPossibileTiles = MovementPossibileTiles.GetMovementPossibileTiles(map, fred.Location, 16);
            Vector3 destinationCheck = Vector3.Zero;
            foreach (Vector3 item in movementPossibileTiles)
            {
                if (item == destination)
                {
                    destinationCheck = item;
                }
            }
            Assert.AreEqual(destination, destinationCheck);
            PathFindingResult pathFindingResult = PathFinding.FindPath(fred.Location, destination, map);
            List<ActionResult> movementResults = CharacterMovement.MoveCharacter(fred, map, pathFindingResult, null);

            string mapResult = MapCore.GetMapStringWithItems(map, movementPossibileTiles);
            string mapExpected = @"
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . o . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . o o o o o . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . o o o o o o o o o . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . o o o o o o o o o o o o o o o . . . . . . . . . . . . 
. . . . . . . . . . . o o o o o o o o o o o o o o o o o o o . . . . . . . . . . 
. . . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . . 
. . . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . . 
. . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . 
. . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . 
. . . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . . 
. . . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . . 
. . . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . . 
. . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . 
. . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . 
. . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . 
. . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . 
. . . . o o P o o o o o o o o o o o o o . o o o o o o o o o o o o o o o o . . . 
. . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . 
. . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . 
. . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . 
. . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . 
. . . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . . 
. . . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . . 
. . . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . . 
. . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . 
. . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . 
. . . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . . 
. . . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . . 
. . . . . . . . . . . o o o o o o o o o o o o o o o o o o o . . . . . . . . . . 
. . . . . . . . . . . . . o o o o o o o o o o o o o o o . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . o o o o o o o o o . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . o o o o o . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . o . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
";

            //Assert
            Assert.AreEqual(mapExpected, mapResult);
            Assert.AreEqual(0, fred.ActionPointsCurrent);
            Assert.AreEqual(14, movementResults.Count);
            string log = @"
Fred is moving from <20, 0, 20> to <19, 0, 20>
Fred is moving from <19, 0, 20> to <18, 0, 20>
Fred is moving from <18, 0, 20> to <17, 0, 20>
Fred is moving from <17, 0, 20> to <16, 0, 20>
Fred is moving from <16, 0, 20> to <15, 0, 20>
Fred is moving from <15, 0, 20> to <14, 0, 20>
Fred is moving from <14, 0, 20> to <13, 0, 20>
Fred is moving from <13, 0, 20> to <12, 0, 20>
Fred is moving from <12, 0, 20> to <11, 0, 20>
Fred is moving from <11, 0, 20> to <10, 0, 20>
Fred is moving from <10, 0, 20> to <9, 0, 20>
Fred is moving from <9, 0, 20> to <8, 0, 20>
Fred is moving from <8, 0, 20> to <7, 0, 20>
Fred is moving from <7, 0, 20> to <6, 0, 20>
";
            Assert.AreEqual(log, ActionResultLog.LogString(movementResults));
        }

        [TestMethod]
        public void MovementRange8AndRange16LayedTest()
        {
            //Arrange
            string[,,] map = MapUtility.InitializeMap(40, 1, 40);
            Character fred = CharacterPool.CreateFredHero(map);
            fred.SetLocation(new Vector3(20, 0, 20), map);
            Vector3 destination = new Vector3(6, 0, 20);

            //Act
            List<Vector3> movementPossibileTilesRange8 = MovementPossibileTiles.GetMovementPossibileTiles(map, fred.Location, 8);
            List<Vector3> movementPossibileTilesRange16 = MovementPossibileTiles.GetMovementPossibileTiles(map, fred.Location, 16);
            Vector3 destinationCheck = Vector3.Zero;
            foreach (Vector3 item in movementPossibileTilesRange16)
            {
                if (item == destination)
                {
                    destinationCheck = item;
                }
            }
            Assert.AreEqual(destination, destinationCheck);
            PathFindingResult pathFindingResult = PathFinding.FindPath(fred.Location, destination, map);
            List<ActionResult> movementResults = CharacterMovement.MoveCharacter(fred, map, pathFindingResult, null);

            string mapResult = MapCore.GetMapStringWithItemLayers(map, movementPossibileTilesRange16, movementPossibileTilesRange8);
            string mapExpected = @"
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . o . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . o o o o o . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . o o o o o o o o o . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . o o o o o o o o o o o o o o o . . . . . . . . . . . . 
. . . . . . . . . . . o o o o o o o o o o o o o o o o o o o . . . . . . . . . . 
. . . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . . 
. . . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . . 
. . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . 
. . . . . . . . o o o o o o o o o o o o O o o o o o o o o o o o o . . . . . . . 
. . . . . . . o o o o o o o o o o o O O O O O o o o o o o o o o o o . . . . . . 
. . . . . . . o o o o o o o o o O O O O O O O O O o o o o o o o o o . . . . . . 
. . . . . . . o o o o o o o o O O O O O O O O O O O o o o o o o o o . . . . . . 
. . . . . . o o o o o o o o O O O O O O O O O O O O O o o o o o o o o . . . . . 
. . . . . . o o o o o o o o O O O O O O O O O O O O O o o o o o o o o . . . . . 
. . . . . o o o o o o o o O O O O O O O O O O O O O O O o o o o o o o o . . . . 
. . . . . o o o o o o o o O O O O O O O O O O O O O O O o o o o o o o o . . . . 
. . . . o o P o o o o o O O O O O O O O . O O O O O O O O o o o o o o o o . . . 
. . . . . o o o o o o o o O O O O O O O O O O O O O O O o o o o o o o o . . . . 
. . . . . o o o o o o o o O O O O O O O O O O O O O O O o o o o o o o o . . . . 
. . . . . . o o o o o o o o O O O O O O O O O O O O O o o o o o o o o . . . . . 
. . . . . . o o o o o o o o O O O O O O O O O O O O O o o o o o o o o . . . . . 
. . . . . . . o o o o o o o o O O O O O O O O O O O o o o o o o o o . . . . . . 
. . . . . . . o o o o o o o o o O O O O O O O O O o o o o o o o o o . . . . . . 
. . . . . . . o o o o o o o o o o o O O O O O o o o o o o o o o o o . . . . . . 
. . . . . . . . o o o o o o o o o o o o O o o o o o o o o o o o o . . . . . . . 
. . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . 
. . . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . . 
. . . . . . . . . o o o o o o o o o o o o o o o o o o o o o o o . . . . . . . . 
. . . . . . . . . . . o o o o o o o o o o o o o o o o o o o . . . . . . . . . . 
. . . . . . . . . . . . . o o o o o o o o o o o o o o o . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . o o o o o o o o o . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . o o o o o . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . o . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
";

            //Assert
            Assert.AreEqual(mapExpected, mapResult);
            Assert.AreEqual(0, fred.ActionPointsCurrent);
            Assert.AreEqual(14, movementResults.Count);
            string log = @"
Fred is moving from <20, 0, 20> to <19, 0, 20>
Fred is moving from <19, 0, 20> to <18, 0, 20>
Fred is moving from <18, 0, 20> to <17, 0, 20>
Fred is moving from <17, 0, 20> to <16, 0, 20>
Fred is moving from <16, 0, 20> to <15, 0, 20>
Fred is moving from <15, 0, 20> to <14, 0, 20>
Fred is moving from <14, 0, 20> to <13, 0, 20>
Fred is moving from <13, 0, 20> to <12, 0, 20>
Fred is moving from <12, 0, 20> to <11, 0, 20>
Fred is moving from <11, 0, 20> to <10, 0, 20>
Fred is moving from <10, 0, 20> to <9, 0, 20>
Fred is moving from <9, 0, 20> to <8, 0, 20>
Fred is moving from <8, 0, 20> to <7, 0, 20>
Fred is moving from <7, 0, 20> to <6, 0, 20>
";
            Assert.AreEqual(log, ActionResultLog.LogString(movementResults));
        }
    }
}
