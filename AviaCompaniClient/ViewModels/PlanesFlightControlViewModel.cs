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
    public class PlanesFlightControlViewModel : ViewModelBase
    {
        private string _message;
        public string Message
        {
            get => _message;
            set => this.RaiseAndSetIfChanged(ref _message, value);
        }

        private Plane _selectedPlane;
        public Plane SelectedPlane
        {
            get => _selectedPlane;
            set => this.RaiseAndSetIfChanged(ref _selectedPlane, value);
        }

        private HttpClient client = new HttpClient();
        private ObservableCollection<Plane> _planes;
        public ObservableCollection<Plane> Planes
        {
            get => _planes;
            set => this.RaiseAndSetIfChanged(ref _planes, value);
        }

        private string _planeName;
        public string PlaneName
        {
            get => _planeName;
            set => this.RaiseAndSetIfChanged(ref _planeName, value);
        }
        public PlanesFlightControlViewModel()
        {
            client.BaseAddress = new Uri("https://localhost:7125");
            Update();
        }
        public async Task Update()
        {
            var response = await client.GetAsync("/planes");
            if (!response.IsSuccessStatusCode)
            {
                PlaneName = $"Ошибка сервера {response.StatusCode}";
            }
            var content = await response.Content.ReadAsStringAsync();
            if (content == null)
            {
                PlaneName = "Пустой ответ от сервера";
            }
            Planes = JsonSerializer.Deserialize<ObservableCollection<Plane>>(content);
            PlaneName = "";
        }

        public async Task Delete()
        {
            if (SelectedPlane == null) return;
            var response = await client.DeleteAsync($"/planes/{SelectedPlane.id}");
            if (!response.IsSuccessStatusCode)
            {
                PlaneName = "Ошибка удаления со стороны сервера";
                return;
            }
            Planes.Remove(SelectedPlane);
            SelectedPlane = null;
            PlaneName = "";
        }

        public async Task Add()
        {
            var plane = new Plane();
            var response = await client.PostAsJsonAsync($"/planes", plane);
            if (!response.IsSuccessStatusCode)
            {
                PlaneName = "Ошибка добавления со стороны сервера";
                return;
            }
            var content = await response.Content.ReadFromJsonAsync<Plane>();
            if (content == null)
            {
                PlaneName = "При добавлении сервер отправил пустой ответ";
                return;
            }
            plane = content;
            Planes.Add(plane);
            SelectedPlane = plane;
        }

        public async Task Edit()
        {
            var response = await client.PutAsJsonAsync($"/planes", SelectedPlane);
            if (!response.IsSuccessStatusCode)
            {
                PlaneName = "Ошибка изминения со стороны сервера";
                return;
            }
            var content = await response.Content.ReadFromJsonAsync<Plane>();
            if (content == null)
            {
                PlaneName = "При изминении сервер отправил пустой ответ";
                return;
            }
            SelectedPlane = content;
        }
    }
}
