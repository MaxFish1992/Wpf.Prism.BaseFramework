﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZdfFlatUI
{
    public class ZGroupBox : HeaderedContentControl
    {
        #region Private属性

        #endregion

        #region 依赖属性定义

        #region HeaderBackground

        public Brush HeaderBackground
        {
            get { return (Brush)GetValue(HeaderBackgroundProperty); }
            set { SetValue(HeaderBackgroundProperty, value); }
        }

        public static readonly DependencyProperty HeaderBackgroundProperty =
            DependencyProperty.Register("HeaderBackground", typeof(Brush), typeof(ZGroupBox));

        #endregion

        #region HorizontalHeaderAlignment

        public HorizontalAlignment HorizontalHeaderAlignment
        {
            get { return (HorizontalAlignment)GetValue(HorizontalHeaderAlignmentProperty); }
            set { SetValue(HorizontalHeaderAlignmentProperty, value); }
        }
        
        public static readonly DependencyProperty HorizontalHeaderAlignmentProperty =
            DependencyProperty.Register("HorizontalHeaderAlignment", typeof(HorizontalAlignment), typeof(ZGroupBox), new PropertyMetadata(HorizontalAlignment.Stretch));

        #endregion

        #region HeaderPadding

        public Thickness HeaderPadding
        {
            get { return (Thickness)GetValue(HeaderPaddingProperty); }
            set { SetValue(HeaderPaddingProperty, value); }
        }
        
        public static readonly DependencyProperty HeaderPaddingProperty =
            DependencyProperty.Register("HeaderPadding", typeof(Thickness), typeof(ZGroupBox));

        #endregion

        #region CornerRadius

        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(CornerRadius), typeof(ZGroupBox), new PropertyMetadata(new CornerRadius(0), CornerRadiusCallback));

        private static void CornerRadiusCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ZGroupBox groupBox = d as ZGroupBox;
            if(groupBox != null)
            {
                CornerRadius radius = (CornerRadius)e.NewValue;
                groupBox.CornerRadiusInner = new CornerRadius(radius.TopLeft, radius.TopRight, 0, 0);
            }
        }

        #endregion

        #endregion

        #region 私有依赖属性

        #region CornerRadiusInner

        public CornerRadius CornerRadiusInner
        {
            get { return (CornerRadius)GetValue(CornerRadiusInnerProperty); }
            private set { SetValue(CornerRadiusInnerProperty, value); }
        }
        
        public static readonly DependencyProperty CornerRadiusInnerProperty =
            DependencyProperty.Register("CornerRadiusInner", typeof(CornerRadius), typeof(ZGroupBox));

        #endregion

        #endregion

        #region Constructors
        static ZGroupBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ZGroupBox), new FrameworkPropertyMetadata(typeof(ZGroupBox)));
        }
        #endregion

        #region Override方法

        #endregion

        #region Private方法

        #endregion
    }
}
