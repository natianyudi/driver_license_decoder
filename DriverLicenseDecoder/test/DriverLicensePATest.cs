using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLicenseDecoder.test
{
    [TestClass]
    public class DriverLicensePATest
    {
        static String barCode = "";
	    static String barCodeWithNo = "";
	    static DriverLicense license = null;

	    [TestInitialize]
	    public void setUpBeforeClass() {
		    String file_path = "C:\\Users\\Twins\\documents\\visual studio 2013\\Projects\\DriverLicenseDecoder\\DriverLicenseDecoder\\res\\txt\\pa";

            try
            {
                using (StreamReader sr = new StreamReader(file_path))
                {
                    barCode = sr.ReadToEnd();

                    barCode = barCode.Replace("\r", "").Replace("\\", "");

                }
            }
            catch (Exception e)
            {

            }
            license = new DriverLicense(barCode);
	    }

	    [TestMethod]
	    public void testState() {
		    Assert.AreEqual("PA", license.getState());
	    }

	    [TestMethod]
	    public void testHeight() {
		    Assert.AreEqual(506, (int) license.getHeight());
	    }

	    [TestMethod]
	    public void testSex() {
		    Assert.AreEqual("M", license.getSex());
	    }

	    [TestMethod]
	    public void testIssuedDate() {
            DateTime dt = (DateTime)license.getLicenseIssuedDate();
            Assert.AreEqual(2010, dt.Year);
		    Assert.AreEqual(12, dt.Month);
		    Assert.AreEqual(4, dt.Day);
	    }

	    [TestMethod]
	    public void testExpDate() {
            DateTime dt = (DateTime)license.getLicenseExpirationDate();
		    Assert.AreEqual(2015, dt.Year);
		    Assert.AreEqual( 1, dt.Month);
            Assert.AreEqual(18, dt.Day);
	    }

	    [TestMethod]
	    public void testDOB() {
            DateTime dt = (DateTime)license.getDOB();
            Assert.AreEqual(1960, dt.Year);
            Assert.AreEqual(2, dt.Month);
            Assert.AreEqual(17, dt.Day);
	    }
	
	    [TestMethod]
	    public void testHasExpired() {
		    Assert.AreEqual( false,license.hasExpired());
	    }

	    [TestMethod]
	    public void testDriverLicenseNumber() {
		    Assert.AreEqual("16117181",license.getDriverLicenseNumber());
	    }

	    [TestMethod]
	    public void testJSON() {
		    Console.Write("json=");
            Console.Write(license.toJson());
	    }
    }
}
