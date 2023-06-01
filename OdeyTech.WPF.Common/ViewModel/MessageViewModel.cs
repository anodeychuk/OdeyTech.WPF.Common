﻿// --------------------------------------------------------------------------
// <copyright file="MessageViewModel.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Linq;
using System.Windows;
using CommunityToolkit.Mvvm.Input;
using OdeyTech.ProductivityKit.Enum;

namespace OdeyTech.WPF.Common.ViewModel
{
  /// <summary>
  /// View model for displaying a message in a modal window.
  /// </summary>
  public partial class MessageViewModel : ModalViewModel
  {
    private string message;
    private Visibility messageTextBoxVisible;
    private int windowHeight;
    private ResizeMode windowResizeMode;
    private bool isOkButtonVisible;
    private bool isYesButtonVisible;
    private bool isNoButtonVisible;
    private ButtonName clickedButton;
    private readonly ButtonName[] buttons;

    /// <summary>
    /// Initializes a new instance of the <see cref="MessageViewModel"/> class.
    /// </summary>
    /// <param name="title">The title of the message window.</param>
    /// <param name="message">The message to display.</param>
    /// <param name="buttons">The buttons to display in the message window.</param>
    public MessageViewModel(string title, string message, ButtonName[] buttons = null) : base()
    {
      WindowTitle = title;
      Message = message;

      if (Message.Length > 100)
      {
        WindowHeight = 500;
        MessageTextBoxVisible = Visibility.Visible;
        WindowResizeMode = ResizeMode.CanResizeWithGrip;
      }
      else
      {
        WindowHeight = 200;
        MessageTextBoxVisible = Visibility.Hidden;
        WindowResizeMode = ResizeMode.NoResize;
      }

      this.buttons = buttons ?? new ButtonName[] { ButtonName.Ok };
      ActivateButtons();
    }

    /// <summary>
    /// Gets or sets the message to display.
    /// </summary>
    public string Message
    {
      get => this.message;
      private set => SetProperty(ref this.message, value, nameof(Message));
    }

    /// <summary>
    /// Gets or sets the visibility of the message text box.
    /// </summary>
    public Visibility MessageTextBoxVisible
    {
      get => this.messageTextBoxVisible;
      private set
      {
        if (this.messageTextBoxVisible != value)
        {
          this.messageTextBoxVisible = value;
          OnPropertyChanged(nameof(MessageTextBoxVisible));
          OnPropertyChanged(nameof(ErrorMessageLabelVisible));
        }
      }
    }

    /// <summary>
    /// Gets the visibility of the error message label.
    /// </summary>
    public Visibility ErrorMessageLabelVisible => this.messageTextBoxVisible == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;

    /// <summary>
    /// Gets or sets the height of the window.
    /// </summary>
    public int WindowHeight
    {
      get => this.windowHeight;
      set => SetProperty(ref this.windowHeight, value, nameof(WindowHeight));
    }

    /// <summary>
    /// Gets or sets the resize mode of the window.
    /// </summary>
    public ResizeMode WindowResizeMode
    {
      get => this.windowResizeMode;
      private set => SetProperty(ref this.windowResizeMode, value, nameof(WindowResizeMode));
    }

    /// <summary>
    /// Command executed when a button is clicked.
    /// </summary>
    /// <param name="button">The clicked button.</param>
    [RelayCommand]
    public void ClickButton(ButtonName button)
    {
      ClickedButton = button;
      Close();
    }

    /// <summary>
    /// Gets or sets the button that was clicked.
    /// </summary>
    public ButtonName ClickedButton
    {
      get => this.clickedButton;
      private set => SetProperty(ref this.clickedButton, value);
    }

    /// <summary>
    /// Gets or sets a value indicating whether the OK button is visible.
    /// </summary>
    public bool IsOkButtonVisible
    {
      get => this.isOkButtonVisible;
      private set => SetProperty(ref this.isOkButtonVisible, value, nameof(IsOkButtonVisible));
    }

    /// <summary>
    /// Gets or sets a value indicating whether the Yes button is visible.
    /// </summary>
    public bool IsYesButtonVisible
    {
      get => this.isYesButtonVisible;
      private set => SetProperty(ref this.isYesButtonVisible, value, nameof(IsYesButtonVisible));
    }

    /// <summary>
    /// Gets or sets a value indicating whether the No button is visible.
    /// </summary>
    public bool IsNoButtonVisible
    {
      get => this.isNoButtonVisible;
      private set => SetProperty(ref this.isNoButtonVisible, value, nameof(IsNoButtonVisible));
    }

    /// <summary>
    /// Activates the buttons based on the specified button names.
    /// </summary>
    public void ActivateButtons()
    {
      IsOkButtonVisible = this.buttons.Any(b => b == ButtonName.Ok);
      IsYesButtonVisible = this.buttons.Any(b => b == ButtonName.Yes);
      IsNoButtonVisible = this.buttons.Any(b => b == ButtonName.No);
    }
  }
}