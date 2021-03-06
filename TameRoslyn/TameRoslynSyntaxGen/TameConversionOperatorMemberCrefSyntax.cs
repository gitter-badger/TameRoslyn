// Copyright (c) Oleg Zudov. All Rights Reserved. Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Zu.TameRoslyn.Syntax
{
    public partial class TameConversionOperatorMemberCrefSyntax : TameMemberCrefSyntax
    {
        public new static string TypeName = "ConversionOperatorMemberCrefSyntax";
        private SyntaxToken _implicitOrExplicitKeyword;
        private bool _implicitOrExplicitKeywordIsChanged;
        private string _implicitOrExplicitKeywordStr;
        private SyntaxToken _operatorKeyword;
        private bool _operatorKeywordIsChanged;
        private string _operatorKeywordStr;
        private CrefParameterListSyntax _parameters;
        private bool _parametersIsChanged;
        private string _parametersStr;
        private TameCrefParameterListSyntax _taParameters;
        private TameTypeSyntax _taType;
        private TypeSyntax _type;
        private bool _typeIsChanged;
        private string _typeStr;

        public TameConversionOperatorMemberCrefSyntax(string code)
        {
            Node = SyntaxFactoryStr.ParseConversionOperatorMemberCref(code);
            AddChildren();
        }

        public TameConversionOperatorMemberCrefSyntax(ConversionOperatorMemberCrefSyntax node)
        {
            Node = node;
            AddChildren();
        }

        public TameConversionOperatorMemberCrefSyntax()
        {
            // ImplicitOrExplicitKeywordStr = DefaultValues.ConversionOperatorMemberCrefSyntaxImplicitOrExplicitKeywordStr;
            // OperatorKeywordStr = DefaultValues.ConversionOperatorMemberCrefSyntaxOperatorKeywordStr;
            // TypeStr = DefaultValues.ConversionOperatorMemberCrefSyntaxTypeStr;
            // ParametersStr = DefaultValues.ConversionOperatorMemberCrefSyntaxParametersStr;
        }

        public override string RoslynTypeName => TypeName;

        public SyntaxToken ImplicitOrExplicitKeyword
        {
            get
            {
                if (_implicitOrExplicitKeywordIsChanged)
                {
                    _implicitOrExplicitKeyword = SyntaxFactoryStr.ParseSyntaxToken(ImplicitOrExplicitKeywordStr);
                    _implicitOrExplicitKeywordIsChanged = false;
                }
                return _implicitOrExplicitKeyword;
            }
            set
            {
                if (_implicitOrExplicitKeyword != value)
                {
                    _implicitOrExplicitKeyword = value;
                    _implicitOrExplicitKeywordIsChanged = false;
                    IsChanged = true;
                }
            }
        }

        public string ImplicitOrExplicitKeywordStr
        {
            get
            {
                if (_implicitOrExplicitKeywordIsChanged) return _implicitOrExplicitKeywordStr;
                return _implicitOrExplicitKeywordStr = _implicitOrExplicitKeyword.Text;
            }
            set
            {
                if (_implicitOrExplicitKeywordStr != value)
                {
                    _implicitOrExplicitKeywordStr = value;
                    IsChanged = true;
                    _implicitOrExplicitKeywordIsChanged = true;
                }
            }
        }

        public SyntaxToken OperatorKeyword
        {
            get
            {
                if (_operatorKeywordIsChanged)
                {
                    if (_operatorKeywordStr == null) _operatorKeyword = default(SyntaxToken);
                    else
                        _operatorKeyword =
                            SyntaxFactoryStr.ParseSyntaxToken(_operatorKeywordStr, SyntaxKind.OperatorKeyword);
                    _operatorKeywordIsChanged = false;
                }
                return _operatorKeyword;
            }
            set
            {
                if (_operatorKeyword != value)
                {
                    _operatorKeyword = value;
                    _operatorKeywordIsChanged = false;
                    IsChanged = true;
                }
            }
        }

        public string OperatorKeywordStr
        {
            get
            {
                if (_operatorKeywordIsChanged) return _operatorKeywordStr;
                return _operatorKeywordStr = _operatorKeyword.Text;
            }
            set
            {
                if (_operatorKeywordStr != value)
                {
                    _operatorKeywordStr = value;
                    IsChanged = true;
                    _operatorKeywordIsChanged = true;
                }
            }
        }

        public TypeSyntax Type
        {
            get
            {
                if (_typeIsChanged)
                {
                    _type = SyntaxFactoryStr.ParseTypeSyntax(TypeStr);
                    _typeIsChanged = false;
                    _taType = null;
                }
                else if (_taType != null && _taType.IsChanged)
                {
                    _type = (TypeSyntax) _taType.Node;
                }
                return _type;
            }
            set
            {
                if (_type != value)
                {
                    _type = value;
                    _typeIsChanged = false;
                    IsChanged = true;
                }
            }
        }

        public string TypeStr
        {
            get
            {
                if (_taType != null && _taType.IsChanged)
                    Type = (TypeSyntax) _taType.Node;
                if (_typeIsChanged) return _typeStr;
                return _typeStr = _type?.ToFullString();
            }
            set
            {
                if (_taType != null && _taType.IsChanged)
                {
                    Type = (TypeSyntax) _taType.Node;
                    _typeStr = _type?.ToFullString();
                }
                if (_typeStr != value)
                {
                    _typeStr = value;
                    IsChanged = true;
                    _typeIsChanged = true;
                    _taType = null;
                }
            }
        }

        public TameTypeSyntax TaType
        {
            get
            {
                if (_taType == null && Type != null)
                    if (Type is IdentifierNameSyntax)
                    {
                        var loc = new TameIdentifierNameSyntax((IdentifierNameSyntax) Type) {TaParent = this};
                        loc.AddChildren();
                        _taType = loc;
                    }
                    else if (Type is GenericNameSyntax)
                    {
                        var loc = new TameGenericNameSyntax((GenericNameSyntax) Type) {TaParent = this};
                        loc.AddChildren();
                        _taType = loc;
                    }
                    else if (Type is AliasQualifiedNameSyntax)
                    {
                        var loc = new TameAliasQualifiedNameSyntax((AliasQualifiedNameSyntax) Type) {TaParent = this};
                        loc.AddChildren();
                        _taType = loc;
                    }
                    else if (Type is QualifiedNameSyntax)
                    {
                        var loc = new TameQualifiedNameSyntax((QualifiedNameSyntax) Type) {TaParent = this};
                        loc.AddChildren();
                        _taType = loc;
                    }
                    else if (Type is OmittedTypeArgumentSyntax)
                    {
                        var loc = new TameOmittedTypeArgumentSyntax((OmittedTypeArgumentSyntax) Type) {TaParent = this};
                        loc.AddChildren();
                        _taType = loc;
                    }
                    else if (Type is PredefinedTypeSyntax)
                    {
                        var loc = new TamePredefinedTypeSyntax((PredefinedTypeSyntax) Type) {TaParent = this};
                        loc.AddChildren();
                        _taType = loc;
                    }
                    else if (Type is NullableTypeSyntax)
                    {
                        var loc = new TameNullableTypeSyntax((NullableTypeSyntax) Type) {TaParent = this};
                        loc.AddChildren();
                        _taType = loc;
                    }
                    else if (Type is PointerTypeSyntax)
                    {
                        var loc = new TamePointerTypeSyntax((PointerTypeSyntax) Type) {TaParent = this};
                        loc.AddChildren();
                        _taType = loc;
                    }
                    else if (Type is ArrayTypeSyntax)
                    {
                        var loc = new TameArrayTypeSyntax((ArrayTypeSyntax) Type) {TaParent = this};
                        loc.AddChildren();
                        _taType = loc;
                    }
                    else if (Type is TupleTypeSyntax)
                    {
                        var loc = new TameTupleTypeSyntax((TupleTypeSyntax) Type) {TaParent = this};
                        loc.AddChildren();
                        _taType = loc;
                    }
                    else if (Type is RefTypeSyntax)
                    {
                        var loc = new TameRefTypeSyntax((RefTypeSyntax) Type) {TaParent = this};
                        loc.AddChildren();
                        _taType = loc;
                    }
                return _taType;
            }
            set
            {
                if (_taType != value)
                {
                    _taType = value;
                    if (_taType != null)
                    {
                        _taType.TaParent = this;
                        _taType.IsChanged = true;
                    }
                    else
                    {
                        IsChanged = true;
                    }
                }
            }
        }

        public CrefParameterListSyntax Parameters
        {
            get
            {
                if (_parametersIsChanged)
                {
                    _parameters = SyntaxFactoryStr.ParseCrefParameterListSyntax(ParametersStr);
                    _parametersIsChanged = false;
                    _taParameters = null;
                }
                else if (_taParameters != null && _taParameters.IsChanged)
                {
                    _parameters = (CrefParameterListSyntax) _taParameters.Node;
                }
                return _parameters;
            }
            set
            {
                if (_parameters != value)
                {
                    _parameters = value;
                    _parametersIsChanged = false;
                    IsChanged = true;
                }
            }
        }

        public string ParametersStr
        {
            get
            {
                if (_taParameters != null && _taParameters.IsChanged)
                    Parameters = (CrefParameterListSyntax) _taParameters.Node;
                if (_parametersIsChanged) return _parametersStr;
                return _parametersStr = _parameters?.ToFullString();
            }
            set
            {
                if (_taParameters != null && _taParameters.IsChanged)
                {
                    Parameters = (CrefParameterListSyntax) _taParameters.Node;
                    _parametersStr = _parameters?.ToFullString();
                }
                if (_parametersStr != value)
                {
                    _parametersStr = value;
                    IsChanged = true;
                    _parametersIsChanged = true;
                    _taParameters = null;
                }
            }
        }

        public TameCrefParameterListSyntax TaParameters
        {
            get
            {
                if (_taParameters == null && Parameters != null)
                {
                    _taParameters = new TameCrefParameterListSyntax(Parameters) {TaParent = this};
                    _taParameters.AddChildren();
                }
                return _taParameters;
            }
            set
            {
                if (_taParameters != value)
                {
                    _taParameters = value;
                    if (_taParameters != null)
                    {
                        _taParameters.TaParent = this;
                        _taParameters.IsChanged = true;
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
            _taType = null;
            _taParameters = null;
        }

        public new void AddChildren()
        {
            base.AddChildren();
            Kind = Node.Kind();
            _implicitOrExplicitKeyword = ((ConversionOperatorMemberCrefSyntax) Node).ImplicitOrExplicitKeyword;
            _implicitOrExplicitKeywordIsChanged = false;
            _operatorKeyword = ((ConversionOperatorMemberCrefSyntax) Node).OperatorKeyword;
            _operatorKeywordIsChanged = false;
            _type = ((ConversionOperatorMemberCrefSyntax) Node).Type;
            _typeIsChanged = false;
            _parameters = ((ConversionOperatorMemberCrefSyntax) Node).Parameters;
            _parametersIsChanged = false;
        }

        public override void SetNotChanged()
        {
            base.SetNotChanged();
            IsChanged = false;
        }

        public override SyntaxNode MakeSyntaxNode()
        {
            var res =
                SyntaxFactory.ConversionOperatorMemberCref(ImplicitOrExplicitKeyword, OperatorKeyword, Type,
                    Parameters);
            IsChanged = false;
            return res;
        }

        public override IEnumerable<TameBaseRoslynNode> GetChildren()
        {
            yield break;
        }

        public override IEnumerable<TameBaseRoslynNode> GetTameFields()
        {
            if (TaType != null) yield return TaType;
            if (TaParameters != null) yield return TaParameters;
        }

        public override IEnumerable<(string filedName, string value)> GetStringFields()
        {
            yield return ("ImplicitOrExplicitKeywordStr", ImplicitOrExplicitKeywordStr);
            yield return ("OperatorKeywordStr", OperatorKeywordStr);
            yield return ("TypeStr", TypeStr);
            yield return ("ParametersStr", ParametersStr);
        }
    }
}