﻿using Battle.Logic.Characters;
using Battle.Logic.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Battle.Tests.Utility
{
    [TestClass]
    [TestCategory("L0")]
    public class WrappingListTests
    {
        [TestMethod]
        public void WrappingListNextStartingAt0Test()
        {
            //Arrange
            int index = 0;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 1 }
            };

            //Act
            index = WrappingList.FindNextIndex(index, characters);

            //Assert
            Assert.AreEqual("B", characters[index].Name);
            Assert.AreEqual(1, characters[index].ActionPointsCurrent);
        }

        [TestMethod]
        public void WrappingListNextStartingAt1Test()
        {
            //Arrange
            int index = 1;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 1 }
            };

            //Act
            index = WrappingList.FindNextIndex(index, characters);

            //Assert
            Assert.AreEqual("C", characters[index].Name);
            Assert.AreEqual(1, characters[index].ActionPointsCurrent);
        }

        [TestMethod]
        public void WrappingListNextStartingAt2Test()
        {
            //Arrange
            int index = 2;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 1 }
            };

            //Act
            index = WrappingList.FindNextIndex(index, characters);

            //Assert
            Assert.AreEqual("A", characters[index].Name);
            Assert.AreEqual(1, characters[index].ActionPointsCurrent);
        }

        [TestMethod]
        public void WrappingListNextStartingAt0NextItemAPsAre0Test()
        {
            //Arrange
            int index = 0;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 0 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 1 }
            };

            //Act
            index = WrappingList.FindNextIndex(index, characters);

            //Assert
            Assert.AreEqual("C", characters[index].Name);
            Assert.AreEqual(1, characters[index].ActionPointsCurrent);
        }

        [TestMethod]
        public void WrappingListNextStartingAt1NextItemAPsAre0Test()
        {
            //Arrange
            int index = 1;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 0 }
            };

            //Act
            index = WrappingList.FindNextIndex(index, characters);

            //Assert
            Assert.AreEqual("A", characters[index].Name);
            Assert.AreEqual(1, characters[index].ActionPointsCurrent);
        }

        [TestMethod]
        public void WrappingListNextStartingAt2NextItemAPsAre0Test()
        {
            //Arrange
            int index = 2;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 0 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 1 }
            };

            //Act
            index = WrappingList.FindNextIndex(index, characters);

            //Assert
            Assert.AreEqual("B", characters[index].Name);
            Assert.AreEqual(1, characters[index].ActionPointsCurrent);
        }

        [TestMethod]
        public void WrappingListNextStartingAt0NoAPsTest()
        {
            //Arrange
            int index = 0;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 0 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 0 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 0 }
            };

            //Act
            index = WrappingList.FindNextIndex(index, characters);

            //Assert
            Assert.AreEqual(-1, index);
        }




        [TestMethod]
        public void WrappingListPreviousStartingAt0Test()
        {
            //Arrange
            int index = 0;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 1 }
            };

            //Act
            index = WrappingList.FindPreviousIndex(index, characters);

            //Assert
            Assert.AreEqual("C", characters[index].Name);
            Assert.AreEqual(1, characters[index].ActionPointsCurrent);
        }

        [TestMethod]
        public void WrappingListPreviousStartingAt1Test()
        {
            //Arrange
            int index = 1;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 1 }
            };

            //Act
            index = WrappingList.FindPreviousIndex(index, characters);

            //Assert
            Assert.AreEqual("A", characters[index].Name);
            Assert.AreEqual(1, characters[index].ActionPointsCurrent);
        }

        [TestMethod]
        public void WrappingListPreviousStartingAt2Test()
        {
            //Arrange
            int index = 2;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 1 }
            };

            //Act
            index = WrappingList.FindPreviousIndex(index, characters);

            //Assert
            Assert.AreEqual("B", characters[index].Name);
            Assert.AreEqual(1, characters[index].ActionPointsCurrent);
        }

        [TestMethod]
        public void WrappingListPreviousStartingAt0NextItemAPsAre0Test()
        {
            //Arrange
            int index = 0;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 0 }
            };

            //Act
            index = WrappingList.FindPreviousIndex(index, characters);

            //Assert
            Assert.AreEqual("B", characters[index].Name);
            Assert.AreEqual(1, characters[index].ActionPointsCurrent);
        }

        [TestMethod]
        public void WrappingListPreviousStartingAt1NextItemAPsAre0Test()
        {
            //Arrange
            int index = 1;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 0 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 1 }
            };

            //Act
            index = WrappingList.FindPreviousIndex(index, characters);

            //Assert
            Assert.AreEqual("C", characters[index].Name);
            Assert.AreEqual(1, characters[index].ActionPointsCurrent);
        }

        [TestMethod]
        public void WrappingListPreviousStartingAt2NextItemAPsAre0Test()
        {
            //Arrange
            int index = 2;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 1 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 0 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 1 }
            };

            //Act
            index = WrappingList.FindPreviousIndex(index, characters);

            //Assert
            Assert.AreEqual("A", characters[index].Name);
            Assert.AreEqual(1, characters[index].ActionPointsCurrent);
        }

        [TestMethod]
        public void WrappingListPreviousStartingAt0NoAPsTest()
        {
            //Arrange
            int index = 0;
            List<Character> characters = new()
            {
                new Character() { Name = "A", HitpointsCurrent = 1, ActionPointsCurrent = 0 },
                new Character() { Name = "B", HitpointsCurrent = 1, ActionPointsCurrent = 0 },
                new Character() { Name = "C", HitpointsCurrent = 1, ActionPointsCurrent = 0 }
            };

            //Act
            index = WrappingList.FindPreviousIndex(index, characters);

            //Assert
            Assert.AreEqual(-1, index);
        }

    }
}
