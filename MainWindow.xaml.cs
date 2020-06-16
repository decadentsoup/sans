// <copyright file="MainWindow.xaml.cs" company="None">
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
    using System.Windows;
    using System.Windows.Input;
    using Microsoft.Win32;

    /// <summary>
    /// Interaction logic for MainWindow.xaml.
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow() =>
            this.InitializeComponent();

        private ComicViewModel ComicVM
        {
            get => (ComicViewModel)this.Resources["Comic"];
        }

        private void OpenExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "Comic book archive|*.cbr;*.cbz;*.cbt;*.cba;*.cb7|All files|*.*",
            };

            if (dialog.ShowDialog() == true)
            {
                this.ComicVM.Open(dialog.FileName);
            }
        }

        private void ExitExecuted(object sender, ExecutedRoutedEventArgs e) =>
            this.Close();

        private void NextPageCanExecute(object sender, CanExecuteRoutedEventArgs e) =>
            e.CanExecute = this.ComicVM.PageNumber < this.ComicVM.PageCount;

        private void NextPageExecuted(object sender, ExecutedRoutedEventArgs e) =>
            this.ComicVM.PageNumber++;

        private void PreviousPageCanExecute(object sender, CanExecuteRoutedEventArgs e) =>
            e.CanExecute = this.ComicVM.PageNumber > 1;

        private void PreviousPageExecuted(object sender, ExecutedRoutedEventArgs e) =>
            this.ComicVM.PageNumber--;

        private void GoToPageExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            // TODO : implement GoToPage
        }

        private void FirstPageExecuted(object sender, ExecutedRoutedEventArgs e) =>
            this.ComicVM.PageNumber = 1;

        private void LastPageExecuted(object sender, ExecutedRoutedEventArgs e) =>
            this.ComicVM.PageNumber = this.ComicVM.PageCount;
    }
}
