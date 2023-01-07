using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.Linq;
using System.Text;
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
                UpdateTotalReport();
            }

            _loadingInputState = false;

            uiOilConsumtionTotal.Text = ToString(ParseFloat(uiFuelConsumption100.Text) * GetOilConsumptionPercent());
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _state.SaveState(this, _wheels, _drivers, _spareParts); 
        }

        private void uiDauComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (uiDauComboBox.SelectedIndex)
            {
                case -1:
                    uiSectorsText.Text = String.Empty;
                    break;

                case 0: // S.A. "Drumuri Balti"
                    uiSectorsText.Text = "Sector Balti, Faleshti, Singerei";
                    break;

                case 1: // S.A. "Drumuri Cahul"
                    uiSectorsText.Text = "Sector Cahul, Cantemir, Taraclia";
                    break;

                case 2: // S.A. "Drumuri Causheni"
                    uiSectorsText.Text = "Sector Causheni, Cainari, Stefan Voda";
                    break;

                case 3: // S.A. "Drumuri Cimishlia"
                    uiSectorsText.Text = "Sector Cimislia, Basarabeasca, Leova";
                    break;

                case 4: // S.A. "Drumuri Comrat"
                    uiSectorsText.Text = "Sector Comrat, Ceadir-Lunga, Vulcaneshti";
                    break;

                case 5: // S.A. "Drumuri Criuleni"
                    uiSectorsText.Text = "Sector Criuleni, Anenii Noi, Chisinau, Dubasari";
                    break;

                case 6: // S.A. "Drumuri Edineti"
                    uiSectorsText.Text = "Sector Edineti, Dondiusheni, Ocnitsa";
                    break;

                case 7: // S.A. "Drumuri Briceni"
                    uiSectorsText.Text = String.Empty;
                    break;

                case 8: // S.A. "Drumuri Ialoveni"
                    uiSectorsText.Text = "Sector Ialoveni, Hincesti, Nisporeni";
                    break;

                case 9: // S.A. "Drumuri Orhei"
                    uiSectorsText.Text = "Sector Orhei, Rezina,Teleneshti";
                    break;

                case 10: // S.A. "Drumuri Rishcani"
                    uiSectorsText.Text = "Sector Rishcani, Drocia, Glodeni";
                    break;

                case 11: // S.A. "Drumuri Soroca"
                    uiSectorsText.Text = "Sector Soroca, Floreshti, Sanatauca, Shaldoneshti";
                    break;

                case 12: // S.A. "Drumuri Strasheni"
                    uiSectorsText.Text = "Sector Strasheni, Calarash, Ungheni";
                    break;
            }
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

        private void uiTrailerCb_CheckedChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            uiTrailerCapacity.Enabled = uiTrailerCb.Checked;
            uiTrailerWeight.Enabled = uiTrailerCb.Checked;

            UpdateTransportReport();
            UpdateTotalReport();
        }

        private void uiTrailerWeight_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
            UpdateTotalReport();
        }

        private void uiTrailerCapacity_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
            UpdateTotalReport();
        }

        private void uiCarCountUd_ValueChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
            UpdateWheelsReport();
            UpdateTotalReport();
        }

        private void uiVelocityText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
            UpdateTotalReport();
        }

        private void uiUnloadTime_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
            UpdateTotalReport();
        }

        private void uiCargoCapacityText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
            UpdateTotalReport();
        }

        private void uiFuelConsumption100_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateOilConsumption100kmText();
            UpdateTotalReport();
        }

        private void uiFuelPrice_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
            UpdateTotalReport();
        }

        private void uiOilConsumptionPercent_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateOilConsumption100kmText();
            UpdateTotalReport();
        }

        private void uiOilConsumtionTotal_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
            UpdateTotalReport();
        }

        private void uiOilPrice_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
            UpdateTotalReport();
        }

        private void uiWinter_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
            UpdateTotalReport();
        }

        private void uiAge_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
            UpdateTotalReport();
        }

        private void uiAmmortizationPerHour_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateTransportReport();
            UpdateTotalReport();
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
                        $"Слишком большое Количество колес {sum}. Ожидается значение меньше или равное {uiCarCountUd.Value * 10}.",
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
            UpdateTotalReport();
        }

        private void uiWheelsSpendingsText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateWheelsReport();
            UpdateTotalReport();
        }

        #endregion // Wheels UI Handlers

        #region Driver UI Handlers

        private void OnDriversListChanged(object sender, ListChangedEventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            UpdateTotalReport();
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
            UpdateTotalReport();
        }

        private void uiDriverTrailerAddText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
            UpdateTotalReport();
        }

        private void uiDriverSpecialGearAdd_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
            UpdateTotalReport();
        }

        private void uiDriverPremiumAddText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
            UpdateTotalReport();
        }

        private void uiDriverEnsuranceAddText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
            UpdateTotalReport();
        }

        private void uiDriverHolydaysAddText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
            UpdateTotalReport();
        }

        private void uiDriverExpAverageText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
            UpdateTotalReport();
        }

        private void uiDriverClassAverageText_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
            UpdateTotalReport();
        }

        private void uiDriverSalaryHourly_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateDriverReport();
            UpdateTotalReport();
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
            UpdateTotalReport();
        }

        private void uiCarCheckCT_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
            UpdateTotalReport();
        }

        private void uiCarCheckRT_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
            UpdateTotalReport();
        }

        private void uiCarCheckCuTrailerAd_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
            UpdateTotalReport();
        }

        private void uiCarCheckCtTrailerAdd_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
            UpdateTotalReport();
        }

        private void uiCarCheckRtTrailerAdd_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
            UpdateTotalReport();
        }

        private void uiCarCheckTarif_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
            uiCarCheckAvgSalary.Text = ToString(CalcCarCheckAverageSalary());
            UpdateTotalReport();
        }

        private void uiCarCheckAvgSalary_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateCarCheckReport();
            UpdateTotalReport();
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
            UpdateTotalReport();
        }

        private void uiSparePartsAverage_TextChanged(object sender, EventArgs e)
        {
            if (_loadingInputState)
            {
                return;
            }

            UpdateSparePartsReport();
            UpdateTotalReport();
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
                    UpdateTotalReport();
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
                var totalCargo = numberOfTrips * ParseFloat(uiCargoCapacityText.Text);

                var item = new ListViewItem(ToString(distance));

                item.SubItems.Add(ToString(numberOfTrips));
                item.SubItems.Add(ToString(totalLength));
                item.SubItems.Add(ToString(hours));
                item.SubItems.Add(ToString(totalCargo));

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
            UpdateDriverSalary();

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

        private void UpdateDriverSalary()
        {
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
                // var salaryHour = GetDriverSalaryHour();
                // var salaryHourFixing = GetDriverSalaryFixingHour();
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

        private void UpdateTotalReport()
        {
            uiTotalListView.Items.Clear();

            var row = 0;

            foreach (var course in _state.Courses)
            {
                var distance = course;
                var item = new ListViewItem(ToString(distance));

                var fuel = uiTransportListView.Items[row].SubItems[7].Text;
                var oil = uiTransportListView.Items[row].SubItems[8].Text;
                var wheels = uiWheelsListView.Items[row].SubItems[4].Text;

                item.SubItems.Add(fuel);
                item.SubItems.Add(oil);
                item.SubItems.Add(wheels);

                var driver = uiDriverListView.Items[row].SubItems[4].Text;
                var carCare = uiCarCheckListView.Items[row].SubItems[6].Text;
                var spareParts = uiSparePartsListView.Items[row].SubItems[4].Text;

                item.SubItems.Add(driver);
                item.SubItems.Add(carCare);
                item.SubItems.Add(spareParts);

                var amortization = uiTransportListView.Items[row].SubItems[9].Text;
                var totalCargo = uiTransportListView.Items[row].SubItems[4].Text;
                var tonCost = (ParseFloat(fuel)
                    + ParseFloat(oil)
                    + ParseFloat(wheels)
                    + ParseFloat(driver)
                    + ParseFloat(carCare)
                    + ParseFloat(spareParts)
                    + ParseFloat(amortization)) / ParseFloat(totalCargo);

                item.SubItems.Add(amortization);
                item.SubItems.Add(totalCargo);
                item.SubItems.Add(ToString(tonCost));

                uiTotalListView.Items.Add(item);

                row++;
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
            uiTotalListView.Items.Clear();
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
            var fuelUsage100km = ParseFloat(uiFuelConsumption100.Text);

            if (uiTrailerCb.Checked)
            {
                var trailerWeight = ParseFloat(uiTrailerWeight.Text);
                var trailerCargoCap = ParseFloat(uiTrailerCapacity.Text);

                if (trailerWeight > 0 && trailerCargoCap > 0)
                {
                    fuelUsage100km += (trailerWeight + trailerCargoCap / 2f) * 1.3f;
                }
            }

            return fuelUsage100km / 100f;
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

        /*
        private void PrintReport()
        {
            using (var doc = new PrintDocument())
            using (var preview = new PrintPreviewDialog())
            using (var dialog = new PrintDialog())
            {
                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                doc.PrintPage += OnPrintPage;
                doc.Print();
            }
        }

        private void OnPrintPage(object sender, PrintPageEventArgs args)
        {
            var font1 = new Font("Arial", 12);

            args.Graphics.Clear(Color.White);

            for (var i = 0; i < 100; ++i)
            {
                args.Graphics.DrawString($"String #{i}", font1, Brushes.Black, new PointF(0, 12 * i));
            }
        }
        */

        private void uiCreateReportBtn_Click(object sender, EventArgs e)
        {
            uiReportText.Clear();

            CreateGeneralReport();
            CreateCoursesReport();
            CreateMachineReport();
            CreateWheelsReport();
            CreateDriverReport();
            CreateCarCheckReport();
            CreateSparePartsReport();
            CreateAmortizationReport();
            CreateTotalsReport();
        }

        private void uiCopyReportBtn_Click(object sender, EventArgs e)
        {
            uiReportText.SelectAll();
            uiReportText.Copy();
            uiReportText.DeselectAll();
        }

        private void CreateGeneralReport()
        {
            uiReportText.AppendText(Environment.NewLine);

            uiReportText.SelectionFont = _reportFontBold;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;
            uiReportText.AppendText($"Calcul №: ___________________________ {Environment.NewLine}");

            uiReportText.AppendText(Environment.NewLine);
            uiReportText.AppendText($"S.A.: {uiDauComboBox.Text} \t\t Sector: {uiSectorsText.Text}\t\t Data: {DateTime.Now.ToShortDateString()}{Environment.NewLine}");

            uiReportText.AppendText(Environment.NewLine);
            uiReportText.AppendText($"Denumirea mehanizmelor: {uiTransportNameText.Text}{Environment.NewLine}");
            uiReportText.AppendText($"Capacitate de încărcare: {uiCargoCapacityText.Text}");

            if (uiTrailerCb.Checked)
            {
                uiReportText.AppendText($"\tGreutatea remorcii: {uiTrailerWeight.Text}\tCapacitatea remorcii: {uiTrailerCapacity.Text}{Environment.NewLine}");
            }
            else
            {
                uiReportText.AppendText(Environment.NewLine);
            }

            uiReportText.AppendText($"Numerul mehanizmelor: {uiCarCountUd.Value}{Environment.NewLine}");
            uiReportText.AppendText($"Nume materialelor: {uiCargoName.Text}{Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
        }

        private void CreateCoursesReport()
        {
            uiReportText.SelectionFont = _reportSectionFont;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;

            uiReportText.AppendText($"1. Marșruturi: {Environment.NewLine}{Environment.NewLine}");

            uiReportText.SelectionFont = _reportFontMono;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;

            //uiReportText.AppendText($"Distanța parcursă\t Numerul curselor\t Cursă totală\t Timpul parcursul\t Tonajul transporturilor{Environment.NewLine}");
            uiReportText.AppendText(string.Format("{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}{5}", "Distanța parcursă", "Numerul curselor", "Cursă totală", "Timpul parcursul", "Tonajul transporturilor", Environment.NewLine));

            foreach (ListViewItem item in uiCourses.Items)
            {
                //uiReportText.AppendText($"{item.SubItems[0].Text}\t\t\t {item.SubItems[1].Text}\t\t\t {item.SubItems[2].Text}\t\t {item.SubItems[3].Text}\t\t\t {item.SubItems[4].Text}{Environment.NewLine}");
                uiReportText.SelectionFont = _reportFontMono;
                uiReportText.SelectionColor = Color.Black;
                uiReportText.SelectionIndent = 0;

                uiReportText.AppendText(string.Format("{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}{5}", item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[4].Text, Environment.NewLine));
            }

            uiReportText.AppendText(Environment.NewLine);
        }

        private void CreateMachineReport()
        {
            uiReportText.SelectionFont = _reportSectionFont;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;

            uiReportText.AppendText($"2.1 Carburanța: {Environment.NewLine}{Environment.NewLine}");
            uiReportText.AppendText($"Carburanța ( 100км ): {uiFuelConsumption100.Text} \t\tIarna: {uiWinter.Text}% \t\tVîrsta: {uiAge.Text}% \t\tPreți (litr): {uiFuelPrice.Text} lei  ({uiFuelBillDate.Text}) {Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
            //uiReportText.AppendText($"Distanța parcursă\t Cursă totală\t Consumul combustibel\t Prețul motorină\t{Environment.NewLine}");
            uiReportText.AppendText(string.Format("{0,-24}{1,-24}{2,-24}{3}", "Cursă totală", "Consumul combustibel", "Prețul motorină", Environment.NewLine));

            foreach (ListViewItem item in uiTransportListView.Items)
            {
                //uiReportText.AppendText($"{item.SubItems[0].Text}\t\t\t {item.SubItems[2].Text}\t\t {item.SubItems[5].Text}\t\t\t {item.SubItems[7].Text}{Environment.NewLine}");
                uiReportText.AppendText(string.Format("{0,-24}{1,-24}{2,-24}{3}", item.SubItems[2].Text, item.SubItems[5].Text, item.SubItems[7].Text, Environment.NewLine));
            }

            uiReportText.AppendText(Environment.NewLine);

            uiReportText.SelectionFont = _reportSectionFont;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;

            uiReportText.AppendText($"2.2 Lubrifianți: {Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
            uiReportText.AppendText($"Consumul lubrifianți în % din combustibel: {uiOilConsumptionPercent.Text}\tPreți (litr): {uiOilPrice.Text} lei ({uiFuelBillDate.Text}) {Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
            //uiReportText.AppendText($"Distanța parcursă\t Consumul lubrifianți\t Prețul lubrifianți{Environment.NewLine}");
            uiReportText.AppendText(string.Format("{0,-24}{1,-24}{2,-24}{3}", "Distanța parcursă", "Consumul lubrifianți", "Prețul lubrifianți", Environment.NewLine));

            foreach (ListViewItem item in uiTransportListView.Items)
            {
                //uiReportText.AppendText($"{item.SubItems[0].Text}\t\t\t {item.SubItems[6].Text}\t\t\t {item.SubItems[8].Text}{Environment.NewLine}");
                uiReportText.AppendText(string.Format("{0,-24}{1,-24}{2,-24}{3}", item.SubItems[0].Text, item.SubItems[6].Text, item.SubItems[8].Text, Environment.NewLine));
            }

            uiReportText.AppendText(Environment.NewLine);
        }

        private void CreateWheelsReport()
        {
            uiReportText.SelectionFont = _reportSectionFont;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;

            uiReportText.AppendText($"3. Consumul de cauciucurilor: {Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
            uiReportText.AppendText($"Parcurs anvelopelor (км):{uiWheelKms.Text}{Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);

            uiReportText.AppendText($"Anvelopelor:{Environment.NewLine}");
            //uiReportText.AppendText($"Număr\t Prețuri (1 buc)\t Data instalarea{Environment.NewLine}");
            uiReportText.AppendText(string.Format("{0,-24}{1,-24}{2,-24}{3}", "Număr", "Prețuri (1 buc)", "Data instalarea", Environment.NewLine));

            foreach (DataGridViewRow row in uiWheelsGrid.Rows)
            {
                //uiReportText.AppendText($"{row.Cells[0].Value}\t {row.Cells[1].Value}\t\t {row.Cells[2].Value}{Environment.NewLine}");
                uiReportText.AppendText(string.Format("{0,-24}{1,-24}{2,-24}{3}", row.Cells[0].Value, row.Cells[1].Value, row.Cells[2].Value, Environment.NewLine));
            }

            uiReportText.AppendText($"Consumuri: {uiWheelsSpendingsText.Text} lei {Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
            //uiReportText.AppendText($"Distanța parcurs\t Cursă totală\t Amortizarea anvelop.{Environment.NewLine}");
            uiReportText.AppendText(string.Format("{0,-24}{1,-24}{2,-24}{3}", "Distanța parcurs", "Cursă totală", "Amortizarea anvelop.", Environment.NewLine));

            foreach (ListViewItem item in uiWheelsListView.Items)
            {
                //uiReportText.AppendText($"{item.SubItems[0].Text}\t\t\t {item.SubItems[2].Text}\t\t {item.SubItems[4].Text}{Environment.NewLine}");
                uiReportText.AppendText(string.Format("{0,-24}{1,-24}{2,-24}{3}", item.SubItems[0].Text, item.SubItems[2].Text, item.SubItems[4].Text, Environment.NewLine));
            }

            uiReportText.AppendText(Environment.NewLine);
        }

        private void CreateDriverReport()
        {
            uiReportText.SelectionFont = _reportSectionFont;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;

            uiReportText.AppendText($"4. Salariul mecanic: {Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
            uiReportText.AppendText($"Tarif: {uiDriverTarif.Text} \t\t\t\t\t");
            uiReportText.AppendText($"Premia: {uiDriverPremiumAddText.Text}{Environment.NewLine}");

            uiReportText.AppendText($"mediu adaos pentru clasa: {uiDriverClassAverageText.Text} \t\tadaos pentru remorca: {uiDriverTrailerAddText.Text} {Environment.NewLine}");
            uiReportText.AppendText($"mediu vechime în muncă: {uiDriverExpAverageText.Text}\t\tadaos pentru special: {uiDriverSpecialGearAdd.Text} {Environment.NewLine}");
            uiReportText.AppendText($"asigurarea socială: {uiDriverEnsuranceAddText.Text} \t\t\tconcediu suplimentar: {uiDriverHolydaysAddText.Text} {Environment.NewLine}");
            
            uiReportText.SelectionFont = _reportFontBold;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;
            uiReportText.AppendText($"Salariul în ora: {uiDriverSalaryHourly.Text}\tTimpul {uiUnloadTime.Text} ore ({uiLoadUnloadType.Text}) {Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);

            //uiReportText.AppendText($"Distanța parcursă\t Numerul curselor\t Cursa totală\t Timpul parcursul\t Salariu{Environment.NewLine}");
            uiReportText.AppendText(string.Format("{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}{5}", "Distanța parcurs", "Numerul curselor", "Cursa totală", "Timpul parcursul", "Salariu", Environment.NewLine));

            foreach (ListViewItem item in uiDriverListView.Items)
            {
                //uiReportText.AppendText($"{item.SubItems[0].Text}\t\t\t {item.SubItems[1].Text}\t\t\t {item.SubItems[2].Text}\t\t {item.SubItems[3].Text}\t\t\t {item.SubItems[4].Text}{Environment.NewLine}");
                uiReportText.AppendText(string.Format("{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}{5}", item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[4].Text, Environment.NewLine));
            }

            uiReportText.AppendText(Environment.NewLine);
        }

        private void CreateCarCheckReport()
        {
            uiReportText.SelectionFont = _reportSectionFont;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;

            uiReportText.AppendText($"5. Cheltuili pentru CT și RT: {Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
            uiReportText.AppendText($"Tarif de reparație: {uiCarCheckTarif.Text}{Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
            //uiReportText.AppendText($"Distanța parcursă\t Cursa totală\t Timpul parcurs\t\t CU\t\t CT\t\t RT\t\t Total{Environment.NewLine}");
            uiReportText.AppendText(string.Format("{0,-20}{1,-17}{2,-17}{3,-10}{4,-10}{5,-10}{6,-10}{7}", "Distanța parcurs", "Cursa totală", "Timpul parcurs", "CU", "CT", "RT", "Total", Environment.NewLine));

            foreach (ListViewItem item in uiCarCheckListView.Items)
            {
                //uiReportText.AppendText($"{item.SubItems[0].Text}\t\t\t {item.SubItems[1].Text}\t\t {item.SubItems[2].Text}\t\t\t {item.SubItems[3].Text}\t\t {item.SubItems[4].Text}\t\t {item.SubItems[5].Text}\t\t {item.SubItems[6].Text}{Environment.NewLine}");
                uiReportText.AppendText(string.Format("{0,-20}{1,-17}{2,-17}{3,-10}{4,-10}{5,-10}{6,-10}{7}", item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[4].Text, item.SubItems[5].Text, item.SubItems[6].Text, Environment.NewLine));
            }

            uiReportText.AppendText(Environment.NewLine);
        }

        private void CreateSparePartsReport()
        {
            uiReportText.SelectionFont = _reportSectionFont;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;

            var averageCost = ParseFloat(uiSparePartsAverage.Text) / ParseFloat(uiSparePartsYearlyRegime.Text);

            uiReportText.AppendText($"6. Cheltuieli pentru piese de schimb: {Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
            uiReportText.AppendText($"mediu cheltuieli piese de schimb: {averageCost.ToString("0.00")} lei\t\tRegim anual: {uiSparePartsYearlyRegime.Text} ore\\anul {Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
            //uiReportText.AppendText($"Distanța parcursă\t Numerul curselor\t Cursa totală\t Timpul parcurs\t Preți{Environment.NewLine}");
            uiReportText.AppendText(string.Format("{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}{5}", "Distanța parcurs", "Numerul curselor", "Cursa totală", "Timpul parcursul", "Preți", Environment.NewLine));

            foreach (ListViewItem item in uiSparePartsListView.Items)
            {
                //uiReportText.AppendText($"{item.SubItems[0].Text}\t\t\t {item.SubItems[1].Text}\t\t\t {item.SubItems[2].Text}\t\t {item.SubItems[3].Text}\t\t {item.SubItems[4].Text}{Environment.NewLine}");
                uiReportText.AppendText(string.Format("{0,-20}{1,-20}{2,-20}{3,-20}{4,-20}{5}", item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[4].Text, Environment.NewLine));
            }

            uiReportText.AppendText(Environment.NewLine);
        }

        private void CreateAmortizationReport()
        {
            uiReportText.SelectionFont = _reportSectionFont;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;

            uiReportText.AppendText($"7. Amortizarea: {Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
            uiReportText.AppendText($"Amortizarea: {uiAmmortizationPerHour.Text} lei\\ore{Environment.NewLine}");
            uiReportText.AppendText(Environment.NewLine);
            //uiReportText.AppendText($"Distanța parcursă\t Timpul parcurs\t Amortizarea{Environment.NewLine}");
            uiReportText.AppendText(string.Format("{0,-20}{1,-20}{2,-20}{3}", "Distanța parcurs", "Timpul parcursul", "Amortizarea", Environment.NewLine));

            foreach (ListViewItem item in uiTransportListView.Items)
            {
                //uiReportText.AppendText($"{item.SubItems[0].Text}\t\t\t {item.SubItems[3].Text}\t\t {item.SubItems[9].Text}{Environment.NewLine}");
                uiReportText.AppendText(string.Format("{0,-20}{1,-20}{2,-20}{3}", item.SubItems[0].Text, item.SubItems[3].Text, item.SubItems[9].Text, Environment.NewLine));
            }

            uiReportText.AppendText(Environment.NewLine);
        }

        private void CreateTotalsReport()
        {
            /*
            uiReportText.SelectionFont = _reportSectionFont;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;

            uiReportText.AppendText($"8. Total: {Environment.NewLine}{Environment.NewLine}");
            uiReportText.AppendText($"Dist-ța\tCarbu-ți\t\tLubr-ți\t\tAnvelop.\tSalariu\t\tCT și RT\tPiese\tAmort-ția\tTonaj total\tCostul 1tn{Environment.NewLine}");

            foreach (ListViewItem item in uiTotalListView.Items)
            {
                uiReportText.AppendText($"{item.SubItems[0].Text}\t{item.SubItems[1].Text}\t\t{item.SubItems[2].Text}\t\t{item.SubItems[3].Text}\t\t{item.SubItems[4].Text}\t\t{item.SubItems[5].Text}\t\t{item.SubItems[6].Text}\t{item.SubItems[7].Text}\t\t{item.SubItems[8].Text}\t\t{item.SubItems[9].Text}{Environment.NewLine}");
            }

            uiReportText.AppendText(Environment.NewLine);
            */

            uiReportText.SelectionFont = _reportSectionFont;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;

            uiReportText.AppendText($"8. Total: {Environment.NewLine}{Environment.NewLine}");

            //uiReportText.SelectionFont = _reportFontMono;
            //uiReportText.SelectionColor = Color.Black;
            //uiReportText.SelectionIndent = 0;

            //uiReportText.AppendText($"Dist-ța   Carbu-ți  Lubr-ți   Anvelop.  Salariu   CT și RT  Piese     Amort-ția Tonaj total  Costul 1tn{Environment.NewLine}");
            //uiReportText.AppendText(string.Format("{0,-10}{1,-10}{2,-10}{3,-10}{4,-10}{5,-10}{6,-10}{7,-10}{8,-10}{9,-10}{10}", "Dist-ța", "Carbu-ți", "Lubr-ți", "Anvelop.", "Salariu", "CT și RT", "Piese", "Amort-ția", "Ton. tot.", "Costul 1tn", Environment.NewLine));

            uiReportText.SelectionFont = _reportFontMonoBold;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;
            uiReportText.AppendText(string.Format("{0,-10}", "Dist-ța"));

            uiReportText.SelectionFont = _reportFontMono;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;
            uiReportText.AppendText(string.Format("{0,-10}{1,-10}{2,-10}{3,-10}{4,-10}{5,-10}{6,-10}{7,-10}", "Carbu-ți", "Lubr-ți", "Anvelop.", "Salariu", "CT și RT", "Piese", "Amort-ția", "Ton. tot."));

            uiReportText.SelectionFont = _reportFontMonoBold;
            uiReportText.SelectionColor = Color.Black;
            uiReportText.SelectionIndent = 0;
            uiReportText.AppendText(string.Format("{0,-10}{1}", "Costul 1tn", Environment.NewLine));

            foreach (ListViewItem item in uiTotalListView.Items)
            {
                /*
                uiReportText.SelectionFont = _reportFontMono;
                uiReportText.SelectionColor = Color.Black;
                uiReportText.SelectionIndent = 0;

                uiReportText.AppendText(string.Format("{0,-10}{1,-10}{2,-10}{3,-10}{4,-10}{5,-10}{6,-10}{7,-10}{8,-10}{9,-10}{10}", item.SubItems[0].Text, item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[4].Text, item.SubItems[5].Text, item.SubItems[6].Text, item.SubItems[7].Text, item.SubItems[8].Text, item.SubItems[9].Text, Environment.NewLine));
                */

                uiReportText.SelectionFont = _reportFontMonoBold;
                uiReportText.SelectionColor = Color.Black;
                uiReportText.SelectionIndent = 0;
                uiReportText.AppendText(string.Format("{0,-10}", item.SubItems[0].Text));

                uiReportText.SelectionFont = _reportFontMono;
                uiReportText.SelectionColor = Color.Black;
                uiReportText.SelectionIndent = 0;
                uiReportText.AppendText(string.Format("{0,-10}{1,-10}{2,-10}{3,-10}{4,-10}{5,-10}{6,-10}{7,-10}", item.SubItems[1].Text, item.SubItems[2].Text, item.SubItems[3].Text, item.SubItems[4].Text, item.SubItems[5].Text, item.SubItems[6].Text, item.SubItems[7].Text, item.SubItems[8].Text));

                uiReportText.SelectionFont = _reportFontMonoBold;
                uiReportText.SelectionColor = Color.Black;
                uiReportText.SelectionIndent = 0;
                uiReportText.AppendText(string.Format("{0,-10}{1}", item.SubItems[9].Text, Environment.NewLine));
            }

            uiReportText.AppendText(Environment.NewLine);
        }

        private InputState _state;
        private BindingList<Wheel> _wheels = new BindingList<Wheel>() { AllowEdit = true, AllowNew = true };
        private BindingList<Driver> _drivers = new BindingList<Driver>() { AllowEdit = true, AllowNew = true };
        private BindingList<SpareParts> _spareParts = new BindingList<SpareParts>() { AllowEdit = true, AllowNew = true };
        private bool _loadingInputState = true;

        private readonly Font _reportSectionFont = new Font("Microsoft Sans Serif", 12, FontStyle.Underline | FontStyle.Bold);
        private readonly Font _reportFontBold = new Font("Microsoft Sans Serif", 10, FontStyle.Bold);
        private readonly Font _reportFontMono = new Font("Consolas", 10, FontStyle.Regular);
        private readonly Font _reportFontMonoBold = new Font("Consolas", 10, FontStyle.Bold);
    }
}
