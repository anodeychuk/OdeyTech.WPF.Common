// --------------------------------------------------------------------------
// <copyright file="WindowFactory.cs" author="Andrii Odeychuk">
//
// Copyright (c) Andrii Odeychuk. ALL RIGHTS RESERVED
// The entire contents of this file is protected by International Copyright Laws.
// </copyright>
// --------------------------------------------------------------------------

using System.Windows;

namespace OdeyTech.WPF.Common.Manager
{
    /// <summary>
    /// Provides a default implementation of the <see cref="IWindowFactory"/> interface.
    /// </summary>
    public class WindowFactory : IWindowFactory
    {
        /// <inheritdoc/>
        public T Create<T>() where T : Window, new() => new();
    }
}
