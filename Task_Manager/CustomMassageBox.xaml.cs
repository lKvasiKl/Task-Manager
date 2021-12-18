using System;
using System.Windows;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Forms;
using System.Windows.Media.Animation;

namespace Task_Manager
{
    /// <summary>
    /// Логика взаимодействия для CustomMassageBox.xaml
    /// </summary>
    public partial class CustomMassageBox : Window
    {
        public enum CMessageTitle
        {
            Error,
            Info,
            Warning,
            Confirm,
            Deleting
        }

        public enum CMessageButton
        {
            Ok,
            Yes,
            No,
            Cancel,
            Delete,
            Confirm
        }

        public CustomMassageBox()
        {
            InitializeComponent();
        }

        static CustomMassageBox cMessageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public CMessageTitle messageTitle { get; set; }

        public string GetTitle(CMessageTitle value)
        {
            return Enum.GetName(typeof(CMessageTitle), value);
        }
        public string GetButtonText(CMessageButton value)
        {
            return Enum.GetName(typeof(CMessageButton), value);
        }

         public static DialogResult Show(string message,CMessageTitle title,CMessageButton okButton,CMessageButton noButton)
        {
            cMessageBox = new();
            cMessageBox.btnOk.Content = cMessageBox.GetButtonText(okButton);
            cMessageBox.btnCancel.Content = cMessageBox.GetButtonText(noButton);
            cMessageBox.txtMessage.Text = message;
            cMessageBox.txtTitle.Text = cMessageBox.GetTitle(title);
            
            
            switch (title)
            {
                case CMessageTitle.Error:
                    cMessageBox.msgLogo.Kind = PackIconKind.Error;
                    cMessageBox.msgLogo.Foreground = Brushes.DarkRed;
                    break;
                case CMessageTitle.Info:
                    cMessageBox.msgLogo.Kind = PackIconKind.InfoCircle;
                    cMessageBox.msgLogo.Foreground = Brushes.DarkRed;
                    cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageTitle.Deleting:
                    Color blue = new();
                    blue.A = 0xFF;
                    blue.R = 0x69;
                    blue.G = 0x8E;
                    blue.B = 0xF1;
                    Color red = new();
                    red.A = 0xFF;
                    red.R = 0xE6;
                    red.G = 0x00;
                    red.B = 0x00;
                    cMessageBox.msgLogo.Kind = PackIconKind.Warning;
                    cMessageBox.msgLogo.Foreground = Brushes.Yellow;
                    cMessageBox.btnCancel.SetValue(Grid.ColumnProperty, 2);
                    cMessageBox.btnCancel.Background = new SolidColorBrush(blue);
                    cMessageBox.btnOk.SetValue(Grid.ColumnProperty, 1);
                    cMessageBox.btnOk.Background = new SolidColorBrush(red);
                    break;
                case CMessageTitle.Confirm:
                    cMessageBox.msgLogo.Kind = PackIconKind.QuestionMark;
                    cMessageBox.msgLogo.Foreground = Brushes.Gray;
                    break;
            }
            cMessageBox.ShowDialog();
            return result;
        }

        private void BntOk_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.Yes;
            Border border = new();
            cMessageBox.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            cMessageBox.Close();
        }
    }
}
