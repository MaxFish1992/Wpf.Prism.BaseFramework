﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ZdfFlatUI
{
    public class ChooseBox : TextBox
    {
        #region private fields
        private Button PART_ChooseButton;
        #endregion

        #region DependencyProperty

        #region ChooseButtonStyle
        /// <summary>
        /// 获取或者设置选择按钮的样式
        /// </summary>
        public Style ChooseButtonStyle
        {
            get { return (Style)GetValue(ChooseButtonStyleProperty); }
            set { SetValue(ChooseButtonStyleProperty, value); }
        }

        public static readonly DependencyProperty ChooseButtonStyleProperty =
            DependencyProperty.Register("ChooseButtonStyle", typeof(Style), typeof(ChooseBox));

        #endregion

        #region ChooseBoxType

        public EnumChooseBoxType ChooseBoxType
        {
            get { return (EnumChooseBoxType)GetValue(ChooseBoxTypeProperty); }
            set { SetValue(ChooseBoxTypeProperty, value); }
        }

        public static readonly DependencyProperty ChooseBoxTypeProperty =
            DependencyProperty.Register("ChooseBoxType", typeof(EnumChooseBoxType), typeof(ChooseBox), new PropertyMetadata(EnumChooseBoxType.SingleFile));

        #endregion

        #region ChooseButtonWidth

        public double ChooseButtonWidth
        {
            get { return (double)GetValue(ChooseButtonWidthProperty); }
            set { SetValue(ChooseButtonWidthProperty, value); }
        }

        public static readonly DependencyProperty ChooseButtonWidthProperty =
            DependencyProperty.Register("ChooseButtonWidth", typeof(double), typeof(ChooseBox), new PropertyMetadata(20d));

        #endregion

        #endregion

        #region Constructors

        static ChooseBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ChooseBox), new FrameworkPropertyMetadata(typeof(ChooseBox)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            this.PART_ChooseButton = this.GetTemplateChild("PART_ChooseButton") as Button;
            if(this.PART_ChooseButton != null)
            {
                this.PART_ChooseButton.Click += PART_ChooseButton_Click;
            }
        }
        
        #endregion

        #region private function

        #endregion

        #region Event Implement Function
        private void PART_ChooseButton_Click(object sender, RoutedEventArgs e)
        {
            switch (this.ChooseBoxType)
            {
                case EnumChooseBoxType.SingleFile:
                    System.Windows.Forms.OpenFileDialog openFileDialog = new System.Windows.Forms.OpenFileDialog();
                    openFileDialog.Multiselect = false;
                    //"文本文件|*.*|C#文件|*.cs|所有文件|*.*"
                    //openFileDialog.Filter = this.Filter;
                    if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.Text = openFileDialog.FileName;
                    }
                    break;
                case EnumChooseBoxType.MultiFile:
                    break;
                case EnumChooseBoxType.Folder:
                    System.Windows.Forms.FolderBrowserDialog folderDialog = new System.Windows.Forms.FolderBrowserDialog();
                    if(folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        this.Text = folderDialog.SelectedPath;
                    }
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
