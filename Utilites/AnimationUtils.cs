using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Animation;

namespace WpfControls.Utilites
{
  public static class AnimationUtils
  {
    public static Storyboard CreateLeftPropertyStoryboard(this FrameworkElement frameworkElement, double from, double to, double milliseconds)
    {
      Storyboard storyboard = new Storyboard();

      DoubleAnimation animation = new DoubleAnimation();
      animation.From = from;
      animation.To = to;
      animation.Duration = new Duration(TimeSpan.FromMilliseconds(milliseconds));

      storyboard.Children.Add(animation);

      Storyboard.SetTarget(animation, frameworkElement);
      Storyboard.SetTargetProperty(animation, new PropertyPath(Canvas.LeftProperty));

      return storyboard;
    }
  }
}
