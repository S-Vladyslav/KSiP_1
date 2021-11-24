using System;
using System.Collections.Generic;
using System.Linq;

namespace KSiP
{
    class Program
    {
        static string Alphabet = "АБВГҐДЕЄЖЗИІЇЙКЛМНОПРСТУФХЦЧШЩЬЮЯабвгґдеєжзиіїйклмнопрстуфхцчшщьюя"; //your alphabet

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;

            var rawText = Text.RawText;

            var encryptedText = Encrypt(rawText, 100);

            var decryptedText = Decrypt(encryptedText, CryptoAnalysis(encryptedText));
           
            Console.WriteLine(decryptedText);
        }

        /// <summary>
        /// Method will encrypt text which you put.
        /// </summary>
        /// <param name="rawText">Text which will be encrypted.</param>
        /// <param name="rot">Rotor number.</param>
        /// <returns>Encrypted text.</returns>
        static string Encrypt(string rawText, int rot)
        {
            var encryptedText = "";

            foreach (var symbol in rawText)
            {
                if (Alphabet.IndexOf(symbol) != -1)
                {
                    encryptedText += Alphabet[(Alphabet.IndexOf(symbol) + rot) % Alphabet.Length];
                }
                else
                {
                    encryptedText += symbol;
                }
            }
            return encryptedText;
        }

        /// <summary>
        /// Method will decrypt text which you put.
        /// </summary>
        /// <param name="encryptedText">Text which will be decrypted.</param>
        /// <param name="rot">Rotor number.</param>
        /// <returns>Decrypted text.</returns>
        static string Decrypt(string encryptedText, int rot)
        {
            var decryptedText = "";

            foreach (var symbol in encryptedText)
            {
                if (Alphabet.IndexOf(symbol) != -1)
                {
                    if ((Alphabet.IndexOf(symbol) - rot) >= 0)
                    {
                        decryptedText += Alphabet[Alphabet.IndexOf(symbol) - rot];
                    }
                    else
                    {
                        decryptedText += Alphabet[(Alphabet.IndexOf(symbol) - rot) + Alphabet.Length];
                    }
                }
                else
                {
                    decryptedText += symbol;
                }
            }

            return decryptedText;
        }

        /// <summary>
        /// Method will find rotor for text which you put.
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns>Rotor number.</returns>
        static int CryptoAnalysis(string encryptedText)
        {
            var dictionary = new Dictionary<char, int>();

            foreach (char symb in encryptedText)
            {
                if (Alphabet.IndexOf(symb) != -1)
                {
                    if (dictionary.TryGetValue(symb, out int i))
                    {
                        dictionary[symb] += 1;
                    }
                    else
                    {
                        dictionary.Add(symb, 1);
                    }
                }
            }

            var mostCommon = dictionary.OrderByDescending(el => el.Value)
                                       .Select(el => el.Key)
                                       .ToList()[0];

            var rot = Alphabet.IndexOf(mostCommon) - Alphabet.IndexOf('о');

            return (rot < 0) ? rot + Alphabet.Length : rot;
        }
    }
}
