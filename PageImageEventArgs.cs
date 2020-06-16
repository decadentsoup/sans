﻿// <copyright file="PageImageEventArgs.cs" company="None">
// Copyright (C) 2020 Megan Ruggiero.
//
// Permission to use, copy, modify, and distribute this software for any
// purpose with or without fee is hereby granted, provided that the above
// copyright notice and this permission notice appear in all copies.
//
// THE SOFTWARE IS PROVIDED "AS IS" AND THE AUTHOR DISCLAIMS ALL WARRANTIES
// WITH REGARD TO THIS SOFTWARE INCLUDING ALL IMPLIED WARRANTIES OF
// MERCHANTABILITY AND FITNESS. IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR
// ANY SPECIAL, DIRECT, INDIRECT, OR CONSEQUENTIAL DAMAGES OR ANY DAMAGES
// WHATSOEVER RESULTING FROM LOSS OF USE, DATA OR PROFITS, WHETHER IN AN
// ACTION OF CONTRACT, NEGLIGENCE OR OTHER TORTIOUS ACTION, ARISING OUT OF
// OR IN CONNECTION WITH THE USE OR PERFORMANCE OF THIS SOFTWARE.
// </copyright>

namespace Sans
{
    using System;
    using System.Windows.Media;

    /// <summary>
    /// Arguments for events involving a page image.
    /// </summary>
    internal class PageImageEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PageImageEventArgs"/> class.
        /// </summary>
        /// <param name="image">New current page image.</param>
        public PageImageEventArgs(ImageSource image) => this.PageImage = image;

        /// <summary>
        /// Gets new current page image as of the event.
        /// </summary>
        public ImageSource PageImage { get; private set; }
    }
}
