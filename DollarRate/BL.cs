using System;
using System.Collections.Generic;
namespace DollarRate
{
    public class BL
    {
        public List<CurrencyValuePerDay> currencyValuesPerDay;
        public List<String> currencyTypes;
        public BL()
        {
            this.currencyValuesPerDay = new List<CurrencyValuePerDay>();
            this.currencyTypes = new List<String>();
        }

        public BL(List<CurrencyValuePerDay> currencyValuesPerDay, List<String> currencyTypes)
        {
            this.currencyValuesPerDay = currencyValuesPerDay;
            this.currencyTypes = currencyTypes;
        }

        //create a list from BL objects for x and y axes
        public List<CurrencyValuePerDay> createList(String[] linesFromCSV, int index)
        {
            DateTime dateValue;
            float floatValue;
            Form1 f = new Form1();
            //create a list of currencies types for the combobox
            var arr1 = linesFromCSV[0].Split(',');
            if(!arr1[0].ToUpper().Equals("DATE"))
            {
                throw new Exceptions("Invalid file");
            }
            for (int i = 1; i < arr1.Length; i++)
            {
                this.currencyTypes.Add(arr1[i]);
            }
            //pass on the table lines and convert columns from string to date and float
            for (int i = 1; i < linesFromCSV.Length; i++)
            {
                var arr = linesFromCSV[i].Split(',');
                if (DateTime.TryParse(arr[0], out dateValue) && float.TryParse(arr[index+1], out floatValue))
                {
                    var t = new CurrencyValuePerDay(dateValue, floatValue);
                    this.currencyValuesPerDay.Add(t);
                }
                else
                {
                    throw new Exceptions("Invalid file");
                }
            }
            return this.currencyValuesPerDay;
        }
    }
}

