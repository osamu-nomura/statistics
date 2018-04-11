using Microsoft.VisualStudio.TestTools.UnitTesting;
using hsb.Statistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.Statistics.Tests
{
    #region 【Test Class】
    [TestClass()]
    public class StatisticsTests
    {
        #region - AverageTest
        /// <summary>
        /// Test of Average
        /// </summary>
        [TestMethod()]
        public void AverageTest()
        {
            var access = new int[]
            {
                354, 351, 344, 362, 327, 349, 361, 360, 333, 366, 343, 349, 358, 373, 334,
                338, 355, 329, 370, 324, 387, 370, 357, 342, 338, 320, 359, 308, 323, 338
            };
            Func<int, double> cv = (n) => (double)n;
            Assert.AreEqual(347.4d, Statistics.Average(access, cv));
            Assert.AreEqual(347.6d, Statistics.Average(access.Take(5), cv));
        }
        #endregion

        #region - TrimedMeanTest
        /// <summary>
        /// Test of TrimedMean
        /// </summary>
        [TestMethod()]
        public void TrimedMeanTest()
        {
            // 末尾の0,0はダミー
            var sales = new int[]
            {
                2024, 2164, 6465, 2186, 2348, 1981, 2256, 3177, 1861, 3249,
                2464, 1975, 2496, 3246, 2465, 1654, 2654, 3321, 6612, 3189,
                0, 0
            };
            Func<int, double> cv = (n) => (double)n;
            Assert.AreEqual(2889, Math.Round(Statistics.Average(sales.Take(20), cv), 0));
            Assert.AreEqual(2484, Math.Round(Statistics.TrimedMean(sales, cv, 0.2d), 0));
        }
        #endregion

        #region - MedianTest
        /// <summary>
        /// Test of Median
        /// </summary>
        [TestMethod()]
        public void MedianTest()
        {
            var sales = new int[]
            {
                2024, 2164, 6465, 2186, 2348, 1981, 2256, 3177, 1861, 3249,
                2464, 1975, 2496, 3246, 2465, 1654, 2654, 3321, 6612, 3189
            };
            Func<int, double> cv = (n) => (double)n;
            Assert.AreEqual(2464, Math.Round(Statistics.Median(sales, cv), 0));
        }
        #endregion

        #region - GeometricMeanTest
        /// <summary>
        /// Test of GeometricMean
        /// </summary>
        [TestMethod()]
        public void GeometricMeanTest()
        {
            var rate = new double[] { 1 + 0.2d, 1 - 0.3d, 1 + 0.1d };
            Assert.AreEqual(0.974d, Math.Round(Statistics.GeometricMean(rate, (n) => n), 3, MidpointRounding.AwayFromZero));
        }
        #endregion

        #region - HarmonicMeanTest
        /// <summary>
        /// Test of HarmonicMean
        /// </summary>
        [TestMethod()]
        public void HarmonicMeanTest()
        {
            var rate = new int[] { 110, 90 };
            Assert.AreEqual(99d, Math.Round(Statistics.HarmonicMean(rate, (n) => (double)n), 0, MidpointRounding.AwayFromZero));
        }
        #endregion

        #region - HistogramTest
        /// <summary>
        /// Test of Histogram
        /// </summary>
        [TestMethod()]
        public void HistogramTest()
        {
            var access = new int[]
            {
                354, 351, 344, 362, 327, 349, 361, 360, 333, 366, 343, 349, 358, 373, 334,
                338, 355, 329, 370, 324, 387, 370, 357, 342, 338, 319, 359, 308, 323, 338
            };
            var expected = new int[]
            {
                0, 1, 1, 4, 5, 5, 6, 4, 3, 1, 0
            };
            Func<int, double> cv = (n) => (double)n;
            Assert.IsTrue(expected.SequenceEqual(
                Statistics.Histogram(access, cv, 290.0d, 10.0d, 11)));
        }
        #endregion

        #region - RelativeFrequencyTest
        /// <summary>
        /// Test of RelativeFrequency
        /// </summary>
        [TestMethod()]
        public void RelativeFrequencyTest()
        {
            var histogram = new int[]
            {
                0, 1, 1, 4, 5, 5, 6, 4, 3, 1, 0
            };
            var expected = new double[]
            {
                0d, 0.033d, 0.033d, 0.133d, 0.167d, 0.167d, 0.2d, 0.133d, 0.1d, 0.033, 0d
            };
            Func<int, double> cv = (n) => (double)n;
            Assert.IsTrue(expected.SequenceEqual(
                Statistics.RelativeFrequency(histogram, cv, 3)));
        }
        #endregion

        #region - ModeTest
        /// <summary>
        /// Test of Mode
        /// </summary>
        [TestMethod()]
        public void ModeTest()
        {
            var values1 = new int[] { 0, 1, 3, 4, 1, 2, 3, 1, 5, 2, 1 };
            Assert.AreEqual(1, Statistics.Mode(values1, v => v));

            var values2 = new string[] { "A", "A", "B", "B", "B", "C", "D" };
            Assert.AreEqual("B", Statistics.Mode(values2, v => v));
        }
        #endregion

        #region - DeviationTest
        /// <summary>
        /// Test of Deviation
        /// </summary>
        [TestMethod()]
        public void DeviationTest()
        {
            var data = new int[]
            {
                51, 63, 63, 90, 48, 72,
                48, 69, 87, 87, 84, 75,
                78, 81, 57, 87, 75, 57,
                48, 57, 69, 81, 54, 90,
                78, 66, 66, 72, 78, 69
            };
            var expected = new double[]
            {
                -19d,  -7d,  -7d,  20d, -22d,   2d,
                -22d,  -1d,  17d,  17d,  14d,   5d,
                  8d,  11d, -13d,  17d,   5d, -13d,
                -22d, -13d,  -1d,  11d, -16d,  20d,
                  8d,  -4d,  -4d,   2d,   8d,  -1d
            };
            Assert.IsTrue(expected.SequenceEqual(
                Statistics.Deviation(data, n => (double)n)));
            Assert.IsTrue(expected.SequenceEqual(
                Statistics.Deviation(data, n => (double)n, 70.0d)));
        }
        #endregion

        #region VarianceTest
        /// <summary>
        /// Test of Variance
        /// </summary>
        [TestMethod()]
        public void VarianceTest()
        {
            var data = new int[]
            {
                51, 63, 63, 90, 48, 72,
                48, 69, 87, 87, 84, 75,
                78, 81, 57, 87, 75, 57,
                48, 57, 69, 81, 54, 90,
                78, 66, 66, 72, 78, 69
            };
            Assert.AreEqual(168.8d, Math.Round(
                Statistics.Variance(data, n => (double)n), 1, MidpointRounding.AwayFromZero));
            Assert.AreEqual(168.8d, Math.Round(
                Statistics.Variance(data, n => (double)n, 70.0d), 1, MidpointRounding.AwayFromZero));
        }
        #endregion

        #region - StandardDeviationTest
        /// <summary>
        /// Test of StandardDeviation
        /// </summary>
        [TestMethod()]
        public void StandardDeviationTest()
        {
            var data = new int[]
            {
                51, 63, 63, 90, 48, 72,
                48, 69, 87, 87, 84, 75,
                78, 81, 57, 87, 75, 57,
                48, 57, 69, 81, 54, 90,
                78, 66, 66, 72, 78, 69
            };
            Assert.AreEqual(12.99d, Math.Round(
                Statistics.StandardDeviation(data, n => (double)n), 2, MidpointRounding.AwayFromZero));
        }
        #endregion

        #region - StandardizationTest1
        /// <summary>
        /// Test of Standardization1
        /// </summary>
        [TestMethod()]
        public void StandardizationTest1()
        {
            Assert.AreEqual(1.92d, Math.Round(
                Statistics.Standardization(95.0d, 70.0d, 12.99d), 2, MidpointRounding.AwayFromZero));
        }
        #endregion

        #region - StandardizationTest2
        /// <summary>
        /// Test of Standardization2
        /// </summary>
        [TestMethod()]
        public void StandardizationTest2()
        {
            var data = new int[]
            {
                51, 63, 63, 90, 48, 72,
                48, 69, 87, 87, 84, 75,
                78, 81, 57, 87, 75, 57,
                48, 57, 69, 81, 54, 90,
                78, 66, 66, 72, 78, 69
            };

            var sd = Math.Round(Statistics.StandardDeviation(data, n => (double)n), 2, MidpointRounding.AwayFromZero);
            var expected = data.Select(n => Statistics.Standardization(n, 70.0d, sd))
                               .ToArray();
            var result = Statistics.Standardization(data, n => (double)n, 70.0d, sd);
            Assert.IsTrue(expected.SequenceEqual(result));

        }
        #endregion

    }
    #endregion
}