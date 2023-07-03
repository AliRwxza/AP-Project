using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp3
{
    public enum PackageStatus
    {
        /// <summary>
        /// ثبت شده
        /// </summary>
        Submitted,
        /// <summary>
        /// آماده ارسال
        /// </summary>
        ReadyToSend,
        /// <summary>
        /// در حال ارسال
        /// </summary>
        OnTheWay,
        /// <summary>
        /// تحویل داده شده
        /// </summary>
        Delivered
    }
}
