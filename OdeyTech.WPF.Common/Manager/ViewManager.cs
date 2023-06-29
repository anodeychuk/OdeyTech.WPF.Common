// --------------------------------------------------------------------------
// <copyright file="ViewManager.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Windows;
using OdeyTech.ProductivityKit;
using OdeyTech.ProductivityKit.Enum;
using OdeyTech.ProductivityKit.Extension;
using OdeyTech.WPF.Common.Properties;
using OdeyTech.WPF.Common.View;
using OdeyTech.WPF.Common.ViewModel;

namespace OdeyTech.WPF.Common.Manager
{
    /// <summary>
    /// The view manager responsible for showing different views in the application.
    /// </summary>
    public class ViewManager : IViewManager
    {
        private readonly IWindowFactory windowFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewManager"/> class.
        /// </summary>
        public ViewManager()
        {
            this.windowFactory = new WindowFactory();
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="viewModel"/> is null.</exception>
        public void Show<T>(IWindowViewModel viewModel, Window parent = null) where T : Window, new()
            => CreateView<T>(viewModel, parent).Show();

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="viewModel"/> is null.</exception>
        public void ShowDialog<T>(IWindowViewModel viewModel, Window parent = null) where T : Window, new()
            => CreateView<T>(viewModel, parent).ShowDialog();

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="title"/> or <paramref name="message"/> is null or empty.</exception>
        public void ShowError(string title, Exception exception, Window parent = null)
        {
            var message = "Title: {0}{1}".Format(title, GetExceptionMessage(exception, 0));
            ShowMessageView(Resources.ErrorViewTitle, message, parent);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="title"/> or <paramref name="message"/> is null or empty.</exception>
        public void ShowMessageView(string title, string message, Window parent = null)
        {
            ThrowHelper.ThrowIfNullOrEmpty(title, nameof(title));
            ThrowHelper.ThrowIfNullOrEmpty(message, nameof(message));

            ShowDialog<MessageWindow>(new MessageViewModel(title, message), parent);
        }

        /// <inheritdoc/>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="title"/>, <paramref name="message"/>, or <paramref name="buttons"/> is null or empty.</exception>
        public ButtonName ShowAskView(string title, string message, ButtonName[] buttons, Window parent = null)
        {
            ThrowHelper.ThrowIfNullOrEmpty(title, nameof(title));
            ThrowHelper.ThrowIfNullOrEmpty(message, nameof(message));
            ThrowHelper.ThrowIfNull(buttons, nameof(buttons));

            var viewModel = new MessageViewModel(title, message, buttons);
            ShowDialog<MessageWindow>(viewModel, parent);
            return viewModel.ResultButton;
        }

        /// <summary>
        /// Creates an instance of a window of type <typeparamref name="T"/> with the provided view model and parent window.
        /// </summary>
        /// <typeparam name="T">The type of the window to create.</typeparam>
        /// <param name="viewModel">The view model to associate with the window.</param>
        /// <param name="parent">The parent window, if any.</param>
        /// <returns>The created window instance.</returns>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="viewModel"/> is null.</exception>
        private T CreateView<T>(IWindowViewModel viewModel, Window parent = null) where T : Window, new()
        {
            ThrowHelper.ThrowIfNull(viewModel, nameof(viewModel));

            T window = this.windowFactory.Create<T>();
            window.DataContext = viewModel;
            window.Owner = parent;
            viewModel.CurrentWindow = window;
            return window;
        }

        /// <summary>
        /// Retrieves the formatted exception message recursively, including exception details and stack trace.
        /// </summary>
        /// <param name="exception">The exception object.</param>
        /// <param name="innerExceptionLevel">The current level of the inner exception.</param>
        /// <returns>The formatted exception message.</returns>
        private string GetExceptionMessage(Exception exception, int innerExceptionLevel)
        {
            var message = exception.InnerException == null ? string.Empty : GetExceptionMessage(exception.InnerException, innerExceptionLevel + 1);
            message += "\n\n>>>Exception level: {0}\n\nException Type: {1}\n\nMessage: {2}\n\nStack: \n{3}"
                .Format(innerExceptionLevel, exception.GetType(), exception.Message, exception.StackTrace);
            return message;
        }
    }
}
