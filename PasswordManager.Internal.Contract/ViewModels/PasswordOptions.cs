namespace PasswordManager.Internal.Contract.ViewModels
{
    public class PasswordOptions
    {
        public int Length { get; set; } = 6;
        public bool IncludeDigit { get; set; } = true;
        public bool IncludeSpecSymbols { get; set; } = false;
        public bool IncludeBigLetters { get; set; } = false;
    }
}
