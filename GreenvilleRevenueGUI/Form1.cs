using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GreenvilleRevenueGUI
{
    public partial class Form1 : Form
    {
        Cards DeckofCards = new Cards();
        Hand PlayerHand;
        Hand DealerHand;
        Cards ACardBack = new Cards();
        int hit = 1;
        Boolean winLose;
        int funds = 500;

        public Form1()
        {
            InitializeComponent();

            DeckofCards.loadCards();
            DeckofCards.ShuffleCards();
            PlayerHand = new Hand("Jacob West");
            DealerHand = new Hand("Dealer");
            label14.Text = "Funds: " + funds;
            button12.Enabled = false;
            button13.Enabled = false;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            BigPurpleBucsButton();
        }

        private void BigPurpleBucsButton()
        {
            button12.Enabled = true;
            button13.Enabled = true;
            handReset();
            Deal();
        }

        private void Button12_Click(object sender, EventArgs e)
        {
            Boolean lose;
            Card aCard = DeckofCards.getNextCard();
            switch (hit)
            {
                case 1:
                    PlayerHand.dealCard(aCard);
                    button9.Image = aCard.GetCardImage();
                    label11.Text = aCard.GetCardValue().ToString();
                    label3.Text = "Player Card Value: " + PlayerHand.getTotalValue().ToString();
                    hit++;
                    break;
                case 2:
                    PlayerHand.dealCard(aCard);
                    button10.Image = aCard.GetCardImage();
                    label12.Text = aCard.GetCardValue().ToString();
                    label3.Text = "Player Card Value: " + PlayerHand.getTotalValue().ToString();
                    hit++;
                    break;
                case 3:
                    PlayerHand.dealCard(aCard);
                    button11.Image = aCard.GetCardImage();
                    label13.Text = aCard.GetCardValue().ToString();
                    label3.Text = "Player Card Value: " + PlayerHand.getTotalValue().ToString();
                    hit++;
                    break;
                case 4:
                    break;
            }
        }

        public void handReset()
        {
            button12.Enabled = false;
            button13.Enabled = false;
            Card aCard = ACardBack.getTheBackOfTheCard();
            PlayerHand.resetHand();
            DealerHand.resetHand();
            hit = 1;
            label1.Text = "";
            label4.Text = "";
            label5.Text = "";
            label7.Text = "";
            label8.Text = "";
            label9.Text = "";
            label10.Text = "";
            label11.Text = "";
            label12.Text = "";
            label13.Text = "";
            button2.Image = aCard.GetCardImage();
            button3.Image = aCard.GetCardImage();
            button4.Image = aCard.GetCardImage();
            button5.Image = aCard.GetCardImage();
            button6.Image = aCard.GetCardImage();
            button7.Image = aCard.GetCardImage();
            button8.Image = aCard.GetCardImage();
            button9.Image = aCard.GetCardImage();
            button10.Image = aCard.GetCardImage();
            button11.Image = aCard.GetCardImage();
            label3.Text = "Player Card Value: " + PlayerHand.getTotalValue().ToString();
            label2.Text = "Dealer Card Value: ???";
        }

        public Boolean isBust()
        {
            int playerValue = PlayerHand.getTotalValue();
            Boolean isBust = false;
            if (playerValue > 21)
            {
                isBust = true;
                return isBust;
            }
            else
            {
                isBust = false;
                return isBust;
            }
        }

        public void bust()
        {
            if (isBust())
            {
                Boolean lose = false;
                winOrLose(lose);
            }
        }

        //deal out initial cards for dealer and player for deal button.
        public void Deal()
        {

            button12.Enabled = true;
            button13.Enabled = true;
            //dealer hand
            Card aCard = DeckofCards.getNextCard();
            button2.Image = aCard.GetCardImage();
            DealerHand.dealCard(aCard);
            label1.Text = aCard.GetCardValue().ToString();

            aCard = DeckofCards.getNextCard();
            DealerHand.dealCard(aCard);
            label4.Text = "???";
            label2.Text = "Dealer Card Value: ???";


            //player hand
            aCard = DeckofCards.getNextCard();
            button7.Image = aCard.GetCardImage();
            PlayerHand.dealCard(aCard);
            label9.Text = aCard.GetCardValue().ToString();

            aCard = DeckofCards.getNextCard();
            button8.Image = aCard.GetCardImage();
            PlayerHand.dealCard(aCard);
            label10.Text = aCard.GetCardValue().ToString();
            label3.Text = "Player Card Value: " + PlayerHand.getTotalValue().ToString();
        }

        private void wonBet()
        {
            Funds myFunds = new Funds(funds);
            int betAmount = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
            myFunds.SetBetAmount(betAmount);
            myFunds.WonBet();
            label14.Text = "Funds: " + myFunds.GetTotalMoney();
            funds = myFunds.GetTotalMoney();
        }

        private void lostBet()
        {
            Funds myFunds = new Funds(funds);
            int betAmount = Convert.ToInt32(Math.Round(numericUpDown1.Value, 0));
            myFunds.SetBetAmount(betAmount);
            myFunds.LostBet();
            label14.Text = "Funds: " + myFunds.GetTotalMoney();
            funds = myFunds.GetTotalMoney();
        }

        public void winOrLose(Boolean winOrLose)
        {
            if (winOrLose)
            {
                DialogResult dialogResult = MessageBox.Show("You win! Would you like to play again?", "Win!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    wonBet();
                    handReset();
                    Deal();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }

            else
            {
                DialogResult dialogResult = MessageBox.Show("You lose! Would you like to play again?", "Lose!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    lostBet();
                    handReset();
                    Deal();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }
        }

        public Boolean evaluation()
        {
            int playerTotal = PlayerHand.getTotalValue();
            int dealerTotal = DealerHand.getTotalValue();
            int dealerHand = DealerHand.getNumberofCards();
            Boolean dealing = false;
            Boolean winLose = false;
            int index = 0;

            if (playerTotal == 21)
            {
                winLose = true;
                dealing = false;
                winOrLose(winLose);
                return dealing;
            }

            //Will check to see if Aces can be changed to save from a bust
            while (index <= 4)
            {
                dealerTotal = DealerHand.getTotalValue();
                if (dealerTotal > 21)
                {
                    checkDealerAce(index);
                }
                index++;
            }

            dealerTotal = DealerHand.getTotalValue();

            if (playerTotal > 21)
            {
                bust();
                return dealing;
            }

            if (playerTotal > dealerTotal && dealerTotal == 17)
            {
                winLose = true;
                winOrLose(winLose);
                return dealing;
            }

            if (dealerTotal > playerTotal && dealerTotal > 21)
            {
                winLose = true;
                winOrLose(winLose);
                return dealing;
            }

            if(dealerTotal > playerTotal && dealerTotal <= 21)
            {
                winOrLose(winLose);
                return dealing;
            }

            if (playerTotal > dealerTotal)
            {
                dealing = true;
                return dealing;
            }

            if (dealerTotal == playerTotal && dealerTotal < 21 && dealerTotal < 17)
            {
                dealing = true;
                return dealing;
            }


            if (dealerHand == 5 && dealerTotal == playerTotal)
            {
                DialogResult dialogResult = MessageBox.Show("It's a tie! Would you like to play again?", "Tie!", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    handReset();
                    Deal();
                }
                else if (dialogResult == DialogResult.No)
                {
                    Application.Exit();
                }
            }

            return dealing;
        }

        /*
         *********Dealer Section*********
         */

        private void checkDealerAce(int index)
        {
            Card ACard = DealerHand.GetaCard(index);
            Boolean hideLabel = false;
            if (ACard != null)
            {
                switch (index)
                {

                    case 0:
                        changeDealerAce(index);
                        UpdateDealerGraphics(index, hideLabel);
                        break;
                    case 1:
                        changeDealerAce(index);
                        UpdateDealerGraphics(index, hideLabel);
                        break;
                    case 2:
                        changeDealerAce(index);
                        UpdateDealerGraphics(index, hideLabel);
                        break;
                    case 3:
                        changeDealerAce(index);
                        UpdateDealerGraphics(index, hideLabel);
                        break;
                    case 4:
                        changeDealerAce(index);
                        UpdateDealerGraphics(index, hideLabel);
                        break;
                }
            }
        }

        private void changeDealerAce(int index)
        {
            Card ACard = DealerHand.GetaCard(index);
            ACard.GetCardisanAce();
            if (ACard.GetCardisanAce() && ACard.GetCardValue() == 11)
            {
                DealerHand.changeAceValue(index);
            }
        }

        //Stay button, activates dealer
        private void button13_Click(object sender, EventArgs e)
        {
            button12.Enabled = false;
            button13.Enabled = false;
            showDealerCard();
            Boolean dealing = evaluation();
            int hit = 0;

            while (dealing)
            {
                dealing = dealerHit(hit);
                hit++;
                evaluation();
            }
            
        }

        //Show the dealers hand
        public void showDealerCard()
        {
            Card aCard = DealerHand.GetaCard(1);
            label4.Text = aCard.GetCardValue().ToString();
            button3.Image = aCard.GetCardImage();
            label2.Text = "Dealer Card Value: " + DealerHand.getTotalValue().ToString();
        }

        //hits for the dealer
        public Boolean dealerHit(int hit)
        {
            Card aCard = DeckofCards.getNextCard();
            Boolean dealing = true;

            switch (hit)
            {
                case 0:
                    DealerHand.dealCard(aCard);
                    button4.Image = aCard.GetCardImage();
                    label5.Text = aCard.GetCardValue().ToString();
                    label2.Text = "Dealer Card Value: " + DealerHand.getTotalValue();
                    dealing = evaluation();
                    return dealing;
                case 1:
                    aCard = DeckofCards.getNextCard();
                    DealerHand.dealCard(aCard);
                    button5.Image = aCard.GetCardImage();
                    label7.Text = aCard.GetCardValue().ToString();
                    label2.Text = "Dealer Card Value: " + DealerHand.getTotalValue();
                    dealing = evaluation();
                    return dealing;
                case 2:
                    aCard = DeckofCards.getNextCard();
                    DealerHand.dealCard(aCard);
                    button6.Image = aCard.GetCardImage();
                    label8.Text = aCard.GetCardValue().ToString();
                    label2.Text = "Dealer Card Value: " + DealerHand.getTotalValue();
                    dealing = evaluation();
                    return dealing;
            }
            return dealing;
        }

        //----------------------------------------------------
        //GIVE ME ACES BUTTONS - 2 ACES TO DEALER 2 TO PLAYER
        //----------------------------------------------------
        private void Button14_Click_1(object sender, EventArgs e)
        {
            DeckofCards.loadCards();
            DeckofCards.PutAcesFirst();
            BigPurpleBucsButton();
        }

        private void UpdateDealerGraphics(int cardnumber, Boolean HideLabel)
        {
            Card ACard = DealerHand.GetaCard(cardnumber);

            switch (cardnumber)
            {
                case 0:
                    button2.Image = ACard.GetCardImage();
                    label1.Text = " " + ACard.GetCardValue();
                    break;
                case 1:
                    if (HideLabel)
                    {
                        button3.Image = ACard.GetCardImage();
                        label4.Text = "??";
                    }
                    else
                    {
                        button3.Image = ACard.GetCardImage();
                        label4.Text = " " + ACard.GetCardValue();
                    }
                    break;
                case 2:
                    button4.Image = ACard.GetCardImage();
                    label5.Text = " " + ACard.GetCardValue();
                    break;
                case 3:
                    button5.Image = ACard.GetCardImage();
                    label7.Text = " " + ACard.GetCardValue();
                    break;
                case 4:
                    button6.Image = ACard.GetCardImage();
                    label8.Text = " " + ACard.GetCardValue();
                    break;
            }

            if (!HideLabel)
            {
                label2.Text = "Dealer Card Value: " + DealerHand.getTotalValue();
            }

        }

        private void UpdatePlayerGraphics(int index)
        {
            Card ACard = PlayerHand.GetaCard(index);

            switch (index)
            {
                //FIRST CASE STATEMENT
                case 0:
                    button7.Image = ACard.GetCardImage();
                    label9.Text = " " + ACard.GetCardValue();
                    break;
                //SECOND CASE STATEMENT
                case 1:
                    button8.Image = ACard.GetCardImage();
                    label10.Text = " " + ACard.GetCardValue();
                    break;
                case 2:
                    button9.Image = ACard.GetCardImage();
                    label11.Text = " " + ACard.GetCardValue();
                    break;
                case 3:
                    button10.Image = ACard.GetCardImage();
                    label12.Text = " " + ACard.GetCardValue();
                    break;
                case 4:
                    button13.Image = ACard.GetCardImage();
                    label9.Text = " " + ACard.GetCardValue();
                    break;
            }

            label3.Text = "Player Card Value: " + PlayerHand.getTotalValue();
        }

        //----------------------------------------------------------------
        //  IN THIS SECTION I WILL CREATE A WAY TO CHANGE ACES FROM 1 - 11
        //----------------------------------------------------------------

        private void changePlayerAce(int index)
        {
            Card ACard = PlayerHand.GetaCard(index);
            Hand hand = PlayerHand;
            ACard.GetCardisanAce();
            if (ACard.GetCardisanAce())
            {
                PlayerHand.changeAceValue(index);
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Card ACard = PlayerHand.GetaCard(0);
            if (ACard != null)
            {
                changePlayerAce(0);
                UpdatePlayerGraphics(0);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Card ACard = PlayerHand.GetaCard(1);
            if(ACard != null)
            {
                changePlayerAce(1);
                UpdatePlayerGraphics(1);
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Card ACard = PlayerHand.GetaCard(2);
            if (ACard != null)
            {
                changePlayerAce(2);
                UpdatePlayerGraphics(2);
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Card ACard = PlayerHand.GetaCard(3);
            if (ACard != null)
            {
                changePlayerAce(3);
                UpdatePlayerGraphics(3);
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Card ACard = PlayerHand.GetaCard(4);
            if (ACard != null)
            {
                changePlayerAce(4);
                UpdatePlayerGraphics(4);
            }
        }
    }
}

