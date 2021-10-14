using System;
using System.Net.Http;
using System.Windows.Forms;

namespace ArtilleryCalculator
{
    public partial class CalculatorForm : Form
    {
        IDistanceElevationConverter Converter { get; set; } = new DistanceElevationConverter();
        ArtilleryTimingCalculator TimingCalculator { get; } = new ArtilleryTimingCalculator();

        NumpadListener NumpadListener { get; set; } = null;
        ClickListener ClickListener { get; set; } = null;

        DateTime LastNumpadInputAt { get; set; } = DateTime.MinValue;
        DateTime LastClickAt { get; set; } = DateTime.MinValue;

        public CalculatorForm(HttpClient client)
        {
            InitializeComponent();

            Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            enableNumpadCheckbox.Checked = Properties.Settings.Default.EnableNumpadListener;
            stayOnTopCheckbox.Checked = Properties.Settings.Default.StayOnTop;
            enableClickTimerCheckbox.Checked = Properties.Settings.Default.EnableClickTimer;
            enableRussiaConversion.Checked = Properties.Settings.Default.EnableRussiaConversion;

            var updateChecker = new UpdateChecker(client);
            updateChecker.InitializeUpdateChecking();
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

        private void enableClickTimerCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            UpdateClickListener();
            UpdateClickTimerControls();
        }

        private void UpdateClickListener()
        {
            var enabled = Properties.Settings.Default.EnableClickTimer = enableClickTimerCheckbox.Checked;
            if (enabled && ClickListener == null)
            {
                ClickListener = new ClickListener() { Callback = ReceiveClick };
            }
            else if (!enabled && ClickListener != null)
            {
                ClickListener.Dispose();
                ClickListener = null;
            }
        }

        private void UpdateClickTimerControls()
        {
            if (enableClickTimerCheckbox.Checked)
            {
                clickTimerUpdateTimer.Start();
                lastHitLabel.Show();
                lastHitCountdownLabel.Show();
            }
            else
            {
                clickTimerUpdateTimer.Stop();
                lastHitLabel.Hide();
                lastHitCountdownLabel.Hide();
            }
        }

        private void stayOnTopCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = Properties.Settings.Default.StayOnTop = stayOnTopCheckbox.Checked;
        }

        private void enableRussiaConversion_CheckedChanged(object sender, EventArgs e)
        {
            var russiaConversion = Properties.Settings.Default.EnableRussiaConversion = enableRussiaConversion.Checked;
            if (russiaConversion)
            {
                Converter = new RusDistanceElevationConverter();
            }
            else
            {
                Converter = new DistanceElevationConverter();
            }
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

        private void clickTimerUpdateTimer_Tick(object sender, EventArgs e)
        {
            var hitTimePrediction = TimingCalculator.PredictHitTime(LastClickAt);
            var timeUntilHit = hitTimePrediction- DateTime.Now;
            var remainingSeconds = Math.Round(timeUntilHit.TotalSeconds);

            if (remainingSeconds < 0)
            {
                lastHitCountdownLabel.Text = "";
            }
            else
            {
                lastHitCountdownLabel.Text = remainingSeconds.ToString();
            }
        }

        private void ReceiveClick()
        {
            LastClickAt = DateTime.Now;
        }
    }
}
