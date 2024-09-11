using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace TestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestToBinary()
        {
            string res = "00000110";
            string retour = WindowsForms_Projet_Image.Image123.ToBinary(6);
            Assert.AreEqual(res, retour);

        }

        [TestMethod]

        public void TestToDec()
        {
            int res = 8;
            int bin = WindowsForms_Projet_Image.Image123.toDec("00001000");
            Assert.AreEqual(res, bin);
        }

        [TestMethod]

        public void TestLargeurBtoInt()
        {
            int res = 197637;
            byte[] param = new byte[21];
            int c = 5;
            for (int i = 0; i < 21; i++)
            {
                if (i < 18)
                {
                    param[i] = 0;
                }
                else
                {
                    param[i] = Convert.ToByte(c);
                    c--;
                }
            }
            Assert.AreEqual(res, WindowsForms_Projet_Image.Image123.LargeurBtoInt(param));
        }
    }
}