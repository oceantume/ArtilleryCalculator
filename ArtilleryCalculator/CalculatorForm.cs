using System;
using System.Windows.Forms;

namespace ArtilleryCalculator
{
    public partial class CalculatorForm : Form
    {
        DistanceElevationConverter Converter { get; } = new DistanceElevationConverter();
        NumpadListener NumpadListener { get; set; } = null;

        DateTime LastNumpadInputAt { get; set; } = DateTime.MinValue;

        public CalculatorForm()
        {
            InitializeComponent();

            Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            enableNumpadCheckbox.Checked = Properties.Settings.Default.EnableNumpadListener;
            stayOnTopCheckbox.Checked = Properties.Settings.Default.StayOnTop;
        }

        private void CalculatorForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Properties.Settings.Default.Save();
        }

        private void enableNumpadCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateNumpadListener();
        }

        private void UpdateNumpadListener()
        {
            var enabled = Properties.Settings.Default.EnableNumpadListener = enableNumpadCheckbox.Checked;
            if (enabled && NumpadListener == null)
            {
                NumpadListener = new NumpadListener() { Callback = ReceiveNumpadDigit };
            }
            else if (!enabled && NumpadListener != null)
            {
                NumpadListener.Dispose();
                NumpadListener = null;
            }
        }

        private void stayOnTopCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = Properties.Settings.Default.StayOnTop = stayOnTopCheckbox.Checked;
        }

        private void distanceInput_ValueChanged(object sender, EventArgs e)
        {
            elevationInput.Value = Converter.ConvertDistanceToElevation(distanceInput.Value);
        }

        private void ReceiveNumpadDigit(int digit)
        {
            if (ActiveForm == this)
            {
                return;
            }

            if (DateTime.Now - LastNumpadInputAt > TimeSpan.FromSeconds(1))
            {
                distanceInput.Value = digit;
            }
            else
            {
                var newStringValue = distanceInput.Value.ToString() + digit;
                if (decimal.TryParse(newStringValue, out var newValue))
                {
                    distanceInput.Value = newValue;
                }
            }

            LastNumpadInputAt = DateTime.Now;
        }
    }
}
