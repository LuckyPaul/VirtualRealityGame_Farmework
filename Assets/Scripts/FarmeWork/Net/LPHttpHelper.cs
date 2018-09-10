/*

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BestHTTP;
using System;
using System.IO;

namespace LuckyPual.Net
{

    public class LPHttpHepler 
    {

        /// <summary>
        /// get 请求
        /// </summary>
        public void GetRequest(RequestSendMessage requestSendMessage)
        {
            HTTPRequest Request = new HTTPRequest(requestSendMessage.uri, HTTPMethods.Get, requestSendMessage.OnRequestFinished);
            Request.Send();
        }
        ///// <summary>
        ///// 请求回调
        ///// </summary>
        ///// <param name="originalRequest">原始请求</param>
        ///// <param name="response">服务器响应</param>
        //private void OnRequestFinished(HTTPRequest originalRequest, HTTPResponse response)
        //{
        //    Debug.Log("回调");
        //}
        /// <summary>
        /// post 请求
        /// </summary>
        public void PostRequest(RequestSendMessage requestSendMessage)
        {
            HTTPRequest Request = new HTTPRequest(requestSendMessage.uri, HTTPMethods.Post, requestSendMessage.OnRequestFinished);
            Request.Send();
        }

        /// <summary>
        /// 图片下载
        /// </summary>
        /// <param name="requestSendMessage"></param>
        /// <returns></returns>
        public Texture2D DownloadImage(RequestSendMessage requestSendMessage)
        {
            Texture2D resoultTex = new Texture2D(0, 0);
            HTTPRequest Request = new HTTPRequest(requestSendMessage.uri, (request, response) => { resoultTex.LoadImage(response.Data); }).Send();
            return resoultTex;
        }

        /// <summary>
        /// 文本下载
        /// </summary>
        /// <param name="requestSendMessage"></param>
        /// <returns></returns>
        public string DownloadText(RequestSendMessage requestSendMessage)
        {
            string resoultText = null;
            HTTPRequest Request = new HTTPRequest(requestSendMessage.uri);
            Request.Send();
            resoultText = Request.Response.DataAsText;

            return resoultText;
        }


    }

    public class RequestSendMessage
    {
        //public string IP;
        //public string Port;
        //public string Message;

        public Uri uri;
        public OnRequestFinishedDelegate OnRequestFinished;

    }
}
*/