using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZdfFlatUI.CstmControls.Marquee
{
    /// <summary>
    /// 按照步骤 1a 或 1b 操作，然后执行步骤 2 以在 XAML 文件中使用此自定义控件。
    ///
    /// 步骤 1a) 在当前项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:ZdfFlatUI.CstmControls.Marquee"
    ///
    ///
    /// 步骤 1b) 在其他项目中存在的 XAML 文件中使用该自定义控件。
    /// 将此 XmlNamespace 特性添加到要使用该特性的标记文件的根 
    /// 元素中: 
    ///
    ///     xmlns:MyNamespace="clr-namespace:ZdfFlatUI.CstmControls.Marquee;assembly=ZdfFlatUI.CstmControls.Marquee"
    ///
    /// 您还需要添加一个从 XAML 文件所在的项目到此项目的项目引用，
    /// 并重新生成以避免编译错误: 
    ///
    ///     在解决方案资源管理器中右击目标项目，然后依次单击
    ///     “添加引用”->“项目”->[浏览查找并选择此项目]
    ///
    ///
    /// 步骤 2)
    /// 继续操作并在 XAML 文件中使用控件。
    ///
    ///     <MyNamespace:MarqueeText/>
    ///
    /// </summary>
    public class MarqueeText : Control
    {
        private Canvas mContainer = null;
        private TextBlock mMarqueer = null;
        private bool mIsMarqueerOn = false;
        private MarqueeType mMarqueeType = MarqueeType.RightToLeft;
        private Storyboard mStoryboard = null;

        static MarqueeText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarqueeText), new FrameworkPropertyMetadata(typeof(MarqueeText)));
        }

        public MarqueeText()
        {
        }

        public override void OnApplyTemplate()
        {
            mContainer = GetTemplateChild("Container") as Canvas;
            mMarqueer = GetTemplateChild("Marqueer") as TextBlock;

            base.OnApplyTemplate();

            if (null == mContainer)
            {
                return;
            }
            mContainer.Height = this.Height;
            mContainer.Width = this.Width;

            if (null == mMarqueer)
            {
                return;
            }
            mMarqueer.LayoutUpdated += OnMarqueerLayoutUpdated;

            this.Loaded += new RoutedEventHandler(OnLoaded);
        }

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            double offset = 0;
            if ((MarqueeType.RightToLeft == mMarqueeType) || (MarqueeType.LeftToRight == mMarqueeType))
            {
                offset = mContainer.ActualHeight - mMarqueer.ActualHeight;
                mMarqueer.Margin = new Thickness(0, offset / 2.0, 0, 0);
            }
            else if ((MarqueeType.TopToBottom == mMarqueeType) || (MarqueeType.BottomToTop == mMarqueeType))
            {
                offset = mContainer.ActualWidth - mMarqueer.ActualWidth;
                mMarqueer.Margin = new Thickness(offset / 2.0, 0, 0, 0);
            }

            if (AlwaysRun)
            {
                StartMarqueeing(mMarqueeType);
            }
        }

        public MarqueeType MarqueeType
        {
            get { return mMarqueeType; }
            set { mMarqueeType = value; }
        }

        public bool AlwaysRun { get; set; }

        public static readonly DependencyProperty MarqueeContentProperty = DependencyProperty.Register("MarqueeContent", typeof(string), typeof(MarqueeText), new PropertyMetadata(""));
        public string MarqueeContent
        {
            get
            {
                return (string)GetValue(MarqueeContentProperty);
            }
            set
            {
                SetValue(MarqueeContentProperty, value);
            }
        }

        private void OnMarqueerLayoutUpdated(object sender, EventArgs e)
        {
            if (!mIsMarqueerOn)
            {
                if ((mMarqueer.ActualWidth >= mContainer.ActualWidth) || (mMarqueer.ActualHeight >= mContainer.ActualHeight))
                {
                    StartMarqueeing(mMarqueeType);
                }
            }
            else
            {
                if ((mMarqueer.ActualWidth >= mContainer.ActualWidth) || (mMarqueer.ActualHeight >= mContainer.ActualHeight))
                {
                }
                else
                {
                    StopMarqueeing();
                }
            }
        }


        private double _marqueeDuration;
        public double MarqueeDuration
        {
            get { return _marqueeDuration; }
            set { _marqueeDuration = value; }
        }

        public void StartMarqueeing(MarqueeType marqueeType)
        {
            mIsMarqueerOn = true;
            mStoryboard = new Storyboard();

            if (MarqueeType.LeftToRight == marqueeType)
            {
                LeftToRightMarquee();
            }
            else if (MarqueeType.RightToLeft == marqueeType)
            {
                RightToLeftMarquee();
            }
            else if (MarqueeType.TopToBottom == marqueeType)
            {
                TopToBottomMarquee();
            }
            else if (MarqueeType.BottomToTop == marqueeType)
            {
                BottomToTopMarquee();
            }
            else
            {
                mIsMarqueerOn = false;
                mStoryboard.Stop();
                mStoryboard = null;
            }
        }

        public void StopMarqueeing()
        {
            if (AlwaysRun)
            {
                return;
            }

            mIsMarqueerOn = false;
            mStoryboard.Stop();
            while (0 < mStoryboard.Children.Count)
            {
                mStoryboard.Children.RemoveAt(0);
            }
            mStoryboard = null;
        }

        private void LeftToRightMarquee()
        {
            double height = mContainer.ActualHeight - mMarqueer.ActualHeight;
            mMarqueer.Margin = new Thickness(0, height / 2.0, 0, 0);

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = -mMarqueer.ActualWidth;
            doubleAnimation.To = mContainer.ActualWidth;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(_marqueeDuration));
            //mMarqueer.BeginAnimation(Canvas.LeftProperty, mDoubleAnimation);

            Storyboard.SetTarget(doubleAnimation, mMarqueer);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Canvas.LeftProperty));
            mStoryboard.Children.Add(doubleAnimation);
            mStoryboard.Begin();
        }

        private void RightToLeftMarquee()
        {
            double height = mContainer.ActualHeight - mMarqueer.ActualHeight;
            mMarqueer.Margin = new Thickness(0, height / 2.0, 0, 0);

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = -mMarqueer.ActualWidth;
            doubleAnimation.To = mContainer.ActualWidth;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(_marqueeDuration));
            //mMarqueer.BeginAnimation(Canvas.RightProperty, mDoubleAnimation);

            Storyboard.SetTarget(doubleAnimation, mMarqueer);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Canvas.RightProperty));
            mStoryboard.Children.Add(doubleAnimation);
            mStoryboard.Begin();
        }

        private void TopToBottomMarquee()
        {
            double width = mContainer.ActualWidth - mMarqueer.ActualWidth;
            mMarqueer.Margin = new Thickness(width / 2.0, 0, 0, 0);

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = -mMarqueer.ActualHeight;
            doubleAnimation.To = mContainer.ActualHeight;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(_marqueeDuration));
            //mMarqueer.BeginAnimation(Canvas.TopProperty, mDoubleAnimation);

            Storyboard.SetTarget(doubleAnimation, mMarqueer);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Canvas.TopProperty));
            mStoryboard.Children.Add(doubleAnimation);
            mStoryboard.Begin();
        }

        private void BottomToTopMarquee()
        {
            double width = mContainer.ActualWidth - mMarqueer.ActualWidth;
            mMarqueer.Margin = new Thickness(width / 2.0, 0, 0, 0);

            DoubleAnimation doubleAnimation = new DoubleAnimation();
            doubleAnimation.From = -mMarqueer.ActualHeight;
            doubleAnimation.To = mContainer.ActualHeight;
            doubleAnimation.RepeatBehavior = RepeatBehavior.Forever;
            doubleAnimation.Duration = new Duration(TimeSpan.FromSeconds(_marqueeDuration));
            //mMarqueer.BeginAnimation(Canvas.BottomProperty, mDoubleAnimation);

            Storyboard.SetTarget(doubleAnimation, mMarqueer);
            Storyboard.SetTargetProperty(doubleAnimation, new PropertyPath(Canvas.BottomProperty));
            mStoryboard.Children.Add(doubleAnimation);
            mStoryboard.Begin();
        }
    }

    public enum MarqueeType
    {
        LeftToRight,
        RightToLeft,
        TopToBottom,
        BottomToTop
    }
}
