using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bantay_Kalamidad_Pilipinas__Backend_
{
    internal class Pledge
    {
        private string Donor_ID;
        private string Date_Pledge;
        private string Pledge_Status;

        public Pledge(string donor_ID, string date_Pledge, string pledge_Status)
        {
            Donor_ID = donor_ID;
            Date_Pledge = date_Pledge;
            Pledge_Status = pledge_Status;
        }
    }
}
