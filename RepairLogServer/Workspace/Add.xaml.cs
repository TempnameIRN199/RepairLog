using RepairLog_Server.Database;
using RepairLogServer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RepairLogServer.Workspace
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Window
    {
        public NintendoContext _context;
        public int _opps;
        public Iraqaholic iraq = new Iraqaholic();

        public Add(NintendoContext db, int item)
        {
            InitializeComponent();
            LoadData(db, item);
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_opps == 0)
            {
                TextBox name = (TextBox)_grid.Children[1];
                ComboBox isWorking = (ComboBox)_grid.Children[3];

                Device device = new Device
                {
                    DevName = name.Text,
                    IsWorking = isWorking.SelectedIndex == 0 ? true : false
                };

                _context.Devices.Add(device);
                _context.SaveChanges();
            }
            else if (_opps == 1)
            {
                TextBox description = (TextBox)_grid.Children[1];
                TextBox cause = (TextBox)_grid.Children[3];
                ComboBox device = (ComboBox)_grid.Children[5];

                Breakdown breakdown = new Breakdown
                {
                    Description = description.Text,
                    Cause = cause.Text,
                    DeviceId = device.SelectedIndex + 1
                };

                _context.Breakdowns.Add(breakdown);
                _context.SaveChanges();
            }
            else if (_opps == 2)
            {
                ComboBox device = (ComboBox)_grid.Children[1];
                DatePicker startDate = (DatePicker)_grid.Children[3];
                DatePicker endDate = (DatePicker)_grid.Children[5];
                ComboBox status = (ComboBox)_grid.Children[7];

                Repair repair = new Repair
                {
                    DeviceId = device.SelectedIndex + 1,
                    StartDate = startDate.SelectedDate.Value,
                    EndDate = endDate.SelectedDate.Value,
                    Status = (Statused)status.SelectedIndex
                };

                _context.Repairs.Add(repair);
                _context.SaveChanges();
            }
            else if (_opps == 3)
            {
                ComboBox device = (ComboBox)_grid.Children[1];

                Repaired repaired = new Repaired
                {
                    DeviceId = device.SelectedIndex + 1
                };

                _context.Repaireds.Add(repaired);
                _context.SaveChanges();
            }
            else if (_opps == 4)
            {
                ComboBox device = (ComboBox)_grid.Children[1];

                Non_repairable repaired = new Non_repairable
                {
                    DeviceId = device.SelectedIndex + 1
                };

                _context.Non_repairables.Add(repaired);
                _context.SaveChanges();
            }
            this.Close();
        }

        private void CreateButton(string content, int row)
        {
            Button btn = new Button();
            Grid.SetRow(btn, row);
            btn.Content = content;
            btn.Margin = new Thickness(10, 10, 10, 10);
            btn.HorizontalAlignment = HorizontalAlignment.Stretch;
            btn.VerticalAlignment = VerticalAlignment.Center;
            btn.FontSize = 20;
            btn.FontFamily = new FontFamily("Cascadia Mono ExtraLight");
            btn.Click += Save_Click;
            _grid.Children.Add(btn);
        }

        public void LoadData(NintendoContext db, int item)
        {
            _context = db;

            if (item == 0)
            {
                _grid.Children.Clear();
                List<string> strings = new List<string> { "True", "False" };
                _opps = item;
                this.Width = 300;
                this.Height = 300;
                iraq.CreateRow(_grid, 5);
                iraq.CreateLabel(_grid, "Name", 0);
                iraq.CreateTextBox(_grid, 1);
                iraq.CreateLabel(_grid, "Is Working", 2);
                iraq.CreateComboBox(_grid, 3);
                iraq.FillComboBox((ComboBox)_grid.Children[3], strings);
                CreateButton("Save", 4);
            }
            else if (item == 1)
            {
                _grid.Children.Clear();
                List<Device> devices = db.Devices.ToList();
                _opps = item;
                this.Width = 300;
                this.Height = 400;
                iraq.CreateRow(_grid, 7);
                iraq.CreateLabel(_grid, "Description", 0);
                iraq.CreateTextBox(_grid, 1);
                iraq.CreateLabel(_grid, "Cause", 2);
                iraq.CreateTextBox(_grid, 3);
                iraq.CreateLabel(_grid, "Device", 4);
                iraq.CreateComboBox(_grid, 5);
                iraq.FillComboBoxDevice((ComboBox)_grid.Children[5], devices);
                CreateButton("Save", 6);
            }
            else if (item == 2)
            {
                _grid.Children.Clear();
                List<Device> devices = db.Devices.ToList();
                List<string> statuses = new List<string> { "InProgress", "Completed", "Canceled", "Dropped" };
                _opps = item;
                this.Width = 300;
                this.Height = 600;
                iraq.CreateRow(_grid, 9);
                iraq.CreateLabel(_grid, "Device", 0);
                iraq.CreateComboBox(_grid, 1);
                iraq.FillComboBoxDevice((ComboBox)_grid.Children[1], devices);
                iraq.CreateLabel(_grid, "Start Date", 2);
                iraq.CreateDatePicker(_grid, 3);
                iraq.CreateLabel(_grid, "End Date", 4);
                iraq.CreateDatePicker(_grid, 5);
                iraq.CreateLabel(_grid, "Status", 6);
                iraq.CreateComboBox(_grid, 7);
                iraq.FillComboBox((ComboBox)_grid.Children[7], statuses);
                CreateButton("Save", 8);
            }
            else if (item == 3)
            {
                _grid.Children.Clear();
                List<Device> devices = db.Devices.ToList();
                _opps = item;
                this.Width = 300;
                this.Height = 250;
                iraq.CreateRow(_grid, 3);
                iraq.CreateLabel(_grid, "Device", 0);
                iraq.CreateComboBox(_grid, 1);
                iraq.FillComboBoxDevice((ComboBox)_grid.Children[1], devices);
                CreateButton("Save", 2);
            }
            else if (item == 4)
            {
                _grid.Children.Clear();
                List<Device> devices = db.Devices.ToList();
                _opps = item;
                this.Width = 300;
                this.Height = 250;
                iraq.CreateRow(_grid, 3);
                iraq.CreateLabel(_grid, "Device", 0);
                iraq.CreateComboBox(_grid, 1);
                iraq.FillComboBoxDevice((ComboBox)_grid.Children[1], devices);
                CreateButton("Save", 2);
            }
        }
    }
}
