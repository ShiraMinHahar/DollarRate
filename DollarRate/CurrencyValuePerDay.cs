using System;

namespace DollarRate
{
    public class CurrencyValuePerDay
    {
        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        private float currencyValue;
        public float CurrencyValue
        {
            get { return currencyValue; }
            set { currencyValue = value; }
        }

        public CurrencyValuePerDay()
        {

        }
        public CurrencyValuePerDay(DateTime date, float currencyValue)
        {
            this.date = date;
            this.currencyValue = currencyValue;
        }
    }
}
