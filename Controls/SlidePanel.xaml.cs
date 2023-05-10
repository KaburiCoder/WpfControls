using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using WpfControls.Utilites;

namespace WpfControls.Controls
{
  public partial class SlidePanel : UserControl
  {
    private Border? _slider;
    private SliderState _sliderState = SliderState.Closed;

    public static readonly DependencyProperty SliderWidthProperty =
        DependencyProperty.Register("SliderWidth", typeof(double), typeof(SlidePanel), new PropertyMetadata(300d, OnSliderChanged));
    public static readonly DependencyProperty SliderLocationProperty =
        DependencyProperty.Register("SliderLocation", typeof(SliderLocation), typeof(SlidePanel), new PropertyMetadata(SliderLocation.Left, OnSliderChanged));

    /// <summary>
    /// SliderWidth, SliderLocation 속성 변경 시 slider의 left위치 변경
    /// </summary>
    private static void OnSliderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      if (d is SlidePanel slidePanel)
      {
        slidePanel.ChangeSliderLeft();
      }
    }

    /// <summary>
    /// slidePanel의 부모 너비 반환
    /// </summary>
    private double ParentActualWidth
    {
      get
      {
        FrameworkElement? parent = Parent as FrameworkElement;
        return parent != null ? parent.ActualWidth : 0;
      }
    }

    /// <summary>
    /// slider의 애니메이션 호출 메서드
    /// </summary>
    private void BeginAnimation()
    {
      (double from, double to) leftRange = GetLeftRange();
      Storyboard storyboard = _slider!.CreateLeftPropertyStoryboard(from: leftRange.from, to: leftRange.to, milliseconds: AnimationSpeed);
      storyboard.Completed += Storyboard_Completed;
      storyboard.Begin();
    }

    /// <summary>
    /// slider의 애니메이션 완료 이벤트
    /// </summary>
    private void Storyboard_Completed(object? sender, EventArgs e)
    {
      if (_sliderState == SliderState.Opening)
      {
        _sliderState = SliderState.Opened;
      }
      else
      {
        _sliderState = SliderState.Closed;
        this.SendToBack();
      }
    }

    /// <summary>
    /// 배경 마우스 클릭 시 슬라이더 close이벤트
    /// </summary>
    private void opacityGrid_MouseUp(object sender, MouseButtonEventArgs e)
    {
      Close();
    }

    /// <summary>
    /// 슬라이더 left 설정 메서드
    /// </summary>
    private void ChangeSliderLeft()
    {
      if (_slider != null)
      {
        double left = SliderLocation == SliderLocation.Left ? -SliderWidth : ParentActualWidth;
        Canvas.SetLeft(_slider, left);
      }
    }

    /// <summary>
    /// slidePanel의 속성에 따른 애니메이션 Left의 from, to값 반환 메서드
    /// </summary>
    private (double from, double to) GetLeftRange()
    {
      if (SliderLocation == SliderLocation.Left)
      {
        if (_sliderState == SliderState.Opening)
        {
          return (from: -SliderWidth, to: 0);
        }
        else
        {
          return (from: 0, to: -SliderWidth);
        }
      }
      else
      {
        if (_sliderState == SliderState.Opening)
        {
          return (from: ParentActualWidth, to: ParentActualWidth - SliderWidth);
        }
        else
        {
          return (from: ParentActualWidth - SliderWidth, to: ParentActualWidth);
        }
      }
    }

    /// <summary>
    /// slidePanel 로드
    /// </summary>
    private void SlidePanel_Loaded(object sender, RoutedEventArgs e)
    {
      this.SendToBack();
      ChangeSliderLeft();
    }

    /// <summary>
    /// template 객체 찾기 및 할당
    /// </summary>
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();

      _slider = (Border)GetTemplateChild("slider");
    }

    /// <summary>
    /// 생성자
    /// </summary>
    public SlidePanel()
    {
      InitializeComponent();
      Loaded += SlidePanel_Loaded;
    }

    /// <summary>
    /// slider의 너비 속성
    /// </summary>
    public double SliderWidth
    {
      get { return (double)GetValue(SliderWidthProperty); }
      set { SetValue(SliderWidthProperty, value); }
    }

    /// <summary>
    /// 슬라이더 애니메이션 위치 Left : 좌측에서 우측방향, Right : 우측에서 좌측방향
    /// </summary>
    public SliderLocation SliderLocation
    {
      get { return (SliderLocation)GetValue(SliderLocationProperty); }
      set { SetValue(SliderLocationProperty, value); }
    }

    /// <summary>
    /// slider애니메이션 속도(milliseconds)
    /// </summary>
    public double AnimationSpeed { get; set; } = 300d;

    /// <summary>
    /// 슬라이더 열기
    /// </summary>
    public void Open()
    {
      if (_sliderState != SliderState.Closed) return;

      _sliderState = SliderState.Opening;
      BeginAnimation();
      this.BringToFront();
    }

    /// <summary>
    /// 슬라이더 닫기
    /// </summary>
    public void Close()
    {
      if (_sliderState != SliderState.Opened) return;

      _sliderState = SliderState.Closing;
      BeginAnimation();
    }
  }
}
