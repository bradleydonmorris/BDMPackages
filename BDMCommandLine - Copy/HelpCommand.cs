using BDMCommandLine;

namespace BDMCommandLine.Commands
{
    public class HelpCommand : CommandBase
	{
        public HelpCommand()
            : base(
                "Help",
                "Used to display help.",
				"Help [command]",
                "DataMover help [command]",
                ["ssqe"],
                null
            )
        { }

        public override void Execute()
        {
            base.DataLayer = new SQLServerDataLayer(base.Arguments["SQLServerConnectionString".ToLower()].GetValue());
			base.Execute();
        }
    }
}
