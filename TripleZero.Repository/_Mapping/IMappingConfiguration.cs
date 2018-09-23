using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace TripleZero.Repository.Mapping
{
    internal interface IMappingConfiguration
    {
        IMapper GetConfigureMapper();
    }
}
