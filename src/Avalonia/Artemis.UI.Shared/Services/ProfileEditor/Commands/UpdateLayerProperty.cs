﻿using System;
using Artemis.Core;

namespace Artemis.UI.Shared.Services.ProfileEditor.Commands;

/// <summary>
///     Represents a profile editor command that can be used to update a layer property of type <typeparamref name="T" />.
/// </summary>
public class UpdateLayerProperty<T> : IProfileEditorCommand
{
    private readonly LayerProperty<T> _layerProperty;
    private readonly T _newValue;
    private readonly T _originalValue;
    private readonly TimeSpan? _time;
    private LayerPropertyKeyframe<T>? _newKeyframe;

    /// <summary>
    ///     Creates a new instance of the <see cref="UpdateLayerProperty{T}" /> class.
    /// </summary>
    public UpdateLayerProperty(LayerProperty<T> layerProperty, T newValue, TimeSpan? time)
    {
        _layerProperty = layerProperty;
        _originalValue = layerProperty.CurrentValue;
        _newValue = newValue;
        _time = time;

        DisplayName = $"Update {_layerProperty.PropertyDescription.Name ?? "property"}";
    }

    /// <summary>
    ///     Creates a new instance of the <see cref="UpdateLayerProperty{T}" /> class.
    /// </summary>
    public UpdateLayerProperty(LayerProperty<T> layerProperty, T newValue, T originalValue, TimeSpan? time)
    {
        _layerProperty = layerProperty;
        _originalValue = originalValue;
        _newValue = newValue;
        _time = time;

        DisplayName = $"Update {_layerProperty.PropertyDescription.Name ?? "property"}";
    }

    #region Implementation of IProfileEditorCommand

    /// <inheritdoc />
    public string DisplayName { get; private set; }

    /// <inheritdoc />
    public void Execute()
    {
        // If there was already a keyframe from a previous execute that was undone, put it back
        if (_newKeyframe != null)
            _layerProperty.AddKeyframe(_newKeyframe);
        else
        {
            _newKeyframe = _layerProperty.SetCurrentValue(_newValue, _time);
            if (_newKeyframe != null)
                DisplayName = $"Add {_layerProperty.PropertyDescription.Name ?? "property"} keyframe";
        }
    }

    /// <inheritdoc />
    public void Undo()
    {
        if (_newKeyframe != null)
            _layerProperty.RemoveKeyframe(_newKeyframe);
        else
            _layerProperty.SetCurrentValue(_originalValue, _time);
    }

    #endregion
}