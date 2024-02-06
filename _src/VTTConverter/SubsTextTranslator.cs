using Google.Cloud.Translation.V2;

namespace Phrazer
{
    class SubsTextTranslator
    {
        
        
        static string RequestTranslationByGoogle (string text) {
            var client = TranslationClient.Create();
            var response = client.TranslateText(text, LanguageCodes.German, LanguageCodes.English);
            return response.TranslatedText;
        }

        public static string[] TranslateText (string[] textBufferList) {
            string text = "";
            foreach (string row in textBufferList)
            {
                text = text + row + "\n";
            }
            string translation = RequestTranslationByGoogle(text);
            return translation.Split('\n');
        }
        
    }
}
