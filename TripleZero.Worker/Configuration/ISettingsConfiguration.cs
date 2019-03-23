using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleZero.Worker.Settings
{
    public interface ISettingsConfiguration
    {
        IConfigurationRoot GetConfiguration();
    }
}
