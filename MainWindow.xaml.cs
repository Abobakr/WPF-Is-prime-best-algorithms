// Ignore Spelling: App

using System;
using System.Collections.Generic;
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
using System.Diagnostics;
using System.Security.Cryptography;

namespace IsPrimeNumberComparisonApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<ulong> primes = new List<ulong>() { 2, 3, 5, 7 };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            ulong n = ulong.Parse(textBox.Text);
            if (n < 2)
            {
                lableTime1.Content = "Not Prime";
                return;
            }

            var watch = Stopwatch.StartNew();

            ulong i = 2;
            var end = Math.Sqrt(n);
            while (i <= end && n % i != 0)
                i = i + 1;

            if (i <= end)
                lableStatus1.Content = "Not Prime";
            else lableStatus1.Content = "Prime";


            watch.Stop();

            lableTime1.Content = watch.ElapsedMilliseconds.ToString() + " ms";
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            ulong n = ulong.Parse(textBox.Text);
            if (n < 2)
            {
                lableStatus2.Content = "Not Prime";
                return;
            }
            if (n == 2)
            {


                return;
            }
            if (n % 2 == 0)
            {
                lableStatus2.Content = "Not Prime";
                return;
            }

            var watch = Stopwatch.StartNew();

            ulong i = 3;
            var end = Math.Sqrt(n);
            while (i <= end && n % i != 0)
                i = i + 2;

            if (i <= end)
                lableStatus2.Content = "Not Prime";
            else lableStatus2.Content = "Prime";

            watch.Stop();
            lableTime2.Content = watch.ElapsedMilliseconds.ToString() + " ms";

        }

        private void button3_Click(object sender, RoutedEventArgs e)
        {
            ulong n = ulong.Parse(textBox.Text);

            if (n <= 1)
            {
                lableStatus3.Content = "Not Prime";
                return;
            }

            if (n == 2 || n == 3)
            {
                lableStatus3.Content = "Prime";
                return;
            }

            if (n % 2 == 0 || n % 3 == 0)
            {
                lableStatus3.Content = "Not Prime";
                return;
            }

            var watch = Stopwatch.StartNew();

            // [5],[7] -- [11],[13] -- [17],[19] -- 21,[23] -- [29],[31] -- 35,[37] -- [41],[43]          
            var end = Math.Sqrt(n);
            ulong i = 5;
            while (i <= end && n % i != 0 && n % (i + 2) != 0)
                i = i + 6;

            if (i <= end)
                lableStatus3.Content = "Not Prime";
            else lableStatus3.Content = "Prime";

            watch.Stop();

            lableTime3.Content = watch.ElapsedMilliseconds.ToString() + " ms";


        }

        private void button4_Click(object sender, RoutedEventArgs e)
        {
            ulong n = ulong.Parse(textBox.Text);

            var watch = Stopwatch.StartNew();

            if (!isPrime(n))
                lableStatus4.Content = "Not Prime";
            else
                lableStatus4.Content = "Prime";

            watch.Stop();
            lableTime4.Content = watch.ElapsedMilliseconds.ToString() + " ms";
        }

        bool isPrime(ulong n)
        {
            if (n <= 1)
                return false;

            var last = primes.Count - 1;
            if (primes[last] > n && bnrySrch(primes, n))
                return true;

            for (var p = 0; p < primes.Count; p++)
                if (n % primes[p] == 0)
                    return false;

            var nRoot = Math.Sqrt(n);
            var i = primes[last] + 2; // i is the first odd possible prime

            while (i <= nRoot && n % i != 0)
            {
                var p = 0;
                var iRoot = Math.Sqrt(i);
                while (i % primes[p] != 0 && primes[p] <= iRoot)
                    p++;

                if (primes[p] > iRoot) //i is prime
                    primes.Add(i);

                // Move like this: 7,9,11,13 --[+4]--> 17,19,21,23 --[+4]--> 27,29,31,33
                i = (i % 10 == 3) ? i + 4 : i + 2;
            }
            if (i <= nRoot)
                return false;
            //else
            return true;
        }

        bool bnrySrch(List<ulong> list, ulong key)
        {
            var left = 0;
            var right = list.Count - 1;

            while (left <= right)
            {
                var mid = (left + right) / 2;
                if (list[mid] == key)
                    return true;
                if (key < list[mid])
                    right = mid - 1;
                else left = mid + 1;
            }
            return false;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string text = textBox.Text;
            bool containsNumber;

            if (text == "")
                containsNumber = false;
            else
                containsNumber = text.All(char.IsDigit);

            button1.IsEnabled = containsNumber;
            button2.IsEnabled = containsNumber;
            button3.IsEnabled = containsNumber;
            button4.IsEnabled = containsNumber;
        }
    }

}

