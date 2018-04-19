namespace DeckOfCards
{
    public class Card
    {
        public int Value { get; }

        public CardSuit Suit { get; }

        public Card(int value, CardSuit suit)
        {
            Value = value;
            Suit = suit;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", GetCardValue(), GetcardSuit());
        }

        public string GetcardSuit()
        {
            char suitIcon;

            switch (Suit)
            {
                case CardSuit.Clubs:
                    suitIcon = '\u2663';
                    break;
                case CardSuit.Spades:
                    suitIcon = '\u2660';
                    break;
                case CardSuit.Diamonds:
                    suitIcon = '\u2666';
                    break;
                case CardSuit.Hearts:
                    suitIcon = '\u2665';
                    break;
                default:
                    suitIcon = '?';
                    break;
            }

            return suitIcon.ToString();
        }

        public string GetCardValue()
        {
            string cardValue;

            switch (Value)
            {
                case 1:
                    cardValue = "A";
                    break;
                case 11:
                    cardValue = "J";
                    break;
                case 12:
                    cardValue = "Q";
                    break;
                case 13:
                    cardValue = "K";
                    break;
                default:
                    cardValue = Value.ToString();
                    break;
            }

            return cardValue;
        }
    }
}