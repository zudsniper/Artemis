﻿using System.Drawing;
using Artemis.Core.Models.Profile;
using Artemis.Core.Models.Surface;
using Artemis.Core.Plugins.Abstract;
using Artemis.Core.Plugins.Models;
using QRCoder;

namespace Artemis.Plugins.LayerTypes.Brush
{
    public class BrushLayerType : LayerType
    {
        public BrushLayerType(PluginInfo pluginInfo) : base(pluginInfo)
        {
        }

        public override void EnablePlugin()
        {
            var qrGenerator = new QRCodeGenerator();
        }

        public override void DisablePlugin()
        {
        }

        public override void Update(Layer layer)
        {
            var config = layer.LayerTypeConfiguration as BrushConfiguration;
            if (config == null)
                return;

            // Update the brush
        }

        public override void Render(Layer device, Surface surface, Graphics graphics)
        {
        }

        public override void Dispose()
        {
        }
    }
}