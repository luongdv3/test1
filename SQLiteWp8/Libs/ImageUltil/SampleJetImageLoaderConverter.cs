using JetImageLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using SQLiteWp8.Libs.ImageUltil;

namespace SQLiteWp8.Libs.ImageUltil
{
    public class SampleJetImageLoaderConverter : BaseJetImageLoaderConverter
    {
        protected override JetImageLoaderConfig GetJetImageLoaderConfig()
        {
            return SampleJetImageLoaderImpl.GetJetImageLoaderConfig();
        }

        public override object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
           
            // you can do some manipulations with image url (value arguement)
           object data = base.Convert(value, targetType, parameter, culture);
                   
            return data;
        }
    }
}
