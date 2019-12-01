﻿using System;

namespace Artemis.Storage.Entities.Profile
{
    public class LayerElementEntity
    {
        public Guid PluginGuid { get; set; }
        public string LayerElementType { get; set; }
        public string Configuration { get; set; }
    }
}