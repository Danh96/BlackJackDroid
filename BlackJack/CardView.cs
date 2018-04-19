using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;

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
    }
}