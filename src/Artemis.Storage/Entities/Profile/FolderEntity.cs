﻿using System;
using System.Collections.Generic;
using Artemis.Storage.Entities.Profile.Abstract;
using LiteDB;

namespace Artemis.Storage.Entities.Profile;

public class FolderEntity : RenderElementEntity
{
    public FolderEntity()
    {
        LayerEffects = new List<LayerEffectEntity>();
    }

    public int Order { get; set; }
    public string Name { get; set; }
    public bool IsExpanded { get; set; }
    public bool Suspended { get; set; }

    [BsonRef("ProfileEntity")]
    public ProfileEntity Profile { get; set; }

    public Guid ProfileId { get; set; }
}