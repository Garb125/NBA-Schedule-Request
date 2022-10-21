using System;
using System.Collections.Generic;
using System.Text;

namespace NBA_Schedule_Request
{
    public class Depot
    {
        public string resource { get; set; }
        public Parameters parameters { get; set; }
        public IList<ResultSet> resultSets { get; set; }

    }

    public class Parameters
    {
        public string PlayerOrTeam { get; set; }
        public string LeagueID { get; set; }
        public object Season { get; set; }
        public object SeasonType { get; set; }
        public object TeamID { get; set; }
        public object VsTeamID { get; set; }
        public string PlayerID { get; set; }
        public object GameID { get; set; }
        public object Outcome { get; set; }
        public object Location { get; set; }
        public object DateFrom { get; set; }
        public object DateTo { get; set; }
        public object VsConference { get; set; }
        public object VsDivision { get; set; }
        public object Conference { get; set; }
        public object Division { get; set; }
        public object DraftYear { get; set; }
        public object DraftNumber { get; set; }
        public object DraftRound { get; set; }
        public object DraftTeamID { get; set; }
        public object RookieYear { get; set; }
        public object YearsExperience { get; set; }
        public object SeasonSegment { get; set; }
        public object PORound { get; set; }
        public object StarterBench { get; set; }
        public string GtPTS { get; set; }
        public object GtREB { get; set; }
        public object GtAST { get; set; }
        public object GtSTL { get; set; }
        public object GtBLK { get; set; }
        public object GtOREB { get; set; }
        public object GtDREB { get; set; }
        public object GtDD { get; set; }
        public object GtTD { get; set; }
        public object GtMINUTES { get; set; }
        public object GtTOV { get; set; }
        public object GtPF { get; set; }
        public object GtFGM { get; set; }
        public object GtFGA { get; set; }
        public object GtFG_PCT { get; set; }
        public object GtFTM { get; set; }
        public object GtFTA { get; set; }
        public object GtFT_PCT { get; set; }
        public object GtFG3M { get; set; }
        public object GtFG3A { get; set; }
        public object GtFG3_PCT { get; set; }
        public object LtPTS { get; set; }
        public object LtREB { get; set; }
        public object LtAST { get; set; }
        public object LtSTL { get; set; }
        public object LtBLK { get; set; }
        public object LtOREB { get; set; }
        public object LtDREB { get; set; }
        public object LtDD { get; set; }
        public object LtTD { get; set; }
        public object LtMINUTES { get; set; }
        public object LtTOV { get; set; }
        public object LtPF { get; set; }
        public object LtFGM { get; set; }
        public object LtFGA { get; set; }
        public object LtFG_PCT { get; set; }
        public object LtFTM { get; set; }
        public object LtFTA { get; set; }
        public object LtFT_PCT { get; set; }
        public object LtFG3M { get; set; }
        public object LtFG3A { get; set; }
        public object LtFG3_PCT { get; set; }
        public object EqPTS { get; set; }
        public object EqREB { get; set; }
        public object EqAST { get; set; }
        public object EqSTL { get; set; }
        public object EqBLK { get; set; }
        public object EqOREB { get; set; }
        public object EqDREB { get; set; }
        public object EqDD { get; set; }
        public object EqTD { get; set; }
        public object EqMINUTES { get; set; }
        public object EqTOV { get; set; }
        public object EqPF { get; set; }
        public object EqFGM { get; set; }
        public object EqFGA { get; set; }
        public object EqFG_PCT { get; set; }
        public object EqFTM { get; set; }
        public object EqFTA { get; set; }
        public object EqFT_PCT { get; set; }
        public object EqFG3M { get; set; }
        public object EqFG3A { get; set; }
        public object EqFG3_PCT { get; set; }


    }

    public class ResultSet
    {
        public string name { get; set; }
        public IList<string> headers { get; set; }
        public IList<IList<object>> rowSet { get; set; }
    }


}

