using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MadCalc
{
    public partial class MainForm : Form
    {
        public class Wheel
        {
            [DisplayName("Колличество")]
            public uint Count { get; set; } // no more than car_count * 10
            [DisplayName("Стоимость (штука)")]
            public float Cost { get; set; }
            [DisplayName("Дата")]
            public DateTime Date { get; set; } // no more than current date
        }

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            uiComboBoxMechType.Items.Clear();
            uiComboBoxMechType.Items.AddRange(new object[] { "Автомобиль", "Трактор" });

            uiComboBoxMechType.SelectedIndex = 0;
            uiComboBoxMechModel.SelectedIndex = 0;

            uiWheelsGrid.DataSource = _wheels;

            _wheels.ListChanged += OnWheelsListChanged;
            // _wheels.AddingNew += OnWheelsAddingNew;
        }

        private void uiComboBoxMechType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // todo: move this to logic!
            // set speed, for cars - 40, for tracktors - 25

            switch(uiComboBoxMechType.SelectedIndex)
            {
                case 0:
                    uiUpDownVelocity.Value = 40;
                    break;

                case 1:
                    uiUpDownVelocity.Value = 25;
                    break;
            }
        }

        private void uiAddCourseBtn_Click(object sender, EventArgs e)
        {
            using (var dlg = new AddCoursesDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    UpdateCoursesReport(dlg.Courses);
                    UpdateTransportReport(dlg.Courses);
                }
            }
        }

        private void UpdateCoursesReport(IEnumerable<int> courses)
        {
            var fuelFactor = GetFuelFactor();

            foreach (var course in courses)
            {
                var distance = course;
                var numberOfTrips = CalcNumberOfTrips(distance, out var hours);
                var totalLength = numberOfTrips * 2 * distance;
                var totalCargo = numberOfTrips * uiUpDownCargoCapacity.Value;
                var totalFuel = totalLength * GetFuelConsumption1km();

                if (fuelFactor > 0.001f)
                {
                    totalFuel += totalFuel * fuelFactor;
                }

                var totalOil = totalFuel * GetOilCOnsumptionPercent(); //GetOilConsumption(totalLength);
                var totalFuelPrice = totalFuel * float.Parse(uiFuelPrice.Text);
                var totalOilPrice = totalOil * float.Parse(uiOilPrice.Text);
                var wheelsAmortization = float.Parse(uiWheelSpendings.Text) / float.Parse(uiWheelKms.Text) / (float)uiCarCountUd.Value * totalLength;
                var carAmmortization = hours * float.Parse(uiAmmortizationPerHour.Text);

                // todo: uiWheelKms.Text - check for 0

                var item = new ListViewItem(distance.ToString());

                item.SubItems.Add(numberOfTrips.ToString());
                item.SubItems.Add(totalLength.ToString());
                item.SubItems.Add(totalCargo.ToString());
                item.SubItems.Add(totalFuel.ToString());
                item.SubItems.Add(totalOil.ToString());
                item.SubItems.Add(totalFuelPrice.ToString());
                item.SubItems.Add(totalOilPrice.ToString());
                item.SubItems.Add(wheelsAmortization.ToString());
                item.SubItems.Add(carAmmortization.ToString());
                item.SubItems.Add(hours.ToString());

                uiCourses.Items.Add(item);
            }
        }

        private void UpdateTransportReport(IEnumerable<int> courses)
        {
            var fuelFactor = GetFuelFactor();

            foreach (var course in courses)
            {
                var distance = course;
                var numberOfTrips = CalcNumberOfTrips(distance, out var hours);
                var totalLength = numberOfTrips * 2 * distance;
                var totalCargo = numberOfTrips * uiUpDownCargoCapacity.Value;
                var totalFuel = totalLength * GetFuelConsumption1km();

                if (fuelFactor > 0.001f)
                {
                    totalFuel += totalFuel * fuelFactor;
                }

                var totalOil = totalFuel * GetOilCOnsumptionPercent();
                var totalFuelPrice = totalFuel * float.Parse(uiFuelPrice.Text);
                var totalOilPrice = totalOil * float.Parse(uiOilPrice.Text);

                var item = new ListViewItem(distance.ToString());

                item.SubItems.Add(numberOfTrips.ToString());
                item.SubItems.Add(totalLength.ToString());
                item.SubItems.Add(totalCargo.ToString());
                item.SubItems.Add(totalFuel.ToString());
                item.SubItems.Add(totalOil.ToString());
                item.SubItems.Add(totalFuelPrice.ToString());
                item.SubItems.Add(totalOilPrice.ToString());

                uiTransportListView.Items.Add(item);
            }
        }

        private void uiDeleteCourseBtn_Click(object sender, EventArgs e)
        {
            if (uiCourses.SelectedItems.Count > 0)
            {
                uiCourses.Items.Remove(uiCourses.SelectedItems[0]); // todo: allow to remove range
            }
        }

        private void uiClearCoursesBtn_Click(object sender, EventArgs e)
        {
            uiCourses.Items.Clear();
        }

        private int CalcNumberOfTrips(int length, out float hours)
        {
            var count = 0;
            var time = 0f;

            hours = 1;

            do
            {
                time = GetTripTime(length, count);

                if (time <= 8.05f)
                {
                    count++;
                    hours = time;
                }
            }
            while (time <= 8.05f);

            return count;
        }

        private float GetTripTime(float length, int tripCount)
        {
            // t<=8, где t=( l*2*r/40+r* Traz), r это кол-во рейсов
            return (float)(length * 2 * tripCount / 40 + tripCount * float.Parse(uiUnloadTime.Text));
        }

        private void uiReportTestBtn_Click(object sender, EventArgs e)
        {
            var font1 = new Font("Arial", 10);
            //var font2 = new Font("Arial", 20);

            using (var doc = new PrintDocument())
            using (var dialog = new PrintDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                doc.PrintPage += (printSender, printArgs) =>
                {
                    printArgs.Graphics.Clear(Color.White);

                    for (var i=0; i < 100; ++i)
                    {
                        printArgs.Graphics.DrawString($"String #{i}", font1, Brushes.Black, new PointF(0, 12 * i));
                    }
                };

                doc.Print();
            }
        }

        private void uiOilConsumptionPercent_TextChanged(object sender, EventArgs e)
        {
            UpdateFuelConsumption100kmText();
        }

        private float GetOilCOnsumptionPercent()
        {
            return float.Parse(uiOilConsumptionPercent.Text.EndsWith("%") ? uiOilConsumptionPercent.Text.Substring(0, uiOilConsumptionPercent.Text.Length - 1) : uiOilConsumptionPercent.Text) * 0.01f;
        }

        private void uiFuelConsumption100_TextChanged(object sender, EventArgs e)
        {
            UpdateFuelConsumption100kmText();
        }

        private void UpdateFuelConsumption100kmText()
        {
            uiOilConsumtionTotal.Text = (float.Parse(uiFuelConsumption100.Text) * GetOilCOnsumptionPercent()).ToString();
        }

        private float GetFuelConsumption1km()
        {
            return float.Parse(uiFuelConsumption100.Text) / 100;
        }

        private float GetFuelFactor()
        {
            var winter = float.Parse(uiWinter.Text);
            var age = float.Parse(uiAge.Text);

            return (winter + age) / 100;
        }

        private float GetWheelsTotalCost()
        {
            var cost = 0f;

            foreach (var wheel in _wheels)
            {
                cost += wheel.Count * wheel.Cost;
            }

            return cost;
        }

        private void OnWheelsListChanged(object sender, ListChangedEventArgs e)
        {
            uiWheelSpendings.Text = GetWheelsTotalCost().ToString();
        }

        private BindingList<Wheel> _wheels = new BindingList<Wheel>() { AllowEdit = true, AllowNew = true };

        private void uiWheelsGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            var str = uiWheelsGrid[e.ColumnIndex, e.RowIndex].EditedFormattedValue.ToString();

            if (string.IsNullOrEmpty(str))
            {
                return;
            }

            if (e.ColumnIndex == 0) // wheel count
            {
                var count = uint.Parse(str);
                var sum = 0u;

                foreach (var wheel2 in _wheels)
                {
                    sum += wheel2.Count;
                }

                sum += count;

                if (sum > uiCarCountUd.Value * 10)
                {
                    MessageBox.Show(
                        $"Слишком большое колличество колес {sum}. Ожидается значение меньше или равное {uiCarCountUd.Value * 10}.",
                            "Не верные данные",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                    e.Cancel = true;
                }
            }
            else if (e.ColumnIndex == 2) // date
            {
                try
                {
                    var date = DateTime.Parse(str);

                    if (date >= DateTime.Now)
                    {
                        MessageBox.Show(
                            $"Не верная дата {str}. Дата должна быть меньше сегодняшней.",
                            "Не верные данные",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        e.Cancel = true;
                    }
                }
                catch
                {
                }
            }
        }
    }
}
