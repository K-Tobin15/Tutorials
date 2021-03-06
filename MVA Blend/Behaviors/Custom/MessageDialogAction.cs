﻿using Microsoft.Xaml.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace Behaviors.Custom
{
    class MessageDialogAction: DependencyObject, IAction
    {
        public string Message { get; set; }
        public object Execute(object sender, object parameter)
        {
            new MessageDialog(this.Message).ShowAsync();
            return null;
        }
    }
}
