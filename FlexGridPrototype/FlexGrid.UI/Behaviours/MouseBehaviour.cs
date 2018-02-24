using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MVVM;

namespace FlexGrid.UI.Behaviours
{
    public class MouseBehaviour
    {


        public static ICommand GetMouseDownCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(MouseDownCommandProperty);
        }

        public static void SetMouseDownCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(MouseDownCommandProperty, value);
        }

        // Using a DependencyProperty as the backing store for MouseDownCommand.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MouseDownCommandProperty =
            DependencyProperty.RegisterAttached("MouseDownCommand", typeof(ICommand), typeof(MouseBehaviour), new FrameworkPropertyMetadata(MouseDownCommandPropertyChanged));

        private static void MouseDownCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement fe = d as FrameworkElement;
            if (fe != null)
            {
                fe.MouseDown += FrameworkElement_MouseDown;
            }
        }

        static void FrameworkElement_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FrameworkElement fe = sender as FrameworkElement;
            if (fe != null)
            {
                ICommand command = GetMouseDownCommand(fe);
                if (command != null)
                {
                    // Learning:
                    // We can send MouseButtonEventArgs as the parameter to ICommand
                    // However, that would require the ViewModel to have reference to PresenationCore (to access the class MouseButtonEventArgs)
                    // And then again something for Point class
                    // And the deal breaker would be that e.GetPosition requires the FrameworkElement relative to which it returns the point
                    // The ViewModel must absolutely not deal with the UI Element !
                    // So, what we are going to do instead is to extract the information we need in the ViewModel here and send it as a parameter instead
                    // That would be the Point class (mouse pointer position where mouse down occured)
                    // Note that Point also requires WindowsBase.dll, though .. but we're gonna live with it .. no problem
                    //if (command.CanExecute(e))
                    //{
                    //    command.Execute(e);
                    //}

                    //DelegateUndoCommand<object> delegateUndoCommand = command as DelegateUndoCommand<object>;
                    //if (delegateUndoCommand != null)
                    //{

                    //}
                    Point point = e.GetPosition(fe);
                    if (point != null)
                    {
                        if (command.CanExecute(point))
                        {
                            //UndoManager.UndoManager.Instance.Execute(delegateUndoCommand);
                            command.Execute(point);
                            e.Handled = true; // This is important !
                        }
                    }
                }
            }
        }

        
    }
}
