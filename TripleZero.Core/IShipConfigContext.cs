using SWGoH.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TripleZero.Core
{
    public interface IShipConfigContext
    {
        Task<List<ShipConfig>> GetShipsConfig();
    }
}
