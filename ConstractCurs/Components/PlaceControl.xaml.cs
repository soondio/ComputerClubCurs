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
    /// Логика взаимодействия для PlaceControl.xaml
    /// </summary>
    public partial class PlaceControl : UserControl
    {
        public static readonly DependencyProperty CommandProperty1 =
        DependencyProperty.Register(
        "Command1",
        typeof(ICommand),
        typeof(PlaceControl));

        public ICommand Command1
        {
            get
            {
                return (ICommand)GetValue(CommandProperty1);
            }

            set
            {
                SetValue(CommandProperty1, value);
            }
        }

        public PlaceControl()
        {
            InitializeComponent();

        }
    }
}
