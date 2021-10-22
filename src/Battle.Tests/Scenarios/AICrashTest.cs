﻿//using Battle.Logic.Characters;
//using Battle.Logic.Encounters;
//using Battle.Logic.Game;
//using Battle.Logic.Map;
//using Battle.Logic.SaveGames;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Collections.Generic;
//using System.IO;
//using System.Numerics;
//using System.Reflection;

//namespace Battle.Tests.Scenarios
//{
//    [System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
//    [TestClass]
//    [TestCategory("L2")]
//    public class AICrashTest
//    {
//        private string _rootPath;

//        [TestInitialize]
//        public void GameSerializationStartUp()
//        {
//            _rootPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
//        }

//        [TestMethod]
//        public void AICrashesOnFirstTurnTest()
//        {
//            //Arrange
//            string path = _rootPath + @"\SaveGames\Saves\Save013.json";

//            //Act
//            string fileContents;
//            using (var streamReader = new StreamReader(path))
//            {
//                fileContents = streamReader.ReadToEnd();
//            }
//            Mission mission = GameSerialization.LoadGame(fileContents);
//            mission.StartMission();
//            mission.MoveToNextTurn();

//            AIAction aIAction = mission.CalculateAIAction(mission.Teams[1].Characters[0], mission.Teams);
//            string mapString = aIAction.MapString;
//            string mapStringExpected = @"
//. . . . . . . . . . . □ . . ■ . . □ . . . . . . . . □ . . . . . ■ . . . . . . . . . ■ . . . ■ . . . 
//. . . . . . . . . . . . . . . . . . . . ■ . . . ■ . . . . . . . . . . . . . . . . . . ■ . . . . □ . 
//. . . . . . . . . . . . . . . . ■ . . . . . . . . . . . . ■ . . . . . . . . . . . . . . . . . . . . 
//. . . . . . . . . . ■ . . . . . . . . ■ . . . . . . . . . □ ■ . . . . . . . . . . . . . . . . . . . 
//. . . ■ . . ■ . . . □ . . . . . . . . . . □ . . . . . . . . . . . . . . . . . . . . . . . . . . . . 
//. . . . □ . . . . □ . . . . . . . . . . . . . . . . . . . . . . . . □ . . . . . . . . ■ . . . . . . 
//. . . . . □ . . . . . . . . . . . . . . . . . . . □ . . . . . . . . . . . . . . . □ . . . . . . □ . 
//. . . . ■ . . . . . . . . . □ . . . . . . . . . . . . . . . . . . . . . . . . . ■ . . . . . □ . . . 
//. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . ■ . . . . ■ . . □ . . . . . . . . . . . 
//. . . . . . . . . . . . . . . . . . . . . . □ . . . . □ . . □ . . . . . . . . . ■ . . . . . . . . . 
//. . . . . . . . . . . □ . . . . □ . . . . . . . . . . . . . . . . . . . . □ . . . . . . ■ . . . . . 
//. . . . . . . □ . . . . . . . . . . . . . . . . . . . . . . . ■ . . . . . . ■ . . . . . . . . . . . 
//. . . . . ■ . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . ■ . . . . . . . . . 
//. . . . . . . . . . . . . . . . . . . 1 ■ . . . ■ . . . . . . ■ . . . ■ . . . . . . . . . . . . . . 
//. . . . . . ■ . . . . . . ■ . . . 1 1 0 0 0 . . . . . □ . . . . . . . . . . . . . . . . . . . . . . 
//. □ . . . . . . . . . . . . . 1 1 1 0 0 1 1 0 0 . . . . . . ■ . . . . . . . . . . . ■ . . ■ . . . . 
//. . . . . . . . . . . . 1 1 1 1 1 0 0 0 □ 1 0 0 2 ■ P □ . . . . . . . . . . . . . . . . . . . . . . 
//. . . . . . . . ■ . 1 4 3 3 3 3 3 0 0 1 □ 1 0 0 □ 4 1 1 1 . □ . . . . . . . . ■ . □ . . . . . . . . 
//. . . . □ . □ . . 1 1 ■ 3 3 3 3 1 1 3 1 1 0 0 1 1 0 0 1 1 1 1 . . . . . . . . . . ■ . . . . . . . . 
//. . . . . . . . 1 1 1 3 3 3 3 1 1 1 □ 3 1 1 3 1 1 3 1 0 0 1 3 . . . . . . . . . . . . . . . . . □ . 
//. . . . . . . 1 1 1 4 3 3 3 1 1 1 1 1 1 1 3 3 1 □ □ 1 0 0 1 □ 3 . . . . . . . . . . . . . . . ■ . . 
//□ . . . . . . 1 1 1 ■ 3 3 1 1 1 1 1 1 1 3 1 1 3 1 1 0 0 ■ 2 0 □ . ■ . . . ■ . . . ■ . . . . . . ■ . 
//. . . . . . 1 1 1 1 1 3 3 1 1 3 1 1 4 3 3 3 3 1 1 1 1 1 0 0 0 2 1 . . . . . . . □ . . □ . . . . . . 
//. . . . . . 1 1 1 1 3 3 1 4 3 1 3 1 ■ 6 1 1 1 1 1 3 3 0 0 0 0 ■ 4 . ■ . . . □ . □ . . . . . . . . . 
//. . . . . . . 3 1 4 3 1 4 ■ 1 1 3 1 3 3 1 1 4 1 3 3 □ 1 0 0 2 4 1 . . . . . . . . . . . . . . . . . 
//. . . . . 1 1 □ 1 ■ 1 1 □ 1 4 3 4 1 3 3 1 4 ■ 6 3 3 0 1 0 0 ■ ■ 2 1 . . . □ . . . . . . . . . . ■ . 
//. . . . . 1 1 1 1 ■ 1 1 1 1 ■ 3 ■ 6 3 1 1 ■ 6 5 5 2 0 □ 1 0 0 0 ■ 2 . . . . . □ . . . □ . . . . . . 
//□ . . . □ 1 1 1 1 1 1 1 1 1 1 3 3 3 3 1 5 5 5 5 2 2 2 0 0 0 0 1 0 0 0 . . . . . . . . . ■ . . . . . 
//. . . . 1 1 1 1 1 1 1 1 1 1 1 3 3 3 3 3 7 5 5 5 2 2 2 0 0 0 0 0 1 1 1 . . . . . . . . . . . . . . . 
//■ . . 1 0 1 1 1 1 1 1 3 1 1 1 5 3 5 3 P □ 7 5 2 2 2 2 0 0 0 0 1 1 0 0 . . . . . . . . . . . . . . . 
//. ■ . . 0 1 1 1 1 1 1 1 1 1 3 □ 7 5 3 5 5 5 3 2 2 2 2 0 0 0 1 0 □ 1 . . . . . . . . . . . . . . . . 
//■ . . . 0 1 1 1 1 1 1 1 1 3 5 5 5 3 5 5 5 3 2 2 3 2 2 0 0 1 0 0 0 1 0 . . . . . . □ . . □ . . . . . 
//. . . . . 0 1 1 1 1 1 3 5 3 5 5 5 3 5 5 3 2 2 2 □ 3 2 3 0 0 0 0 0 □ . . . . . . . . . . . □ . . . . 
//. . . . . 0 1 1 1 3 3 3 □ 3 5 5 6 5 5 5 3 2 0 0 0 0 ■ 2 0 0 0 0 0 ■ . . . ■ . . . . □ . . . . . . . 
//. . . . . . 1 3 3 3 3 3 3 3 5 5 ■ 6 3 1 2 2 2 2 2 0 0 □ 1 0 0 0 6 . . . . . . . . . . . □ ■ . . . . 
//. . . . . □ 1 3 3 3 3 3 3 3 3 5 5 5 3 3 2 2 2 2 0 0 0 0 0 0 3 1 ■ . . . . . . . . . . . . . □ ■ . . 
//. . P ■ . . 0 1 3 □ 3 3 □ 3 3 □ 5 3 3 2 2 2 0 0 0 0 0 0 0 ■ 4 □ 1 . . . . . . . . . . . . . . . . . 
//. . . . . . . 1 3 3 3 3 3 3 3 3 3 1 1 2 0 0 0 0 0 0 0 0 □ 1 1 3 . . . . □ . . . . . □ . . . . . . . 
//. . . . . . . ■ 4 3 3 3 3 3 3 3 1 1 0 0 0 0 0 0 0 0 0 1 1 1 ■ 6 □ . . . . . . . ■ . □ . . . . . . . 
//. . . . □ □ . . ■ 1 3 3 3 3 3 1 1 1 0 0 0 0 0 0 0 1 1 1 3 3 1 . . . . . . . . ■ . . . . . . □ . . . 
//. . . . . . . . . 0 1 3 3 3 1 1 1 0 0 0 0 0 0 0 1 1 3 1 1 1 1 . . . . . . . . . . . . □ . . . . . . 
//. . . □ . □ . . . □ 0 ■ 6 3 1 1 1 0 0 0 0 0 1 1 ■ □ 3 1 0 . . . . . . . . . . . . . . . . . . . . . 
//. . . . . . . . □ . . . □ 1 1 1 0 0 0 0 1 1 1 1 1 . □ . . . . . . . . . . . . . . . . . . . . . . . 
//. . . □ . □ . ■ . . . . . . . 1 0 0 1 1 1 0 0 0 . . . . . . . . . . . . . . . . . . . . . . . . □ . 
//. . . . . . . . . . . . . . . . . 1 0 0 0 0 . . . . . . . . . . . ■ . □ . . . ■ . . . . . . . . . . 
//. . . . . . . . . ■ ■ . . . ■ . . . . 1 . . . . . . ■ . . . . . □ . . . . . . . . ■ . . . . . ■ . . 
//. . . . . . . . . . . . . . P . . . . . . . . . . . ■ . . . . . . . . . . . . . . . . . . . . . . . 
//. . . . . . . . . P . . . . . . □ . . . . . . . . . . . . . . . . . . . . . . . . . . . ■ . . . □ . 
//. . . . . . . . . . . . . . . . . . . . . . . . . . . . . □ . □ . . . . . . . . . . . . . . . . ■ . 
//";

