using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LRSkipAsync;

namespace StackAsync
{
    public class CS_StackAsync
    {
        #region 共有領域
        private List<int> buffer;       // 数値用
        private List<string> strbuf;    // 文字列用
        private CS_LskipAsync lskip;    // 文字列整形
        private CS_RskipAsync rskip;
        #endregion

        #region コンストラクタ
        public CS_StackAsync()
        {   // コンストラクタ
            // 共有領域を初期化する
            buffer = new List<int>();
            strbuf = new List<string>();

            // 文字列整形を用意する
            rskip = new CS_RskipAsync();
            lskip = new CS_LskipAsync();
        }
        #endregion

        #region モジュール
        public async Task PushAsync(int Data)
        {   // 数値情報をスタックに格納
            await Task.Factory.StartNew(() =>
                buffer.Insert(0, Data)            // 要素設定
            );
        }

        public async Task<int> PopAsync()
        {   // 数値情報をスタックから取り出し
            int ret;

            try
            {
                ret = buffer[0];              // 要素取り出し
                buffer.RemoveAt(0);         // 要素情報削除

                return (ret);
            }
            catch (ArgumentOutOfRangeException)
            {   // 空スタックからの情報取り出し
                throw;                      // 呼び出しへthrow
            }
        }

        public async Task<int> QueAsync(int Data)
        {   // 数値情報をＦＩＦＯで取り出し
            int ret;
            int i;

            try
            {
                i = buffer.Count;           // 下限要素の取り出し
                ret = buffer[i - 1];
                buffer.RemoveAt(i - 1);     // 下限要素の削除

                buffer.Insert(0, Data);            // 上限の要素設定

                return (ret);

            }
            catch (ArgumentOutOfRangeException)
            {   // 空スタックからの情報取り出し
                throw;                      // 呼び出しへthrow
            }
        }

        public async Task<int> ViewAsync(int vpos)
        {	// 指定位置の数値情報を取り出し
            return (buffer[vpos]);
        }

        public async Task<int> CountAsync()
        {   // 数値情報スタックから、件数取り出し
            return (buffer.Count());
        }

        public async Task<int> chknumAsync(int Data)
        {   // 登録確認
            return (buffer.IndexOf(Data));
        }

        public async Task SPushAsync(String Sdata)
        {   // 文字列情報をスタックに格納
            String wbuf;

            rskip.Wbuf = Sdata;             // 不要情報を削除
            await rskip.ExecAsync();
            lskip.Wbuf = rskip.Wbuf;
            await lskip.ExecAsync();
            wbuf = lskip.Wbuf;

            if (wbuf != null)
            {   // 設定情報は有るか？
                strbuf.Insert(0, wbuf);            // 要素設定
            }
        }

        public async Task<String> SPopAsync()
        {   // 文字列情報をスタックから取り出し
            String wbuf;

            try
            {
                wbuf = strbuf[0];              // 要素取り出し
                strbuf.RemoveAt(0);         // 要素情報削除

                return (wbuf);
            }
            catch (ArgumentOutOfRangeException)
            {   // 空スタックからの情報取り出し
                throw;                      // 呼び出しへthrow
            }
        }

        public async Task<String> SQueAsync(String Sdata)
        {   // 文字列情報をＦＩＦＯで取り出し
            String wbuf;
            String ret;
            int i;

            rskip.Wbuf = Sdata;             // 不要情報を削除
            await rskip.ExecAsync();
            lskip.Wbuf = rskip.Wbuf;
            await lskip.ExecAsync();
            wbuf = lskip.Wbuf;

            if (wbuf != null)
            {   // 設定情報は有るか？
                try
                {
                    i = strbuf.Count;           // 下限要素の取り出し
                    ret = strbuf[i - 1];
                    strbuf.RemoveAt(i - 1);     // 下限要素の削除

                    strbuf.Insert(0, wbuf);            // 上限の要素設定
                }
                catch (ArgumentOutOfRangeException)
                {   // 空スタックからの情報取り出し
                    throw;                      // 呼び出しへthrow
                }
            }
            else
            {
                ret = null;
            }

            return (ret);
        }

        public async Task<String> SViewAsync(int vpos)
        {   // 指定位置の文字列情報を取り出し
            return (strbuf[vpos]);
        }

        public async Task<int> SCountAsync()
        {   // 数値情報スタックから、件数取り出し
            return (strbuf.Count());
        }

        public async Task<int> chkstrAsync(String Sdata)
        {   // 登録確認
            String wbuf;

            rskip.Wbuf = Sdata;             // 不要情報を削除
            await rskip.ExecAsync();
            lskip.Wbuf = rskip.Wbuf;
            await lskip.ExecAsync();
            wbuf = lskip.Wbuf;

            return (strbuf.IndexOf(wbuf));
        }

        public async Task ClearAsync()
        {   // 作業領域の初期化
            await dclearAsync();
            await sclearAsync();
        }
        #endregion

        #region サブ・モジュール
        private async Task dclearAsync()
        {   // 数値情報管理を初期化する
            await Task.Factory.StartNew(() =>
                buffer.Clear()
            );
        }

        private async Task sclearAsync()
        {   // 文字列情報管理を初期化する
            await Task.Factory.StartNew(() =>
                strbuf.Clear()
            );
        }
        #endregion

    }
}
