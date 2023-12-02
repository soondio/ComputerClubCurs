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

namespace ConstractCurs.Components
{
    /// <summary>
    /// Логика взаимодействия для PlaceCardInfo.xaml
    /// </summary>
    public partial class PlaceCardInfo : UserControl
    {
        public static readonly DependencyProperty CommandProperty2 =
           DependencyProperty.Register(
           "Command2",
           typeof(ICommand),
           typeof(PlaceCardInfo));

        public ICommand Command2
        {
            get
            {
                return (ICommand)GetValue(CommandProperty2);
            }

            set
            {
                SetValue(CommandProperty2, value);
            }
        }

        public PlaceCardInfo()
        {
            InitializeComponent();

        }
    }
}
