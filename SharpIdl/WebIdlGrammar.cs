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

        public virtual Expression Whitespace()
        {
            return +("\t"._() | "\n" | "\r" | " " | ("//" + -(!NewLine() + Peg.Any)) | ("/*" + +(!"*/"._() + Peg.Any) + "*/"));
        }
    }
}