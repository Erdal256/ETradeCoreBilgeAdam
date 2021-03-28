﻿using System;
using System.Collections.Generic;
using System.Text;

namespace AppCore.Business.Models.Results.Bases
{
    public class ExceptionResult : Result
    {
        public ExceptionResult(Exception exception, bool showException = false)
            : base(ResultStatus.Exception,
                  showException == true ?
                  (exception != null ? "Exception: "+ exception.Message + 
                  (exception.InnerException != null ? " | Inner Excption:" + exception.InnerException.Message
                  : "")
                  : "")
                  : "Exception")
        {

        }
        public ExceptionResult() : base(ResultStatus.Exception,"Exception")
        {

        }
    }
    public class ExceptionResult<TResultType>: Result<TResultType>
    {

        public ExceptionResult(Exception exception, bool showException = false)
           : base(ResultStatus.Exception,
                 showException == true ?
                 (exception != null ? "Exception: " + exception.Message +
                 (exception.InnerException != null ? " | Inner Excption:" + exception.InnerException.Message
                 : "")
                 : "")
                 : "Exception",
                 default)
        {

        }
        public ExceptionResult() : base(ResultStatus.Exception, "Exception", default)
        {

        }
    }
}
