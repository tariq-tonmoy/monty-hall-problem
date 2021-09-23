using MontyHallProblemSimulation.Shared.SharedDto;
using MontyHallProblemSimulation.Shared.SharedDto.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.ClientSide.ViewModel
{
    public class SimulationRequestViewModel : NotifyPropertyChanged
    {
        public const int PageSize = 10;

        private string environment;
        private long numberOfSimulations;
        private bool changeDoor;
        private string liveUpdate;
        private long count;
        private int currentPageIndex;
        private bool prevButtonEnabled;
        private bool nextButtonEnabled;

        private ObservableCollection<QueryResponse> simulations;
        private QueryResponse selectedSimulation;

        public SimulationRequestViewModel()
        {
            this.LiveUpdate = string.Empty;
            this.Simulations = new ObservableCollection<QueryResponse>();
        }

        public QueryResponse SelectedSimulation
        {
            get => this.selectedSimulation;
            set => SetProperty(ref this.selectedSimulation, value);
        }

        public ObservableCollection<QueryResponse> Simulations
        {
            get => simulations;
            set => SetProperty(ref simulations, value);
        }

        public int CurrentPageIndex
        {
            get
            {
                return currentPageIndex;
            }
            set
            {
                SetProperty(ref currentPageIndex, value);
                if (currentPageIndex == 0)
                {
                    this.PrevButtonEnabled = false;
                }
                else
                {
                    this.PrevButtonEnabled = true;
                }

                if (this.Count == 0 || (value + 1) * PageSize >= Count)
                {
                    this.NextButtonEnabled = false;
                }
                else
                {
                    this.NextButtonEnabled = true;
                }
            }
        }

        public bool PrevButtonEnabled
        {
            get => prevButtonEnabled;
            set => SetProperty(ref prevButtonEnabled, value);
        }

        public bool NextButtonEnabled
        {
            get => nextButtonEnabled;
            set => SetProperty(ref nextButtonEnabled, value);
        }

        public string Environment
        {
            get => environment;
            set => SetProperty(ref environment, value);
        }

        public long NumberOfSimulations
        {
            get => numberOfSimulations;
            set => SetProperty(ref numberOfSimulations, value);
        }

        public long Count
        {
            get => count;
            set => SetProperty(ref count, value);
        }

        public bool ChangeDoor
        {
            get => changeDoor;
            set => SetProperty(ref changeDoor, value);
        }

        public string LiveUpdate
        {
            get => liveUpdate;
            set => SetProperty(ref liveUpdate, value);
        }

        public void UpdateLiveSimulationResults(SimulationEventDto simulation)
        {
            this.LiveUpdate += $"{simulation.NumberOfSimulations} simulations\nDoor Changed: {simulation.ChangeDoor}\n{simulation.SuccessCount} success\n{simulation.FailCount} fails\nSuccessRate: {Math.Round(simulation.SuccessRatio, 2)}%\n\n";
        }

        public void UpdateQueryResponseWithCount(QueryResponseWithCount response)
        {
            this.Count = response.Count;
            this.CurrentPageIndex = this.currentPageIndex;
            this.Simulations.Clear();
            if (response.Responses != null && response.Responses.Any())
            {
                foreach (var item in response.Responses)
                {
                    this.Simulations.Add(item);
                }
            }
        }
    }
}