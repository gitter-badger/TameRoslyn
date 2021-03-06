// Copyright (c) Oleg Zudov. All Rights Reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Zu.TameRoslyn.Syntax
{
    public partial class TameXmlTextAttributeSyntax : TameXmlAttributeSyntax
    {
        public new static string TypeName = "XmlTextAttributeSyntax";
        private TameSyntaxTokenList _taTextTokens;
        private SyntaxTokenList _textTokens;
        private bool _textTokensIsChanged;
        private string _textTokensStr;

        public TameXmlTextAttributeSyntax(string code)
        {
            Node = SyntaxFactoryStr.ParseXmlTextAttribute(code);
            AddChildren();
        }

        public TameXmlTextAttributeSyntax(XmlTextAttributeSyntax node)
        {
            Node = node;
            AddChildren();
        }

        public TameXmlTextAttributeSyntax()
        {
            // TextTokensStr = DefaultValues.XmlTextAttributeSyntaxTextTokensStr;
            // NameStr = DefaultValues.XmlTextAttributeSyntaxNameStr;
            // EqualsTokenStr = DefaultValues.XmlTextAttributeSyntaxEqualsTokenStr;
            // StartQuoteTokenStr = DefaultValues.XmlTextAttributeSyntaxStartQuoteTokenStr;
            // EndQuoteTokenStr = DefaultValues.XmlTextAttributeSyntaxEndQuoteTokenStr;
        }

        public override string RoslynTypeName => TypeName;

        public SyntaxTokenList TextTokens
        {
            get
            {
                if (_textTokensIsChanged)
                {
                    _textTokens = SyntaxFactoryStr.ParseSyntaxTokenList(TextTokensStr);
                    _textTokensIsChanged = false;
                    _taTextTokens = null;
                }
                else if (_taTextTokens != null && _taTextTokens.IsChanged)
                {
                    _textTokens = _taTextTokens.Node;
                }
                return _textTokens;
            }
            set
            {
                if (_textTokens != value)
                {
                    _textTokens = value;
                    _textTokensIsChanged = false;
                    IsChanged = true;
                }
            }
        }

        public string TextTokensStr
        {
            get
            {
                if (_taTextTokens != null && _taTextTokens.IsChanged)
                    TextTokens = _taTextTokens.Node;
                if (_textTokensIsChanged) return _textTokensStr;
                return _textTokensStr = _textTokens.ToFullString();
            }
            set
            {
                if (_taTextTokens != null && _taTextTokens.IsChanged)
                {
                    TextTokens = _taTextTokens.Node;
                    _textTokensStr = _textTokens.ToFullString();
                }
                if (_textTokensStr != value)
                {
                    _textTokensStr = value;
                    IsChanged = true;
                    _textTokensIsChanged = true;
                    _taTextTokens = null;
                }
            }
        }

        public TameSyntaxTokenList TaTextTokens
        {
            get
            {
                if (_taTextTokens == null)
                {
                    _taTextTokens = new TameSyntaxTokenList(TextTokens) {TaParent = this};
                    _taTextTokens.AddChildren();
                }
                return _taTextTokens;
            }
            set
            {
                if (_taTextTokens != value)
                {
                    _taTextTokens = value;
                    if (_taTextTokens != null)
                    {
                        _taTextTokens.TaParent = this;
                        _taTextTokens.IsChanged = true;
                    }
                    else
                    {
                        IsChanged = true;
                    }
                }
            }
        }

        public override void Clear()
        {
            base.Clear();
            _taTextTokens = null;
        }

        public new void AddChildren()
        {
            base.AddChildren();
            Kind = Node.Kind();
            _textTokens = ((XmlTextAttributeSyntax) Node).TextTokens;
            _textTokensIsChanged = false;
        }

        public override void SetNotChanged()
        {
            base.SetNotChanged();
            IsChanged = false;
        }

        public override SyntaxNode MakeSyntaxNode()
        {
            var res = SyntaxFactory.XmlTextAttribute(Name, EqualsToken, StartQuoteToken, TextTokens, EndQuoteToken);
            IsChanged = false;
            return res;
        }

        public override IEnumerable<TameBaseRoslynNode> GetChildren()
        {
            yield break;
        }

        public override IEnumerable<TameBaseRoslynNode> GetTameFields()
        {
            if (TaName != null) yield return TaName;
        }

        public override IEnumerable<(string filedName, string value)> GetStringFields()
        {
            yield return ("TextTokensStr", TextTokensStr);
            yield return ("NameStr", NameStr);
            yield return ("EqualsTokenStr", EqualsTokenStr);
            yield return ("StartQuoteTokenStr", StartQuoteTokenStr);
            yield return ("EndQuoteTokenStr", EndQuoteTokenStr);
        }
    }
}