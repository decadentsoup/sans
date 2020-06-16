// <copyright file="ComicViewModel.cs" company="None">
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
    using System.ComponentModel;
    using System.Windows.Media;

    /// <summary>
    /// ViewModel managing a single <see cref="Comic"/>.
    /// </summary>
    internal class ComicViewModel : INotifyPropertyChanged
    {
        private ImageSource pageImage;

        /// <inheritdoc/>
        public event PropertyChangedEventHandler PropertyChanged;

        // TODO : privatize Comic?

        /// <summary>
        /// Gets the current comic loaded into memory, or null if no comic loaded.
        /// </summary>
        public Comic Comic { get; private set; }

        /// <summary>
        /// Gets or sets the current page (1-indexed).
        /// </summary>
        public int PageNumber
        {
            get => this.Comic == null ? 0 : this.Comic.PageIndex + 1;
            set
            {
                if (this.Comic != null)
                {
                    this.Comic.PageIndex = value - 1;
                    this.InvokePropertyChanged(nameof(this.PageNumber));
                }
            }
        }

        /// <summary>
        /// Gets the number of pages in the current <see cref="Comic"/>.
        /// </summary>
        public int PageCount => this.Comic == null ? 0 : this.Comic.PageCount;

        /// <summary>
        /// Gets the current page's image.
        /// </summary>
        public ImageSource PageImage
        {
            get => this.pageImage;
            private set
            {
                this.pageImage = value;
                this.InvokePropertyChanged(nameof(this.PageImage));
            }
        }

        /// <summary>
        /// Open a new comic archive, replacing any currently open comic archive.
        /// </summary>
        /// <param name="path">Path to the comic archive.</param>
        public void Open(string path)
        {
            this.Comic = new Comic(path);

            this.InvokePropertyChanged(nameof(this.PageNumber));
            this.InvokePropertyChanged(nameof(this.PageCount));

            this.Comic.PageImageLoaded += (sender, e) => this.PageImage = e.PageImage;
        }

        private void InvokePropertyChanged(string name) =>
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
