using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace LTTimer.Model
{
    public static class SoundEffectSettingService
    {
        private static ISettings AppSettings => CrossSettings.Current;

        #region Setting Constants

        private const string SettingsKey = "SoundEffectSetting";
        private static readonly bool SettingsDefault = true;

        #endregion


        public static bool GeneralSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault<bool>(SettingsKey, SettingsDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue<bool>(SettingsKey, value);
            }
        }

    }
}