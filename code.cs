 public class Solution {
    public IList<string> FindAllConcatenatedWordsInADict(string[] words)
    {
        

        int len = words.Length;

        IList<string> result = new List<string>();

        WordTreeNode head = new WordTreeNode();  //Create the Tree node using the default constructor.

        for (int i = 0; i < len; i++)
        {
            // Add to word tree
            Add2WordTree(head, words[i]);
        }



        for (int i = 0; i < len; i++)
        {
            string cur = words[i];
            // find if comprized of more than two words
            if (CheckWordTree(head, cur))
            {
                result.Add(cur);
            }

        }

        return result;

    }

    private void Add2WordTree(WordTreeNode head, string word, int index = 0)
    {

        if (index == word.Length)
        {
            head.WordEnd = true;
            return;
        }

        char curChar = word[index];

        Dictionary<char, WordTreeNode> curChildChar = head.ChildChar;

        if (curChildChar.ContainsKey(curChar))
        {
            WordTreeNode child = curChildChar[curChar];
            Add2WordTree(child, word, index + 1);
        }
        else
        {
            WordTreeNode newNode = new WordTreeNode();
            curChildChar.Add(curChar, newNode);
            Add2WordTree(newNode, word, index + 1);
        }


    }


    private bool CheckWordTree(WordTreeNode head, string word, int index = 0, bool moreThanOneWord = false)
    {
        int len = word.Length;

        WordTreeNode Pointer = head;

        //bool moreThanOne = false;

        for (int i = index; i < len; i++)
        {
            // Does child pointer match current char

            char curChar = word[i];
            Dictionary<char, WordTreeNode> curChildChar = Pointer.ChildChar;
            WordTreeNode child = null;
            if (curChildChar.ContainsKey(curChar))
            {
                child = curChildChar[curChar];

            }
            else
            {
                return false;
            }

            // Is the child word end
            if (child.WordEnd)
            {
                if (i == len - 1)
                {
                    if (moreThanOneWord)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    if (CheckWordTree(head, word, i + 1, true))
                    {
                        return true;
                    }
                    else
                    {
                        // current wordend won't work, find next wordend
                        Pointer = child;
                    }
                }

            }
            else
            {
                Pointer = child;
            }
        }


        return false;
    }


}

class WordTreeNode
{
    public WordTreeNode()
    {
        ChildChar = new Dictionary<char, WordTreeNode>();
        WordEnd = false;
    }

    public bool WordEnd
    {
        get; set;
    }

    public Dictionary<char, WordTreeNode> ChildChar
    {
        get; set;
    }

}

