using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using DeckOfCards;

namespace BlackJack
{
    public class CardView : LinearLayout
    {
        TextView topLeftChar;
        TextView bottomRightChar;
        TextView centreSuitChar;
        LinearLayout cardLayout;

        public CardView(Context context) :
            base(context)
        {
            InflateLayout(context);
        }
        public CardView(Context context, IAttributeSet attrs) :
            base(context, attrs)
        {
            InflateLayout(context);
        }

        public CardView(Context context, IAttributeSet attrs, int defStyle) :
            base(context, attrs, defStyle)
        {
            InflateLayout(context);
        }

        void InflateLayout(Context context)
        {
            LayoutInflater inflater = LayoutInflater.FromContext(context);
            inflater.Inflate(Resource.Layout.CardTemplate, this);

            topLeftChar = FindViewById<TextView>(Resource.Id.TopLeftChar);
            bottomRightChar = FindViewById<TextView>(Resource.Id.BottomRightChar);
            centreSuitChar = FindViewById<TextView>(Resource.Id.CentreSuitChar);
            cardLayout = FindViewById<LinearLayout>(Resource.Id.LayoutCard);
        }

        public void SetCardValues(Card card)
        {
            topLeftChar.Text = GetCardValue(card);
            bottomRightChar.Text = GetCardValue(card);
            centreSuitChar.Text = GetcardSuit(card);
        }

        public void SetDealerCardFaceDown(bool faceDown = true)
        {
            if (faceDown == true)
            {
                cardLayout.SetBackgroundResource(Resource.Drawable.CardBack);
                topLeftChar.Text = string.Empty;
                bottomRightChar.Text = string.Empty;
                centreSuitChar.Text = string.Empty;
            }
            else
            {
                cardLayout.SetBackgroundResource(Resource.Drawable.CardBackground);
            }
        }

        private string GetcardSuit(Card card)
        {
            char suitIcon;

            switch (card.Suit)
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

        public string GetCardValue(Card card)
        {
            string cardValue;

            switch (card.Value)
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
                    cardValue = card.Value.ToString();
                    break;
            }

            return cardValue;
        }
    }
}