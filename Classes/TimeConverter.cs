using System;
using System.Diagnostics;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        /// <summary>
        /// Converts the input time into Berlin clock representation
        /// </summary>
        /// <param name="aTime">a time.</param>
        /// <returns>
        /// String that represents the Berlin format
        /// </returns>
        public string convertTime(string aTime)
        {
            try
            {
                // Split the input string with the ':' separator
                string[] inputTime = aTime.Split(':');

                // Write the result.
                string result = string.Format("{0}\n{1}\n{2}\n{3}\n{4}",
                    getSeconds(int.Parse(inputTime[2])),
                    getTopHours(int.Parse(inputTime[0])),
                    getBottomHours(int.Parse(inputTime[0])),
                    getTopMinutes(int.Parse(inputTime[1])),
                    getBottomMinutes(int.Parse(inputTime[1])));

                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return "";
            }
        }

        /// <summary>
        /// On the top of the clock there is a yellow lamp that blinks on/off every two seconds
        /// </summary>
        /// <param name="number">Input seconds</param>
        /// <returns>The seconds representation O or Y</returns>
        protected String getSeconds(int number)
        {
            if (number % 2 == 0) return "Y";
            else return "O";
        }

        /// <summary>
        /// In the top row there are 4 red lamps. Every lamp represents 5 hours.
        /// </summary>
        /// <param name="number">Input hours</param>
        /// <returns>The top hours representation O or R</returns>
        protected String getTopHours(int number)
        {
            return getOnOff(4, getTopNumberOfOnSigns(number));
        }

        /// <summary>
        /// In the lower row of red lamps every lamp represents 1 hour
        /// </summary>
        /// <param name="number">Input hours</param>
        /// <returns>The bottom hours representation O or R</returns>
        protected String getBottomHours(int number)
        {
            return getOnOff(4, number % 5);
        }

        /// <summary>
        /// In the first row every lamp represents 5 minutes. 
        /// In this first row the 3rd, 6th and 9th lamp are red and indicate the first quarter, half and last quarter of an hour. 
        /// The other lamps are yellow.
        /// </summary>
        /// <param name="number">Input minutes</param>
        /// <returns>The top minutes representation O or Y or R</returns>
        protected String getTopMinutes(int number)
        {
            return getOnOff(11, getTopNumberOfOnSigns(number), "Y").Replace("YYY", "YYR");
        }

        /// <summary>
        /// In the last row with 4 lamps every lamp represents 1 minute.
        /// </summary>
        /// <param name="number">Input minutes</param>
        /// <returns>The bottom minutes representation O or Y</returns>
        protected String getBottomMinutes(int number)
        {
            return getOnOff(4, number % 5, "Y");
        }

        private String getOnOff(int lamps, int onSigns)
        {
            return getOnOff(lamps, onSigns, "R");
        }

        /// <summary>
        /// Gets the on off string concatenation
        /// </summary>
        /// <param name="lamps">The lamps of the row</param>
        /// <param name="onSigns">The count of ON signs</param>
        /// <param name="onSign">The sign </param>
        /// <returns>String.</returns>
        private String getOnOff(int lamps, int onSigns, String onSign)
        {
            string output = "";
            for (int i = 0; i < onSigns; i++)
            {
                output += onSign;
            }
            for (int i = 0; i < (lamps - onSigns); i++)
            {
                output += "O";
            }
            return output;
        }

        /// <summary>
        /// Gets the top number of on signs, which is * 5
        /// </summary>
        /// <param name="number">The number for top row on hours & minutes</param>
        /// <returns>System.Int32.</returns>
        private int getTopNumberOfOnSigns(int number)
        {
            return (number - (number % 5)) / 5;
        }
    }
}
