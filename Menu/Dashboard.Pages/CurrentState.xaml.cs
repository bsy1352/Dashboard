using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Menu.Dashboard.Pages
{
    /// <summary>
    /// CurrentState.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CurrentState : UserControl, INotifyPropertyChanged
    {
        private double _value1;
        private double _value2;
        private double _value3;
        private double _value4;

        private double _lastLecture;
        private double _trend;

        public CurrentState()
        {
            InitializeComponent();

            Value1 = 0;
            Value2 = 0;
            Value3 = 0;
            Value4 = 0;

            DataContext = this;

            LastHourSeries = new SeriesCollection
            {
                new LineSeries
                {
                    AreaLimit = -10,
                    Values = new ChartValues<ObservableValue>
                    {
                        new ObservableValue(3),
                        new ObservableValue(5),
                        new ObservableValue(6),
                        new ObservableValue(7),
                        new ObservableValue(3),
                        new ObservableValue(4),
                        new ObservableValue(2),
                        new ObservableValue(5),
                        new ObservableValue(8),
                        new ObservableValue(3),
                        new ObservableValue(5),
                        new ObservableValue(6),
                        new ObservableValue(7),
                        new ObservableValue(3),
                        new ObservableValue(4),
                        new ObservableValue(2),
                        new ObservableValue(5),
                        new ObservableValue(8)
                    }
                }
            };
            _trend = 8;



            Task.Run(() =>
            {
                var r = new Random();
                while (true)
                {
                    Thread.Sleep(500);
                    _trend += (r.NextDouble() > 0.3 ? 1 : -1)*r.Next(0, 5);
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        LastHourSeries[0].Values.Add(new ObservableValue(_trend));
                        LastHourSeries[0].Values.RemoveAt(0);
                        SetLecture();
                    });
                }
            });

            DataContext = this;

            this.Loaded += (_sender, _e) =>
            {
                try
                {
                    Thread value1 = new Thread(ChangeValueOnClick);
                    Thread value = new Thread(AscendingNumber);
                    value1.Start();
                    value.Start();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    
                }
            };

            
        }

        public double Value1
        {
            get { return _value1; }
            set
            {
                _value1 = value;
                OnPropertyChanged("Value1");
            }
           
        }
        public double Value2
        {
            get { return _value2; }
            set
            {
                _value2 = value;
                OnPropertyChanged("Value2");
            }
            
        }
        public double Value3
        {
            get { return _value3; }
            set
            {
                _value3 = value;
                OnPropertyChanged("Value3");
            }
            
        }

        public double Value4
        {
            get { return _value4; }
            set
            {
                _value4 = value;
                OnPropertyChanged("Value4");
            }

        }

        private void ChangeValueOnClick()
        {
            while (true)
            {
                Value1 = new Random().Next(0, 100);
                Thread.Sleep(500);
                Value2 = new Random().Next(0, 100);
                Thread.Sleep(500);
                Value3 = new Random().Next(0, 100);
                Thread.Sleep(800);
            }
            
        }

        private void AscendingNumber()
        {
            while (true)
            {
                if(Value4 != 100)
                {
                    Value4++;
                    Thread.Sleep(3000);
                }
                else
                {
                    Value4 = 0;
                }
                

                
            }
        }
  

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public SeriesCollection LastHourSeries { get; set; }

        public double LastLecture
        {
            get { return _lastLecture; }
            set
            {
                _lastLecture = value;
                OnPropertyChanged("LastLecture");
            }
        }

        private void SetLecture()
        {
            var target = ((ChartValues<ObservableValue>)LastHourSeries[0].Values).Last().Value;
            var step = (target - _lastLecture) / 4;

            Task.Run(() =>
            {
                for (var i = 0; i < 4; i++)
                {
                    Thread.Sleep(100);
                    LastLecture += step;
                }
                LastLecture = target;
            });

        }


       
    }
}

