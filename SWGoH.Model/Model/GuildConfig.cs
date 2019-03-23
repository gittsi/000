using System;
using System.Collections.Generic;
using System.Text;

namespace SWGoH.Model
{
    public class GuildConfig : Cache
    {
        public string Name { get; set; }
        public List<string> Aliases { get; set; }
        public string DefaultPlayerAllyCode { get; set; }
        public override bool LoadedFromCache { get => base.LoadedFromCache; set => base.LoadedFromCache = value; }
        public bool AutoQueue { get; set; }
    }
}