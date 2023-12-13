using System;

namespace BDMCommandLine
{
	public class ConsoleText
	{
		public static ConsoleColor DefaultForegroundColor { get; set; } = Console.ForegroundColor;
		public static ConsoleColor DefaultBackgroundColor { get; set; } = Console.BackgroundColor;

		public ConsoleColor ForegroundColor { get; set; }
		public ConsoleColor BackgroundColor { get; set; }
		public String Text { get; set; }

		public ConsoleText(String text)
		{
			this.ForegroundColor = ConsoleText.DefaultForegroundColor;
			this.BackgroundColor = ConsoleText.DefaultBackgroundColor;
			this.Text = text;
		}

		public ConsoleText(ConsoleColor foreground, String text)
		{
			this.ForegroundColor = foreground;
			this.BackgroundColor = ConsoleText.DefaultBackgroundColor;
			this.Text = text;
		}

		public ConsoleText(ConsoleColor foreground, ConsoleColor background, String text)
		{
			this.ForegroundColor = foreground;
			this.BackgroundColor = background;
			this.Text = text;
		}

		public static ConsoleText BlankLine() => new(ConsoleText.DefaultForegroundColor, ConsoleText.DefaultBackgroundColor, "\n");

		public static ConsoleText BlankLines(Int32 count) => new(ConsoleText.DefaultForegroundColor, ConsoleText.DefaultBackgroundColor, "\n".Replicate(count));

		public static ConsoleText Default(String text) => new(ConsoleText.DefaultForegroundColor, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText Black(String text) => new(ConsoleColor.Black, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText DarkBlue(String text) => new(ConsoleColor.DarkBlue, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText DarkGreen(String text) => new(ConsoleColor.DarkGreen, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText DarkCyan(String text) => new(ConsoleColor.DarkCyan, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText DarkRed(String text) => new(ConsoleColor.DarkRed, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText DarkMagenta(String text) => new(ConsoleColor.DarkMagenta, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText DarkYellow(String text) => new(ConsoleColor.DarkYellow, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText Gray(String text) => new(ConsoleColor.Gray, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText DarkGray(String text) => new(ConsoleColor.DarkGray, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText Blue(String text) => new(ConsoleColor.Blue, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText Green(String text) => new(ConsoleColor.Green, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText Cyan(String text) => new(ConsoleColor.Cyan, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText Red(String text) => new(ConsoleColor.Red, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText Magenta(String text) => new(ConsoleColor.Magenta, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText Yellow(String text) => new(ConsoleColor.Yellow, ConsoleText.DefaultBackgroundColor, text);
		public static ConsoleText White(String text) => new(ConsoleColor.White, ConsoleText.DefaultBackgroundColor, text);
	}
}
