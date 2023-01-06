using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace MadCalc
{
    public partial class MainForm : Form
    {
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

            uiDriversGrid.DataSource = _drivers;
            _drivers.ListChanged += OnDriversListChanged;

            uiSparePartsGrid.DataSource = _spareParts;
            _spareParts.ListChanged += OnSparePartsListChanged;

            _state = InputState.TryLoadState(this);

            if (_state == null)
            {
                _state = new InputState();
            }

            _state.Wheels.ForEach(wheel => _wheels.Add(wheel));
            _state.Drivers.ForEach(driver => _drivers.Add(driver));
            _state.SpareParts.ForEach(sparePart => _spareParts.Add(sparePart));

            if (_state.Courses.Any())
            {
                UpdateCoursesReport();
                UpdateTransportReport();
                UpdateWheelsReport();
                UpdateDriverReport();
                UpdateSparePartsReport();
                UpdateCarCheckReport();
            }

            /*
            if (courses != null)
            {
                _courses.AddRange(courses);

                UpdateCoursesReport();
                UpdateTransportReport();
                UpdateWheelsReport();
                UpdateDriverReport();
                UpdateSparePartsReport();
                UpdateCarCheckReport();
            }
            */

            _loadingInputState = false;

            uiOilConsumtionTotal.Text = ToString(ParseFloat(uiFuelConsumption100.Text) * GetOilConsumptionPercent());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _state.SaveState(this, _wheels, _drivers, _spareParts); 
        }

        #region Transport UI Handlers

        private void uiComboBoxMechType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            switch(uiComboBoxMechType.SelectedIndex)
            {
                case 0:
                    uiVelocityText.Text = 40.ToString();
                    break;

                case 1:
                    uiVelocityText.Text = 25.ToString();
                    break;
            }
        }

        private void uiCarCountUd_ValueChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
        }

        private void uiVelocityText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
        }

        private void uiUnloadTime_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
        }

        private void uiCargoCapacityText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
        }

        private void uiFuelConsumption100_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateOilConsumption100kmText();
        }

        private void uiFuelPrice_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
        }

        private void uiOilConsumptionPercent_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateOilConsumption100kmText();
        }

        private void uiOilConsumtionTotal_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
        }

        private void uiOilPrice_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
        }

        private void uiWinter_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
        }

        private void uiAge_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
        }

        private void uiAmmortizationPerHour_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
        }

        #endregion // Transport UI Handlers

        #region Wheels UI Handlers

        private void uiWheelsGrid_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

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

                // sum += count;

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

        private void OnWheelsListChanged(object sender, ListChangedEventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            uiWheelsSpendingsText.Text = ToString(GetWheelsTotalCost());
        }

        private void uiDeleteWheelsBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in uiWheelsGrid.SelectedRows)
            {
                uiWheelsGrid.Rows.RemoveAt(row.Index);
            }
        }

        private void uiWheelKms_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateWheelsReport();
        }

        private void uiWheelsSpendingsText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateWheelsReport();
        }

        #endregion // Wheels UI Handlers

        #region Driver UI Handlers

        private void OnDriversListChanged(object sender, ListChangedEventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            var totalClass = 0u;
            var totalExp = 0u;

            foreach (var driver in _drivers)
            {
                totalClass += driver.ClassAdd;
                totalExp += driver.ExpirienceAdd;
            }

            var carCount = (uint)uiCarCountUd.Value;
            var classAverage = totalClass > 0 ? (float)totalClass / carCount : 0f;
            var expAverage = totalExp > 0 ? (float)totalExp / carCount : 0f;

            uiDriverClassAverageText.Text = ToString(classAverage);
            uiDriverExpAverageText.Text = ToString(expAverage);

            var salaryHour = GetDriverSalaryHour(classAverage, expAverage);

            uiDriverSalaryHourly.Text = ToString(salaryHour);
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
        }

        private void uiDeleteDriverBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in uiDriversGrid.SelectedRows)
            {
                uiDriversGrid.Rows.RemoveAt(row.Index);
            }
        }

        private void uiDriverTarif_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
        }

        private void uiDriverTrailerAddText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
        }

        private void uiDriverSpecialGearAdd_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
        }

        private void uiDriverPremiumAddText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
        }

        private void uiDriverEnsuranceAddText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
        }

        private void uiDriverHolydaysAddText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
        }

        private void uiDriverExpAverageText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
        }

        private void uiDriverClassAverageText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
        }

        private void uiDriverSalaryHourly_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
        }

        #endregion // Driver UI Handlers

        #region Car Check UI Handlers

        private void uiCarCheckCU_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
        }

        private void uiCarCheckCT_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
        }

        private void uiCarCheckRT_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
        }

        private void uiCarCheckCuTrailerAd_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
        }

        private void uiCarCheckCtTrailerAdd_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
        }

        private void uiCarCheckRtTrailerAdd_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
        }

        private void uiCarCheckTarif_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
        }

        private void uiCarCheckAvgSalary_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
        }

        #endregion // Car Check UI Handlers

        #region Spare Parts UI Handlers

        private void OnSparePartsListChanged(object sender, ListChangedEventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            var averge = 0f;

            foreach (var sparePart in _spareParts)
            {
                averge += sparePart.Price;
            }

            if (averge > 0f)
            {
                averge /= (uint)uiCarCountUd.Value;
            }

            uiSparePartsAverage.Text = ToString(averge);
        }

        private void uiSparePartsDeleteBtn_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in uiSparePartsGrid.SelectedRows)
            {
                uiSparePartsGrid.Rows.RemoveAt(row.Index);
            }
        }

        private void uiSparePartsYearlyRegime_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateSparePartsReport();
        }

        private void uiSparePartsAverage_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateSparePartsReport();
        }

        #endregion // Spare Parts UI Handlers

        private void uiAddCourseBtn_Click(object sender, EventArgs e)
        {
            using (var dlg = new AddCoursesDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    _state.Courses.Clear();
                    _state.Courses.AddRange(dlg.Courses);

                    UpdateCoursesReport();
                    UpdateTransportReport();
                    UpdateWheelsReport();
                    UpdateDriverReport();
                    UpdateSparePartsReport();
                    UpdateCarCheckReport();
                }
            }
        }

        private void UpdateCoursesReport()
        {
            uiCourses.Items.Clear();

            foreach (var course in _state.Courses)
            {
                var distance = course;
                var numberOfTrips = CalcNumberOfTrips(distance, out var hours);
                var totalLength = numberOfTrips * 2 * distance;

                var item = new ListViewItem(ToString(distance));

                item.SubItems.Add(ToString(numberOfTrips));
                item.SubItems.Add(ToString(totalLength));
                item.SubItems.Add(ToString(hours));

                uiCourses.Items.Add(item);
            }
        }

        private void UpdateTransportReport()
        {
            uiTransportListView.Items.Clear();

            var fuelFactor = GetFuelFactor();

            foreach (var course in _state.Courses)
            {
                var distance = course;
                var numberOfTrips = CalcNumberOfTrips(distance, out var hours);
                var totalLength = numberOfTrips * 2 * distance;
                var totalCargo = numberOfTrips * ParseFloat(uiCargoCapacityText.Text);
                var totalFuel = totalLength * GetFuelConsumption1km();

                if (fuelFactor > 0.001f)
                {
                    totalFuel += totalFuel * fuelFactor;
                }

                var totalOil = totalFuel * ParseFloat(uiOilConsumtionTotal.Text);
                var totalFuelPrice = totalFuel * ParseFloat(uiFuelPrice.Text);
                var totalOilPrice = totalOil * ParseFloat(uiOilPrice.Text);
                var carAmmortization = hours * ParseFloat(uiAmmortizationPerHour.Text);

                var item = new ListViewItem(ToString(distance));

                item.SubItems.Add(numberOfTrips.ToString());
                item.SubItems.Add(ToString(totalLength));
                item.SubItems.Add(ToString(hours));
                item.SubItems.Add(ToString(totalCargo));
                item.SubItems.Add(ToString(totalFuel));
                item.SubItems.Add(ToString(totalOil));
                item.SubItems.Add(ToString(totalFuelPrice));
                item.SubItems.Add(ToString(totalOilPrice));
                item.SubItems.Add(ToString(carAmmortization));

                uiTransportListView.Items.Add(item);
            }
        }

        private void UpdateWheelsReport()
        {
            uiWheelsListView.Items.Clear();

            foreach (var course in _state.Courses)
            {
                var distance = course;
                var numberOfTrips = CalcNumberOfTrips(distance, out var hours);
                var totalLength = numberOfTrips * 2 * distance;
                var wheelsAmortization = ParseFloat(uiWheelsSpendingsText.Text) / ParseFloat(uiWheelKms.Text) / (float)uiCarCountUd.Value * totalLength;

                var item = new ListViewItem(distance.ToString());

                item.SubItems.Add(numberOfTrips.ToString());
                item.SubItems.Add(ToString(totalLength));
                item.SubItems.Add(ToString(hours));
                item.SubItems.Add(ToString(wheelsAmortization));

                uiWheelsListView.Items.Add(item);
            }
        }

        private void UpdateDriverReport()
        {
            uiDriverListView.Items.Clear();

            foreach (var course in _state.Courses)
            {
                var distance = course;
                var numberOfTrips = CalcNumberOfTrips(distance, out var hours);
                var totalLength = numberOfTrips * 2 * distance;
                var totalSalary = hours * ParseFloat(uiDriverSalaryHourly.Text);

                var item = new ListViewItem(distance.ToString());

                item.SubItems.Add(numberOfTrips.ToString());
                item.SubItems.Add(ToString(totalLength));
                item.SubItems.Add(ToString(hours));
                item.SubItems.Add(ToString(totalSalary));

                uiDriverListView.Items.Add(item);
            }
        }

        private void UpdateSparePartsReport()
        {
            uiSparePartsListView.Items.Clear();

            if (string.IsNullOrEmpty(uiSparePartsAverage.Text))
            {
                return;
            }

            foreach (var course in _state.Courses)
            {
                var distance = course;
                var numberOfTrips = CalcNumberOfTrips(distance, out var hours);
                var totalLength = numberOfTrips * 2 * distance;
                var cost = (ParseFloat(uiSparePartsAverage.Text) / ParseFloat(uiSparePartsYearlyRegime.Text)) * hours;

                var item = new ListViewItem(distance.ToString());

                item.SubItems.Add(numberOfTrips.ToString());
                item.SubItems.Add(ToString(totalLength));
                item.SubItems.Add(ToString(hours));
                item.SubItems.Add(ToString(cost));

                uiSparePartsListView.Items.Add(item);
            }
        }

        private void UpdateCarCheckReport()
        {
            uiCarCheckListView.Items.Clear();

            foreach (var course in _state.Courses)
            {
                var distance = course;
                var numberOfTrips = CalcNumberOfTrips(distance, out var hours);
                var totalLength = numberOfTrips * 2 * distance;
                var cu = (ParseFloat(uiCarCheckCU.Text) + ParseFloat(uiCarCheckCuTrailerAd.Text)) / 8f * hours;
                var ct = (ParseFloat(uiCarCheckCT.Text) + ParseFloat(uiCarCheckCtTrailerAdd.Text)) * totalLength / 1000f;
                var rt = (ParseFloat(uiCarCheckRT.Text) + ParseFloat(uiCarCheckRtTrailerAdd.Text)) * totalLength / 1000f;
                var salaryHour = GetDriverSalaryHour();
                var salaryHourFixing = GetDriverSalaryFixingHour();
                var averageSalary = ParseFloat(uiCarCheckAvgSalary.Text); // (salaryHour + salaryHourFixing) / 2f;
                var totalSalary = (cu + ct + rt) * averageSalary;

                // uiCarCheckAvgSalary.Text = ToString(averageSalary);

                var item = new ListViewItem(distance.ToString());

                item.SubItems.Add(totalLength.ToString());
                item.SubItems.Add(ToString(hours));
                item.SubItems.Add(ToString(cu));
                item.SubItems.Add(ToString(ct));
                item.SubItems.Add(ToString(rt));
                item.SubItems.Add(ToString(totalSalary));

                uiCarCheckListView.Items.Add(item);
            }
        }

        /* remove selected course
        private void uiDeleteCourseBtn_Click(object sender, EventArgs e)
        {
            if (uiCourses.SelectedItems.Count > 0)
            {
                uiCourses.Items.Remove(uiCourses.SelectedItems[0]); // todo: allow to remove range
            }
        }
        */

        private void uiClearCoursesBtn_Click(object sender, EventArgs e)
        {
            uiCourses.Items.Clear();
            uiTransportListView.Items.Clear();
            uiWheelsListView.Items.Clear();
            uiDriverListView.Items.Clear();
            uiCarCheckListView.Items.Clear();
            uiSparePartsListView.Items.Clear();
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
            var velocity = ParseFloat(uiVelocityText.Text);
            return (float)(length * 2 * tripCount / velocity + tripCount * ParseFloat(uiUnloadTime.Text));
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

        private float GetOilConsumptionPercent()
        {
            return ParseFloat(uiOilConsumptionPercent.Text.EndsWith("%") ? uiOilConsumptionPercent.Text.Substring(0, uiOilConsumptionPercent.Text.Length - 1) : uiOilConsumptionPercent.Text) * 0.01f;
        }

        private void UpdateOilConsumption100kmText()
        {
            uiOilConsumtionTotal.Text = ToString(ParseFloat(uiFuelConsumption100.Text) * GetOilConsumptionPercent());
        }

        private float GetFuelConsumption1km()
        {
            return ParseFloat(uiFuelConsumption100.Text) / 100;
        }

        private float GetFuelFactor()
        {
            var winter = ParseFloat(uiWinter.Text);
            var age = ParseFloat(uiAge.Text);

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

        private float GetDriverSalaryHour()
        {
            return GetDriverSalaryHour(
                ParseFloat(uiDriverClassAverageText.Text),
                ParseFloat(uiDriverExpAverageText.Text));
        }

        private float GetDriverSalaryHour(float classAverage, float expAverage)
        {
            var additionAdd = ParseFloat(uiDriverTrailerAddText.Text);
            var specialGear = ParseFloat(uiDriverSpecialGearAdd.Text);
            var holydaysAdd = ParseFloat(uiDriverHolydaysAddText.Text);
            var ensurance = ParseFloat(uiDriverEnsuranceAddText.Text);
            var premium = ParseFloat(uiDriverPremiumAddText.Text);

            return ParseFloat(uiDriverTarif.Text)
                * (1 + (additionAdd + specialGear + classAverage + expAverage + premium) / 100)
                * (1 + holydaysAdd / 100)
                * (1 + ensurance / 100);
        }

        private float GetDriverSalaryFixingHour()
        {
            var holydaysAdd = ParseFloat(uiDriverHolydaysAddText.Text);
            var ensurance = ParseFloat(uiDriverEnsuranceAddText.Text);
            var expAverage = ParseFloat(uiDriverExpAverageText.Text);

            return ParseFloat(uiCarCheckTarif.Text)
                * (1 + expAverage / 100)
                * (1 + holydaysAdd / 100)
                * (1 + ensurance / 100);
        }

        private float CalcCarCheckAverageSalary()
        {
            var salaryHour = GetDriverSalaryHour();
            var salaryHourFixing = GetDriverSalaryFixingHour();

            return (salaryHour + salaryHourFixing) / 2f;
        }

        private static float ParseFloat(string text)
        {
            var str = text.Contains(",") ? text.Replace(',', '.') : text;
            return float.Parse(str, CultureInfo.InvariantCulture);
        }

        private static string ToString(float value)
        {
            return value.ToString("0.00");
        }

        private InputState _state;
        private BindingList<Wheel> _wheels = new BindingList<Wheel>() { AllowEdit = true, AllowNew = true };
        private BindingList<Driver> _drivers = new BindingList<Driver>() { AllowEdit = true, AllowNew = true };
        private BindingList<SpareParts> _spareParts = new BindingList<SpareParts>() { AllowEdit = true, AllowNew = true };
        private bool _loadingInputState = true;
    }
}
