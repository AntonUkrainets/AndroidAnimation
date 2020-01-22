using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Com.Airbnb.Lottie;
using Android.Animation;
using Android.Content;
using Android.Views.Animations;
using Xamarin.Essentials;
using Android.Views;

using System;
using static AnimationAndroid.Resource.Id;
using static AnimationAndroid.Resource.Drawable;

namespace AnimationAndroid
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, Animator.IAnimatorListener
    {
        private Button _navigationActivityButton;

        public void OnAnimationStart(Animator animation) { }

        public void OnAnimationRepeat(Animator animation) { }

        public void OnAnimationCancel(Animator animation) { }

        public void OnAnimationEnd(Animator animation)
        {
            var _alphaAnimation = new AlphaAnimation(fromAlpha: 0.0f, toAlpha: 1.0f)
            {
                Duration = 10000//10 sec
            };

            _navigationActivityButton.StartAnimation(_alphaAnimation);
            _navigationActivityButton.Visibility = ViewStates.Visible;
        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Platform.Init(this, savedInstanceState);

            SetContentView(Resource.Layout.activity_main);
            InitControlls();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _navigationActivityButton.Click -= NextButton_Clicked;
        }

        private void NextButton_Clicked(object sender, EventArgs e)
        {
            StartAnimationActivity();

            OverridePendingTransition(EnterAnimation, ExitAnimation);
        }

        private void StartAnimationActivity()
        {
            var animationIntent = new Intent(this, typeof(AnimationActivity));
            StartActivity(animationIntent);
        }

        private void InitControlls()
        {
            FindViewById<LottieAnimationView>(MainAnimationView).AddAnimatorListener(this);

            _navigationActivityButton = FindViewById<Button>(NextNavigationViewButton);
            _navigationActivityButton.Click += NextButton_Clicked;
        }
    }
}