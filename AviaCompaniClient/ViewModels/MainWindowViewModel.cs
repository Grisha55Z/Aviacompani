using AviaCompaniClient.Views;

namespace AviaCompaniClient.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel() 
        {
            _FlightsUserControl = new FlightPlanesControl();
            _FlightsUserControl.DataContext = new FlightPlanesControlViewModel();
            _PlanesUserControl = new PlanesFlightControl();
            _PlanesUserControl.DataContext = new PlanesFlightControlViewModel();
        }
        public FlightPlanesControl _FlightsUserControl { get; set; }
        public PlanesFlightControl _PlanesUserControl { get; set; }
    }
}