using FirstFloor.ModernUI.Windows.Controls;
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

namespace At.FF.Krems.ModernConfig
{
    using System.Threading;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Task.Factory.StartNew(
                () =>
                    {
                        Thread.Sleep(5000);
                        this.Dispatcher.Invoke(
                            () =>
                            {
                                this.LogoData =
                                    Geometry.Parse(
                                        "M 16 3 C 8.8321429 3 3 8.8321429 3 16 C 3 23.167857 8.8321429 29 16 29 C 23.167857 29 29 23.167857 29 16 C 29 8.8321429 23.167857 3 16 3 z M 16 5 C 22.086977 5 27 9.9130231 27 16 C 27 22.086977 22.086977 27 16 27 C 9.9130231 27 5 22.086977 5 16 C 5 9.9130231 9.9130231 5 16 5 z M 15 10 L 15 18 L 17 18 L 17 10 L 15 10 z M 15 20 L 15 22 L 17 22 L 17 20 L 15 20 z");
                            });
                    });
        }
    }
}
