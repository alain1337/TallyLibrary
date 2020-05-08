using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace FileSizes
{
    public static class BigLetters
    {
        public static string[] Render(string s)
        {
            var lines = new string[8];
            foreach (var c in s)
            {
                var i = Map.IndexOf(c);
                if (i < 0)
                    i = Map.Length - 1;
                var fi = i * 8;
                for (var y = 0; y < 8; y++)
                    lines[y] += Font[fi + y];
            }

            return lines;
        }

        const string Map = "0123456789%.";
        static readonly string[] Font = FontRaw
            .Split(Environment.NewLine)
            .Select(s => Regex.Replace(s, @"\$@+$", ""))
            .ToArray();
        const string FontRaw = @"
   ###  $@
  #   # $@
 #     #$@
 #     #$@
 #     #$@
  #   # $@
   ###  $@
        $@@
   #  $@
  ##  $@
 # #  $@
   #  $@
   #  $@
   #  $@
 #####$@
      $@@
  ##### $@
 #     #$@
       #$@
  ##### $@
 #      $@
 #      $@
 #######$@
        $@@
  ##### $@
 #     #$@
       #$@
  ##### $@
       #$@
 #     #$@
  ##### $@
        $@@
 #      $@
 #    # $@
 #    # $@
 #    # $@
 #######$@
      # $@
      # $@
        $@@
 #######$@
 #      $@
 #      $@
 ###### $@
       #$@
 #     #$@
  ##### $@
        $@@
  ##### $@
 #     #$@
 #      $@
 ###### $@
 #     #$@
 #     #$@
  ##### $@
        $@@
 #######$@
 #    # $@
     #  $@
    #   $@
   #    $@
   #    $@
   #    $@
        $@@
  ##### $@
 #     #$@
 #     #$@
  ##### $@
 #     #$@
 #     #$@
  ##### $@
        $@@
  ##### $@
 #     #$@
 #     #$@
  ######$@
       #$@
 #     #$@
  ##### $@
        $@@
 ###   #$@
 # #  # $@
 ### #  $@
    #   $@
   # ###$@
  #  # #$@
 #   ###$@
        $@@
   $@
   $@
   $@
   $@
   $@
   $@
 # $@
   $@@";
    }
}
