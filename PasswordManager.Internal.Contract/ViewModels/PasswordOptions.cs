namespace PasswordManager.Internal.Contract.ViewModels
{
    public class PasswordOptions
    {
        public int Length { get; set; }
        public bool IncludeDigit { get; set; }
        public bool IncludeSpecSymbols { get; set; }
        public bool IncludeBigLetters { get; set; }
    }
}
