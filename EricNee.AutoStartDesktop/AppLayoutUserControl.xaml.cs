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
using EricNee.AutoStartDesktop.Library;

namespace EricNee.AutoStartDesktop
{
    /// <summary>
    /// Interaction logic for AppLayoutUserControl.xaml
    /// </summary>
    public partial class AppLayoutUserControl : UserControl
    {
        public AppLayoutUserControl(IEnumerable<AppEntity> processes)
        {
            Processes = processes;
            InitializeComponent();
        }

        public IEnumerable<AppEntity> Processes { get; }

        private void AppGrid_Loaded(object sender, RoutedEventArgs e)
        {
            var columnCount = 10;
            for (int i = 0; i < columnCount; i++)
            {
                AppGrid.ColumnDefinitions.Add(new ColumnDefinition());

            }

            var rowCount = Processes.Count() % 10 == 0 ? Processes.Count() / 10 : Processes.Count() / 10 + 1;
            for (int i = 0; i < rowCount; i++)
            {
                AppGrid.RowDefinitions.Add(new RowDefinition());
            }

            for (int i = 0; i < Processes.Count(); i++)
            {
                var currentRow = i / 10;
                var currentColumn = i % 10;
                var appIcon = new AppIconUserControl(Processes.ElementAt(i));
                AppGrid.Children.Add(appIcon);
                Grid.SetRow(appIcon, currentRow);
                Grid.SetColumn(appIcon, currentColumn);
            }
        }
    }
}
