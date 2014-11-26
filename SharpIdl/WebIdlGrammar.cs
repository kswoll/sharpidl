using PEG;
using PEG.SyntaxTree;

namespace SharpIdl
{
    public class WebIdlGrammar : Grammar<WebIdlGrammar>
    {
        private static WebIdlGrammar instance = Create();

        public static WebIdlGrammar Instance
        {
            get { return instance; }
        }

        public virtual Expression Base16Integer()
        {
            return "0" + ("x"._() | "X") + +('0'.To('9') | 'A'.To('F') | 'a'.To('f'));
        }

        public virtual Expression Base8Integer()
        {
            return "0" + +'0'.To('8');
        }

        public virtual Expression Base10Integer()
        {
            return +'0'.To('9');
        }

        public virtual Expression Integer()
        {
            return ~"-"._() + (Base16Integer() | Base8Integer() | (!("08"._() | "09") + Base10Integer()));
        }

        public virtual Expression Float()
        {
            return ~"-"._() + (
                (
                    (
                        (-'0'.To('9') + '.' + +'0'.To('9')) | 
                        (+'0'.To('9') + '.' + -'0'.To('9'))
                    ) + 
                    ~(
                        ('e'._() | 'E') + 
                        ~('+'._() | '-') + 
                        +'0'.To('9')
                    )
                ) |
                (
                    +('0'.To('9') + ('e'._() | 'E') + ~('+'._() | '-') + +'0'.To('9'))
                )
            );
        }

        public virtual Expression Identifier()
        {
            return 'a'.To('Z') + +('a'.To('Z') | '0'.To('9'));
        }

        public virtual Expression String()
        {
            return '"' + +(!'"'._() + Peg.Any) + '"';
        }

        public virtual Expression NewLine()
        {
            return "\r\n"._() | "\r" | "\n";
        }

        public virtual Expression WS()
        {
            return +("\t"._() | "\n" | "\r" | " " | ("//" + -(!NewLine() + Peg.Any)) | ("/*" + +(!"*/"._() + Peg.Any) + "*/"));
        }

        public virtual Expression ExtendedAttributeNamedArgList()
        {
            return Identifier() + "=" + Identifier() + "(" + ArgumentList() + ")";
        }

        public virtual Expression ArgumentList()
        {
            return ~Argument() + -(~WS() + "," + ~WS() + Argument());
        }

        public virtual Expression Argument()
        {
            return ExtendedAttributeList() + OptionalOrRequiredArgument();
        }

        public virtual Expression OptionalOrRequiredArgument()
        {
            return "optional"._();// + Type() + ArgumentName() + Default() | Type() + Ellipsis() + ArgumentName(); h
        }

        public virtual Expression ExtendedAttributeList()
        {
            return "[" + ~WS() + ExtendedAttribute() + -(~WS() + "," + ~WS() + ExtendedAttribute());
        }

        public virtual Expression ExtendedAttribute()
        {
            return "(" + ~WS() + ExtendedAttributeInner() + ~WS() + ExtendedAttributeRest() + ~WS() |
                "[" + ~WS() + ExtendedAttributeInner() + ~WS() + "]" + ~WS() + ExtendedAttributeRest() |
                "{" + ~WS() + ExtendedAttributeInner() + ~WS() + "}" + ~WS() + ExtendedAttributeRest() |
                Other() + ExtendedAttributeRest();
        }

        public virtual Expression ExtendedAttributeRest()
        {
            return ExtendedAttribute();
        }

        public virtual Expression ExtendedAttributeInner()
        {
            return "(" + ~WS() + ExtendedAttributeInner() + ~WS() + ")" + ~WS() + ExtendedAttributeInner() |
                "[" + ~WS() + ExtendedAttributeInner() + ~WS() + "]" + ~WS() + ExtendedAttributeInner() |
                "{" + ~WS() + ExtendedAttributeInner() + ~WS() + "}" + ~WS() + ExtendedAttributeInner() |
                OtherOrComma() + ExtendedAttributeInner();
        }

        public virtual Expression Other()
        {
            return Float() | Integer() | String() | "-" | "..." | "." | ":" | ";" | "<" | ">" | 
                "=" | "?" | "Date" | "DOMString" | "Infinity" | "NaN" | "any" | "boolean" | "byte" | "double" |
                "false" | "float" | "long" | "null" | "object" | "octet" | "or" | "optional" | "sequence" | 
                "short" | "true" | "unsigned" | "void" | Identifier() | 
                ArgumentNameKeyword();
        }

        public virtual Expression ArgumentNameKeyword()
        {
            return "argument"._() | "callback" | "const" | "creator" | "deleter" | "dictionary" | "enum" | 
                "exception" | "getter" | "implements" | "inherit" | "interface" | "legacycaller" | "partial" | 
                "setter" | "static" | "stringifier" | "typedef" | "unrestricted";
        }

        public virtual Expression OtherOrComma()
        {
            return Other() | ",";
        }
    }
}