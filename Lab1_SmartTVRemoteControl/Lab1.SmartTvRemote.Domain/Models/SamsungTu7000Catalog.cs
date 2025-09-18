// Domain/Models/SamsungTu7000Catalog.cs
namespace Lab1.SmartTvRemote.Domain.Models
{
    internal static class SamsungTu7000Catalog
    {
        private static readonly IReadOnlyDictionary<SamsungTu7000Size, string> _modelCodes =
            new Dictionary<SamsungTu7000Size, string>
            {
                [SamsungTu7000Size.In43] = "UN43TU7000",
                [SamsungTu7000Size.In50] = "UN50TU7000",
                [SamsungTu7000Size.In55] = "UN55TU7000",
                [SamsungTu7000Size.In58] = "UN58TU7000",
                [SamsungTu7000Size.In65] = "UN65TU7000",
                [SamsungTu7000Size.In70] = "UN70TU7000",
                [SamsungTu7000Size.In75] = "UN75TU7000",
            };

        public static bool TryGetCode(SamsungTu7000Size size, out string code) =>
            _modelCodes.TryGetValue(size, out code!);

        public static string GetCodeOrThrow(SamsungTu7000Size size) =>
            _modelCodes.TryGetValue(size, out var code)
                ? code
                : throw new ArgumentOutOfRangeException(nameof(size), "Unsupported TU7000 size.");

        public static IReadOnlyCollection<SamsungTu7000Size> SupportedSizes =>
            _modelCodes.Keys.ToList().AsReadOnly();
    }
}
