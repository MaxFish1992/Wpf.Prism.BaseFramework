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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlueOrigin.Wpf.Controls
{
    public class MultiLineView : ItemsControl
    {
        private bool AutoScroll = true;

        static MultiLineView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MultiLineView), new FrameworkPropertyMetadata(typeof(MultiLineView)));
        }

        public MultiLineView()
        {
            AddHandler(ScrollViewer.ScrollChangedEvent, new ScrollChangedEventHandler(ScroolViewer_ScrollChanged), true);
        }


        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            var scrollViewer = GetTemplateChild("PART_ScroolViewer") as ScrollViewer;
        }

        private void ScroolViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = e.OriginalSource as ScrollViewer;
            // User scroll event : set or unset autoscroll mode
            if (e.ExtentHeightChange == 0)
            {   // Content unchanged : user scroll event
                if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                {   // Scroll bar is in bottom
                    // Set autoscroll mode
                    AutoScroll = true;
                }
                else
                {   // Scroll bar isn't in bottom
                    // Unset autoscroll mode
                    AutoScroll = false;
                }
            }

            // Content scroll event : autoscroll eventually
            if (AutoScroll && e.ExtentHeightChange != 0)
            {   // Content changed and autoscroll mode set
                // Autoscroll
                scrollViewer.ScrollToVerticalOffset(scrollViewer.ExtentHeight);
            }

            e.Handled = true;
        }
    }
}
