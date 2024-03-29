##############
# Structures #
##############
class Ast:
    def __init__(self, name, args):
        self.Name = name
        if ("list" in str(type(args)) ):
            self.Args = args
        else:
            self.Args = [args]

    def class_properties(self):
        return "\n".join([f"        public {arg.Type} {arg.Name} {{ get; }}" for arg in self.Args])

    def ctor_parameters(self):
        return ", ".join([f"{arg.Type} {arg.Prop}" for arg in self.Args])

    def ctor_setters(self):
        return "\n".join([f"{' '*12}{arg.Name} = {arg.Prop};" for arg in self.Args])


class Arg:
    def __init__(self, _type, name):
        self.Type = _type
        self.Name = name
        self.Prop = "op" if name == "Operator" else name.lower()


def visitor_class(baseClass, asts):
    interfaceMethods = "\n".join([f"        R Visit{ast.Name}{baseClass}({ast.Name} expr);" for ast in asts])

    return f"""
    public interface IVisitor<R> {{
{interfaceMethods}
    }}
"""


###########
# Globals #
###########
DEBUG = False

baseClass = "Expr"

asts = [
    Ast("Binary", [
            Arg("Expr","Left"),
            Arg("Token", "Operator"),
            Arg("Expr", "Right")]),

    Ast("Grouping", Arg("Expr", "Expression")),

    Ast("Literal", Arg("Object", "Value")),

    Ast("Unary", [
            Arg("Token","Operator"),
            Arg("Expr", "Right")]),
]



###################
# String Building #
###################

# Setup
astfile = f"""
using System;

namespace NovaLox
{{

    public abstract class {baseClass} {{
        public abstract R Accept<R>(IVisitor<R> visitor);
    }}

{visitor_class(baseClass, asts)}
"""

# Work
for ast in asts:
    astfile +=f"""
    public class {ast.Name} : Expr
    {{
{ast.class_properties()}

        public {ast.Name}({ast.ctor_parameters()})
        {{
{ast.ctor_setters()}
        }}

        public override R Accept<R>(IVisitor<R> visitor)
        {{
            return visitor.Visit{ast.Name}{baseClass}(this);
        }}
    }}
"""

# Finally
astfile += "}"


###########
# File IO #
###########
if __name__ == '__main__':
    if DEBUG:
        print(astfile)
    else:
        with open("Expr.cs", "w+") as file:
            file.write(astfile)