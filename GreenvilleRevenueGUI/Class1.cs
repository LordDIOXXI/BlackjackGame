using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace GreenvilleRevenueGUI
{
    class Card
    {
        private Image image;
        private int value;
        private int secondValue;
        private Boolean isAce;

        public Card(Image myImage, int myValue)
        {
            image = myImage;
            value = myValue;
            isAce = false;
            secondValue = 0;
        }

        public void setCardToAce()
        {
            isAce = true;
        }

        public Boolean GetCardisanAce()
        {
            return isAce;
        }

        public Image GetCardImage()
        {
            return image;
        }

        public int GetCardValue()
        {
            return value;
        }

        public void setCardValue(int newValue)
        {
            value = newValue;
        }
    }
}
