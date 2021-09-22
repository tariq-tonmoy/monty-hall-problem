using Newtonsoft.Json;

namespace MontyHallProblemSimulation.Shared.Utility
{
    internal class JsonSerializationSessingsProvider
    {
        public JsonSerializerSettings GetJsonSerializerSettingsForPrivateProperties()
        {
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.ContractResolver = new IncludePrivateStateContractResolver();
            return jsonSerializerSettings;
        }
    }
}
