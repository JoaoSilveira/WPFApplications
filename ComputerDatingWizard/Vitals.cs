using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace ComputerDatingWizard
{
    public class Vitals
    {
        public string Name;
        public string Home;
        public string Gender;
        public string FavoriteOS;
        public string Directory;
        public string MomsMaidenName;
        public string Pet;
        public string Income;

        public static RadioButton GetCheckedRadioButton(GroupBox grpbox)
        {
            if (grpbox.Content is Panel pnl)
                foreach (UIElement el in pnl.Children)
                    if (el is RadioButton radio && radio.IsChecked.GetValueOrDefault())
                        return radio;

            return null;
        }
    }
}
