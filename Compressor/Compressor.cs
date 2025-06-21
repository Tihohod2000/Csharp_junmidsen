namespace Compressor;

public class Compressor
{
    public string Compression(string link)
    {
        char[] arrLink = link.ToCharArray();
        List<string?> shortLink = new List<string?>();
        List<LetterInfo> chars = new List<LetterInfo>();

        for (int i = 0; i < arrLink.Length;)
        {
            int countIdenticalChar = 0; //количесвто адинаковых подряд букв
            char current = arrLink[i];

            for (int j = i; j < arrLink.Length; j++)
            {
                if (arrLink[i] == arrLink[j])
                {
                    countIdenticalChar++;
                    //если элемент последний
                    if (j == arrLink.Length - 1)
                    {
                        i = j + 1;
                        break;
                    }
                }
                else
                {
                    i = j;
                    break;
                }
            }

            chars.Add(new LetterInfo
            {
                Letter = current,
                CountIdenticalChar = countIdenticalChar
            });
        }


        foreach (var current in chars)
        {
            if (current.CountIdenticalChar > 1)
            {
                shortLink.Add(Convert.ToString(current.Letter) + Convert.ToString(current.CountIdenticalChar));
            }
            else
            {
                shortLink.Add(Convert.ToString(current.Letter));
            }
        }


        return string.Join("", shortLink);
    }

    public string Decompression(string? link)
    {
        char[] arrLink = link.ToCharArray();
        List<char> fullLink = new List<char>();

        for (int i = 0; i < arrLink.Length;)
        {
            char currentLetter = arrLink[i];
            //если текущий последний в массиве
            if (i == arrLink.Length - 1)
            {
                fullLink.Add(currentLetter);
                break;
            }

            //если следующий элемент буква, а не цифра
            if (!char.IsDigit(arrLink[i + 1]))
            {
                fullLink.Add(currentLetter);
                i++;
            }
            else
            {
                int currentLetterIteration = (int)char.GetNumericValue(arrLink[i + 1]);
                for (int j = 0; j < currentLetterIteration; j++)
                {
                    fullLink.Add(currentLetter);
                }

                i += 2;
            }
        }

        return string.Join("", fullLink);
    }
}