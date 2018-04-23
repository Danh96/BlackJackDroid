﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using DeckOfCards;

namespace BlackJack
{
    [Activity(Label = "Game", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen", ScreenOrientation = ScreenOrientation.Portrait)]
    public class GameActivity : Activity
    {
        private PlayingCardDeck Deck;
        private List<Card> PlayersHand = new List<Card>();
        private List<Card> DealerHand = new List<Card>();
        private int DealersHandTotal;
        private int PlayersHandTotal;
        private int DealerGameScore;
        private int PlayerGameScore;

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

            //TODO Only allow for 5 cards to be drawn
            GameStart();
        }

        private async void HitButton_Click(object sender, EventArgs e)
        {
            PlayersHand.Add(Deck.RemoveTopCard());
            PrintPlayerHand(PlayersHand);
            PlayersHandTotal = UpdateScore(PlayersHand);
            playersHandText.Text = "Your hand total: " + PlayersHandTotal.ToString();

            await CheckIfBust();
        }

        private async void StickButton_Click(object sender, EventArgs e)
        {
            buttonHit.Enabled = false;
            buttonStick.Enabled = false;

            await DealersTurn();
        }

        private void GameStart()
        {
            Deck = new PlayingCardDeck();

            Deck.Shuffle();

            PlayersHandTotal = 0;
            PlayersHand.Clear();

            DealersHandTotal = 0;
            DealerHand.Clear();

            buttonHit.Enabled = true;
            buttonStick.Enabled = true;

            playerGameScoreText.Text = "Your score: " + PlayerGameScore.ToString();
            dealerGameScoreText.Text = "Dealer score: " + DealerGameScore.ToString();

            dealersFirstCard.Visibility = ViewStates.Invisible;
            dealersSecondCard.Visibility = ViewStates.Invisible;
            dealersThirdCard.Visibility = ViewStates.Invisible;
            dealersFourthCard.Visibility = ViewStates.Invisible;
            dealersFifthCard.Visibility = ViewStates.Invisible;

            playersThirdCard.Visibility = ViewStates.Invisible;
            playersFourthCard.Visibility = ViewStates.Invisible;
            playersFifthCard.Visibility = ViewStates.Invisible;

            PlayersHand.Add(Deck.RemoveTopCard());
            DealerHand.Add(Deck.RemoveTopCard());
            PlayersHand.Add(Deck.RemoveTopCard());
            DealerHand.Add(Deck.RemoveTopCard());

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
                        dealersFirstCard.SetCardValues(DealerHand[count - 1]);
                        dealersFirstCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                    case 2:
                        dealersSecondCard.SetCardValues(DealerHand[count - 1]);
                        dealersSecondCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                    case 3:
                        dealersThirdCard.SetCardValues(DealerHand[count - 1]);
                        dealersThirdCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                    case 4:
                        dealersFourthCard.SetCardValues(DealerHand[count - 1]);
                        dealersFourthCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                    case 5:
                        dealersFifthCard.SetCardValues(DealerHand[count - 1]);
                        dealersFifthCard.Visibility = ViewStates.Visible;
                        count--;
                        break;
                }
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

            convoText.Text = "Dealers turn";
            await Task.Delay(1000);

            PrintDealersHand(DealerHand);
            DealersHandTotal = UpdateScore(DealerHand);
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
                    DealerHand.Add(Deck.RemoveTopCard());
                    PrintDealersHand(DealerHand);
                    DealersHandTotal = UpdateScore(DealerHand);
                    dealersHandText.Text = "Dealers hand total: " + DealersHandTotal;
                    await CheckIfBust();
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
                playersHandText.Text = "Your hand total: Bust!";
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
                playerGameScoreText.Text = "Your score: " + PlayerGameScore.ToString();
                convoText.Text = "Player wins hand.";
            }
            else if (PlayersHandTotal < DealersHandTotal)
            {
                DealerGameScore++;
                dealerGameScoreText.Text = "Dealer score: " + DealerGameScore.ToString();
                convoText.Text = "Dealer wins hand.";
            }
            else if (PlayersHandTotal == DealersHandTotal)
            {
                DealerGameScore++;
                convoText.Text = "Draw, points go to dealer.";
            }

            await Task.Delay(1000);
            await CheckIfGameContinues();
        }

        private async Task CheckIfGameContinues()
        {
            if (PlayerGameScore == 3 || DealerGameScore == 3)
            {
                Intent intent = new Intent(this, typeof(EndGameActivity));
                intent.PutExtra("playerGameScore", PlayerGameScore);
                intent.PutExtra("dealerGameScore", DealerGameScore);
                StartActivity(intent);
                Finish();
            }
            else
            {
                await Task.Delay(1000);
                convoText.Text = "Next Round!";
                GameStart();
            }
        }
    }
}