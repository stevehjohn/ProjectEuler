using System.Text;
using JetBrains.Annotations;
using ProjectEuler.Infrastructure;

namespace ProjectEuler.Solutions;

[UsedImplicitly]
public class Puzzle0836 : Puzzle
{
    public override string GetAnswer()
    {
        var html = @"
<p>Let $A$ be an <b>affine plane</b> over a <b>radically integral local field</b> $F$ with residual characteristic $p$.</p>

<p>We consider an <b>open oriented line section</b> $U$ of $A$ with normalized Haar measure $m$.</p>

<p>Define $f(m, p)$ as the maximal possible discriminant of the <b>jacobian</b> associated to the <b>orthogonal kernel embedding</b> of $U$ <span style=""white-space:nowrap;"">into $A$.</span></p>

<p>Find $f(20230401, 57)$. Give as your answer the concatenation of the first letters of each bolded word.</p>";

        var split = html.Split('<');

        var result = new StringBuilder();

        foreach (var item in split)
        {
            if (item.StartsWith("b>"))
            {
                var words = item[2..].Split(' ');

                foreach (var word in words)
                {
                    result.Append(word[0]);
                }
            }
        }
        
        return result.ToString();
    }
}