//            //Assert
//            Assert.AreEqual(ActionTypeEnum.MoveThenAttack, aIAction.ActionType);
//            //Assert.AreEqual(6, aIAction.Score);
//            Assert.AreEqual(new Vector3(19, 0, 19), aIAction.StartLocation);
//            Assert.AreEqual(new Vector3(16, 0, 23), aIAction.EndLocation);
//            //Assert.AreEqual(mapStringExpected, mapString);

//            //Act
//            mission.MoveCharacter(mission.Teams[1].Characters[0],
//                mission.Teams[1],
//                mission.Teams[0],
//                aIAction.EndLocation);
//            EncounterResult encounterResult = mission.AttackCharacter(mission.Teams[1].Characters[0],
//                mission.Teams[1].Characters[0].WeaponEquipped,
//                mission.Teams[1].GetCharacter(mission.Teams[1].Characters[0].TargetCharacters[0]),
//                mission.Teams[1],
//                mission.Teams[0]);

//            //Assert
//            string log = @"
//Jethro is attacking with Shotgun, targeted on Fred
//Hit: Chance to hit: 75, (dice roll: 81)
//Damage range: 3-5, (dice roll: 76)
//Critical chance: 70, (dice roll: 55)
//Critical damage range: 9-13, (dice roll: 76)
//Armor prevented 1 damage to character Fred
//11 damage dealt to character Fred, HP is now -7
//Fred is killed
//100 XP added to character Jethro, for a total of 100 XP
//Jethro is ready to level up
//";
//            Assert.AreEqual(log, encounterResult.LogString);

