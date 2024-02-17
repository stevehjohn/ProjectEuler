using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0059 : Puzzle
{
    private char[] _cipherText;
    
    public override string GetAnswer()
    {
        LoadInput();
        
        ParseInput();

        var a = 'a';

        var b = 'a';

        var c = 'a';

        while (true)
        {
            var plainText = new string(Decrypt($"{a}{b}{c}"));

            if (plainText.Contains("Euler"))
            {
                return plainText.ToCharArray().Select(l => (int) l).Sum().ToString("N0");
            }

            c++;

            if (c > 'z')
            {
                c = 'a';

                b++;

                if (b > 'z')
                {
                    b = 'a';

                    a++;
                }
            }
        }

        throw new NotImplementedException();
    }

    private char[] Decrypt(string key)
    {
        var decrypted = new char[_cipherText.Length];

        var keyIndex = 0;
        
        for (var i = 0; i < _cipherText.Length; i++)
        {
            decrypted[i] = (char) (_cipherText[i] ^ key[keyIndex]);

            keyIndex++;

            if (keyIndex == key.Length)
            {
                keyIndex = 0;
            }
        }

        return decrypted;
    }

    private void ParseInput()
    {
        _cipherText = Input[0].Split(',').Select(c => (char) byte.Parse(c)).ToArray();
    }
}