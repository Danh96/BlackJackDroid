using Android.App;
using Android.OS;

namespace BlackJack
{
    [Activity(Label = "Game", Theme = "@android:style/Theme.Holo.NoActionBar.Fullscreen")]
    public class GameActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Game);
        }
    }
}
