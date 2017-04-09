using Completed;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

/// <summary>
/// Overriding default log handler for Debug.Log to show text on screen
/// </summary>
public class MyFileLogHandler : ILogHandler
{
    private ILogHandler m_DefaultLogHandler = Debug.logger.logHandler;
    public Text bobText;
    public Text elsaText;
    public Text outlawText;                    
    public Text undertakerText;

    public MyFileLogHandler(Text bobText, Text elsaText, Text outlawText, Text undertakerText)
   // public MyFileLogHandler(Text outlawText, Text undertakerText)
    {
        // Replace the default debug log handler
        Debug.logger.logHandler = this;
        this.bobText = bobText;
        this.elsaText = elsaText;
        this.outlawText = outlawText;
        this.undertakerText = undertakerText;
    }

    //public MyFileLogHandler(Text outlawText, Text undertakerText) {
    //    Debug.logger.logHandler = this;
    //    this.outlawText = outlawText;
    //    this.undertakerText= undertakerText;
    //}

    public void LogFormat(LogType logType, UnityEngine.Object context, string format, params object[] args)
    {
         var message = String.Format(format, args);


        if (message.StartsWith("Bob:"))
        {
            bobText.text = message;
        }
        else if (message.StartsWith("Elsa:"))
        {
            elsaText.text = message;
        }
       
          if (message.StartsWith("Outlaw:"))
        {
            outlawText.text = message;
        }
        else if (message.StartsWith("Undertaker:"))
        {
            undertakerText.text = message;
        }

        m_DefaultLogHandler.LogFormat (logType, context, format, args);
    }

    public void LogException(Exception exception, UnityEngine.Object context)
    {
        m_DefaultLogHandler.LogException(exception, context);
    }


}
