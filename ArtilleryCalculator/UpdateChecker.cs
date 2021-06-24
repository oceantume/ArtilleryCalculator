using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ArtilleryCalculator
{
    class UpdateChecker
    {
        ArtilleryCalculatorApiClient ApiClient { get; }

        public UpdateChecker(HttpClient httpClient)
        {
            ApiClient = new ArtilleryCalculatorApiClient(httpClient);
        }

        public void InitializeUpdateChecking()
        {
            if (!Properties.Settings.Default.HasSelectedCheckForUpdatesPreference)
            {
                PromptUserForUpdateChecks();
            }

            if (Properties.Settings.Default.ShouldCheckForUpdates)
            {
                CheckForUpdates();
            }
        }

        void PromptUserForUpdateChecks()
        {
            var caption = "Automatically check for updates";
            var text = "Do you want the application to automatically check for updates when you open it?\r\n\r\nIf you select yes, you will see a message box like this one whenever new updates of Artillery Calculator are published.";
            var result = MessageBox.Show(text, caption, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Properties.Settings.Default.ShouldCheckForUpdates = true;
                Properties.Settings.Default.HasSelectedCheckForUpdatesPreference = true;
            }
            else if (result == DialogResult.No)
            {
                Properties.Settings.Default.ShouldCheckForUpdates = false;
                Properties.Settings.Default.HasSelectedCheckForUpdatesPreference = true;
            }
        }

        void CheckForUpdates()
        {
            SynchronizationContext context = SynchronizationContext.Current;

            Task.Run(async () =>
            {
                var info = await ApiClient.GetLatestVersionInformation();
                if (info?.Outdated ?? false)
                {
                    context.Post(new SendOrPostCallback((state) =>
                    {
                        if (!IsVersionAlreadyIgnored(info.Version))
                        {
                            PromptUserToUpdate(info);
                        }
                    }), null);
                }
            });
        }

        bool IsVersionAlreadyIgnored(string version)
        {
            var lastIgnoredVersion = Properties.Settings.Default.LastIgnoredVersion;
            return !string.IsNullOrEmpty(lastIgnoredVersion) || lastIgnoredVersion == version;
        }

        void PromptUserToUpdate(ArtilleryCalculatorApiClient.LatestVersionInformation info)
        {
            var caption = info.Name;
            var text = $"A new version of Artillery Calculator is available. Do you want to open your browser to download it now?\r\n\r\nIf you select no, you won't be reminded until the next release.";
            var result = MessageBox.Show(text, caption, MessageBoxButtons.YesNo);

            if (result == DialogResult.Yes)
            {
                Process.Start(info.Url);
            }
            else if (result == DialogResult.No)
            {
                Properties.Settings.Default.LastIgnoredVersion = info.Version;
            }
        }
    }
}
