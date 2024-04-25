using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Data.Entity;
using RepairLog_Server.Database;

namespace RepairLogServer.Workspace
{
    public class Iraqaholic
    {
        public void CreateRow(Grid name, int count)
        {
            for (int i = 0; i < count; i++)
            {
                name.RowDefinitions.Add(new RowDefinition());
            }
        }

        public void CreateLabel(Grid name, string content, int row)
        {
            Label lbl = new Label();
            Grid.SetRow(lbl, row);
            lbl.Content = content;
            lbl.Margin = new Thickness(10, 10, 10, 10);
            lbl.HorizontalAlignment = HorizontalAlignment.Stretch;
            lbl.VerticalAlignment = VerticalAlignment.Center;
            lbl.FontSize = 20;
            lbl.FontFamily = new FontFamily("Cascadia Mono ExtraLight");
            name.Children.Add(lbl);
        }

        public void CreateTextBox(Grid name, int row)
        {
            TextBox _txtBox = new TextBox();
            Grid.SetRow(_txtBox, row);
            _txtBox.Margin = new Thickness(10, 10, 10, 10);
            _txtBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            _txtBox.VerticalAlignment = VerticalAlignment.Center;
            _txtBox.FontSize = 20;
            _txtBox.FontFamily = new FontFamily("Cascadia Mono ExtraLight");
            name.Children.Add(_txtBox);
        }

        public void CreateComboBox(Grid name, int row)
        {
            ComboBox _cmbBox = new ComboBox();
            Grid.SetRow(_cmbBox, row);
            _cmbBox.Margin = new Thickness(10, 10, 10, 10);
            _cmbBox.HorizontalAlignment = HorizontalAlignment.Stretch;
            _cmbBox.VerticalAlignment = VerticalAlignment.Center;
            _cmbBox.FontSize = 20;
            _cmbBox.FontFamily = new FontFamily("Cascadia Mono ExtraLight");
            name.Children.Add(_cmbBox);
        }

        public void CreateDatePicker(Grid name, int row)
        {
            DatePicker _datePicker = new DatePicker();
            Grid.SetRow(_datePicker, row);
            _datePicker.Margin = new Thickness(10, 10, 10, 10);
            _datePicker.HorizontalAlignment = HorizontalAlignment.Stretch;
            _datePicker.VerticalAlignment = VerticalAlignment.Center;
            _datePicker.FontSize = 20;
            _datePicker.FontFamily = new FontFamily("Cascadia Mono ExtraLight");
            name.Children.Add(_datePicker);
        }

        public void FillComboBox(ComboBox cmbBox, List<string> items)
        {
            foreach (var item in items)
            {
                cmbBox.Items.Add(item);
            }
        }

        public void FillComboBoxDevice(ComboBox cmbBox, List<Device> items)
        {
            foreach (var item in items)
            {
                cmbBox.Items.Add(item.DevName);
            }
        }
    }
}
