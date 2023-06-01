// --------------------------------------------------------------------------
// <copyright file="ModalViewModel.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OdeyTech.ProductivityKit.Enum;
using OdeyTech.WPF.Common.Utility;

namespace OdeyTech.WPF.Common.ViewModel
{
  /// <summary>
  /// Abstract base class for ViewModel classes that represent modals.
  /// </summary>
  public abstract partial class ModalViewModel : ObservableObject, IWindowViewModel
  {
    private bool? dialogResult;

    /// <summary>
    /// Gets or sets the DialogResult used to programmatically close dialogs.
    /// </summary>
    public bool? DialogResult
    {
      get => this.dialogResult;
      set => SetProperty(ref this.dialogResult, value, nameof(DialogResult));
    }

    /// <inheritdoc/>
    public string WindowTitle { get; set; }

    /// <inheritdoc/>
    public Window CurrentWindow { get; set; }

    /// <summary>
    /// Gets or sets the result button of the modal.
    /// </summary>
    public ButtonName ResultButton { get; set; }

    /// <summary>
    /// Command to close the modal.
    /// </summary>
    [RelayCommand]
    public virtual void Close() => DialogResult = DialogCloser.ForceClose;
  }
}