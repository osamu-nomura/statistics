using System;
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
    }
    #endregion
}
