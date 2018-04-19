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
        }

        public void SetCardValues(Card card)
        {
            topLeftChar.Text = card.Value.ToString();
            bottomRightChar.Text = card.Value.ToString();
            centreSuitChar.Text = GetcardSuit(card);
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
    }
}