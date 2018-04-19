using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using DeckOfCards;

namespace BlackJack
{
    [Activity(Label = "Game", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class GameActivity : Activity
    {
        private PlayingCardDeck Deck;
        private List<Card> PlayersHand = new List<Card>();
        private List<Card> DealerHand = new List<Card>();
        private int DealersHandTotal;
        private int PlayersHandTotal;
        private int DealerGameScore;
        private int PlayerGameScore;

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
            PlayersHand.Add(Deck.RemoveTopCard());
            //TODO implement a way to print card values
            //PrintHand(PlayersHand);
            PlayersHandTotal = UpdateScore(PlayersHand);
            //TODO implement a way to update the players score in the score bar
            //PrintPlayerScore();
        }

        private void StickButton_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void NewGame()
        {
            Deck = new PlayingCardDeck();

            Deck.Shuffle();

            PlayersHandTotal = 0;
            PlayersHand.Clear();

            DealersHandTotal = 0;
            DealerHand.Clear();

            PlayersHand.Add(Deck.RemoveTopCard());
            DealerHand.Add(Deck.RemoveTopCard());
            PlayersHand.Add(Deck.RemoveTopCard());
            DealerHand.Add(Deck.RemoveTopCard());

            DealersHandTotal = UpdateScore(DealerHand);

            //TODO implement a way to print card values
            //PrintHand(PlayersHand);

            PlayersHandTotal = UpdateScore(PlayersHand);

            //TODO implement a way to update the players score in the score bar
            //PrintPlayerScore();
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
                if (total + 11 > 21)
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

        private void DealersTurn()
        {
            bool dealersTurn = true;

            while (dealersTurn)
            {
                if (DealersHandTotal > PlayersHandTotal || DealersHandTotal == -1)
                {
                    dealersTurn = false;
                }
                else if (DealersHandTotal <= 16)
                {
                    DealerHand.Add(Deck.RemoveTopCard());
                    DealersHandTotal = UpdateScore(DealerHand);
                    CheckIfBust();
                }
                else
                {
                    dealersTurn = false;
                }
            }
        }

        private void CheckIfBust()
        {
            if (PlayersHandTotal > 21)
            {
                PlayersHandTotal = -1;
            }

            if (DealersHandTotal > 21)
            {
                DealersHandTotal = -1;
            }
        }
    }
}