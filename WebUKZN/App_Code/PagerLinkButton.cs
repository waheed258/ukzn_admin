﻿using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
namespace App_Code
{
    /// <summary>
    /// Summary description for PagerLinkButton
    /// </summary>
    public class PagerLinkButton : LinkButton
    {
        public PagerLinkButton(IPostBackContainer container)
        {
            _container = container;
        }

        public void EnableCallback(string argument)
        {
            _enableCallback = true;
            _callbackArgument = argument;
        }

        public override bool CausesValidation
        {
            get { return false; }
            set { throw new ApplicationException("Cannot set validation on pager buttons"); }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            SetCallbackProperties();
            base.Render(writer);
        }

        private void SetCallbackProperties()
        {
            if (_enableCallback)
            {
                ICallbackContainer container = _container as ICallbackContainer;
                if (container != null)
                {
                    string callbackScript = container.GetCallbackScript(this, _callbackArgument);
                    if (!string.IsNullOrEmpty(callbackScript)) OnClientClick = callbackScript;
                }
            }
        }

        #region Private fields

        private readonly IPostBackContainer _container;
        private bool _enableCallback;
        private string _callbackArgument;

        #endregion
    }
}