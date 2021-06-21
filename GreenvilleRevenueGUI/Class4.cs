using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenvilleRevenueGUI
{
    //MONEY CLASS
    class Funds
    {
        private int totalmoney = 0;
        private int betamount = 10;

        public Funds(int initialfunds)
        {
            totalmoney = initialfunds;
        }

        public int GetBetAmount()
        {
            return betamount;
        }
        public void SetBetAmount(int betamt)
        {
            betamount = betamt;
        }
        public void WonBet()
        {
            totalmoney = totalmoney + betamount;
        }
        public void LostBet()
        {
            totalmoney = totalmoney - betamount;
        }

        public int GetTotalMoney()
        {
            return totalmoney;
        }
    }

}