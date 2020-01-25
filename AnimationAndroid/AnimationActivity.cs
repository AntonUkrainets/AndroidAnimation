using Android.App;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Support.Animation;
using Android.Util;
using Android.Views.Animations;
using Android.Widget;

using System;
using static AnimationAndroid.Resource.Id;
using static AnimationAndroid.Resource.Drawable;

namespace AnimationAndroid
{
    [Activity(Label = "AnimationActivity")]
    public class AnimationActivity : Activity
    {
        private Button _startRotation;
        private Button _endRotation;

        private Button _startScale;
        private Button _endScale;

        private Button _startShake;
        private Button _endShake;

        private Button _startColor;
        private Button _endColor;

        private Button _startSprings;

        private ImageView _calendarImageView;
        private ImageView _heartImageView;
        private ImageView _buddiesImageView;
        private ImageView _gradientImageView;
        private ImageView _buddyImageView;

        private AnimationDrawable _animation;
        private Animation _animationScale;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_animation);

            CreateControls();
            FillControlls();
        }

        private void StartRotationButtonClick(object sender, EventArgs e)
        {
            var rotateAnimation = new RotateAnimation(
                        fromDegrees: 0,
                        toDegrees: 359,
                        pivotXType: Dimension.RelativeToSelf,
                        pivotXValue: 0.5f,
                        pivotYType: Dimension.RelativeToSelf, 
                        pivotYValue: 0.5f)
            {
                Duration = 1000, // 1 second
                FillAfter = false,
                RepeatCount = Animation.StartOnFirstFrame,
                Interpolator = new LinearInterpolator()
            };

            _calendarImageView.StartAnimation(rotateAnimation);
        }

        private void StartScaleButtonClick(object sender, EventArgs e)
        {
            _animationScale = AnimationUtils.LoadAnimation(this, skale);
            _heartImageView.StartAnimation(_animationScale);
        }

        private void StartShakeButtonClick(object sender, EventArgs e)
        {
            var animationShake = AnimationUtils.LoadAnimation(this, shake);
            _buddiesImageView.StartAnimation(animationShake);
        }

        private void StartColorButtonClick(object sender, EventArgs e)
        {
            _animation = (AnimationDrawable)_gradientImageView.Background;
            _animation.SetEnterFadeDuration(ms: 1000);
            _animation.SetExitFadeDuration(ms :2000);
            _animation.Start();          
        }

        private void StartSpringsButtonClick(object sender, EventArgs e)
        {
            var springAnimation = new SpringAnimation(_buddyImageView, DynamicAnimation.TranslationY, finalPosition: 0);
            springAnimation.Spring.SetStiffness(SpringForce.StiffnessLow);
            springAnimation.Spring.SetDampingRatio(SpringForce.DampingRatioHighBouncy);
            springAnimation.SetStartVelocity(TranslateDpToPx(dp: -1000));
            springAnimation.Start();
        }

        private float TranslateDpToPx(float dp)
                        => TypedValue.ApplyDimension(ComplexUnitType.Dip, dp, Resources.DisplayMetrics);

        private void StopButtonsClick(object sender, EventArgs e)
        {
            _calendarImageView.ClearAnimation();
            _heartImageView.ClearAnimation();
            _buddiesImageView.ClearAnimation();

            _animation?.Stop();
        }

        private void FillControlls()
        {
            _startRotation.Click += StartRotationButtonClick;
            _endRotation.Click += StopButtonsClick;

            _startScale.Click += StartScaleButtonClick;
            _endScale.Click += StopButtonsClick;

            _startShake.Click += StartShakeButtonClick;
            _endShake.Click += StopButtonsClick;

            _startColor.Click += StartColorButtonClick;
            _endColor.Click += StopButtonsClick;

            _startSprings.Click += StartSpringsButtonClick;
        }

        private void CreateControls()
        {      
            _startRotation = FindViewById<Button>(StartRotation);
            _endRotation = FindViewById<Button>(StopRotation);

            _startScale = FindViewById<Button>(StartScale);
            _endScale = FindViewById<Button>(StopScale);

            _startShake = FindViewById<Button>(StartShake);
            _endShake = FindViewById<Button>(StopShake);

            _startColor = FindViewById<Button>(StartColor);
            _endColor = FindViewById<Button>(StopColor);

            _startSprings = FindViewById<Button>(StartSprings);

            _calendarImageView = FindViewById<ImageView>(CalendarImageView);
            _heartImageView = FindViewById<ImageView>(HeartImageView);
            _buddiesImageView = FindViewById<ImageView>(BuddiesImageView);
            _gradientImageView = FindViewById<ImageView>(GradientImageView);
            _buddyImageView = FindViewById<ImageView>(BuddyImageView);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            _startRotation.Click -= StartRotationButtonClick;
            _endRotation.Click -= StopButtonsClick;

            _startScale.Click -= StartScaleButtonClick;
            _endScale.Click -= StopButtonsClick;

            _startShake.Click -= StartShakeButtonClick;
            _endShake.Click -= StopButtonsClick;

            _startColor.Click -= StartColorButtonClick;
            _endColor.Click -= StopButtonsClick;

            _startSprings.Click -= StartSpringsButtonClick;
        }
    }
}