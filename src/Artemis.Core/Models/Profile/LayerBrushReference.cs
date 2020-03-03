﻿using System;
using Artemis.Core.Plugins.LayerBrush;

namespace Artemis.Core.Models.Profile
{
    /// <summary>
    ///     A reference to a <see cref="LayerBrushDescriptor" />
    /// </summary>
    public class LayerBrushReference
    {
        /// <summary>
        ///     The GUID of the plugin the brush descriptor resides in
        /// </summary>
        public Guid BrushPluginGuid { get; set; }

        /// <summary>
        ///     The full type name of the brush descriptor
        /// </summary>
        public string BrushType { get; set; }
    }
}