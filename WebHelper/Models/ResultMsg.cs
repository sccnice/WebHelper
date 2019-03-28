using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebHelper.Models
{
    public class ResultMsg
    {
        public ResultMsg(bool resultState) {
            ResultState = resultState;
          if (!ResultState)  Msg = "出错了！请检查";
            else Msg = "操作成功！";
        }
        public bool ResultState { get; set; }
        public string Code { get; set; }
        public string Msg { get; set; }
        public dynamic Data { get; set; }
    }
}
