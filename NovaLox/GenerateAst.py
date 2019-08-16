# Glorified dictionaries
class Ast:
    def __init__(self, name, args):
        self.Name = name
        if ("list" in str(type(args)) ):
            self.Args = args
        else:
            self.Args = [args]


class Arg:
    def __init__(self, _type, name):
        self.Type = _type
        self.Name = name

# Globals
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


# Setup
astfile = f"""
using System;

namespace NovaLox
{{

    public abstract class {baseClass} {{ }}
"""

# Work
for ast in asts:
    classProperties = "\n".join([f"        public readonly {arg.Type} {arg.Name};" for arg in ast.Args])
    ctorParams = ", ".join([f"{arg.Type} _{arg.Name.lower()}" for arg in ast.Args])
    ctorSetters = "\n".join([f"{' '*12}this.{arg.Name} = _{arg.Name.lower()};" for arg in ast.Args])

    astfile +=f"""
    public class {ast.Name}
    {{
{classProperties}

        public {ast.Name}({ctorParams})
        {{
{ctorSetters}
        }}
    }}
"""

# Finally
astfile += "}"

# And save
with open("Expr.cs", "w+") as file:
    file.write(astfile)