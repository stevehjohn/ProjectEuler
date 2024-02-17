using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0059 : Puzzle
{
    private byte[] _cipherText;
    
    public override string GetAnswer()
    {
        LoadInput();
        
        ParseInput();
        
        throw new NotImplementedException();
    }

    private byte[] Decrypt(string key)
    {
        var decrypted = new byte[_cipherText.Length];

        var keyIndex = 0;
        
        for (var i = 0; i < _cipherText.Length; i++)
        {
            decrypted[i] = (byte) (_cipherText[i] ^ key[keyIndex]);

            keyIndex++;

            if (keyIndex > key.Length)
            {
                keyIndex = 0;
            }
        }

        return decrypted;
    }

    private void ParseInput()
    {
        _cipherText = Input[0].Split(',').Select(byte.Parse).ToArray();
    }
}