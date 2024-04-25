using RepairLog_Server.Database;
using RepairLogServer.Database;
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
using System.Windows.Shapes;

namespace RepairLogServer.Workspace
{
    /// <summary>
    /// Логика взаимодействия для Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        public NintendoContext _context;
        public int _opps;
        public Iraqaholic iraq = new Iraqaholic();

        Device devices = new Device();
        Breakdown breakdowns = new Breakdown();
        Repair repairs = new Repair();
        Repaired repaireds = new Repaired();
        Non_repairable non_Repairables = new Non_repairable();

        public Edit(NintendoContext db, int item, Device device = null, Breakdown breakdown = null,
            Repair repair = null, Repaired repaired = null, Non_repairable non_Repairable = null)
        {
            InitializeComponent();
            _context = db;
            if (item == 0 && device != null)
            {
                devices = device;
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
                ((TextBox)_grid.Children[1]).Text = devices.DevName;
                ((ComboBox)_grid.Children[3]).SelectedIndex = devices.IsWorking ? 0 : 1;
            }
            else if (item == 1 && breakdown != null)
            {
                breakdowns = breakdown;
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
                ((TextBox)_grid.Children[1]).Text = breakdowns.Description;
                ((TextBox)_grid.Children[3]).Text = breakdowns.Cause;
                ((ComboBox)_grid.Children[5]).SelectedIndex = breakdowns.DeviceId - 1;
            }
            else if (item == 2 && repair != null)
            {
                repairs = repair;
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
                ((ComboBox)_grid.Children[1]).SelectedIndex = repairs.DeviceId - 1;
                ((DatePicker)_grid.Children[3]).SelectedDate = repairs.StartDate;
                ((DatePicker)_grid.Children[5]).SelectedDate = repairs.EndDate;
                ((ComboBox)_grid.Children[7]).SelectedIndex = (int)repairs.Status;
            }
            else if (item == 3 && repaired != null)
            {
                repaireds = repaired;
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
                ((ComboBox)_grid.Children[1]).SelectedIndex = repaireds.DeviceId - 1;
            }
            else if (item == 4 && non_Repairable != null)
            {
                non_Repairables = non_Repairable;
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
                ((ComboBox)_grid.Children[1]).SelectedIndex = non_Repairables.DeviceId - 1;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_opps == 0)
            {
                devices.DevName = ((TextBox)_grid.Children[1]).Text;
                devices.IsWorking = ((ComboBox)_grid.Children[3]).SelectedIndex == 0 ? true : false;
                _context.Entry(devices).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                MessageBox.Show("Device has been updated");
                this.Close();
            }
            else if (_opps == 1)
            {
                breakdowns.Description = ((TextBox)_grid.Children[1]).Text;
                breakdowns.Cause = ((TextBox)_grid.Children[3]).Text;
                breakdowns.DeviceId = ((ComboBox)_grid.Children[5]).SelectedIndex + 1;
                _context.Entry(breakdowns).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                MessageBox.Show("Breakdown has been updated");
                this.Close();
            }
            else if (_opps == 2)
            {
                repairs.DeviceId = ((ComboBox)_grid.Children[1]).SelectedIndex + 1;
                repairs.StartDate = ((DatePicker)_grid.Children[3]).SelectedDate.Value;
                repairs.EndDate = ((DatePicker)_grid.Children[5]).SelectedDate.Value;
                repairs.Status = (Statused)((ComboBox)_grid.Children[7]).SelectedIndex;
                _context.Entry(repairs).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                MessageBox.Show("Repair has been updated");
                this.Close();
            }
            else if (_opps == 3)
            {
                repaireds.DeviceId = ((ComboBox)_grid.Children[1]).SelectedIndex + 1;
                _context.Entry(repaireds).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                MessageBox.Show("Repaired has been updated");
                this.Close();
            }
            else if (_opps == 4)
            {
                non_Repairables.DeviceId = ((ComboBox)_grid.Children[1]).SelectedIndex + 1;
                _context.Entry(non_Repairables).State = System.Data.Entity.EntityState.Modified;
                _context.SaveChanges();
                MessageBox.Show("Non-repairable has been updated");
                this.Close();
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
    }
}
