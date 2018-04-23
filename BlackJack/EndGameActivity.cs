using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Widget;

namespace BlackJack
{
    [Activity(Label = "EndGameActivity", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen", ScreenOrientation = ScreenOrientation.Portrait)]
    public class EndGameActivity : Activity
    {
        private int PlayerGameScore;
        private int DealerGameScore;
        private TextView winner;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.EndGame);

            Button buttonHome = FindViewById<Button>(Resource.Id.ButtonHome);
            winner = FindViewById<TextView>(Resource.Id.WinnerText);

            PlayerGameScore = Intent.Extras.GetInt("playerGameScore");
            DealerGameScore = Intent.Extras.GetInt("dealerGameScore");


            buttonHome.Click += ButtonHome_CLick;

            SetWinnerText();
        }

        private void ButtonHome_CLick(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            intent.SetFlags(ActivityFlags.ReorderToFront);
            StartActivity(intent);
            Finish();
        }

        private void SetWinnerText()
        {
            if (PlayerGameScore > DealerGameScore)
            {
                winner.Text = "Player wins!";
            }
            else 
            {
                winner.Text = "Dealer wins!";
            }
        }
    }
}