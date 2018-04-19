using System;
using Android.App;
using Android.OS;
using Android.Widget;

namespace BlackJack
{
    [Activity(Label = "Game", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class GameActivity : Activity
    {
        TextView score;
        TextView handTotal;
        TextView convoText;

        CardView dealersFirstCard;
        CardView dealersSecondCard;
        CardView dealersThirdCard;
        CardView dealersFourthCard;
        CardView dealersFifthCard;

        CardView playersFirstCard;
        CardView playersSecondCard;
        CardView playersThirdCard;
        CardView playersFourthCard;
        CardView playersFifthCard;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Game);

            score = FindViewById<TextView>(Resource.Id.Score);
            handTotal = FindViewById<TextView>(Resource.Id.HandTotal);
            convoText = FindViewById<TextView>(Resource.Id.ConvoText);

            dealersFirstCard = FindViewById<CardView>(Resource.Id.DealersFirstCard);
            dealersSecondCard = FindViewById<CardView>(Resource.Id.DealersSecondCard);
            dealersThirdCard = FindViewById<CardView>(Resource.Id.DealersThirdCard);
            dealersFourthCard = FindViewById<CardView>(Resource.Id.DealersFourthCard);
            dealersFifthCard = FindViewById<CardView>(Resource.Id.DealersFifthCard);

            playersFirstCard = FindViewById<CardView>(Resource.Id.PlayersFirstCard);
            playersSecondCard = FindViewById<CardView>(Resource.Id.PlayersSecondCard);
            playersThirdCard = FindViewById<CardView>(Resource.Id.PlayersThirdCard);
            playersFourthCard = FindViewById<CardView>(Resource.Id.PlayersFourthCard);
            playersFifthCard = FindViewById<CardView>(Resource.Id.PlayersFifthCard);

            Button buttonStick = FindViewById<Button>(Resource.Id.ButtonStick);
            Button buttonHit = FindViewById<Button>(Resource.Id.ButtonHit);

            buttonStick.Click += StickButton_Click;
            buttonHit.Click += HitButton_Click;
        }

        private void HitButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void StickButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