//            List<Character> charactersInView = FieldOfView.GetCharactersInView(mission.Map,
//                   mission.Teams[1].Characters[1].Location,
//                   mission.Teams[1].Characters[1].ShootingRange,
//                   mission.Teams[0].Characters);
//            Assert.AreEqual(0, charactersInView.Count);
//            List<Character> charactersInView2 = FieldOfView.GetCharactersInView(mission.Map,
//                   mission.Teams[1].Characters[0].Location,
//                   mission.Teams[1].Characters[1].ShootingRange,
//                   mission.Teams[0].Characters);
//            Assert.AreEqual(2, charactersInView2.Count);
//            CharacterAI ai2 = new CharacterAI();
//            AIAction aIAction2 = ai2.CalculateAIAction(mission.Map,
//                mission.Teams[1].Characters[1],
//                mission.Teams,
//                mission.RandomNumbers);
//            string mapString2 = aIAction2.MapString;
//            string mapStringExpected2 = @"
//. . . . . . . . . . . □ . . ■ . . □ . . . . . . . . □ . . . . . ■ . . . . . . . . . ■ . . . ■ . . . 
//. . . . . . . . . . . . . . . . . . . . ■ . . . ■ 4 1 1 1 . . . . . . . . . . . . . . ■ . . . . □ . 
//. . . . . . . . . . . . . . . . ■ . . . . . 1 1 1 1 1 1 1 ■ . . . . . . . . . . . . . . . . . . . . 
//. . . . . . . . . . ■ . . . . . . . . ■ 4 3 1 1 1 1 1 1 1 □ ■ 4 1 1 . . . . . . . . . . . . . . . . 
//. . . ■ . . ■ . . . □ . . . . . . 1 1 1 1 □ 3 1 1 1 1 1 1 1 1 1 1 1 3 1 . . . . . . . . . . . . . . 
//. . . . □ . . . . □ . . . . . 1 1 1 1 1 1 1 1 1 1 3 1 1 1 1 1 1 1 1 □ 3 1 . . . . . . ■ . . . . . . 
//. . . . . □ . . . . . . . . . 1 1 1 1 1 1 1 1 1 1 □ 3 1 1 1 1 1 1 1 1 1 1 1 . . . □ . . . . . . □ . 
//. . . . ■ . . . . . . . . . □ 1 1 1 1 1 1 1 1 1 1 1 1 1 1 1 4 1 1 1 1 4 1 1 3 . ■ . . . . . □ . . . 
//. . . . . . . . . . . . . . 0 0 1 1 1 1 1 1 3 1 1 1 0 3 1 1 ■ 4 1 1 1 ■ 4 1 □ . . . . . . . . . . . 
//. . . . . . . . . . . . . 0 0 0 3 1 1 1 1 1 □ 3 0 0 0 □ 0 1 □ 3 1 1 1 1 1 3 1 1 ■ . . . . . . . . . 
//. . . . . . . . . . . □ . 0 0 1 □ 3 1 1 1 1 0 0 0 0 0 0 0 0 0 4 1 1 1 1 1 □ 4 1 . . . . ■ . . . . . 
//. . . . . . . □ . . . . . 0 0 1 1 1 1 1 1 0 0 0 0 0 0 0 0 0 0 ■ 4 1 1 1 1 1 ■ 4 . . . . . . . . . . 
//. . . . . ■ . . . . . . 0 0 1 1 1 1 1 1 4 0 1 0 0 0 0 0 0 0 0 0 0 1 1 4 1 1 1 1 ■ . . . . . . . . . 
//. . . . . . . . . . . . 0 2 1 1 1 1 1 1 ■ 2 0 1 ■ 0 0 0 0 0 0 ■ 0 1 1 ■ 4 1 1 1 1 . . . . . . . . . 
//. . . . . . ■ . . . . 0 0 ■ 1 1 1 1 1 0 0 0 1 0 1 0 0 □ 0 0 0 0 0 0 1 1 1 1 1 1 1 1 . . . . . . . . 
//. □ . . . . . . . . . 0 0 1 1 1 1 1 0 0 1 1 0 0 0 4 0 0 0 0 ■ 0 0 1 1 1 1 1 1 1 1 . ■ . . ■ . . . . 
//. . . . . . . . . . . . 1 1 1 1 1 0 0 0 □ 1 2 0 2 ■ P □ 0 0 0 0 0 0 1 1 1 1 1 4 1 3 . . . . . . . . 
//. . . . . . . . ■ . . . 3 3 3 3 3 0 0 1 □ 3 2 2 □ 0 1 1 0 0 □ 0 0 1 1 1 1 1 1 ■ 4 □ . . . . . . . . 
//. . . . □ . □ . . . . ■ 3 3 3 3 1 1 3 1 3 4 2 3 0 0 2 3 1 0 0 0 0 0 1 1 1 1 1 1 1 ■ . . . . . . . . 
//. . . . . . . . . . . . 3 3 3 1 1 1 □ 3 5 5 3 1 1 5 3 0 2 0 3 0 0 1 1 1 1 1 1 1 1 . . . . . . . □ . 
//. . . . . . . . . . . . 3 3 1 1 1 1 1 1 5 5 3 1 □ □ 3 2 2 0 □ 3 1 4 1 1 1 4 1 1 1 . . . . . . ■ . . 
//□ . . . . . . . . . ■ . . 1 1 1 1 1 1 1 3 1 3 0 1 5 4 2 ■ 2 0 □ 4 ■ 4 1 1 ■ 4 1 . ■ . . . . . . ■ . 
//. . . . . . . . . . . . . 1 1 3 1 1 4 3 1 3 5 1 1 5 5 3 2 0 0 2 1 1 4 1 1 1 3 1 □ . . □ . . . . . . 
//. . . . . . . . . . . . . 4 3 1 1 1 ■ 4 3 1 1 1 5 5 5 2 2 0 0 ■ 4 1 ■ 4 1 1 □ 3 □ . . . . . . . . . 
//. . . . . . . . . . . . . ■ 1 1 3 3 1 3 1 1 4 3 3 3 □ 1 0 0 2 4 1 0 1 0 1 3 1 . . . . . . . . . . . 
//. . . . . . . □ . ■ . . □ . 4 3 4 1 1 1 1 4 ■ 6 3 1 0 1 0 0 ■ ■ 2 1 0 0 0 □ 3 . . . . . . . . . ■ . 
//. . . . . . . . . ■ . . . . ■ 1 ■ 4 1 1 1 ■ 6 3 3 0 0 □ 1 0 0 0 ■ 2 0 0 0 0 . □ . . . □ . . . . . . 
//□ . . . □ . . . . . . . . . . . 1 1 1 1 3 3 3 3 0 0 0 0 0 0 0 1 0 0 0 0 0 . . . . . . . ■ . . . . . 
//. . . . . . . . . . . . . . . . . 1 3 3 5 3 3 3 0 0 0 0 0 0 0 0 1 1 1 0 . . . . . . . . . . . . . . 
//■ . . . . . . . . . . . . . . . . . . 3 □ 5 3 0 0 0 0 0 0 0 0 1 . 0 . . . . . . . . . . . . . . . . 
//. ■ . . . . . . . . . . . . . □ . . . . . . 1 0 0 0 0 0 0 0 1 . □ . . . . . . . . . . . . . . . . . 
//■ . . . . . . . . . . . . . . . . . . . . . . . 1 0 0 . 0 . . . . . . . . . . . . □ . . □ . . . . . 
//. . . . . . . . . . . . . . . . . . . . . . . . □ . . . . . . . . □ . . . . . . . . . . . □ . . . . 
//. . . . . . . . . . . . □ . . . P . . . . . . . . . ■ . . . . . . ■ . . . ■ . . . . □ . . . . . . . 
//. . . . . . . . . . . . . . . . ■ . . . . . . . . . . □ . . . . . . . . . . . . . . . . □ ■ . . . . 
//. . . . . □ . . . . . . . . . . . . . . . . . . . . . . . . . . ■ . . . . . . . . . . . . . □ ■ . . 
//. . P ■ . . . . . □ . . □ . . □ . . . . . . . . . . . . . ■ . □ . . . . . . . . . . . . . . . . . . 
//. . . . . . . . . . . . . . . . . . . . . . . . . . . . □ . . . . . . . □ . . . . . □ . . . . . . . 
//. . . . . . . ■ . . . . . . . . . . . . . . . . . . . . . . ■ . □ . . . . . . . ■ . □ . . . . . . . 
//. . . . □ □ . . ■ . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . ■ . . . . . . □ . . . 
//. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . □ . . . . . . 
//. . . □ . □ . . . □ . ■ . . . . . . . . . . . . ■ □ . . . . . . . . . . . . . . . . . . . . . . . . 
//. . . . . . . . □ . . . □ . . . . . . . . . . . . . □ . . . . . . . . . . . . . . . . . . . . . . . 
//. . . □ . □ . ■ . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . □ . 
//. . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . . ■ . □ . . . ■ . . . . . . . . . . 
//. . . . . . . . . ■ ■ . . . ■ . . . . . . . . . . . ■ . . . . . □ . . . . . . . . ■ . . . . . ■ . . 
//. . . . . . . . . . . . . . P . . . . . . . . . . . ■ . . . . . . . . . . . . . . . . . . . . . . . 
//. . . . . . . . . P . . . . . . □ . . . . . . . . . . . . . . . . . . . . . . . . . . . ■ . . . □ . 
//. . . . . . . . . . . . . . . . . . . . . . . . . . . . . □ . □ . . . . . . . . . . . . . . . . ■ . 
//";

//            //Assert
//            Assert.AreEqual(ActionTypeEnum.DoubleMove, aIAction2.ActionType);
//            //Assert.AreEqual(5, aIAction2.Score);
//            Assert.AreEqual(new Vector3(26, 0, 32), aIAction2.StartLocation);
//            Assert.AreEqual(new Vector3(20, 0, 20), aIAction2.EndLocation);
//            //Assert.AreEqual(mapStringExpected2, mapString2);

//            mission.MoveCharacter(mission.Teams[1].Characters[1],
//                mission.Teams[1],
//                mission.Teams[0],
//                aIAction2.EndLocation);
//        }
//    }
//}
