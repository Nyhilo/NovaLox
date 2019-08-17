
using System;

namespace NovaLox
{

    public abstract class Expr {
        public abstract R Accept<R>(IVisitor<R> visitor);
    }


    public interface IVisitor<R> {
        R VisitBinaryExpr(Binary expr);
        R VisitGroupingExpr(Grouping expr);
        R VisitLiteralExpr(Literal expr);
        R VisitUnaryExpr(Unary expr);
    }


    public class Binary : Expr
    {
        public readonly Expr Left;
        public readonly Token Operator;
        public readonly Expr Right;

        public Binary(Expr _left, Token _operator, Expr _right)
        {
            this.Left = _left;
            this.Operator = _operator;
            this.Right = _right;
        }

        public override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitBinaryExpr(this);
        }
    }

    public class Grouping : Expr
    {
        public readonly Expr Expression;

        public Grouping(Expr _expression)
        {
            this.Expression = _expression;
        }

        public override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitGroupingExpr(this);
        }
    }

    public class Literal : Expr
    {
        public readonly Object Value;

        public Literal(Object _value)
        {
            this.Value = _value;
        }

        public override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitLiteralExpr(this);
        }
    }

    public class Unary : Expr
    {
        public readonly Token Operator;
        public readonly Expr Right;

        public Unary(Token _operator, Expr _right)
        {
            this.Operator = _operator;
            this.Right = _right;
        }

        public override R Accept<R>(IVisitor<R> visitor)
        {
            return visitor.VisitUnaryExpr(this);
        }
    }
}