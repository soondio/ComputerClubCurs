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
    /// Логика взаимодействия для FoodCardControl.xaml
    /// </summary>
    public partial class FoodCardControl : UserControl
    {
        public static readonly DependencyProperty CommandProperty3 =
           DependencyProperty.Register(
           "Command3",
           typeof(ICommand),
           typeof(FoodCardControl));

        public ICommand Command3
        {
            get
            {
                return (ICommand)GetValue(CommandProperty3);
            }

            set
            {
                SetValue(CommandProperty3, value);
            }
        }

        public FoodCardControl()
        {
            InitializeComponent();

        }
    }
}
