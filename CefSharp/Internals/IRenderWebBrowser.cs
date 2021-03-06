﻿// Copyright © 2010-2017 The CefSharp Authors. All rights reserved.
//
// Use of this source code is governed by a BSD-style license that can be found in the LICENSE file.

using System;

namespace CefSharp.Internals
{
    public interface IRenderWebBrowser : IWebBrowserInternal
    {
        ScreenInfo GetScreenInfo();
        ViewRect GetViewRect();
        /// <summary>
        /// Called to retrieve the translation from view coordinates to actual screen coordinates. 
        /// </summary>
        /// <param name="viewX">x</param>
        /// <param name="viewY">y</param>
        /// <param name="screenX">screen x</param>
        /// <param name="screenY">screen y</param>
        /// <returns>Return true if the screen coordinates were provided.</returns>
        bool GetScreenPoint(int viewX, int viewY, out int screenX, out int screenY);

        /// <summary>
        /// Called when an element should be painted. Pixel values passed to this method are scaled relative to view coordinates based on the
        /// value of <see cref="ScreenInfo.DeviceScaleFactor"/> returned from <see cref="IRenderWebBrowser.GetScreenInfo"/>.
        /// Called on the CEF UI Thread
        /// </summary>
        /// <param name="type">indicates whether the element is the view or the popup widget.</param>
        /// <param name="dirtyRect">contains the set of rectangles in pixel coordinates that need to be repainted</param>
        /// <param name="buffer">The bitmap will be will be  width * height *4 bytes in size and represents a BGRA image with an upper-left origin</param>
        /// <param name="width">width</param>
        /// <param name="height">height</param>
        void OnPaint(PaintElementType type, Rect dirtyRect, IntPtr buffer, int width, int height);

        void SetCursor(IntPtr cursor, CursorType type);

        /// <summary>
        /// Called when the user starts dragging content in the web view. 
        /// OS APIs that run a system message loop may be used within the StartDragging call.
        /// Don't call any of IBrowserHost::DragSource*Ended* methods after returning false.
        /// Call IBrowserHost.DragSourceEndedAt and DragSourceSystemDragEnded either synchronously or asynchronously to inform the web view that the drag operation has ended. 
        /// </summary>
        /// <param name="dragData"> Contextual information about the dragged content</param>
        /// <param name="mask"></param>
        /// <param name="x">is the drag start location in screen coordinates</param>
        /// <param name="y">is the drag start location in screen coordinates</param>
        /// <returns>Return true to handle the drag operation.</returns>
        bool StartDragging(IDragData dragData, DragOperationsMask mask, int x, int y);
        void UpdateDragCursor(DragOperationsMask operation);

        void SetPopupIsOpen(bool show);
        void SetPopupSizeAndPosition(int width, int height, int x, int y);

        /// <summary>
        /// Called when the IME composition range has changed.
        /// </summary>
        /// <param name="selectedRange">is the range of characters that have been selected</param>
        /// <param name="characterBounds">is the bounds of each character in view coordinates.</param>
        void OnImeCompositionRangeChanged(Range selectedRange, Rect[] characterBounds);
    };
}
