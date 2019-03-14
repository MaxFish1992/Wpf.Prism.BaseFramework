using BlueOrigin.Wpf.Controls.Core.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlueOrigin.Wpf.Controls
{
    /// <summary>
    /// 多选下拉框
    /// </summary>
    public class MultiComboBox : ComboBox
    {
        public static DependencyProperty WaterMarkProperty = DependencyProperty.Register("WaterMark", typeof(string), typeof(MultiComboBox), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.AffectsArrange, null));

        /// <summary>
        /// 未选中时内容区域显示的水印
        /// </summary>
        public string WaterMark
        {
            get
            {
                return (string)GetValue(WaterMarkProperty);
            }
            set
            {
                SetValue(WaterMarkProperty, value);
            }
        }

        public static DependencyProperty SelectedItemsProperty = DependencyProperty.Register("SelectedItems", typeof(IList), typeof(MultiComboBox));

        /// <summary>
        /// 选中的项
        /// </summary>
        public IList SelectedItems
        {
            get
            {
                return (IList)GetValue(SelectedItemsProperty);
            }
            set
            {
                SetValue(SelectedItemsProperty, value);
            }
        }


        public ICommand SelectedChangedCommand
        {
            get
            {
                return new ActionCommand(SelectedChanged);
            }
        }

        public ICommand ClearClickCommand
        {
            get
            {
                return new ActionCommand(ClearClick);
            }
        }


        static MultiComboBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiComboBox), new FrameworkPropertyMetadata(typeof(MultiComboBox)));
        }


        public void SelectedChanged()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in this.SelectedItems)
            {
                var type = item.GetType();
                var pInfo = type.GetProperty(DisplayMemberPath, typeof(string));

                sb.Append(pInfo.GetValue(item, null)).Append(";");
            }
            this.Text = sb.ToString().TrimEnd(';');
        }

        public void ClearClick()
        {
            SelectedItems.Clear();
        }
    }
}
