using System;
using NUnit.Framework;
using ReversiRestApi;

namespace ReversiUnitTests
{
    [TestFixture]
    public class SpelTest
    {
        // geen kleur = 0
        // Wit = 1
        // Zwart = 2

        [Test]
        public void ZetMogelijk__BuitenBord_Exception()
        {
            // Arrange
            var spel = new Game();
            //     0 1 2 3 4 5 6 7
            //                     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     1 <
            // Act
            spel.PlayerTurn = Color.White;
            //var actual = spel.ZetMogelijk(8, 8);
            var ex = Assert.Throws<Exception>(delegate { spel.MoveIsPossible(8, 8); });
            Assert.That(ex.Message, Is.EqualTo("Zet (8,8) ligt buiten het bord!"));

            // Assert
        }

        [Test]
        public void ZetMogelijk_StartSituatieZet23Zwart_ReturnTrue()
        {
            // Arrange
            var spel = new Game();
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 2 0 0 0 0  <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.Black;
            var actual = spel.MoveIsPossible(2, 3);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_StartSituatieZet23Wit_ReturnFalse()
        {
            // Arrange
            var spel = new Game();
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 1 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.White;
            var actual = spel.MoveIsPossible(2, 3);
            // Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void ZetMogelijk_ZetAanDeRandBoven_ReturnTrue()
        {
            // Arrange
            var spel = new Game();
            spel.Board[1, 3] = Color.White;
            spel.Board[2, 3] = Color.White;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            spel.PlayerTurn = Color.Black;
            var actual = spel.MoveIsPossible(0, 3);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandBoven_ReturnFalse()
        {
            // Arrange
            var spel = new Game();
            spel.Board[1, 3] = Color.White;
            spel.Board[2, 3] = Color.White;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 1 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.White;
            var actual = spel.MoveIsPossible(0, 3);
            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandBovenEnTotBenedenReedsGevuld_ReturnTrue()
        {
            // Arrange
            var spel = new Game();
            spel.Board[1, 3] = Color.White;
            spel.Board[2, 3] = Color.White;
            spel.Board[3, 3] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[5, 3] = Color.White;
            spel.Board[6, 3] = Color.White;
            spel.Board[7, 3] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 1 1 0 0 0
            // 5   0 0 0 1 0 0 0 0
            // 6   0 0 0 1 0 0 0 0
            // 7   0 0 0 2 0 0 0 0

            // Act
            spel.PlayerTurn = Color.Black;
            var actual = spel.MoveIsPossible(0, 3);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandBovenEnTotBenedenReedsGevuld_ReturnFalse()
        {
            // Arrange
            var spel = new Game();
            spel.Board[1, 3] = Color.White;
            spel.Board[2, 3] = Color.White;
            spel.Board[3, 3] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[5, 3] = Color.White;
            spel.Board[6, 3] = Color.White;
            spel.Board[7, 3] = Color.White;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 1 1 0 0 0
            // 5   0 0 0 1 0 0 0 0
            // 6   0 0 0 1 0 0 0 0
            // 7   0 0 0 1 0 0 0 0

            // Act
            spel.PlayerTurn = Color.Black;
            var actual = spel.MoveIsPossible(0, 3);
            // Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void ZetMogelijk_ZetAanDeRandRechts_ReturnTrue()
        {
            // Arrange
            var spel = new Game();
            spel.Board[4, 5] = Color.White;
            spel.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 2 0 0 0 0  
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 1 1 2 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            spel.PlayerTurn = Color.Black;
            var actual = spel.MoveIsPossible(4, 7);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandRechts_ReturnFalse()
        {
            // Arrange
            var spel = new Game();
            spel.Board[4, 5] = Color.White;
            spel.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 1 0 0 0 0  
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 1 1 1 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.White;
            var actual = spel.MoveIsPossible(4, 7);
            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandRechtsEnTotLinksReedsGevuld_ReturnTrue()
        {
            // Arrange
            var spel = new Game();
            spel.Board[4, 0] = Color.Black;
            spel.Board[4, 1] = Color.White;
            spel.Board[4, 2] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[4, 4] = Color.White;
            spel.Board[4, 5] = Color.White;
            spel.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0 
            // 4   2 1 1 1 1 1 1 2 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.Black;
            var actual = spel.MoveIsPossible(4, 7);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandRechtsEnTotLinksReedsGevuld_ReturnFalse()
        {
            // Arrange
            var spel = new Game();
            spel.Board[4, 0] = Color.Black;
            spel.Board[4, 1] = Color.White;
            spel.Board[4, 2] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[4, 4] = Color.White;
            spel.Board[4, 5] = Color.White;
            spel.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  

            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   2 1 1 1 1 1 1 1 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.White;
            var actual = spel.MoveIsPossible(4, 7);
            // Assert
            Assert.IsFalse(actual);
        }


        //     0 1 2 3 4 5 6 7
        //                     
        // 0   0 0 0 0 0 0 0 0  
        // 1   0 0 0 0 0 0 0 0
        // 2   0 0 0 0 0 0 0 0
        // 3   0 0 0 1 2 0 0 0
        // 4   0 0 0 2 1 0 0 0
        // 5   0 0 0 0 0 0 0 0
        // 6   0 0 0 0 0 0 0 0
        // 7   0 0 0 0 0 0 0 0


        [Test]
        public void ZetMogelijk_StartSituatieZet22Wit_ReturnFalse()
        {
            // Arrange
            var spel = new Game();
            //     0 1 2 3 4 5 6 7
            //         v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.White;
            var actual = spel.MoveIsPossible(2, 2);
            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ZetMogelijk_StartSituatieZet22Zwart_ReturnFalse()
        {
            // Arrange
            var spel = new Game();
            //     0 1 2 3 4 5 6 7
            //         v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.Black;
            var actual = spel.MoveIsPossible(2, 2);
            // Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void ZetMogelijk_ZetAanDeRandRechtsBoven_ReturnTrue()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 5] = Color.Black;
            spel.Board[1, 6] = Color.Black;
            spel.Board[5, 2] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 1  <
            // 1   0 0 0 0 0 0 2 0
            // 2   0 0 0 0 0 2 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 1 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            spel.PlayerTurn = Color.White;
            var actual = spel.MoveIsPossible(0, 7);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandRechtsBoven_ReturnFalse()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 5] = Color.Black;
            spel.Board[1, 6] = Color.Black;
            spel.Board[5, 2] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 2  <
            // 1   0 0 0 0 0 0 2 0
            // 2   0 0 0 0 0 2 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 1 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            spel.PlayerTurn = Color.Black;
            var actual = spel.MoveIsPossible(0, 7);
            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandRechtsOnder_ReturnTrue()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 2] = Color.Black;
            spel.Board[5, 5] = Color.White;
            spel.Board[6, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 1 0 0
            // 6   0 0 0 0 0 0 1 0
            // 7   0 0 0 0 0 0 0 2 <
            // Act
            spel.PlayerTurn = Color.Black;
            var actual = spel.MoveIsPossible(7, 7);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandRechtsOnder_ReturnFalse()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 2] = Color.Black;
            spel.Board[5, 5] = Color.White;
            spel.Board[6, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  <
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 1 0 0
            // 6   0 0 0 0 0 0 1 0
            // 7   0 0 0 0 0 0 0 1
            // Act
            spel.PlayerTurn = Color.White;
            var actual = spel.MoveIsPossible(7, 7);
            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandLinksBoven_ReturnTrue()
        {
            // Arrange
            var spel = new Game();
            spel.Board[1, 1] = Color.White;
            spel.Board[2, 2] = Color.White;
            spel.Board[5, 5] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   2 0 0 0 0 0 0 0  <
            // 1   0 1 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 2 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0 
            // Act
            spel.PlayerTurn = Color.Black;
            var actual = spel.MoveIsPossible(0, 0);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandLinksBoven_ReturnFalse()
        {
            // Arrange
            var spel = new Game();
            spel.Board[1, 1] = Color.White;
            spel.Board[2, 2] = Color.White;
            spel.Board[5, 5] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 0 0 0 0 0 0 0  <
            // 1   0 1 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 2 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0          
            // Act
            spel.PlayerTurn = Color.White;
            var actual = spel.MoveIsPossible(0, 0);
            // Assert
            Assert.IsFalse(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandLinksOnder_ReturnTrue()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 5] = Color.White;
            spel.Board[5, 2] = Color.Black;
            spel.Board[6, 1] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 1 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 2 0 0 0 0 0
            // 6   0 2 0 0 0 0 0 0
            // 7   1 0 0 0 0 0 0 0 <
            // Act
            spel.PlayerTurn = Color.White;
            var actual = spel.MoveIsPossible(7, 0);
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void ZetMogelijk_ZetAanDeRandLinksOnder_ReturnFalse()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 5] = Color.White;
            spel.Board[5, 2] = Color.Black;
            spel.Board[6, 1] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  <
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 1 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 2 0 0 0 0 0
            // 6   0 2 0 0 0 0 0 0
            // 7   2 0 0 0 0 0 0 0
            // Act
            spel.PlayerTurn = Color.Black;
            var actual = spel.MoveIsPossible(7, 0);
            // Assert
            Assert.IsFalse(actual);
        }

        //---------------------------------------------------------------------------
        [Test]
        //[ExpectedException(typeof(Exception))]
        public void DoeZet_BuitenBord_Exception()
        {
            // Arrange
            var spel = new Game();
            //     0 1 2 3 4 5 6 7
            //                     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     1 <
            // Act
            spel.PlayerTurn = Color.White;
            //spel.DoeZet(8, 8);
            var ex = Assert.Throws<Exception>(delegate { spel.MakeMove(8, 8); });
            Assert.That(ex.Message, Is.EqualTo("Zet (8,8) ligt buiten het bord!"));

            // Assert
            Assert.AreEqual(Color.White, spel.Board[3, 3]);
            Assert.AreEqual(Color.White, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[3, 4]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);

            Assert.AreEqual(Color.White, spel.PlayerTurn);
        }

        [Test]
        public void DoeZet_StartSituatieZet23Zwart_ZetCorrectUitgevoerd()
        {
            // Arrange
            var spel = new Game();
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 2 0 0 0 0  <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.Black;
            spel.MakeMove(2, 3);
            // Assert
            Assert.AreEqual(Color.Black, spel.Board[2, 3]);
            Assert.AreEqual(Color.Black, spel.Board[3, 3]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);

            Assert.AreEqual(Color.White, spel.PlayerTurn);
        }

        [Test]
        public void DoeZet_StartSituatieZet23Wit_Exception()
        {
            // Arrange
            var spel = new Game();
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 1 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.White;
            var ex = Assert.Throws<Exception>(delegate { spel.MakeMove(2, 3); });
            Assert.That(ex.Message, Is.EqualTo("Zet (2,3) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, spel.Board[3, 3]);
            Assert.AreEqual(Color.White, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[3, 4]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);

            Assert.AreEqual(Color.None, spel.Board[2, 3]);

            Assert.AreEqual(Color.White, spel.PlayerTurn);
        }


        [Test]
        public void DoeZet_ZetAanDeRandBoven_ZetCorrectUitgevoerd()
        {
            // Arrange
            var spel = new Game();
            spel.Board[1, 3] = Color.White;
            spel.Board[2, 3] = Color.White;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            spel.PlayerTurn = Color.Black;
            spel.MakeMove(0, 3);
            // Assert
            Assert.AreEqual(Color.Black, spel.Board[0, 3]);
            Assert.AreEqual(Color.Black, spel.Board[1, 3]);
            Assert.AreEqual(Color.Black, spel.Board[2, 3]);
            Assert.AreEqual(Color.Black, spel.Board[3, 3]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);

            Assert.AreEqual(Color.White, spel.PlayerTurn);
        }

        [Test]
        public void DoeZet_ZetAanDeRandBoven_Exception()
        {
            // Arrange
            var spel = new Game();
            spel.Board[1, 3] = Color.White;
            spel.Board[2, 3] = Color.White;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 1 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.White;
            var ex = Assert.Throws<Exception>(delegate { spel.MakeMove(0, 3); });
            Assert.That(ex.Message, Is.EqualTo("Zet (0,3) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, spel.Board[3, 3]);
            Assert.AreEqual(Color.White, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[3, 4]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);

            Assert.AreEqual(Color.White, spel.Board[1, 3]);
            Assert.AreEqual(Color.White, spel.Board[2, 3]);

            Assert.AreEqual(Color.None, spel.Board[0, 3]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandBovenEnTotBenedenReedsGevuld_ZetCorrectUitgevoerd()
        {
            // Arrange
            var spel = new Game();
            spel.Board[1, 3] = Color.White;
            spel.Board[2, 3] = Color.White;
            spel.Board[3, 3] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[5, 3] = Color.White;
            spel.Board[6, 3] = Color.White;
            spel.Board[7, 3] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 1 1 0 0 0
            // 5   0 0 0 1 0 0 0 0
            // 6   0 0 0 1 0 0 0 0
            // 7   0 0 0 2 0 0 0 0

            // Act
            spel.PlayerTurn = Color.Black;
            spel.MakeMove(0, 3);
            // Assert
            Assert.AreEqual(Color.Black, spel.Board[0, 3]);
            Assert.AreEqual(Color.Black, spel.Board[1, 3]);
            Assert.AreEqual(Color.Black, spel.Board[2, 3]);
            Assert.AreEqual(Color.Black, spel.Board[3, 3]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);
            Assert.AreEqual(Color.Black, spel.Board[5, 3]);
            Assert.AreEqual(Color.Black, spel.Board[6, 3]);
            Assert.AreEqual(Color.Black, spel.Board[7, 3]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandBovenEnTotBenedenReedsGevuld_Exception()
        {
            // Arrange
            var spel = new Game();
            spel.Board[1, 3] = Color.White;
            spel.Board[2, 3] = Color.White;
            spel.Board[3, 3] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[5, 3] = Color.White;
            spel.Board[6, 3] = Color.White;
            spel.Board[7, 3] = Color.White;
            //     0 1 2 3 4 5 6 7
            //           v
            // 0   0 0 0 2 0 0 0 0  <
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 1 1 0 0 0
            // 5   0 0 0 1 0 0 0 0
            // 6   0 0 0 1 0 0 0 0
            // 7   0 0 0 1 0 0 0 0

            // Act
            spel.PlayerTurn = Color.Black;
            var ex = Assert.Throws<Exception>(delegate { spel.MakeMove(0, 3); });
            Assert.That(ex.Message, Is.EqualTo("Zet (0,3) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, spel.Board[3, 3]);
            Assert.AreEqual(Color.White, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[3, 4]);
            Assert.AreEqual(Color.White, spel.Board[4, 3]);

            Assert.AreEqual(Color.White, spel.Board[1, 3]);
            Assert.AreEqual(Color.White, spel.Board[2, 3]);
            Assert.AreEqual(Color.None, spel.Board[0, 3]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechts_ZetCorrectUitgevoerd()
        {
            // Arrange
            var spel = new Game();
            spel.Board[4, 5] = Color.White;
            spel.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 1 1 2 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            spel.PlayerTurn = Color.Black;
            spel.MakeMove(4, 7);
            // Assert
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);
            Assert.AreEqual(Color.Black, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[4, 5]);
            Assert.AreEqual(Color.Black, spel.Board[4, 6]);
            Assert.AreEqual(Color.Black, spel.Board[4, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechts_Exception()
        {
            // Arrange
            var spel = new Game();
            spel.Board[4, 5] = Color.White;
            spel.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 1 0 0 0 0  
            // 1   0 0 0 1 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 1 1 1 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.White;
            //spel.DoeZet(4, 7);
            var ex = Assert.Throws<Exception>(delegate { spel.MakeMove(4, 7); });
            Assert.That(ex.Message, Is.EqualTo("Zet (4,7) is niet mogelijk!"));


            // Assert
            Assert.AreEqual(Color.White, spel.Board[3, 3]);
            Assert.AreEqual(Color.White, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[3, 4]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);

            Assert.AreEqual(Color.White, spel.Board[4, 5]);
            Assert.AreEqual(Color.White, spel.Board[4, 6]);
            Assert.AreEqual(Color.None, spel.Board[4, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechtsEnTotLinksReedsGevuld_ZetCorrectUitgevoerd()
        {
            // Arrange
            var spel = new Game();
            spel.Board[4, 0] = Color.Black;
            spel.Board[4, 1] = Color.White;
            spel.Board[4, 2] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[4, 4] = Color.White;
            spel.Board[4, 5] = Color.White;
            spel.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0 
            // 4   2 1 1 1 1 1 1 2 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.Black;
            spel.MakeMove(4, 7);
            // Assert
            Assert.AreEqual(Color.Black, spel.Board[4, 0]);
            Assert.AreEqual(Color.Black, spel.Board[4, 1]);
            Assert.AreEqual(Color.Black, spel.Board[4, 2]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);
            Assert.AreEqual(Color.Black, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[4, 5]);
            Assert.AreEqual(Color.Black, spel.Board[4, 6]);
            Assert.AreEqual(Color.Black, spel.Board[4, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechtsEnTotLinksReedsGevuld_Exception()
        {
            // Arrange
            var spel = new Game();
            spel.Board[4, 0] = Color.Black;
            spel.Board[4, 1] = Color.White;
            spel.Board[4, 2] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[4, 4] = Color.White;
            spel.Board[4, 5] = Color.White;
            spel.Board[4, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  

            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   2 1 1 1 1 1 1 1 <
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.White;

            var ex = Assert.Throws<Exception>(delegate { spel.MakeMove(4, 7); });
            Assert.That(ex.Message, Is.EqualTo("Zet (4,7) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, spel.Board[3, 3]);
            Assert.AreEqual(Color.White, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[3, 4]);
            Assert.AreEqual(Color.White, spel.Board[4, 3]);

            Assert.AreEqual(Color.Black, spel.Board[4, 0]);
            Assert.AreEqual(Color.White, spel.Board[4, 1]);
            Assert.AreEqual(Color.White, spel.Board[4, 2]);

            Assert.AreEqual(Color.White, spel.Board[4, 5]);
            Assert.AreEqual(Color.White, spel.Board[4, 6]);
            Assert.AreEqual(Color.None, spel.Board[4, 7]);
        }


        //     0 1 2 3 4 5 6 7
        //                     
        // 0   0 0 0 0 0 0 0 0  
        // 1   0 0 0 0 0 0 0 0
        // 2   0 0 0 0 0 0 0 0
        // 3   0 0 0 1 2 0 0 0
        // 4   0 0 0 2 1 0 0 0
        // 5   0 0 0 0 0 0 0 0
        // 6   0 0 0 0 0 0 0 0
        // 7   0 0 0 0 0 0 0 0


        [Test]
        public void DoeZet_StartSituatieZet22Wit_Exception()
        {
            // Arrange
            var spel = new Game();
            //     0 1 2 3 4 5 6 7
            //         v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.White;
            var ex = Assert.Throws<Exception>(delegate { spel.MakeMove(2, 2); });
            Assert.That(ex.Message, Is.EqualTo("Zet (2,2) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, spel.Board[3, 3]);
            Assert.AreEqual(Color.White, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[3, 4]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);

            Assert.AreEqual(Color.None, spel.Board[2, 2]);
        }

        [Test]
        public void DoeZet_StartSituatieZet22Zwart_Exception()
        {
            // Arrange
            var spel = new Game();
            //     0 1 2 3 4 5 6 7
            //         v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0 <
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0

            // Act
            spel.PlayerTurn = Color.Black;
            var ex = Assert.Throws<Exception>(delegate { spel.MakeMove(2, 2); });
            Assert.That(ex.Message, Is.EqualTo("Zet (2,2) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, spel.Board[3, 3]);
            Assert.AreEqual(Color.White, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[3, 4]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);

            Assert.AreEqual(Color.None, spel.Board[2, 2]);
        }


        [Test]
        public void DoeZet_ZetAanDeRandRechtsBoven_ZetCorrectUitgevoerd()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 5] = Color.Black;
            spel.Board[1, 6] = Color.Black;
            spel.Board[5, 2] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 1  <
            // 1   0 0 0 0 0 0 2 0
            // 2   0 0 0 0 0 2 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 1 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            spel.PlayerTurn = Color.White;
            spel.MakeMove(0, 7);
            // Assert
            Assert.AreEqual(Color.White, spel.Board[5, 2]);
            Assert.AreEqual(Color.White, spel.Board[4, 3]);
            Assert.AreEqual(Color.White, spel.Board[3, 4]);
            Assert.AreEqual(Color.White, spel.Board[2, 5]);
            Assert.AreEqual(Color.White, spel.Board[1, 6]);
            Assert.AreEqual(Color.White, spel.Board[0, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechtsBoven_Exception()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 5] = Color.Black;
            spel.Board[1, 6] = Color.Black;
            spel.Board[5, 2] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 2  <
            // 1   0 0 0 0 0 0 2 0
            // 2   0 0 0 0 0 2 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 1 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            // Act
            spel.PlayerTurn = Color.Black;
            var ex = Assert.Throws<Exception>(delegate { spel.MakeMove(0, 7); });
            Assert.That(ex.Message, Is.EqualTo("Zet (0,7) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, spel.Board[3, 3]);
            Assert.AreEqual(Color.White, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[3, 4]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);

            Assert.AreEqual(Color.Black, spel.Board[1, 6]);
            Assert.AreEqual(Color.Black, spel.Board[2, 5]);

            Assert.AreEqual(Color.White, spel.Board[5, 2]);

            Assert.AreEqual(Color.None, spel.Board[0, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechtsOnder_ZetCorrectUitgevoerd()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 2] = Color.Black;
            spel.Board[5, 5] = Color.White;
            spel.Board[6, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 1 0 0
            // 6   0 0 0 0 0 0 1 0
            // 7   0 0 0 0 0 0 0 2 <
            // Act
            spel.PlayerTurn = Color.Black;
            spel.MakeMove(7, 7);
            // Assert
            Assert.AreEqual(Color.Black, spel.Board[2, 2]);
            Assert.AreEqual(Color.Black, spel.Board[3, 3]);
            Assert.AreEqual(Color.Black, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[5, 5]);
            Assert.AreEqual(Color.Black, spel.Board[6, 6]);
            Assert.AreEqual(Color.Black, spel.Board[7, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandRechtsOnder_Exception()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 2] = Color.Black;
            spel.Board[5, 5] = Color.White;
            spel.Board[6, 6] = Color.White;
            //     0 1 2 3 4 5 6 7
            //                   v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 2 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 1 0 0
            // 6   0 0 0 0 0 0 1 0
            // 7   0 0 0 0 0 0 0 1 <
            // Act
            spel.PlayerTurn = Color.White;
            var ex = Assert.Throws<Exception>(delegate { spel.MakeMove(7, 7); });
            Assert.That(ex.Message, Is.EqualTo("Zet (7,7) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, spel.Board[3, 3]);
            Assert.AreEqual(Color.White, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[3, 4]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);

            Assert.AreEqual(Color.Black, spel.Board[2, 2]);
            Assert.AreEqual(Color.White, spel.Board[5, 5]);
            Assert.AreEqual(Color.White, spel.Board[6, 6]);

            Assert.AreEqual(Color.None, spel.Board[7, 7]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandLinksBoven_ZetCorrectUitgevoerd()
        {
            // Arrange
            var spel = new Game();
            spel.Board[1, 1] = Color.White;
            spel.Board[2, 2] = Color.White;
            spel.Board[5, 5] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   2 0 0 0 0 0 0 0  <
            // 1   0 1 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 2 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0 
            // Act
            spel.PlayerTurn = Color.Black;
            spel.MakeMove(0, 0);
            // Assert
            Assert.AreEqual(Color.Black, spel.Board[0, 0]);
            Assert.AreEqual(Color.Black, spel.Board[1, 1]);
            Assert.AreEqual(Color.Black, spel.Board[2, 2]);
            Assert.AreEqual(Color.Black, spel.Board[3, 3]);
            Assert.AreEqual(Color.Black, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[5, 5]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandLinksBoven_Exception()
        {
            // Arrange
            var spel = new Game();
            spel.Board[1, 1] = Color.White;
            spel.Board[2, 2] = Color.White;
            spel.Board[5, 5] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 0 0 0 0 0 0 0  <
            // 1   0 1 0 0 0 0 0 0
            // 2   0 0 1 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 2 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0          
            // Act
            spel.PlayerTurn = Color.White;
            //spel.DoeZet(0, 0);
            var ex = Assert.Throws<Exception>(delegate { spel.MakeMove(0, 0); });
            Assert.That(ex.Message, Is.EqualTo("Zet (0,0) is niet mogelijk!"));


            // Assert
            Assert.AreEqual(Color.White, spel.Board[3, 3]);
            Assert.AreEqual(Color.White, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[3, 4]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);

            Assert.AreEqual(Color.White, spel.Board[1, 1]);
            Assert.AreEqual(Color.White, spel.Board[2, 2]);

            Assert.AreEqual(Color.Black, spel.Board[5, 5]);

            Assert.AreEqual(Color.None, spel.Board[0, 0]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandLinksOnder_ZetCorrectUitgevoerd()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 5] = Color.White;
            spel.Board[5, 2] = Color.Black;
            spel.Board[6, 1] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 1 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 2 0 0 0 0 0
            // 6   0 2 0 0 0 0 0 0
            // 7   1 0 0 0 0 0 0 0 <
            // Act
            spel.PlayerTurn = Color.White;
            spel.MakeMove(7, 0);
            // Assert
            Assert.AreEqual(Color.White, spel.Board[7, 0]);
            Assert.AreEqual(Color.White, spel.Board[6, 1]);
            Assert.AreEqual(Color.White, spel.Board[5, 2]);
            Assert.AreEqual(Color.White, spel.Board[4, 3]);
            Assert.AreEqual(Color.White, spel.Board[3, 4]);
            Assert.AreEqual(Color.White, spel.Board[2, 5]);
        }

        [Test]
        public void DoeZet_ZetAanDeRandLinksOnder_Exception()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 5] = Color.White;
            spel.Board[5, 2] = Color.Black;
            spel.Board[6, 1] = Color.Black;
            //     0 1 2 3 4 5 6 7
            //     v
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 1 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 2 0 0 0 0 0
            // 6   0 2 0 0 0 0 0 0
            // 7   2 0 0 0 0 0 0 0 <
            // Act
            spel.PlayerTurn = Color.Black;
            var ex = Assert.Throws<Exception>(delegate { spel.MakeMove(7, 0); });
            Assert.That(ex.Message, Is.EqualTo("Zet (7,0) is niet mogelijk!"));

            // Assert
            Assert.AreEqual(Color.White, spel.Board[3, 3]);
            Assert.AreEqual(Color.White, spel.Board[4, 4]);
            Assert.AreEqual(Color.Black, spel.Board[3, 4]);
            Assert.AreEqual(Color.Black, spel.Board[4, 3]);

            Assert.AreEqual(Color.White, spel.Board[2, 5]);
            Assert.AreEqual(Color.Black, spel.Board[5, 2]);
            Assert.AreEqual(Color.Black, spel.Board[6, 1]);

            Assert.AreEqual(Color.None, spel.Board[7, 7]);

            Assert.AreEqual(Color.None, spel.Board[7, 0]);
        }

        [Test]
        public void Pas_ZwartAanZetGeenZetMogelijk_ReturnTrueEnWisselBeurt()
        {
            // Arrange  (zowel wit als zwart kunnen niet meer)
            var spel = new Game();
            spel.Board[0, 0] = Color.White;
            spel.Board[0, 1] = Color.White;
            spel.Board[0, 2] = Color.White;
            spel.Board[0, 3] = Color.White;
            spel.Board[0, 4] = Color.White;
            spel.Board[0, 5] = Color.White;
            spel.Board[0, 6] = Color.White;
            spel.Board[0, 7] = Color.White;
            spel.Board[1, 0] = Color.White;
            spel.Board[1, 1] = Color.White;
            spel.Board[1, 2] = Color.White;
            spel.Board[1, 3] = Color.White;
            spel.Board[1, 4] = Color.White;
            spel.Board[1, 5] = Color.White;
            spel.Board[1, 6] = Color.White;
            spel.Board[1, 7] = Color.White;
            spel.Board[2, 0] = Color.White;
            spel.Board[2, 1] = Color.White;
            spel.Board[2, 2] = Color.White;
            spel.Board[2, 3] = Color.White;
            spel.Board[2, 4] = Color.White;
            spel.Board[2, 5] = Color.White;
            spel.Board[2, 6] = Color.White;
            spel.Board[2, 7] = Color.White;
            spel.Board[3, 0] = Color.White;
            spel.Board[3, 1] = Color.White;
            spel.Board[3, 2] = Color.White;
            spel.Board[3, 3] = Color.White;
            spel.Board[3, 4] = Color.White;
            spel.Board[3, 5] = Color.White;
            spel.Board[3, 6] = Color.White;
            spel.Board[3, 7] = Color.None;
            spel.Board[4, 0] = Color.White;
            spel.Board[4, 1] = Color.White;
            spel.Board[4, 2] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[4, 4] = Color.White;
            spel.Board[4, 5] = Color.White;
            spel.Board[4, 6] = Color.None;
            spel.Board[4, 7] = Color.None;
            spel.Board[5, 0] = Color.White;
            spel.Board[5, 1] = Color.White;
            spel.Board[5, 2] = Color.White;
            spel.Board[5, 3] = Color.White;
            spel.Board[5, 4] = Color.White;
            spel.Board[5, 5] = Color.White;
            spel.Board[5, 6] = Color.None;
            spel.Board[5, 7] = Color.Black;
            spel.Board[6, 0] = Color.White;
            spel.Board[6, 1] = Color.White;
            spel.Board[6, 2] = Color.White;
            spel.Board[6, 3] = Color.White;
            spel.Board[6, 4] = Color.White;
            spel.Board[6, 5] = Color.White;
            spel.Board[6, 6] = Color.White;
            spel.Board[6, 7] = Color.None;
            spel.Board[7, 0] = Color.White;
            spel.Board[7, 1] = Color.White;
            spel.Board[7, 2] = Color.White;
            spel.Board[7, 3] = Color.White;
            spel.Board[7, 4] = Color.White;
            spel.Board[7, 5] = Color.White;
            spel.Board[7, 6] = Color.White;
            spel.Board[7, 7] = Color.White;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 1 1 1 1 1 1 1  
            // 1   1 1 1 1 1 1 1 1
            // 2   1 1 1 1 1 1 1 1
            // 3   1 1 1 1 1 1 1 0
            // 4   1 1 1 1 1 1 0 0
            // 5   1 1 1 1 1 1 0 2
            // 6   1 1 1 1 1 1 1 0
            // 7   1 1 1 1 1 1 1 1
            // Act
            spel.PlayerTurn = Color.Black;
            spel.Skip();
            // Assert
            Assert.AreEqual(Color.White, spel.PlayerTurn);
        }

        [Test]
        public void Pas_WitAanZetGeenZetMogelijk_ReturnTrueEnWisselBeurt()
        {
            // Arrange  (zowel wit als zwart kunnen niet meer)
            var spel = new Game();
            spel.Board[0, 0] = Color.White;
            spel.Board[0, 1] = Color.White;
            spel.Board[0, 2] = Color.White;
            spel.Board[0, 3] = Color.White;
            spel.Board[0, 4] = Color.White;
            spel.Board[0, 5] = Color.White;
            spel.Board[0, 6] = Color.White;
            spel.Board[0, 7] = Color.White;
            spel.Board[1, 0] = Color.White;
            spel.Board[1, 1] = Color.White;
            spel.Board[1, 2] = Color.White;
            spel.Board[1, 3] = Color.White;
            spel.Board[1, 4] = Color.White;
            spel.Board[1, 5] = Color.White;
            spel.Board[1, 6] = Color.White;
            spel.Board[1, 7] = Color.White;
            spel.Board[2, 0] = Color.White;
            spel.Board[2, 1] = Color.White;
            spel.Board[2, 2] = Color.White;
            spel.Board[2, 3] = Color.White;
            spel.Board[2, 4] = Color.White;
            spel.Board[2, 5] = Color.White;
            spel.Board[2, 6] = Color.White;
            spel.Board[2, 7] = Color.White;
            spel.Board[3, 0] = Color.White;
            spel.Board[3, 1] = Color.White;
            spel.Board[3, 2] = Color.White;
            spel.Board[3, 3] = Color.White;
            spel.Board[3, 4] = Color.White;
            spel.Board[3, 5] = Color.White;
            spel.Board[3, 6] = Color.White;
            spel.Board[3, 7] = Color.None;
            spel.Board[4, 0] = Color.White;
            spel.Board[4, 1] = Color.White;
            spel.Board[4, 2] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[4, 4] = Color.White;
            spel.Board[4, 5] = Color.White;
            spel.Board[4, 6] = Color.None;
            spel.Board[4, 7] = Color.None;
            spel.Board[5, 0] = Color.White;
            spel.Board[5, 1] = Color.White;
            spel.Board[5, 2] = Color.White;
            spel.Board[5, 3] = Color.White;
            spel.Board[5, 4] = Color.White;
            spel.Board[5, 5] = Color.White;
            spel.Board[5, 6] = Color.None;
            spel.Board[5, 7] = Color.Black;
            spel.Board[6, 0] = Color.White;
            spel.Board[6, 1] = Color.White;
            spel.Board[6, 2] = Color.White;
            spel.Board[6, 3] = Color.White;
            spel.Board[6, 4] = Color.White;
            spel.Board[6, 5] = Color.White;
            spel.Board[6, 6] = Color.White;
            spel.Board[6, 7] = Color.None;
            spel.Board[7, 0] = Color.White;
            spel.Board[7, 1] = Color.White;
            spel.Board[7, 2] = Color.White;
            spel.Board[7, 3] = Color.White;
            spel.Board[7, 4] = Color.White;
            spel.Board[7, 5] = Color.White;
            spel.Board[7, 6] = Color.White;
            spel.Board[7, 7] = Color.White;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 1 1 1 1 1 1 1  
            // 1   1 1 1 1 1 1 1 1
            // 2   1 1 1 1 1 1 1 1
            // 3   1 1 1 1 1 1 1 0
            // 4   1 1 1 1 1 1 0 0
            // 5   1 1 1 1 1 1 0 2
            // 6   1 1 1 1 1 1 1 0
            // 7   1 1 1 1 1 1 1 1
            // Act
            spel.PlayerTurn = Color.White;
            spel.Skip();
            // Assert
            Assert.AreEqual(Color.Black, spel.PlayerTurn);
        }

        [Test]
        public void Afgelopen_GeenZetMogelijk_ReturnTrue()
        {
            // Arrange  (zowel wit als zwart kunnen niet meer)
            var spel = new Game();
            spel.Board[0, 0] = Color.White;
            spel.Board[0, 1] = Color.White;
            spel.Board[0, 2] = Color.White;
            spel.Board[0, 3] = Color.White;
            spel.Board[0, 4] = Color.White;
            spel.Board[0, 5] = Color.White;
            spel.Board[0, 6] = Color.White;
            spel.Board[0, 7] = Color.White;
            spel.Board[1, 0] = Color.White;
            spel.Board[1, 1] = Color.White;
            spel.Board[1, 2] = Color.White;
            spel.Board[1, 3] = Color.White;
            spel.Board[1, 4] = Color.White;
            spel.Board[1, 5] = Color.White;
            spel.Board[1, 6] = Color.White;
            spel.Board[1, 7] = Color.White;
            spel.Board[2, 0] = Color.White;
            spel.Board[2, 1] = Color.White;
            spel.Board[2, 2] = Color.White;
            spel.Board[2, 3] = Color.White;
            spel.Board[2, 4] = Color.White;
            spel.Board[2, 5] = Color.White;
            spel.Board[2, 6] = Color.White;
            spel.Board[2, 7] = Color.White;
            spel.Board[3, 0] = Color.White;
            spel.Board[3, 1] = Color.White;
            spel.Board[3, 2] = Color.White;
            spel.Board[3, 3] = Color.White;
            spel.Board[3, 4] = Color.White;
            spel.Board[3, 5] = Color.White;
            spel.Board[3, 6] = Color.White;
            spel.Board[3, 7] = Color.None;
            spel.Board[4, 0] = Color.White;
            spel.Board[4, 1] = Color.White;
            spel.Board[4, 2] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[4, 4] = Color.White;
            spel.Board[4, 5] = Color.White;
            spel.Board[4, 6] = Color.None;
            spel.Board[4, 7] = Color.None;
            spel.Board[5, 0] = Color.White;
            spel.Board[5, 1] = Color.White;
            spel.Board[5, 2] = Color.White;
            spel.Board[5, 3] = Color.White;
            spel.Board[5, 4] = Color.White;
            spel.Board[5, 5] = Color.White;
            spel.Board[5, 6] = Color.None;
            spel.Board[5, 7] = Color.Black;
            spel.Board[6, 0] = Color.White;
            spel.Board[6, 1] = Color.White;
            spel.Board[6, 2] = Color.White;
            spel.Board[6, 3] = Color.White;
            spel.Board[6, 4] = Color.White;
            spel.Board[6, 5] = Color.White;
            spel.Board[6, 6] = Color.White;
            spel.Board[6, 7] = Color.None;
            spel.Board[7, 0] = Color.White;
            spel.Board[7, 1] = Color.White;
            spel.Board[7, 2] = Color.White;
            spel.Board[7, 3] = Color.White;
            spel.Board[7, 4] = Color.White;
            spel.Board[7, 5] = Color.White;
            spel.Board[7, 6] = Color.White;
            spel.Board[7, 7] = Color.White;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 1 1 1 1 1 1 1  
            // 1   1 1 1 1 1 1 1 1
            // 2   1 1 1 1 1 1 1 1
            // 3   1 1 1 1 1 1 1 0
            // 4   1 1 1 1 1 1 0 0
            // 5   1 1 1 1 1 1 0 2
            // 6   1 1 1 1 1 1 1 0
            // 7   1 1 1 1 1 1 1 1
            // Act
            spel.PlayerTurn = Color.White;
            var actual = spel.Finished();
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void Afgelopen_GeenZetMogelijkAllesBezet_ReturnTrue()
        {
            // Arrange  (zowel wit als zwart kunnen niet meer)
            var spel = new Game();
            spel.Board[0, 0] = Color.White;
            spel.Board[0, 1] = Color.White;
            spel.Board[0, 2] = Color.White;
            spel.Board[0, 3] = Color.White;
            spel.Board[0, 4] = Color.White;
            spel.Board[0, 5] = Color.White;
            spel.Board[0, 6] = Color.White;
            spel.Board[0, 7] = Color.White;
            spel.Board[1, 0] = Color.White;
            spel.Board[1, 1] = Color.White;
            spel.Board[1, 2] = Color.White;
            spel.Board[1, 3] = Color.White;
            spel.Board[1, 4] = Color.White;
            spel.Board[1, 5] = Color.White;
            spel.Board[1, 6] = Color.White;
            spel.Board[1, 7] = Color.White;
            spel.Board[2, 0] = Color.White;
            spel.Board[2, 1] = Color.White;
            spel.Board[2, 2] = Color.White;
            spel.Board[2, 3] = Color.White;
            spel.Board[2, 4] = Color.White;
            spel.Board[2, 5] = Color.White;
            spel.Board[2, 6] = Color.White;
            spel.Board[2, 7] = Color.White;
            spel.Board[3, 0] = Color.White;
            spel.Board[3, 1] = Color.White;
            spel.Board[3, 2] = Color.White;
            spel.Board[3, 3] = Color.White;
            spel.Board[3, 4] = Color.White;
            spel.Board[3, 5] = Color.White;
            spel.Board[3, 6] = Color.White;
            spel.Board[3, 7] = Color.White;
            spel.Board[4, 0] = Color.White;
            spel.Board[4, 1] = Color.White;
            spel.Board[4, 2] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[4, 4] = Color.White;
            spel.Board[4, 5] = Color.White;
            spel.Board[4, 6] = Color.Black;
            spel.Board[4, 7] = Color.Black;
            spel.Board[5, 0] = Color.White;
            spel.Board[5, 1] = Color.White;
            spel.Board[5, 2] = Color.White;
            spel.Board[5, 3] = Color.White;
            spel.Board[5, 4] = Color.White;
            spel.Board[5, 5] = Color.White;
            spel.Board[5, 6] = Color.Black;
            spel.Board[5, 7] = Color.Black;
            spel.Board[6, 0] = Color.White;
            spel.Board[6, 1] = Color.White;
            spel.Board[6, 2] = Color.White;
            spel.Board[6, 3] = Color.White;
            spel.Board[6, 4] = Color.White;
            spel.Board[6, 5] = Color.White;
            spel.Board[6, 6] = Color.White;
            spel.Board[6, 7] = Color.Black;
            spel.Board[7, 0] = Color.White;
            spel.Board[7, 1] = Color.White;
            spel.Board[7, 2] = Color.White;
            spel.Board[7, 3] = Color.White;
            spel.Board[7, 4] = Color.White;
            spel.Board[7, 5] = Color.White;
            spel.Board[7, 6] = Color.White;
            spel.Board[7, 7] = Color.White;

            //     0 1 2 3 4 5 6 7
            //     v
            // 0   1 1 1 1 1 1 1 1  
            // 1   1 1 1 1 1 1 1 1
            // 2   1 1 1 1 1 1 1 1
            // 3   1 1 1 1 1 1 1 2
            // 4   1 1 1 1 1 1 2 2
            // 5   1 1 1 1 1 1 2 2
            // 6   1 1 1 1 1 1 1 2
            // 7   1 1 1 1 1 1 1 1
            // Act
            spel.PlayerTurn = Color.White;
            var actual = spel.Finished();
            // Assert
            Assert.IsTrue(actual);
        }

        [Test]
        public void Afgelopen_WelZetMogelijk_ReturnFalse()
        {
            // Arrange
            var spel = new Game();
            //     0 1 2 3 4 5 6 7
            //                     
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     
            // Act
            spel.PlayerTurn = Color.White;
            var actual = spel.Finished();
            // Assert
            Assert.IsFalse(actual);
        }


        [Test]
        public void OverwegendeKleur_Gelijk_ReturnKleurGeen()
        {
            // Arrange
            var spel = new Game();
            //     0 1 2 3 4 5 6 7
            //                     
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 0 0 0 0 0
            // 3   0 0 0 1 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     
            // Act
            var actual = spel.WinningColor();
            // Assert
            Assert.AreEqual(Color.None, actual);
        }

        [Test]
        public void OverwegendeKleur_Zwart_ReturnKleurZwart()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 3] = Color.Black;
            spel.Board[3, 3] = Color.Black;
            spel.Board[4, 3] = Color.Black;
            spel.Board[3, 4] = Color.Black;
            spel.Board[4, 4] = Color.White;

            //     0 1 2 3 4 5 6 7
            //                     
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 2 0 0 0 0
            // 3   0 0 0 2 2 0 0 0
            // 4   0 0 0 2 1 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     
            // Act
            var actual = spel.WinningColor();
            // Assert
            Assert.AreEqual(Color.Black, actual);
        }

        [Test]
        public void OverwegendeKleur_Wit_ReturnKleurWit()
        {
            // Arrange
            var spel = new Game();
            spel.Board[2, 3] = Color.White;
            spel.Board[3, 3] = Color.White;
            spel.Board[4, 3] = Color.White;
            spel.Board[3, 4] = Color.White;
            spel.Board[4, 4] = Color.Black;


            //     0 1 2 3 4 5 6 7
            //                     
            // 0   0 0 0 0 0 0 0 0  
            // 1   0 0 0 0 0 0 0 0
            // 2   0 0 0 1 0 0 0 0
            // 3   0 0 0 1 1 0 0 0
            // 4   0 0 0 1 2 0 0 0
            // 5   0 0 0 0 0 0 0 0
            // 6   0 0 0 0 0 0 0 0
            // 7   0 0 0 0 0 0 0 0
            //                     
            // Act
            var actual = spel.WinningColor();
            // Assert
            Assert.AreEqual(Color.White, actual);
        }
    }
}