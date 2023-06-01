# OdeyTech.WPF.Common

`OdeyTech.WPF.Common` is a library that provides common functionalities for WPF applications. It includes classes for managing dialogs, displaying messages, handling exceptions, and more.

## Features

The `OdeyTech.WPF.Common` NuGet package provides the following features:

### Manager
- `ViewManager`: Represents a view manager responsible for displaying windows and dialogs in an application.
    - `Show<T>`: Shows a window of type <T> with the provided view model.
    - `ShowDialog<T>`: Shows a dialog window of type <T> with the provided view model.
    - `ShowError`: Shows an error message with the specified title and exception details.
    - `ShowMessageView`: Shows a message view with the specified title and message.
    - `ShowAskView`: Shows a message box with the specified title, message, and buttons.
    
### Utility
- `DialogCloser`: Class for closing dialogs programmatically.
    - `SetDialogResult`: Sets the DialogResult for the target window.
- `ExceptionHandler`: Provides exception handling extension methods for WPF Application objects.
    - `SetupExceptionHandling`: Sets up exception handling for the given WPF application.

### View
- `MessageWindow`: A modal window for displaying a message.

### ViewModel
- `IWindowViewModel`: Represents a view model for a window.
- `ModalViewModel`: Abstract base class for ViewModel classes that represent modals.
- `MessageViewModel`: View model for displaying a message in a modal window.

## Sample Usage

### Showing a Window with a View Model
~~~csharp
using OdeyTech.WPF.Common.Manager;

IWindowViewModel viewModel = new MyWindowViewModel();
IViewManager viewManager = new ViewManager();
viewManager.Show<MyWindow>(viewModel, parentWindow);
~~~

### Showing a Message Box
~~~csharp
using OdeyTech.WPF.Common.Manager;
using OdeyTech.ProductivityKit.Enum;

IViewManager viewManager = new ViewManager();
ButtonName result = viewManager.ShowAskView("Confirmation", "Are you sure?", new[] { ButtonName.Yes, ButtonName.No }, parentWindow);
if (result == ButtonName.Yes)
{
    // Handle Yes button clicked
}
else if (result == ButtonName.No)
{
    // Handle No button clicked
}
~~~

### Showing a Message View
~~~csharp
using OdeyTech.WPF.Common.Manager;

IViewManager viewManager = new ViewManager();
viewManager.ShowMessageView("Information", "Operation completed successfully.", parentWindow);
~~~

### Showing a Dialog Window with a View Model
~~~csharp
using OdeyTech.WPF.Common.Manager;

IWindowViewModel viewModel = new MyDialogViewModel();
IViewManager viewManager = new ViewManager();
viewManager.ShowDialog<MyDialogWindow>(viewModel, parentWindow);
~~~

### Showing an Error Message
~~~csharp
using OdeyTech.WPF.Common.Manager;

IViewManager viewManager = new ViewManager();
Exception exception = new Exception("Something went wrong");
viewManager.ShowError("Error", exception, parentWindow);
~~~

### Setting up Exception Handling for WPF Application
~~~csharp
using System;
using OdeyTech.WPF.Common.Manager;
using OdeyTech.WPF.Common.Utility;

var services = new ServiceCollection();
services.AddSingleton<IViewManager, ViewManager>();
Application.Current.SetupExceptionHandling(serviceProvider);
~~~

### Setting programmatically closing dialog
~~~xml
<Window
    ...
    xmlns:helper="clr-namespace:OdeyTech.WPF.Common.Utility;assembly=OdeyTech.WPF.Common"
    ...
    helper:DialogCloser.DialogResult="{Binding DialogResult}"
/>    
~~~

## Getting Started

To get started with `OdeyTech.WPF.Common`, follow these steps:

1. Install the `OdeyTech.WPF.Common` NuGet package.
2. Add references to the `OdeyTech.WPF.Common` namespace in your code files.
3. Use the provided classes and utilities according to your application's requirements.

## Contributing
We welcome contributions to `OdeyTech.WPF.Common`! Feel free to submit pull requests or raise issues to help us improve the library.

## License
`OdeyTech.WPF.Common` is released under the [MIT License][LICENSE]. See the LICENSE file for more information.

## Stay in Touch
For more information, updates, and future releases, follow me on [LinkedIn][LIn] I'd be happy to connect and discuss any questions or ideas you might have.

[//]: #
   [LIn]: <https://www.linkedin.com/in/anodeychuk/>
   [LICENSE]: <https://github.com/anodeychuk/OdeyTech.WPF.Common/blob/main/LICENSE>