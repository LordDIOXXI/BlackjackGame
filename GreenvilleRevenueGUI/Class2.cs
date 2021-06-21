using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace GreenvilleRevenueGUI
{
    class Cards
    {
        Random rand = new Random();
        Card[] AllCards = new Card[52];
        int currentCard = 0;
        Card ACardBack;
        int tries;

        public Cards()
        {
            loadCards();
        }

        public void loadCards()
        {
            Card ACard;
            String msg = "";
            try
            {
                string[] list = Directory.GetFiles(@"cards", "*.gif");
                Array.Sort(list);// need this for the mac computer

                for (int index = 0; index < 52; index++)
                {
                    int value = getNextCardValue(index);
                    Image image = Image.FromFile(list[index]);

                    ACard = new Card(image, value);
                    if (index > 31 && index < 36)
                    {
                        ACard.setCardToAce();
                    }
                    AllCards[index] = ACard;
                }
                string[] list2 = Directory.GetFiles(@"cards", "Wfswbackcard*.gif");
                Image Backimage = Image.FromFile(list2[0]);
                ACardBack = new Card(Backimage, 0);
            }
            catch (Exception e)
            {
                if (tries < 2)
                {
                    msg = "Error Please make sure the card files in the Directory. \nWhen you put the cards in the Directory hit OK button.\n\n " + e.ToString();
                    MessageBox.Show(msg);
                    tries++;
                    loadCards();

                }
                else
                {
                    Environment.Exit(1);
                }

            }
        }

        public void PutAcesFirst()
        {
            int aceindex = 0;
            for (int index = 0; index < 52; index++)
            {
                Card TempCard1 = AllCards[index];

                if (TempCard1.GetCardisanAce())
                {
                    Card OriginalCard = AllCards[aceindex];
                    TempCard1.setCardToAce();
                    AllCards[aceindex] = TempCard1;
                    AllCards[index] = OriginalCard;
                    aceindex++;
                }

            }

            for (int index = 0; index < 52; index++)
            {
                Card TempCard1 = AllCards[index];
                if (TempCard1.GetCardValue() == 10)
                {
                    Card TempCardJack = AllCards[index];
                    Card TempCard4 = AllCards[4];
                    AllCards[index] = TempCard4;
                    AllCards[4] = TempCardJack;

                }
            }

            currentCard = 0;//RWW
        }

        public Card getTheBackOfTheCard()
        {
            return ACardBack;
        }

        public Card getNextCard()
        {
            int value = currentCard;
            currentCard++;
            if (currentCard == 52)
            {
                currentCard = 0;
                ShuffleCards();
            }

            return AllCards[value];
        }

        private int getNextCardValue(int currentCard)
        {
            int cardValue = 0;
            if (currentCard < 33)
            {
                cardValue = (currentCard / 4) + 2;
            }

            else
            {
                cardValue = 10;
            }


            if (currentCard >= 32 && currentCard <= 35)
            {
                cardValue = 11;
            }

            return cardValue;
        }

        public int getCurrentCardNumber()
        {
            return currentCard;
        }

        public void ShuffleCards()
        {

            int numTimes = rand.Next(41, 100);

            for (int x = 0; x < numTimes; x++)
            {
                int r1 = rand.Next(1, 51);
                int r2 = rand.Next(1, 51);
                Card C1 = AllCards[r1];
                Card C2 = AllCards[r2];
                AllCards[r2] = C1;
            }


        }

    }
}
