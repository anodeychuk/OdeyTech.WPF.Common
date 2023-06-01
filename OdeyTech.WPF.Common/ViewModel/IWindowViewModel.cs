// --------------------------------------------------------------------------
// <copyright file="IWindowViewModel.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Windows;

namespace OdeyTech.WPF.Common.ViewModel
{
  /// <summary>
  /// Represents a view model for a window.
  /// </summary>
  public interface IWindowViewModel
  {
    /// <summary>
    /// Gets or sets the title of the window.
    /// </summary>
    string WindowTitle { get; }

    /// <summary>
    /// Gets or sets the current window associated with the view model.
    /// </summary>
    Window CurrentWindow { get; set; }
  }
}