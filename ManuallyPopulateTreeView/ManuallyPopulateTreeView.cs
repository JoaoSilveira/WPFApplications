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

namespace ManuallyPopulateTreeView
{
    class ManuallyPopulateTreeView : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new ManuallyPopulateTreeView());
        }

        public ManuallyPopulateTreeView()
        {
            Title = "Manually Populate TreeView";

            var tree = new TreeView();
            Content = tree;

            var itemAnimal = new TreeViewItem();
            itemAnimal.Header = "Animal";
            tree.Items.Add(itemAnimal);

            var itemDog = new TreeViewItem();
            itemDog.Header = "Dog";
            itemDog.Items.Add("Poodle");
            itemDog.Items.Add("Irish Setter");
            itemDog.Items.Add("German Shepherd");
            itemAnimal.Items.Add(itemDog);

            var itemCat = new TreeViewItem();
            itemCat.Header = "Cat";
            itemCat.Items.Add("Calico");

            var item = new TreeViewItem();
            item.Header = "Alley Cat";
            itemCat.Items.Add(item);

            var btn = new Button();
            btn.Content = "Noodles";
            itemCat.Items.Add(btn);

            itemCat.Items.Add("Siamese");
            itemAnimal.Items.Add(itemCat);

            var itemPrimate = new TreeViewItem();
            itemPrimate.Header = "Primate";
            itemPrimate.Items.Add("Chimpanzee");
            itemPrimate.Items.Add("Bonobo");
            itemPrimate.Items.Add("Human");
            itemAnimal.Items.Add(itemPrimate);

            var itemMineral = new TreeViewItem();
            itemMineral.Header = "Mineral";
            itemMineral.Items.Add("Calcium");
            itemMineral.Items.Add("Zinc");
            itemMineral.Items.Add("Iron");
            tree.Items.Add(itemMineral);

            var itemVegetable = new TreeViewItem();
            itemVegetable.Header = "Vegetable";
            itemVegetable.Items.Add("Carrot");
            itemVegetable.Items.Add("Aspargus");
            itemVegetable.Items.Add("Broccoli");
            tree.Items.Add(itemVegetable);
        }
    }
}
