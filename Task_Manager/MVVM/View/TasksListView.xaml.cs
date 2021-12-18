using System;
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

namespace Task_Manager.MVVM.View
{
    /// <summary>
    /// Логика взаимодействия для TasksListView.xaml
    /// </summary>
    public partial class TasksListView : UserControl
    {
        public TasksListView()
        {
            InitializeComponent();
        }

        private void taskListName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (taskListName.Text.Length == 0)
            {
                CustomMassageBox.Show("The tasks list name cannot be empty!", CustomMassageBox.CMessageTitle.Info, CustomMassageBox.CMessageButton.Ok, CustomMassageBox.CMessageButton.Cancel);
                taskListName.Text = "The list without name";
            }
        }
    }
}
