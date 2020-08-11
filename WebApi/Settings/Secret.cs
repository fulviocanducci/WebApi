using System.Text;

namespace WebApi.Settings
{
    public static class Secret
    {
        public const string Value = "fedaf7d8863b48e197b9287d492b708e";
        public static byte[] ValueToByte => Encoding.ASCII.GetBytes(Value);
    }
}
