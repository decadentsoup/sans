// <copyright file="Comic.cs" company="None">
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
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO.Compression;
    using System.Linq;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Open and navigate comic archives.
    /// </summary>
    internal class Comic
    {
        private static readonly string[] SupportedExtensions =
        {
            ".bmp", ".dib", ".jpg", ".jpeg", ".jpe", ".jif", ".jfif", ".jfi",
            ".png", ".tiff", ".tif", ".jxr", ".hdp", ".wdp", ".gif", ".ico",
        };

        // TODO : call archive.Close() at some point...
        private readonly ZipArchive archive;
        private readonly SortedList<string, ZipArchiveEntry> pages;

        private int pageIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="Comic"/> class.
        /// </summary>
        /// <param name="path">Path to the comic archive.</param>
        public Comic(string path)
        {
            this.archive = ZipFile.OpenRead(path);
            this.pages = new SortedList<string, ZipArchiveEntry>();

            foreach (var entry in this.archive.Entries)
            {
                if (CheckPage(entry.FullName))
                {
                    this.pages.Add(entry.FullName, entry);
                }
                else
                {
                    Debug.WriteLine("Skipping {0}", entry.FullName);
                }
            }

            this.PageIndex = 0;
            this.PageCount = this.pages.Count;
        }

        /// <summary>
        /// Raised when a new image has been loaded for the current page.
        /// </summary>
        public event EventHandler<PageImageEventArgs> PageImageLoaded;

        /// <summary>
        /// Gets or sets the index of the page currently being viewed.
        /// </summary>
        public int PageIndex
        {
            get => this.pageIndex;
            set
            {
                var stream = this.pages.ElementAt(this.pageIndex = value).Value.Open();
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = stream;
                image.EndInit();

                // TODO : handle failed downloads
                image.DownloadCompleted += (sender, e) =>
                {
                    image.Freeze();
                    stream.Close();
                    this.PageImageLoaded?.Invoke(this, new PageImageEventArgs(image));
                };
            }
        }

        /// <summary>
        /// Gets the number of pages in this archive.
        /// </summary>
        public int PageCount { get; private set; }

        private static bool CheckPage(string pageName)
        {
            var index = pageName.LastIndexOf('.');

            if (index < 0)
            {
                return false;
            }

            return SupportedExtensions.Contains(pageName.Substring(index).ToLower());
        }
    }
}
