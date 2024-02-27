using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_GAMF1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void btn_feladat1_Click(object sender, RoutedEventArgs e)
        {
            await  loadNumbers();
        }

        async Task loadNumbers()
        {
            var startTime = DateTime.Now;
            long[] numbers = await File.ReadAllLinesAsync("./files/szamok.txt").ContinueWith(res => res.Result.Select(long.Parse).ToArray());
            var coprime = numbers.Where(x => IsCoPrime(x, 1310438493L)).Count();
            var anagram = numbers.Where(x => areAnagrams(x, 2354211341L)).Count();
            var frequentDigits = numbers.SelectMany(x => TwoDigits(x)).ToList().GroupBy(x => x).OrderByDescending(x => x.Count()).Select(x => x.Key).First();
            var endTime = DateTime.Now;
            var peformanceTime = endTime - startTime;
            lb_elso.Content = $"a): {coprime}\nb): {anagram}\nc): {frequentDigits}\nidő: {peformanceTime.Milliseconds} ms";

        }
        //O(n) time complexity
        List<int> TwoDigits(long number)
        {
           string num = number.ToString();
           List<int> result = new();
           for(int i = 0; i < num.Length -1;i++)
           {
                result.Add(int.Parse($"{num[i]}{num[i+1]}"));
           }
            return result;
        }

        bool areAnagrams(long a, long b)
        {
            string number_1 = a.ToString();
            string number_2 = b.ToString();
            var sorted_1 = number_1.OrderBy(c => c).ToArray();
            var sorted_2 = number_2.OrderBy(c => c).ToArray();
            return sorted_1.SequenceEqual(sorted_2);
        }

        bool IsCoPrime(long a, long b)
        {
            long ld, ldRem;
            while(b > 0)
            {
                ld = a / b;
                ldRem = a % b;
                a = b;
                b = ldRem;

            }
            return a == 1;
        }

       
    }
}
