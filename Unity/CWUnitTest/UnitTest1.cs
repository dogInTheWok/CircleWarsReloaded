using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Engine;

namespace CWUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGame()
        {
            Game game = new Game();
            Assert.AreEqual(Game.GameState.NotStarted, game.CurrentGameState.Value);
        }

        [TestMethod]
        public void TestStartGame()
        {
            Game game = new Game();
            game.Start();
            Assert.AreEqual(Game.GameState.RunningDistribution, game.CurrentGameState.Value);
        }
    }
}
