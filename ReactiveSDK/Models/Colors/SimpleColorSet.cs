using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Reactive.Components {
    /// <summary>
    /// A simple color set for general purposes.
    /// </summary>
    [PublicAPI]
    public class SimpleColorSet : IColorSet {
        public Color ActiveColor {
            get => _activeColor;
            set {
                _activeColor = value;
                SetUpdatedEvent?.Invoke();
            }
        }

        public Color HoveredColor {
            get => _hoveredColor;
            set {
                _hoveredColor = value;
                SetUpdatedEvent?.Invoke();
            }
        }

        public Color NotInteractableColor {
            get => _notInteractableColor;
            set {
                _notInteractableColor = value;
                SetUpdatedEvent?.Invoke();
            }
        }

        public Color Color {
            get => _color;
            set {
                _color = value;
                SetUpdatedEvent?.Invoke();
            }
        }

        private Color _notInteractableColor;
        private Color _activeColor;
        private Color _hoveredColor;
        private Color _color;

        public event Action? SetUpdatedEvent;

        public Color GetColor(GraphicState state) {
            if (state.IsHovered()) {
                return state.IsActive() ? ActiveColor : _hoveredColor;
            }
            if (state.IsActive()) {
                return _activeColor;
            }
            if (!state.IsInteractable()) {
                return _notInteractableColor;
            }
            return _color;
        }
    }
}