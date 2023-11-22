using Avalonia.Controls;
using Aviacompani.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Tmds.DBus.Protocol;

namespace AviaCompaniClient.ViewModels
{
    public class FlightPlanesControlViewModel : ViewModelBase
    {
        private string _message;
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }
        private Flight _selectedFlight;
        public Flight SelectedFlight
        {
            get => _selectedFlight; 
            set => this.RaiseAndSetIfChanged(ref _selectedFlight, value);
        }

        private HttpClient client = new HttpClient();
        private ObservableCollection<Flight> _flights;
        public ObservableCollection<Flight> Flights
        {
            get => _flights;
            set => this.RaiseAndSetIfChanged(ref _flights, value);
        }

        private string _flightName;
        public string FlightName
        {
            get => _flightName;
            set => this.RaiseAndSetIfChanged(ref _flightName, value);
        }
        public FlightPlanesControlViewModel()
        {
            client.BaseAddress = new Uri("https://localhost:7125/");
            Update();
        }
        public async Task Update()
        {
            var response = await client.GetAsync("/flights");
            if (!response.IsSuccessStatusCode) 
            {
                FlightName = $"Ошибка сервера {response.StatusCode}";
            }
            var content = await response.Content.ReadAsStringAsync();
            if (content == null)
            {
                FlightName = "Пустой ответ от сервера";
            }
            Flights = JsonSerializer.Deserialize<ObservableCollection<Flight>>(content);
            FlightName = "";
        }

        public async Task Delete()
        {
            if (SelectedFlight == null) return;
            var response = await client.DeleteAsync($"/courses/{SelectedFlight.Id}");
            if (!response.IsSuccessStatusCode)
            {
                FlightName = "Ошибка удаления со стороны сервера";
                return;
            }
            Flights.Remove(SelectedFlight);
            SelectedFlight = null;
            FlightName = "";
        }

        public async Task Add()
        {
            var flight = new Flight();
            var response = await client.PostAsJsonAsync($"/flights", flight);
            if (!response.IsSuccessStatusCode)
            {
                FlightName = "Ошибка добавления со стороны сервера";
                return;
            }
            var content = await response.Content.ReadFromJsonAsync<Flight>();
            if (content == null)
            {
                FlightName = "При добавлении сервер отправил пустой ответ";
                return;
            }
            flight = content;
            Flights.Add(flight);
            SelectedFlight = flight;
        }

        public async Task Edit()
        {
            var response = await client.PutAsJsonAsync($"/flight", SelectedFlight);
            if (!response.IsSuccessStatusCode)
            {
                FlightName = "Ошибка изминения со стороны сервера";
                return;
            }
            var content = await response.Content.ReadFromJsonAsync<Flight>();
            if (content == null)
            {
                FlightName = "При изминении сервер отправил пустой ответ";
                return;
            }
            SelectedFlight = content;
        }
    }
}
