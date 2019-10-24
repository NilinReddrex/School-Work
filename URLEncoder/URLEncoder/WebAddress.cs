using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace URLEncoder
{
    class WebAddress
    {
        private readonly string urlFormat;
        private readonly string[] dynamicParameters;

        /// <summary>
        /// A dictionary of characters that need to be converted to be used properly in the URL.
        /// </summary>
        private readonly Dictionary<string, string> encodingCharacters = new Dictionary<string, string>()
        {
            { " ", "%20" },
            { "<", "%3C" },
            { ">", "%3E" },
            { "#", "%23" },
            { "%", "%25" },
            { "\"", "%22"},
            { ";", "%3B" },
            { "/", "%2F" },
            { "?", "%3F" },
            { ":", "%3A" },
            { "@", "%40" },
            { "&", "%26" },
            { "=", "%3D" },
            { "+", "%2B" },
            { "$", "%24" },
            { ",", "%2C" },
            { "{", "%7B" },
            { "}", "%7D" },
            { "|", "%7C" },
            { "\\","%5C" },
            { "^", "%5E" },
            { "[", "%5B" },
            { "]", "%5D" },
            { "`", "%60" },
        };

        /// <summary>
        /// List of invalid characters.
        /// </summary>
        private readonly List<char> invalidCharacters = new List<char>()
        {
            (char)0x00,
            (char)0x01,
            (char)0x02,
            (char)0x03,
            (char)0x04,
            (char)0x05,
            (char)0x06,
            (char)0x07,
            (char)0x08,
            (char)0x09,
            (char)0x0A,
            (char)0x0B,
            (char)0x0C,
            (char)0x0D,
            (char)0x0E,
            (char)0x0F,
            (char)0x10,
            (char)0x11,
            (char)0x12,
            (char)0x13,
            (char)0x14,
            (char)0x15,
            (char)0x16,
            (char)0x17,
            (char)0x18,
            (char)0x19,
            (char)0x1A,
            (char)0x1B,
            (char)0x1C,
            (char)0x1D,
            (char)0x1F,
            (char)0x7F
        };

        /// <summary>
        /// Gets the web address with the parameters encoded and inserted. 
        /// </summary>
        /// <returns>a fully encoded web address</returns>
        public string GetEncodedURL()
        {
            return string.Format(this.urlFormat, this.dynamicParameters);
        }
        
        /// <summary>
        /// The constructor of the web address object.
        /// </summary>
        /// <param name="format">The URL format.</param>
        /// <param name="parameters">Input from the user.</param>
        public WebAddress(string format, string[] parameters)
        {
            this.urlFormat = format;
            this.dynamicParameters = parameters;

            for (int i = 0; i < parameters.Length; i++)
            {
                if (!this.IsUrlValid(parameters[i]))
                {
                    throw new Exception("You entered an invalid input.");
                }

                this.dynamicParameters[i] = this.UrlEncode(parameters[i]);
            }

       
        }

        /// <summary>
        /// Determines if input is valid.
        /// </summary>
        /// <param name="input"> a value to be determined</param>
        /// <returns> true if valid else false</returns>
        private bool IsUrlValid(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (this.invalidCharacters.Contains(input[i]))
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Encodeds the URL.
        /// </summary>
        /// <param name="parameter"> Value to be encoded.</param>
        /// <returns> Encoded value</returns>
        private string UrlEncode(string parameter)
        {         
            string encodedValue = string.Empty;
            foreach (char character in parameter)
            {
                if (this.encodingCharacters.TryGetValue(character.ToString(), out string encodedCharacter))
                {
                    encodedValue += encodedCharacter;
                }
                else
                {
                    encodedValue += character;
                }
            }

            return encodedValue; 
        }
    }
}
