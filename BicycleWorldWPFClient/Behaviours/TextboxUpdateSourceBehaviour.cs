using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Data;
using System.Windows;
using System.Windows.Controls;

namespace BicycleWorldWPFClient
{
    public class TextboxUpdateSourceBehaviour : Behavior<TextBox>
    {
        BindingExpression binding;
        private static Type behaviourType = typeof(TextboxUpdateSourceBehaviour);

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject is TextBox)
            {
                (AssociatedObject as TextBox).TextChanged += ControlUpdateSourceBehavior_Update;
                binding = AssociatedObject.GetBindingExpression(TextBox.TextProperty);
            }
        }

        protected override void OnDetaching()
        {
            if (AssociatedObject is TextBox)
            {
                (AssociatedObject as TextBox).TextChanged -= ControlUpdateSourceBehavior_Update;
            }

            binding = null;

            base.OnDetaching();
        }

        void ControlUpdateSourceBehavior_Update(object sender, RoutedEventArgs e)
        {
            if (binding != null)
            {
                binding.UpdateSource();
            }
        }
    }
}