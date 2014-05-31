using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharedClassLibrary
{
    /// <summary>
    /// A class that defines a person
    /// </summary>
    public class Person
    {
        #region Private instance variables

        private String name;
        private String firstname;
        private DateTime birthDate;
        private String gender;
        private Boolean married;
        private String salutation;

        #endregion

        #region Private methods

        /// <summary>
        /// Calculates the age of the person.
        /// </summary>
        /// <returns></returns>
        private Int32 CalculateAge()
        {
            //Store today's date
            DateTime now = DateTime.Today;
            //Calculate the difference in years
            Int32 years = now.Year - birthDate.Year;
            //Subtract one year, if the current date is 
            //before the birthday
            if ((now.Month < birthDate.Month) || 
                (now.Month == birthDate.Month && 
                now.Day < birthDate.Day))
            {
                years--;
            }
            return years;
        }

        #endregion

        #region Public properties

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        public String Firstname
        {
            get { return firstname; }
            set { firstname = value; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public String Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public Boolean Married
        {
            get { return married; }
            set { married = value; }
        }

        /// <summary>
        /// Readonly property which returns the
        /// age of the person
        /// </summary>
        public Int32 Age
        {
            get { return CalculateAge(); }
        }

        /// <summary>
        /// Readonly property that returns true
        /// if the person is a minor
        /// </summary>
        public Boolean Minor
        {
            get { return (Age < 18); }
        }

        /// <summary>
        /// Readonly property that returns true
        /// if the person is an adult
        /// </summary>
        public Boolean Adult
        {
            get { return (Age >= 18); }
        }

        public String Salutation
        {
            get { return salutation; }
            set { salutation = value; }
        }

        #endregion
    }
}
