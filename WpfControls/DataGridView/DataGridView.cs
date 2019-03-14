using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace BlueOrigin.Wpf.Controls
{
    /// <summary>
    /// 拓展原生DataGrid控件的表格
    /// 1. 多选绑定
    /// </summary>
    public class DataGridView : DataGrid
    {
        public DataGridView()
        {
            this.SelectionChanged += CustomDataGrid_SelectionChanged;
        }

        public IList SelectedItemList
        {
            get { return (IList)GetValue(SelectedItemListProperty); }
            set { SetValue(SelectedItemListProperty, value); }
        }

        public static readonly DependencyProperty SelectedItemListProperty =
            DependencyProperty.Register("SelectedItemList", typeof(IList), typeof(DataGridView));

        private void CustomDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.SelectedItemList = this.SelectedItems;
        }
    }
}
