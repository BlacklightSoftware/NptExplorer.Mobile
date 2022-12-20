using NptExplorer.Mobile.Helpers;

namespace NptExplorer.Mobile.Constants
{
	public class SystemConstants
	{
		public static readonly string AppCenterIosKey = AppSettingsManager.Settings["AppCenterIosKey"];
		public static readonly string AppCenterAndroidKey = AppSettingsManager.Settings["AppCenterAndroidKey"];

		public static readonly string TwitterApiBearerToken = AppSettingsManager.Settings["TwitterApiBearerToken"];

		public static readonly string BaseUrl = AppSettingsManager.Settings["BaseUrl"];
		public static readonly string BlobUri = AppSettingsManager.Settings["BlobUri"];
		public static readonly string TermsAndroid = AppSettingsManager.Settings["TermsAndroid"];
		public static readonly string TermsIos = AppSettingsManager.Settings["TermsIos"];
		public static readonly string PolicyAndroid = AppSettingsManager.Settings["PolicyAndroid"];
		public static readonly string PolicyIos = AppSettingsManager.Settings["PolicyIos"];

		public static readonly string ClientId = AppSettingsManager.Settings["ClientId"];
		public static readonly string TenantName = AppSettingsManager.Settings["TenantName"];
		public static readonly string TenantId = AppSettingsManager.Settings["TenantId"];
		public static readonly string SignInPolicy = AppSettingsManager.Settings["SignInPolicy"];
		public static readonly string IosKeychainSecurityGroups = AppSettingsManager.Settings["AppId"];
		public static readonly string[] Scopes = { "openid", "offline_access" };
		public static readonly string AuthorityBase = $"https://{TenantName}.b2clogin.com/tfp/{TenantId}/";
		public static readonly string AuthoritySignIn = $"{AuthorityBase}{SignInPolicy}";
	}
}