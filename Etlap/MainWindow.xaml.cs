using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace Etlap
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private EtelekServices etelekServices;
		private Etelek EtelekToUpdate;
		public MainWindow()
		{
			InitializeComponent();
			etelekServices = new EtelekServices();
			Read();
		}

		private void Read()
		{
			etlapTable.ItemsSource = etelekServices.GetAll();
			etlapTable.AutoGenerateColumns = false;
			DataGridTextColumn textColumn1 = new DataGridTextColumn();
			DataGridTextColumn textColumn2 = new DataGridTextColumn();
			DataGridTextColumn textColumn3 = new DataGridTextColumn();
			textColumn1.Header = "Név";
			textColumn1.Binding = new Binding("Nev");
			etlapTable.Columns.Add(textColumn1);
			textColumn2.Header = "Ár";
			textColumn2.Binding = new Binding("Ar");
			etlapTable.Columns.Add(textColumn2);
			textColumn3.Header = "Kategoria";
			textColumn3.Binding = new Binding("Kategoria");
			etlapTable.Columns.Add(textColumn3);
			
			

			
		}
		private void ReadUpdate()
		{
			etlapTable.ItemsSource = etelekServices.GetAll();
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			Etelek selected = etlapTable.SelectedItem as Etelek;
			if(selected == null)
			{
				MessageBox.Show("Törléshez előbb válasszon ki egy elemet!");
				return;
			}
			MessageBoxResult clickedButton = MessageBox.Show($"Biztos hogy törölni akarod a kiválasztott elemet?:{selected.Nev}", "Törlés", MessageBoxButton.YesNo);
			if(clickedButton == MessageBoxResult.Yes)
			{
				if (etelekServices.Delete(selected.Id))
				{
					MessageBox.Show("Sikeres törlés!");
				}
				else
				{
					MessageBox.Show("Hiba történt a törlés során!");
				}
				Read();
			}
		}


		private void SzazalekButton_Click(object sender, RoutedEventArgs e)
		{
			int szazelek = Convert.ToInt32(szazelekTextBox.Text)/100+1;

				Etelek selected = etlapTable.SelectedItem as Etelek;
				if (selected==null)
				{
					etelekServices.UpdateAll(szazelek);
					ReadUpdate();

				}
				else
				{
					
				}
		
		}

	




	}
}
