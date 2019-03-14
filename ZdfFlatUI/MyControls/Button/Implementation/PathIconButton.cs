﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ZdfFlatUI
{
    /// <summary>
    /// 只有图标的按钮（不显示按钮文本）
    /// </summary>
    public class PathIconButton : Button
    {
        #region 依赖属性

        #region Path相关属性
        public static readonly DependencyProperty PathWidthProperty = DependencyProperty.Register("PathWidth"
            , typeof(double), typeof(PathIconButton), new FrameworkPropertyMetadata(13d));
        /// <summary>
        /// Path的宽度
        /// </summary>
        public double PathWidth
        {
            get { return (double)GetValue(PathWidthProperty); }
            set { SetValue(PathWidthProperty, value); }
        }

        public static readonly DependencyProperty PathDataProperty = DependencyProperty.Register("PathData"
            , typeof(PathGeometry), typeof(PathIconButton));
        /// <summary>
        /// Path的Data
        /// </summary>
        public PathGeometry PathData
        {
            get { return (PathGeometry)GetValue(PathDataProperty); }
            set { SetValue(PathDataProperty, value); }
        }

        public static readonly DependencyProperty NormalPathColorProperty = DependencyProperty.Register("NormalPathColor"
            , typeof(Brush), typeof(PathIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(40, 139, 225))));
        /// <summary>
        /// 正常显示时Path的颜色
        /// </summary>
        public Brush NormalPathColor
        {
            get { return (Brush)GetValue(NormalPathColorProperty); }
            set { SetValue(NormalPathColorProperty, value); }
        }

        public static readonly DependencyProperty MouseOverPathColorProperty = DependencyProperty.Register("MouseOverPathColor"
            , typeof(Brush), typeof(PathIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(40, 139, 225))));
        /// <summary>
        /// 鼠标悬浮时Path的颜色
        /// </summary>
        public Brush MouseOverPathColor
        {
            get { return (Brush)GetValue(MouseOverPathColorProperty); }
            set { SetValue(MouseOverPathColorProperty, value); }
        }

        public static readonly DependencyProperty PressedPathColorProperty = DependencyProperty.Register("PressedPathColor"
            , typeof(Brush), typeof(PathIconButton), new FrameworkPropertyMetadata(new SolidColorBrush(Color.FromRgb(36, 127, 207))));
        /// <summary>
        /// 鼠标按下时Path的颜色
        /// </summary>
        public Brush PressedPathColor
        {
            get { return (Brush)GetValue(PressedPathColorProperty); }
            set { SetValue(PressedPathColorProperty, value); }
        }

        #endregion

        #region 按钮属性

        public static readonly DependencyProperty CornerRadiusProperty = DependencyProperty.Register("CornerRadius"
            , typeof(CornerRadius), typeof(PathIconButton));
        /// <summary>
        /// 按钮四周圆角
        /// </summary>
        public CornerRadius CornerRadius
        {
            get { return (CornerRadius)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }

        public static readonly DependencyProperty MouseOverBackgroundProperty = DependencyProperty.Register("MouseOverBackground"
            , typeof(Brush), typeof(PathIconButton));
        /// <summary>
        /// 鼠标悬浮时按钮的背景色
        /// </summary>
        public Brush MouseOverBackground
        {
            get { return (Brush)GetValue(MouseOverBackgroundProperty); }
            set { SetValue(MouseOverBackgroundProperty, value); }
        }

        public static readonly DependencyProperty PressedBackgroundProperty = DependencyProperty.Register("PressedBackground"
            , typeof(Brush), typeof(PathIconButton));
        /// <summary>
        /// 鼠标按下时按钮的背景色
        /// </summary>
        public Brush PressedBackground
        {
            get { return (Brush)GetValue(PressedBackgroundProperty); }
            set { SetValue(PressedBackgroundProperty, value); }
        }
        #endregion

        #endregion

        static PathIconButton()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(PathIconButton), new FrameworkPropertyMetadata(typeof(PathIconButton)));
        }
    }
}
