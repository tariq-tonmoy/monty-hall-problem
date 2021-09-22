namespace MontyHallProblemSimulation.Shared.SharedDto
{
    public class SimulationBusinessRuleViolationCodes
    {
        public const string SimulationAlreadyCreated = nameof(SimulationAlreadyCreated);
        public const string SimulationDoesNotExists = nameof(SimulationDoesNotExists);
        public const string SimulationIsAlreadyDeactivated = nameof(SimulationIsAlreadyDeactivated);
        public const string PropertyValueMissing = nameof(PropertyValueMissing);
        public const string PropertyValueNotInCorrectFormat = nameof(PropertyValueNotInCorrectFormat);
        public const string Success = nameof(Success);
    }
}
