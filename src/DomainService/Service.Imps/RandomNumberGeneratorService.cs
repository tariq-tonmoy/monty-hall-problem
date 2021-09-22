using Microsoft.Extensions.Configuration;
using MontyHallProblemSimulation.Domain.DomainService.Abstractions;
using MontyHallProblemSimulation.Domain.DomainService.DataModels;
using MontyHallProblemSimulation.Shared.Utility.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MontyHallProblemSimulation.Domain.DomainService.Service.Imps
{
    public class RandomNumberGeneratorService : IRandomNumberGeneratorService
    {
        private readonly IDateTimeProvider dateTimeProvider;

        public RandomNumberGeneratorService(IDateTimeProvider dateTimeProvider)
        {
            this.dateTimeProvider = dateTimeProvider;
        }

        public List<int[]> GenerateRandomNumbers(List<RandomNumberGeneratorPayload> payloads)
        {
            List<int[]> randomNumbersArray = new List<int[]>();

            foreach (var payload in payloads)
            {
                var total = payload.Total;
                int[] randomNumbers = new int[total];
                Random random = new Random((int)this.dateTimeProvider.GetUtcDateTime().Ticks);

                for (int i = 0; i < total; i++)
                {
                    randomNumbers[i] = random.Next(payload.MinValue, payload.MaxValue);
                }

                randomNumbersArray.Add(randomNumbers);
            }

            return randomNumbersArray;
        }
    }
}
