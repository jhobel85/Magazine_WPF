using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Magazine_Conveyor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Magazine mag;

        public MainWindow()
        {
            InitializeComponent();
            mag = Magazine.GetInstance(Magazine.PLACES_AVAILABLE_DEFAULT, false, true);
            FindFreePlaceService = new FindFreePlaceService(mag);
            this.DataContext = mag;
            mag.NeededPlaces = Magazine.NEEDED_PLACES_DEFAULT;
        }

        private void btnAddMagazine_Click(object sender, RoutedEventArgs e)
        {
            var currNeededPlaces = Magazine.NEEDED_PLACES_DEFAULT;
            int.TryParse(txtNeededPlaces.Text, out currNeededPlaces);

            var posCount = mag.PositionCount;
            var isRot = mag.IsRotary;
            mag = Magazine.GetInstance(posCount, isRot, true);
            FindFreePlaceService = new FindFreePlaceService(mag);
            if (!this.DataContext.Equals(mag))
            {
                this.DataContext = mag;
            }
            mag.UpdatePositionsVisibility();
            mag.NeededPlaces = currNeededPlaces;
        }

        private void btnNeededPlace_Click(object sender, RoutedEventArgs e)
        {
            FindFreePlaceService.Execute();            
            if (FindFreePlaceService.FreePlace == -1)
            {
                var neededPlace = mag.NeededPlaces;
                MessageBox.Show("There is not enought free positions for needed places " + neededPlace);
            }            
        }

        public FindFreePlaceService FindFreePlaceService { get; private set; }
    }
}