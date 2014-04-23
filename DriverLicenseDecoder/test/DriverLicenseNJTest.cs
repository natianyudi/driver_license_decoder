using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace DriverLicenseDecoder.test
{
    [TestClass]
    public class DriverLicenseNJTest
    {
        static String barCode = "";
        static String barCodeWithNo = "";
        static DriverLicense license = null;

        [TestInitialize]
        public void setUpBeforeClass()
		{
            String file_path = "C:\\Users\\Twins\\documents\\visual studio 2013\\Projects\\DriverLicenseDecoder\\DriverLicenseDecoder\\res\\txt\\nj";

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
            if(license == null)
                license = new DriverLicense(barCode);
	    }

         [TestMethod]
	    public void testState() {
            Assert.AreEqual("NJ", license.getState(),"State should be NJ");
	    }


        [TestMethod]
	    public void testHeight() {
            Assert.AreEqual(67, (int)license.getHeight(), "Height should be 67");
	    }

	    [TestMethod]
	    public void testSex() {
            Assert.AreEqual("M", license.getSex(), "Sex should be M");
	    }

	    [TestMethod]
	    public void testIssuedDate() {
            if(license.getLicenseIssuedDate().HasValue)
            {
                DateTime dt = (DateTime) license.getLicenseIssuedDate();
		        Assert.AreEqual( 2012, dt.Year, "IssuedDate should have year 2010");
		        Assert.AreEqual(5, dt.Month,"IssuedDate should have month 7");
                Assert.AreEqual(24, dt.Day, "IssuedDate should have date 23");
            }
	    }

	    [TestMethod]
	    public void testExpDate() {
            DateTime dt = (DateTime)license.getLicenseExpirationDate();
		    Assert.AreEqual(2013, dt.Year,"EXP should have year 2013");
		    Assert.AreEqual(5, dt.Month,"EXP should have month 5");
		    Assert.AreEqual(1,dt.Day ,"EXP should have date 1");
	    }

	    [TestMethod]
	    public void testDOB() {
            DateTime dt = (DateTime)license.getDOB();
		    Assert.AreEqual(1962,dt.Year,"DOB should have year 1962");
		    Assert.AreEqual(10,dt.Month,"DOB should have month 10");
		    Assert.AreEqual( 26,dt.Day,"DOB should have date 26");
	    }

	    [TestMethod]
	    public void testHasExpired() {
		    Assert.AreEqual( true,license.hasExpired(),"License should have expired");
	    }

	    [TestMethod]
	    public void testDriverLicenseNumber() {
		    Assert.AreEqual("D46220533109611", license.getDriverLicenseNumber(), "LicenseNumber should be D46220533109611");
	    }

	    [TestMethod]
	    public void testJSON() {
		    Console.Write("json=");
            Console.Write(license.toJson());
	    }
    }
}
