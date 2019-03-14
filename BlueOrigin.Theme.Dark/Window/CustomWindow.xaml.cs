using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace BlueOrigin.Theme.Dark
{
    public partial class CustomWindow
    {
        // 关闭
        private void CustomWindowBtnClose_Click(object sender, RoutedEventArgs e)
        {
            Window win = (Window)((FrameworkElement)sender).TemplatedParent;
            win.Close();
        }
        // 最小化

        private void CustomWindowBtnMinimized_Click(object sender, RoutedEventArgs e)
        {
            Window win = (Window)((FrameworkElement)sender).TemplatedParent;
            win.WindowState = WindowState.Minimized;
        }

        // 最大化、还原

        private void CustomWindowBtnMaxNormal_Click(object sender, RoutedEventArgs e)
        {
            Window win = (Window)((FrameworkElement)sender).TemplatedParent;
            if (win.WindowState == WindowState.Maximized)
            {
                win.WindowState = WindowState.Normal;
            }
            else
            {
                // 不覆盖任务栏
                win.MaxWidth = SystemParameters.WorkArea.Width;
                win.MaxHeight = SystemParameters.WorkArea.Height;
                win.WindowState = WindowState.Maximized;
            }
        }

        private void CustomWindow_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Window win = (Window)((FrameworkElement)sender).TemplatedParent;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                win.DragMove();
            }
            if (e.ClickCount == 2)
            {
                //if (win.WindowState == WindowState.Normal)
                //{
                //    win.WindowState = WindowState.Maximized;
                //}
                //else if (win.WindowState == WindowState.Maximized)
                //{
                //    win.WindowState = WindowState.Normal;
                //}
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
          


        }
    }
}
