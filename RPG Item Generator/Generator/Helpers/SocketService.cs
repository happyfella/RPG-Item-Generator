using RPG_Item_Generator.Models.External;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPG_Item_Generator.Generator.Helpers
{
    internal class SocketService
    {
        private readonly CalculationService _calculationService;

        public SocketService()
        {
            _calculationService = new CalculationService();
        }

        public int GenerateNumberOfSockets(ItemDefinition definition)
        {
            if(definition.IsSocketed && !definition.IsConsumable)
            {
                var result = _calculationService.GetRandomInteger(definition.MinimumSocket, definition.MaximumSocket, false);
                return result;
            }
            
            return 0;
        }
    }
}
