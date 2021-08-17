

namespace BookStore.Helper
{
    class HelperFunctions
    {
        public static string RemovedUtfAndToLower(string input)
        {

            int CheckContainsInReplaceString(char inputChar)
            {
                string toReplace = "ăâđêôơưàảãạáằẳẵặắầẩẫậấèẻẽẹéềểễệếìỉĩịíòỏõọóồổỗộốờởỡợớùủũụúừửữựứỳỷỹỵýđð";
                for (int i = 0; i < toReplace.Length; ++i)
                {
                    if (toReplace[i] == inputChar)
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
                int checkValue = CheckContainsInReplaceString(result[i]);
                if (checkValue != -1)
                {
                    result = result.Replace(result[i], toMatch[checkValue]);
                }
            }

            return result.ToLower();
        }

        public static int RateSearchResult(string searchText, string matchedText)
        {
            // longer match => higher rate
            int result = searchText.Length * 10;
            string removedUtfSearchText = HelperFunctions.RemovedUtfAndToLower(searchText);
            string removedUtfMatchedText = HelperFunctions.RemovedUtfAndToLower(matchedText);




            int indexOfMatch = matchedText.IndexOf(searchText);
            if (indexOfMatch <= 0)
            {
                indexOfMatch = removedUtfMatchedText
                .IndexOf(removedUtfMatchedText);
            }
    
            // match at the beginning => higher rate
            result -= indexOfMatch * 2;

            // match with better Vietnamese symbol => higher rate

            for (int i = 0; i < searchText.Length; ++i)
            {
                if (indexOfMatch >= 0 && searchText[i] == matchedText[indexOfMatch + i])
                {
                    result+= 5;
                }
            }
            return result;
        }
        public static bool IsNumericString(string inputString)
        {
            bool result = true;
            foreach (var character in inputString)
            {
                if (character < '0' || character > '9')
                {
                    result = false;
                }
            }
            return result;
        }

    }
}