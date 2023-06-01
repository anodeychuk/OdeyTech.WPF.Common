// --------------------------------------------------------------------------
// <copyright file="IViewManager.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System;
using System.Windows;
using OdeyTech.ProductivityKit.Enum;
using OdeyTech.WPF.Common.ViewModel;

namespace OdeyTech.WPF.Common.Manager
{
  /// <summary>
  /// Represents a view manager responsible for displaying windows and dialogs in an application.
  /// </summary>
  public interface IViewManager
  {
    /// <summary>
    /// Shows a window of type <typeparamref name="T"/> with the provided view model.
    /// </summary>
    /// <typeparam name="T">The type of the window to show.</typeparam>
    /// <param name="viewModel">The view model to associate with the window.</param>
    /// <param name="parent">The parent window, if any.</param>
    void Show<T>(IWindowViewModel viewModel, Window parent = null) where T : Window, new();

    /// <summary>
    /// Shows a message box with the specified title, message, and buttons.
    /// </summary>
    /// <param name="title">The title of the message box.</param>
    /// <param name="message">The message to display.</param>
    /// <param name="buttons">The buttons to include in the message box.</param>
    /// <param name="parent">The parent window, if any.</param>
    /// <returns>The button clicked by the user.</returns>
    ButtonName ShowAskView(string title, string message, ButtonName[] buttons, Window parent = null);

    /// <summary>
    /// Shows a message view with the specified title and message.
    /// </summary>
    /// <param name="title">The title of the message view.</param>
    /// <param name="message">The message to display.</param>
    /// <param name="parent">The parent window, if any.</param>
    void ShowMessageView(string title, string message, Window parent = null);

    /// <summary>
    /// Shows a dialog window of type <typeparamref name="T"/> with the provided view model.
    /// </summary>
    /// <typeparam name="T">The type of the dialog window to show.</typeparam>
    /// <param name="viewModel">The view model to associate with the dialog window.</param>
    /// <param name="parent">The parent window, if any.</param>
    void ShowDialog<T>(IWindowViewModel viewModel, Window parent = null) where T : Window, new();

    /// <summary>
    /// Shows an error message with the specified title and exception details.
    /// </summary>
    /// <param name="title">The title of the error message.</param>
    /// <param name="exception">The exception object containing the error details.</param>
    /// <param name="parent">The parent window, if any.</param>
    void ShowError(string title, Exception exception, Window parent = null);
  }
}