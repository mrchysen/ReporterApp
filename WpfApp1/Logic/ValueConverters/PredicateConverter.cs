﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Diagnostics;
using KrasTechMontacApplication.Logic.Cars;


namespace WpfApp1.Logic.ValueConverters
{
    public class PredicatConverter : IValueConverter
    {
        protected Predicate<Car> Predicate;
        protected string TruePredicateText;
        protected string FalsePredicateText;


        public PredicatConverter(Predicate<Car> predicate, string TruePredicateText, string FalsePredicateText)
        {
            Predicate = predicate;
            this.TruePredicateText = TruePredicateText;
            this.FalsePredicateText = FalsePredicateText;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (Predicate((Car)(value))) ? TruePredicateText : FalsePredicateText;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
