using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ExploreDependencyProperties
{
    public class MetadataToFlags : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FrameworkPropertyMetadataOptions flags = 0;
            var metadata = value as FrameworkPropertyMetadata;

            if (metadata == null)
                return null;

            if (metadata.AffectsMeasure)
                flags |= FrameworkPropertyMetadataOptions.AffectsMeasure;

            if (metadata.AffectsArrange)
                flags |= FrameworkPropertyMetadataOptions.AffectsArrange;

            if (metadata.AffectsParentMeasure)
                flags |= FrameworkPropertyMetadataOptions.AffectsParentMeasure;

            if (metadata.AffectsParentArrange)
                flags |= FrameworkPropertyMetadataOptions.AffectsParentArrange;

            if (metadata.AffectsRender)
                flags |= FrameworkPropertyMetadataOptions.AffectsRender;

            if (metadata.Inherits)
                flags |= FrameworkPropertyMetadataOptions.Inherits;

            if (metadata.IsNotDataBindable)
                flags |= FrameworkPropertyMetadataOptions.NotDataBindable;

            if (metadata.BindsTwoWayByDefault)
                flags |= FrameworkPropertyMetadataOptions.BindsTwoWayByDefault;

            if (metadata.Journal)
                flags |= FrameworkPropertyMetadataOptions.Journal;

            return flags;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return new FrameworkPropertyMetadata(null, (FrameworkPropertyMetadataOptions)value);
        }
    }
}
