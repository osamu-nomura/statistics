﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace hsb.Statistics
{
    #region 【Statistics】
    /// <summary>
    /// 統計ライブラリ
    /// </summary>
    public static class Statistics
    {
        #region - Average : 平均を求める
        /// <summary>
        /// 平均を求める
        /// </summary>
        /// <typeparam name="T">型パラメータ</typeparam>
        /// <param name="source">投入データ</param>
        /// <param name="convert">投入データから平均値を求める値を返す関数</param>
        /// <returns>平均値</returns>
        public static double Average<T>(this IEnumerable<T> source, Func<T, double> convert)
        {
            var count = 0;
            var sum = 0d;
            foreach (var data in source)
            {
                count++;
                sum += convert(data);
            }
            return sum / count;
        }
        #endregion

        #region - TrimedMean : 刈り込み平均を取得する
        /// <summary>
        /// 刈り込み平均を取得する
        /// </summary>
        /// <typeparam name="T">型パラメータ</typeparam>
        /// <param name="source">投入データ</param>
        /// <param name="convert">投入データから平均値を求める値を返す関数</param>
        /// <param name="rate">刈り込み率</param>
        /// <returns>平均値</returns>
        public static double TrimedMean<T>(this IEnumerable<T> source, Func<T, double> convert, double rate)
        {
            var sortedList = source.Select(convert).OrderBy(d => d).ToArray();
            var size = sortedList.Count();
            var n = (int)Math.Floor(size * rate / 2.0d);
            var sum = 0d;
            for (var i = n; i < (size - n); i++)
            {
                sum += sortedList[i];
            }
            return sum / (size - (n * 2));
        }
        #endregion

        #region - Median : 中央値を取得する
        /// <summary>
        /// 中央値を取得する
        /// </summary>
        /// <typeparam name="T">型パラメータ</typeparam>
        /// <param name="source">投入データ</param>
        /// <param name="convert">投入データから中央値を求める値を返す関数</param>
        /// <returns>中央値</returns>
        public static double Median<T>(this IEnumerable<T> source, Func<T, double> convert)
        {
            var sortedList = source.Select(convert).OrderBy(d => d).ToArray();
            var size = sortedList.Count();
            if (size % 2 == 0)
                // 要素が偶数なら中央の２つの平均値を返す
                return (sortedList[size / 2 - 1] + sortedList[size / 2]) / 2.0d;
            else
                return sortedList[size / 2];
        }
        #endregion

        #region - Mode : 最頻値を返す
        /// <summary>
        /// 最頻値を返す
        /// </summary>
        /// <typeparam name="T1">型パラメータ1</typeparam>
        /// <typeparam name="T2">型パラメータ2</typeparam>
        /// <param name="source">投入データ</param>
        /// <param name="convert">投入データから最頻値を求める値を返す関数</param>
        /// <returns>最頻値</returns>
        public static T2 Mode<T1,T2>(this IEnumerable<T1> source, Func<T1, T2> convert)
        {
            var dict = new Dictionary<T2, int>();
            foreach (var data in source)
            {
                var v = convert(data);
                if (dict.ContainsKey(v))
                    dict[v]++;
                else
                    dict.Add(v, 1);
            }
            return dict.OrderByDescending(kv => kv.Value).First().Key;
        }
        #endregion

        #region - GeometricMean : 幾何平均を取得する
        /// <summary>
        ///  幾何平均を取得する
        /// </summary>
        /// <typeparam name="T">型パラメータ</typeparam>
        /// <param name="source">投入データ</param>
        /// <param name="convert">投入データから平均値を求める値を返す関数</param>
        /// <returns>平均値</returns>
        public static double GeometricMean<T>(this IEnumerable<T> source, Func<T, double> convert)
        {
            var sum = 1d;
            var count = 0;
            foreach (var data in source)
            {
                count++;
                sum *= convert(data);
            }
            return Math.Pow(sum, (1.0d / count));
        }
        #endregion

        #region - HarmonicMean : 調和平均を取得する。
        /// <summary>
        /// 調和平均を取得する。
        /// </summary>
        /// <typeparam name="T">型パラメータ</typeparam>
        /// <param name="source">投入データ</param>
        /// <param name="convert">投入データから平均値を求める値を返す関数</param>
        /// <returns>平均値</returns>
        public static double HarmonicMean<T>(this IEnumerable<T> source, Func<T, double> convert)
        {
            var sum = 0d;
            var count = 0;
            foreach (var data in source)
            {
                count++;
                sum += (1.0d / convert(data));
            }
            return count / sum;
        }
        #endregion

        #region - Histogram : ヒストグラムを取得する
        /// <summary>
        /// ヒストグラムを取得する
        /// </summary>
        /// <typeparam name="T">型パラメータ</typeparam>
        /// <param name="source">投入データ</param>
        /// <param name="convert">投入データからヒストグラム求める値を返す関数</param>
        /// <param name="start">開始値</param>
        /// <param name="step">度数</param>
        /// <param name="size">個数</param>
        /// <returns>ヒストグラム</returns>
        public static int[] Histogram<T>(this IEnumerable<T> source, Func<T, double> convert,
                                                 double start, double step, int size)
        {
            var histogram = new int[size];
            foreach (var data in source)
            {
                var i = (int)Math.Floor((convert(data) - start) / step);
                if (i >= 0 && i < size)
                    histogram[i]++;
            }
            return histogram;
        }
        #endregion

        #region - RelativeFrequency : 相対度数を取得する　
        /// <summary>
        /// 相対度数を取得する
        /// </summary>
        /// <typeparam name="T">型パラメータ</typeparam>
        /// <param name="source">投入データ</param>
        /// <param name="convert">投入データから相対度数を求める値を返す関数</param>
        /// <returns>相対度数</returns>
        public static double[] RelativeFrequency<T>(this IEnumerable<T> source, Func<T, double> convert, int digits = 2)
        {
            // 相対度数＝値÷合計
            var sum = source.Sum(convert);
            return source.Select(data => Math.Round(convert(data) / sum, digits)).ToArray();
        }
        #endregion

        #region -  Deviation : 偏差を取得する
        /// <summary>
        /// 偏差を取得する
        /// </summary>
        /// <typeparam name="T">型パラメータ</typeparam>
        /// <param name="source">投入データ</param>
        /// <param name="convert">投入データから偏差を求める値を返す関数</param>
        /// <param name="average">投入データの平均値(オプション)</param>
        /// <returns>偏差のリスト</returns>
        public static double[] Deviation<T>(IEnumerable<T> source, Func<T, double> convert, double? average = null)
        {
            if (average == null)
                average = Average(source, convert);
            return source.Select(data => convert(data) - average.Value).ToArray();
        }
        #endregion

        #region - Variance : 分散を取得する
        /// <summary>
        /// 分散を取得する
        /// </summary>
        /// <typeparam name="T">型パラメータ</typeparam>
        /// <param name="source">投入データ</param>
        /// <param name="convert">投入データから分散を求める値を返す関数</param>
        /// <param name="average">投入データの平均値(オプション)</param>
        /// <returns>分散</returns>
        public static double Variance<T>(IEnumerable<T> source, Func<T, double> convert, double? average = null)
        {
            var deviations = Deviation(source, convert, average);
            return deviations.Sum(d => Math.Pow(d, 2)) / deviations.Count();
        }
        #endregion

        #region - StandardDeviation : 標準偏差を取得する
        /// <summary>
        /// 標準偏差を取得する
        /// </summary>
        /// <typeparam name="T">型パラメータ</typeparam>
        /// <param name="source">投入データ</param>
        /// <param name="convert">投入データから標準偏差を求める値を返す関数</param>
        /// <param name="average">投入データの平均値(オプション)</param>
        /// <returns>標準偏差</returns>
        public static double StandardDeviation<T>(IEnumerable<T> source, Func<T, double> convert, double? average = null)
        {
            return Math.Sqrt(Variance(source, convert, average));
        }
        #endregion

        #region - Standardization : 標準化データを取得する
        /// <summary>
        /// 標準化データを取得する
        /// </summary>
        /// <typeparam name="T">型パラメータ</typeparam>
        /// <param name="source">投入データ</param>
        /// <param name="convert">投入データから標準化データを求める値を返す関数</param>
        /// <param name="average">投入データの平均値(オプション)</param>
        /// <returns>標準化データのリスト</returns>
        public static double[] Standardization<T>(IEnumerable<T> source, Func<T, double> convert, double? average = null, double? sd = null)
        {
            var deviations = Deviation(source, convert, average);
            if (sd == null)
                sd = Math.Sqrt(deviations.Sum(d => Math.Pow(d, 2)) / deviations.Count());
            return deviations.Select(n => n / sd.Value).ToArray();
        }
        #endregion

        #region - Standardization : 標準化データを取得する
        /// <summary>
        /// 標準化データを取得する
        /// </summary>
        /// <param name="value">投入値</param>
        /// <param name="average">平均値</param>
        /// <param name="sd">標準偏差</param>
        /// <returns>標準化データ</returns>
        public static double Standardization(double value, double average, double sd)
        {
            return (value - average) / sd;
        }
        #endregion
    }
    #endregion
}
