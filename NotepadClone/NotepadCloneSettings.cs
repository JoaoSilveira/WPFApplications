using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Xml.Serialization;

namespace NotepadClone
{
    public class NotepadCloneSettings
    {
        public WindowState WindowState = WindowState.Normal;
        public Rect RestoreBounds = Rect.Empty;
        public TextWrapping TextWrapping = TextWrapping.NoWrap;
        public string FontFamily = string.Empty;
        public string FontStyle = new FontStyleConverter().ConvertToString(FontStyles.Normal);
        public string FontWeight = new FontWeightConverter().ConvertToString(FontWeights.Normal);
        public string FontStretch = new FontStretchConverter().ConvertToString(FontStretches.Normal);
        public double FontSize = 11;

        public virtual bool Save(string strAppData)
        {
            try
            {
                Directory.CreateDirectory(Path.GetDirectoryName(strAppData));
                using (var write = new StreamWriter(strAppData))
                {
                    new XmlSerializer(GetType()).Serialize(write, this);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public static T Load<T>(string strAppData)
        {
            try
            {
                using(var reader = new StreamReader(strAppData))
                {
                    return (T)new XmlSerializer(typeof(T)).Deserialize(reader);
                }
            }
            catch
            {
                return (T)typeof(T).GetConstructor(Type.EmptyTypes).Invoke(null);
            }
        }
    }
}
