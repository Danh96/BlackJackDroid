using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Views;
using Android.Widget;
using DeckOfCards;

namespace BlackJack
{
    [Activity(Label = "Game", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen", ScreenOrientation = ScreenOrientation.Portrait, NoHistory = true)]
    public class GameActivity : Activity
    {
        private MediaPlayer _player = new MediaPlayer();
        private PlayingCardDeck Deck;
        private List<Card> PlayersHand = new List<Card>();
        private List<Card> DealersHand = new List<Card>();
        private int DealersHandTotal;
        private int PlayersHandTotal;
        private int DealerGameScore;
        private int PlayerGameScore;
        private int MaxMatchPoint;

        private TextView playerGameScoreText;
        private TextView dealerGameScoreText;
        private TextView playersHandText;
        private TextView dealersHandText;
        private TextView convoText;

        private Button buttonStick;
        private Button buttonHit;

        private CardView dealersFirstCard;
        private CardView dealersSecondCard;
        private CardView dealersThirdCard;
        private CardView dealersFourthCard;
        private CardView dealersFifthCard;

        private CardView playersFirstCard;
        private CardView playersSecondCard;
        private CardView playersThirdCard;
        private CardView playersFourthCard;
        private CardView playersFifthCard;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Game);

            playerGameScoreText = FindViewById<TextView>(Resource.Id.PlayerGameScore);
            dealerGameScoreText = FindViewById<TextView>(Resource.Id.DealerGameScore);
            playersHandText = FindViewById<TextView>(Resource.Id.PlayersHandText);
            dealersHandText = FindViewById<TextView>(Resource.Id.DealersHandText);
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

            buttonStick = FindViewById<Button>(Resource.Id.ButtonStick);
            buttonHit = FindViewById<Button>(Resource.Id.ButtonHit);

            buttonStick.Click += StickButton_Click;
            buttonHit.Click += HitButton_Click;

            SetCardsToInvisible();
            SelectMatchPointsDialogPopUp();
        }

        private async void HitButton_Click(object sender, EventArgs e)
        {
            PlayersHand.Add(Deck.RemoveTopCard());
            PrintPlayerHand(PlayersHand);
            PlayersHandTotal = UpdateScore(PlayersHand);
            playersHandText.Text = "Players hand total: " + PlayersHandTotal.ToString();

            await CheckIfBust();
            await CheckIfPlayerHasFiveCardTrick();
        }

        private async void StickButton_Click(object sender, EventArgs e)
        {
            buttonHit.Enabled = false;
            buttonStick.Enabled = false;

            await DealersTurn();
        }

        private void SetCardsToInvisible()
        {
            dealersFirstCard.Visibility = ViewStates.Invisible;
            dealersSecondCard.Visibility = ViewStates.Invisible;
            dealersThirdCard.Visibility = ViewStates.Invisible;
            dealersFourthCard.Visibility = ViewStates.Invisible;
            dealersFifthCard.Visibility = ViewStates.Invisible;

            playersFirstCard.Visibility = ViewStates.Invisible;
            playersSecondCard.Visibility = ViewStates.Invisible;
            playersThirdCard.Visibility = ViewStates.Invisible;
            playersFourthCard.Visibility = ViewStates.Invisible;
            playersFifthCard.Visibility = ViewStates.Invisible;
        }

        private void GameStart()
        {
            Deck = new PlayingCardDeck();

            Deck.Shuffle();

            PlayersHandTotal = 0;
            PlayersHand.Clear();

            DealersHandTotal = 0;
            DealersHand.Clear();

            buttonHit.Enabled = true;
            buttonStick.Enabled = true;

            playerGameScoreText.Text = "Players score: " + PlayerGameScore.ToString();
            dealerGameScoreText.Text = "Dealers score: " + DealerGameScore.ToString();

            dealersFirstCard.Visibility = ViewStates.Visible;
            dealersSecondCard.Visibility = ViewStates.Visible;
            dealersThirdCard.Visibility = ViewStates.Invisible;
            dealersFourthCard.Visibility = ViewStates.Invisible;
            dealersFifthCard.Visibility = ViewStates.Invisible;

            dealersFirstCard.SetDealerCardFaceDown();
            dealersSecondCard.SetDealerCardFaceDown();

            playersThirdCard.Visibility = ViewStates.Invisible;
            playersFourthCard.Visibility = ViewStates.Invisible;
            playersFifthCard.Visibility = ViewStates.Invisible;

            PlayersHand.Add(Deck.RemoveTopCard());
            DealersHand.Add(Deck.RemoveTopCard());
            PlayersHand.Add(Deck.RemoveTopCard());
            DealersHand.Add(Deck.RemoveTopCard());

            dealersHandText.Text = "Dealers hand total: " + DealersHandTotal;

            PrintPlayerHand(PlayersHand);

            PlayersHandTotal = UpdateScore(PlayersHand);
            playersHandText.Text = "Players hand total: " + PlayersHandTotal.ToString();

            convoText.Text = "Players turn";
        }

        private void PrintPlayerHand(List<Card> hand)
        {
            var count = hand.Count();

            foreach (Card card in hand)
            {
                switch (count)
                {
                    case 1:
                        playersFirstCard.SetCardValues(PlayersHand[count - 1]);
                        playersFirstCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                    case 2:
                        playersSecondCard.SetCardValues(PlayersHand[count - 1]);
                        playersSecondCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                    case 3:
                        playersThirdCard.SetCardValues(PlayersHand[count - 1]);
                        playersThirdCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                    case 4:
                        playersFourthCard.SetCardValues(PlayersHand[count - 1]);
                        playersFourthCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                    case 5:
                        playersFifthCard.SetCardValues(PlayersHand[count - 1]);
                        playersFifthCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                }
            }
        }

        private void PrintDealersHand(List<Card> hand)
        {
            var count = hand.Count();

            foreach (Card card in hand)
            {
                switch (count)
                {
                    case 1:
                        dealersFirstCard.SetDealerCardFaceDown(false);
                        dealersFirstCard.SetCardValues(DealersHand[count - 1]);
                        dealersFirstCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                    case 2:
                        dealersSecondCard.SetDealerCardFaceDown(false);
                        dealersSecondCard.SetCardValues(DealersHand[count - 1]);
                        dealersSecondCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                    case 3:
                        dealersThirdCard.SetCardValues(DealersHand[count - 1]);
                        dealersThirdCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                    case 4:
                        dealersFourthCard.SetCardValues(DealersHand[count - 1]);
                        dealersFourthCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                    case 5:
                        dealersFifthCard.SetCardValues(DealersHand[count - 1]);
                        dealersFifthCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                }
            }
        }

        private async Task CheckIfPlayerHasFiveCardTrick()
        {
            if (PlayersHand.Count == 5 && PlayersHandTotal != -1)
            {
                buttonHit.Enabled = false;
                buttonStick.Enabled = false;
                playersHandText.Text = "Players hand total: Five cards under!";
                PlayersHandTotal = 100;
                await DealersTurn();
            }
        }

        private void CheckIfDealerHasFiveCardTrick()
        {
            if (DealersHand.Count == 5 && DealersHandTotal != -1)
            {
                dealersHandText.Text = "Dealers hand total: Five cards under!";
                DealersHandTotal = 100;
            }
        }

        private int UpdateScore(List<Card> hand)
        {
            int HandTotal = 0;
            List<Card> aces = new List<Card>();

            foreach (Card c in hand)
            {
                if (c.Value == 1)
                {
                    aces.Add(c);
                }
                else if (c.Value > 10)
                {
                    HandTotal += 10;
                }
                else
                {
                    HandTotal += c.Value;
                }
            }

            return SetAceValue(aces, HandTotal);
        }

        private int SetAceValue(List<Card> aces, int total)
        {
            int HandTotal = total;

            foreach (Card c in aces)
            {
                if (HandTotal + 11 > 21)
                {
                    HandTotal++;
                }
                else
                {
                    HandTotal += 11;
                }
            }

            return HandTotal;
        }

        private async Task DealersTurn()
        {
            bool dealersTurn = true;

            await Task.Delay(1000);
            convoText.Text = "Dealers turn";
            await Task.Delay(2000);

            PrintDealersHand(DealersHand);
            DealersHandTotal = UpdateScore(DealersHand);
            dealersHandText.Text = "Dealers hand total: " + DealersHandTotal;

            while (dealersTurn)
            {
                if (DealersHandTotal > PlayersHandTotal || DealersHandTotal == -1)
                {
                    dealersTurn = false;
                }
                else if (DealersHandTotal <= 16)
                {
                    await Task.Delay(1000);
                    DealersHand.Add(Deck.RemoveTopCard());
                    PrintDealersHand(DealersHand);
                    DealersHandTotal = UpdateScore(DealersHand);
                    dealersHandText.Text = "Dealers hand total: " + DealersHandTotal;
                    await CheckIfBust();
                    CheckIfDealerHasFiveCardTrick();
                }
                else
                {
                    dealersTurn = false;
                }

                await Task.Delay(1000);
            }

            await UpdateGameScore();
        }

        private async Task CheckIfBust()
        {
            if (PlayersHandTotal > 21)
            {
                PlayersHandTotal = -1;
                playersHandText.Text = "Players hand total: Bust!";
                buttonHit.Enabled = false;
                buttonStick.Enabled = false;
                await DealersTurn();
            }

            if (DealersHandTotal > 21)
            {
                DealersHandTotal = -1;
                dealersHandText.Text = "Dealers hand total: Bust!";
            }
        }

        private async Task UpdateGameScore()
        {
            if (PlayersHandTotal > DealersHandTotal)
            {
                PlayerGameScore++;
                playerGameScoreText.Text = "Players score: " + PlayerGameScore.ToString();
                convoText.Text = "Players hand wins.";
            }
            else if (PlayersHandTotal < DealersHandTotal)
            {
                DealerGameScore++;
                dealerGameScoreText.Text = "Dealers score: " + DealerGameScore.ToString();
                convoText.Text = "Dealers hand wins.";
            }
            else if (PlayersHandTotal == DealersHandTotal)
            {
                DealerGameScore++;
                convoText.Text = "Draw, points go to dealer.";
            }

            await Task.Delay(2000);
            await CheckIfGameContinues();
        }

        private async Task CheckIfGameContinues()
        {
            if (PlayerGameScore == MaxMatchPoint || DealerGameScore == MaxMatchPoint)
            {
                Intent intent = new Intent(this, typeof(EndGameActivity));
                intent.PutExtra("playerGameScore", PlayerGameScore);
                intent.PutExtra("dealerGameScore", DealerGameScore);
                StartActivity(intent);
                Finish();
            }
            else
            {
                convoText.Text = "Next Round!";
                StartShufflePlayer();
                await Task.Delay(1000);
                GameStart();
            }
        }

        private void SelectMatchPointsDialogPopUp()
        {
            var MatchPointAlert = (new AlertDialog.Builder(this)).Create();
            MatchPointAlert.SetCancelable(false);
            MatchPointAlert.SetMessage("Please select the number of points you want to play for.");
            MatchPointAlert.SetTitle("Match length selector");
            MatchPointAlert.SetButton("10 points", SetMatchPointsToTen);
            MatchPointAlert.SetButton2("3 points", SetMatchPointsToThree);
            MatchPointAlert.SetButton3("5 points", SetMatchPointsToFive);
            MatchPointAlert.Show();

            StyleAlertDialog(MatchPointAlert);
        }

        private static void StyleAlertDialog(Dialog dialog)
        {
            try
            {
                var resources = dialog.Context.Resources;

                var alertTitleId = resources.GetIdentifier("alertTitle", "id", "android");
                var alertTitle = (TextView)dialog.Window.DecorView.FindViewById(alertTitleId);
                alertTitle.SetTextColor(Android.Graphics.Color.Red);

                var titleDividerId = resources.GetIdentifier("titleDivider", "id", "android");
                var titleDivider = dialog.Window.DecorView.FindViewById(titleDividerId);
                titleDivider.SetBackgroundColor(Android.Graphics.Color.Red);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private void SetMatchPointsToTen(object sender, DialogClickEventArgs e)
        {
            MaxMatchPoint = 10;
            StartShufflePlayer();
            GameStart();
        }

        private void SetMatchPointsToFive(object sender, DialogClickEventArgs e)
        {
            MaxMatchPoint = 5;
            StartShufflePlayer();
            GameStart();
        }

        private void SetMatchPointsToThree(object sender, DialogClickEventArgs e)
        {
            MaxMatchPoint = 3;
            StartShufflePlayer();
            GameStart();
        }

        private void StartShufflePlayer()
        {
            _player = MediaPlayer.Create(this, Resource.Raw.ShuffleSound);
            _player.Start();
        }

        public override void OnBackPressed()
        {
            Finish();
        }
    }
}