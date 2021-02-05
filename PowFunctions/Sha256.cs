namespace bc.PowFunctions
{
    public class Sha256
    {
        private readonly string _answer;
        public Sha256(string answer)
        {
            _answer = answer;
        }
        public bool VerifyPow(string guess)
        { 
            return _answer == guess;
        }
    }
}