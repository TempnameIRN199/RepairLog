using RepairLog_Server.Database;
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
using Newtonsoft.Json;
using System.IO;
using System.Diagnostics;

namespace RepairLogServer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public Context _context { get; set; }

        public List<Device> Devices { get; set; }
        public List<Breakdown> Breakdowns { get; set; }
        public List<Repair> Repairs { get; set; }
        public List<Repaired> Repaireds { get; set; }
        public List<Non_repairable> Non_repairables { get; set; }

        public Device device = new Device();
        public Breakdown breakdown = new Breakdown();
        public Repair repair = new Repair();
        public Repaired repaired = new Repaired();
        public Non_repairable non_repairable = new Non_repairable();

        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        public void LoadData()
        {
            _context = new Context();
        }

        private void _comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_comboBox.SelectedIndex == 0)
            {
                Devices = _context.Devices.AsNoTracking().ToList();
                _listView.ItemsSource = null;
                _gridView.Columns.Clear();
                _gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Name",
                    DisplayMemberBinding = new Binding("DevName"),
                    Width = 150
                });
                _gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Is Working",
                    DisplayMemberBinding = new Binding("IsWorking"),
                    Width = 150
                });
                _listView.ItemsSource = Devices;
            }
            else if (_comboBox.SelectedIndex == 1)
            {
                _listView.ItemsSource = null;
                _gridView.Columns.Clear();
                _gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Description",
                    DisplayMemberBinding = new Binding("Description"),
                    Width = 150
                });
                _gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Cause",
                    DisplayMemberBinding = new Binding("Cause"),
                    Width = 150
                });
                _gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Device",
                    DisplayMemberBinding = new Binding("Device.DevName"),
                    Width = 150
                });
                Breakdowns = _context.Breakdowns.AsNoTracking().ToList();
                for (int i = 0; i < Breakdowns.Count; i++)
                {
                    var breakd = Breakdowns.ToList()[i];
                    breakd.Device = _context.Devices.Where(d => d.Id == breakd.DeviceId).FirstOrDefault();
                }
                _listView.ItemsSource = Breakdowns.ToList();
            }
            else if (_comboBox.SelectedIndex == 2)
            {
                _listView.ItemsSource = null;
                _gridView.Columns.Clear();
                _gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Device",
                    DisplayMemberBinding = new Binding("Device.DevName"),
                    Width = 150
                });
                _gridView.Columns.Add(new GridViewColumn
                {
                    Header = "StartDate",
                    DisplayMemberBinding = new Binding("StartDate"),
                    Width = 105
                });
                _gridView.Columns.Add(new GridViewColumn
                {
                    Header = "EndDate",
                    DisplayMemberBinding = new Binding("EndDate"),
                    Width = 105
                });
                _gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Status",
                    DisplayMemberBinding = new Binding("Status"),
                    Width = 150
                });

                Repairs = _context.Repairs.AsNoTracking().ToList();
                for (int i = 0; i < Repairs.Count; i++)
                {
                    var repair = Repairs.ToList()[i];
                    repair.Device = _context.Devices.Where(d => d.Id == repair.DeviceId).FirstOrDefault();
                }
                _listView.ItemsSource = Repairs.ToList();
            }
            else if (_comboBox.SelectedIndex == 3)
            {
                _listView.ItemsSource = null;
                _gridView.Columns.Clear();
                _gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Device",
                    DisplayMemberBinding = new Binding("Device.DevName"),
                    Width = 150
                });
                Repaireds = _context.Repaireds.AsNoTracking().ToList();
                for (int i = 0; i < Repaireds.Count; i++)
                {
                    var repairds = Repaireds.ToList()[i];
                    repairds.Device = _context.Devices.Where(d => d.Id == repairds.DeviceId).FirstOrDefault();
                }
                _listView.ItemsSource = Repaireds.ToList();
            }
            else if (_comboBox.SelectedIndex == 4)
            {
                _listView.ItemsSource = null;
                _gridView.Columns.Clear();
                _gridView.Columns.Add(new GridViewColumn
                {
                    Header = "Device",
                    DisplayMemberBinding = new Binding("Device.DevName"),
                    Width = 150
                });
                Non_repairables = _context.Non_repairables.AsNoTracking().ToList();
                for (int i = 0; i < Non_repairables.Count; i++)
                {
                    var non_repairds = Non_repairables.ToList()[i];
                    non_repairds.Device = _context.Devices.Where(d => d.Id == non_repairds.DeviceId).FirstOrDefault();
                }
                _listView.ItemsSource = Non_repairables.ToList();
            }
        }

        private void _btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void _btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (_comboBox.SelectedIndex == 0)
            {
                if (_listView.SelectedItem != null)
                {
                    foreach (Device item in _listView.SelectedItems)
                    {
                        device = _context.Devices.Find(item.Id);
                    }
                    _context.Devices.Remove(device);
                    _context.SaveChanges();
                }
                _comboBox_SelectionChanged(null, null);
            }
            else if (_comboBox.SelectedIndex == 1)
            {
                if (_listView.SelectedItem != null)
                {
                    foreach (Breakdown item in _listView.SelectedItems)
                    {
                        breakdown = _context.Breakdowns.Find(item.Id);
                    }
                    _context.Breakdowns.Remove(breakdown);
                    _context.SaveChanges();
                }
                _comboBox_SelectionChanged(null, null);
            }
            else if (_comboBox.SelectedIndex == 2)
            {
                if (_listView.SelectedItem != null)
                {
                    foreach (Repair item in _listView.SelectedItems)
                    {
                        repair = _context.Repairs.Find(item.Id);
                    }
                    _context.Repairs.Remove(repair);
                    _context.SaveChanges();
                }
                _comboBox_SelectionChanged(null, null);
            }
            else if (_comboBox.SelectedIndex == 3)
            {
                if (_listView.SelectedItem != null)
                {
                    foreach (Repaired item in _listView.SelectedItems)
                    {
                        repaired = _context.Repaireds.Find(item.Id);
                    }
                    _context.Repaireds.Remove(repaired);
                    _context.SaveChanges();
                }
                _comboBox_SelectionChanged(null, null);
            }
            else if (_comboBox.SelectedIndex == 4)
            {
                if (_listView.SelectedItem != null)
                {
                    foreach (Non_repairable item in _listView.SelectedItems)
                    {
                        non_repairable = _context.Non_repairables.Find(item.Id);
                    }
                    _context.Non_repairables.Remove(non_repairable);
                    _context.SaveChanges();
                }
                _comboBox_SelectionChanged(null, null);
            }
        }

        private void _listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void _listView_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource is ScrollViewer)
            {
                _listView.UnselectAll();
            }
        }

        private void CleanJsonFile(string filePath)
        {
            try
            {
                string originalContent = File.ReadAllText(filePath);
                string cleanedContent = originalContent.Replace("\"", "");
                string modifiedContent = "\"" + cleanedContent + "\"";
                File.WriteAllText(filePath, modifiedContent);
                Console.WriteLine("Файл успешно отредактирован.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Произошла ошибка: " + ex.Message);
            }
        }

        private void _bntLog_Click(object sender, RoutedEventArgs e)
        {
            string tempFilePath = @"Logs\log_temp.txt";
            CleanJsonFile(@"Logs\log.json");
            using (StreamReader file = File.OpenText(@"Logs\log.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                try
                {
                    var log = (string)serializer.Deserialize(file, typeof(string));
                    File.WriteAllText(tempFilePath, log);
                }
                catch (JsonException ex)
                {
                    Console.WriteLine("Error during JSON deserialization: " + ex.Message);
                }
            }
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = $"/k type \"{tempFilePath}\"";
            process.Start();
            process.WaitForExit();
        }
    }
}
