﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace ZdfFlatUI
{
    public class FloatingActionMenu : ItemsControl
    {
        #region const string
        public const string PopupPartName = "PART_Popup";
        #endregion

        #region private fields
        private Popup PART_Popup;
        private ToggleButton PART_ToggleButton;
        #endregion

        #region DependencyProperty

        #region IsDropdownOpen

        public bool IsDropDownOpen
        {
            get { return (bool)GetValue(IsDropDownOpenProperty); }
            set { SetValue(IsDropDownOpenProperty, value); }
        }
        
        public static readonly DependencyProperty IsDropDownOpenProperty =
            DependencyProperty.Register("IsDropDownOpen", typeof(bool), typeof(FloatingActionMenu), new PropertyMetadata(false, IsDropDownOpenChangedCallback));

        private static void IsDropDownOpenChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FloatingActionMenu menu = d as FloatingActionMenu;
            bool flag;
            if(bool.TryParse(Convert.ToString(e.NewValue), out flag))
            {
                if(flag)
                {
                    menu.AnimateChildOpen();
                    VisualStateManager.GoToState(menu, "PopupOpen", true);
                }
                else
                {
                    menu.AnimateChildClose();
                }
            }
        }

        #endregion

        #region DisplayTipContentMemberPath

        /// <summary>
        /// 获取或者设置浮动按钮的提示内容的属性名称
        /// </summary>
        public string DisplayTipContentMemberPath
        {
            get { return (string)GetValue(DisplayTipContentMemberPathProperty); }
            set { SetValue(DisplayTipContentMemberPathProperty, value); }
        }
        
        public static readonly DependencyProperty DisplayTipContentMemberPathProperty =
            DependencyProperty.Register("DisplayTipContentMemberPath", typeof(string), typeof(FloatingActionMenu));

        #endregion

        #region Trigger

        /// <summary>
        /// 获取或者设置按钮组的弹出方式
        /// </summary>
        public EnumTrigger Trigger
        {
            get { return (EnumTrigger)GetValue(TriggerProperty); }
            set { SetValue(TriggerProperty, value); }
        }
        
        public static readonly DependencyProperty TriggerProperty =
            DependencyProperty.Register("Trigger", typeof(EnumTrigger), typeof(FloatingActionMenu), new PropertyMetadata(EnumTrigger.Click));

        #endregion

        #region PlacementDirection
        /// <summary>
        /// 获取或者设置按钮组的弹出位置(共有左、上、右、下四个方向)
        /// </summary>
        public EnumPlacementDirection PlacementDirection
        {
            get { return (EnumPlacementDirection)GetValue(PlacementDirectionProperty); }
            set { SetValue(PlacementDirectionProperty, value); }
        }
        
        public static readonly DependencyProperty PlacementDirectionProperty =
            DependencyProperty.Register("PlacementDirection", typeof(EnumPlacementDirection), typeof(FloatingActionMenu), new PropertyMetadata(EnumPlacementDirection.Top));

        #endregion

        #region ItemOrientation

        public Orientation ItemOrientation
        {
            get { return (Orientation)GetValue(ItemOrientationProperty); }
            set { SetValue(ItemOrientationProperty, value); }
        }
        
        public static readonly DependencyProperty ItemOrientationProperty =
            DependencyProperty.Register("ItemOrientation", typeof(Orientation), typeof(FloatingActionMenu), new PropertyMetadata(Orientation.Vertical));

        #endregion

        #endregion

        #region Events

        #region ItemClickEvent

        public static readonly RoutedEvent ItemClickEvent = EventManager.RegisterRoutedEvent("ItemClick",
            RoutingStrategy.Bubble, typeof(RoutedPropertyChangedEventHandler<object>), typeof(FloatingActionMenu));

        public event RoutedPropertyChangedEventHandler<object> ItemClick
        {
            add
            {
                this.AddHandler(ItemClickEvent, value);
            }
            remove
            {
                this.RemoveHandler(ItemClickEvent, value);
            }
        }

        public virtual void OnItemClick(object oldValue, object newValue)
        {
            RoutedPropertyChangedEventArgs<object> arg = new RoutedPropertyChangedEventArgs<object>(oldValue, newValue, ItemClickEvent);
            this.RaiseEvent(arg);
        }

        #endregion

        #endregion

        #region Constructors

        static FloatingActionMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingActionMenu), new FrameworkPropertyMetadata(typeof(FloatingActionMenu)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_Popup = this.GetTemplateChild(PopupPartName) as Popup;
            this.PART_ToggleButton = this.GetTemplateChild("PART_ToggleButton") as ToggleButton;
            if (this.PART_ToggleButton != null && this.Trigger == EnumTrigger.Hover)
            {
                this.MouseEnter += (o, e) =>
                {
                    this.PART_ToggleButton.IsChecked = true;
                };
                this.MouseLeave += (o, e) =>
                {
                    this.PART_ToggleButton.IsChecked = false;
                };
            }

            SetPopupPosition();

            this.ItemContainerGenerator.StatusChanged += ItemContainerGenerator_StatusChanged;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            FloatingActionButton floatingActionButton = element as FloatingActionButton;
            if(!string.IsNullOrEmpty(this.DisplayTipContentMemberPath))
            {
                Binding binding = new Binding(this.DisplayTipContentMemberPath);
                floatingActionButton.SetBinding(FloatingActionButton.TipContentProperty, binding);
            }
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new FloatingActionButton();
        }
        #endregion

        #region private function
        private void AnimateChild(bool reverse)
        {
            var sineEase = new SineEase();
            double translateCoordinateFrom = 0d;

            string direction = string.Empty;
            switch (this.PlacementDirection)
            {
                case EnumPlacementDirection.Left:
                    direction = "X";
                    translateCoordinateFrom = 80;
                    break;
                case EnumPlacementDirection.Right:
                    direction = "X";
                    translateCoordinateFrom = -80;
                    break;
                case EnumPlacementDirection.Top:
                    translateCoordinateFrom = 80;
                    direction = "Y";
                    break;
                case EnumPlacementDirection.Bottom:
                    translateCoordinateFrom = -80;
                    direction = "Y";
                    break;
            }
            
            var translateCoordinatePath = string.Format("(UIElement.RenderTransform).(TransformGroup.Children)[1].(TranslateTransform.{0})", direction);

            for (int i = 0; i < this.Items.Count; i++)
            {
                UIElement element = this.ItemContainerGenerator.ContainerFromIndex(i) as UIElement;

                if(element == null)
                {
                    continue;
                }

                var elementTranslateCoordinateFrom = translateCoordinateFrom * i;
                var translateTransform = new TranslateTransform(
                    this.ItemOrientation == Orientation.Horizontal ? translateCoordinateFrom : 0, 
                    this.ItemOrientation == Orientation.Horizontal ? 0 : translateCoordinateFrom);

                var transformGroup = new TransformGroup
                {
                    Children = new TransformCollection(new Transform[]
                    {
                        new ScaleTransform(0, 0),
                        translateTransform
                    })
                };
                element.SetCurrentValue(RenderTransformOriginProperty, new Point(.5, .5));
                element.RenderTransform = transformGroup;

                var deferredStart = i * 20;
                var deferredEnd = deferredStart + 150.0;

                var absoluteZeroKeyTime = KeyTime.FromPercent(0.0);
                var deferredStartKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(deferredStart));
                var deferredEndKeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(deferredEnd));

                var opacityAnimation = new DoubleAnimationUsingKeyFrames();
                opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, absoluteZeroKeyTime, sineEase));
                opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, deferredStartKeyTime, sineEase));
                opacityAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, deferredEndKeyTime, sineEase));
                Storyboard.SetTargetProperty(opacityAnimation, new PropertyPath("Opacity"));
                Storyboard.SetTarget(opacityAnimation, element);

                var scaleXAnimation = new DoubleAnimationUsingKeyFrames();
                scaleXAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, absoluteZeroKeyTime, sineEase));
                scaleXAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, deferredStartKeyTime, sineEase));
                scaleXAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, deferredEndKeyTime, sineEase));
                Storyboard.SetTargetProperty(scaleXAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleX)"));
                Storyboard.SetTarget(scaleXAnimation, element);

                var scaleYAnimation = new DoubleAnimationUsingKeyFrames();
                scaleYAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, absoluteZeroKeyTime, sineEase));
                scaleYAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, deferredStartKeyTime, sineEase));
                scaleYAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(1, deferredEndKeyTime, sineEase));
                Storyboard.SetTargetProperty(scaleYAnimation, new PropertyPath("(UIElement.RenderTransform).(TransformGroup.Children)[0].(ScaleTransform.ScaleY)"));
                Storyboard.SetTarget(scaleYAnimation, element);

                var translateCoordinateAnimation = new DoubleAnimationUsingKeyFrames();
                translateCoordinateAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(elementTranslateCoordinateFrom, absoluteZeroKeyTime, sineEase));
                translateCoordinateAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(elementTranslateCoordinateFrom, deferredStartKeyTime, sineEase));
                translateCoordinateAnimation.KeyFrames.Add(new EasingDoubleKeyFrame(0, deferredEndKeyTime, sineEase));

                Storyboard.SetTargetProperty(translateCoordinateAnimation, new PropertyPath(translateCoordinatePath));
                Storyboard.SetTarget(translateCoordinateAnimation, element);

                var storyboard = new Storyboard();
                storyboard.Children.Add(opacityAnimation);
                storyboard.Children.Add(scaleXAnimation);
                storyboard.Children.Add(scaleYAnimation);
                storyboard.Children.Add(translateCoordinateAnimation);

                if (reverse)
                {
                    storyboard.AutoReverse = true;
                    storyboard.Begin();
                    storyboard.Seek(TimeSpan.FromMilliseconds(deferredEnd));
                    storyboard.Resume();
                }
                else
                {
                    storyboard.Begin();
                }
            }
        }

        private void AnimateChildOpen()
        {
            this.AnimateChild(false);
        }

        private void AnimateChildClose()
        {
            this.AnimateChild(true);
        }

        private void ItemContainerGenerator_StatusChanged(object sender, EventArgs e)
        {
            if (this.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
            {
                this.AnimateChild(false);
            }
        }

        private void SetPopupPosition()
        {
            if (this.PART_Popup != null)
            {
                switch (this.PlacementDirection)
                {
                    case EnumPlacementDirection.Left:
                        this.PART_Popup.Placement = PlacementMode.Left;
                        this.PART_Popup.HorizontalOffset = 0;
                        this.PART_Popup.VerticalOffset = -10;
                        this.ItemOrientation = Orientation.Horizontal;
                        break;
                    case EnumPlacementDirection.Top:
                        this.PART_Popup.Placement = PlacementMode.Top;
                        this.ItemOrientation = Orientation.Vertical;
                        this.PART_Popup.HorizontalOffset = -10;
                        break;
                    case EnumPlacementDirection.Right:
                        this.PART_Popup.Placement = PlacementMode.Right;
                        this.PART_Popup.HorizontalOffset = 0;
                        this.PART_Popup.VerticalOffset = -10;
                        this.ItemOrientation = Orientation.Horizontal;
                        break;
                    case EnumPlacementDirection.Bottom:
                        this.PART_Popup.Placement = PlacementMode.Bottom;
                        this.ItemOrientation = Orientation.Vertical;
                        this.PART_Popup.HorizontalOffset = -10;
                        this.PART_Popup.VerticalOffset = 0;
                        break;
                    default:
                        break;
                }
            }
        }
        #endregion

        #region Event Implement Function

        #endregion
    }
}
