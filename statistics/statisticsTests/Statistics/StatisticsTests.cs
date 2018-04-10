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
    }
    #endregion
}