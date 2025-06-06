﻿using System;
using JetBrains.Annotations;
using UnityEngine;

namespace Reactive.Components;

[PublicAPI]
public static class ComponentExtensions {
    #region Button

    public static T WithAccentColor<T>(
        this T button,
        Color color
    ) where T : ColoredButton {
        button.Colors = new SimpleColorSet {
            NotInteractableColor = color.ColorWithAlpha(0.25f),
            HoveredColor = color.ColorWithAlpha(0.7f),
            Color = color.ColorWithAlpha(0.4f),
        };
        return button;
    }

    #endregion

    #region Graphic

    public static T WithPointerEvents<T>(
        this T graphic,
        Action<PointerEventsHandler>? onEnter = null,
        Action<PointerEventsHandler>? onLeave = null,
        Action<PointerEventsHandler>? onDown = null,
        Action<PointerEventsHandler>? onUp = null,
        Action<PointerEventsHandler>? onDrag = null,
        Action<PointerEventsHandler>? onDragBegin = null,
        Action<PointerEventsHandler>? onDragEnd = null,
        Action<PointerEventsHandler>? onScroll = null
    ) where T : IReactiveComponent, IGraphic {
        var pointerHandler = graphic.Content.GetOrAddComponent<PointerEventsHandler>();

        if (onEnter != null) {
            pointerHandler.PointerEnterEvent += (x, _) => onEnter.Invoke(x);
        }
        if (onLeave != null) {
            pointerHandler.PointerExitEvent += (x, _) => onLeave.Invoke(x);
        }
        if (onUp != null) {
            pointerHandler.PointerUpEvent += (x, _) => onUp.Invoke(x);
        }
        if (onDown != null) {
            pointerHandler.PointerDownEvent += (x, _) => onDown.Invoke(x);
        }
        if (onDrag != null) {
            pointerHandler.PointerDragEvent += (x, _) => onDrag.Invoke(x);
        }
        if (onDragBegin != null) {
            pointerHandler.PointerDragBeginEvent += (x, _) => onDragBegin.Invoke(x);
        }
        if (onDragEnd != null) {
            pointerHandler.PointerDragEndEvent += (x, _) => onDragEnd.Invoke(x);
        }
        if (onScroll != null) {
            pointerHandler.PointerScrollEvent += (x, _) => onScroll.Invoke(x);
        }

        return graphic;
    }

    #endregion

    #region Other

    [Pure]
    public static T In<T>(this ILayoutItem comp) where T : ILayoutDriver, new() {
        return new T {
            Children = { comp.WithRectExpand() }
        };
    }
    
    /// <summary>
    /// Binds a held component to a variable. Has less priority than basic Bind.
    /// </summary>
    /// <param name="holder">A holder to take the component from.</param>
    /// <param name="variable">A variable to bind to.</param>
    public static T Bind<T, TBind>(this T holder, ref TBind variable, int _ = 0) where T : IComponentHolder<TBind>, IReactiveComponent {
        variable = holder.Component;
        return holder;
    }
        
    /// <summary>
    /// Exports a held component to a variable. Has less priority than basic Export.
    /// </summary>
    /// <param name="holder">A holder to take the component from.</param>
    /// <param name="variable">A variable to export to.</param>
    public static T Export<T, TExport>(this T holder, out TExport variable, int _ = 0) where T : IComponentHolder<TExport>, IReactiveComponent {
        variable = holder.Component;
        return holder;
    }

    #endregion
}