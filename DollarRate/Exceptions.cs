using System;

namespace DollarRate
{
    public class Exceptions:Exception
    {
        private string message;

        public string Mmessage
        {
            get { return message; }
            set { message = value; }
        }
        public Exceptions() 
        {
            this.message = "There was an error, please try again";
        }
        public Exceptions(String message)
        {
            this.message = message;
        }  
    }
}
