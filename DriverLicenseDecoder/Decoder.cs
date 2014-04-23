using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriverLicenseDecoder
{
    public static partial class MyExtensions
    {
        public static string MySubString(this string s, int start, int end)
        {
            int endI = end - start;
            if (endI == 0 || end < 0)
                return s.Substring(start);
            else
                return s.Substring(start, endI);
        }
    }

    class Decoder
    {
        

        static char PDF_LINEFEED = '\n';
        static char PDF_RECORDSEP = (char)30;
        static char PDF_SEGTERM = '\n';
        static String PDF_FILETYPE = "ANSI ";
        static Dictionary<String, String> fields = new Dictionary<String, String>
        {
            {"DAA", "Name"},
		    {"DLDAA", "Name"},
		    {"DAB", "LastName"},
		    {"DCS", "LastName"},
		    {"DAC", "FirstName"},
		    {"DCT", "FirstName"},
		    {"DAD", "MiddleName"},

		    {"DBC", "Sex"},
		    {"DAU", "Height"},
		    {"DAY", "EyeColor"},

		    {"DAG", "Address"},
		    {"DAI", "City"},
		    {"DAN", "City"},
		    {"DAJ", "State"},
		    {"DAO", "State"},
		    {"DAK", "ZipCode"},
		    {"DAP", "Zipcode"},
		    {"DCG", "Country"},

		    {"DBB", "DOB"},
		    {"DAQ", "DriverLicenseNumber"},
		    {"DBD", "LicenseIssuedDate"},
		    {"DBA", "LicenseExpirationDate"}

        };

        public Dictionary<string,Object> headers {get; private set;}
        public Dictionary<String, String> subfile { get; private set; }

        public Decoder(String data)
        {
            headers = decodeHeaders(data);
            subfile = decodeSubFile(data);
        }

        public String getFileType()
        {
            String result = (String)headers["FileType"];
            if (result != null && !String.IsNullOrEmpty(result))
            {
                result = result.Trim().ToUpper();
            }
            else
            {
                result = "";
            }
            return result;
        }

        public int getIdentificationNumber()
        {
            return Convert.ToInt32(headers["IdentificationNumber"]);
        }

        public int getVersionNumber()
        {
            return Convert.ToInt32(headers["VersionNumber"]);
        }

        public int getJurisdictionVerstion()
        {
            return Convert.ToInt32(headers["JurisdictionVerstion"]);
        }

        public String getSubfileType()
        {
            String result = (String)headers["SubfileType"];
            if (result != null && !String.IsNullOrEmpty(result))
            {
                result = result.Trim();
            }
            else
            {
                result = "";
            }
            return result;
        }

        public int getSubfileOffset()
        {
            return Convert.ToInt32(headers["SubfileOffset"]);
        }

        public int getSubfileLength()
        {
            return Convert.ToInt32(headers["SubfileLength"]);
        }

        protected Dictionary<String, Object> decodeHeaders(String data)
        {
            Dictionary<string,Object> hm = new Dictionary<string,Object>();

            // declare
            char complianceIndicator, dataElementSeparator, recordSeparator, segmentTerminator;
            String fileType, entries, subfileType;
            int versionNumber, issuerIdentificationNumber, jurisdictionVerstion, offset, length;

            // extract headers
            char[] test = data.ToCharArray();
            complianceIndicator = data[0];
            dataElementSeparator = data[1];
            recordSeparator = data[2];
            segmentTerminator = data[3];
            fileType = data.MySubString(4, 9);
            hm.Add("FileType", fileType);
            issuerIdentificationNumber = Convert.ToInt32(data.MySubString(9, 15));
            hm.Add("IdentificationNumber", issuerIdentificationNumber);
            versionNumber = Convert.ToInt32(data.MySubString(15, 17));
            hm.Add("VersionNumber", versionNumber);

            if (versionNumber <= 1)
            {
                entries = data.MySubString(17, 19);
                subfileType = data.MySubString(19, 21);
                offset = Convert.ToInt32(data.MySubString(21, 25));
                length = Convert.ToInt32(data.MySubString(25, 29));
            }
            else
            {
                jurisdictionVerstion = Convert.ToInt32(data.MySubString(17, 19));
                hm.Add("JurisdictionVerstion", jurisdictionVerstion);
                entries = data.MySubString(19, 21);
                subfileType = data.MySubString(21, 23);
                hm.Add("SubfileType", subfileType);
                offset = Convert.ToInt32(data.MySubString(23, 27));
                length = Convert.ToInt32(data.MySubString(27, 31));
            }

            if (fileType == "ANSI ")
            {
                offset += 2;
            }

            hm.Add("SubfileOffset", offset);
            hm.Add("SubfileLength", length);

            return hm;
        }

        protected Dictionary<String, String> decodeSubFile(String data) {
		    int offset = getSubfileOffset();
		    int length = getSubfileLength();

		    // subfile
		    String subfile = data.MySubString(offset, offset + length);

		    // store in name value pair
  
		    String[] lines = subfile.Split('\n');
		    Dictionary<String, String> hm = new Dictionary<String, String>();
		    foreach (String l in lines) {
			    if (l.Count() > 3) {
				    String key = l.MySubString(0, 3);
				    String value = l.MySubString(3, 3);
                    if (fields.ContainsKey(key))
                    {
					    hm.Add(fields[key], value);
				    }
			    }
		    }
		    return hm;
	    }

    }
}
