using CodeChallenge.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Repository
{
    public static class StaticInitalDatabaseData
    {
        public static List<Challenge> StaticChallanges = new List<Challenge>
        {

            new Challenge
    {
        ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
        Name = "Palindrome Number",
        Difficulty = "Easy",
        Type = "Algorithms",
        MainMethodName = "IsPalindrome",
        Descriptions = new List<ChallengeDescription>
        {
            new ChallengeDescription
            {
                DescriptionId = Guid.NewGuid(),
                ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
                Language = "kz",
                HtmlContent = "<p>Сан палиндром болып табылатын болса, сондай-ақ сан олардың керісіне өзгермейді. Бір жолмен бұл сандарды кері айналдырудан кейін өзгермейтін сандарды анықтау мәселесі. Мысалы, 121-дің керісі оның өзі, яғни 121.</p>"
            },
            new ChallengeDescription
            {
                DescriptionId = Guid.NewGuid(),
                ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
                Language = "ru",
                HtmlContent = "<p>Число считается палиндромом, если оно остается неизменным при перевертышах. В некотором роде это вопрос о идентификации чисел, которые не меняются после обращения. Например, обратное число 121 - это его же, то есть 121.</p>"
            },
            new ChallengeDescription
            {
                DescriptionId = Guid.NewGuid(),
                ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
                Language = "cn",
                HtmlContent = "<p>一个数字如果在颠倒后仍然不变，那么它就被视为是回文。 这在某种程度上是确定在反转后仍不变的数字的问题。 比如 121 的反转就是它自己，也就是 121.</p>"
            },
            new ChallengeDescription
            {
                DescriptionId = Guid.NewGuid(),
                ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
                Language = "us",
                HtmlContent = "<p>A number is regarded as palindrome if it remains the same when they are reversed. In a way it's a question of figuring out numbers that remain the same after being reversed. For example, the reverse of 121 is itself, i.e., 121.</p>"
            }
        },
        Solutions = new List<ChallengeSolution>
        {
            new ChallengeSolution
            {
                SolutionId = Guid.NewGuid(),
                ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
                Language = "kz",
                HtmlContent = "<p>Бұл тапсырманы шешу үшін, біз санды айналдырамыз және ол палиндром болып табылатын ретте сол санды тексереміз. Әрекеттер: 1) Санды кері айналдырыңыз. 2) Кері болған санды бастапқы санмен салыстырыңыз. Егер олар бірдей болса, онда сан палиндром.</p>"
            },
            new ChallengeSolution
            {
                SolutionId = Guid.NewGuid(),
                ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
                Language = "ru",
                HtmlContent = "<p>Для решения этой задачи, мы перевернем число и проверим, является ли оно палиндромом. Шаги: 1) перевернуть число. 2) сравните обратное число с исходным. Если они одинаковы, то число является палиндромом.</p>"
            },
            new ChallengeSolution
            {
                SolutionId = Guid.NewGuid(),
                ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
                Language = "cn",
                HtmlContent = "<p>要解决这个问题，我们将翻转数字然后检查它是否为回文。 步骤如下：1）反转数字。 2）比较反过来的数字和原数。 如果它们相同，则数字是回文。</p>"
            },
            new ChallengeSolution
            {
                SolutionId = Guid.NewGuid(),
                ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
                Language = "us",
                HtmlContent = "<p>To solve the problem, we reverse the number and check whether it's palindrome. Steps involved are: 1) reverse the number. 2) compare the reversed number with the original. If they are same then the number is palindrome.</p>"
            }
        },
        CodeTemplates = new List<ChallengeCodeTemplate>
        {
            new ChallengeCodeTemplate
            {
                TemplateId = Guid.NewGuid(),
                ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
                ProgrammingLanguage = "csharp",
                CodeTemplate = @"
public class Solution 
{
    public bool IsPalindrome(int x) 
    {
        
    }
}"
            }
        },
        TestCases = new List<TestCase>
        {
            new TestCase
            {
                TestCaseId = Guid.NewGuid(),
                ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
                InputParameters = new List<InputParameter>
                {
                    new InputParameter { Type = "int", Value = "121" }
                },
                ExpectedResult = new InputParameter { Type = "bool", Value = "True" }
            },
            new TestCase
            {
                TestCaseId = Guid.NewGuid(),
                ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
                InputParameters = new List<InputParameter>
                {
                    new InputParameter { Type = "int", Value = "-121" }
                },
                ExpectedResult = new InputParameter { Type = "bool", Value = "False" }
            },
            new TestCase
            {
                TestCaseId = Guid.NewGuid(),
                ChallengeId = Guid.Parse("83df0290-84d4-46ab-833e-6c37d3682f52"),
                InputParameters = new List<InputParameter>
                {
                    new InputParameter { Type = "int", Value = "10" }
                },
                ExpectedResult = new InputParameter { Type = "bool", Value = "False" }
            }
        }
    },

            new Challenge
{
    ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
    Name = "String Reversal",
    Difficulty = "easy",
    Type = "algorithms",
    MainMethodName = "ReverseString",
    Descriptions = new List<ChallengeDescription>()
    {
        new ChallengeDescription
        {
            DescriptionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
            Language = "us",
            HtmlContent = "<p>You are given a string, your task is to reverse this string and return the reversed string.</p>"
        },
        new ChallengeDescription
        {
            DescriptionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
            Language = "ru",
            HtmlContent = "<p>Вам дана строка, ваша задача - перевернуть эту строку и вернуть перевернутую строку.</p>"
        },
        new ChallengeDescription
        {
            DescriptionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
            Language = "cn",
            HtmlContent = "<p>给定一个字符串，您的任务是反转该字符串并返回反转后的字符串。</p>"
        },
        new ChallengeDescription
        {
            DescriptionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
            Language = "kz",
            HtmlContent = "<p>Сізге жол беріледі, сіздің тапсырмасыз бұл жолды кері қайтару және болған жолды қайтару.</p>"
        }
    },
    Solutions = new List<ChallengeSolution>()
    {
        new ChallengeSolution
        {
            SolutionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
            Language = "us",
            HtmlContent = "<h2>Solution</h2><p>One way to solve this problem is by using two pointers. One pointing to the beginning of the string, and another one to the end of the string. Swap these characters and move the pointers towards the center. Do this until the pointers overlap.</p>"
        },
        new ChallengeSolution
        {
            SolutionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
            Language = "ru",
            HtmlContent = "<h2>Решение</h2><p>Один из способов решения этой проблемы - использование двух указателей. Один указывает на начало строки, а другой - на конец строки. Обменяйте эти символы и переместите указатели к центру. Сделайте это до тех пор, пока указатели не пересекутся.</p>"
        },
        new ChallengeSolution
        {
            SolutionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
            Language = "cn",
            HtmlContent = "<h2>解决办法</h2><p>解决此问题的一种方法是使用两个指针。一个指向字符串的开头，另一个指向字符串的结尾。交换这些字符并将指针向中心移动。直到指针重叠为止。</p>"
        },
        new ChallengeSolution
        {
            SolutionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
            Language = "kz",
            HtmlContent = "<h2>Шешім</h2><p> Бұл проблеманы шешу үшін бір тәсіл - екі көрсеткіч арқылы. Бірі жолдың басын көрсетеді, екіншісі жолдың соңын. Осы символдарды ауыстырып, көрсеткіштерді ортасына жылжытыңыз. Бұлды көрсеткіштер қиысып шыққанға дейін жасаңыз.</p>"
        },
    },
    CodeTemplates = new List<ChallengeCodeTemplate>()
    {
        new ChallengeCodeTemplate
        {
            TemplateId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
            ProgrammingLanguage = "csharp",
            CodeTemplate =
@"public class Solution {
    public string ReverseString(string s) {
        // TODO: Implement this function
        throw new NotImplementedException();
    }
}"
        }
    },
    TestCases = new List<TestCase>()
    {
        new TestCase
        {
            TestCaseId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
            InputParameters = new List<InputParameter>
            {
                new InputParameter { Type = "string", Value = "\"hello\"" },
            },
            ExpectedResult = new InputParameter { Type = "string", Value = "\"olleh\"" }
        },
        new TestCase
        {
            TestCaseId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
            InputParameters = new List<InputParameter>
            {
                new InputParameter { Type = "string", Value = "\"world\"" },
            },
            ExpectedResult = new InputParameter { Type = "string", Value = "\"dlrow\"" }
        },
        new TestCase
        {
            TestCaseId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("16fb3d30-4648-4331-acc4-bea06e15bba9"),
            InputParameters = new List<InputParameter>
            {
                new InputParameter { Type = "string", Value = "\"AI\"" },
            },
            ExpectedResult = new InputParameter { Type = "string", Value = "\"IA\"" }
        }
    }
},
            new Challenge
{
    ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
    Name = "Two Sum",
    Difficulty = "easy",
    Type = "algorithm",
    MainMethodName = "TwoSum",
    Descriptions = new List<ChallengeDescription>()
    {
        new ChallengeDescription
        {
            DescriptionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
            Language = "kz",
            HtmlContent = "<p>Бізге берілген массив әр элементі болмай, екі элементінің топ сомасы target арқылы берілген данаға тең болуы талап етілген. Бұл екі элементін стеutedStringу.</p>"
        },
        new ChallengeDescription
        {
            DescriptionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
            Language = "ru",
            HtmlContent = "<p>Дан массив, в котором нам нужно найти два таких элемеента, сумма которых равна заданному нами числу. Поиск этих двух номеров и есть ваша задача.</p>"
        },
        new ChallengeDescription
        {
            DescriptionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
            Language = "cn",
            HtmlContent = "<p>给我们一个数组，我们需要找到两个元素，它们的总和等于我们给定的数。 查找这两个数字就是你的任务。</p>"
        },
        new ChallengeDescription
        {
            DescriptionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
            Language = "us",
            HtmlContent = "<p>You are given an array, you need to find two elements in it such that their sum equals the number we've given. Your task is to find those two numbers.</p>"
        }
    },
    Solutions = new List<ChallengeSolution>()
    {
        new ChallengeSolution
        {
            SolutionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
            Language = "kz",
            HtmlContent = "<p>Бұл мәселені шешу үшін біз hashMap дәстүрлі қолданады. Массивтегі барлық элементте қатысу пайда болады. hashMap деген сөз ойында ептеген екі элементінің қосылуын орындай алады.</p>"
        },
        new ChallengeSolution
        {
            SolutionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
            Language = "ru",
            HtmlContent = "<p>Чтобы решить эту задачу, нам понадобится воспользоваться структурой данных hashMap. Обойдёте все элементы массива, используйте hashMap для хранения обрабатываемых элементов и их индексов, и проверьте есть ли наше разность в hashMap или нет.</p>"
        },
        new ChallengeSolution
        {
            SolutionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
            Language = "cn",
            HtmlContent = "<p>要解决这个问题，我们需要使用数据结构hashMap。 处理数组中的所有元素，使用hashMap存储正在处理的元素和其索引，如果我们的差异存在于hashMap中，则返回这二者的索引。</p>"
        },
        new ChallengeSolution
        {
            SolutionId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
            Language = "us",
            HtmlContent = "<p>To solve this problem, we'll need to use a data structure known as a hashMap. Process all elements in the array, use hashMap to store the elements being processed and their indexes, if our difference exist in the hashMap then return those two indexes.</p>"
        }
    },
    CodeTemplates = new List<ChallengeCodeTemplate>()
    {
        new ChallengeCodeTemplate
        {
            TemplateId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
            ProgrammingLanguage = "csharp",
            CodeTemplate =
@"public class Solution {
    public int[] TwoSum(int[] nums, int target) {
        var map = new Dictionary<int, int>();
        for (int i = 0; i < nums.Length; i++) {
            int complement = target - nums[i];
            if (map.ContainsKey(complement)) {
                return new int[] { map[complement], i };
            }
            map[nums[i]] = i;
        }
        throw new Exception('No two sum solution');
    }
}"
        }
    },
    TestCases = new List<TestCase>()
    {
        new TestCase
        {
            TestCaseId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
            InputParameters = new List<InputParameter>
            {
                new InputParameter { Type = "int[]", Value = "[2, 7, 11, 15]" },
                new InputParameter { Type = "int", Value = "9" }
            },
            ExpectedResult = new InputParameter { Type = "int[]", Value = "[0, 1]" }
        },
        new TestCase
        {
            TestCaseId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
            InputParameters = new List<InputParameter>
            {
                new InputParameter { Type = "int[]", Value = "[3, 2, 4]" },
                new InputParameter { Type = "int", Value = "6" }
            },
            ExpectedResult = new InputParameter { Type = "int[]", Value = "[1, 2]" }
        },
        new TestCase
        {
            TestCaseId = Guid.NewGuid(),
            ChallengeId = Guid.Parse("e7e1530a-05a8-4fbb-8491-070f1a739501"),
            InputParameters = new List<InputParameter>
            {
                new InputParameter { Type = "int[]", Value = "[3, 3]" },
                new InputParameter { Type = "int", Value = "6" }
            },
            ExpectedResult = new InputParameter { Type = "int[]", Value = "[0, 1]" }
        }
    }
},
            new Challenge
            {
                ChallengeId = Guid.Parse("ae559f53-e4f4-464d-84c7-f666b61ce22d"),
                Name = "Binary Search",
                Difficulty = "easy",
                Type = "Algorithms",
                MainMethodName = "Search",
                Descriptions = new List<ChallengeDescription>
                {
                    new ChallengeDescription
                    {
                        DescriptionId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("ae559f53-e4f4-464d-84c7-f666b61ce22d"),
                        Language = "us",
                        HtmlContent = "<p>Implement a function that performs binary search on a sorted array of integers to find the index of a given target integer. If the target is not found, return -1.</p>"
                          + "<p><strong>Example:</strong> Given nums = [1, 2, 3, 4, 5], target = 3, the function should return 2.</p>"
                          + "<p><strong>Note:</strong> Assume that all elements in the array are unique and the array is sorted in ascending order.</p>"
                    },
                    // The following descriptions are the placeholder translations for other languages.
                    new ChallengeDescription
                    {
                        DescriptionId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("ae559f53-e4f4-464d-84c7-f666b61ce22d"),
                        Language = "ru",
                        HtmlContent = "<p>Реализуйте функцию, которая выполняет двоичный поиск в отсортированном массиве целых чисел для нахождения индекса заданного целого числа. Если цель не найдена, верните -1.</p>"
                          + "<p><strong>Пример:</strong> Допустим nums = [1, 2, 3, 4, 5], target = 3, функция должна вернуть 2.</p>"
                          + "<p><strong>Примечание:</strong> Предположим, что все элементы в массиве уникальны и массив отсортирован в порядке возрастания.</p>"
                    },
                    new ChallengeDescription
                    {
                        DescriptionId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("ae559f53-e4f4-464d-84c7-f666b61ce22d"),
                        Language = "kz",
                        HtmlContent = "<p>Сәйкестендірілген бүтіндер массивында берілген мақсатты бүтіндінің индексін табу үшін біртекті іздеуді орындайтын функцияны іске қосыңыз. Егер мақсат табылмаса, -1 қайтарыңыз.</p>"
                          + "<p><strong>Мысал:</strong> Берілген nums = [1, 2, 3, 4, 5], мақсат = 3, функцияның 2 қайтару керек.</p>"
                          + "<p><strong>Ескерту:</strong> Массивтегі барлық элементтер бірегей және массив өсу ретімен сұрыпталған деп болжаңыз.</p>"
                    },
                    new ChallengeDescription
                    {
                        DescriptionId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("ae559f53-e4f4-464d-84c7-f666b61ce22d"),
                        Language = "cn",
                        HtmlContent = "<p>实现一个函数，对一个有序的整数数组进行二分查找，以找到给定目标整数的索引。如果没有找到目标，返回-1。</p>"
                          + "<p><strong>示例：</strong>给定 nums = [1, 2, 3, 4, 5], target = 3，函数应该返回 2。</p>"
                          + "<p><strong>注：</strong>假设数组中的所有元素都是唯一的，并且数组按升序排序。</p>"
                    }
                },
                Solutions = new List<ChallengeSolution>
                {
                    new ChallengeSolution
                    {
                        SolutionId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("ae559f53-e4f4-464d-84c7-f666b61ce22d"),
                        Language = "us",
                        HtmlContent = "<p>Binary search is an efficient algorithm for finding an item from a sorted list of items. It works by repeatedly dividing in half the portion of the list that could contain the item, until you've narrowed down the possible locations to just one.</p>"
                          + "<p>Here is a C# implementation of the binary search algorithm:</p>"
                          + "<pre>public int Search(int[] nums, int target) {\n"
                          + "    int left = 0;\n"
                          + "    int right = nums.Length - 1;\n"
                          + "    while (left <= right) {\n"
                          + "        int mid = left + (right - left) / 2;\n"
                          + "        if (nums[mid] == target) return mid;\n"
                          + "        if (nums[mid] < target) left = mid + 1;\n"
                          + "        else right = mid - 1;\n"
                          + "    }\n"
                          + "    return -1;\n"
                          + "}</pre>"
                    },
                    // Solutions in other languages (ru, kz, cn) will follow the same format but translated accordingly.
                    // Placeholder solutions for ru, kz, cn languages similar to the us version but in respective languages.
                },
                CodeTemplates = new List<ChallengeCodeTemplate>
                {
                    new ChallengeCodeTemplate
                    {
                        TemplateId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("ae559f53-e4f4-464d-84c7-f666b61ce22d"),
                        ProgrammingLanguage = "csharp",
                        CodeTemplate = @"public class Solution {
                                public int Search(int[] nums, int target) {
                                    // Write your code here.
                                }
                            }"
                    }
                },
                TestCases = new List<TestCase>
                {
                    new TestCase
                    {
                        TestCaseId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("ae559f53-e4f4-464d-84c7-f666b61ce22d"),
                        InputParameters = new List<InputParameter>
                        {
                            new InputParameter { Type = "int[]", Value = "[1, 2, 3, 4, 5]" },
                            new InputParameter { Type = "int", Value = "3" }
                        },
                        ExpectedResult = new InputParameter { Type = "int", Value = "2" }
                    },
                    new TestCase
                    {
                        TestCaseId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("ae559f53-e4f4-464d-84c7-f666b61ce22d"),
                        InputParameters = new List<InputParameter>
                        {
                            new InputParameter { Type = "int[]", Value = "[1, 2, 3, 4, 5]" },
                            new InputParameter { Type = "int", Value = "6" }
                        },
                        ExpectedResult = new InputParameter { Type = "int", Value = "-1" }
                    },
                    new TestCase
                    {
                        TestCaseId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("ae559f53-e4f4-464d-84c7-f666b61ce22d"),
                        InputParameters = new List<InputParameter>
                        {
                            new InputParameter { Type = "int[]", Value = "[-5, -3, 0, 2, 4, 8]" },
                            new InputParameter { Type = "int", Value = "-3" }
                        },
                        ExpectedResult = new InputParameter { Type = "int", Value = "1" }
                    }
                    // Additional test cases can be added here.
                }

            },

            new Challenge
            {
                ChallengeId = Guid.Parse("d566c97e-beb3-4051-b5da-1e953888cf12"),
                Name = "Find the Duplicate Number",
                Difficulty = "medium",
                Type = "Array Manipulation",
                MainMethodName = "FindDuplicate",
                Descriptions = new List<ChallengeDescription>
                {
                    // English Description
                    new ChallengeDescription
                    {
                        DescriptionId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("d566c97e-beb3-4051-b5da-1e953888cf12"),
                        Language = "us",
                        HtmlContent = "<p>Given an array <code>nums</code> containing <code>n + 1</code> integers where each integer is between 1 and <code>n</code> (inclusive), prove that at least one duplicate number must exist. Assume that only one duplicate number exists and find the duplicate one. You must solve the problem without modifying the array and use only constant, O(1) extra space. The runtime complexity should be less than O(n^2).</p><p><strong>Example:</strong> Given nums = [1, 3, 4, 2, 2], the function should return 2, as the number 2 is the duplicate.</p>"
                    },
                    // Russian Description (Machine-translated)
                    new ChallengeDescription
                    {
                        DescriptionId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("d566c97e-beb3-4051-b5da-1e953888cf12"),
                        Language = "ru",
                        HtmlContent = "<p>Дан массив <code>nums</code>, содержащий <code>n + 1</code> целое число, где каждое целое число находится в диапазоне от 1 до <code>n</code> (включительно). Докажите, что должно существовать хотя бы одно дублирующее число. Предполагается, что существует только одно дублирующее число, найдите его. Задачу нужно решить без модификации массива, используя лишь константное, O(1) дополнительное пространство. Сложность алгоритма должна быть меньше O(n^2).</p><p><strong>Пример:</strong> Для nums = [1, 3, 4, 2, 2], функция должна вернуть 2, так как число 2 является дубликатом.</p>"
                    },
                    // Kazakh Description (Machine-translated)
                    new ChallengeDescription
                    {
                        DescriptionId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("d566c97e-beb3-4051-b5da-1e953888cf12"),
                        Language = "kz",
                        HtmlContent = "<p><code>nums</code> массивінде әрқайсысы 1-ден <code>n</code> аралығына жататын <code>n + 1</code> бүтін сан бар. Кем дегенде бір қайталанатын сан болуы керек екенін дәлелдеңіз. Тек бір қайталанатын сан болды деп болжаңыз және оны табыңыз. Мәселені массивті өзгертпестен және тек қосымша O(1) кеңістік пайдалана отырып шешуіңіз керек. Алгоритмнің уақыт күрделілігі O(n^2)-дан аз болуы тиіс.</p><p><strong>Мысал:</strong> nums = [1, 3, 4, 2, 2] берілгенде, функция 2 санын қайтаруы керек, өйткені 2 саны қайталанады.</p>"
                    },
                    // Chinese Description (Machine-translated)
                    new ChallengeDescription
                    {
                        DescriptionId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("d566c97e-beb3-4051-b5da-1e953888cf12"),
                        Language = "cn",
                        HtmlContent = "<p>给定一个包含 <code>n + 1</code> 个整数的数组 <code>nums</code>，其中每个整数的范围都在 1 到 <code>n</code>（含）之间，请证明至少存在一个重复的数字。假设只有一个重复的数字，请找出这个重复的数字。你必须在不修改数组的情况下解决这个问题，并且只能使用常数级别 O(1) 的额外空间。算法的运行时间应该小于 O(n^2)。</p><p><strong>示例：</strong>给定 nums = [1, 3, 4, 2, 2]，函数应该返回 2，因为 2 是那个重复的数字。</p>"
                    }
                },
                Solutions = new List<ChallengeSolution>
                {
                    // English Solution
                    new ChallengeSolution
                    {
                        SolutionId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("d566c97e-beb3-4051-b5da-1e953888cf12"),
                        Language = "us",
                        HtmlContent = "<p>The solution to the 'Find the Duplicate Number' challenge utilizes Floyd's Tortoise and Hare algorithm (cycle detection). The algorithm employs two pointers moving at different speeds and will eventually meet at the duplicate number due to the cyclical structure caused by the repeat. Refer to the code template for the implementation.</p>"
                    },
                    // The same solution description translated into Russian, Kazakh, and Chinese.
                },
                CodeTemplates = new List<ChallengeCodeTemplate>
                {
                    // Only one entry for C#
                    new ChallengeCodeTemplate
                    {
                        TemplateId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("d566c97e-beb3-4051-b5da-1e953888cf12"),
                        ProgrammingLanguage = "csharp",
                        CodeTemplate = "public class Solution {\n" +
                           "    public int FindDuplicate(int[] nums) {\n" +
                           "        // Implementation goes here.\n" +
                           "    }\n" +
                           "}"
                    }
                },
                TestCases = new List<TestCase>
                {
                    // TestCase 1
                    new TestCase
                    {
                        TestCaseId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("d566c97e-beb3-4051-b5da-1e953888cf12"),
                        InputParameters = new List<InputParameter>
                        {
                            new InputParameter { Type = "int[]", Value = "[1, 3, 4, 2, 2]" }
                        },
                        ExpectedResult = new InputParameter { Type = "int", Value = "2" }
                    },
                    // TestCase 2
                    new TestCase
                    {
                        TestCaseId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("d566c97e-beb3-4051-b5da-1e953888cf12"),
                        InputParameters = new List<InputParameter>
                        {
                            new InputParameter { Type = "int[]", Value = "[3, 1, 3, 4, 2]" }
                        },
                        ExpectedResult = new InputParameter { Type = "int", Value = "3" }
                    },
                    // TestCase 3
                    new TestCase
                    {
                        TestCaseId = Guid.NewGuid(),
                        ChallengeId = Guid.Parse("d566c97e-beb3-4051-b5da-1e953888cf12"),
                        InputParameters = new List<InputParameter>
                        {
                            new InputParameter { Type = "int[]", Value = "[1, 1, 2]" }
                        },
                        ExpectedResult = new InputParameter { Type = "int", Value = "1" }
                    },
                }
            }
        };


    }
}



    

