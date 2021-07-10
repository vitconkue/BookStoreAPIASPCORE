

namespace BookStore.Helper
{
    class HelperFunctions
    {
        public static string RemovedUTFAndToLower(string input)
        {

            int checkContainsInReplaceString(char input_char)
            {
                string toReplace = "ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýđð";
                for (int i = 0; i < toReplace.Length; ++i)
                {
                    if (toReplace[i] == input_char)
                    {
                        return i;
                    }
                }
                return -1;
            }
            string result = input;

            string toMatch = "aadeoouaaaaaaaaaaaaaaaeeeeeeeeeeiiiiiooooooooooooooouuuuuuuuuuyyyyydd";

            for (int i = 0; i < result.Length; ++i)
            {
                int checkValue = checkContainsInReplaceString(result[i]);
                if (checkValue != -1)
                {
                    result = result.Replace(result[i], toMatch[checkValue]);
                }
            }

            return result.ToLower();
        }

        public static int rateSearchResult(string searchText, string matchedText)
        {
            // longer match => higher rate
            int result = searchText.Length * 10;
            string removedUTFSearchText = HelperFunctions.RemovedUTFAndToLower(searchText);
            string removedUTFMatchedText = HelperFunctions.RemovedUTFAndToLower(matchedText);




            int indexOfMatch = matchedText.IndexOf(searchText);
            if (indexOfMatch <= 0)
            {
                indexOfMatch = removedUTFMatchedText
                .IndexOf(removedUTFMatchedText);
            }
    
            // match at the beginning => higher rate
            result -= indexOfMatch * 2;

            // match with better Vietnamse symbol => higher rate

            for (int i = 0; i < searchText.Length; ++i)
            {
                if (indexOfMatch >= 0 && searchText[i] == matchedText[indexOfMatch + i])
                {
                    result+= 5;
                }
            }
            return result;
        }
        public static bool isNumericString(string input_string)
        {
            bool result = true;
            for (int i = 0; i < input_string.Length; i++)
            {
                if (input_string[i] < '0' || input_string[i] > '9')
                {
                    result = false;
                }
            }
            return result;
        }

    }
}