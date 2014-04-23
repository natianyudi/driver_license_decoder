using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DriverLicenseDecoder
{

    class DriverLicense
    {
        private String dataString;
        private Dictionary<String, String> dataHash;
        private Decoder decoder;

        public DriverLicense(String barCode)
        {
            dataString = barCode;
            decoder = new Decoder(barCode);
            dataHash = decoder.subfile;
        }

        public Decoder getDecoder()
        {
            return decoder;
        }

        /**
         * Get extracted first name or parse from name string.
         * 
         * @return
         * 
         *         Examples:
         * 
         *         Name: "TianYu Huang", firstName: "TianYu";
         * 
         *         Name: "Tian Yu Huang", firstName: "Tian";
         * 
         *         Name: "Tian Yu Z Huang", firstName: "Tian Yu Z";
         * 
         */
        public String getFirstName()
        {
            String firstName = "";
            if(dataHash.ContainsKey("FirstName"))
                firstName = (String)dataHash["FirstName"];

            if (firstName != null && !String.IsNullOrEmpty(firstName))
            {
                firstName = firstName.Trim();
            }
            else
            {
                String name = "";
                if (dataHash.ContainsKey("Name"))
                    name = (String)dataHash["Name"];

                if (name != null && !!String.IsNullOrEmpty(name))
                {
                    String[] nameTokens = name.Split(' ');
                    if (nameTokens.Count() <= 3)
                    {
                        firstName = nameTokens[0].Trim();
                    }
                    else
                    {
                        for (int i = 1; i < nameTokens.Count(); i++)
                        {
                            firstName += nameTokens[i].Trim();
                            if (i < nameTokens.Count() - 1)
                            {
                                firstName += " ";
                            }
                        }
                    }
                }
                else
                {
                    firstName = "";
                }
            }
            return firstName;
        }

        /**
         * Get extracted last name or parse from name string.
         * 
         * @return
         * 
         *         Examples:
         * 
         *         Name: "TianYu Huang", lastName: "Huang";
         * 
         *         Name: "Tian Yu Huang", lastName: "Huang";
         * 
         *         Name: "Tian Yu Z Huang", lastName: "Huang";
         * 
         */
        public String getLastName()
        {
            String lastName = "";
            bool success = dataHash.TryGetValue("LastName", out lastName);
            if (lastName != null && !String.IsNullOrEmpty(lastName) && success)
            {
                lastName = lastName.Trim();
            }
            else
            {
                String name = "";
                if (dataHash.ContainsKey("Name"))
                    name = (String)dataHash["Name"];
                if (name != null && !String.IsNullOrEmpty(name))
                {
                    String[] nameTokens = name.Split(' ');
                    if (nameTokens.Count() == 1)
                    {
                        lastName = "";
                    }
                    else
                    {
                        lastName = nameTokens[nameTokens.Count() - 1].Trim();
                    }
                }
                else
                {
                    lastName = "";
                }
            }
            return lastName;
        }

        /**
         * Get extracted middle name or parse from name string.
         * 
         * @return
         * 
         *         Examples:
         * 
         *         Name: "TianYu Huang", middleName: "";
         * 
         *         Name: "Tian Yu Huang", middleName: "Yu";
         * 
         *         Name: "Tian Yu Z Huang", middleName: "";
         * 
         */
        public String getMiddleName()
        {
            String middleName = (String)dataHash["MiddleName"];
            if (middleName != null && !String.IsNullOrEmpty(middleName))
            {
                middleName = middleName.Trim();
            }
            else
            {
                String name = (String)dataHash["Name"];
                if (name != null && !String.IsNullOrEmpty(name))
                {
                    String[] nameTokens = name.Split(' ');
                    if (nameTokens.Count() == 3)
                    {
                        middleName = nameTokens[1].Trim();
                    }
                    else
                    {
                        middleName = "";
                    }
                }
                else
                {
                    middleName = "";
                }
            }
            return middleName;
        }

        /**
         * Get extracted state
         * 
         * @return 2-Letter state abbreviations
         */
        public String getState()
        {
            String state = (String)dataHash["State"];
            if (state != null && !String.IsNullOrEmpty(state))
            {
                state = state.Trim().ToUpper();
            }
            else
            {
                state = "";
            }
            return state;
        }

        /**
         * Get extracted address
         * 
         * @return Address
         */
        public String getAddress()
        {
            String address = (String)dataHash["Address"];
            if (address != null && !String.IsNullOrEmpty(address))
            {
                address = address.Trim();
            }
            else
            {
                address = "";
            }
            return address;
        }

        /**
         * Get extracted city
         * 
         * @return City
         */
        public String getCity()
        {
            String city = (String) dataHash["City"];
            if (city != null && !String.IsNullOrEmpty(city))
            {
                city = city.Trim();
            }
            else
            {
                city = "";
            }
            return city;
        }

        /**
         * Get extracted ZIP code
         * 
         * @return ZIP code
         */
        public String getZipCode()
        {
            String zipCode = (String) dataHash["ZipCode"];
            if (zipCode != null && !String.IsNullOrEmpty(zipCode))
            {
                zipCode = zipCode.Trim();
            }
            else
            {
                zipCode = "";
            }
            return zipCode;
        }

        /**
         * Get extracted country
         * 
         * @return Country
         */
        public String getCountry()
        {
            String country = (String) dataHash["Country"];
            if (country != null && !String.IsNullOrEmpty(country))
            {
                country = country.Trim().ToUpper();
            }
            else
            {
                country = "";
            }
            return country;
        }

        /**
         * Get extracted eye color
         * 
         * @return Eye color
         */
        public String getEyeColor()
        {
            String eyeColor = (String) dataHash["EyeColor"];
            if (eyeColor != null && !String.IsNullOrEmpty(eyeColor))
            {
                eyeColor = eyeColor.Trim();
            }
            else
            {
                eyeColor = "";
            }
            return eyeColor;
        }

        /**
         * Get extracted driver's license number
         * 
         * @return Driver's license number
         */
        public String getDriverLicenseNumber()
        {
            String licenseNumber = (String) dataHash["DriverLicenseNumber"];
            if (licenseNumber != null && !String.IsNullOrEmpty(licenseNumber))
            {
                licenseNumber = Regex.Replace(licenseNumber.Trim(),"[.]", "");
            }
            else
            {
                licenseNumber = "";
            }
            return licenseNumber;
        }

        /**
         * Get sex
         * 
         * @return Sex
         */
        public String getSex()
        {
            String sex = (String) dataHash["Sex"];
            if (sex != null && !String.IsNullOrEmpty(sex))
            {
                sex = sex.Trim();
                if (sex == "1")
                {
                    sex = "M";
                }
                else if (sex == "2")
                {
                    sex = "F";
                }
                else
                {
                    sex = sex.ToUpper();
                }
            }
            else
            {
                sex = "";
            }
            return sex;
        }

        /**
         * Get parsed DOB
         * 
         * @return DOB
         */
        public DateTime? getDOB()
        {
            DateTime? calendar = null;
            String dob = (String) dataHash["DOB"];
            if (dob != null && !String.IsNullOrEmpty(dob))
            {
                calendar = parseDate(dob);
                return calendar;
            }
            else
            {
                // Not found
            }
            return calendar;
        }

        /**
         * Get parsed LicenseIssuedDate
         * 
         * @return LicenseIssuedDate
         */
        public DateTime? getLicenseIssuedDate()
        {
            DateTime? calendar = null;
            String licenseIssuedDate = (String) dataHash["LicenseIssuedDate"];
            if (licenseIssuedDate != null && !String.IsNullOrEmpty(licenseIssuedDate))
            {
                calendar = parseDate(licenseIssuedDate);
            }
            else
            {
                // Not found
            }
            return calendar;
        }

        /**
         * Get parsed LicenseExpirationDate
         * 
         * @return LicenseExpirationDate
         */
        public DateTime? getLicenseExpirationDate()
        {
            DateTime? calendar = null;
            String licenseExpirationDate = (String) dataHash["LicenseExpirationDate"];
            if (licenseExpirationDate != null && !String.IsNullOrEmpty(licenseExpirationDate))
            {
                calendar = parseDate(licenseExpirationDate);
            }
            else
            {
                // Not found
            }
            return calendar;
        }

        /**
         * Get parsed Height
         * 
         * @return Height
         */
        public double getHeight()
        {
            String height = (String) dataHash["Height"];
            if (height != null && !String.IsNullOrEmpty(height))
            {
                height = Regex.Replace(height.Trim(), "[\\D]", ""); 
            }
            else
            {
                height = "";
            }
            return Convert.ToDouble(height);
        }

        public bool hasExpired()
        {
            DateTime dt = DateTime.Now;
            return getLicenseExpirationDate() < dt;
        }

        /**
         * Get current object representation in JSON string
         * 
         * @return Serialized string in JSON
         */
        public String toJson()
        {
            //{
            //  "Name": "Apple",
            //  "Expiry": "2008-12-28T00:00:00",
            //  "Sizes": [
            //    "Small"
            //  ]
            //}
            String json = "";

            Dictionary<String, Object> jsonHash = new Dictionary<String, Object>();
            jsonHash.Add("first_name", getFirstName());
            jsonHash.Add("last_name", getLastName());
            jsonHash.Add("address", getAddress());
            jsonHash.Add("city", getCity());
            jsonHash.Add("state", getState());
            jsonHash.Add("zipcode", getZipCode());
            jsonHash.Add("driver_license_number", getDriverLicenseNumber());
            jsonHash.Add("eye_color", getEyeColor());
            jsonHash.Add("height", getHeight());
            jsonHash.Add("sex", getSex());

            jsonHash.Add("dob", formatDate(getDOB()));
            jsonHash.Add("license_issued_date", formatDate(getLicenseIssuedDate()));
            jsonHash.Add("license_expiration_date",
                    formatDate(getLicenseExpirationDate()));
            json = JsonConvert.SerializeObject(jsonHash);
            //json = gson.toJson(jsonHash);
            return json;
        }

        // -----------------------------------------------------------------------//

        protected DateTime parseDate(String date)
        {
            String format = "ISO";
            int potentialYear = Convert.ToInt32(date.MySubString(0, 4));
            if (potentialYear > 1300)
            {
                format = "Other";
            }

            // Parse calendar based on format
            int year, month, day;
            DateTime calendar;
            if (format == "ISO")
            {
                year = Convert.ToInt32(date.MySubString(4, 8));
                month = Convert.ToInt32(date.MySubString(0, 2));
                day = Convert.ToInt32(date.MySubString(2, 4));
            }
            else
            {
                year = Convert.ToInt32(date.MySubString(0, 4));
                month = Convert.ToInt32(date.MySubString(4, 6));
                day = Convert.ToInt32(date.MySubString(6, 8));
            }
            calendar = new DateTime(year, month, day, 0, 0, 0);
            return calendar;
        }

        protected String formatDate(DateTime? date)
        {
            if (date.HasValue)
            {
                DateTime dt = (DateTime)date;
                return dt.ToString("MM/dd/yyyy");
            }
            else
                return "";
            
        }
    }
}
