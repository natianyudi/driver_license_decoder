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
    public class DriverLicenseNYTest
    {
        static String barCode = "";
	    static String barCodeWithNo = "";
	    static DriverLicense license = null;

	   [TestInitialize]
	    public void setUpBeforeClass() 
       {
           String file_path = "C:\\Users\\Twins\\documents\\visual studio 2013\\Projects\\DriverLicenseDecoder\\DriverLicenseDecoder\\res\\txt\\ny";

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
		    Assert.AreEqual("NY", license.getState());
	    }

	    [TestMethod]
	    public void testHeight() {
		    Assert.AreEqual(511, (int) license.getHeight());
	    }

	    [TestMethod]
	    public void testSex() {
		    Assert.AreEqual("M", license.getSex());
	    }

	    [TestMethod]
	    public void testIssuedDate() {
            if(license.getLicenseIssuedDate().HasValue)
            {
                DateTime dt = (DateTime) license.getLicenseIssuedDate();
		        Assert.AreEqual( 2011, dt.Year, "IssuedDate should have year 2011");
		        Assert.AreEqual(3, dt.Month,"IssuedDate should have month 3");
                Assert.AreEqual(30, dt.Day, "IssuedDate should have date 30");
            }
	    }

	    [TestMethod]
	    public void testExpDate() {
            DateTime dt = (DateTime)license.getLicenseExpirationDate();
		    Assert.AreEqual(2019, dt.Year);
		    Assert.AreEqual(3, dt.Month);
		    Assert.AreEqual(30,dt.Day );
	    }

	    [TestMethod]
	    public void testDOB() {
             DateTime dt = (DateTime)license.getDOB();
		    Assert.AreEqual(1961,dt.Year,"DOB should have year 1962");
		    Assert.AreEqual(1,dt.Month,"DOB should have month 10");
		    Assert.AreEqual( 20,dt.Day,"DOB should have date 26");
	    }
	
	    [TestMethod]
	    public void testHasExpired() {
		    Assert.AreEqual( false,license.hasExpired());
	    }

	    [TestMethod]
	    public void testDriverLicenseNumber() {
		    Assert.AreEqual( "283184287",
				    license.getDriverLicenseNumber());
	    }

	    [TestMethod]
	    public void testJSON() {
		    Console.Write("json=");
		    Console.Write(license.toJson());
	    }
    }
}
