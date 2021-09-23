using Microsoft.Extensions.Options;
using MontyHallProblemSimulation.ClientSide.HubClient;
using MontyHallProblemSimulation.ClientSide.ViewModel;
using MontyHallProblemSimulation.ClientSide.WebClient.Abstractions;
using MontyHallProblemSimulation.Shared.SharedDto;
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

namespace FrontEndClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SimulationRequestViewModel requestViewModel;
        private readonly IPublishSimulateCommandService publishService;
        private readonly NotificationOrchestrator orchestrator;
        private readonly IQueryClient queryClient;

        public MainWindow(IPublishSimulateCommandService publishService, NotificationOrchestrator orchestrator, IQueryClient queryClient, NotificationClientWorker _)
        {
            InitializeComponent();
            requestViewModel = new SimulationRequestViewModel();
            DataContext = requestViewModel;
            this.publishService = publishService;
            this.orchestrator = orchestrator;
            this.queryClient = queryClient;
            this.orchestrator.OnActionPerformed += Orchestrator_OnActionPerformed;
            this.orchestrator.AppInitialized();
        }

        private void Orchestrator_OnActionPerformed(ActionType actionType, SimulationEventDto simulation = null)
        {
            if (simulation != null)
            {
                this.requestViewModel.UpdateLiveSimulationResults(simulation);
            }
        }

        private void PublishServerConnectionLostMessage()
        {
            MessageBox.Show("Server connection lost!");

        }

        private async Task UpdateSimulationList()
        {
            try
            {
                var queryResponseWithCount = await this.queryClient.GetSimulations(this.requestViewModel.CurrentPageIndex, SimulationRequestViewModel.PageSize, this.requestViewModel.Environment);
                this.requestViewModel.UpdateQueryResponseWithCount(queryResponseWithCount);
            }
            catch (Exception)
            {
                this.PublishServerConnectionLostMessage();
            }

        }

        private async void SimulateButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.requestViewModel.NumberOfSimulations <= 0 || string.IsNullOrWhiteSpace(this.requestViewModel.Environment))
            {
                MessageBox.Show("Select Environment and input number of strings");
                return;
            }

            try
            {
                var res = await this.publishService.PublishCreateSimulationCommandAsync(this.requestViewModel.NumberOfSimulations, this.requestViewModel.ChangeDoor, this.requestViewModel.Environment);

                if (res == false)
                {
                    this.PublishServerConnectionLostMessage();
                }

            }
            catch (Exception)
            {
                this.PublishServerConnectionLostMessage();
            }

        }

        private async void EnvSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.orchestrator.EnvironmentChanged(this.requestViewModel.Environment);
            await this.UpdateSimulationList();

        }

        private async void PrevButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.requestViewModel.Environment))
            {
                MessageBox.Show("Select Environment.");
                return;
            }

            this.requestViewModel.CurrentPageIndex--;
            await this.UpdateSimulationList();
        }

        private async void NextButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.requestViewModel.Environment))
            {
                MessageBox.Show("Select Environment.");
                return;
            }

            this.requestViewModel.CurrentPageIndex++;
            await this.UpdateSimulationList();
        }

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            await this.UpdateSimulationList();
        }


        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            try
            {
                var res = await this.publishService.PublishDeactivateSimulationCommandAsync((Guid)button.CommandParameter, this.requestViewModel.Environment);

                if (res == false)
                {
                    this.PublishServerConnectionLostMessage();
                    return;
                }
            }
            catch (Exception)
            {
                this.PublishServerConnectionLostMessage();
            }

        }

        private async void RerunButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            try
            {
                var res = await this.publishService.PublishRerunSimulationCommandAsync((Guid)button.CommandParameter, this.requestViewModel.Environment);

                if (res == false)
                {
                    this.PublishServerConnectionLostMessage();
                    return;
                }
            }
            catch (Exception)
            {
                this.PublishServerConnectionLostMessage();
            }
        }
    }
}
