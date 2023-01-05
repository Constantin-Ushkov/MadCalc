using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace MadCalc
{
    internal class InputState
    {
        public static string FilePath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "inputs.json");

        public Dictionary<string, string> TextBoxes { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, decimal> UpDowns { get; set; } = new Dictionary<string, decimal>();
        public Dictionary<string, int> ComboBoxes { get; set; } = new Dictionary<string, int>();

        public List<int> Courses { get; set; } = new List<int>();
        public List<Wheel> Wheels { get; set; } = new List<Wheel>();
        public List<Driver> Drivers { get; set; } = new List<Driver>();
        public List<SpareParts> SpareParts { get; set; } = new List<SpareParts>();

        public void SaveState(Form form, IEnumerable<Wheel> wheels, IEnumerable<Driver> drivers, IEnumerable<SpareParts> spareParts)
        {
            try
            {
                TextBoxes.Clear();
                UpDowns.Clear();
                ComboBoxes.Clear();

                Wheels.Clear();
                Drivers.Clear();
                SpareParts.Clear();

                Wheels.AddRange(wheels);
                Drivers.AddRange(drivers);
                SpareParts.AddRange(spareParts);

                var textBoxes = GetTextBoxes(form);
                var upDowns = GetUpDowns(form);
                var comboBoxes = GetComboBoxes(form);

                foreach (var box in textBoxes)
                {
                    if (!string.IsNullOrEmpty(box.Name))
                    {
                        TextBoxes.Add(box.Name, box.Text);
                    }
                }

                foreach (var upDown in upDowns)
                {
                    if (!string.IsNullOrEmpty(upDown.Name))
                    {
                        UpDowns.Add(upDown.Name, upDown.Value);
                    }
                }

                foreach (var box in comboBoxes)
                {
                    if (!string.IsNullOrEmpty(box.Name))
                    {
                        ComboBoxes.Add(box.Name, box.SelectedIndex);
                    }
                }

                var serializer = new JsonSerializer();

                using (var streamWriter = new StreamWriter(FilePath))
                using (var jsonWriter = new JsonTextWriter(streamWriter))
                {
                    serializer.Serialize(jsonWriter, this);
                }
            }
            catch (Exception ex)
            {
                // todo: log
            }
        }

        public static InputState TryLoadState(Form form)
        {
            try
            {
                if (!File.Exists(FilePath))
                {
                    return null;
                }

                var serializer = new JsonSerializer();

                using (var streamReader = new StreamReader(FilePath))
                using (var jsonReader = new JsonTextReader(streamReader))
                {
                    var state = serializer.Deserialize(jsonReader, typeof(InputState)) as InputState;
                    state.UpdateControls(form);

                    return state;
                }
            }
            catch (Exception ex)
            {
                // todo: log
                return null;
            }
        }

        private void UpdateControls(Form form)
        {
            foreach (var box in TextBoxes)
            {
                var controls = form.Controls.Find(box.Key, true);

                if (controls.Any() && controls.First() is TextBox textBox)
                {
                    textBox.Text = box.Value;
                }
            }

            foreach (var upDown in UpDowns)
            {
                var controls = form.Controls.Find(upDown.Key, true);

                if (controls.Any() && controls.First() is NumericUpDown numericUpDown)
                {
                    numericUpDown.Value = upDown.Value;
                }
            }

            foreach (var box in ComboBoxes)
            {
                var controls = form.Controls.Find(box.Key, true);

                if (controls.Any() && controls.First() is ComboBox comboBox)
                {
                    comboBox.SelectedIndex = box.Value;
                }
            }
        }

        private static IEnumerable<TextBox> GetTextBoxes(Control control)
        {
            var list = new List<TextBox>();

            foreach (var child in control.Controls)
            {
                if (child is Control childControl)
                {
                    if (childControl is TextBox textBox)
                    {
                        list.Add(textBox);
                    }

                    GetTextBoxes(childControl, list);
                }
            }

            return list;
        }

        private static IEnumerable<NumericUpDown> GetUpDowns(Control control)
        {
            var list = new List<NumericUpDown>();

            foreach (var child in control.Controls)
            {
                if (child is Control childControl)
                {
                    if (childControl is NumericUpDown upDown)
                    {
                        list.Add(upDown);
                    }

                    GetUpDowns(childControl, list);
                }
            }

            return list;
        }

        private static IEnumerable<ComboBox> GetComboBoxes(Control control)
        {
            var list = new List<ComboBox>();

            foreach (var child in control.Controls)
            {
                if (child is Control childControl)
                {
                    if (childControl is ComboBox box)
                    {
                        list.Add(box);
                    }

                    GetComboBoxes(childControl, list);
                }
            }

            return list;
        }

        private static void GetTextBoxes(Control control, List<TextBox> list)
        {
            foreach (var child in control.Controls)
            {
                if (child is Control childControl)
                {
                    if (childControl is TextBox textBox)
                    {
                        list.Add(textBox);
                    }

                    GetTextBoxes(childControl, list);
                }
            }
        }

        private static void GetUpDowns(Control control, List<NumericUpDown> list)
        {
            foreach (var child in control.Controls)
            {
                if (child is Control childControl)
                {
                    if (childControl is NumericUpDown upDown)
                    {
                        list.Add(upDown);
                    }

                    GetUpDowns(childControl, list);
                }
            }
        }

        private static void GetComboBoxes(Control control, List<ComboBox> list)
        {
            foreach (var child in control.Controls)
            {
                if (child is Control childControl)
                {
                    if (childControl is ComboBox box)
                    {
                        list.Add(box);
                    }

                    GetComboBoxes(childControl, list);
                }
            }
        }
    }
}
